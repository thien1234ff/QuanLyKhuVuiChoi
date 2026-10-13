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
    }
}
