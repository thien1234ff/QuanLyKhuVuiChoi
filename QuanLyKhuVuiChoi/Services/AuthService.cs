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

        public static void Logout()
        {
            CurrentUser = null;
            CurrentEmployee = null;
        }
    }
}
