using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhuVuiChoi.Models
{
    [Table("Ve")]
    public class Ve
    {
        [Key]
        [StringLength(20)]
        public string MaVe { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TenVe { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LoaiVe { get; set; } = string.Empty; // Vé ngày, Vé trẻ em, Vé người lớn, Vé gia đình, Vé VIP

        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiaVe { get; set; }

        [StringLength(50)]
        public string ThoiHan { get; set; } = "Trong ngày";

        [StringLength(255)]
        public string? MoTa { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; } = "Đang áp dụng"; // Đang áp dụng, Ngừng áp dụng
    }
}
