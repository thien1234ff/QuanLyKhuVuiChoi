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
    public class EmployeeService
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;

        public EmployeeService(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
        }

        public List<NhanVien> GetAll()
        {
            return _context.NhanViens.OrderBy(n => n.MaNV).ToList();
        }

        public NhanVien? GetById(string maNV)
        {
            return _context.NhanViens.FirstOrDefault(n => n.MaNV == maNV);
        }

        public List<NhanVien> Filter(string keyword, string chucVu, string trangThai)
        {
            var query = _context.NhanViens.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var kw = keyword.Trim().ToLower();
                query = query.Where(n => n.MaNV.ToLower().Contains(kw) ||
                                         n.HoTen.ToLower().Contains(kw) ||
                                         n.SoDienThoai.ToLower().Contains(kw));
            }

            if (!string.IsNullOrWhiteSpace(chucVu) && chucVu != "Tất cả")
            {
                query = query.Where(n => n.ChucVu == chucVu);
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
            {
                query = query.Where(n => n.TrangThai == trangThai);
            }

            return query.OrderBy(n => n.MaNV).ToList();
        }

        public (bool Success, string Message) Add(NhanVien nhanVien)
        {
            if (string.IsNullOrWhiteSpace(nhanVien.MaNV))
                return (false, "Mã nhân viên không được để trống.");

            if (string.IsNullOrWhiteSpace(nhanVien.HoTen))
                return (false, "Họ và tên nhân viên không được để trống.");

            if (!ValidationHelper.IsValidPhoneNumber(nhanVien.SoDienThoai))
                return (false, "Số điện thoại không hợp lệ.");

            if (!string.IsNullOrWhiteSpace(nhanVien.Email) && !ValidationHelper.IsValidEmail(nhanVien.Email))
                return (false, "Email không đúng định dạng.");

            if (_context.NhanViens.Any(n => n.MaNV == nhanVien.MaNV))
                return (false, $"Mã nhân viên '{nhanVien.MaNV}' đã tồn tại.");

            try
            {
                _context.NhanViens.Add(nhanVien);
                _context.SaveChanges();
                return (true, "Thêm nhân viên thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu nhân viên: {ex.Message}");
            }
        }

        public (bool Success, string Message) Update(NhanVien nhanVien)
        {
            var existing = _context.NhanViens.FirstOrDefault(n => n.MaNV == nhanVien.MaNV);
            if (existing == null)
                return (false, $"Không tìm thấy nhân viên với mã '{nhanVien.MaNV}'.");

            if (string.IsNullOrWhiteSpace(nhanVien.HoTen))
                return (false, "Họ tên không được để trống.");

            if (!ValidationHelper.IsValidPhoneNumber(nhanVien.SoDienThoai))
                return (false, "Số điện thoại không hợp lệ.");

            try
            {
                existing.HoTen = nhanVien.HoTen.Trim();
                existing.NgaySinh = nhanVien.NgaySinh;
                existing.GioiTinh = nhanVien.GioiTinh;
                existing.SoDienThoai = nhanVien.SoDienThoai.Trim();
                existing.Email = string.IsNullOrWhiteSpace(nhanVien.Email) ? null : nhanVien.Email.Trim();
                existing.DiaChi = string.IsNullOrWhiteSpace(nhanVien.DiaChi) ? null : nhanVien.DiaChi.Trim();
                existing.ChucVu = nhanVien.ChucVu;
                existing.NgayVaoLam = nhanVien.NgayVaoLam;
                existing.TrangThai = nhanVien.TrangThai;

                _context.SaveChanges();
                return (true, "Cập nhật nhân viên thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật: {ex.Message}");
            }
        }

        public (bool Success, string Message) Delete(string maNV)
        {
            var existing = _context.NhanViens.Include(n => n.HoaDons).FirstOrDefault(n => n.MaNV == maNV);
            if (existing == null)
                return (false, $"Không tìm thấy nhân viên với mã '{maNV}'.");

            if (existing.HoaDons != null && existing.HoaDons.Any())
            {
                return (false, $"Không thể xóa nhân viên '{existing.HoTen}' vì nhân viên này đã lập {existing.HoaDons.Count} hóa đơn trong hệ thống.");
            }

            try
            {
                _context.NhanViens.Remove(existing);
                _context.SaveChanges();
                return (true, $"Đã xóa nhân viên '{existing.HoTen}' thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa nhân viên: {ex.Message}");
            }
        }

        public string GenerateNextId()
        {
            var lastId = _context.NhanViens
                .Where(n => n.MaNV.StartsWith("NV"))
                .OrderByDescending(n => n.MaNV)
                .Select(n => n.MaNV)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(lastId) || lastId.Length <= 2)
            {
                return "NV001";
            }

            if (int.TryParse(lastId.Substring(2), out int num))
            {
                return $"NV{(num + 1):D3}";
            }

            return $"NV{DateTime.Now:fff}";
        }

        public ImportResult<NhanVien> ValidateAndParseExcel(string filePath)
        {
            var result = new ImportResult<NhanVien>();
            var dataTable = _excelService.ReadExcelToDataTable(filePath);
            result.TotalRows = dataTable.Rows.Count;

            var existingCodes = _context.NhanViens.Select(n => n.MaNV).ToHashSet();
            var currentBatchCodes = new HashSet<string>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int rowNumber = i + 2;
                var row = dataTable.Rows[i];

                string maNV = row.Table.Columns.Contains("MaNV") ? row["MaNV"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(maNV) && row.Table.Columns.Contains("Mã Nhân Viên"))
                    maNV = row["Mã Nhân Viên"]?.ToString()?.Trim() ?? "";

                string hoTen = row.Table.Columns.Contains("HoTen") ? row["HoTen"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(hoTen) && row.Table.Columns.Contains("Họ Tên"))
                    hoTen = row["Họ Tên"]?.ToString()?.Trim() ?? "";

                string ngaySinhStr = row.Table.Columns.Contains("NgaySinh") ? row["NgaySinh"]?.ToString()?.Trim() ?? "" : "";
                string gioiTinh = row.Table.Columns.Contains("GioiTinh") ? row["GioiTinh"]?.ToString()?.Trim() ?? "" : "Nam";
                string sdt = row.Table.Columns.Contains("SoDienThoai") ? row["SoDienThoai"]?.ToString()?.Trim() ?? "" : "";
                string email = row.Table.Columns.Contains("Email") ? row["Email"]?.ToString()?.Trim() ?? "" : "";
                string diaChi = row.Table.Columns.Contains("DiaChi") ? row["DiaChi"]?.ToString()?.Trim() ?? "" : "";
                string chucVu = row.Table.Columns.Contains("ChucVu") ? row["ChucVu"]?.ToString()?.Trim() ?? "" : "Thu ngân";
                string trangThai = row.Table.Columns.Contains("TrangThai") ? row["TrangThai"]?.ToString()?.Trim() ?? "" : "Đang làm việc";

                if (string.IsNullOrWhiteSpace(maNV))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã nhân viên không được để trống.");
                    continue;
                }

                if (existingCodes.Contains(maNV) || currentBatchCodes.Contains(maNV))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã nhân viên '{maNV}' đã tồn tại.");
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

                DateTime? ngaySinh = null;
                if (!string.IsNullOrWhiteSpace(ngaySinhStr) && DateTime.TryParse(ngaySinhStr, out DateTime dt))
                {
                    ngaySinh = dt;
                }

                currentBatchCodes.Add(maNV);

                var nv = new NhanVien
                {
                    MaNV = maNV,
                    HoTen = hoTen,
                    NgaySinh = ngaySinh,
                    GioiTinh = string.IsNullOrWhiteSpace(gioiTinh) ? "Nam" : gioiTinh,
                    SoDienThoai = sdt,
                    Email = string.IsNullOrWhiteSpace(email) ? null : email,
                    DiaChi = string.IsNullOrWhiteSpace(diaChi) ? null : diaChi,
                    ChucVu = string.IsNullOrWhiteSpace(chucVu) ? "Thu ngân" : chucVu,
                    NgayVaoLam = DateTime.Today,
                    TrangThai = string.IsNullOrWhiteSpace(trangThai) ? "Đang làm việc" : trangThai
                };

                result.ValidItems.Add(nv);
            }

            result.SuccessCount = result.ValidItems.Count;
            return result;
        }

        public (bool Success, string Message) SaveImportedEmployees(List<NhanVien> employees)
        {
            if (employees == null || !employees.Any())
                return (false, "Không có nhân viên nào để lưu.");

            try
            {
                _context.NhanViens.AddRange(employees);
                _context.SaveChanges();
                return (true, $"Đã nhập thành công {employees.Count} nhân viên.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu: {ex.Message}");
            }
        }

        public void ExportToExcel(List<NhanVien> employees, string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("Mã NV");
            dt.Columns.Add("Họ và Tên");
            dt.Columns.Add("Ngày Sinh", typeof(DateTime));
            dt.Columns.Add("Giới Tính");
            dt.Columns.Add("Số Điện Thoại");
            dt.Columns.Add("Email");
            dt.Columns.Add("Địa Chỉ");
            dt.Columns.Add("Chức Vụ");
            dt.Columns.Add("Ngày Vào Làm", typeof(DateTime));
            dt.Columns.Add("Trạng Thái");

            foreach (var n in employees)
            {
                dt.Rows.Add(
                    n.MaNV,
                    n.HoTen,
                    n.NgaySinh.HasValue ? (object)n.NgaySinh.Value : DBNull.Value,
                    n.GioiTinh,
                    n.SoDienThoai,
                    n.Email ?? "",
                    n.DiaChi ?? "",
                    n.ChucVu,
                    n.NgayVaoLam,
                    n.TrangThai
                );
            }

            _excelService.ExportDataTableToExcel(dt, "NhanVien", filePath, "DANH SÁCH NHÂN VIÊN KHU VUI CHƠI");
        }
    }
}
