namespace QuanLyKhuVuiChoi.Forms.Auth
{
    partial class FormDangKy
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            pnlCard = new Panel();
            btnHuy = new Button();
            btnDangKy = new Button();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtSoDienThoai = new TextBox();
            lblSoDienThoai = new Label();
            txtHoTen = new TextBox();
            lblHoTen = new Label();
            txtXacNhanMatKhau = new TextBox();
            lblXacNhanMatKhau = new Label();
            txtMatKhau = new TextBox();
            lblMatKhau = new Label();
            txtTenDangNhap = new TextBox();
            lblTenDangNhap = new Label();
            pnlTop.SuspendLayout();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(15, 23, 42);
            pnlTop.Controls.Add(lblSubtitle);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(25, 18, 25, 10);
            pnlTop.Size = new Size(560, 95);
            pnlTop.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Location = new Point(26, 55);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(367, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Tạo tài khoản nhân viên mới cho khu vui chơi FunZone";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(24, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(272, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.BorderStyle = BorderStyle.FixedSingle;
            pnlCard.Controls.Add(btnHuy);
            pnlCard.Controls.Add(btnDangKy);
            pnlCard.Controls.Add(txtEmail);
            pnlCard.Controls.Add(lblEmail);
            pnlCard.Controls.Add(txtSoDienThoai);
            pnlCard.Controls.Add(lblSoDienThoai);
            pnlCard.Controls.Add(txtHoTen);
            pnlCard.Controls.Add(lblHoTen);
            pnlCard.Controls.Add(txtXacNhanMatKhau);
            pnlCard.Controls.Add(lblXacNhanMatKhau);
            pnlCard.Controls.Add(txtMatKhau);
            pnlCard.Controls.Add(lblMatKhau);
            pnlCard.Controls.Add(txtTenDangNhap);
            pnlCard.Controls.Add(lblTenDangNhap);
            pnlCard.Location = new Point(30, 115);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(500, 470);
            pnlCard.TabIndex = 1;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(148, 163, 184);
            btnHuy.Cursor = Cursors.Hand;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(265, 400);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(205, 42);
            btnHuy.TabIndex = 13;
            btnHuy.Text = "Hủy bỏ";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += BtnHuy_Click;
            // 
            // btnDangKy
            // 
            btnDangKy.BackColor = Color.FromArgb(16, 185, 129);
            btnDangKy.Cursor = Cursors.Hand;
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Location = new Point(30, 400);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(220, 42);
            btnDangKy.TabIndex = 12;
            btnDangKy.Text = "ĐĂNG KÝ NGAY";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += BtnDangKy_Click;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(30, 345);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(440, 30);
            txtEmail.TabIndex = 11;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.Location = new Point(30, 322);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(51, 20);
            lblEmail.TabIndex = 10;
            lblEmail.Text = "Email:";
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Font = new Font("Segoe UI", 10F);
            txtSoDienThoai.Location = new Point(30, 280);
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.Size = new Size(440, 30);
            txtSoDienThoai.TabIndex = 9;
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSoDienThoai.Location = new Point(30, 257);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(104, 20);
            lblSoDienThoai.TabIndex = 8;
            lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 10F);
            txtHoTen.Location = new Point(30, 215);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(440, 30);
            txtHoTen.TabIndex = 7;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHoTen.Location = new Point(30, 192);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(80, 20);
            lblHoTen.TabIndex = 6;
            lblHoTen.Text = "Họ và tên:";
            // 
            // txtXacNhanMatKhau
            // 
            txtXacNhanMatKhau.Font = new Font("Segoe UI", 10F);
            txtXacNhanMatKhau.Location = new Point(255, 150);
            txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            txtXacNhanMatKhau.PasswordChar = '●';
            txtXacNhanMatKhau.Size = new Size(215, 30);
            txtXacNhanMatKhau.TabIndex = 5;
            // 
            // lblXacNhanMatKhau
            // 
            lblXacNhanMatKhau.AutoSize = true;
            lblXacNhanMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblXacNhanMatKhau.Location = new Point(255, 127);
            lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            lblXacNhanMatKhau.Size = new Size(147, 20);
            lblXacNhanMatKhau.TabIndex = 4;
            lblXacNhanMatKhau.Text = "Xác nhận mật khẩu:";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Segoe UI", 10F);
            txtMatKhau.Location = new Point(30, 150);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '●';
            txtMatKhau.Size = new Size(210, 30);
            txtMatKhau.TabIndex = 3;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMatKhau.Location = new Point(30, 127);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(79, 20);
            lblMatKhau.TabIndex = 2;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Segoe UI", 10F);
            txtTenDangNhap.Location = new Point(30, 85);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(440, 30);
            txtTenDangNhap.TabIndex = 1;
            txtTenDangNhap.TextChanged += txtTenDangNhap_TextChanged;
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTenDangNhap.Location = new Point(30, 62);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(116, 20);
            lblTenDangNhap.TabIndex = 0;
            lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // FormDangKy
            // 
            AcceptButton = btnDangKy;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(560, 610);
            Controls.Add(pnlCard);
            Controls.Add(pnlTop);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormDangKy";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ĐĂNG KÝ TÀI KHOẢN MỚI";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label lblXacNhanMatKhau;
        private System.Windows.Forms.TextBox txtXacNhanMatKhau;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnHuy;
    }
}
