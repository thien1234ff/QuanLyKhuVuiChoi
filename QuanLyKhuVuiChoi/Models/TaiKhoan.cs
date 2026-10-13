using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhuVuiChoi.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [StringLength(50)]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string MatKhau { get; set; } = string.Empty;

        [StringLength(20)]
        public string? MaNV { get; set; }

        [Required]
        [StringLength(20)]
        public string Quyen { get; set; } = "NhanVien"; // Admin, NhanVien

        public bool TrangThai { get; set; } = true;

        [ForeignKey("MaNV")]
        public virtual NhanVien? NhanVien { get; set; }
    }
}
