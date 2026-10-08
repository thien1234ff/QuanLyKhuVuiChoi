using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Auth
{
    public class FormDangNhap : Form
    {
        private readonly AuthService _authService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        private TextBox txtTenDangNhap = null!;
        private TextBox txtMatKhau = null!;
        private CheckBox chkHienMatKhau = null!;
        private Button btnDangNhap = null!;
        private Button btnThoat = null!;

        public FormDangNhap(QuanLyKhuVuiChoiDbContext context)
        {
            _context = context;
            _authService = new AuthService(_context);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "ĐĂNG NHẬP HỆ THỐNG - QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ";
            this.Size = new Size(540, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;

            // Top Header Panel
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = UITheme.DarkSidebar,
                Padding = new Padding(25, 20, 25, 10)
            };

            var lblLogo = new Label
            {
                Text = "🎡 WONDERLAND PARK",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(25, 18),
                AutoSize = true
            };

            var lblSubtitle = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ TOÀN DIỆN",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(27, 52),
                AutoSize = true
            };

            pnlTop.Controls.Add(lblLogo);
            pnlTop.Controls.Add(lblSubtitle);

            // Card Panel in Center
            var pnlCard = new Panel
            {
                Location = new Point(35, 125),
                Size = new Size(455, 300),
                BackColor = Color.White,
                Padding = new Padding(25)
            };

            var lblFormTitle = new Label
            {
                Text = "ĐĂNG NHẬP TÀI KHOẢN",
                Font = UITheme.SubHeaderFont,
                ForeColor = UITheme.Primary,
                Location = new Point(25, 15),
                AutoSize = true
            };

            var lblUser = new Label { Text = "Tên đăng nhập:", Location = new Point(25, 52), AutoSize = true, Font = UITheme.BoldFont };
            txtTenDangNhap = new TextBox { Location = new Point(25, 75), Size = new Size(405, 28), Font = new Font("Segoe UI", 11F), Text = "admin" };

            var lblPass = new Label { Text = "Mật khẩu:", Location = new Point(25, 115), AutoSize = true, Font = UITheme.BoldFont };
            txtMatKhau = new TextBox { Location = new Point(25, 138), Size = new Size(405, 28), Font = new Font("Segoe UI", 11F), PasswordChar = '●', Text = "123456" };

            chkHienMatKhau = new CheckBox { Text = "Hiển thị mật khẩu", Location = new Point(25, 172), AutoSize = true };
            chkHienMatKhau.CheckedChanged += (s, e) => txtMatKhau.PasswordChar = chkHienMatKhau.Checked ? '\0' : '●';

            btnDangNhap = new Button { Text = "ĐĂNG NHẬP", Location = new Point(25, 205), Size = new Size(250, 40), Font = UITheme.BoldFont };
            UITheme.StyleButton(btnDangNhap, UITheme.Primary, Color.White);
            btnDangNhap.Click += BtnDangNhap_Click;

            btnThoat = new Button { Text = "Thoát", Location = new Point(285, 205), Size = new Size(145, 40), Font = UITheme.BoldFont };
            UITheme.StyleButton(btnThoat, Color.FromArgb(148, 163, 184), Color.White);
            btnThoat.Click += (s, e) => Application.Exit();

            // Quick Fill Links
            var lblQuick = new Label { Text = "Đăng nhập nhanh:", Location = new Point(25, 258), AutoSize = true, Font = new Font("Segoe UI", 8.5F), ForeColor = UITheme.TextMuted };
            var lnkAdmin = new LinkLabel { Text = "Admin", Location = new Point(135, 258), AutoSize = true, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold) };
            lnkAdmin.LinkClicked += (s, e) => { txtTenDangNhap.Text = "admin"; txtMatKhau.Text = "123456"; };

            var lnkCashier = new LinkLabel { Text = "Thu ngân (thungan1)", Location = new Point(185, 258), AutoSize = true, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold) };
            lnkCashier.LinkClicked += (s, e) => { txtTenDangNhap.Text = "thungan1"; txtMatKhau.Text = "123456"; };

            pnlCard.Controls.Add(lblFormTitle);
            pnlCard.Controls.Add(lblUser);
            pnlCard.Controls.Add(txtTenDangNhap);
            pnlCard.Controls.Add(lblPass);
            pnlCard.Controls.Add(txtMatKhau);
            pnlCard.Controls.Add(chkHienMatKhau);
            pnlCard.Controls.Add(btnDangNhap);
            pnlCard.Controls.Add(btnThoat);
            pnlCard.Controls.Add(lblQuick);
            pnlCard.Controls.Add(lnkAdmin);
            pnlCard.Controls.Add(lnkCashier);

            this.Controls.Add(pnlCard);
            this.Controls.Add(pnlTop);

            this.AcceptButton = btnDangNhap;
        }

        private void BtnDangNhap_Click(object? sender, EventArgs e)
        {
            var (success, msg) = _authService.Login(txtTenDangNhap.Text, txtMatKhau.Text);
            if (success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(msg, "Đăng Nhập Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
            }
        }
    }
}
