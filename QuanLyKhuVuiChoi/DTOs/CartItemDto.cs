namespace QuanLyKhuVuiChoi.DTOs
{
    public class CartItemDto
    {
        public string LoaiHang { get; set; } = "Ve"; // Ve, DichVu, TroChoi
        public string MaHang { get; set; } = string.Empty;
        public string TenHang { get; set; } = string.Empty;
        public int SoLuong { get; set; } = 1;
        public decimal DonGia { get; set; } = 0;
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
