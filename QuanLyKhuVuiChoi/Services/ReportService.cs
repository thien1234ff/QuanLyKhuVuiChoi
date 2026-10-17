using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;

namespace QuanLyKhuVuiChoi.Services
{
    public class ReportService
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;

        public ReportService(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
        }

        public DashboardSummaryDto GetDashboardSummary()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var validInvoices = _context.HoaDons
                .Where(h => h.TrangThai == "Đã thanh toán")
                .Select(h => new { h.NgayLap, h.TongTien })
                .ToList();

            var ticketDetails = _context.ChiTietHoaDons
                .Where(c => c.HoaDon != null && c.HoaDon.TrangThai == "Đã thanh toán" && c.HoaDon.NgayLap >= startOfMonth && c.LoaiHang == "Ve")
                .Select(c => c.SoLuong)
                .ToList();

            var dto = new DashboardSummaryDto
            {
                TongKhachHang = _context.KhachHangs.Count(),
                TongNhanVien = _context.NhanViens.Count(n => n.TrangThai == "Đang làm việc"),
                TongTroChoi = _context.TroChois.Count(),
                TongDichVu = _context.DichVus.Count(),
                TongHoaDon = validInvoices.Count,
                DoanhThuHomNay = validInvoices.Where(h => h.NgayLap.Date == today).Sum(h => h.TongTien),
                DoanhThuThangNay = validInvoices.Where(h => h.NgayLap >= startOfMonth).Sum(h => h.TongTien),
                SoVeDaBanThangNay = ticketDetails.Sum()
            };

            return dto;
        }

        public List<RevenueReportDto> GetRevenueByPeriod(string periodType, DateTime fromDate, DateTime toDate)
        {
            var validInvoices = _context.HoaDons
                .Include(h => h.ChiTietHoaDons)
                .Where(h => h.TrangThai == "Đã thanh toán" && h.NgayLap.Date >= fromDate.Date && h.NgayLap.Date <= toDate.Date)
                .ToList();

            var result = new List<RevenueReportDto>();

            if (periodType == "Theo Ngày")
            {
                var grouped = validInvoices
                    .GroupBy(h => h.NgayLap.Date)
                    .OrderBy(g => g.Key);

                foreach (var g in grouped)
                {
                    var items = g.SelectMany(h => h.ChiTietHoaDons).ToList();
                    result.Add(new RevenueReportDto
                    {
                        ThoiGian = g.Key.ToString("dd/MM/yyyy"),
                        SoHoaDon = g.Count(),
                        SoVeBan = items.Where(i => i.LoaiHang == "Ve").Sum(i => i.SoLuong),
                        DoanhThuVe = items.Where(i => i.LoaiHang == "Ve").Sum(i => i.ThanhTien),
                        DoanhThuDichVu = items.Where(i => i.LoaiHang == "DichVu").Sum(i => i.ThanhTien),
                        DoanhThuTroChoi = items.Where(i => i.LoaiHang == "TroChoi").Sum(i => i.ThanhTien),
                        TongDoanhThu = g.Sum(h => h.TongTien)
                    });
                }
            }
            else if (periodType == "Theo Tháng")
            {
                var grouped = validInvoices
                    .GroupBy(h => new { h.NgayLap.Year, h.NgayLap.Month })
                    .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month);

                foreach (var g in grouped)
                {
                    var items = g.SelectMany(h => h.ChiTietHoaDons).ToList();
                    result.Add(new RevenueReportDto
                    {
                        ThoiGian = $"Tháng {g.Key.Month:D2}/{g.Key.Year}",
                        SoHoaDon = g.Count(),
                        SoVeBan = items.Where(i => i.LoaiHang == "Ve").Sum(i => i.SoLuong),
                        DoanhThuVe = items.Where(i => i.LoaiHang == "Ve").Sum(i => i.ThanhTien),
                        DoanhThuDichVu = items.Where(i => i.LoaiHang == "DichVu").Sum(i => i.ThanhTien),
                        DoanhThuTroChoi = items.Where(i => i.LoaiHang == "TroChoi").Sum(i => i.ThanhTien),
                        TongDoanhThu = g.Sum(h => h.TongTien)
                    });
                }
            }
            else // Theo Năm
            {
                var grouped = validInvoices
                    .GroupBy(h => h.NgayLap.Year)
                    .OrderBy(g => g.Key);

                foreach (var g in grouped)
                {
                    var items = g.SelectMany(h => h.ChiTietHoaDons).ToList();
                    result.Add(new RevenueReportDto
                    {
                        ThoiGian = $"Năm {g.Key}",
                        SoHoaDon = g.Count(),
                        SoVeBan = items.Where(i => i.LoaiHang == "Ve").Sum(i => i.SoLuong),
                        DoanhThuVe = items.Where(i => i.LoaiHang == "Ve").Sum(i => i.ThanhTien),
                        DoanhThuDichVu = items.Where(i => i.LoaiHang == "DichVu").Sum(i => i.ThanhTien),
                        DoanhThuTroChoi = items.Where(i => i.LoaiHang == "TroChoi").Sum(i => i.ThanhTien),
                        TongDoanhThu = g.Sum(h => h.TongTien)
                    });
                }
            }

            return result;
        }

        public List<CategoryRevenueDto> GetRevenueByCategory(string categoryType, DateTime fromDate, DateTime toDate)
        {
            var validItems = _context.ChiTietHoaDons
                .Include(c => c.HoaDon)
                .Where(c => c.HoaDon != null && c.HoaDon.TrangThai == "Đã thanh toán" &&
                            c.HoaDon.NgayLap.Date >= fromDate.Date && c.HoaDon.NgayLap.Date <= toDate.Date);

            if (categoryType == "Vé")
            {
                validItems = validItems.Where(c => c.LoaiHang == "Ve");
            }
            else if (categoryType == "Dịch Vụ")
            {
                validItems = validItems.Where(c => c.LoaiHang == "DichVu");
            }
            else if (categoryType == "Trò Chơi")
            {
                validItems = validItems.Where(c => c.LoaiHang == "TroChoi");
            }

            var itemList = validItems.ToList();
            decimal totalRevenueAll = itemList.Sum(i => i.ThanhTien);
            if (totalRevenueAll == 0) totalRevenueAll = 1; // tránh chia cho 0

            var grouped = itemList
                .GroupBy(i => new { i.MaHang, i.TenHang, i.LoaiHang })
                .Select(g => new CategoryRevenueDto
                {
                    TenHangMuc = g.Key.TenHang,
                    Loai = g.Key.LoaiHang,
                    SoLuongDaBan = g.Sum(i => i.SoLuong),
                    DoanhThu = g.Sum(i => i.ThanhTien),
                    TiLePhanTram = Math.Round((double)(g.Sum(i => i.ThanhTien) / totalRevenueAll * 100), 2)
                })
                .OrderByDescending(c => c.DoanhThu)
                .ToList();

            return grouped;
        }

        public void ExportRevenueReportToExcel(List<RevenueReportDto> reports, string periodType, string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("Thời Gian");
            dt.Columns.Add("Số Hóa Đơn", typeof(int));
            dt.Columns.Add("Số Vé Bán", typeof(int));
            dt.Columns.Add("Doanh Thu Vé", typeof(decimal));
            dt.Columns.Add("Doanh Thu Dịch Vụ", typeof(decimal));
            dt.Columns.Add("Doanh Thu Trò Chơi", typeof(decimal));
            dt.Columns.Add("Tổng Doanh Thu", typeof(decimal));

            foreach (var r in reports)
            {
                dt.Rows.Add(r.ThoiGian, r.SoHoaDon, r.SoVeBan, r.DoanhThuVe, r.DoanhThuDichVu, r.DoanhThuTroChoi, r.TongDoanhThu);
            }

            _excelService.ExportDataTableToExcel(dt, "DoanhThu", filePath, $"BÁO CÁO DOANH THU ({periodType.ToUpper()})");
        }

        public (decimal TongDoanhThu, int SoVeBan, int SoDichVu, int SoHoaDon, int SoKhachHang) GetSummaryMetrics(DateTime fromDate, DateTime toDate)
        {
            var from = fromDate.Date;
            var to = toDate.Date.AddDays(1).AddTicks(-1);

            var validInvoices = _context.HoaDons
                .Include(h => h.ChiTietHoaDons)
                .Where(h => h.TrangThai == "Đã thanh toán" && h.NgayLap >= from && h.NgayLap <= to)
                .ToList();

            var allItems = validInvoices.SelectMany(h => h.ChiTietHoaDons).ToList();

            decimal revenue = validInvoices.Sum(h => h.TongTien);
            int tickets = allItems.Where(i => i.LoaiHang == "Ve").Sum(i => i.SoLuong);
            int services = allItems.Where(i => i.LoaiHang == "DichVu").Sum(i => i.SoLuong);
            int invoices = validInvoices.Count;
            int customers = validInvoices.Where(h => !string.IsNullOrEmpty(h.MaKH)).Select(h => h.MaKH).Distinct().Count();

            return (revenue, tickets, services, invoices, customers);
        }

        public void ExportComprehensiveSummaryToExcel(DateTime fromDate, DateTime toDate, string filePath)
        {
            var from = fromDate.Date;
            var to = toDate.Date.AddDays(1).AddTicks(-1);

            var validInvoices = _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.ChiTietHoaDons)
                .Where(h => h.TrangThai == "Đã thanh toán" && h.NgayLap >= from && h.NgayLap <= to)
                .OrderBy(h => h.NgayLap)
                .ToList();

            var allItems = validInvoices.SelectMany(h => h.ChiTietHoaDons).ToList();

            using var workbook = new ClosedXML.Excel.XLWorkbook();

            // ---------------------------------------------------------
            // SHEET 1: TỔNG QUAN (KPIs & Cơ cấu thanh toán)
            // ---------------------------------------------------------
            var ws1 = workbook.Worksheets.Add("Tổng Quan");
            ws1.ShowGridLines = true;

            // Banner Title
            ws1.Cell(1, 1).Value = "BÁO CÁO TỔNG KẾT HOẠT ĐỘNG KHU VUI CHƠI";
            var titleRange = ws1.Range(1, 1, 1, 5);
            titleRange.Merge();
            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.FontSize = 15;
            titleRange.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
            titleRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#1E3A8A");
            titleRange.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            titleRange.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
            ws1.Row(1).Height = 35;

            ws1.Cell(2, 1).Value = $"Kỳ báo cáo: Từ {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}  |  Thời gian xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}  |  Hệ thống: Wonderland Park POS";
            var subRange = ws1.Range(2, 1, 2, 5);
            subRange.Merge();
            subRange.Style.Font.Italic = true;
            subRange.Style.Font.FontSize = 9.5;
            subRange.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromHtml("#475569");
            subRange.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            ws1.Row(2).Height = 22;

            // Section 1: KPI Metrics
            ws1.Cell(4, 1).Value = "I. CÁC CHỈ SỐ HOẠT ĐỘNG CHÍNH (KEY PERFORMANCE INDICATORS)";
            var sec1Range = ws1.Range(4, 1, 4, 5);
            sec1Range.Merge();
            sec1Range.Style.Font.Bold = true;
            sec1Range.Style.Font.FontSize = 11;
            sec1Range.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromHtml("#1E3A8A");

            string[] kpiHeaders = { "STT", "CHỈ SỐ HOẠT ĐỘNG", "GIÁ TRỊ", "ĐƠN VỊ", "GHI CHÚ / CHI TIẾT" };
            for (int i = 0; i < kpiHeaders.Length; i++)
            {
                var c = ws1.Cell(5, i + 1);
                c.Value = kpiHeaders[i];
                c.Style.Font.Bold = true;
                c.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                c.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#2563EB");
                c.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                c.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                c.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            }
            ws1.Row(5).Height = 25;

            decimal totalRevenue = validInvoices.Sum(h => h.TongTien);
            decimal ticketRevenue = allItems.Where(i => i.LoaiHang == "Ve").Sum(i => i.ThanhTien);
            decimal serviceRevenue = allItems.Where(i => i.LoaiHang == "DichVu").Sum(i => i.ThanhTien);
            int totalInvoices = validInvoices.Count;
            int totalTickets = allItems.Where(i => i.LoaiHang == "Ve").Sum(i => i.SoLuong);
            int totalServices = allItems.Where(i => i.LoaiHang == "DichVu").Sum(i => i.SoLuong);
            decimal aov = totalInvoices > 0 ? Math.Round(totalRevenue / totalInvoices, 0) : 0;
            int registeredCustCount = validInvoices.Where(h => !string.IsNullOrEmpty(h.MaKH)).Select(h => h.MaKH).Distinct().Count();
            int guestInvoicesCount = validInvoices.Count(h => string.IsNullOrEmpty(h.MaKH));

            var kpiRows = new List<(string Name, object Value, string Unit, string Note, bool isCurrency)>
            {
                ("Tổng doanh thu thực thu", (double)totalRevenue, "VNĐ", "Doanh thu sau khi đã trừ giảm giá/chiết khấu", true),
                ("Doanh thu bán vé vào cổng", (double)ticketRevenue, "VNĐ", totalRevenue > 0 ? $"{ticketRevenue / totalRevenue * 100:N1}% tổng doanh thu" : "0%", true),
                ("Doanh thu dịch vụ & ẩm thực", (double)serviceRevenue, "VNĐ", totalRevenue > 0 ? $"{serviceRevenue / totalRevenue * 100:N1}% tổng doanh thu" : "0%", true),
                ("Tổng số hóa đơn giao dịch", totalInvoices, "Hóa đơn", "Số giao dịch hoàn tất thành công", false),
                ("Giá trị trung bình mỗi đơn (AOV)", (double)aov, "VNĐ / HĐ", "Doanh thu trung bình trên mỗi hóa đơn", true),
                ("Tổng số lượt vé bán ra", totalTickets, "Lượt vé", "Khách vào cổng tham quan", false),
                ("Tổng số dịch vụ phục vụ", totalServices, "Lượt DV", "Các dịch vụ tiện ích, ăn uống đã bán", false),
                ("Khách hàng thành viên mua hàng", registeredCustCount, "Khách hàng", "Khách hàng có thông tin lưu trên hệ thống", false),
                ("Lượt khách vãng lai mua tại quầy", guestInvoicesCount, "Lượt mua", "Khách lẻ mua trực tiếp không lưu tên", false)
            };

            int curRow = 6;
            for (int r = 0; r < kpiRows.Count; r++)
            {
                var rowData = kpiRows[r];
                ws1.Cell(curRow, 1).Value = r + 1;
                ws1.Cell(curRow, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws1.Cell(curRow, 2).Value = rowData.Name;
                ws1.Cell(curRow, 2).Style.Font.Bold = (r == 0);

                var valCell = ws1.Cell(curRow, 3);
                if (rowData.isCurrency)
                {
                    valCell.Value = Convert.ToDouble(rowData.Value);
                    valCell.Style.NumberFormat.Format = "#,##0";
                    valCell.Style.Font.Bold = true;
                    if (r == 0) valCell.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromHtml("#16A34A");
                }
                else
                {
                    valCell.Value = Convert.ToInt64(rowData.Value);
                    valCell.Style.NumberFormat.Format = "#,##0";
                    valCell.Style.Font.Bold = true;
                }
                valCell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                ws1.Cell(curRow, 4).Value = rowData.Unit;
                ws1.Cell(curRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws1.Cell(curRow, 5).Value = rowData.Note;
                ws1.Cell(curRow, 5).Style.Font.Italic = true;

                for (int c = 1; c <= 5; c++)
                {
                    var cell = ws1.Cell(curRow, c);
                    cell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    cell.Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.FromHtml("#E2E8F0");
                    if (r % 2 == 1) cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#F8FAFC");
                }
                ws1.Row(curRow).Height = 22;
                curRow++;
            }

            // Section 2: Phương thức thanh toán
            curRow += 2;
            ws1.Cell(curRow, 1).Value = "II. PHÂN BỔ DOANH THU THEO PHƯƠNG THỨC THANH TOÁN";
            var sec2Range = ws1.Range(curRow, 1, curRow, 5);
            sec2Range.Merge();
            sec2Range.Style.Font.Bold = true;
            sec2Range.Style.Font.FontSize = 11;
            sec2Range.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromHtml("#1E3A8A");
            curRow++;

            string[] payHeaders = { "STT", "PHƯƠNG THỨC THANH TOÁN", "SỐ GIAO DỊCH", "TỔNG TIỀN (VNĐ)", "TỶ TRỌNG (%)" };
            for (int i = 0; i < payHeaders.Length; i++)
            {
                var c = ws1.Cell(curRow, i + 1);
                c.Value = payHeaders[i];
                c.Style.Font.Bold = true;
                c.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                c.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#2563EB");
                c.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                c.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                c.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            }
            ws1.Row(curRow).Height = 25;
            curRow++;

            var payGrouped = validInvoices
                .GroupBy(h => string.IsNullOrWhiteSpace(h.PhuongThucThanhToan) ? "Khác" : h.PhuongThucThanhToan)
                .Select(g => new
                {
                    Method = g.Key,
                    Count = g.Count(),
                    Amount = g.Sum(h => h.TongTien)
                })
                .OrderByDescending(p => p.Amount)
                .ToList();

            int payIndex = 1;
            foreach (var p in payGrouped)
            {
                ws1.Cell(curRow, 1).Value = payIndex++;
                ws1.Cell(curRow, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                ws1.Cell(curRow, 2).Value = p.Method;

                ws1.Cell(curRow, 3).Value = p.Count;
                ws1.Cell(curRow, 3).Style.NumberFormat.Format = "#,##0";
                ws1.Cell(curRow, 3).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                ws1.Cell(curRow, 4).Value = (double)p.Amount;
                ws1.Cell(curRow, 4).Style.NumberFormat.Format = "#,##0";
                ws1.Cell(curRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                double pct = totalRevenue > 0 ? (double)(p.Amount / totalRevenue * 100) : 0;
                ws1.Cell(curRow, 5).Value = $"{pct:N1}%";
                ws1.Cell(curRow, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                for (int c = 1; c <= 5; c++)
                {
                    var cell = ws1.Cell(curRow, c);
                    cell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    cell.Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.FromHtml("#E2E8F0");
                    if (payIndex % 2 == 1) cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#F8FAFC");
                }
                ws1.Row(curRow).Height = 22;
                curRow++;
            }

            ws1.Columns().AdjustToContents(10.0, 50.0);

            // ---------------------------------------------------------
            // SHEET 2: DOANH THU THEO NGÀY
            // ---------------------------------------------------------
            var ws2 = workbook.Worksheets.Add("Doanh Thu Theo Ngày");
            ws2.ShowGridLines = true;

            ws2.Cell(1, 1).Value = "DOANH THU THEO TỪNG NGÀY TRONG KỲ";
            var r2Title = ws2.Range(1, 1, 1, 7);
            r2Title.Merge();
            r2Title.Style.Font.Bold = true;
            r2Title.Style.Font.FontSize = 14;
            r2Title.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
            r2Title.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#1E3A8A");
            r2Title.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            r2Title.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
            ws2.Row(1).Height = 32;

            string[] dailyHeaders = { "STT", "NGÀY GIAO DỊCH", "SỐ HÓA ĐƠN", "SỐ VÉ BÁN", "DOANH THU VÉ (VNĐ)", "DOANH THU DỊCH VỤ (VNĐ)", "TỔNG DOANH THU (VNĐ)" };
            for (int i = 0; i < dailyHeaders.Length; i++)
            {
                var c = ws2.Cell(3, i + 1);
                c.Value = dailyHeaders[i];
                c.Style.Font.Bold = true;
                c.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                c.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#2563EB");
                c.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                c.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                c.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            }
            ws2.Row(3).Height = 25;

            var dailyGroups = validInvoices
                .GroupBy(h => h.NgayLap.Date)
                .OrderBy(g => g.Key)
                .ToList();

            int dRow = 4;
            int dIndex = 1;
            foreach (var g in dailyGroups)
            {
                var gItems = g.SelectMany(h => h.ChiTietHoaDons).ToList();
                decimal dTicketRev = gItems.Where(i => i.LoaiHang == "Ve").Sum(i => i.ThanhTien);
                decimal dServiceRev = gItems.Where(i => i.LoaiHang == "DichVu").Sum(i => i.ThanhTien);
                decimal dTotalRev = g.Sum(h => h.TongTien);
                int dTickets = gItems.Where(i => i.LoaiHang == "Ve").Sum(i => i.SoLuong);

                ws2.Cell(dRow, 1).Value = dIndex++;
                ws2.Cell(dRow, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws2.Cell(dRow, 2).Value = g.Key.ToString("dd/MM/yyyy");
                ws2.Cell(dRow, 2).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws2.Cell(dRow, 3).Value = g.Count();
                ws2.Cell(dRow, 3).Style.NumberFormat.Format = "#,##0";
                ws2.Cell(dRow, 3).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                ws2.Cell(dRow, 4).Value = dTickets;
                ws2.Cell(dRow, 4).Style.NumberFormat.Format = "#,##0";
                ws2.Cell(dRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                ws2.Cell(dRow, 5).Value = (double)dTicketRev;
                ws2.Cell(dRow, 5).Style.NumberFormat.Format = "#,##0";
                ws2.Cell(dRow, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                ws2.Cell(dRow, 6).Value = (double)dServiceRev;
                ws2.Cell(dRow, 6).Style.NumberFormat.Format = "#,##0";
                ws2.Cell(dRow, 6).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                ws2.Cell(dRow, 7).Value = (double)dTotalRev;
                ws2.Cell(dRow, 7).Style.NumberFormat.Format = "#,##0";
                ws2.Cell(dRow, 7).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;
                ws2.Cell(dRow, 7).Style.Font.Bold = true;

                for (int c = 1; c <= 7; c++)
                {
                    var cell = ws2.Cell(dRow, c);
                    cell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    cell.Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.FromHtml("#E2E8F0");
                    if (dIndex % 2 == 1) cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#F8FAFC");
                }
                ws2.Row(dRow).Height = 22;
                dRow++;
            }

            // Summary Total Row
            ws2.Cell(dRow, 1).Value = "TỔNG CỘNG";
            var dTotRange = ws2.Range(dRow, 1, dRow, 2);
            dTotRange.Merge();
            dTotRange.Style.Font.Bold = true;
            dTotRange.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

            ws2.Cell(dRow, 3).Value = totalInvoices;
            ws2.Cell(dRow, 3).Style.NumberFormat.Format = "#,##0";
            ws2.Cell(dRow, 3).Style.Font.Bold = true;
            ws2.Cell(dRow, 3).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

            ws2.Cell(dRow, 4).Value = totalTickets;
            ws2.Cell(dRow, 4).Style.NumberFormat.Format = "#,##0";
            ws2.Cell(dRow, 4).Style.Font.Bold = true;
            ws2.Cell(dRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

            ws2.Cell(dRow, 5).Value = (double)ticketRevenue;
            ws2.Cell(dRow, 5).Style.NumberFormat.Format = "#,##0";
            ws2.Cell(dRow, 5).Style.Font.Bold = true;
            ws2.Cell(dRow, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

            ws2.Cell(dRow, 6).Value = (double)serviceRevenue;
            ws2.Cell(dRow, 6).Style.NumberFormat.Format = "#,##0";
            ws2.Cell(dRow, 6).Style.Font.Bold = true;
            ws2.Cell(dRow, 6).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

            ws2.Cell(dRow, 7).Value = (double)totalRevenue;
            ws2.Cell(dRow, 7).Style.NumberFormat.Format = "#,##0";
            ws2.Cell(dRow, 7).Style.Font.Bold = true;
            ws2.Cell(dRow, 7).Style.Font.FontColor = ClosedXML.Excel.XLColor.FromHtml("#16A34A");
            ws2.Cell(dRow, 7).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

            for (int c = 1; c <= 7; c++)
            {
                var cell = ws2.Cell(dRow, c);
                cell.Style.Border.TopBorder = ClosedXML.Excel.XLBorderStyleValues.Medium;
                cell.Style.Border.BottomBorder = ClosedXML.Excel.XLBorderStyleValues.Double;
                cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#F1F5F9");
            }
            ws2.Row(dRow).Height = 24;

            ws2.Columns().AdjustToContents(10.0, 45.0);

            // ---------------------------------------------------------
            // SHEET 3: CHI TIẾT VÉ & DỊCH VỤ ĐÃ BÁN
            // ---------------------------------------------------------
            var ws3 = workbook.Worksheets.Add("Chi Tiết Bán Hàng");
            ws3.ShowGridLines = true;

            ws3.Cell(1, 1).Value = "TỔNG HỢP DANH MỤC VÉ & DỊCH VỤ ĐÃ BÁN";
            var r3Title = ws3.Range(1, 1, 1, 7);
            r3Title.Merge();
            r3Title.Style.Font.Bold = true;
            r3Title.Style.Font.FontSize = 14;
            r3Title.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
            r3Title.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#1E3A8A");
            r3Title.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            r3Title.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
            ws3.Row(1).Height = 32;

            string[] itemHeaders = { "STT", "MÃ HÀNG", "TÊN MẶT HÀNG", "PHÂN LOẠI", "SỐ LƯỢNG BÁN", "DOANH THU (VNĐ)", "TỶ TRỌNG (%)" };
            for (int i = 0; i < itemHeaders.Length; i++)
            {
                var c = ws3.Cell(3, i + 1);
                c.Value = itemHeaders[i];
                c.Style.Font.Bold = true;
                c.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                c.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#2563EB");
                c.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                c.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                c.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            }
            ws3.Row(3).Height = 25;

            var itemGroups = allItems
                .GroupBy(i => new { i.MaHang, i.TenHang, i.LoaiHang })
                .Select(g => new
                {
                    g.Key.MaHang,
                    g.Key.TenHang,
                    Loai = g.Key.LoaiHang == "Ve" ? "Vé vào cổng" : "Dịch vụ & Ẩm thực",
                    Qty = g.Sum(x => x.SoLuong),
                    Amount = g.Sum(x => x.ThanhTien)
                })
                .OrderByDescending(x => x.Amount)
                .ToList();

            int itemRow = 4;
            int itemIndex = 1;
            foreach (var item in itemGroups)
            {
                ws3.Cell(itemRow, 1).Value = itemIndex++;
                ws3.Cell(itemRow, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws3.Cell(itemRow, 2).Value = item.MaHang;
                ws3.Cell(itemRow, 2).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws3.Cell(itemRow, 3).Value = item.TenHang;

                ws3.Cell(itemRow, 4).Value = item.Loai;
                ws3.Cell(itemRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws3.Cell(itemRow, 5).Value = item.Qty;
                ws3.Cell(itemRow, 5).Style.NumberFormat.Format = "#,##0";
                ws3.Cell(itemRow, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                ws3.Cell(itemRow, 6).Value = (double)item.Amount;
                ws3.Cell(itemRow, 6).Style.NumberFormat.Format = "#,##0";
                ws3.Cell(itemRow, 6).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                double pct = totalRevenue > 0 ? (double)(item.Amount / totalRevenue * 100) : 0;
                ws3.Cell(itemRow, 7).Value = $"{pct:N1}%";
                ws3.Cell(itemRow, 7).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                for (int c = 1; c <= 7; c++)
                {
                    var cell = ws3.Cell(itemRow, c);
                    cell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    cell.Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.FromHtml("#E2E8F0");
                    if (itemIndex % 2 == 1) cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#F8FAFC");
                }
                ws3.Row(itemRow).Height = 22;
                itemRow++;
            }

            ws3.Columns().AdjustToContents(10.0, 50.0);

            // ---------------------------------------------------------
            // SHEET 4: TOP KHÁCH HÀNG
            // ---------------------------------------------------------
            var ws4 = workbook.Worksheets.Add("Top Khách Hàng");
            ws4.ShowGridLines = true;

            ws4.Cell(1, 1).Value = "TOP KHÁCH HÀNG CÓ CHI TIÊU NHIỀU NHẤT TRONG KỲ";
            var r4Title = ws4.Range(1, 1, 1, 6);
            r4Title.Merge();
            r4Title.Style.Font.Bold = true;
            r4Title.Style.Font.FontSize = 14;
            r4Title.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
            r4Title.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#1E3A8A");
            r4Title.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            r4Title.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
            ws4.Row(1).Height = 32;

            string[] custHeaders = { "TOP", "MÃ KH", "HỌ TÊN KHÁCH HÀNG", "SỐ ĐIỆN THOẠI", "SỐ LẦN GIAO DỊCH", "TỔNG CHI TIÊU (VNĐ)" };
            for (int i = 0; i < custHeaders.Length; i++)
            {
                var c = ws4.Cell(3, i + 1);
                c.Value = custHeaders[i];
                c.Style.Font.Bold = true;
                c.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                c.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#2563EB");
                c.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                c.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                c.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            }
            ws4.Row(3).Height = 25;

            var topCusts = validInvoices
                .GroupBy(h => new
                {
                    MaKH = h.KhachHang != null ? h.KhachHang.MaKH : (h.MaKH ?? "LE"),
                    HoTen = h.KhachHang != null ? h.KhachHang.HoTen : "Khách vãng lai tại quầy",
                    SDT = h.KhachHang != null ? h.KhachHang.SoDienThoai : "-"
                })
                .Select(g => new
                {
                    g.Key.MaKH,
                    g.Key.HoTen,
                    g.Key.SDT,
                    Orders = g.Count(),
                    Spend = g.Sum(h => h.TongTien)
                })
                .OrderByDescending(x => x.Spend)
                .Take(50)
                .ToList();

            int cRow = 4;
            int cIndex = 1;
            foreach (var cust in topCusts)
            {
                ws4.Cell(cRow, 1).Value = cIndex++;
                ws4.Cell(cRow, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws4.Cell(cRow, 2).Value = cust.MaKH;
                ws4.Cell(cRow, 2).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws4.Cell(cRow, 3).Value = cust.HoTen;

                ws4.Cell(cRow, 4).Value = cust.SDT;
                ws4.Cell(cRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws4.Cell(cRow, 5).Value = cust.Orders;
                ws4.Cell(cRow, 5).Style.NumberFormat.Format = "#,##0";
                ws4.Cell(cRow, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                ws4.Cell(cRow, 6).Value = (double)cust.Spend;
                ws4.Cell(cRow, 6).Style.NumberFormat.Format = "#,##0";
                ws4.Cell(cRow, 6).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;
                ws4.Cell(cRow, 6).Style.Font.Bold = true;

                for (int c = 1; c <= 6; c++)
                {
                    var cell = ws4.Cell(cRow, c);
                    cell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    cell.Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.FromHtml("#E2E8F0");
                    if (cIndex % 2 == 1) cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#F8FAFC");
                }
                ws4.Row(cRow).Height = 22;
                cRow++;
            }

            ws4.Columns().AdjustToContents(10.0, 45.0);

            // Ensure directory exists & save
            var dir = System.IO.Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

            workbook.SaveAs(filePath);
        }
    }
}
