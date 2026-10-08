using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Services
{
    public class TicketService
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;

        public TicketService(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
        }

        public List<Ve> GetAll()
        {
            return _context.Ves.OrderBy(v => v.MaVe).ToList();
        }

        public Ve? GetById(string maVe)
        {
            return _context.Ves.FirstOrDefault(v => v.MaVe == maVe);
        }

        public List<Ve> Filter(string keyword, string loaiVe, string trangThai)
        {
            var query = _context.Ves.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var kw = keyword.Trim().ToLower();
                query = query.Where(v => v.MaVe.ToLower().Contains(kw) || v.TenVe.ToLower().Contains(kw));
            }

            if (!string.IsNullOrWhiteSpace(loaiVe) && loaiVe != "Tất cả")
            {
                query = query.Where(v => v.LoaiVe == loaiVe);
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
            {
                query = query.Where(v => v.TrangThai == trangThai);
            }

            return query.OrderBy(v => v.MaVe).ToList();
        }

        public (bool Success, string Message) Add(Ve ve)
        {
            if (string.IsNullOrWhiteSpace(ve.MaVe))
                return (false, "Mã vé không được để trống.");

            if (string.IsNullOrWhiteSpace(ve.TenVe))
                return (false, "Tên vé không được để trống.");

            if (ve.GiaVe < 0)
                return (false, "Giá vé phải lớn hơn hoặc bằng 0.");

            if (_context.Ves.Any(v => v.MaVe == ve.MaVe))
                return (false, $"Mã vé '{ve.MaVe}' đã tồn tại.");

            try
            {
                _context.Ves.Add(ve);
                _context.SaveChanges();
                return (true, "Thêm vé thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu vé: {ex.Message}");
            }
        }

        public (bool Success, string Message) Update(Ve ve)
        {
            var existing = _context.Ves.FirstOrDefault(v => v.MaVe == ve.MaVe);
            if (existing == null)
                return (false, $"Không tìm thấy vé với mã '{ve.MaVe}'.");

            if (string.IsNullOrWhiteSpace(ve.TenVe))
                return (false, "Tên vé không được để trống.");

            if (ve.GiaVe < 0)
                return (false, "Giá vé phải lớn hơn hoặc bằng 0.");

            try
            {
                existing.TenVe = ve.TenVe.Trim();
                existing.LoaiVe = ve.LoaiVe;
                existing.GiaVe = ve.GiaVe;
                existing.ThoiHan = ve.ThoiHan;
                existing.MoTa = ve.MoTa;
                existing.TrangThai = ve.TrangThai;

                _context.SaveChanges();
                return (true, "Cập nhật vé thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật: {ex.Message}");
            }
        }

        public (bool Success, string Message) Delete(string maVe)
        {
            var existing = _context.Ves.FirstOrDefault(v => v.MaVe == maVe);
            if (existing == null)
                return (false, $"Không tìm thấy vé với mã '{maVe}'.");

            bool hasInvoices = _context.ChiTietHoaDons.Any(c => c.LoaiHang == "Ve" && c.MaHang == maVe);
            if (hasInvoices)
            {
                return (false, $"Không thể xóa vé '{existing.TenVe}' vì đang tồn tại trong các hóa đơn đã bán.");
            }

            try
            {
                _context.Ves.Remove(existing);
                _context.SaveChanges();
                return (true, $"Đã xóa vé '{existing.TenVe}' thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa: {ex.Message}");
            }
        }

        public string GenerateNextId()
        {
            var lastId = _context.Ves
                .Where(v => v.MaVe.StartsWith("VE"))
                .OrderByDescending(v => v.MaVe)
                .Select(v => v.MaVe)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(lastId) || lastId.Length <= 2)
            {
                return "VE001";
            }

            if (int.TryParse(lastId.Substring(2), out int num))
            {
                return $"VE{(num + 1):D3}";
            }

            return $"VE{DateTime.Now:fff}";
        }

        public ImportResult<Ve> ValidateAndParseExcel(string filePath)
        {
            var result = new ImportResult<Ve>();
            var dataTable = _excelService.ReadExcelToDataTable(filePath);
            result.TotalRows = dataTable.Rows.Count;

            var existingCodes = _context.Ves.Select(v => v.MaVe).ToHashSet();
            var currentBatchCodes = new HashSet<string>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int rowNumber = i + 2;
                var row = dataTable.Rows[i];

                string maVe = row.Table.Columns.Contains("MaVe") ? row["MaVe"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(maVe) && row.Table.Columns.Contains("Mã Vé"))
                    maVe = row["Mã Vé"]?.ToString()?.Trim() ?? "";

                string tenVe = row.Table.Columns.Contains("TenVe") ? row["TenVe"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(tenVe) && row.Table.Columns.Contains("Tên Vé"))
                    tenVe = row["Tên Vé"]?.ToString()?.Trim() ?? "";

                string loaiVe = row.Table.Columns.Contains("LoaiVe") ? row["LoaiVe"]?.ToString()?.Trim() ?? "" : "Vé ngày";
                if (string.IsNullOrEmpty(loaiVe) && row.Table.Columns.Contains("Loại Vé"))
                    loaiVe = row["Loại Vé"]?.ToString()?.Trim() ?? "Vé ngày";

                string giaVeStr = row.Table.Columns.Contains("GiaVe") ? row["GiaVe"]?.ToString()?.Trim() ?? "0" : "0";
                if (giaVeStr == "0" && row.Table.Columns.Contains("Giá Vé"))
                    giaVeStr = row["Giá Vé"]?.ToString()?.Trim() ?? "0";

                string thoiHan = row.Table.Columns.Contains("ThoiHan") ? row["ThoiHan"]?.ToString()?.Trim() ?? "Trong ngày" : "Trong ngày";
                string trangThai = row.Table.Columns.Contains("TrangThai") ? row["TrangThai"]?.ToString()?.Trim() ?? "Đang áp dụng" : "Đang áp dụng";
                string moTa = row.Table.Columns.Contains("MoTa") ? row["MoTa"]?.ToString()?.Trim() ?? "" : "";

                if (string.IsNullOrWhiteSpace(maVe))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã vé không được để trống.");
                    continue;
                }

                if (existingCodes.Contains(maVe) || currentBatchCodes.Contains(maVe))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã vé '{maVe}' đã tồn tại.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(tenVe))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Tên vé không được để trống.");
                    continue;
                }

                if (!decimal.TryParse(giaVeStr.Replace(",", "").Replace(".", ""), out decimal giaVe) || giaVe < 0)
                {
                    result.Errors.Add($"Dòng {rowNumber}: Giá vé '{giaVeStr}' không hợp lệ.");
                    continue;
                }

                currentBatchCodes.Add(maVe);

                var ve = new Ve
                {
                    MaVe = maVe,
                    TenVe = tenVe,
                    LoaiVe = string.IsNullOrWhiteSpace(loaiVe) ? "Vé ngày" : loaiVe,
                    GiaVe = giaVe,
                    ThoiHan = string.IsNullOrWhiteSpace(thoiHan) ? "Trong ngày" : thoiHan,
                    TrangThai = string.IsNullOrWhiteSpace(trangThai) ? "Đang áp dụng" : trangThai,
                    MoTa = string.IsNullOrWhiteSpace(moTa) ? null : moTa
                };

                result.ValidItems.Add(ve);
            }

            result.SuccessCount = result.ValidItems.Count;
            return result;
        }

        public (bool Success, string Message) SaveImportedTickets(List<Ve> tickets)
        {
            if (tickets == null || !tickets.Any())
                return (false, "Không có vé nào để lưu.");

            try
            {
                _context.Ves.AddRange(tickets);
                _context.SaveChanges();
                return (true, $"Đã nhập thành công {tickets.Count} loại vé.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu: {ex.Message}");
            }
        }

        public void ExportToExcel(List<Ve> tickets, string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("Mã Vé");
            dt.Columns.Add("Tên Vé");
            dt.Columns.Add("Loại Vé");
            dt.Columns.Add("Giá Vé", typeof(decimal));
            dt.Columns.Add("Thời Hạn");
            dt.Columns.Add("Trạng Thái");
            dt.Columns.Add("Mô Tả");

            foreach (var v in tickets)
            {
                dt.Rows.Add(v.MaVe, v.TenVe, v.LoaiVe, v.GiaVe, v.ThoiHan, v.TrangThai, v.MoTa ?? "");
            }

            _excelService.ExportDataTableToExcel(dt, "Ve", filePath, "DANH SÁCH CÁC LOẠI VÉ KHU VUI CHƠI");
        }
    }
}
