using System;

namespace QuanLyKhuVuiChoi.DTOs
{
    public class RevenueReportDto
    {
        public string ThoiGian { get; set; } = string.Empty; // Ngày / Tháng / Năm
        public int SoHoaDon { get; set; } = 0;
        public int SoVeBan { get; set; } = 0;
        public decimal DoanhThuVe { get; set; } = 0;
        public decimal DoanhThuDichVu { get; set; } = 0;
        public decimal DoanhThuTroChoi { get; set; } = 0;
        public decimal TongDoanhThu { get; set; } = 0;
    }

    public class CategoryRevenueDto
    {
        public string TenHangMuc { get; set; } = string.Empty;
        public string Loai { get; set; } = string.Empty;
        public int SoLuongDaBan { get; set; } = 0;
        public decimal DoanhThu { get; set; } = 0;
        public double TiLePhanTram { get; set; } = 0;
    }
}
