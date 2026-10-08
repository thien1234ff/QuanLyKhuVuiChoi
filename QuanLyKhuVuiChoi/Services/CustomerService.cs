using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Services
{
    public class CustomerService
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;

        public CustomerService(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
        }

        public List<KhachHang> GetAll()
        {
            return _context.KhachHangs.OrderByDescending(k => k.NgayDangKy).ToList();
        }

        public KhachHang? GetById(string maKH)
        {
            return _context.KhachHangs.FirstOrDefault(k => k.MaKH == maKH);
        }

        public List<KhachHang> Search(string keyword, string searchType = "Contains")
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAll();

            keyword = keyword.Trim().ToLower();
            var query = _context.KhachHangs.AsQueryable();

            switch (searchType)
            {
                case "StartsWith":
                    query = query.Where(k => k.MaKH.ToLower().StartsWith(keyword) ||
                                             k.HoTen.ToLower().StartsWith(keyword) ||
                                             k.SoDienThoai.ToLower().StartsWith(keyword) ||
                                             (k.Email != null && k.Email.ToLower().StartsWith(keyword)));
                    break;
                case "Exact":
                    query = query.Where(k => k.MaKH.ToLower() == keyword ||
                                             k.HoTen.ToLower() == keyword ||
                                             k.SoDienThoai.ToLower() == keyword ||
                                             (k.Email != null && k.Email.ToLower() == keyword));
                    break;
                case "Contains":
                default:
                    query = query.Where(k => k.MaKH.ToLower().Contains(keyword) ||
                                             k.HoTen.ToLower().Contains(keyword) ||
                                             k.SoDienThoai.ToLower().Contains(keyword) ||
                                             (k.Email != null && k.Email.ToLower().Contains(keyword)));
                    break;
            }

            return query.OrderByDescending(k => k.NgayDangKy).ToList();
        }

        public (bool Success, string Message) Add(KhachHang khachHang)
        {
            if (string.IsNullOrWhiteSpace(khachHang.MaKH))
                return (false, "Mã khách hàng không được để trống.");

            if (string.IsNullOrWhiteSpace(khachHang.HoTen))
                return (false, "Họ và tên khách hàng không được để trống.");

            if (!ValidationHelper.IsValidPhoneNumber(khachHang.SoDienThoai))
                return (false, "Số điện thoại không hợp lệ (phải gồm 10 chữ số).");

            if (!string.IsNullOrWhiteSpace(khachHang.Email) && !ValidationHelper.IsValidEmail(khachHang.Email))
                return (false, "Địa chỉ email không đúng định dạng.");

            if (_context.KhachHangs.Any(k => k.MaKH == khachHang.MaKH))
                return (false, $"Mã khách hàng '{khachHang.MaKH}' đã tồn tại trong hệ thống.");

            if (_context.KhachHangs.Any(k => k.SoDienThoai == khachHang.SoDienThoai))
                return (false, $"Số điện thoại '{khachHang.SoDienThoai}' đã được sử dụng bởi khách hàng khác.");

            try
            {
                _context.KhachHangs.Add(khachHang);
                _context.SaveChanges();
                return (true, "Thêm khách hàng thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu cơ sở dữ liệu: {ex.Message}");
            }
        }

        public (bool Success, string Message) Update(KhachHang khachHang)
        {
            var existing = _context.KhachHangs.FirstOrDefault(k => k.MaKH == khachHang.MaKH);
            if (existing == null)
                return (false, $"Không tìm thấy khách hàng với mã '{khachHang.MaKH}'.");

            if (string.IsNullOrWhiteSpace(khachHang.HoTen))
                return (false, "Họ và tên khách hàng không được để trống.");

            if (!ValidationHelper.IsValidPhoneNumber(khachHang.SoDienThoai))
                return (false, "Số điện thoại không hợp lệ (phải gồm 10 chữ số).");

            if (!string.IsNullOrWhiteSpace(khachHang.Email) && !ValidationHelper.IsValidEmail(khachHang.Email))
                return (false, "Địa chỉ email không đúng định dạng.");

            // Kiểm tra trùng SĐT với người khác
            if (_context.KhachHangs.Any(k => k.SoDienThoai == khachHang.SoDienThoai && k.MaKH != khachHang.MaKH))
                return (false, $"Số điện thoại '{khachHang.SoDienThoai}' đã được sử dụng bởi khách hàng khác.");

            try
            {
                existing.HoTen = khachHang.HoTen.Trim();
                existing.NgaySinh = khachHang.NgaySinh;
                existing.GioiTinh = khachHang.GioiTinh;
                existing.SoDienThoai = khachHang.SoDienThoai.Trim();
                existing.Email = string.IsNullOrWhiteSpace(khachHang.Email) ? null : khachHang.Email.Trim();
                existing.DiaChi = string.IsNullOrWhiteSpace(khachHang.DiaChi) ? null : khachHang.DiaChi.Trim();

                _context.SaveChanges();
                return (true, "Cập nhật thông tin khách hàng thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật cơ sở dữ liệu: {ex.Message}");
            }
        }

        public (bool Success, string Message) Delete(string maKH)
        {
            var existing = _context.KhachHangs.Include(k => k.HoaDons).FirstOrDefault(k => k.MaKH == maKH);
            if (existing == null)
                return (false, $"Không tìm thấy khách hàng với mã '{maKH}'.");

            if (existing.HoaDons != null && existing.HoaDons.Any())
            {
                return (false, $"Không thể xóa khách hàng '{existing.HoTen}' ({maKH}) vì đang tồn tại {existing.HoaDons.Count} hóa đơn liên quan. Vui lòng kiểm tra lại dữ liệu.");
            }

            try
            {
                _context.KhachHangs.Remove(existing);
                _context.SaveChanges();
                return (true, $"Đã xóa khách hàng '{existing.HoTen}' thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa khách hàng: {ex.Message}");
            }
        }

        public string GenerateNextId()
        {
            var lastId = _context.KhachHangs
                .Where(k => k.MaKH.StartsWith("KH"))
                .OrderByDescending(k => k.MaKH)
                .Select(k => k.MaKH)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(lastId) || lastId.Length <= 2)
            {
                return "KH001";
            }

            if (int.TryParse(lastId.Substring(2), out int num))
            {
                return $"KH{(num + 1):D3}";
            }

            return $"KH{DateTime.Now:fff}";
        }

        public ImportResult<KhachHang> ValidateAndParseExcel(string filePath)
        {
            var result = new ImportResult<KhachHang>();
            var dataTable = _excelService.ReadExcelToDataTable(filePath);
            result.TotalRows = dataTable.Rows.Count;

            var existingCodes = _context.KhachHangs.Select(k => k.MaKH).ToHashSet();
            var existingPhones = _context.KhachHangs.Select(k => k.SoDienThoai).ToHashSet();

            var currentBatchCodes = new HashSet<string>();
            var currentBatchPhones = new HashSet<string>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int rowNumber = i + 2; // Dòng 1 là tiêu đề
                var row = dataTable.Rows[i];

                string maKH = row.Table.Columns.Contains("MaKH") ? row["MaKH"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(maKH) && row.Table.Columns.Contains("Mã Khách Hàng"))
                    maKH = row["Mã Khách Hàng"]?.ToString()?.Trim() ?? "";

                string hoTen = row.Table.Columns.Contains("HoTen") ? row["HoTen"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(hoTen) && row.Table.Columns.Contains("Họ Tên"))
                    hoTen = row["Họ Tên"]?.ToString()?.Trim() ?? "";

                string ngaySinhStr = row.Table.Columns.Contains("NgaySinh") ? row["NgaySinh"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(ngaySinhStr) && row.Table.Columns.Contains("Ngày Sinh"))
                    ngaySinhStr = row["Ngày Sinh"]?.ToString()?.Trim() ?? "";

                string gioiTinh = row.Table.Columns.Contains("GioiTinh") ? row["GioiTinh"]?.ToString()?.Trim() ?? "" : "Nam";
                if (string.IsNullOrEmpty(gioiTinh) && row.Table.Columns.Contains("Giới Tính"))
                    gioiTinh = row["Giới Tính"]?.ToString()?.Trim() ?? "Nam";

                string sdt = row.Table.Columns.Contains("SoDienThoai") ? row["SoDienThoai"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(sdt) && row.Table.Columns.Contains("Số Điện Thoại"))
                    sdt = row["Số Điện Thoại"]?.ToString()?.Trim() ?? "";

                string email = row.Table.Columns.Contains("Email") ? row["Email"]?.ToString()?.Trim() ?? "" : "";
                string diaChi = row.Table.Columns.Contains("DiaChi") ? row["DiaChi"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(diaChi) && row.Table.Columns.Contains("Địa Chỉ"))
                    diaChi = row["Địa Chỉ"]?.ToString()?.Trim() ?? "";

                // Validate
                if (string.IsNullOrWhiteSpace(maKH))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã khách hàng không được để trống.");
                    continue;
                }

                if (existingCodes.Contains(maKH) || currentBatchCodes.Contains(maKH))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã khách hàng '{maKH}' đã tồn tại.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(hoTen))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Họ tên không được để trống.");
                    continue;
                }

                if (!ValidationHelper.IsValidPhoneNumber(sdt))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Số điện thoại '{sdt}' không hợp lệ.");
                    continue;
                }

                if (existingPhones.Contains(sdt) || currentBatchPhones.Contains(sdt))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Số điện thoại '{sdt}' đã tồn tại trong hệ thống.");
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(email) && !ValidationHelper.IsValidEmail(email))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Email '{email}' không đúng định dạng.");
                    continue;
                }

                DateTime? ngaySinh = null;
                if (!string.IsNullOrWhiteSpace(ngaySinhStr) && DateTime.TryParse(ngaySinhStr, out DateTime parsedDate))
                {
                    ngaySinh = parsedDate;
                }

                currentBatchCodes.Add(maKH);
                currentBatchPhones.Add(sdt);

                var kh = new KhachHang
                {
                    MaKH = maKH,
                    HoTen = hoTen,
                    NgaySinh = ngaySinh,
                    GioiTinh = string.IsNullOrWhiteSpace(gioiTinh) ? "Nam" : gioiTinh,
                    SoDienThoai = sdt,
                    Email = string.IsNullOrWhiteSpace(email) ? null : email,
                    DiaChi = string.IsNullOrWhiteSpace(diaChi) ? null : diaChi,
                    NgayDangKy = DateTime.Now
                };

                result.ValidItems.Add(kh);
            }

            result.SuccessCount = result.ValidItems.Count;
            return result;
        }

        public (bool Success, string Message) SaveImportedCustomers(List<KhachHang> customers)
        {
            if (customers == null || !customers.Any())
                return (false, "Không có khách hàng hợp lệ nào để lưu.");

            try
            {
                _context.KhachHangs.AddRange(customers);
                _context.SaveChanges();
                return (true, $"Đã nhập thành công {customers.Count} khách hàng vào hệ thống.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu dữ liệu: {ex.Message}");
            }
        }

        public void ExportToExcel(List<KhachHang> customers, string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("Mã KH");
            dt.Columns.Add("Họ và Tên");
            dt.Columns.Add("Ngày Sinh", typeof(DateTime));
            dt.Columns.Add("Giới Tính");
            dt.Columns.Add("Số Điện Thoại");
            dt.Columns.Add("Email");
            dt.Columns.Add("Địa Chỉ");
            dt.Columns.Add("Ngày Đăng Ký", typeof(DateTime));

            foreach (var c in customers)
            {
                dt.Rows.Add(
                    c.MaKH,
                    c.HoTen,
                    c.NgaySinh.HasValue ? (object)c.NgaySinh.Value : DBNull.Value,
                    c.GioiTinh,
                    c.SoDienThoai,
                    c.Email ?? "",
                    c.DiaChi ?? "",
                    c.NgayDangKy
                );
            }

            _excelService.ExportDataTableToExcel(dt, "KhachHang", filePath, "DANH SÁCH KHÁCH HÀNG KHU VUI CHƠI");
        }
    }
}
