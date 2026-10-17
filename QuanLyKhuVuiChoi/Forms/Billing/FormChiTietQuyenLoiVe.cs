using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.DTOs;

namespace QuanLyKhuVuiChoi.Forms.Billing
{
    public class FormChiTietQuyenLoiVe : Form
    {
        private readonly TicketDetailInfo _info;
        public bool RequestedAddToCart { get; private set; } = false;

        public FormChiTietQuyenLoiVe(TicketDetailInfo info)
        {
            _info = info ?? throw new ArgumentNullException(nameof(info));
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = $"Chi Tiết Vé & Quyền Lợi - {_info.TenVe}";
            Size = new Size(720, 680);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(248, 250, 252);
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            // 1. TOP HEADER PANEL
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 115,
                BackColor = Color.White,
                Padding = new Padding(24, 16, 24, 14)
            };
            pnlTop.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawLine(pen, 0, pnlTop.Height - 1, pnlTop.Width, pnlTop.Height - 1);
            };

            var lblSubtitle = new Label
            {
                Text = "QUẦY POS / THÔNG TIN CHI TIẾT GÓI VÉ",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                Location = new Point(24, 12),
                AutoSize = true
            };

            var lblName = new Label
            {
                Text = _info.TenVe,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(22, 32),
                Size = new Size(440, 30),
                AutoEllipsis = true
            };

            // Badges row: MaVe, LoaiVe, ThoiHan
            var pnlBadges = new FlowLayoutPanel
            {
                Location = new Point(24, 68),
                Size = new Size(440, 32),
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent
            };

            pnlBadges.Controls.Add(CreateBadge($"Mã: {_info.MaVe}", Color.FromArgb(241, 245, 249), Color.FromArgb(71, 85, 105)));
            pnlBadges.Controls.Add(CreateBadge(_info.LoaiVe, Color.FromArgb(238, 242, 255), Color.FromArgb(79, 70, 229)));
            pnlBadges.Controls.Add(CreateBadge($"Thời hạn: {_info.ThoiHan}", Color.FromArgb(254, 243, 199), Color.FromArgb(180, 83, 9)));

            // Price section on right
            var lblPriceLabel = new Label
            {
                Text = "GIÁ VÉ NIÊM YẾT",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(480, 24),
                Size = new Size(200, 16),
                TextAlign = ContentAlignment.TopRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            var lblPrice = new Label
            {
                Text = $"{_info.GiaVe:N0} đ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                Location = new Point(480, 42),
                Size = new Size(200, 35),
                TextAlign = ContentAlignment.TopRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            pnlTop.Controls.Add(lblSubtitle);
            pnlTop.Controls.Add(lblName);
            pnlTop.Controls.Add(pnlBadges);
            pnlTop.Controls.Add(lblPriceLabel);
            pnlTop.Controls.Add(lblPrice);

            // 2. BOTTOM ACTION PANEL
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.White,
                Padding = new Padding(20, 12, 20, 12)
            };
            pnlBottom.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawLine(pen, 0, 0, pnlBottom.Width, 0);
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(95, 38),
                Location = new Point(375, 13),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(71, 85, 105),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Close();

            var btnAdd = new Button
            {
                Text = "Thêm Vào Giỏ Hàng",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(200, 38),
                Location = new Point(485, 13),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += (s, e) =>
            {
                RequestedAddToCart = true;
                DialogResult = DialogResult.OK;
                Close();
            };

            var lblTip = new Label
            {
                Text = "Nhân viên quầy có thể tư vấn trực tiếp quyền lợi này cho khách hàng",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(20, 22),
                AutoSize = true,
                MaximumSize = new Size(340, 40)
            };

            pnlBottom.Controls.Add(lblTip);
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Controls.Add(btnAdd);

            // 3. MAIN BODY SCROLL PANEL
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(24, 16, 24, 16),
                BackColor = Color.FromArgb(248, 250, 252)
            };

            int currentY = 16;
            const int sectionWidth = 650;

            // SECTION 1: Các trò chơi & khu vực bao gồm trong vé
            var pnlSection1 = CreateContentSection(
                "CÁC TRÒ CHƠI & KHU VỰC VUI CHƠI ĐƯỢC THAM GIA",
                _info.BaoGom,
                "•",
                Color.FromArgb(16, 185, 129),
                Color.FromArgb(236, 253, 245),
                sectionWidth
            );
            pnlSection1.Location = new Point(24, currentY);
            pnlBody.Controls.Add(pnlSection1);
            currentY += pnlSection1.Height + 14;

            // SECTION 2: Tiện ích & Quà tặng kèm theo
            if (_info.QuaTangTienIch != null && _info.QuaTangTienIch.Count > 0)
            {
                var pnlSection2 = CreateContentSection(
                    "TIỆN ÍCH & QUÀ TẶNG KÈM THEO VÉ",
                    _info.QuaTangTienIch,
                    "•",
                    Color.FromArgb(245, 158, 11),
                    Color.FromArgb(255, 251, 235),
                    sectionWidth
                );
                pnlSection2.Location = new Point(24, currentY);
                pnlBody.Controls.Add(pnlSection2);
                currentY += pnlSection2.Height + 14;
            }

            // SECTION 3: Đối tượng & Điều kiện áp dụng
            if (!string.IsNullOrWhiteSpace(_info.DoiTuongDieuKien))
            {
                var pnlSection3 = CreateInfoBox(
                    "ĐỐI TƯỢNG & ĐIỀU KIỆN ÁP DỤNG",
                    _info.DoiTuongDieuKien,
                    Color.FromArgb(59, 102, 245),
                    Color.FromArgb(239, 246, 255),
                    sectionWidth
                );
                pnlSection3.Location = new Point(24, currentY);
                pnlBody.Controls.Add(pnlSection3);
                currentY += pnlSection3.Height + 14;
            }

            // SECTION 4: Lưu ý & Quy định sử dụng
            if (!string.IsNullOrWhiteSpace(_info.LuuY))
            {
                var pnlSection4 = CreateInfoBox(
                    "LƯU Ý QUAN TRỌNG KHI SỬ DỤNG VÉ",
                    _info.LuuY,
                    Color.FromArgb(239, 68, 68),
                    Color.FromArgb(254, 242, 242),
                    sectionWidth
                );
                pnlSection4.Location = new Point(24, currentY);
                pnlBody.Controls.Add(pnlSection4);
                currentY += pnlSection4.Height + 20;
            }

            Controls.Add(pnlBody);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
        }

        private Label CreateBadge(string text, Color bg, Color textCol)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = bg,
                ForeColor = textCol,
                Padding = new Padding(8, 4, 8, 4),
                AutoSize = true,
                Margin = new Padding(0, 0, 8, 0)
            };
            return lbl;
        }

        private Panel CreateContentSection(string headerText, List<string> items, string bulletIcon, Color iconColor, Color boxBg, int width)
        {
            var pnl = new Panel
            {
                BackColor = Color.White,
                Padding = new Padding(16, 14, 16, 14),
                Width = width,
                AutoSize = false
            };
            pnl.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            var lblHeader = new Label
            {
                Text = headerText,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(14, 12),
                Size = new Size(width - 28, 22)
            };
            pnl.Controls.Add(lblHeader);

            int itemY = 40;
            foreach (var item in items)
            {
                var lblIcon = new Label
                {
                    Text = bulletIcon,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = iconColor,
                    Location = new Point(16, itemY + 1),
                    Size = new Size(16, 20),
                    TextAlign = ContentAlignment.TopCenter
                };

                var lblText = new Label
                {
                    Text = item,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(51, 65, 85),
                    Location = new Point(36, itemY),
                    AutoSize = true,
                    MaximumSize = new Size(width - 52, 0)
                };

                pnl.Controls.Add(lblIcon);
                pnl.Controls.Add(lblText);
                itemY += Math.Max(lblText.Height, 20) + 8;
            }

            pnl.Height = itemY + 12;
            return pnl;
        }

        private Panel CreateInfoBox(string headerText, string contentText, Color accentColor, Color bg, int width)
        {
            var pnl = new Panel
            {
                BackColor = bg,
                Padding = new Padding(16, 12, 16, 12),
                Width = width,
                AutoSize = false
            };
            pnl.Paint += (s, e) =>
            {
                using var borderPen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(borderPen, 0, 0, pnl.Width - 1, pnl.Height - 1);

                using var accentBrush = new SolidBrush(accentColor);
                e.Graphics.FillRectangle(accentBrush, 0, 0, 4, pnl.Height);
            };

            var lblHeader = new Label
            {
                Text = headerText,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(14, 10),
                Size = new Size(width - 28, 20)
            };

            var lblText = new Label
            {
                Text = contentText,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(14, 32),
                AutoSize = true,
                MaximumSize = new Size(width - 28, 0)
            };

            pnl.Controls.Add(lblHeader);
            pnl.Controls.Add(lblText);
            pnl.Height = Math.Max(lblText.Location.Y + lblText.Height + 12, 68);
            return pnl;
        }
    }
}
