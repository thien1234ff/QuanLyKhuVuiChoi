namespace QuanLyKhuVuiChoi.Forms.Auth
{
    partial class FormDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblSubtitle = new Label();
            lblLogo = new Label();
            pnlCard = new Panel();
            lnkQuenMatKhau = new LinkLabel();
            lnkDangKy = new LinkLabel();
            btnThoat = new Button();
            btnDangNhap = new Button();
            chkHienMatKhau = new CheckBox();
            txtMatKhau = new TextBox();
            lblMatKhau = new Label();
            txtTenDangNhap = new TextBox();
            lblTenDangNhap = new Label();
            lblFormTitle = new Label();
            pnlTop.SuspendLayout();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(15, 23, 42);
            pnlTop.Controls.Add(lblSubtitle);
            pnlTop.Controls.Add(lblLogo);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(25, 18, 25, 10);
            pnlTop.Size = new Size(524, 111);
            pnlTop.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Location = new Point(27, 60);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(385, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ TOÀN DIỆN";
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(25, 18);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(334, 37);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "🎡 WONDERLAND PARK";
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.BorderStyle = BorderStyle.FixedSingle;
            pnlCard.Controls.Add(lnkQuenMatKhau);
            pnlCard.Controls.Add(lnkDangKy);
            pnlCard.Controls.Add(btnThoat);
            pnlCard.Controls.Add(btnDangNhap);
            pnlCard.Controls.Add(chkHienMatKhau);
            pnlCard.Controls.Add(txtMatKhau);
            pnlCard.Controls.Add(lblMatKhau);
            pnlCard.Controls.Add(txtTenDangNhap);
            pnlCard.Controls.Add(lblTenDangNhap);
            pnlCard.Controls.Add(lblFormTitle);
            pnlCard.Location = new Point(35, 130);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(25);
            pnlCard.Size = new Size(454, 340);
            pnlCard.TabIndex = 1;
            // 
            // lnkQuenMatKhau
            // 
            lnkQuenMatKhau.AutoSize = true;
            lnkQuenMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkQuenMatKhau.LinkColor = Color.FromArgb(220, 38, 38);
            lnkQuenMatKhau.Location = new Point(315, 295);
            lnkQuenMatKhau.Name = "lnkQuenMatKhau";
            lnkQuenMatKhau.Size = new Size(116, 20);
            lnkQuenMatKhau.TabIndex = 9;
            lnkQuenMatKhau.TabStop = true;
            lnkQuenMatKhau.Text = "Quên mật khẩu?";
            lnkQuenMatKhau.LinkClicked += LnkQuenMatKhau_LinkClicked;
            // 
            // lnkDangKy
            // 
            lnkDangKy.AutoSize = true;
            lnkDangKy.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkDangKy.LinkColor = Color.FromArgb(79, 70, 229);
            lnkDangKy.Location = new Point(25, 295);
            lnkDangKy.Name = "lnkDangKy";
            lnkDangKy.Size = new Size(159, 20);
            lnkDangKy.TabIndex = 8;
            lnkDangKy.TabStop = true;
            lnkDangKy.Text = "Chưa có tài khoản? ĐK";
            lnkDangKy.LinkClicked += LnkDangKy_LinkClicked;
            // 
            // btnThoat
            // 
            btnThoat.BackColor = Color.FromArgb(148, 163, 184);
            btnThoat.Cursor = Cursors.Hand;
            btnThoat.FlatAppearance.BorderSize = 0;
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThoat.ForeColor = Color.White;
            btnThoat.Location = new Point(285, 235);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(145, 42);
            btnThoat.TabIndex = 7;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += BtnThoat_Click;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(79, 70, 229);
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(25, 235);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(250, 42);
            btnDangNhap.TabIndex = 6;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += BtnDangNhap_Click;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkHienMatKhau.Location = new Point(25, 195);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(148, 24);
            chkHienMatKhau.TabIndex = 5;
            chkHienMatKhau.Text = "Hiển thị mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += ChkHienMatKhau_CheckedChanged;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMatKhau.Location = new Point(25, 150);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '●';
            txtMatKhau.Size = new Size(405, 32);
            txtMatKhau.TabIndex = 4;
            txtMatKhau.Text = "123456";
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMatKhau.Location = new Point(25, 125);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(86, 21);
            lblMatKhau.TabIndex = 3;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTenDangNhap.Location = new Point(25, 80);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(405, 32);
            txtTenDangNhap.TabIndex = 2;
            txtTenDangNhap.Text = "admin";
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenDangNhap.Location = new Point(25, 55);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(127, 21);
            lblTenDangNhap.TabIndex = 1;
            lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // lblFormTitle
            // 
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFormTitle.ForeColor = Color.FromArgb(79, 70, 229);
            lblFormTitle.Location = new Point(25, 15);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(272, 30);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "ĐĂNG NHẬP TÀI KHOẢN";
            // 
            // FormDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(524, 495);
            Controls.Add(pnlCard);
            Controls.Add(pnlTop);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ĐĂNG NHẬP HỆ THỐNG - FUNZONE";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.CheckBox chkHienMatKhau;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.LinkLabel lnkDangKy;
        private System.Windows.Forms.LinkLabel lnkQuenMatKhau;
    }
}
