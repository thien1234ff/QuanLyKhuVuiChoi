using System;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Auth
{
    public partial class FormDangKy : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly AuthService _authService;

        public FormDangKy() : this(new QuanLyKhuVuiChoiDbContext())
        {
        }

        public FormDangKy(QuanLyKhuVuiChoiDbContext context)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _authService = new AuthService(_context);

            InitializeComponent();
        }

        private void BtnDangKy_Click(object? sender, EventArgs e)
        {
            var username = txtTenDangNhap.Text.Trim();
            var password = txtMatKhau.Text;
            var confirmPassword = txtXacNhanMatKhau.Text;
            var hoTen = txtHoTen.Text.Trim();
            var sdt = txtSoDienThoai.Text.Trim();
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng điền tên đăng nhập và mật khẩu!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }

            var result = _authService.Register(username, password, hoTen, sdt, email);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
