using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhuVuiChoi.Models
{
    [Table("DichVu")]
    public class DichVu
    {
        [Key]
        [StringLength(20)]
        public string MaDichVu { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TenDichVu { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LoaiDichVu { get; set; } = string.Empty; // Ẩm thực, Nước uống, Thuê đồ, Khu nghỉ ngơi, Khác

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DonGia { get; set; }

        public int SoLuongTon { get; set; } = 0;

        [StringLength(50)]
        public string TrangThai { get; set; } = "Đang phục vụ"; // Đang phục vụ, Hết hàng, Tạm ngưng

        [StringLength(255)]
        public string? MoTa { get; set; }
    }
}
