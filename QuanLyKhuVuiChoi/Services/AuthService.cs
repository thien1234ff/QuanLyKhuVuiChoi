using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Services
{
    public class AuthService
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;

        public static TaiKhoan? CurrentUser { get; private set; }
        public static NhanVien? CurrentEmployee { get; private set; }
        public static bool IsAdmin => CurrentUser != null && CurrentUser.Quyen.Equals("Admin", StringComparison.OrdinalIgnoreCase);

        public AuthService(QuanLyKhuVuiChoiDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public (bool Success, string Message) Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
            }

            var account = _context.TaiKhoans
                .Include(t => t.NhanVien)
                .FirstOrDefault(t => t.TenDangNhap == username.Trim());

            if (account == null)
            {
                return (false, "Tên đăng nhập không tồn tại trong hệ thống.");
            }

            if (!account.TrangThai)
            {
                return (false, "Tài khoản này hiện đang bị khóa. Vui lòng liên hệ quản trị viên.");
            }

            if (account.MatKhau != password.Trim())
            {
                return (false, "Mật khẩu không chính xác.");
            }

            CurrentUser = account;
            CurrentEmployee = account.NhanVien;

            return (true, $"Đăng nhập thành công! Chào mừng {account.NhanVien?.HoTen ?? account.TenDangNhap}.");
        }

        public (bool Success, string Message) Register(string username, string password, string hoTen, string sdt, string? email, string quyen = "NhanVien")
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Tên đăng nhập và mật khẩu không được để trống.");
            }

            if (username.Length < 3)
            {
                return (false, "Tên đăng nhập phải có ít nhất 3 ký tự.");
            }

            if (password.Length < 6)
            {
                return (false, "Mật khẩu phải có ít nhất 6 ký tự.");
            }

            if (_context.TaiKhoans.Any(t => t.TenDangNhap.ToLower() == username.Trim().ToLower()))
            {
                return (false, "Tên đăng nhập này đã tồn tại trong hệ thống.");
            }

            // Tạo nhân viên mới nếu chưa có
            var nextIdNum = _context.NhanViens.Count() + 1;
            var maNV = $"NV{nextIdNum:D3}";
            while (_context.NhanViens.Any(n => n.MaNV == maNV))
            {
                nextIdNum++;
                maNV = $"NV{nextIdNum:D3}";
            }

            var nv = new NhanVien
            {
                MaNV = maNV,
                HoTen = string.IsNullOrWhiteSpace(hoTen) ? username : hoTen.Trim(),
                SoDienThoai = string.IsNullOrWhiteSpace(sdt) ? "0900000000" : sdt.Trim(),
                Email = email?.Trim(),
                ChucVu = quyen == "Admin" ? "Quản lý" : "Nhân viên",
                TrangThai = "Đang làm việc",
                NgayVaoLam = DateTime.Today
            };

            _context.NhanViens.Add(nv);

            var account = new TaiKhoan
            {
                TenDangNhap = username.Trim(),
                MatKhau = password.Trim(),
                MaNV = nv.MaNV,
                Quyen = quyen,
                TrangThai = true
            };

            _context.TaiKhoans.Add(account);
            _context.SaveChanges();

            return (true, "Đăng ký tài khoản thành công! Bạn có thể đăng nhập ngay.");
        }

        public (bool Success, string Message) ForgotPassword(string username, string emailOrPhone, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(emailOrPhone) || string.IsNullOrWhiteSpace(newPassword))
            {
                return (false, "Vui lòng nhập đầy đủ thông tin.");
            }

            if (newPassword.Length < 6)
            {
                return (false, "Mật khẩu mới phải có ít nhất 6 ký tự.");
            }

            var account = _context.TaiKhoans
                .Include(t => t.NhanVien)
                .FirstOrDefault(t => t.TenDangNhap.ToLower() == username.Trim().ToLower());

            if (account == null)
            {
                return (false, "Không tìm thấy tên đăng nhập này trong hệ thống.");
            }

            // Kiểm tra email hoặc số điện thoại của nhân viên
            var matched = false;
            if (account.NhanVien != null)
            {
                if (!string.IsNullOrWhiteSpace(account.NhanVien.Email) && account.NhanVien.Email.Trim().Equals(emailOrPhone.Trim(), StringComparison.OrdinalIgnoreCase))
                    matched = true;
                if (!string.IsNullOrWhiteSpace(account.NhanVien.SoDienThoai) && account.NhanVien.SoDienThoai.Trim() == emailOrPhone.Trim())
                    matched = true;
            }

            // Cho phép tài khoản admin mặc định nếu chưa gán thông tin
            if (!matched && account.TenDangNhap.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                matched = true;
            }

            if (!matched)
            {
                return (false, "Email hoặc số điện thoại xác minh không khớp với tài khoản.");
            }

            account.MatKhau = newPassword.Trim();
            _context.SaveChanges();

            return (true, "Đặt lại mật khẩu thành công! Bạn có thể đăng nhập bằng mật khẩu mới.");
        }

        public static void Logout()
        {
            CurrentUser = null;
            CurrentEmployee = null;
        }
    }
}
