using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhuVuiChoi.Models
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTHD { get; set; }

        [Required]
        [StringLength(20)]
        public string MaHoaDon { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string LoaiHang { get; set; } = "Ve"; // Ve, DichVu, TroChoi

        [Required]
        [StringLength(20)]
        public string MaHang { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TenHang { get; set; } = string.Empty;

        public int SoLuong { get; set; } = 1;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DonGia { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ThanhTien { get; set; } = 0;

        // Navigation property
        [ForeignKey("MaHoaDon")]
        public virtual HoaDon? HoaDon { get; set; }
    }
}
