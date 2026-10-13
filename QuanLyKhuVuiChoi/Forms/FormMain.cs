using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Forms.Auth;
using QuanLyKhuVuiChoi.Forms.Billing;
using QuanLyKhuVuiChoi.Forms.Customer;
using QuanLyKhuVuiChoi.Forms.Dashboard;
using QuanLyKhuVuiChoi.Forms.Employee;
using QuanLyKhuVuiChoi.Forms.Game;
using QuanLyKhuVuiChoi.Forms.Report;
using QuanLyKhuVuiChoi.Forms.Service;
using QuanLyKhuVuiChoi.Forms.Ticket;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms
{
    public partial class FormMain : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;
        private System.Windows.Forms.Timer? systemTimer;
        private Form? _currentChildForm = null;
        private Button? _currentActiveTab = null;

        public FormMain() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormMain(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _excelService = excelService ?? new ExcelService();

            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            InitSystem();
        }

        private void InitSystem()
        {
            UpdateUserStatus();

            lblClock.Text = GetCurrentFormattedDate();
            systemTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            systemTimer.Tick += (s, e) => lblClock.Text = GetCurrentFormattedDate();
            systemTimer.Start();

            // Mặc định mở ngay Dashboard tổng quan trong panel nội dung
            OpenChildTab(new FormDashboard(_context, _excelService), btnTabDashboard);
        }

        public void UpdateUserStatus()
        {
            if (AuthService.CurrentUser != null)
            {
                string userName = AuthService.CurrentEmployee?.HoTen ?? AuthService.CurrentUser.TenDangNhap;
                string role = AuthService.CurrentUser.Quyen;
                string roleLabel = string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase) ? "Quản trị viên" : "Nhân viên";
                lblUserGreeting.Text = $"Chào: {userName} ({roleLabel})";
                lblUserGreeting.Visible = true;
                btnLogout.Visible = true;
                btnHeaderDangNhap.Visible = false;
                btnHeaderDangKy.Visible = false;

                // Tab Quản lý nhân viên chỉ hiển thị cho Admin
                btnTabNhanVien.Visible = string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase);
                lblMenuSection2.Visible = true;
                pnlDivider.Visible = true;
                btnTabVe.Visible = true;
            }
            else
            {
                lblUserGreeting.Text = "Khách";
                lblUserGreeting.Visible = false;
                btnLogout.Visible = false;
                btnHeaderDangNhap.Visible = true;
                btnHeaderDangKy.Visible = true;

                btnTabNhanVien.Visible = false;
                btnTabVe.Visible = false;
                lblMenuSection2.Visible = false;
                pnlDivider.Visible = false;
            }
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
            return $"{dayOfWeek}, {now:dd/MM/yyyy HH:mm:ss}";
        }

        // =========================================================
        // SIDEBAR TAB NAVIGATION (MỞ FORM CON VÀO PANEL NỘI DUNG)
        // =========================================================

        private void OpenChildTab(Form childForm, Button activeTabButton)
        {
            try
            {
                // Đóng form con trước đó nếu có
                if (_currentChildForm != null)
                {
                    _currentChildForm.Close();
                    _currentChildForm.Dispose();
                    _currentChildForm = null;
                }

                _currentChildForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                pnlContent.Controls.Clear();
                pnlContent.Controls.Add(childForm);
                pnlContent.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();

                HighlightTab(activeTabButton);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị màn hình chức năng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HighlightTab(Button activeTabButton)
        {
            var tabList = new[]
            {
                btnTabDashboard,
                btnTabBanVe,
                btnTabHoaDon,
                btnTabTroChoi,
                btnTabDichVu,
                btnTabKhachHang,
                btnTabVe,
                btnTabNhanVien
            };

            foreach (var tab in tabList)
            {
                if (tab == activeTabButton)
                {
                    tab.BackColor = Color.FromArgb(59, 130, 246); // Xanh dương active
                    tab.ForeColor = Color.White;
                }
                else
                {
                    tab.BackColor = Color.Transparent;
                    tab.ForeColor = Color.FromArgb(241, 245, 249); // Trắng / Bạc sáng rõ nét
                }
            }

            _currentActiveTab = activeTabButton;
        }

        private void BtnTabDashboard_Click(object? sender, EventArgs e)
        {
            OpenChildTab(new FormDashboard(_context, _excelService), btnTabDashboard);
        }

        private void BtnTabBanVe_Click(object? sender, EventArgs e)
        {
            OpenChildTab(new FormBanVe(_context, _excelService), btnTabBanVe);
        }

        private void BtnTabHoaDon_Click(object? sender, EventArgs e)
        {
            OpenChildTab(new FormHoaDon(_context, _excelService), btnTabHoaDon);
        }

        private void BtnTabTroChoi_Click(object? sender, EventArgs e)
        {
            OpenChildTab(new FormTroChoi(_context, _excelService), btnTabTroChoi);
        }

        private void BtnTabDichVu_Click(object? sender, EventArgs e)
        {
            OpenChildTab(new FormDichVu(_context, _excelService), btnTabDichVu);
        }

        private void BtnTabKhachHang_Click(object? sender, EventArgs e)
        {
            OpenChildTab(new FormKhachHang(_context, _excelService), btnTabKhachHang);
        }

        private void BtnTabVe_Click(object? sender, EventArgs e)
        {
            OpenChildTab(new FormVe(_context, _excelService), btnTabVe);
        }

        private void BtnTabNhanVien_Click(object? sender, EventArgs e)
        {
            OpenChildTab(new FormNhanVien(_context, _excelService), btnTabNhanVien);
        }

        // =========================================================
        // HEADER AUTH BUTTONS (ĐĂNG NHẬP, ĐĂNG KÝ, ĐĂNG XUẤT)
        // =========================================================

        private void BtnHeaderDangNhap_Click(object? sender, EventArgs e)
        {
            using var loginForm = new FormDangNhap(_context);
            if (loginForm.ShowDialog(this) == DialogResult.OK)
            {
                UpdateUserStatus();
                OpenChildTab(new FormDashboard(_context, _excelService), btnTabDashboard);
            }
        }

        private void BtnHeaderDangKy_Click(object? sender, EventArgs e)
        {
            using var regForm = new FormDangKy(_context);
            regForm.ShowDialog(this);
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?", "Xác Nhận Đăng Xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                AuthService.Logout();
                UpdateUserStatus();
                OpenChildTab(new FormDashboard(_context, _excelService), btnTabDashboard);
            }
        }

        private void MenuQuenMatKhau_Click(object? sender, EventArgs e)
        {
            using var f = new FormQuenMatKhau(_context);
            f.ShowDialog(this);
        }

        private void MenuThoat_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuAbout_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ WONDERLAND\n\n" +
                "• Phiên bản: 2.0 Modern (Sidebar Tabs)\n" +
                "• 6 Chức năng cốt lõi được bố trí dạng Tab gọn gàng, tiện dụng\n" +
                "• Tương thích 100% C# .NET 8 WinForms và SQL Server\n" +
                "• Xử lý xuất/nhập Excel: ClosedXML\n" +
                "• ORM: Entity Framework Core 8\n\n" +
                "Bản quyền © 2026 Wonderland Amusement Park.",
                "Giới Thiệu Hệ Thống",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
