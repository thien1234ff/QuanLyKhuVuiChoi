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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtXacNhanMatKhau = new System.Windows.Forms.TextBox();
            this.lblXacNhanMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlTop.Controls.Add(this.lblSubtitle);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(25, 18, 25, 10);
            this.pnlTop.Size = new System.Drawing.Size(560, 95);
            this.pnlTop.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(26, 55);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(354, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Tạo tài khoản nhân viên mới cho khu vui chơi FunZone";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(24, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(281, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.btnHuy);
            this.pnlCard.Controls.Add(this.btnDangKy);
            this.pnlCard.Controls.Add(this.txtEmail);
            this.pnlCard.Controls.Add(this.lblEmail);
            this.pnlCard.Controls.Add(this.txtSoDienThoai);
            this.pnlCard.Controls.Add(this.lblSoDienThoai);
            this.pnlCard.Controls.Add(this.txtHoTen);
            this.pnlCard.Controls.Add(this.lblHoTen);
            this.pnlCard.Controls.Add(this.txtXacNhanMatKhau);
            this.pnlCard.Controls.Add(this.lblXacNhanMatKhau);
            this.pnlCard.Controls.Add(this.txtMatKhau);
            this.pnlCard.Controls.Add(this.lblMatKhau);
            this.pnlCard.Controls.Add(this.txtTenDangNhap);
            this.pnlCard.Controls.Add(this.lblTenDangNhap);
            this.pnlCard.Location = new System.Drawing.Point(30, 115);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(500, 470);
            this.pnlCard.TabIndex = 1;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(265, 400);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(205, 42);
            this.btnHuy.TabIndex = 13;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnDangKy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangKy.FlatAppearance.BorderSize = 0;
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(30, 400);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(220, 42);
            this.btnDangKy.TabIndex = 12;
            this.btnDangKy.Text = "ĐĂNG KÝ NGAY";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.BtnDangKy_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(30, 345);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(440, 30);
            this.txtEmail.TabIndex = 11;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(30, 322);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 20);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email:";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(30, 280);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(440, 30);
            this.txtSoDienThoai.TabIndex = 9;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoDienThoai.Location = new System.Drawing.Point(30, 257);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(104, 20);
            this.lblSoDienThoai.TabIndex = 8;
            this.lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHoTen.Location = new System.Drawing.Point(30, 215);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(440, 30);
            this.txtHoTen.TabIndex = 7;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.Location = new System.Drawing.Point(30, 192);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(80, 20);
            this.lblHoTen.TabIndex = 6;
            this.lblHoTen.Text = "Họ và tên:";
            // 
            // txtXacNhanMatKhau
            // 
            this.txtXacNhanMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtXacNhanMatKhau.Location = new System.Drawing.Point(255, 150);
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.PasswordChar = '●';
            this.txtXacNhanMatKhau.Size = new System.Drawing.Size(215, 30);
            this.txtXacNhanMatKhau.TabIndex = 5;
            // 
            // lblXacNhanMatKhau
            // 
            this.lblXacNhanMatKhau.AutoSize = true;
            this.lblXacNhanMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblXacNhanMatKhau.Location = new System.Drawing.Point(255, 127);
            this.lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            this.lblXacNhanMatKhau.Size = new System.Drawing.Size(147, 20);
            this.lblXacNhanMatKhau.TabIndex = 4;
            this.lblXacNhanMatKhau.Text = "Xác nhận mật khẩu:";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMatKhau.Location = new System.Drawing.Point(30, 150);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '●';
            this.txtMatKhau.Size = new System.Drawing.Size(210, 30);
            this.txtMatKhau.TabIndex = 3;
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMatKhau.Location = new System.Drawing.Point(30, 127);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(79, 20);
            this.lblMatKhau.TabIndex = 2;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenDangNhap.Location = new System.Drawing.Point(30, 85);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(440, 30);
            this.txtTenDangNhap.TabIndex = 1;
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenDangNhap.Location = new System.Drawing.Point(30, 62);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(116, 20);
            this.lblTenDangNhap.TabIndex = 0;
            this.lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // FormDangKy
            // 
            this.AcceptButton = this.btnDangKy;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(560, 610);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ĐĂNG KÝ TÀI KHOẢN MỚI";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

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
