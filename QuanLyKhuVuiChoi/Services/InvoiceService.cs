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
    public class InvoiceService
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;

        public InvoiceService(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
        }

        public List<HoaDon> GetAll()
        {
            return _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.ChiTietHoaDons)
                .OrderByDescending(h => h.NgayLap)
                .ToList();
        }

        public HoaDon? GetById(string maHoaDon)
        {
            return _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.ChiTietHoaDons)
                .FirstOrDefault(h => h.MaHoaDon == maHoaDon);
        }

        public List<HoaDon> Filter(string keyword, DateTime fromDate, DateTime toDate, string? maKH = null)
        {
            var query = _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.ChiTietHoaDons)
                .Where(h => h.NgayLap.Date >= fromDate.Date && h.NgayLap.Date <= toDate.Date);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var kw = keyword.Trim().ToLower();
                query = query.Where(h => h.MaHoaDon.ToLower().Contains(kw) ||
                                         (h.KhachHang != null && h.KhachHang.HoTen.ToLower().Contains(kw)) ||
                                         (h.NhanVien != null && h.NhanVien.HoTen.ToLower().Contains(kw)));
            }

            if (!string.IsNullOrWhiteSpace(maKH) && maKH != "Tất cả")
            {
                query = query.Where(h => h.MaKH == maKH);
            }

            return query.OrderByDescending(h => h.NgayLap).ToList();
        }

        public string GenerateNextId()
        {
            var lastId = _context.HoaDons
                .Where(h => h.MaHoaDon.StartsWith("HD"))
                .OrderByDescending(h => h.MaHoaDon)
                .Select(h => h.MaHoaDon)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(lastId) || lastId.Length <= 2)
            {
                return "HD001";
            }

            if (int.TryParse(lastId.Substring(2), out int num))
            {
                return $"HD{(num + 1):D3}";
            }

            return $"HD{DateTime.Now:yyyyMMddHHmmss}";
        }

        public (bool Success, string Message, string? MaHoaDon) CreateInvoice(
            string? maKH,
            string? maNV,
            List<CartItemDto> items,
            decimal giamGiaPercent,
            string phuongThucThanhToan,
            string? ghiChu)
        {
            if (items == null || !items.Any())
                return (false, "Giỏ hàng hiện đang trống. Vui lòng chọn vé hoặc dịch vụ cần mua.", null);

            decimal subtotal = items.Sum(i => i.ThanhTien);
            if (giamGiaPercent < 0) giamGiaPercent = 0;
            if (giamGiaPercent > 100) giamGiaPercent = 100;

            decimal discountAmount = subtotal * (giamGiaPercent / 100m);
            decimal total = subtotal - discountAmount;
            if (total < 0) total = 0;

            string maHD = GenerateNextId();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var hoaDon = new HoaDon
                    {
                        MaHoaDon = maHD,
                        MaKH = string.IsNullOrWhiteSpace(maKH) ? null : maKH,
                        MaNV = string.IsNullOrWhiteSpace(maNV) ? null : maNV,
                        NgayLap = DateTime.Now,
                        TongTien = total,
                        GiamGia = giamGiaPercent,
                        PhuongThucThanhToan = string.IsNullOrWhiteSpace(phuongThucThanhToan) ? "Tiền mặt" : phuongThucThanhToan,
                        TrangThai = "Đã thanh toán",
                        GhiChu = ghiChu
                    };

                    _context.HoaDons.Add(hoaDon);

                    foreach (var item in items)
                    {
                        var cthd = new ChiTietHoaDon
                        {
                            MaHoaDon = maHD,
                            LoaiHang = item.LoaiHang,
                            MaHang = item.MaHang,
                            TenHang = item.TenHang,
                            SoLuong = item.SoLuong,
                            DonGia = item.DonGia,
                            ThanhTien = item.ThanhTien
                        };
                        _context.ChiTietHoaDons.Add(cthd);

                        // Trừ tồn kho nếu là Dịch vụ
                        if (item.LoaiHang == "DichVu")
                        {
                            var dv = _context.DichVus.FirstOrDefault(d => d.MaDichVu == item.MaHang);
                            if (dv != null)
                            {
                                dv.SoLuongTon = Math.Max(0, dv.SoLuongTon - item.SoLuong);
                                if (dv.SoLuongTon == 0) dv.TrangThai = "Hết hàng";
                            }
                        }
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                    return (true, $"Thanh toán thành công! Mã hóa đơn: {maHD}", maHD);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi xử lý tạo hóa đơn: {ex.Message}", null);
                }
            }
        }

        public (bool Success, string Message) CancelInvoice(string maHoaDon, string lyDo)
        {
            var hd = _context.HoaDons.Include(h => h.ChiTietHoaDons).FirstOrDefault(h => h.MaHoaDon == maHoaDon);
            if (hd == null)
                return (false, $"Không tìm thấy hóa đơn '{maHoaDon}'.");

            if (hd.TrangThai == "Đã hủy")
                return (false, "Hóa đơn này đã ở trạng thái Đã hủy trước đó.");

            try
            {
                hd.TrangThai = "Đã hủy";
                hd.GhiChu = string.IsNullOrWhiteSpace(hd.GhiChu)
                    ? $"[Đã hủy lúc {DateTime.Now:dd/MM/yyyy HH:mm}: {lyDo}]"
                    : $"{hd.GhiChu} | [Hủy: {lyDo}]";

                // Hoàn lại kho nếu là dịch vụ
                foreach (var item in hd.ChiTietHoaDons)
                {
                    if (item.LoaiHang == "DichVu")
                    {
                        var dv = _context.DichVus.FirstOrDefault(d => d.MaDichVu == item.MaHang);
                        if (dv != null)
                        {
                            dv.SoLuongTon += item.SoLuong;
                            if (dv.TrangThai == "Hết hàng" && dv.SoLuongTon > 0)
                                dv.TrangThai = "Đang phục vụ";
                        }
                    }
                }

                _context.SaveChanges();
                return (true, $"Đã hủy hóa đơn '{maHoaDon}' thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hủy hóa đơn: {ex.Message}");
            }
        }

        public (bool Success, string Message) DeleteInvoice(string maHoaDon)
        {
            var hd = _context.HoaDons.Include(h => h.ChiTietHoaDons).FirstOrDefault(h => h.MaHoaDon == maHoaDon);
            if (hd == null)
                return (false, $"Không tìm thấy hóa đơn '{maHoaDon}'.");

            try
            {
                _context.HoaDons.Remove(hd);
                _context.SaveChanges();
                return (true, $"Đã xóa vĩnh viễn hóa đơn '{maHoaDon}' khỏi hệ thống.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi xóa hóa đơn: {ex.Message}");
            }
        }

        public void ExportInvoiceListToExcel(List<HoaDon> invoices, string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("Mã Hóa Đơn");
            dt.Columns.Add("Mã KH");
            dt.Columns.Add("Tên Khách Hàng");
            dt.Columns.Add("Thu Ngân");
            dt.Columns.Add("Ngày Lập", typeof(DateTime));
            dt.Columns.Add("Giảm Giá (%)", typeof(decimal));
            dt.Columns.Add("Tổng Tiền", typeof(decimal));
            dt.Columns.Add("PT Thanh Toán");
            dt.Columns.Add("Trạng Thái");
            dt.Columns.Add("Ghi Chú");

            foreach (var h in invoices)
            {
                dt.Rows.Add(
                    h.MaHoaDon,
                    h.MaKH ?? "Khách vãng lai",
                    h.KhachHang?.HoTen ?? "Khách vãng lai",
                    h.NhanVien?.HoTen ?? "Hệ thống",
                    h.NgayLap,
                    h.GiamGia,
                    h.TongTien,
                    h.PhuongThucThanhToan,
                    h.TrangThai,
                    h.GhiChu ?? ""
                );
            }

            _excelService.ExportDataTableToExcel(dt, "HoaDon", filePath, "DANH SÁCH HÓA ĐƠN BÁN HÀNG");
        }
    }
}
