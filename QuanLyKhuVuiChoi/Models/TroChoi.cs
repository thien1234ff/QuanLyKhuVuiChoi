using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhuVuiChoi.Models
{
    [Table("TroChoi")]
    public class TroChoi
    {
        [Key]
        [StringLength(20)]
        public string MaTroChoi { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TenTroChoi { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LoaiTroChoi { get; set; } = string.Empty; // Cảm giác mạnh, Gia đình, Trẻ em, Dưới nước, Phiêu lưu

        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiaVe { get; set; }

        public int DoTuoiToiThieu { get; set; } = 0;

        public int SucChua { get; set; } = 1;

        [StringLength(50)]
        public string TrangThai { get; set; } = "Đang hoạt động"; // Đang hoạt động, Bảo trì, Tạm ngưng

        [StringLength(255)]
        public string? MoTa { get; set; }
    }
}
