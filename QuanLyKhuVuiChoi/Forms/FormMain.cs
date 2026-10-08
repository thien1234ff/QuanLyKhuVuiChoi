using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Forms.Billing;
using QuanLyKhuVuiChoi.Forms.Customer;
using QuanLyKhuVuiChoi.Forms.Dashboard;
using QuanLyKhuVuiChoi.Forms.Employee;
using QuanLyKhuVuiChoi.Forms.EmployeeTicketImportExport;
using QuanLyKhuVuiChoi.Forms.Game;
using QuanLyKhuVuiChoi.Forms.GameServiceImportExport;
using QuanLyKhuVuiChoi.Forms.Report;
using QuanLyKhuVuiChoi.Forms.Service;
using QuanLyKhuVuiChoi.Forms.Ticket;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms
{
    public class FormMain : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;

        // Containers
        private Panel pnlSidebar = null!;
        private Panel pnlTopBar = null!;
        private Panel pnlContent = null!;
        private Label lblClock = null!;
        private System.Windows.Forms.Timer systemTimer = null!;

        private Button? currentActiveBtn = null;
        private Form? currentChildForm = null;

        public FormMain(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;

            InitializeComponent();
            OpenChildForm(new FormDashboard(_context, _excelService));
        }

        private void InitializeComponent()
        {
            this.Text = "FUNZONE - PARK MANAGEMENT SYSTEM";
            this.Size = new Size(1440, 880);
            this.MinimumSize = new Size(1250, 780);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;

            // ==========================================
            // 1. SIDEBAR PANEL (LEFT)
            // ==========================================
            pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 240,
                BackColor = UITheme.DarkSidebar,
                Padding = new Padding(0)
            };

            // Top Brand Logo
            var pnlLogo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = UITheme.DarkSidebar,
                Padding = new Padding(20, 18, 20, 10)
            };
            var lblLogo = new Label
            {
                Text = "🎡 FUNZONE",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(18, 14),
                AutoSize = true
            };
            var lblSubLogo = new Label
            {
                Text = "PARK MANAGEMENT",
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(56, 189, 248), // Cyan
                Location = new Point(22, 45),
                AutoSize = true
            };
            pnlLogo.Controls.Add(lblLogo);
            pnlLogo.Controls.Add(lblSubLogo);

            // Bottom Profile Badge (Admin)
            var pnlProfile = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 74,
                BackColor = Color.FromArgb(15, 20, 36),
                Padding = new Padding(16, 12, 16, 12)
            };

            var pnlAvatar = new Panel
            {
                Size = new Size(40, 40),
                Location = new Point(16, 16),
                BackColor = Color.FromArgb(37, 99, 235)
            };
            pnlAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var brush = new SolidBrush(Color.White);
                using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                using var font = new Font("Segoe UI", 12F, FontStyle.Bold);
                e.Graphics.DrawString("A", font, brush, new RectangleF(0, 0, 40, 40), sf);
            };

            var lblAdminName = new Label
            {
                Text = AuthService.CurrentEmployee?.HoTen ?? "Admin",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(64, 16),
                AutoSize = true
            };

            var lblAdminRole = new Label
            {
                Text = AuthService.CurrentUser?.Quyen ?? "Quản trị viên",
                Font = UITheme.SmallFont,
                ForeColor = UITheme.SidebarText,
                Location = new Point(65, 38),
                AutoSize = true
            };

            var btnLogout = new Button
            {
                Text = "🚪",
                Size = new Size(30, 30),
                Location = new Point(195, 20),
                FlatStyle = FlatStyle.Flat,
                ForeColor = UITheme.SidebarText,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Bạn có muốn đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    AuthService.Logout();
                    this.Close();
                }
            };

            pnlProfile.Controls.Add(pnlAvatar);
            pnlProfile.Controls.Add(lblAdminName);
            pnlProfile.Controls.Add(lblAdminRole);
            pnlProfile.Controls.Add(btnLogout);

            // Middle Menu Flow Container
            var pnlMenu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(12, 10, 12, 10)
            };

            Button btnDash = CreateNavButton("📊  Dashboard", (s, e) => OpenChildForm(new FormDashboard(_context, _excelService)));
            pnlMenu.Controls.Add(btnDash);
            pnlMenu.Controls.Add(CreateNavButton("🎟️  Bán vé (POS)", (s, e) => OpenChildForm(new FormBanVe(_context, _excelService))));
            pnlMenu.Controls.Add(CreateNavButton("👤  Khách hàng", (s, e) => OpenChildForm(new FormKhachHang(_context, _excelService))));
            pnlMenu.Controls.Add(CreateNavButton("🎡  Khu vui chơi", (s, e) => OpenChildForm(new FormTroChoi(_context, _excelService))));
            pnlMenu.Controls.Add(CreateNavButton("🍔  Dịch vụ", (s, e) => OpenChildForm(new FormDichVu(_context, _excelService))));
            pnlMenu.Controls.Add(CreateNavButton("👥  Nhân viên", (s, e) => OpenChildForm(new FormNhanVien(_context, _excelService))));
            pnlMenu.Controls.Add(CreateNavButton("🎫  Loại vé", (s, e) => OpenChildForm(new FormVe(_context, _excelService))));
            pnlMenu.Controls.Add(CreateNavButton("💳  Thanh toán", (s, e) => OpenChildForm(new FormHoaDon(_context, _excelService))));
            pnlMenu.Controls.Add(CreateNavButton("📈  Báo cáo", (s, e) => OpenChildForm(new FormBaoCaoDoanhThu(_context, _excelService))));
            pnlMenu.Controls.Add(CreateNavButton("⚙️  Cài đặt / Excel", (s, e) => OpenChildForm(new FormExportKhachHang(_context, _excelService))));

            pnlSidebar.Controls.Add(pnlMenu);
            pnlSidebar.Controls.Add(pnlProfile);
            pnlSidebar.Controls.Add(pnlLogo);

            // ==========================================
            // 2. TOPBAR PANEL (TOP)
            // ==========================================
            pnlTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 78,
                BackColor = Color.White,
                Padding = new Padding(28, 14, 28, 12)
            };
            pnlTopBar.Paint += (s, e) =>
            {
                using var pen = new Pen(UITheme.CardBorder, 1);
                e.Graphics.DrawLine(pen, 0, pnlTopBar.Height - 1, pnlTopBar.Width, pnlTopBar.Height - 1);
            };

            // Left Welcome Message
            var lblWelcomeTitle = new Label
            {
                Text = $"Chào mừng trở lại, {AuthService.CurrentEmployee?.HoTen ?? "Admin"}! 👋",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = UITheme.TextMain,
                Location = new Point(24, 12),
                AutoSize = true
            };

            var lblWelcomeSub = new Label
            {
                Text = "Cùng xem hoạt động của khu vui chơi hôm nay nhé!",
                Font = UITheme.SmallFont,
                ForeColor = UITheme.TextMuted,
                Location = new Point(26, 42),
                AutoSize = true
            };

            // Right Group: Search Box + Bell + Clock
            var pnlSearch = new Panel
            {
                Size = new Size(180, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(720, 20),
                BackColor = Color.FromArgb(248, 250, 252)
            };
            pnlSearch.Paint += (s, e) =>
            {
                using var pen = new Pen(UITheme.BorderColor, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlSearch.Width - 1, pnlSearch.Height - 1);
            };
            var txtSearch = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(248, 250, 252),
                Font = UITheme.SmallFont,
                ForeColor = UITheme.TextMuted,
                Text = "🔍 Tìm kiếm...",
                Location = new Point(10, 9),
                Width = 160
            };
            pnlSearch.Controls.Add(txtSearch);

            // Bell Notification Icon Badge
            var pnlBell = new Panel
            {
                Size = new Size(38, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(910, 20),
                BackColor = Color.FromArgb(248, 250, 252),
                Cursor = Cursors.Hand
            };
            pnlBell.Paint += (s, e) =>
            {
                using var pen = new Pen(UITheme.BorderColor, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBell.Width - 1, pnlBell.Height - 1);

                // Bell icon
                using var font = new Font("Segoe UI Emoji", 11F);
                e.Graphics.DrawString("🔔", font, Brushes.Gray, 6, 7);

                // Red badge count
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(Brushes.Crimson, 20, 4, 14, 14);
                using var fontBadge = new Font("Segoe UI", 6.5F, FontStyle.Bold);
                using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString("3", fontBadge, Brushes.White, new RectangleF(20, 4, 14, 14), sf);
            };

            // Date & Clock Badge Card
            var pnlClockCard = new Panel
            {
                Size = new Size(240, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(960, 20),
                BackColor = Color.White
            };
            pnlClockCard.Paint += (s, e) =>
            {
                using var pen = new Pen(UITheme.BorderColor, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlClockCard.Width - 1, pnlClockCard.Height - 1);
            };

            lblClock = new Label
            {
                Text = GetCurrentFormattedDate(),
                Font = UITheme.SmallBoldFont,
                ForeColor = UITheme.TextMain,
                Location = new Point(8, 8),
                AutoSize = true
            };
            pnlClockCard.Controls.Add(lblClock);

            pnlTopBar.Controls.Add(lblWelcomeTitle);
            pnlTopBar.Controls.Add(lblWelcomeSub);
            pnlTopBar.Controls.Add(pnlSearch);
            pnlTopBar.Controls.Add(pnlBell);
            pnlTopBar.Controls.Add(pnlClockCard);

            // Timer for Clock
            systemTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            systemTimer.Tick += (s, e) => lblClock.Text = GetCurrentFormattedDate();
            systemTimer.Start();

            // ==========================================
            // 3. MAIN CONTENT CONTAINER
            // ==========================================
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UITheme.Background
            };

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlTopBar);
            this.Controls.Add(pnlSidebar);

            // Select Dashboard button by default
            SetActiveNavButton(btnDash);
        }

        private string GetCurrentFormattedDate()
        {
            var now = DateTime.Now;
            string dayOfWeek = now.DayOfWeek switch
            {
                DayOfWeek.Monday => "Thứ Hai",
                DayOfWeek.Tuesday => "Thứ Ba",
                DayOfWeek.Wednesday => "Thứ Tư",
                DayOfWeek.Thursday => "Thứ Năm",
                DayOfWeek.Friday => "Thứ Sáu",
                DayOfWeek.Saturday => "Thứ Bảy",
                DayOfWeek.Sunday => "Chủ Nhật",
                _ => ""
            };
            return $"📅 {dayOfWeek}, {now:dd/MM/yyyy HH:mm:ss}";
        }

        private Button CreateNavButton(string text, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(216, 42),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = UITheme.SidebarText,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 2, 0, 2)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = UITheme.SidebarHover;

            btn.Click += (s, e) =>
            {
                SetActiveNavButton(btn);
                onClick(s, e);
            };

            return btn;
        }

        private void SetActiveNavButton(Button btn)
        {
            if (currentActiveBtn != null)
            {
                currentActiveBtn.BackColor = Color.Transparent;
                currentActiveBtn.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                currentActiveBtn.ForeColor = UITheme.SidebarText;
            }

            btn.BackColor = UITheme.Primary;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            currentActiveBtn = btn;
        }

        public void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
