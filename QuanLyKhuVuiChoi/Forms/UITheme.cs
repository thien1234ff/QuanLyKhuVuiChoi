using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhuVuiChoi.Forms
{
    public static class UITheme
    {
        // Modern Color Palette from Reference Image (FUNZONE)
        public static readonly Color Primary = Color.FromArgb(59, 102, 245);       // #3B66F5 Royal Blue
        public static readonly Color PrimaryLight = Color.FromArgb(239, 246, 255);  // #EFF6FF
        public static readonly Color PrimaryDark = Color.FromArgb(37, 76, 206);
        public static readonly Color Secondary = Color.FromArgb(168, 85, 247);     // #A855F7 Purple
        public static readonly Color SecondaryLight = Color.FromArgb(250, 245, 255);
        public static readonly Color Success = Color.FromArgb(16, 185, 129);       // #10B981 Green
        public static readonly Color SuccessLight = Color.FromArgb(236, 253, 245);
        public static readonly Color SuccessText = Color.FromArgb(21, 128, 61);
        public static readonly Color Warning = Color.FromArgb(245, 158, 11);       // #F59E0B Amber
        public static readonly Color WarningLight = Color.FromArgb(255, 251, 235);
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);         // #EF4444 Red
        public static readonly Color DangerLight = Color.FromArgb(254, 242, 242);
        
        public static readonly Color DarkSidebar = Color.FromArgb(19, 25, 43);     // #13192B Sleek Dark Navy
        public static readonly Color SidebarHover = Color.FromArgb(32, 42, 68);
        public static readonly Color SidebarText = Color.FromArgb(142, 156, 174);
        
        public static readonly Color Background = Color.FromArgb(244, 246, 251);   // #F4F6FB Clean Soft Blue-Gray
        public static readonly Color CardBg = Color.White;
        public static readonly Color CardBorder = Color.FromArgb(234, 239, 245);   // Soft border
        public static readonly Color TextMain = Color.FromArgb(15, 23, 42);        // #0F172A Slate 900
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);    // #64748B Slate 500
        public static readonly Color BorderColor = Color.FromArgb(226, 232, 240);

        // Fonts
        public static readonly Font HeaderFont = new Font("Segoe UI", 16F, FontStyle.Bold);
        public static readonly Font SubHeaderFont = new Font("Segoe UI", 11.5F, FontStyle.Bold);
        public static readonly Font TitleFont = new Font("Segoe UI", 13F, FontStyle.Bold);
        public static readonly Font MainFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        public static readonly Font BoldFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        public static readonly Font SmallFont = new Font("Segoe UI", 8.5F, FontStyle.Regular);
        public static readonly Font SmallBoldFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        public static readonly Font LargeStatFont = new Font("Segoe UI", 20F, FontStyle.Bold);

        public static void StyleCardPanel(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.Padding = new Padding(20);
            panel.Paint += (s, e) =>
            {
                using var pen = new Pen(CardBorder, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            };
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = Color.FromArgb(241, 245, 249);
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Font = MainFont;

            // Header Style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 10, 12, 10);
            dgv.ColumnHeadersHeight = 44;

            // Rows Style
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = TextMain;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);
            dgv.DefaultCellStyle.Padding = new Padding(12, 6, 12, 6);
            dgv.RowTemplate.Height = 40;

            // Alternating Row
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(253, 254, 255);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);

            // Enable double buffering for smooth scrolling
            var type = dgv.GetType();
            var pi = type.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            pi?.SetValue(dgv, true, null);
        }

        public static void StyleButton(Button btn, Color bg, Color text, int? height = null)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = text;
            btn.Font = SmallBoldFont;
            btn.Cursor = Cursors.Hand;
            if (height.HasValue)
            {
                btn.Height = height.Value;
            }
            else if (btn.Height > 32 || btn.Height == 0)
            {
                btn.Height = 32;
            }
        }

        public static void StyleSecondaryButton(Button btn, int height = 30)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btn.BackColor = Color.FromArgb(248, 250, 252);
            btn.ForeColor = Color.FromArgb(71, 85, 105);
            btn.Font = SmallBoldFont;
            btn.Cursor = Cursors.Hand;
            btn.Height = height;
        }

        public static void StyleDangerButton(Button btn, int height = 30)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(254, 242, 242);
            btn.ForeColor = Danger;
            btn.Font = SmallBoldFont;
            btn.Cursor = Cursors.Hand;
            btn.Height = height;
        }
    }
}
