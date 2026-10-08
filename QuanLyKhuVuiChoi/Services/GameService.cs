using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Services
{
    public class GameService
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;

        public GameService(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
        }

        public List<TroChoi> GetAll()
        {
            return _context.TroChois.OrderBy(t => t.MaTroChoi).ToList();
        }

        public TroChoi? GetById(string maTroChoi)
        {
            return _context.TroChois.FirstOrDefault(t => t.MaTroChoi == maTroChoi);
        }

        public List<TroChoi> Filter(string keyword, string loaiTroChoi, string trangThai)
        {
            var query = _context.TroChois.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var kw = keyword.Trim().ToLower();
                query = query.Where(t => t.MaTroChoi.ToLower().Contains(kw) || t.TenTroChoi.ToLower().Contains(kw));
            }

            if (!string.IsNullOrWhiteSpace(loaiTroChoi) && loaiTroChoi != "Tất cả")
            {
                query = query.Where(t => t.LoaiTroChoi == loaiTroChoi);
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
            {
                query = query.Where(t => t.TrangThai == trangThai);
            }

            return query.OrderBy(t => t.MaTroChoi).ToList();
        }

        public (bool Success, string Message) Add(TroChoi troChoi)
        {
            if (string.IsNullOrWhiteSpace(troChoi.MaTroChoi))
                return (false, "Mã trò chơi không được để trống.");

            if (string.IsNullOrWhiteSpace(troChoi.TenTroChoi))
                return (false, "Tên trò chơi không được để trống.");

            if (troChoi.GiaVe < 0)
                return (false, "Giá vé trò chơi phải lớn hơn hoặc bằng 0.");

            if (troChoi.SucChua <= 0)
                return (false, "Sức chứa trò chơi phải là số nguyên dương.");

            if (_context.TroChois.Any(t => t.MaTroChoi == troChoi.MaTroChoi))
                return (false, $"Mã trò chơi '{troChoi.MaTroChoi}' đã tồn tại.");

            try
            {
                _context.TroChois.Add(troChoi);
                _context.SaveChanges();
                return (true, "Thêm trò chơi thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu dữ liệu: {ex.Message}");
            }
        }

        public (bool Success, string Message) Update(TroChoi troChoi)
        {
            var existing = _context.TroChois.FirstOrDefault(t => t.MaTroChoi == troChoi.MaTroChoi);
            if (existing == null)
                return (false, $"Không tìm thấy trò chơi với mã '{troChoi.MaTroChoi}'.");

            if (string.IsNullOrWhiteSpace(troChoi.TenTroChoi))
                return (false, "Tên trò chơi không được để trống.");

            if (troChoi.GiaVe < 0)
                return (false, "Giá vé trò chơi phải lớn hơn hoặc bằng 0.");

            if (troChoi.SucChua <= 0)
                return (false, "Sức chứa trò chơi phải lớn hơn 0.");

            try
            {
                existing.TenTroChoi = troChoi.TenTroChoi.Trim();
                existing.LoaiTroChoi = troChoi.LoaiTroChoi;
                existing.GiaVe = troChoi.GiaVe;
                existing.DoTuoiToiThieu = troChoi.DoTuoiToiThieu;
                existing.SucChua = troChoi.SucChua;
                existing.TrangThai = troChoi.TrangThai;
                existing.MoTa = troChoi.MoTa;

                _context.SaveChanges();
                return (true, "Cập nhật trò chơi thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật: {ex.Message}");
            }
        }

        public (bool Success, string Message) Delete(string maTroChoi)
        {
            var existing = _context.TroChois.FirstOrDefault(t => t.MaTroChoi == maTroChoi);
            if (existing == null)
                return (false, $"Không tìm thấy trò chơi với mã '{maTroChoi}'.");

            // Kiểm tra ràng buộc trong chi tiết hóa đơn
            bool hasInvoices = _context.ChiTietHoaDons.Any(c => c.LoaiHang == "TroChoi" && c.MaHang == maTroChoi);
            if (hasInvoices)
            {
                return (false, $"Không thể xóa trò chơi '{existing.TenTroChoi}' vì đang tồn tại vé/giao dịch liên quan trong lịch sử bán hàng.");
            }

            try
            {
                _context.TroChois.Remove(existing);
                _context.SaveChanges();
                return (true, $"Đã xóa trò chơi '{existing.TenTroChoi}' thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi xóa trò chơi: {ex.Message}");
            }
        }

        public string GenerateNextId()
        {
            var lastId = _context.TroChois
                .Where(t => t.MaTroChoi.StartsWith("TC"))
                .OrderByDescending(t => t.MaTroChoi)
                .Select(t => t.MaTroChoi)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(lastId) || lastId.Length <= 2)
            {
                return "TC001";
            }

            if (int.TryParse(lastId.Substring(2), out int num))
            {
                return $"TC{(num + 1):D3}";
            }

            return $"TC{DateTime.Now:fff}";
        }

        public ImportResult<TroChoi> ValidateAndParseExcel(string filePath)
        {
            var result = new ImportResult<TroChoi>();
            var dataTable = _excelService.ReadExcelToDataTable(filePath);
            result.TotalRows = dataTable.Rows.Count;

            var existingCodes = _context.TroChois.Select(t => t.MaTroChoi).ToHashSet();
            var currentBatchCodes = new HashSet<string>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int rowNumber = i + 2;
                var row = dataTable.Rows[i];

                string maTC = row.Table.Columns.Contains("MaTroChoi") ? row["MaTroChoi"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(maTC) && row.Table.Columns.Contains("Mã Trò Chơi"))
                    maTC = row["Mã Trò Chơi"]?.ToString()?.Trim() ?? "";

                string tenTC = row.Table.Columns.Contains("TenTroChoi") ? row["TenTroChoi"]?.ToString()?.Trim() ?? "" : "";
                if (string.IsNullOrEmpty(tenTC) && row.Table.Columns.Contains("Tên Trò Chơi"))
                    tenTC = row["Tên Trò Chơi"]?.ToString()?.Trim() ?? "";

                string loaiTC = row.Table.Columns.Contains("LoaiTroChoi") ? row["LoaiTroChoi"]?.ToString()?.Trim() ?? "" : "Cảm giác mạnh";
                if (string.IsNullOrEmpty(loaiTC) && row.Table.Columns.Contains("Loại Trò Chơi"))
                    loaiTC = row["Loại Trò Chơi"]?.ToString()?.Trim() ?? "Cảm giác mạnh";

                string giaVeStr = row.Table.Columns.Contains("GiaVe") ? row["GiaVe"]?.ToString()?.Trim() ?? "0" : "0";
                if (giaVeStr == "0" && row.Table.Columns.Contains("Giá Vé"))
                    giaVeStr = row["Giá Vé"]?.ToString()?.Trim() ?? "0";

                string doTuoiStr = row.Table.Columns.Contains("DoTuoiToiThieu") ? row["DoTuoiToiThieu"]?.ToString()?.Trim() ?? "0" : "0";
                string sucChuaStr = row.Table.Columns.Contains("SucChua") ? row["SucChua"]?.ToString()?.Trim() ?? "1" : "1";
                string trangThai = row.Table.Columns.Contains("TrangThai") ? row["TrangThai"]?.ToString()?.Trim() ?? "Đang hoạt động" : "Đang hoạt động";
                string moTa = row.Table.Columns.Contains("MoTa") ? row["MoTa"]?.ToString()?.Trim() ?? "" : "";

                if (string.IsNullOrWhiteSpace(maTC))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã trò chơi không được để trống.");
                    continue;
                }

                if (existingCodes.Contains(maTC) || currentBatchCodes.Contains(maTC))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Mã trò chơi '{maTC}' đã tồn tại.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(tenTC))
                {
                    result.Errors.Add($"Dòng {rowNumber}: Tên trò chơi không được để trống.");
                    continue;
                }

                if (!decimal.TryParse(giaVeStr.Replace(",", "").Replace(".", ""), out decimal giaVe) || giaVe < 0)
                {
                    result.Errors.Add($"Dòng {rowNumber}: Giá vé '{giaVeStr}' không hợp lệ (phải >= 0).");
                    continue;
                }

                int.TryParse(doTuoiStr, out int doTuoi);
                if (!int.TryParse(sucChuaStr, out int sucChua) || sucChua <= 0)
                {
                    sucChua = 20;
                }

                currentBatchCodes.Add(maTC);

                var tc = new TroChoi
                {
                    MaTroChoi = maTC,
                    TenTroChoi = tenTC,
                    LoaiTroChoi = string.IsNullOrWhiteSpace(loaiTC) ? "Cảm giác mạnh" : loaiTC,
                    GiaVe = giaVe,
                    DoTuoiToiThieu = doTuoi,
                    SucChua = sucChua,
                    TrangThai = string.IsNullOrWhiteSpace(trangThai) ? "Đang hoạt động" : trangThai,
                    MoTa = string.IsNullOrWhiteSpace(moTa) ? null : moTa
                };

                result.ValidItems.Add(tc);
            }

            result.SuccessCount = result.ValidItems.Count;
            return result;
        }

        public (bool Success, string Message) SaveImportedGames(List<TroChoi> games)
        {
            if (games == null || !games.Any())
                return (false, "Không có trò chơi nào để lưu.");

            try
            {
                _context.TroChois.AddRange(games);
                _context.SaveChanges();
                return (true, $"Đã nhập thành công {games.Count} trò chơi vào hệ thống.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi lưu dữ liệu: {ex.Message}");
            }
        }

        public void ExportToExcel(List<TroChoi> games, string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("Mã Trò Chơi");
            dt.Columns.Add("Tên Trò Chơi");
            dt.Columns.Add("Loại Trò Chơi");
            dt.Columns.Add("Giá Vé", typeof(decimal));
            dt.Columns.Add("Độ Tuổi Tối Thiểu", typeof(int));
            dt.Columns.Add("Sức Chứa", typeof(int));
            dt.Columns.Add("Trạng Thái");
            dt.Columns.Add("Mô Tả");

            foreach (var g in games)
            {
                dt.Rows.Add(g.MaTroChoi, g.TenTroChoi, g.LoaiTroChoi, g.GiaVe, g.DoTuoiToiThieu, g.SucChua, g.TrangThai, g.MoTa ?? "");
            }

            _excelService.ExportDataTableToExcel(dt, "TroChoi", filePath, "DANH SÁCH TRÒ CHƠI KHU VUI CHƠI");
        }
    }
}
