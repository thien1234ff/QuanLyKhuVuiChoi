using System;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Auth
{
    public partial class FormDangNhap : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly AuthService _authService;

        public FormDangNhap() : this(new QuanLyKhuVuiChoiDbContext())
        {
        }

        public FormDangNhap(QuanLyKhuVuiChoiDbContext context)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _authService = new AuthService(_context);

            InitializeComponent();
        }

        private void BtnDangNhap_Click(object? sender, EventArgs e)
        {
            var username = txtTenDangNhap.Text.Trim();
            var password = txtMatKhau.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            var result = _authService.Login(username, password);
            if (result.Success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtMatKhau.Focus();
            }
        }

        private void BtnThoat_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ChkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = chkHienMatKhau.Checked ? '\0' : '●';
        }

        private void LnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using var form = new FormDangKy(_context);
            form.ShowDialog(this);
        }

        private void LnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using var form = new FormQuenMatKhau(_context);
            form.ShowDialog(this);
        }
    }
}
