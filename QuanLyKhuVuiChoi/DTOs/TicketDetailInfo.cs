using System;
using System.Collections.Generic;

namespace QuanLyKhuVuiChoi.DTOs
{
    /// <summary>
    /// Thông tin mô tả chi tiết quyền lợi và dịch vụ bao gồm trong vé
    /// </summary>
    public class TicketDetailInfo
    {
        public string MaVe { get; set; } = string.Empty;
        public string TenVe { get; set; } = string.Empty;
        public string LoaiVe { get; set; } = "Vé ngày";
        public decimal GiaVe { get; set; }
        public string ThoiHan { get; set; } = "Trong ngày";
        public string TrangThai { get; set; } = "Đang áp dụng";

        /// <summary>
        /// Tóm tắt nhanh quyền lợi nổi bật hiển thị trên Card
        /// </summary>
        public string Highlights { get; set; } = string.Empty;

        /// <summary>
        /// Danh sách cụ thể các trò chơi / khu vực được tham gia ("Vé đó có gì")
        /// </summary>
        public List<string> BaoGom { get; set; } = new List<string>();

        /// <summary>
        /// Quà tặng và tiện ích đi kèm (Voucher, tủ gửi đồ, nước uống...)
        /// </summary>
        public List<string> QuaTangTienIch { get; set; } = new List<string>();

        /// <summary>
        /// Đối tượng & điều kiện áp dụng (Chiều cao, độ tuổi, xuất trình thẻ SV...)
        /// </summary>
        public string DoiTuongDieuKien { get; set; } = string.Empty;

        /// <summary>
        /// Ghi chú & quy định khi sử dụng vé
        /// </summary>
        public string LuuY { get; set; } = string.Empty;
    }
}
