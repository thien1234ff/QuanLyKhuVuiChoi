using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhuVuiChoi.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [StringLength(20)]
        public string MaHoaDon { get; set; } = string.Empty;

        [StringLength(20)]
        public string? MaKH { get; set; }

        [StringLength(20)]
        public string? MaNV { get; set; }

        public DateTime NgayLap { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TongTien { get; set; } = 0;

        [Column(TypeName = "decimal(5, 2)")]
        public decimal GiamGia { get; set; } = 0; // % giảm giá

        [StringLength(50)]
        public string PhuongThucThanhToan { get; set; } = "Tiền mặt"; // Tiền mặt, Chuyển khoản, Thẻ

        [StringLength(50)]
        public string TrangThai { get; set; } = "Đã thanh toán"; // Đã thanh toán, Đã hủy

        [StringLength(255)]
        public string? GhiChu { get; set; }

        // Navigation properties
        [ForeignKey("MaKH")]
        public virtual KhachHang? KhachHang { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien? NhanVien { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}
