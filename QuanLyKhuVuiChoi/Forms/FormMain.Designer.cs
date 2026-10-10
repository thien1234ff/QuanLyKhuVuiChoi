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
            this.menuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTroChoi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDichVu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNghiepVu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBanVe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCaoDoanhThu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTienIch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImportKH = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportKH = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblUserGreeting = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusReady = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.flpModules = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCardDashboard = new System.Windows.Forms.Button();
            this.btnCardBanVe = new System.Windows.Forms.Button();
            this.btnCardHoaDon = new System.Windows.Forms.Button();
            this.btnCardBaoCao = new System.Windows.Forms.Button();
            this.btnCardTroChoi = new System.Windows.Forms.Button();
            this.btnCardDichVu = new System.Windows.Forms.Button();
            this.btnCardVe = new System.Windows.Forms.Button();
            this.btnCardKhachHang = new System.Windows.Forms.Button();
            this.btnCardNhanVien = new System.Windows.Forms.Button();
            this.btnCardDangNhap = new System.Windows.Forms.Button();
            this.btnCardDangKy = new System.Windows.Forms.Button();
            this.btnCardQuenMatKhau = new System.Windows.Forms.Button();
            this.btnCardImportKH = new System.Windows.Forms.Button();
            this.btnCardExportKH = new System.Windows.Forms.Button();
            this.menuMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.flpModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.Color.White;
            this.menuMain.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHeThong,
            this.menuDanhMuc,
            this.menuNghiepVu,
            this.menuBaoCao,
            this.menuTienIch,
            this.menuTroGiup});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1280, 29);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
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
            this.menuHeThong.Size = new System.Drawing.Size(90, 25);
            this.menuHeThong.Text = "🔐 Hệ Thống";
            // 
            // menuDangNhap
            // 
            this.menuDangNhap.Name = "menuDangNhap";
            this.menuDangNhap.Size = new System.Drawing.Size(224, 26);
            this.menuDangNhap.Text = "🔑 Đăng Nhập";
            this.menuDangNhap.Click += new System.EventHandler(this.BtnCardDangNhap_Click);
            // 
            // menuDangKy
            // 
            this.menuDangKy.Name = "menuDangKy";
            this.menuDangKy.Size = new System.Drawing.Size(224, 26);
            this.menuDangKy.Text = "📝 Đăng Ký";
            this.menuDangKy.Click += new System.EventHandler(this.BtnCardDangKy_Click);
            // 
            // menuQuenMatKhau
            // 
            this.menuQuenMatKhau.Name = "menuQuenMatKhau";
            this.menuQuenMatKhau.Size = new System.Drawing.Size(224, 26);
            this.menuQuenMatKhau.Text = "❓ Quên Mật Khẩu";
            this.menuQuenMatKhau.Click += new System.EventHandler(this.BtnCardQuenMatKhau_Click);
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
            this.menuDangXuat.Text = "🚪 Đăng Xuất";
            this.menuDangXuat.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // menuThoat
            // 
            this.menuThoat.Name = "menuThoat";
            this.menuThoat.Size = new System.Drawing.Size(224, 26);
            this.menuThoat.Text = "❌ Thoát";
            this.menuThoat.Click += new System.EventHandler(this.MenuThoat_Click);
            // 
            // menuDanhMuc
            // 
            this.menuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuKhachHang,
            this.menuNhanVien,
            this.menuTroChoi,
            this.menuDichVu,
            this.menuVe});
            this.menuDanhMuc.Name = "menuDanhMuc";
            this.menuDanhMuc.Size = new System.Drawing.Size(95, 25);
            this.menuDanhMuc.Text = "📁 Danh Mục";
            // 
            // menuKhachHang
            // 
            this.menuKhachHang.Name = "menuKhachHang";
            this.menuKhachHang.Size = new System.Drawing.Size(224, 26);
            this.menuKhachHang.Text = "👥 Quản Lý Khách Hàng";
            this.menuKhachHang.Click += new System.EventHandler(this.BtnCardKhachHang_Click);
            // 
            // menuNhanVien
            // 
            this.menuNhanVien.Name = "menuNhanVien";
            this.menuNhanVien.Size = new System.Drawing.Size(224, 26);
            this.menuNhanVien.Text = "👔 Quản Lý Nhân Viên";
            this.menuNhanVien.Click += new System.EventHandler(this.BtnCardNhanVien_Click);
            // 
            // menuTroChoi
            // 
            this.menuTroChoi.Name = "menuTroChoi";
            this.menuTroChoi.Size = new System.Drawing.Size(224, 26);
            this.menuTroChoi.Text = "🎡 Quản Lý Trò Chơi";
            this.menuTroChoi.Click += new System.EventHandler(this.BtnCardTroChoi_Click);
            // 
            // menuDichVu
            // 
            this.menuDichVu.Name = "menuDichVu";
            this.menuDichVu.Size = new System.Drawing.Size(224, 26);
            this.menuDichVu.Text = "🍔 Quản Lý Dịch Vụ";
            this.menuDichVu.Click += new System.EventHandler(this.BtnCardDichVu_Click);
            // 
            // menuVe
            // 
            this.menuVe.Name = "menuVe";
            this.menuVe.Size = new System.Drawing.Size(224, 26);
            this.menuVe.Text = "🎫 Quản Lý Loại Vé";
            this.menuVe.Click += new System.EventHandler(this.BtnCardVe_Click);
            // 
            // menuNghiepVu
            // 
            this.menuNghiepVu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBanVe,
            this.menuHoaDon});
            this.menuNghiepVu.Name = "menuNghiepVu";
            this.menuNghiepVu.Size = new System.Drawing.Size(96, 25);
            this.menuNghiepVu.Text = "🛒 Nghiệp Vụ";
            // 
            // menuBanVe
            // 
            this.menuBanVe.Name = "menuBanVe";
            this.menuBanVe.Size = new System.Drawing.Size(224, 26);
            this.menuBanVe.Text = "🎟️ Bán Vé POS";
            this.menuBanVe.Click += new System.EventHandler(this.BtnCardBanVe_Click);
            // 
            // menuHoaDon
            // 
            this.menuHoaDon.Name = "menuHoaDon";
            this.menuHoaDon.Size = new System.Drawing.Size(224, 26);
            this.menuHoaDon.Text = "🧾 Quản Lý Hóa Đơn";
            this.menuHoaDon.Click += new System.EventHandler(this.BtnCardHoaDon_Click);
            // 
            // menuBaoCao
            // 
            this.menuBaoCao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDashboard,
            this.menuBaoCaoDoanhThu});
            this.menuBaoCao.Name = "menuBaoCao";
            this.menuBaoCao.Size = new System.Drawing.Size(81, 25);
            this.menuBaoCao.Text = "📊 Báo Cáo";
            // 
            // menuDashboard
            // 
            this.menuDashboard.Name = "menuDashboard";
            this.menuDashboard.Size = new System.Drawing.Size(232, 26);
            this.menuDashboard.Text = "📈 Dashboard Thống Kê";
            this.menuDashboard.Click += new System.EventHandler(this.BtnCardDashboard_Click);
            // 
            // menuBaoCaoDoanhThu
            // 
            this.menuBaoCaoDoanhThu.Name = "menuBaoCaoDoanhThu";
            this.menuBaoCaoDoanhThu.Size = new System.Drawing.Size(232, 26);
            this.menuBaoCaoDoanhThu.Text = "💰 Báo Cáo Doanh Thu";
            this.menuBaoCaoDoanhThu.Click += new System.EventHandler(this.BtnCardBaoCao_Click);
            // 
            // menuTienIch
            // 
            this.menuTienIch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuImportKH,
            this.menuExportKH});
            this.menuTienIch.Name = "menuTienIch";
            this.menuTienIch.Size = new System.Drawing.Size(76, 25);
            this.menuTienIch.Text = "📥 Tiện Ích";
            // 
            // menuImportKH
            // 
            this.menuImportKH.Name = "menuImportKH";
            this.menuImportKH.Size = new System.Drawing.Size(248, 26);
            this.menuImportKH.Text = "📥 Nhập Excel Khách Hàng";
            this.menuImportKH.Click += new System.EventHandler(this.BtnCardImportKH_Click);
            // 
            // menuExportKH
            // 
            this.menuExportKH.Name = "menuExportKH";
            this.menuExportKH.Size = new System.Drawing.Size(248, 26);
            this.menuExportKH.Text = "📤 Xuất Excel Khách Hàng";
            this.menuExportKH.Click += new System.EventHandler(this.BtnCardExportKH_Click);
            // 
            // menuTroGiup
            // 
            this.menuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuTroGiup.Name = "menuTroGiup";
            this.menuTroGiup.Size = new System.Drawing.Size(81, 25);
            this.menuTroGiup.Text = "ℹ️ Trợ Giúp";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(227, 26);
            this.menuAbout.Text = "Giới Thiệu Ứng Dụng";
            this.menuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.lblClock);
            this.pnlHeader.Controls.Add(this.lblUserGreeting);
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblLogo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 29);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.pnlHeader.Size = new System.Drawing.Size(1280, 80);
            this.pnlHeader.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1140, 24);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(115, 34);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "🚪 Đăng Xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // lblClock
            // 
            this.lblClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClock.AutoSize = true;
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblClock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblClock.Location = new System.Drawing.Point(860, 30);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(217, 21);
            this.lblClock.TabIndex = 3;
            this.lblClock.Text = "📅 Thứ Năm, 01/01/2026 00:00";
            // 
            // lblUserGreeting
            // 
            this.lblUserGreeting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserGreeting.AutoSize = true;
            this.lblUserGreeting.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserGreeting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(189)))), ((int)(((byte)(248)))));
            this.lblUserGreeting.Location = new System.Drawing.Point(680, 29);
            this.lblUserGreeting.Name = "lblUserGreeting";
            this.lblUserGreeting.Size = new System.Drawing.Size(133, 23);
            this.lblUserGreeting.TabIndex = 2;
            this.lblUserGreeting.Text = "👤 Chào: Admin";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSub.Location = new System.Drawing.Point(24, 46);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(437, 20);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "TRUNG TÂM ĐIỀU HÀNH 12 CHỨC NĂNG ĐỘC LẬP (WINFORMS .NET 8)";
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(22, 14);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(395, 32);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "🎡 FUNZONE PARK MANAGEMENT";
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 774);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1280, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusReady
            // 
            this.lblStatusReady.Name = "lblStatusReady";
            this.lblStatusReady.Size = new System.Drawing.Size(128, 20);
            this.lblStatusReady.Text = "✅ Sẵn sàng làm việc";
            // 
            // lblStatusDb
            // 
            this.lblStatusDb.Name = "lblStatusDb";
            this.lblStatusDb.Size = new System.Drawing.Size(952, 20);
            this.lblStatusDb.Spring = true;
            this.lblStatusDb.Text = "🗄️ CSDL: SQL Server (Kết nối ổn định)";
            // 
            // lblStatusVersion
            // 
            this.lblStatusVersion.Name = "lblStatusVersion";
            this.lblStatusVersion.Size = new System.Drawing.Size(185, 20);
            this.lblStatusVersion.Text = "Phiên bản: 2.0.0 (.NET 8 WinForms)";
            // 
            // pnlBody
            // 
            this.pnlBody.AutoScroll = true;
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlBody.Controls.Add(this.flpModules);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 109);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 25, 30, 25);
            this.pnlBody.Size = new System.Drawing.Size(1280, 665);
            this.pnlBody.TabIndex = 3;
            // 
            // flpModules
            // 
            this.flpModules.AutoScroll = true;
            this.flpModules.Controls.Add(this.btnCardDashboard);
            this.flpModules.Controls.Add(this.btnCardBanVe);
            this.flpModules.Controls.Add(this.btnCardHoaDon);
            this.flpModules.Controls.Add(this.btnCardBaoCao);
            this.flpModules.Controls.Add(this.btnCardTroChoi);
            this.flpModules.Controls.Add(this.btnCardDichVu);
            this.flpModules.Controls.Add(this.btnCardVe);
            this.flpModules.Controls.Add(this.btnCardKhachHang);
            this.flpModules.Controls.Add(this.btnCardNhanVien);
            this.flpModules.Controls.Add(this.btnCardDangNhap);
            this.flpModules.Controls.Add(this.btnCardDangKy);
            this.flpModules.Controls.Add(this.btnCardQuenMatKhau);
            this.flpModules.Controls.Add(this.btnCardImportKH);
            this.flpModules.Controls.Add(this.btnCardExportKH);
            this.flpModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpModules.Location = new System.Drawing.Point(30, 25);
            this.flpModules.Name = "flpModules";
            this.flpModules.Size = new System.Drawing.Size(1220, 615);
            this.flpModules.TabIndex = 0;
            // 
            // btnCardDashboard
            // 
            this.btnCardDashboard.BackColor = System.Drawing.Color.White;
            this.btnCardDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardDashboard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardDashboard.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardDashboard.Location = new System.Drawing.Point(12, 12);
            this.btnCardDashboard.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardDashboard.Name = "btnCardDashboard";
            this.btnCardDashboard.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardDashboard.Size = new System.Drawing.Size(265, 125);
            this.btnCardDashboard.TabIndex = 0;
            this.btnCardDashboard.Text = "📊 TRANG CHỦ / DASHBOARD\r\n\r\nTổng quan chỉ số KPI, doanh thu và lưu lượng khách";
            this.btnCardDashboard.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardDashboard.UseVisualStyleBackColor = false;
            this.btnCardDashboard.Click += new System.EventHandler(this.BtnCardDashboard_Click);
            // 
            // btnCardBanVe
            // 
            this.btnCardBanVe.BackColor = System.Drawing.Color.White;
            this.btnCardBanVe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardBanVe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardBanVe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardBanVe.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardBanVe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardBanVe.Location = new System.Drawing.Point(301, 12);
            this.btnCardBanVe.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardBanVe.Name = "btnCardBanVe";
            this.btnCardBanVe.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardBanVe.Size = new System.Drawing.Size(265, 125);
            this.btnCardBanVe.TabIndex = 1;
            this.btnCardBanVe.Text = "🎟️ BÁN VÉ POS QUẦY\r\n\r\nTạo đơn bán vé & dịch vụ, tính tiền, áp mã giảm giá";
            this.btnCardBanVe.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardBanVe.UseVisualStyleBackColor = false;
            this.btnCardBanVe.Click += new System.EventHandler(this.BtnCardBanVe_Click);
            // 
            // btnCardHoaDon
            // 
            this.btnCardHoaDon.BackColor = System.Drawing.Color.White;
            this.btnCardHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardHoaDon.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardHoaDon.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardHoaDon.Location = new System.Drawing.Point(590, 12);
            this.btnCardHoaDon.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardHoaDon.Name = "btnCardHoaDon";
            this.btnCardHoaDon.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardHoaDon.Size = new System.Drawing.Size(265, 125);
            this.btnCardHoaDon.TabIndex = 2;
            this.btnCardHoaDon.Text = "🧾 QUẢN LÝ HÓA ĐƠN\r\n\r\nLịch sử giao dịch, xem chi tiết, hủy & xuất hóa đơn";
            this.btnCardHoaDon.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardHoaDon.UseVisualStyleBackColor = false;
            this.btnCardHoaDon.Click += new System.EventHandler(this.BtnCardHoaDon_Click);
            // 
            // btnCardBaoCao
            // 
            this.btnCardBaoCao.BackColor = System.Drawing.Color.White;
            this.btnCardBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardBaoCao.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardBaoCao.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardBaoCao.Location = new System.Drawing.Point(879, 12);
            this.btnCardBaoCao.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardBaoCao.Name = "btnCardBaoCao";
            this.btnCardBaoCao.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardBaoCao.Size = new System.Drawing.Size(265, 125);
            this.btnCardBaoCao.TabIndex = 3;
            this.btnCardBaoCao.Text = "📈 BÁO CÁO DOANH THU\r\n\r\nThống kê theo ngày/tháng/năm, ClosedXML xuất báo cáo";
            this.btnCardBaoCao.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardBaoCao.UseVisualStyleBackColor = false;
            this.btnCardBaoCao.Click += new System.EventHandler(this.BtnCardBaoCao_Click);
            // 
            // btnCardTroChoi
            // 
            this.btnCardTroChoi.BackColor = System.Drawing.Color.White;
            this.btnCardTroChoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardTroChoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardTroChoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardTroChoi.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardTroChoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardTroChoi.Location = new System.Drawing.Point(12, 161);
            this.btnCardTroChoi.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardTroChoi.Name = "btnCardTroChoi";
            this.btnCardTroChoi.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardTroChoi.Size = new System.Drawing.Size(265, 125);
            this.btnCardTroChoi.TabIndex = 4;
            this.btnCardTroChoi.Text = "🎡 QUẢN LÝ TRÒ CHƠI\r\n\r\nDanh mục trò chơi, khu vực, trạng thái bảo trì & quy định";
            this.btnCardTroChoi.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardTroChoi.UseVisualStyleBackColor = false;
            this.btnCardTroChoi.Click += new System.EventHandler(this.BtnCardTroChoi_Click);
            // 
            // btnCardDichVu
            // 
            this.btnCardDichVu.BackColor = System.Drawing.Color.White;
            this.btnCardDichVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardDichVu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardDichVu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardDichVu.Location = new System.Drawing.Point(301, 161);
            this.btnCardDichVu.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardDichVu.Name = "btnCardDichVu";
            this.btnCardDichVu.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardDichVu.Size = new System.Drawing.Size(265, 125);
            this.btnCardDichVu.TabIndex = 5;
            this.btnCardDichVu.Text = "🍔 QUẢN LÝ DỊCH VỤ\r\n\r\nDịch vụ ăn uống, cho thuê thiết bị, quản lý tồn kho";
            this.btnCardDichVu.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardDichVu.UseVisualStyleBackColor = false;
            this.btnCardDichVu.Click += new System.EventHandler(this.BtnCardDichVu_Click);
            // 
            // btnCardVe
            // 
            this.btnCardVe.BackColor = System.Drawing.Color.White;
            this.btnCardVe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardVe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardVe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardVe.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardVe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardVe.Location = new System.Drawing.Point(590, 161);
            this.btnCardVe.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardVe.Name = "btnCardVe";
            this.btnCardVe.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardVe.Size = new System.Drawing.Size(265, 125);
            this.btnCardVe.TabIndex = 6;
            this.btnCardVe.Text = "🎫 QUẢN LÝ LOẠI VÉ\r\n\r\nCấu hình bảng giá vé tham quan, vé combo, trọn gói, VIP";
            this.btnCardVe.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardVe.UseVisualStyleBackColor = false;
            this.btnCardVe.Click += new System.EventHandler(this.BtnCardVe_Click);
            // 
            // btnCardKhachHang
            // 
            this.btnCardKhachHang.BackColor = System.Drawing.Color.White;
            this.btnCardKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardKhachHang.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardKhachHang.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardKhachHang.Location = new System.Drawing.Point(879, 161);
            this.btnCardKhachHang.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardKhachHang.Name = "btnCardKhachHang";
            this.btnCardKhachHang.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardKhachHang.Size = new System.Drawing.Size(265, 125);
            this.btnCardKhachHang.TabIndex = 7;
            this.btnCardKhachHang.Text = "👥 QUẢN LÝ KHÁCH HÀNG\r\n\r\nHồ sơ khách hàng, thẻ thành viên, điểm tích lũy";
            this.btnCardKhachHang.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardKhachHang.UseVisualStyleBackColor = false;
            this.btnCardKhachHang.Click += new System.EventHandler(this.BtnCardKhachHang_Click);
            // 
            // btnCardNhanVien
            // 
            this.btnCardNhanVien.BackColor = System.Drawing.Color.White;
            this.btnCardNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardNhanVien.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardNhanVien.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardNhanVien.Location = new System.Drawing.Point(12, 310);
            this.btnCardNhanVien.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardNhanVien.Name = "btnCardNhanVien";
            this.btnCardNhanVien.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardNhanVien.Size = new System.Drawing.Size(265, 125);
            this.btnCardNhanVien.TabIndex = 8;
            this.btnCardNhanVien.Text = "👔 QUẢN LÝ NHÂN VIÊN\r\n\r\nDanh sách nhân sự, phân quyền vai trò, ca làm việc";
            this.btnCardNhanVien.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardNhanVien.UseVisualStyleBackColor = false;
            this.btnCardNhanVien.Click += new System.EventHandler(this.BtnCardNhanVien_Click);
            // 
            // btnCardDangNhap
            // 
            this.btnCardDangNhap.BackColor = System.Drawing.Color.White;
            this.btnCardDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardDangNhap.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardDangNhap.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardDangNhap.Location = new System.Drawing.Point(301, 310);
            this.btnCardDangNhap.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardDangNhap.Name = "btnCardDangNhap";
            this.btnCardDangNhap.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardDangNhap.Size = new System.Drawing.Size(265, 125);
            this.btnCardDangNhap.TabIndex = 9;
            this.btnCardDangNhap.Text = "🔑 ĐĂNG NHẬP HỆ THỐNG\r\n\r\nMở cửa sổ đăng nhập độc lập để kiểm tra xác thực";
            this.btnCardDangNhap.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardDangNhap.UseVisualStyleBackColor = false;
            this.btnCardDangNhap.Click += new System.EventHandler(this.BtnCardDangNhap_Click);
            // 
            // btnCardDangKy
            // 
            this.btnCardDangKy.BackColor = System.Drawing.Color.White;
            this.btnCardDangKy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardDangKy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardDangKy.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardDangKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardDangKy.Location = new System.Drawing.Point(590, 310);
            this.btnCardDangKy.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardDangKy.Name = "btnCardDangKy";
            this.btnCardDangKy.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardDangKy.Size = new System.Drawing.Size(265, 125);
            this.btnCardDangKy.TabIndex = 10;
            this.btnCardDangKy.Text = "📝 ĐĂNG KÝ TÀI KHOẢN\r\n\r\nMở cửa sổ đăng ký tài khoản nhân viên / quản trị viên";
            this.btnCardDangKy.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardDangKy.UseVisualStyleBackColor = false;
            this.btnCardDangKy.Click += new System.EventHandler(this.BtnCardDangKy_Click);
            // 
            // btnCardQuenMatKhau
            // 
            this.btnCardQuenMatKhau.BackColor = System.Drawing.Color.White;
            this.btnCardQuenMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardQuenMatKhau.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardQuenMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardQuenMatKhau.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardQuenMatKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardQuenMatKhau.Location = new System.Drawing.Point(879, 310);
            this.btnCardQuenMatKhau.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardQuenMatKhau.Name = "btnCardQuenMatKhau";
            this.btnCardQuenMatKhau.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardQuenMatKhau.Size = new System.Drawing.Size(265, 125);
            this.btnCardQuenMatKhau.TabIndex = 11;
            this.btnCardQuenMatKhau.Text = "❓ QUÊN MẬT KHẨU\r\n\r\nKhôi phục tài khoản, xác thực mã OTP và đặt mật khẩu mới";
            this.btnCardQuenMatKhau.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardQuenMatKhau.UseVisualStyleBackColor = false;
            this.btnCardQuenMatKhau.Click += new System.EventHandler(this.BtnCardQuenMatKhau_Click);
            // 
            // btnCardImportKH
            // 
            this.btnCardImportKH.BackColor = System.Drawing.Color.White;
            this.btnCardImportKH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardImportKH.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardImportKH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardImportKH.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardImportKH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardImportKH.Location = new System.Drawing.Point(12, 459);
            this.btnCardImportKH.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardImportKH.Name = "btnCardImportKH";
            this.btnCardImportKH.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardImportKH.Size = new System.Drawing.Size(265, 125);
            this.btnCardImportKH.TabIndex = 12;
            this.btnCardImportKH.Text = "📥 NHẬP EXCEL KHÁCH HÀNG\r\n\r\nNạp danh sách khách hàng từ file ClosedXML Excel";
            this.btnCardImportKH.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardImportKH.UseVisualStyleBackColor = false;
            this.btnCardImportKH.Click += new System.EventHandler(this.BtnCardImportKH_Click);
            // 
            // btnCardExportKH
            // 
            this.btnCardExportKH.BackColor = System.Drawing.Color.White;
            this.btnCardExportKH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardExportKH.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCardExportKH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCardExportKH.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCardExportKH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnCardExportKH.Location = new System.Drawing.Point(301, 459);
            this.btnCardExportKH.Margin = new System.Windows.Forms.Padding(12);
            this.btnCardExportKH.Name = "btnCardExportKH";
            this.btnCardExportKH.Padding = new System.Windows.Forms.Padding(16);
            this.btnCardExportKH.Size = new System.Drawing.Size(265, 125);
            this.btnCardExportKH.TabIndex = 13;
            this.btnCardExportKH.Text = "📤 XUẤT EXCEL KHÁCH HÀNG\r\n\r\nXuất báo cáo ClosedXML có định dạng màu sắc";
            this.btnCardExportKH.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCardExportKH.UseVisualStyleBackColor = false;
            this.btnCardExportKH.Click += new System.EventHandler(this.BtnCardExportKH_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainMenuStrip = this.menuMain;
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FUNZONE - HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.flpModules.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem menuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem menuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem menuTroChoi;
        private System.Windows.Forms.ToolStripMenuItem menuDichVu;
        private System.Windows.Forms.ToolStripMenuItem menuVe;
        private System.Windows.Forms.ToolStripMenuItem menuNghiepVu;
        private System.Windows.Forms.ToolStripMenuItem menuBanVe;
        private System.Windows.Forms.ToolStripMenuItem menuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem menuDashboard;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCaoDoanhThu;
        private System.Windows.Forms.ToolStripMenuItem menuTienIch;
        private System.Windows.Forms.ToolStripMenuItem menuImportKH;
        private System.Windows.Forms.ToolStripMenuItem menuExportKH;
        private System.Windows.Forms.ToolStripMenuItem menuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblUserGreeting;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusReady;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusDb;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusVersion;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.FlowLayoutPanel flpModules;
        private System.Windows.Forms.Button btnCardDashboard;
        private System.Windows.Forms.Button btnCardBanVe;
        private System.Windows.Forms.Button btnCardHoaDon;
        private System.Windows.Forms.Button btnCardBaoCao;
        private System.Windows.Forms.Button btnCardTroChoi;
        private System.Windows.Forms.Button btnCardDichVu;
        private System.Windows.Forms.Button btnCardVe;
        private System.Windows.Forms.Button btnCardKhachHang;
        private System.Windows.Forms.Button btnCardNhanVien;
        private System.Windows.Forms.Button btnCardDangNhap;
        private System.Windows.Forms.Button btnCardDangKy;
        private System.Windows.Forms.Button btnCardQuenMatKhau;
        private System.Windows.Forms.Button btnCardImportKH;
        private System.Windows.Forms.Button btnCardExportKH;
    }
}
