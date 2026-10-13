using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhuVuiChoi.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [StringLength(20)]
        public string MaNV { get; set; } = string.Empty;

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

        [Required]
        [StringLength(50)]
        public string ChucVu { get; set; } = string.Empty;

        public DateTime NgayVaoLam { get; set; } = DateTime.Today;

        [StringLength(50)]
        public string TrangThai { get; set; } = "Đang làm việc";

        // Navigation properties
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        public virtual TaiKhoan? TaiKhoan { get; set; }
    }
}
