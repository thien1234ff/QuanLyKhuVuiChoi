using System;
using System.ComponentModel;
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

        public FormMain() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormMain(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;

            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            InitSystem();
        }

        private void InitSystem()
        {
            string userName = AuthService.CurrentEmployee?.HoTen ?? "Quản trị viên";
            string role = AuthService.CurrentUser?.Quyen ?? "Admin";
            lblUserGreeting.Text = $"👤 Chào: {userName} ({role})";

            lblClock.Text = GetCurrentFormattedDate();
            systemTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            systemTimer.Tick += (s, e) => lblClock.Text = GetCurrentFormattedDate();
            systemTimer.Start();
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

        // =========================================================
        // 12 INDEPENDENT FUNCTION NAVIGATION HANDLERS
        // =========================================================

        private void BtnCardDashboard_Click(object? sender, EventArgs e)
        {
            var f = new FormDashboard(_context, _excelService);
            f.Show();
        }

        private void BtnCardBanVe_Click(object? sender, EventArgs e)
        {
            var f = new FormBanVe(_context, _excelService);
            f.Show();
        }

        private void BtnCardHoaDon_Click(object? sender, EventArgs e)
        {
            var f = new FormHoaDon(_context, _excelService);
            f.Show();
        }

        private void BtnCardBaoCao_Click(object? sender, EventArgs e)
        {
            var f = new FormBaoCaoDoanhThu(_context, _excelService);
            f.Show();
        }

        private void BtnCardTroChoi_Click(object? sender, EventArgs e)
        {
            var f = new FormTroChoi(_context, _excelService);
            f.Show();
        }

        private void BtnCardDichVu_Click(object? sender, EventArgs e)
        {
            var f = new FormDichVu(_context, _excelService);
            f.Show();
        }

        private void BtnCardVe_Click(object? sender, EventArgs e)
        {
            var f = new FormVe(_context, _excelService);
            f.Show();
        }

        private void BtnCardKhachHang_Click(object? sender, EventArgs e)
        {
            var f = new FormKhachHang(_context, _excelService);
            f.Show();
        }

        private void BtnCardNhanVien_Click(object? sender, EventArgs e)
        {
            var f = new FormNhanVien(_context, _excelService);
            f.Show();
        }

        private void BtnCardDangNhap_Click(object? sender, EventArgs e)
        {
            var f = new FormDangNhap(_context);
            f.ShowDialog(this);
        }

        private void BtnCardDangKy_Click(object? sender, EventArgs e)
        {
            var f = new FormDangKy(_context);
            f.ShowDialog(this);
        }

        private void BtnCardQuenMatKhau_Click(object? sender, EventArgs e)
        {
            var f = new FormQuenMatKhau(_context);
            f.ShowDialog(this);
        }

        private void BtnCardImportKH_Click(object? sender, EventArgs e)
        {
            var f = new FormImportKhachHang(_context, _excelService);
            f.ShowDialog(this);
        }

        private void BtnCardExportKH_Click(object? sender, EventArgs e)
        {
            var f = new FormExportKhachHang(_context, _excelService);
            f.ShowDialog(this);
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?", "Xác Nhận Đăng Xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                AuthService.Logout();
                this.Close();
            }
        }

        private void MenuThoat_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuAbout_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ FUNZONE PARK\n\n" +
                "• Phiên bản: 2.0.0 (WinForms .NET 8 C# & SQL Server)\n" +
                "• Kiến trúc: 12 Chức năng độc lập với Form riêng biệt\n" +
                "• Tương thích 100% Visual Studio 2022 View Designer kéo thả\n" +
                "• Thư viện xử lý Excel: ClosedXML\n" +
                "• ORM: Entity Framework Core 8\n\n" +
                "Bản quyền © 2026 FunZone Management System.",
                "Giới Thiệu Hệ Thống",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
