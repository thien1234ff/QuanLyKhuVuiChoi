namespace QuanLyKhuVuiChoi.Forms
{
    partial class FormMain
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDangNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDangKy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuenMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBanVe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTroChoi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDichVu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeaderRight = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnHeaderDangKy = new System.Windows.Forms.Button();
            this.btnHeaderDangNhap = new System.Windows.Forms.Button();
            this.lblUserGreeting = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.flpSidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMenuSection1 = new System.Windows.Forms.Label();
            this.btnTabDashboard = new System.Windows.Forms.Button();
            this.btnTabBanVe = new System.Windows.Forms.Button();
            this.btnTabHoaDon = new System.Windows.Forms.Button();
            this.btnTabTroChoi = new System.Windows.Forms.Button();
            this.btnTabDichVu = new System.Windows.Forms.Button();
            this.btnTabKhachHang = new System.Windows.Forms.Button();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblMenuSection2 = new System.Windows.Forms.Label();
            this.btnTabVe = new System.Windows.Forms.Button();
            this.btnTabNhanVien = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusReady = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlHeaderRight.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.flpSidebar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.Color.White;
            this.menuMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHeThong,
            this.menuDanhMuc,
            this.menuTroGiup});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1300, 28);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuMain";
            // 
            // menuHeThong
            // 
            this.menuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDangNhap,
            this.menuDangKy,
            this.menuQuenMatKhau,
            this.toolStripSeparator1,
            this.menuDangXuat,
            this.menuThoat});
            this.menuHeThong.Name = "menuHeThong";
            this.menuHeThong.Size = new System.Drawing.Size(86, 24);
            this.menuHeThong.Text = "Hệ Thống";
            // 
            // menuDangNhap
            // 
            this.menuDangNhap.Name = "menuDangNhap";
            this.menuDangNhap.Size = new System.Drawing.Size(224, 26);
            this.menuDangNhap.Text = "Đăng Nhập";
            this.menuDangNhap.Click += new System.EventHandler(this.BtnHeaderDangNhap_Click);
            // 
            // menuDangKy
            // 
            this.menuDangKy.Name = "menuDangKy";
            this.menuDangKy.Size = new System.Drawing.Size(224, 26);
            this.menuDangKy.Text = "Đăng Ký";
            this.menuDangKy.Click += new System.EventHandler(this.BtnHeaderDangKy_Click);
            // 
            // menuQuenMatKhau
            // 
            this.menuQuenMatKhau.Name = "menuQuenMatKhau";
            this.menuQuenMatKhau.Size = new System.Drawing.Size(224, 26);
            this.menuQuenMatKhau.Text = "Quên Mật Khẩu";
            this.menuQuenMatKhau.Click += new System.EventHandler(this.MenuQuenMatKhau_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // menuDangXuat
            // 
            this.menuDangXuat.Name = "menuDangXuat";
            this.menuDangXuat.Size = new System.Drawing.Size(224, 26);
            this.menuDangXuat.Text = "Đăng Xuất";
            this.menuDangXuat.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // menuThoat
            // 
            this.menuThoat.Name = "menuThoat";
            this.menuThoat.Size = new System.Drawing.Size(224, 26);
            this.menuThoat.Text = "Thoát";
            this.menuThoat.Click += new System.EventHandler(this.MenuThoat_Click);
            // 
            // menuDanhMuc
            // 
            this.menuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDashboard,
            this.menuBanVe,
            this.menuHoaDon,
            this.menuTroChoi,
            this.menuDichVu,
            this.menuKhachHang,
            this.menuVe,
            this.menuNhanVien});
            this.menuDanhMuc.Name = "menuDanhMuc";
            this.menuDanhMuc.Size = new System.Drawing.Size(95, 24);
            this.menuDanhMuc.Text = "Chức Năng";
            // 
            // menuDashboard
            // 
            this.menuDashboard.Name = "menuDashboard";
            this.menuDashboard.Size = new System.Drawing.Size(232, 26);
            this.menuDashboard.Text = "Tổng Quan / Báo Cáo";
            this.menuDashboard.Click += new System.EventHandler(this.BtnTabDashboard_Click);
            // 
            // menuBanVe
            // 
            this.menuBanVe.Name = "menuBanVe";
            this.menuBanVe.Size = new System.Drawing.Size(232, 26);
            this.menuBanVe.Text = "Bán Vé POS";
            this.menuBanVe.Click += new System.EventHandler(this.BtnTabBanVe_Click);
            // 
            // menuHoaDon
            // 
            this.menuHoaDon.Name = "menuHoaDon";
            this.menuHoaDon.Size = new System.Drawing.Size(232, 26);
            this.menuHoaDon.Text = "Quản Lý Hóa Đơn";
            this.menuHoaDon.Click += new System.EventHandler(this.BtnTabHoaDon_Click);
            // 
            // menuTroChoi
            // 
            this.menuTroChoi.Name = "menuTroChoi";
            this.menuTroChoi.Size = new System.Drawing.Size(232, 26);
            this.menuTroChoi.Text = "Quản Lý Trò Chơi";
            this.menuTroChoi.Click += new System.EventHandler(this.BtnTabTroChoi_Click);
            // 
            // menuDichVu
            // 
            this.menuDichVu.Name = "menuDichVu";
            this.menuDichVu.Size = new System.Drawing.Size(232, 26);
            this.menuDichVu.Text = "Quản Lý Dịch Vụ";
            this.menuDichVu.Click += new System.EventHandler(this.BtnTabDichVu_Click);
            // 
            // menuKhachHang
            // 
            this.menuKhachHang.Name = "menuKhachHang";
            this.menuKhachHang.Size = new System.Drawing.Size(232, 26);
            this.menuKhachHang.Text = "Quản Lý Khách Hàng";
            this.menuKhachHang.Click += new System.EventHandler(this.BtnTabKhachHang_Click);
            // 
            // menuVe
            // 
            this.menuVe.Name = "menuVe";
            this.menuVe.Size = new System.Drawing.Size(232, 26);
            this.menuVe.Text = "Bảng Giá Loại Vé";
            this.menuVe.Click += new System.EventHandler(this.BtnTabVe_Click);
            // 
            // menuNhanVien
            // 
            this.menuNhanVien.Name = "menuNhanVien";
            this.menuNhanVien.Size = new System.Drawing.Size(232, 26);
            this.menuNhanVien.Text = "Quản Lý Nhân Viên";
            this.menuNhanVien.Click += new System.EventHandler(this.BtnTabNhanVien_Click);
            // 
            // menuTroGiup
            // 
            this.menuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuTroGiup.Name = "menuTroGiup";
            this.menuTroGiup.Size = new System.Drawing.Size(66, 24);
            this.menuTroGiup.Text = "Hỗ Trợ";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(155, 26);
            this.menuAbout.Text = "Giới Thiệu";
            this.menuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.pnlHeaderRight);
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblLogo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 28);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1300, 68);
            this.pnlHeader.TabIndex = 1;
            // 
            // pnlHeaderRight
            // 
            this.pnlHeaderRight.Controls.Add(this.btnLogout);
            this.pnlHeaderRight.Controls.Add(this.btnHeaderDangKy);
            this.pnlHeaderRight.Controls.Add(this.btnHeaderDangNhap);
            this.pnlHeaderRight.Controls.Add(this.lblUserGreeting);
            this.pnlHeaderRight.Controls.Add(this.lblClock);
            this.pnlHeaderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHeaderRight.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlHeaderRight.Location = new System.Drawing.Point(400, 0);
            this.pnlHeaderRight.Name = "pnlHeaderRight";
            this.pnlHeaderRight.Padding = new System.Windows.Forms.Padding(0, 16, 20, 0);
            this.pnlHeaderRight.Size = new System.Drawing.Size(900, 68);
            this.pnlHeaderRight.TabIndex = 5;
            // 
            // btnLogout
            // 
            this.btnLogout.AutoSize = true;
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(630, 16);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnLogout.MinimumSize = new System.Drawing.Size(130, 36);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btnLogout.Size = new System.Drawing.Size(130, 36);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Đăng Xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // btnHeaderDangKy
            // 
            this.btnHeaderDangKy.AutoSize = true;
            this.btnHeaderDangKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnHeaderDangKy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeaderDangKy.FlatAppearance.BorderSize = 0;
            this.btnHeaderDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeaderDangKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHeaderDangKy.ForeColor = System.Drawing.Color.White;
            this.btnHeaderDangKy.Location = new System.Drawing.Point(495, 16);
            this.btnHeaderDangKy.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnHeaderDangKy.MinimumSize = new System.Drawing.Size(125, 36);
            this.btnHeaderDangKy.Name = "btnHeaderDangKy";
            this.btnHeaderDangKy.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btnHeaderDangKy.Size = new System.Drawing.Size(125, 36);
            this.btnHeaderDangKy.TabIndex = 1;
            this.btnHeaderDangKy.Text = "Đăng Ký";
            this.btnHeaderDangKy.UseVisualStyleBackColor = false;
            this.btnHeaderDangKy.Click += new System.EventHandler(this.BtnHeaderDangKy_Click);
            // 
            // btnHeaderDangNhap
            // 
            this.btnHeaderDangNhap.AutoSize = true;
            this.btnHeaderDangNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnHeaderDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeaderDangNhap.FlatAppearance.BorderSize = 0;
            this.btnHeaderDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeaderDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHeaderDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnHeaderDangNhap.Location = new System.Drawing.Point(345, 16);
            this.btnHeaderDangNhap.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnHeaderDangNhap.MinimumSize = new System.Drawing.Size(140, 36);
            this.btnHeaderDangNhap.Name = "btnHeaderDangNhap";
            this.btnHeaderDangNhap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btnHeaderDangNhap.Size = new System.Drawing.Size(140, 36);
            this.btnHeaderDangNhap.TabIndex = 2;
            this.btnHeaderDangNhap.Text = "Đăng Nhập";
            this.btnHeaderDangNhap.UseVisualStyleBackColor = false;
            this.btnHeaderDangNhap.Click += new System.EventHandler(this.BtnHeaderDangNhap_Click);
            // 
            // lblUserGreeting
            // 
            this.lblUserGreeting.AutoSize = true;
            this.lblUserGreeting.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUserGreeting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblUserGreeting.Location = new System.Drawing.Point(219, 23);
            this.lblUserGreeting.Margin = new System.Windows.Forms.Padding(0, 7, 14, 0);
            this.lblUserGreeting.Name = "lblUserGreeting";
            this.lblUserGreeting.Size = new System.Drawing.Size(175, 21);
            this.lblUserGreeting.TabIndex = 3;
            this.lblUserGreeting.Text = "Chào: Quản trị viên";
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblClock.Location = new System.Drawing.Point(20, 24);
            this.lblClock.Margin = new System.Windows.Forms.Padding(0, 8, 14, 0);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(165, 20);
            this.lblClock.TabIndex = 4;
            this.lblClock.Text = "Thứ Hai, 12/10/2026";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSub.Location = new System.Drawing.Point(22, 42);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(262, 19);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ";
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(20, 10);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(260, 32);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "WONDERLAND PARK";
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.pnlContent);
            this.pnlBody.Controls.Add(this.pnlSidebar);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 96);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1300, 694);
            this.pnlBody.TabIndex = 2;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(260, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1040, 694);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSidebar.Controls.Add(this.flpSidebar);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(260, 694);
            this.pnlSidebar.TabIndex = 0;
            // 
            // flpSidebar
            // 
            this.flpSidebar.AutoScroll = true;
            this.flpSidebar.Controls.Add(this.lblMenuSection1);
            this.flpSidebar.Controls.Add(this.btnTabDashboard);
            this.flpSidebar.Controls.Add(this.btnTabBanVe);
            this.flpSidebar.Controls.Add(this.btnTabHoaDon);
            this.flpSidebar.Controls.Add(this.btnTabTroChoi);
            this.flpSidebar.Controls.Add(this.btnTabDichVu);
            this.flpSidebar.Controls.Add(this.btnTabKhachHang);
            this.flpSidebar.Controls.Add(this.pnlDivider);
            this.flpSidebar.Controls.Add(this.lblMenuSection2);
            this.flpSidebar.Controls.Add(this.btnTabVe);
            this.flpSidebar.Controls.Add(this.btnTabNhanVien);
            this.flpSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSidebar.Location = new System.Drawing.Point(0, 0);
            this.flpSidebar.Name = "flpSidebar";
            this.flpSidebar.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.flpSidebar.Size = new System.Drawing.Size(260, 694);
            this.flpSidebar.TabIndex = 0;
            this.flpSidebar.WrapContents = false;
            // 
            // lblMenuSection1
            // 
            this.lblMenuSection1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblMenuSection1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblMenuSection1.Location = new System.Drawing.Point(0, 10);
            this.lblMenuSection1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblMenuSection1.Name = "lblMenuSection1";
            this.lblMenuSection1.Padding = new System.Windows.Forms.Padding(18, 5, 0, 0);
            this.lblMenuSection1.Size = new System.Drawing.Size(260, 26);
            this.lblMenuSection1.TabIndex = 0;
            this.lblMenuSection1.Text = "TÍNH NĂNG CỐT LÕI";
            // 
            // btnTabDashboard
            // 
            this.btnTabDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnTabDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabDashboard.FlatAppearance.BorderSize = 0;
            this.btnTabDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabDashboard.ForeColor = System.Drawing.Color.White;
            this.btnTabDashboard.Location = new System.Drawing.Point(8, 45);
            this.btnTabDashboard.Margin = new System.Windows.Forms.Padding(8, 2, 8, 4);
            this.btnTabDashboard.Name = "btnTabDashboard";
            this.btnTabDashboard.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnTabDashboard.Size = new System.Drawing.Size(244, 48);
            this.btnTabDashboard.TabIndex = 1;
            this.btnTabDashboard.Text = "Tổng Quan";
            this.btnTabDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabDashboard.UseVisualStyleBackColor = false;
            this.btnTabDashboard.Click += new System.EventHandler(this.BtnTabDashboard_Click);
            // 
            // btnTabBanVe
            // 
            this.btnTabBanVe.BackColor = System.Drawing.Color.Transparent;
            this.btnTabBanVe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabBanVe.FlatAppearance.BorderSize = 0;
            this.btnTabBanVe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabBanVe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabBanVe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabBanVe.Location = new System.Drawing.Point(8, 101);
            this.btnTabBanVe.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.btnTabBanVe.Name = "btnTabBanVe";
            this.btnTabBanVe.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnTabBanVe.Size = new System.Drawing.Size(244, 48);
            this.btnTabBanVe.TabIndex = 2;
            this.btnTabBanVe.Text = "Bán Vé POS";
            this.btnTabBanVe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabBanVe.UseVisualStyleBackColor = false;
            this.btnTabBanVe.Click += new System.EventHandler(this.BtnTabBanVe_Click);
            // 
            // btnTabHoaDon
            // 
            this.btnTabHoaDon.BackColor = System.Drawing.Color.Transparent;
            this.btnTabHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabHoaDon.FlatAppearance.BorderSize = 0;
            this.btnTabHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabHoaDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabHoaDon.Location = new System.Drawing.Point(8, 157);
            this.btnTabHoaDon.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.btnTabHoaDon.Name = "btnTabHoaDon";
            this.btnTabHoaDon.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnTabHoaDon.Size = new System.Drawing.Size(244, 48);
            this.btnTabHoaDon.TabIndex = 3;
            this.btnTabHoaDon.Text = "Quản Lý Hóa Đơn";
            this.btnTabHoaDon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabHoaDon.UseVisualStyleBackColor = false;
            this.btnTabHoaDon.Click += new System.EventHandler(this.BtnTabHoaDon_Click);
            // 
            // btnTabTroChoi
            // 
            this.btnTabTroChoi.BackColor = System.Drawing.Color.Transparent;
            this.btnTabTroChoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabTroChoi.FlatAppearance.BorderSize = 0;
            this.btnTabTroChoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabTroChoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabTroChoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabTroChoi.Location = new System.Drawing.Point(8, 213);
            this.btnTabTroChoi.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.btnTabTroChoi.Name = "btnTabTroChoi";
            this.btnTabTroChoi.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnTabTroChoi.Size = new System.Drawing.Size(244, 48);
            this.btnTabTroChoi.TabIndex = 4;
            this.btnTabTroChoi.Text = "Quản Lý Trò Chơi";
            this.btnTabTroChoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabTroChoi.UseVisualStyleBackColor = false;
            this.btnTabTroChoi.Click += new System.EventHandler(this.BtnTabTroChoi_Click);
            // 
            // btnTabDichVu
            // 
            this.btnTabDichVu.BackColor = System.Drawing.Color.Transparent;
            this.btnTabDichVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabDichVu.FlatAppearance.BorderSize = 0;
            this.btnTabDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabDichVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabDichVu.Location = new System.Drawing.Point(8, 269);
            this.btnTabDichVu.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.btnTabDichVu.Name = "btnTabDichVu";
            this.btnTabDichVu.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnTabDichVu.Size = new System.Drawing.Size(244, 48);
            this.btnTabDichVu.TabIndex = 5;
            this.btnTabDichVu.Text = "Quản Lý Dịch Vụ";
            this.btnTabDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabDichVu.UseVisualStyleBackColor = false;
            this.btnTabDichVu.Click += new System.EventHandler(this.BtnTabDichVu_Click);
            // 
            // btnTabKhachHang
            // 
            this.btnTabKhachHang.BackColor = System.Drawing.Color.Transparent;
            this.btnTabKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabKhachHang.FlatAppearance.BorderSize = 0;
            this.btnTabKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabKhachHang.Location = new System.Drawing.Point(8, 325);
            this.btnTabKhachHang.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.btnTabKhachHang.Name = "btnTabKhachHang";
            this.btnTabKhachHang.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnTabKhachHang.Size = new System.Drawing.Size(244, 48);
            this.btnTabKhachHang.TabIndex = 6;
            this.btnTabKhachHang.Text = "Quản Lý Khách Hàng";
            this.btnTabKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabKhachHang.UseVisualStyleBackColor = false;
            this.btnTabKhachHang.Click += new System.EventHandler(this.BtnTabKhachHang_Click);
            // 
            // pnlDivider
            // 
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.pnlDivider.Location = new System.Drawing.Point(12, 385);
            this.pnlDivider.Margin = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(236, 1);
            this.pnlDivider.TabIndex = 7;
            // 
            // lblMenuSection2
            // 
            this.lblMenuSection2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblMenuSection2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblMenuSection2.Location = new System.Drawing.Point(0, 394);
            this.lblMenuSection2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblMenuSection2.Name = "lblMenuSection2";
            this.lblMenuSection2.Padding = new System.Windows.Forms.Padding(18, 5, 0, 0);
            this.lblMenuSection2.Size = new System.Drawing.Size(260, 26);
            this.lblMenuSection2.TabIndex = 8;
            this.lblMenuSection2.Text = "CẤU HÌNH & QUẢN TRỊ";
            // 
            // btnTabVe
            // 
            this.btnTabVe.BackColor = System.Drawing.Color.Transparent;
            this.btnTabVe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabVe.FlatAppearance.BorderSize = 0;
            this.btnTabVe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabVe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabVe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabVe.Location = new System.Drawing.Point(8, 429);
            this.btnTabVe.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.btnTabVe.Name = "btnTabVe";
            this.btnTabVe.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnTabVe.Size = new System.Drawing.Size(244, 48);
            this.btnTabVe.TabIndex = 9;
            this.btnTabVe.Text = "Bảng Giá Loại Vé";
            this.btnTabVe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabVe.UseVisualStyleBackColor = false;
            this.btnTabVe.Click += new System.EventHandler(this.BtnTabVe_Click);
            // 
            // btnTabNhanVien
            // 
            this.btnTabNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.btnTabNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabNhanVien.FlatAppearance.BorderSize = 0;
            this.btnTabNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabNhanVien.Location = new System.Drawing.Point(8, 485);
            this.btnTabNhanVien.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.btnTabNhanVien.Name = "btnTabNhanVien";
            this.btnTabNhanVien.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnTabNhanVien.Size = new System.Drawing.Size(244, 48);
            this.btnTabNhanVien.TabIndex = 10;
            this.btnTabNhanVien.Text = "Quản Lý Nhân Viên";
            this.btnTabNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabNhanVien.UseVisualStyleBackColor = false;
            this.btnTabNhanVien.Click += new System.EventHandler(this.BtnTabNhanVien_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusReady,
            this.lblStatusDb,
            this.lblStatusVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 790);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1300, 26);
            this.statusStrip1.TabIndex = 3;
            // 
            // lblStatusReady
            // 
            this.lblStatusReady.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblStatusReady.Name = "lblStatusReady";
            this.lblStatusReady.Size = new System.Drawing.Size(125, 20);
            this.lblStatusReady.Text = "Hệ thống sẵn sàng";
            // 
            // lblStatusDb
            // 
            this.lblStatusDb.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.lblStatusDb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatusDb.Name = "lblStatusDb";
            this.lblStatusDb.Size = new System.Drawing.Size(209, 20);
            this.lblStatusDb.Text = "CSDL: SQL Server (Wonderland)";
            // 
            // lblStatusVersion
            // 
            this.lblStatusVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatusVersion.Name = "lblStatusVersion";
            this.lblStatusVersion.Size = new System.Drawing.Size(107, 20);
            this.lblStatusVersion.Text = "Phiên bản: 2.0.0";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1300, 816);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainMenuStrip = this.menuMain;
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WONDERLAND PARK - HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlHeaderRight.ResumeLayout(false);
            this.pnlHeaderRight.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.flpSidebar.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuHeThong;
        private System.Windows.Forms.ToolStripMenuItem menuDangNhap;
        private System.Windows.Forms.ToolStripMenuItem menuDangKy;
        private System.Windows.Forms.ToolStripMenuItem menuQuenMatKhau;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem menuThoat;
        private System.Windows.Forms.ToolStripMenuItem menuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem menuDashboard;
        private System.Windows.Forms.ToolStripMenuItem menuBanVe;
        private System.Windows.Forms.ToolStripMenuItem menuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem menuTroChoi;
        private System.Windows.Forms.ToolStripMenuItem menuDichVu;
        private System.Windows.Forms.ToolStripMenuItem menuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem menuVe;
        private System.Windows.Forms.ToolStripMenuItem menuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem menuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.FlowLayoutPanel pnlHeaderRight;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblUserGreeting;
        private System.Windows.Forms.Button btnHeaderDangNhap;
        private System.Windows.Forms.Button btnHeaderDangKy;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.FlowLayoutPanel flpSidebar;
        private System.Windows.Forms.Label lblMenuSection1;
        private System.Windows.Forms.Button btnTabDashboard;
        private System.Windows.Forms.Button btnTabBanVe;
        private System.Windows.Forms.Button btnTabHoaDon;
        private System.Windows.Forms.Button btnTabTroChoi;
        private System.Windows.Forms.Button btnTabDichVu;
        private System.Windows.Forms.Button btnTabKhachHang;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblMenuSection2;
        private System.Windows.Forms.Button btnTabVe;
        private System.Windows.Forms.Button btnTabNhanVien;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusReady;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusDb;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusVersion;
    }
}
