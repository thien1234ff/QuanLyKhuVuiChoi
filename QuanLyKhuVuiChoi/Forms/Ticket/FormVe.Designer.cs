namespace QuanLyKhuVuiChoi.Forms.Ticket
{
    partial class FormVe
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
            this.txtThoiHan = new System.Windows.Forms.TextBox();
            this.lblThoiHan = new System.Windows.Forms.Label();
            this.numGiaVe = new System.Windows.Forms.NumericUpDown();
            this.lblGiaVe = new System.Windows.Forms.Label();
            this.cboLoaiVe = new System.Windows.Forms.ComboBox();
            this.lblLoaiVe = new System.Windows.Forms.Label();
            this.txtTenVe = new System.Windows.Forms.TextBox();
            this.lblTenVe = new System.Windows.Forms.Label();
            this.btnAutoId = new System.Windows.Forms.Button();
            this.txtMaVe = new System.Windows.Forms.TextBox();
            this.lblMaVe = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvVe = new System.Windows.Forms.DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.numGiaVe)).BeginInit();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVe)).BeginInit();
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
            this.lblTotal.Size = new System.Drawing.Size(161, 21);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Tổng số loại vé: 0";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(85)))), ((int)(((byte)(247)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(437, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🎟️ QUẢN LÝ BẢNG GIÁ & LOẠI VÉ VÀO";
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
            this.pnlInput.Controls.Add(this.txtThoiHan);
            this.pnlInput.Controls.Add(this.lblThoiHan);
            this.pnlInput.Controls.Add(this.numGiaVe);
            this.pnlInput.Controls.Add(this.lblGiaVe);
            this.pnlInput.Controls.Add(this.cboLoaiVe);
            this.pnlInput.Controls.Add(this.lblLoaiVe);
            this.pnlInput.Controls.Add(this.txtTenVe);
            this.pnlInput.Controls.Add(this.lblTenVe);
            this.pnlInput.Controls.Add(this.btnAutoId);
            this.pnlInput.Controls.Add(this.txtMaVe);
            this.pnlInput.Controls.Add(this.lblMaVe);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInput.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.pnlInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlInput.Location = new System.Drawing.Point(0, 60);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(15);
            this.pnlInput.Size = new System.Drawing.Size(375, 640);
            this.pnlInput.TabIndex = 1;
            this.pnlInput.TabStop = false;
            this.pnlInput.Text = "Thông Tin Vé";
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
            this.btnLamMoi.TabIndex = 18;
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
            this.btnXoa.TabIndex = 17;
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
            this.btnSua.TabIndex = 16;
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
            this.btnThem.TabIndex = 15;
            this.btnThem.Text = "➕ Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMoTa.Location = new System.Drawing.Point(135, 315);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(210, 55);
            this.txtMoTa.TabIndex = 14;
            // 
            // lblMoTa
            // 
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMoTa.Location = new System.Drawing.Point(15, 318);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(110, 20);
            this.lblMoTa.TabIndex = 13;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Đang áp dụng",
            "Ngừng áp dụng"});
            this.cboTrangThai.Location = new System.Drawing.Point(135, 275);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(210, 28);
            this.cboTrangThai.TabIndex = 12;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThai.Location = new System.Drawing.Point(15, 278);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(110, 20);
            this.lblTrangThai.TabIndex = 11;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtThoiHan
            // 
            this.txtThoiHan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtThoiHan.Location = new System.Drawing.Point(135, 235);
            this.txtThoiHan.Name = "txtThoiHan";
            this.txtThoiHan.Size = new System.Drawing.Size(210, 27);
            this.txtThoiHan.TabIndex = 10;
            this.txtThoiHan.Text = "Trong ngày";
            // 
            // lblThoiHan
            // 
            this.lblThoiHan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThoiHan.Location = new System.Drawing.Point(15, 238);
            this.lblThoiHan.Name = "lblThoiHan";
            this.lblThoiHan.Size = new System.Drawing.Size(110, 20);
            this.lblThoiHan.TabIndex = 9;
            this.lblThoiHan.Text = "Thời hạn:";
            // 
            // numGiaVe
            // 
            this.numGiaVe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numGiaVe.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numGiaVe.Location = new System.Drawing.Point(135, 195);
            this.numGiaVe.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numGiaVe.Name = "numGiaVe";
            this.numGiaVe.Size = new System.Drawing.Size(210, 27);
            this.numGiaVe.TabIndex = 8;
            this.numGiaVe.ThousandsSeparator = true;
            // 
            // lblGiaVe
            // 
            this.lblGiaVe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiaVe.Location = new System.Drawing.Point(15, 198);
            this.lblGiaVe.Name = "lblGiaVe";
            this.lblGiaVe.Size = new System.Drawing.Size(110, 20);
            this.lblGiaVe.TabIndex = 7;
            this.lblGiaVe.Text = "Giá vé (VNĐ):";
            // 
            // cboLoaiVe
            // 
            this.cboLoaiVe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiVe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoaiVe.FormattingEnabled = true;
            this.cboLoaiVe.Items.AddRange(new object[] {
            "Vé ngày",
            "Vé trẻ em",
            "Vé người lớn",
            "Vé gia đình",
            "Vé VIP"});
            this.cboLoaiVe.Location = new System.Drawing.Point(135, 150);
            this.cboLoaiVe.Name = "cboLoaiVe";
            this.cboLoaiVe.Size = new System.Drawing.Size(210, 28);
            this.cboLoaiVe.TabIndex = 6;
            // 
            // lblLoaiVe
            // 
            this.lblLoaiVe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoaiVe.Location = new System.Drawing.Point(15, 153);
            this.lblLoaiVe.Name = "lblLoaiVe";
            this.lblLoaiVe.Size = new System.Drawing.Size(110, 20);
            this.lblLoaiVe.TabIndex = 5;
            this.lblLoaiVe.Text = "Loại vé:";
            // 
            // txtTenVe
            // 
            this.txtTenVe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenVe.Location = new System.Drawing.Point(135, 108);
            this.txtTenVe.Name = "txtTenVe";
            this.txtTenVe.Size = new System.Drawing.Size(210, 27);
            this.txtTenVe.TabIndex = 4;
            // 
            // lblTenVe
            // 
            this.lblTenVe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenVe.Location = new System.Drawing.Point(15, 111);
            this.lblTenVe.Name = "lblTenVe";
            this.lblTenVe.Size = new System.Drawing.Size(110, 20);
            this.lblTenVe.TabIndex = 3;
            this.lblTenVe.Text = "Tên vé (*):";
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
            // txtMaVe
            // 
            this.txtMaVe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.txtMaVe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaVe.Location = new System.Drawing.Point(135, 64);
            this.txtMaVe.Name = "txtMaVe";
            this.txtMaVe.ReadOnly = true;
            this.txtMaVe.Size = new System.Drawing.Size(130, 27);
            this.txtMaVe.TabIndex = 1;
            // 
            // lblMaVe
            // 
            this.lblMaVe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaVe.Location = new System.Drawing.Point(15, 67);
            this.lblMaVe.Name = "lblMaVe";
            this.lblMaVe.Size = new System.Drawing.Size(110, 20);
            this.lblMaVe.TabIndex = 0;
            this.lblMaVe.Text = "Mã vé:";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dgvVe);
            this.pnlRight.Controls.Add(this.pnlFilter);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(375, 60);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15, 10, 15, 15);
            this.pnlRight.Size = new System.Drawing.Size(775, 640);
            this.pnlRight.TabIndex = 2;
            // 
            // dgvVe
            // 
            this.dgvVe.AllowUserToAddRows = false;
            this.dgvVe.AllowUserToDeleteRows = false;
            this.dgvVe.BackgroundColor = System.Drawing.Color.White;
            this.dgvVe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVe.Location = new System.Drawing.Point(15, 62);
            this.dgvVe.Name = "dgvVe";
            this.dgvVe.ReadOnly = true;
            this.dgvVe.RowHeadersVisible = false;
            this.dgvVe.RowHeadersWidth = 51;
            this.dgvVe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVe.Size = new System.Drawing.Size(745, 563);
            this.dgvVe.TabIndex = 1;
            this.dgvVe.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVe_CellClick);
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
            "Đang áp dụng",
            "Ngừng áp dụng"});
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
            "Vé ngày",
            "Vé trẻ em",
            "Vé người lớn",
            "Vé gia đình",
            "Vé VIP"});
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
            this.lblLoai.Location = new System.Drawing.Point(240, 15);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(60, 20);
            this.lblLoai.TabIndex = 2;
            this.lblLoai.Text = "Loại vé:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(85, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(145, 27);
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
            // FormVe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1150, 700);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormVe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ LOẠI VÉ VÀO CỔNG";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaVe)).EndInit();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVe)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox pnlInput;
        private System.Windows.Forms.Label lblMaVe;
        private System.Windows.Forms.TextBox txtMaVe;
        private System.Windows.Forms.Button btnAutoId;
        private System.Windows.Forms.Label lblTenVe;
        private System.Windows.Forms.TextBox txtTenVe;
        private System.Windows.Forms.Label lblLoaiVe;
        private System.Windows.Forms.ComboBox cboLoaiVe;
        private System.Windows.Forms.Label lblGiaVe;
        private System.Windows.Forms.NumericUpDown numGiaVe;
        private System.Windows.Forms.Label lblThoiHan;
        private System.Windows.Forms.TextBox txtThoiHan;
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
        private System.Windows.Forms.DataGridView dgvVe;
    }
}
