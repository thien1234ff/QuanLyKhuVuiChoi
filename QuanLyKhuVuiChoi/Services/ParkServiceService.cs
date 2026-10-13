using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Services
{
    public class ParkServiceService
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;

        public ParkServiceService(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
        }

        public List<DichVu> GetAll()
        {
            return _context.DichVus.OrderBy(d => d.MaDichVu).ToList();
        }

        public DichVu? GetById(string maDichVu)
        {
            return _context.DichVus.FirstOrDefault(d => d.MaDichVu == maDichVu);
        }

        public List<DichVu> Filter(string keyword, string loaiDichVu, string trangThai)
        {
            var query = _context.DichVus.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var kw = keyword.Trim().ToLower();
                query = query.Where(d => d.MaDichVu.ToLower().Contains(kw) || d.TenDichVu.ToLower().Contains(kw));
            }

            if (!string.IsNullOrWhiteSpace(loaiDichVu) && loaiDichVu != "Tất cả")
            {
                query = query.Where(d => d.LoaiDichVu == loaiDichVu);
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
            {
                query = query.Where(d => d.TrangThai == trangThai);
            }

            return query.OrderBy(d => d.MaDichVu).ToList();
        }

        public (bool Success, string Message) Add(DichVu dichVu)
        {
            if (string.IsNullOrWhiteSpace(dichVu.MaDichVu))
                return (false, "Mã dịch vụ không được để trống.");

            if (string.IsNullOrWhiteSpace(dichVu.TenDichVu))
                return (false, "Tên dịch vụ không được để trống.");

            if (dichVu.DonGia < 0)
                return (false, "Đơn giá dịch vụ phải lớn hơn hoặc bằng 0.");

            if (dichVu.SoLuongTon < 0)
                return (false, "Số lượng tồn không thể là số âm.");

            if (_context.DichVus.Any(d => d.MaDichVu == dichVu.MaDichVu))
                return (false, $"Mã dịch vụ '{dichVu.MaDichVu}' đã tồn tại.");

            try
            {
                _context.DichVus.Add(dichVu);
                _context.SaveChanges();
                return (true, "Thêm dịch vụ thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu dịch vụ: {ex.Message}");
            }
        }

        public (bool Success, string Message) Update(DichVu dichVu)
        {
            var existing = _context.DichVus.FirstOrDefault(d => d.MaDichVu == dichVu.MaDichVu);
            if (existing == null)
                return (false, $"Không tìm thấy dịch vụ với mã '{dichVu.MaDichVu}'.");

            if (string.IsNullOrWhiteSpace(dichVu.TenDichVu))
                return (false, "Tên dịch vụ không được để trống.");

            if (dichVu.DonGia < 0)
                return (false, "Đơn giá phải lớn hơn hoặc bằng 0.");

            if (dichVu.SoLuongTon < 0)
                return (false, "Số lượng tồn không thể âm.");

            try
            {
                existing.TenDichVu = dichVu.TenDichVu.Trim();
                existing.LoaiDichVu = dichVu.LoaiDichVu;
                existing.DonGia = dichVu.DonGia;
                existing.SoLuongTon = dichVu.SoLuongTon;
                existing.TrangThai = dichVu.TrangThai;
                existing.MoTa = dichVu.MoTa;

                _context.SaveChanges();
                return (true, "Cập nhật dịch vụ thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật: {ex.Message}");
            }
        }

        public (bool Success, string Message) UpdateStock(string maDichVu, int newStock)
        {
            if (newStock < 0)
                return (false, "Số lượng tồn không thể âm.");

            var existing = _context.DichVus.FirstOrDefault(d => d.MaDichVu == maDichVu);
            if (existing == null)
                return (false, "Không tìm thấy dịch vụ.");

            existing.SoLuongTon = newStock;
            if (newStock == 0) existing.TrangThai = "Hết hàng";
            else if (existing.TrangThai == "Hết hàng") existing.TrangThai = "Đang phục vụ";

            _context.SaveChanges();
            return (true, $"Đã cập nhật số lượng tồn của '{existing.TenDichVu}' thành {newStock}.");
        }

        public (bool Success, string Message) Delete(string maDichVu)
        {
            var existing = _context.DichVus.FirstOrDefault(d => d.MaDichVu == maDichVu);
            if (existing == null)
                return (false, $"Không tìm thấy dịch vụ với mã '{maDichVu}'.");

            bool hasInvoices = _context.ChiTietHoaDons.Any(c => c.LoaiHang == "DichVu" && c.MaHang == maDichVu);
            if (hasInvoices)
            {
                return (false, $"Không thể xóa dịch vụ '{existing.TenDichVu}' vì đang tồn tại trong các hóa đơn đã lập.");
            }

            try
            {
                _context.DichVus.Remove(existing);
                _context.SaveChanges();
                return (true, $"Đã xóa dịch vụ '{existing.TenDichVu}' thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa dịch vụ: {ex.Message}");
            }
        }

        public string GenerateNextId()
        {
            var lastId = _context.DichVus
                .Where(d => d.MaDichVu.StartsWith("DV"))
                .OrderByDescending(d => d.MaDichVu)
                .Select(d => d.MaDichVu)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(lastId) || lastId.Length <= 2)
            {
                return "DV001";
            }

            if (int.TryParse(lastId.Substring(2), out int num))
            {
                return $"DV{(num + 1):D3}";
            }

            return $"DV{DateTime.Now:fff}";
        }

        public ImportResult<DichVu> ValidateAndParseExcel(string filePath)
        {
            var result = new ImportResult<DichVu>();
            var dataTable = _excelService.ReadExcelToDataTable(filePath);
            result.TotalRows = dataTable.Rows.Count;

            var existingCodes = _context.DichVus.Select(d => d.MaDichVu).ToHashSet();
            var currentBatchCodes = new HashSet<string>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int rowNumber = i + 2;
                var row = dataTable.Rows[i];

                string maDV = row.Table.Columns.Contains("MaDichVu") ? row["MaDichVu"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(maDV) && row.Table.Columns.Contains("Mã Dịch Vụ"))
                    maDV = row["Mã Dịch Vụ"]?.ToString()?.Trim() ?? "";

                string tenDV = row.Table.Columns.Contains("TenDichVu") ? row["TenDichVu"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(tenDV) && row.Table.Columns.Contains("Tên Dịch Vụ"))
                    tenDV = row["Tên Dịch Vụ"]?.ToString()?.Trim() ?? "";

                string loaiDV = row.Table.Columns.Contains("LoaiDichVu") ? row["LoaiDichVu"]?.ToString()?.Trim() ?? "" : "Ẩm thực";
                if (string.IsNullOrEmpty(loaiDV) && row.Table.Columns.Contains("Loại Dịch Vụ"))
                    loaiDV = row["Loại Dịch Vụ"]?.ToString()?.Trim() ?? "Ẩm thực";

                string donGiaStr = row.Table.Columns.Contains("DonGia") ? row["DonGia"]?.ToString()?.Trim() ?? "0" : "0";
                if (donGiaStr == "0" && row.Table.Columns.Contains("Đơn Giá"))
                    donGiaStr = row["Đơn Giá"]?.ToString()?.Trim() ?? "0";

                string soLuongStr = row.Table.Columns.Contains("SoLuongTon") ? row["SoLuongTon"]?.ToString()?.Trim() ?? "0" : "0";
                if (soLuongStr == "0" && row.Table.Columns.Contains("Số Lượng Tồn"))
                    soLuongStr = row["Số Lượng Tồn"]?.ToString()?.Trim() ?? "0";

                string trangThai = row.Table.Columns.Contains("TrangThai") ? row["TrangThai"]?.ToString()?.Trim() ?? "Đang phục vụ" : "Đang phục vụ";
                string moTa = row.Table.Columns.Contains("MoTa") ? row["MoTa"]?.ToString()?.Trim() ?? "" : "";

                if (string.IsNullOrWhiteSpace(maDV))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã dịch vụ không được để trống.");
                    continue;
                }

                if (existingCodes.Contains(maDV) || currentBatchCodes.Contains(maDV))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã dịch vụ '{maDV}' đã tồn tại.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(tenDV))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Tên dịch vụ không được để trống.");
                    continue;
                }

                if (!decimal.TryParse(donGiaStr.Replace(",", "").Replace(".", ""), out decimal donGia) || donGia < 0)
                {
                    result.Errors.Add($"Dòng {rowNumber}: Đơn giá '{donGiaStr}' không hợp lệ.");
                    continue;
                }

                if (!int.TryParse(soLuongStr, out int soLuong) || soLuong < 0)
                {
                    soLuong = 0;
                }

                currentBatchCodes.Add(maDV);

                var dv = new DichVu
                {
                    MaDichVu = maDV,
                    TenDichVu = tenDV,
                    LoaiDichVu = string.IsNullOrWhiteSpace(loaiDV) ? "Ẩm thực" : loaiDV,
                    DonGia = donGia,
                    SoLuongTon = soLuong,
                    TrangThai = string.IsNullOrWhiteSpace(trangThai) ? "Đang phục vụ" : trangThai,
                    MoTa = string.IsNullOrWhiteSpace(moTa) ? null : moTa
                };

                result.ValidItems.Add(dv);
            }

            result.SuccessCount = result.ValidItems.Count;
            return result;
        }

        public (bool Success, string Message) SaveImportedServices(List<DichVu> services)
        {
            if (services == null || !services.Any())
                return (false, "Không có dịch vụ nào để lưu.");

            try
            {
                _context.DichVus.AddRange(services);
                _context.SaveChanges();
                return (true, $"Đã nhập thành công {services.Count} dịch vụ vào hệ thống.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu dữ liệu: {ex.Message}");
            }
        }

        public void ExportToExcel(List<DichVu> services, string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("Mã Dịch Vụ");
            dt.Columns.Add("Tên Dịch Vụ");
            dt.Columns.Add("Loại Dịch Vụ");
            dt.Columns.Add("Đơn Giá", typeof(decimal));
            dt.Columns.Add("Số Lượng Tồn", typeof(int));
            dt.Columns.Add("Trạng Thái");
            dt.Columns.Add("Mô Tả");

            foreach (var s in services)
            {
                dt.Rows.Add(s.MaDichVu, s.TenDichVu, s.LoaiDichVu, s.DonGia, s.SoLuongTon, s.TrangThai, s.MoTa ?? "");
            }

            _excelService.ExportDataTableToExcel(dt, "DichVu", filePath, "DANH SÁCH DỊCH VỤ KHU VUI CHƠI");
        }
    }
}
