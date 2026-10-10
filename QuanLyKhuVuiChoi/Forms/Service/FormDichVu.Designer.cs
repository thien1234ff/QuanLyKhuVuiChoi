namespace QuanLyKhuVuiChoi.Forms.Service
{
    partial class FormDichVu
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.btnPlusStock = new System.Windows.Forms.Button();
            this.numSoLuongTon = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.numDonGia = new System.Windows.Forms.NumericUpDown();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.cboLoaiDichVu = new System.Windows.Forms.ComboBox();
            this.lblLoaiDichVu = new System.Windows.Forms.Label();
            this.txtTenDichVu = new System.Windows.Forms.TextBox();
            this.lblTenDichVu = new System.Windows.Forms.Label();
            this.btnAutoId = new System.Windows.Forms.Button();
            this.txtMaDichVu = new System.Windows.Forms.TextBox();
            this.lblMaDichVu = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.cboFilterTrangThai = new System.Windows.Forms.ComboBox();
            this.lblFilterTrangThai = new System.Windows.Forms.Label();
            this.cboFilterLoai = new System.Windows.Forms.ComboBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).BeginInit();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblTotal);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlHeader.Size = new System.Drawing.Size(1150, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTotal.Location = new System.Drawing.Point(920, 20);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(163, 21);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Tổng số dịch vụ: 0";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(529, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "☕ QUẢN LÝ DANH MỤC DỊCH VỤ & TIỆN ÍCH";
            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.White;
            this.pnlInput.Controls.Add(this.btnLamMoi);
            this.pnlInput.Controls.Add(this.btnXoa);
            this.pnlInput.Controls.Add(this.btnSua);
            this.pnlInput.Controls.Add(this.btnThem);
            this.pnlInput.Controls.Add(this.txtMoTa);
            this.pnlInput.Controls.Add(this.lblMoTa);
            this.pnlInput.Controls.Add(this.cboTrangThai);
            this.pnlInput.Controls.Add(this.lblTrangThai);
            this.pnlInput.Controls.Add(this.btnPlusStock);
            this.pnlInput.Controls.Add(this.numSoLuongTon);
            this.pnlInput.Controls.Add(this.lblSoLuongTon);
            this.pnlInput.Controls.Add(this.numDonGia);
            this.pnlInput.Controls.Add(this.lblDonGia);
            this.pnlInput.Controls.Add(this.cboLoaiDichVu);
            this.pnlInput.Controls.Add(this.lblLoaiDichVu);
            this.pnlInput.Controls.Add(this.txtTenDichVu);
            this.pnlInput.Controls.Add(this.lblTenDichVu);
            this.pnlInput.Controls.Add(this.btnAutoId);
            this.pnlInput.Controls.Add(this.txtMaDichVu);
            this.pnlInput.Controls.Add(this.lblMaDichVu);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInput.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.pnlInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlInput.Location = new System.Drawing.Point(0, 60);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(15);
            this.pnlInput.Size = new System.Drawing.Size(375, 640);
            this.pnlInput.TabIndex = 1;
            this.pnlInput.TabStop = false;
            this.pnlInput.Text = "Thông Tin Dịch Vụ";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnLamMoi.Location = new System.Drawing.Point(15, 475);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(330, 32);
            this.btnLamMoi.TabIndex = 19;
            this.btnLamMoi.Text = "🔄 Làm Mới Dữ Liệu";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(245, 430);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 34);
            this.btnXoa.TabIndex = 18;
            this.btnXoa.Text = "🗑️ Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(130, 430);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 34);
            this.btnSua.TabIndex = 17;
            this.btnSua.Text = "✏️ Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(15, 430);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 34);
            this.btnThem.TabIndex = 16;
            this.btnThem.Text = "➕ Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMoTa.Location = new System.Drawing.Point(135, 305);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(210, 55);
            this.txtMoTa.TabIndex = 15;
            // 
            // lblMoTa
            // 
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMoTa.Location = new System.Drawing.Point(15, 308);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(110, 20);
            this.lblMoTa.TabIndex = 14;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Đang phục vụ",
            "Hết hàng",
            "Tạm ngưng"});
            this.cboTrangThai.Location = new System.Drawing.Point(135, 265);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(210, 28);
            this.cboTrangThai.TabIndex = 13;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThai.Location = new System.Drawing.Point(15, 268);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(110, 20);
            this.lblTrangThai.TabIndex = 12;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // btnPlusStock
            // 
            this.btnPlusStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnPlusStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlusStock.FlatAppearance.BorderSize = 0;
            this.btnPlusStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlusStock.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnPlusStock.ForeColor = System.Drawing.Color.White;
            this.btnPlusStock.Location = new System.Drawing.Point(265, 225);
            this.btnPlusStock.Name = "btnPlusStock";
            this.btnPlusStock.Size = new System.Drawing.Size(80, 28);
            this.btnPlusStock.TabIndex = 11;
            this.btnPlusStock.Text = "+50 Tồn";
            this.btnPlusStock.UseVisualStyleBackColor = false;
            this.btnPlusStock.Click += new System.EventHandler(this.BtnPlusStock_Click);
            // 
            // numSoLuongTon
            // 
            this.numSoLuongTon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numSoLuongTon.Location = new System.Drawing.Point(135, 225);
            this.numSoLuongTon.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSoLuongTon.Name = "numSoLuongTon";
            this.numSoLuongTon.Size = new System.Drawing.Size(120, 27);
            this.numSoLuongTon.TabIndex = 10;
            this.numSoLuongTon.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblSoLuongTon
            // 
            this.lblSoLuongTon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoLuongTon.Location = new System.Drawing.Point(15, 228);
            this.lblSoLuongTon.Name = "lblSoLuongTon";
            this.lblSoLuongTon.Size = new System.Drawing.Size(110, 20);
            this.lblSoLuongTon.TabIndex = 9;
            this.lblSoLuongTon.Text = "Số lượng tồn:";
            // 
            // numDonGia
            // 
            this.numDonGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numDonGia.Increment = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDonGia.Location = new System.Drawing.Point(135, 185);
            this.numDonGia.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numDonGia.Name = "numDonGia";
            this.numDonGia.Size = new System.Drawing.Size(210, 27);
            this.numDonGia.TabIndex = 8;
            this.numDonGia.ThousandsSeparator = true;
            // 
            // lblDonGia
            // 
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDonGia.Location = new System.Drawing.Point(15, 188);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(110, 20);
            this.lblDonGia.TabIndex = 7;
            this.lblDonGia.Text = "Đơn giá (VNĐ):";
            // 
            // cboLoaiDichVu
            // 
            this.cboLoaiDichVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDichVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoaiDichVu.FormattingEnabled = true;
            this.cboLoaiDichVu.Items.AddRange(new object[] {
            "Ẩm thực",
            "Nước uống",
            "Thuê đồ",
            "Khu nghỉ ngơi",
            "Ăn vặt",
            "Dịch vụ khác"});
            this.cboLoaiDichVu.Location = new System.Drawing.Point(135, 145);
            this.cboLoaiDichVu.Name = "cboLoaiDichVu";
            this.cboLoaiDichVu.Size = new System.Drawing.Size(210, 28);
            this.cboLoaiDichVu.TabIndex = 6;
            // 
            // lblLoaiDichVu
            // 
            this.lblLoaiDichVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoaiDichVu.Location = new System.Drawing.Point(15, 148);
            this.lblLoaiDichVu.Name = "lblLoaiDichVu";
            this.lblLoaiDichVu.Size = new System.Drawing.Size(110, 20);
            this.lblLoaiDichVu.TabIndex = 5;
            this.lblLoaiDichVu.Text = "Loại dịch vụ:";
            // 
            // txtTenDichVu
            // 
            this.txtTenDichVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDichVu.Location = new System.Drawing.Point(135, 105);
            this.txtTenDichVu.Name = "txtTenDichVu";
            this.txtTenDichVu.Size = new System.Drawing.Size(210, 27);
            this.txtTenDichVu.TabIndex = 4;
            // 
            // lblTenDichVu
            // 
            this.lblTenDichVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenDichVu.Location = new System.Drawing.Point(15, 108);
            this.lblTenDichVu.Name = "lblTenDichVu";
            this.lblTenDichVu.Size = new System.Drawing.Size(110, 20);
            this.lblTenDichVu.TabIndex = 3;
            this.lblTenDichVu.Text = "Tên dịch vụ (*):";
            // 
            // btnAutoId
            // 
            this.btnAutoId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnAutoId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoId.FlatAppearance.BorderSize = 0;
            this.btnAutoId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAutoId.ForeColor = System.Drawing.Color.White;
            this.btnAutoId.Location = new System.Drawing.Point(275, 63);
            this.btnAutoId.Name = "btnAutoId";
            this.btnAutoId.Size = new System.Drawing.Size(70, 28);
            this.btnAutoId.TabIndex = 2;
            this.btnAutoId.Text = "Mới";
            this.btnAutoId.UseVisualStyleBackColor = false;
            this.btnAutoId.Click += new System.EventHandler(this.BtnAutoId_Click);
            // 
            // txtMaDichVu
            // 
            this.txtMaDichVu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.txtMaDichVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaDichVu.Location = new System.Drawing.Point(135, 64);
            this.txtMaDichVu.Name = "txtMaDichVu";
            this.txtMaDichVu.ReadOnly = true;
            this.txtMaDichVu.Size = new System.Drawing.Size(130, 27);
            this.txtMaDichVu.TabIndex = 1;
            // 
            // lblMaDichVu
            // 
            this.lblMaDichVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaDichVu.Location = new System.Drawing.Point(15, 67);
            this.lblMaDichVu.Name = "lblMaDichVu";
            this.lblMaDichVu.Size = new System.Drawing.Size(110, 20);
            this.lblMaDichVu.TabIndex = 0;
            this.lblMaDichVu.Text = "Mã dịch vụ:";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dgvDichVu);
            this.pnlRight.Controls.Add(this.pnlFilter);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(375, 60);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15, 10, 15, 15);
            this.pnlRight.Size = new System.Drawing.Size(775, 640);
            this.pnlRight.TabIndex = 2;
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDichVu.Location = new System.Drawing.Point(15, 62);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.ReadOnly = true;
            this.dgvDichVu.RowHeadersVisible = false;
            this.dgvDichVu.RowHeadersWidth = 51;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(745, 563);
            this.dgvDichVu.TabIndex = 1;
            this.dgvDichVu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDichVu_CellClick);
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.btnReset);
            this.pnlFilter.Controls.Add(this.cboFilterTrangThai);
            this.pnlFilter.Controls.Add(this.lblFilterTrangThai);
            this.pnlFilter.Controls.Add(this.cboFilterLoai);
            this.pnlFilter.Controls.Add(this.lblLoai);
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(15, 10);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(745, 52);
            this.pnlFilter.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnReset.Location = new System.Drawing.Point(655, 10);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 30);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Đặt lại";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // cboFilterTrangThai
            // 
            this.cboFilterTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboFilterTrangThai.FormattingEnabled = true;
            this.cboFilterTrangThai.Items.AddRange(new object[] {
            "Tất cả",
            "Đang phục vụ",
            "Hết hàng",
            "Tạm ngưng"});
            this.cboFilterTrangThai.Location = new System.Drawing.Point(520, 11);
            this.cboFilterTrangThai.Name = "cboFilterTrangThai";
            this.cboFilterTrangThai.Size = new System.Drawing.Size(125, 28);
            this.cboFilterTrangThai.TabIndex = 5;
            this.cboFilterTrangThai.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblFilterTrangThai
            // 
            this.lblFilterTrangThai.AutoSize = true;
            this.lblFilterTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterTrangThai.Location = new System.Drawing.Point(440, 15);
            this.lblFilterTrangThai.Name = "lblFilterTrangThai";
            this.lblFilterTrangThai.Size = new System.Drawing.Size(78, 20);
            this.lblFilterTrangThai.TabIndex = 4;
            this.lblFilterTrangThai.Text = "Trạng thái:";
            // 
            // cboFilterLoai
            // 
            this.cboFilterLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterLoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboFilterLoai.FormattingEnabled = true;
            this.cboFilterLoai.Items.AddRange(new object[] {
            "Tất cả",
            "Ẩm thực",
            "Nước uống",
            "Thuê đồ",
            "Khu nghỉ ngơi",
            "Ăn vặt",
            "Dịch vụ khác"});
            this.cboFilterLoai.Location = new System.Drawing.Point(300, 11);
            this.cboFilterLoai.Name = "cboFilterLoai";
            this.cboFilterLoai.Size = new System.Drawing.Size(130, 28);
            this.cboFilterLoai.TabIndex = 3;
            this.cboFilterLoai.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoai.Location = new System.Drawing.Point(260, 15);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(42, 20);
            this.lblLoai.TabIndex = 2;
            this.lblLoai.Text = "Loại:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(85, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(165, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Location = new System.Drawing.Point(12, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(70, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // FormDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1150, 700);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormDichVu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ DỊCH VỤ & TIỆN ÍCH";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).EndInit();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox pnlInput;
        private System.Windows.Forms.Label lblMaDichVu;
        private System.Windows.Forms.TextBox txtMaDichVu;
        private System.Windows.Forms.Button btnAutoId;
        private System.Windows.Forms.Label lblTenDichVu;
        private System.Windows.Forms.TextBox txtTenDichVu;
        private System.Windows.Forms.Label lblLoaiDichVu;
        private System.Windows.Forms.ComboBox cboLoaiDichVu;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.NumericUpDown numDonGia;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.NumericUpDown numSoLuongTon;
        private System.Windows.Forms.Button btnPlusStock;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboFilterLoai;
        private System.Windows.Forms.Label lblFilterTrangThai;
        private System.Windows.Forms.ComboBox cboFilterTrangThai;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView dgvDichVu;
    }
}
