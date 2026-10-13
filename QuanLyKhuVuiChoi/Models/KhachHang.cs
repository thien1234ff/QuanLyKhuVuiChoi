using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhuVuiChoi.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [StringLength(20)]
        public string MaKH { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; } = "Nam";

        [Required]
        [StringLength(20)]
        public string SoDienThoai { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? DiaChi { get; set; }

        public DateTime NgayDangKy { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}
