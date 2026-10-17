using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Services
{
    /// <summary>
    /// Danh mục và dịch vụ cung cấp mô tả chi tiết quyền lợi vé ("Vé này có những gì?")
    /// </summary>
    public static class TicketDetailCatalog
    {
        private static readonly Dictionary<string, TicketDetailInfo> _curatedTickets = new(StringComparer.OrdinalIgnoreCase)
        {
            ["VE001"] = new TicketDetailInfo
            {
                MaVe = "VE001",
                TenVe = "Vé vào cổng tiêu chuẩn",
                LoaiVe = "Vé ngày",
                GiaVe = 100000,
                ThoiHan = "Trong ngày",
                Highlights = "Vào cổng tham quan + Đu quay ngựa gỗ + Show nghệ thuật đường phố",
                BaoGom = new List<string>
                {
                    "Tham quan tự do toàn bộ cảnh quan, hoa viên sinh thái và đài phun nước",
                    "Chơi miễn phí các trò chơi ngoài trời cơ bản: Đu quay ngựa gỗ, xích đu cảnh quan, bãi cát kỳ thú",
                    "Thưởng thức các chương trình biểu diễn nghệ thuật xiếc ảo thuật và diễu hành Carnival (theo khung giờ)",
                    "Chụp ảnh check-in tự do không giới hạn tại toàn bộ các đại cảnh tiểu cảnh"
                },
                QuaTangTienIch = new List<string>
                {
                    "Bản đồ chỉ dẫn tham quan khu vui chơi miễn phí",
                    "Wifi công cộng miễn phí toàn khu vực và ghế nghỉ ngơi ven hồ"
                },
                DoiTuongDieuKien = "Áp dụng cho mọi đối tượng khách hàng (người lớn và trẻ em)",
                LuuY = "Không bao gồm cụm trò chơi cảm giác mạnh, khu công viên nước và dịch vụ ẩm thực"
            },

            ["VE002"] = new TicketDetailInfo
            {
                MaVe = "VE002",
                TenVe = "Vé trọn gói người lớn (All-in-one)",
                LoaiVe = "Vé người lớn",
                GiaVe = 250000,
                ThoiHan = "Trong ngày",
                Highlights = "Chơi KHÔNG GIỚI HẠN tất cả trò chơi khô, cảm giác mạnh & công viên nước",
                BaoGom = new List<string>
                {
                    "Bao gồm toàn bộ quyền lợi của Vé vào cổng tiêu chuẩn",
                    "Chơi KHÔNG GIỚI HẠN tất cả trò chơi cảm giác mạnh: Tàu lượn siêu tốc Cyclone, Đu quay khổng lồ Wonder Wheel, Xe điện đụng Bumper Cars, Tháp rơi tự do...",
                    "Vào cửa toàn bộ khu công viên nước Paradise: Hồ bơi tạo sóng, máng trượt tốc độ cao, sông lười",
                    "Trải nghiệm rạp chiếu phim 7D tương tác thực tế ảo và mê cung gương kỳ ảo"
                },
                QuaTangTienIch = new List<string>
                {
                    "Miễn phí 01 tủ gửi đồ locker thông minh trong vòng 4 giờ",
                    "Miễn phí áo phao bơi an toàn tại khu công viên nước"
                },
                DoiTuongDieuKien = "Áp dụng cho khách hàng có chiều cao từ 1m3 trở lên",
                LuuY = "Tuân thủ hướng dẫn an toàn, không khuyến khích người có tiền sử tim mạch tham gia trò chơi tốc độ cao"
            },

            ["VE003"] = new TicketDetailInfo
            {
                MaVe = "VE003",
                TenVe = "Vé trọn gói trẻ em (Cao < 1m3)",
                LoaiVe = "Vé trẻ em",
                GiaVe = 150000,
                ThoiHan = "Trong ngày",
                Highlights = "Trọn gói Kidzone liên hoàn, nhà bóng & cụm công viên nước trẻ em",
                BaoGom = new List<string>
                {
                    "Vào cổng tự do và chơi không giới hạn tại toàn bộ khu phức hợp Kidzone",
                    "Nhà bóng khổng lồ liên hoàn, cầu trượt xoắn ốc, sàn nhún trampoline, đường đua mini",
                    "Cụm công viên nước trẻ em an toàn: Máng trượt nước mini, xô nước khổng lồ đổ thác có cứu hộ giám sát",
                    "Tham gia các hoạt động sáng tạo nghệ thuật: Vẽ tranh cát, nặn tò he, đố vui nhận quà"
                },
                QuaTangTienIch = new List<string>
                {
                    "Tặng 01 bóng bay nghệ thuật tạo hình thú cưng ngộ nghĩnh",
                    "Miễn phí 01 áo phao bơi trẻ em chuyên dụng an toàn tuyệt đối"
                },
                DoiTuongDieuKien = "Áp dụng cho trẻ em có chiều cao dưới 1m3. Bắt buộc có người lớn đi kèm giám sát",
                LuuY = "Bé cần mang vớ/tất chống trượt khi tham gia vui chơi trong khu nhà bóng"
            },

            ["VE004"] = new TicketDetailInfo
            {
                MaVe = "VE004",
                TenVe = "Vé công viên nước Paradise",
                LoaiVe = "Vé ngày",
                GiaVe = 120000,
                ThoiHan = "Trong ngày",
                Highlights = "Trọn gói công viên nước Paradise: Hồ tạo sóng, máng trượt xoắn & sông lười",
                BaoGom = new List<string>
                {
                    "Vé vào cổng chuyên biệt phân khu Công viên nước Paradise",
                    "Thỏa thích tắm biển nhân tạo tại hồ bơi tạo sóng sóng thần cực đã",
                    "Trôi bồng bềnh thư giãn trên dòng sông lười dài 500m ngắm cảnh nhiệt đới",
                    "Chinh phục cụm máng trượt cảm giác mạnh: Lốc xoáy Tornado, Máng trượt siêu tốc 4 làn"
                },
                QuaTangTienIch = new List<string>
                {
                    "Miễn phí áo phao bơi mọi kích cỡ từ trẻ em đến người lớn",
                    "Miễn phí khu vực tắm tráng nước ngọt sạch sẽ và phòng thay đồ hiện đại"
                },
                DoiTuongDieuKien = "Phù hợp mọi lứa tuổi (trẻ em dưới 10 tuổi cần có người lớn đi kèm)",
                LuuY = "Bắt buộc mặc đồ bơi co giãn, không mang thức ăn đồ uống vào khu vực lòng hồ"
            },

            ["VE005"] = new TicketDetailInfo
            {
                MaVe = "VE005",
                TenVe = "Vé combo Gia đình (2 Lớn + 1 Bé)",
                LoaiVe = "Vé gia đình",
                GiaVe = 550000,
                ThoiHan = "Trong ngày",
                Highlights = "Combo tiết kiệm cho 3 người (2 Lớn + 1 Bé), trọn gói tất cả trò chơi khô & nước",
                BaoGom = new List<string>
                {
                    "Vé trọn gói vui chơi All-in-one cho 2 người lớn và 1 trẻ em (tiết kiệm 15% so với mua lẻ)",
                    "Chơi không giới hạn tất cả các trò chơi cảm giác mạnh, trò chơi gia đình, Kidzone và công viên nước",
                    "Cả gia đình cùng trải nghiệm vòng quay khổng lồ Wonder Wheel ngắm toàn cảnh khu vui chơi từ độ cao 50m"
                },
                QuaTangTienIch = new List<string>
                {
                    "TẶNG 01 Voucher giảm 10% tại khu ẩm thực Food Court",
                    "Miễn phí 01 tủ gửi đồ thông minh locker cỡ lớn cho cả gia đình"
                },
                DoiTuongDieuKien = "Áp dụng cho gia đình 3 thành viên (2 người lớn + 1 trẻ em dưới 1m3)",
                LuuY = "Các thành viên trong gia đình cần vào cổng cùng lúc để làm thủ tục soát vé"
            },

            ["VE006"] = new TicketDetailInfo
            {
                MaVe = "VE006",
                TenVe = "Vé combo Gia đình VIP (2 Lớn + 2 Bé)",
                LoaiVe = "Vé gia đình",
                GiaVe = 700000,
                ThoiHan = "Trong ngày",
                Highlights = "Gói VIP 4 người + Tặng 4 nước uống + 1 bắp rang phô mai + Lối đi ưu tiên",
                BaoGom = new List<string>
                {
                    "Vé trọn gói không giới hạn cho gia đình 4 người (2 người lớn + 2 trẻ em)",
                    "Tham gia không giới hạn 100% trò chơi trên cạn, trò chơi dưới nước và cụm Kidzone",
                    "Ưu tiên lối đi riêng giảm thiểu thời gian xếp hàng tại các trò chơi đông khách"
                },
                QuaTangTienIch = new List<string>
                {
                    "TẶNG NGAY 4 voucher nước ngọt lon hoặc nước suối tại quầy ẩm thực",
                    "TẶNG 01 phần bắp rang bơ phô mai cỡ lớn thơm lừng",
                    "Miễn phí 02 tủ gửi đồ locker thông minh cả ngày",
                    "Tặng 01 ảnh kỷ niệm gia đình kích thước 13x18 có khung ảnh lưu niệm"
                },
                DoiTuongDieuKien = "Áp dụng cho đoàn gia đình gồm 2 người lớn và 2 trẻ em",
                LuuY = "Đổi voucher quà tặng nước và bắp rang tại quầy dịch vụ trung tâm"
            },

            ["VE007"] = new TicketDetailInfo
            {
                MaVe = "VE007",
                TenVe = "Vé VIP FastPass ưu tiên",
                LoaiVe = "Vé VIP",
                GiaVe = 450000,
                ThoiHan = "Trong ngày",
                Highlights = "ĐẶC QUYỀN LỐI ĐI FASTPASS: Không cần xếp hàng + Phòng VIP + Đồ uống miễn phí",
                BaoGom = new List<string>
                {
                    "ĐẶC QUYỀN FASTPASS: Lối đi ưu tiên độc quyền, KHÔNG CẦN XẾP HÀNG tại 100% trò chơi",
                    "Chơi trọn gói không giới hạn toàn bộ trò chơi cảm giác mạnh, công viên nước và khu thực tế ảo",
                    "Sử dụng phòng chờ VIP Lounge sang trọng có máy lạnh, ghế massage và đồ ăn nhẹ tự phục vụ"
                },
                QuaTangTienIch = new List<string>
                {
                    "Miễn phí 01 phần nước uống tự chọn (Trà sữa trân châu hoặc Nước ép hoa quả tươi)",
                    "Miễn phí 01 tủ đồ VIP có tích hợp sẵn cổng sạc điện thoại an toàn cả ngày",
                    "Khăn lạnh cao cấp và áo phao bơi VIP chuyên biệt"
                },
                DoiTuongDieuKien = "Dành cho khách hàng muốn tiết kiệm tối đa thời gian và tận hưởng trải nghiệm dịch vụ cao cấp nhất",
                LuuY = "Vui lòng đeo vòng tay VIP FastPass trong suốt quá trình vui chơi để nhân viên nhận diện"
            },

            ["VE008"] = new TicketDetailInfo
            {
                MaVe = "VE008",
                TenVe = "Vé dạ hội vui chơi sau 17h00",
                LoaiVe = "Vé ngày",
                GiaVe = 120000,
                ThoiHan = "Buổi tối",
                Highlights = "Vui chơi từ 17h00: Trình diễn ánh sáng laser 3D mapping + Biểu diễn nhạc nước + Trò chơi đêm",
                BaoGom = new List<string>
                {
                    "Vào cổng từ 17h00 đến giờ đóng cửa (22h00) hòa mình vào không gian lung linh huyền ảo",
                    "Thưởng thức đại tiệc âm thanh ánh sáng, biểu diễn nhạc nước 3D mapping và múa lửa nghệ thuật",
                    "Chơi không giới hạn các trò chơi đêm: Đu quay Wonder Wheel ngắm toàn cảnh thành phố về đêm lung linh, Xe điện đụng Bumper Cars, Tàu lượn đêm..."
                },
                QuaTangTienIch = new List<string>
                {
                    "Tặng que phát sáng dạ quang check-in đêm cho khách tham quan",
                    "Miễn phí tham gia đêm hội âm nhạc DJ ngoài trời cuối tuần"
                },
                DoiTuongDieuKien = "Khung giờ mở bán và áp dụng: Từ 17h00 đến 22h00 mỗi ngày",
                LuuY = "Khu công viên nước ngừng đón khách sau 18h30 để đảm bảo an toàn ban đêm"
            },

            ["VE009"] = new TicketDetailInfo
            {
                MaVe = "VE009",
                TenVe = "Vé học sinh - sinh viên ưu đãi",
                LoaiVe = "Vé người lớn",
                GiaVe = 180000,
                ThoiHan = "Trong ngày",
                Highlights = "Ưu đãi HSSV: Trọn gói trò chơi khô + nước, giảm giá 28% so với vé người lớn",
                BaoGom = new List<string>
                {
                    "Quyền lợi trọn gói như Vé người lớn tiêu chuẩn với mức giá trợ giá đặc biệt dành riêng cho HSSV",
                    "Chinh phục toàn bộ các trò chơi cảm giác mạnh: Tàu lượn siêu tốc Cyclone, Đu quay khổng lồ, Xe điện đụng...",
                    "Thỏa sức vui chơi tại cụm công viên nước Paradise và hồ tạo sóng"
                },
                QuaTangTienIch = new List<string>
                {
                    "Giảm ngay 28% so với giá vé người lớn thông thường (tiết kiệm 70.000đ)",
                    "Tặng voucher giảm 10% khi thuê tủ locker hoặc mua combo đồ ăn vặt"
                },
                DoiTuongDieuKien = "Áp dụng cho học sinh, sinh viên các trường THCS, THPT, Trung cấp, Cao đẳng, Đại học",
                LuuY = "BẮT BUỘC xuất trình Thẻ học sinh/sinh viên còn hiệu lực (hoặc CCCD kèm giấy báo trúng tuyển) tại cửa soát vé"
            },

            ["VE010"] = new TicketDetailInfo
            {
                MaVe = "VE010",
                TenVe = "Vé thẻ tháng Member Platinum",
                LoaiVe = "Vé VIP",
                GiaVe = 1200000,
                ThoiHan = "30 ngày",
                Highlights = "Thẻ thành viên Platinum: Vào cổng KHÔNG GIỚI HẠN trong 30 ngày + Giảm 10% dịch vụ",
                BaoGom = new List<string>
                {
                    "Vào cổng và vui chơi không giới hạn TẤT CẢ các ngày trong tháng (bao gồm cả Thứ 7, Chủ Nhật và các ngày nghỉ Lễ)",
                    "Chơi trọn gói toàn bộ hệ thống trò chơi trên cạn, cảm giác mạnh và công viên nước không giới hạn lượt chơi",
                    "Lối đi check-in nhận diện khuôn mặt hoặc mã QR riêng biệt tại cổng, không cần xếp hàng mua vé"
                },
                QuaTangTienIch = new List<string>
                {
                    "Giảm trực tiếp 10% trên toàn bộ hóa đơn ẩm thực và dịch vụ thuê đồ trong khuôn viên",
                    "Tặng 02 vé mời người thân vào cổng miễn phí trong tháng sinh nhật của chủ thẻ",
                    "Tích điểm thành viên VIP và nhận quà tri ân cuối năm"
                },
                DoiTuongDieuKien = "Thẻ định danh chính chủ có chụp ảnh lưu trữ trong hệ thống",
                LuuY = "Thẻ có giá trị trong 30 ngày kể từ ngày kích hoạt, không áp dụng sang nhượng hoặc cho mượn thẻ"
            }
        };

        /// <summary>
        /// Lấy thông tin chi tiết đầy đủ của một loại vé
        /// </summary>
        public static TicketDetailInfo GetTicketDetail(Ve ve)
        {
            if (ve == null) return new TicketDetailInfo();

            if (!string.IsNullOrWhiteSpace(ve.MaVe) && _curatedTickets.TryGetValue(ve.MaVe.Trim(), out var curated))
            {
                // Cập nhật giá vé hoặc tên vé mới nhất từ DB nếu có thay đổi
                return new TicketDetailInfo
                {
                    MaVe = ve.MaVe,
                    TenVe = !string.IsNullOrWhiteSpace(ve.TenVe) ? ve.TenVe : curated.TenVe,
                    LoaiVe = !string.IsNullOrWhiteSpace(ve.LoaiVe) ? ve.LoaiVe : curated.LoaiVe,
                    GiaVe = ve.GiaVe > 0 ? ve.GiaVe : curated.GiaVe,
                    ThoiHan = !string.IsNullOrWhiteSpace(ve.ThoiHan) ? ve.ThoiHan : curated.ThoiHan,
                    TrangThai = ve.TrangThai ?? "Đang áp dụng",
                    Highlights = curated.Highlights,
                    BaoGom = new List<string>(curated.BaoGom),
                    QuaTangTienIch = new List<string>(curated.QuaTangTienIch),
                    DoiTuongDieuKien = curated.DoiTuongDieuKien,
                    LuuY = curated.LuuY
                };
            }

            // Đối với vé mới tạo trong CSDL chưa có trong danh mục định sẵn
            var autoInfo = new TicketDetailInfo
            {
                MaVe = ve.MaVe,
                TenVe = ve.TenVe,
                LoaiVe = ve.LoaiVe,
                GiaVe = ve.GiaVe,
                ThoiHan = ve.ThoiHan ?? "Trong ngày",
                TrangThai = ve.TrangThai ?? "Đang áp dụng",
                Highlights = !string.IsNullOrWhiteSpace(ve.MoTa) ? ve.MoTa : "Vé tham quan và trải nghiệm khu vui chơi giải trí",
                DoiTuongDieuKien = $"Áp dụng theo quy chuẩn loại vé '{ve.LoaiVe}'",
                LuuY = "Vui lòng giữ vé cẩn thận để xuất trình tại cửa kiểm soát trò chơi."
            };

            // Phân tích quyền lợi dựa trên loại vé
            if (ve.LoaiVe.Contains("VIP", StringComparison.OrdinalIgnoreCase))
            {
                autoInfo.BaoGom.Add("Chơi trọn gói không giới hạn tất cả trò chơi khu vui chơi và công viên nước");
                autoInfo.BaoGom.Add("Đặc quyền lối đi ưu tiên FastPass không cần xếp hàng chờ đợi");
                autoInfo.BaoGom.Add("Sử dụng khu vực nghỉ chân cao cấp VIP Lounge");
                autoInfo.QuaTangTienIch.Add("Miễn phí tủ gửi đồ thông minh và phần nước uống tự chọn");
            }
            else if (ve.LoaiVe.Contains("Trẻ em", StringComparison.OrdinalIgnoreCase))
            {
                autoInfo.BaoGom.Add("Vui chơi không giới hạn tại toàn bộ khu Kidzone và nhà bóng");
                autoInfo.BaoGom.Add("Tham gia các trò chơi vận động nhẹ nhàng và công viên nước thiếu nhi");
                autoInfo.QuaTangTienIch.Add("Miễn phí áo phao bơi trẻ em và quà tặng bong bóng nghệ thuật");
            }
            else if (ve.LoaiVe.Contains("Gia đình", StringComparison.OrdinalIgnoreCase))
            {
                autoInfo.BaoGom.Add("Gói vé trọn gói cho các thành viên trong gia đình");
                autoInfo.BaoGom.Add("Chơi tất cả trò chơi gia đình, trò chơi cảm giác mạnh và công viên nước");
                autoInfo.QuaTangTienIch.Add("Tặng voucher ẩm thực giảm giá và tủ gửi đồ miễn phí");
            }
            else
            {
                autoInfo.BaoGom.Add("Vào cổng tham quan và chơi các trò chơi theo quy định gói vé");
                if (!string.IsNullOrWhiteSpace(ve.MoTa))
                {
                    autoInfo.BaoGom.Add(ve.MoTa);
                }
                autoInfo.QuaTangTienIch.Add("Bản đồ hướng dẫn tham quan và dịch vụ hỗ trợ khách hàng");
            }

            return autoInfo;
        }

        public static string BuildTicketToolTipText(TicketDetailInfo info)
        {
            var lines = new List<string>
            {
                $"{info.TenVe} ({info.MaVe}) - {info.GiaVe:N0} đ",
                $"Nhóm: {info.LoaiVe}  |  Thời hạn: {info.ThoiHan}",
                new string('-', 42),
                "VÉ NÀY BAO GỒM:"
            };

            foreach (var bg in info.BaoGom.Take(3))
            {
                lines.Add($" • {bg}");
            }

            if (info.QuaTangTienIch.Any())
            {
                lines.Add("TIỆN ÍCH / QUÀ TẶNG KÈM THEO:");
                foreach (var qt in info.QuaTangTienIch.Take(2))
                {
                    lines.Add($" • {qt}");
                }
            }

            if (!string.IsNullOrWhiteSpace(info.DoiTuongDieuKien))
            {
                lines.Add($"Đối tượng: {info.DoiTuongDieuKien}");
            }

            lines.Add(new string('-', 42));
            lines.Add("Bấm nút [Chi tiết] trên thẻ để xem bảng quyền lợi đầy đủ!");

            return string.Join(Environment.NewLine, lines);
        }
    }
}
