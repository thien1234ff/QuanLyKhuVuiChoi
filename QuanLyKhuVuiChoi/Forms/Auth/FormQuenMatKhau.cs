using System;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Auth
{
    public partial class FormQuenMatKhau : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly AuthService _authService;

        public FormQuenMatKhau() : this(new QuanLyKhuVuiChoiDbContext())
        {
        }

        public FormQuenMatKhau(QuanLyKhuVuiChoiDbContext context)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _authService = new AuthService(_context);

            InitializeComponent();
        }

        private void BtnXacNhan_Click(object? sender, EventArgs e)
        {
            var username = txtTenDangNhap.Text.Trim();
            var info = txtEmailOrPhone.Text.Trim();
            var newPass = txtMatKhauMoi.Text;
            var confirmPass = txtXacNhanMatKhauMoi.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(info) || string.IsNullOrWhiteSpace(newPass))
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin yêu cầu!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu xác nhận không trùng khớp!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhauMoi.Focus();
                return;
            }

            var result = _authService.ForgotPassword(username, info, newPass);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Xác minh thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
