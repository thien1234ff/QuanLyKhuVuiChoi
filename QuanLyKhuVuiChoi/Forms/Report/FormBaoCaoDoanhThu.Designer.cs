namespace QuanLyKhuVuiChoi.Forms.Report
{
    partial class FormBaoCaoDoanhThu
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.cboKyBaoCao = new System.Windows.Forms.ComboBox();
            this.lblKy = new System.Windows.Forms.Label();
            this.pnlKpi = new System.Windows.Forms.Panel();
            this.pnlKpi3 = new System.Windows.Forms.Panel();
            this.lblTongHoaDon = new System.Windows.Forms.Label();
            this.lblTitleKpi3 = new System.Windows.Forms.Label();
            this.pnlKpi2 = new System.Windows.Forms.Panel();
            this.lblTongVeBan = new System.Windows.Forms.Label();
            this.lblTitleKpi2 = new System.Windows.Forms.Label();
            this.pnlKpi1 = new System.Windows.Forms.Panel();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.lblTitleKpi1 = new System.Windows.Forms.Label();
            this.split = new System.Windows.Forms.SplitContainer();
            this.grpTimeline = new System.Windows.Forms.GroupBox();
            this.dgvRevenue = new System.Windows.Forms.DataGridView();
            this.grpCategory = new System.Windows.Forms.GroupBox();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.pnlCatSwitch = new System.Windows.Forms.Panel();
            this.cboCategoryType = new System.Windows.Forms.ComboBox();
            this.lblCatSelect = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlKpi.SuspendLayout();
            this.pnlKpi3.SuspendLayout();
            this.pnlKpi2.SuspendLayout();
            this.pnlKpi1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.grpTimeline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).BeginInit();
            this.grpCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).BeginInit();
            this.pnlCatSwitch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 10);
            this.pnlHeader.Size = new System.Drawing.Size(1180, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(650, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 BÁO CÁO THỐNG KÊ DOANH THU KHU VUI CHƠI GIẢI TRÍ";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.btnExport);
            this.pnlFilter.Controls.Add(this.btnBaoCao);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.lblTo);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.lblFrom);
            this.pnlFilter.Controls.Add(this.cboKyBaoCao);
            this.pnlFilter.Controls.Add(this.lblKy);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlFilter.Size = new System.Drawing.Size(1180, 65);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(820, 16);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 30);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "📤 Xuất Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBaoCao.FlatAppearance.BorderSize = 0;
            this.btnBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnBaoCao.Location = new System.Drawing.Point(670, 16);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(135, 30);
            this.btnBaoCao.TabIndex = 6;
            this.btnBaoCao.Text = "📊 Xem Báo Cáo";
            this.btnBaoCao.UseVisualStyleBackColor = false;
            this.btnBaoCao.Click += new System.EventHandler(this.BtnBaoCao_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(520, 18);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(125, 27);
            this.dtpTo.TabIndex = 5;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTo.Location = new System.Drawing.Point(440, 21);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(78, 20);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "Đến ngày:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(300, 18);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(125, 27);
            this.dtpFrom.TabIndex = 3;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFrom.Location = new System.Drawing.Point(230, 21);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(69, 20);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "Từ ngày:";
            // 
            // cboKyBaoCao
            // 
            this.cboKyBaoCao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKyBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboKyBaoCao.FormattingEnabled = true;
            this.cboKyBaoCao.Items.AddRange(new object[] {
            "Theo Ngày",
            "Theo Tháng",
            "Theo Năm"});
            this.cboKyBaoCao.Location = new System.Drawing.Point(105, 18);
            this.cboKyBaoCao.Name = "cboKyBaoCao";
            this.cboKyBaoCao.Size = new System.Drawing.Size(110, 28);
            this.cboKyBaoCao.TabIndex = 1;
            this.cboKyBaoCao.SelectedIndexChanged += new System.EventHandler(this.CboKyBaoCao_SelectedIndexChanged);
            // 
            // lblKy
            // 
            this.lblKy.AutoSize = true;
            this.lblKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKy.Location = new System.Drawing.Point(15, 21);
            this.lblKy.Name = "lblKy";
            this.lblKy.Size = new System.Drawing.Size(87, 20);
            this.lblKy.TabIndex = 0;
            this.lblKy.Text = "Kỳ báo cáo:";
            // 
            // pnlKpi
            // 
            this.pnlKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlKpi.Controls.Add(this.pnlKpi3);
            this.pnlKpi2.Location = new System.Drawing.Point(395, 8);
            this.pnlKpi.Controls.Add(this.pnlKpi2);
            this.pnlKpi1.Location = new System.Drawing.Point(20, 8);
            this.pnlKpi.Controls.Add(this.pnlKpi1);
            this.pnlKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpi.Location = new System.Drawing.Point(0, 125);
            this.pnlKpi.Name = "pnlKpi";
            this.pnlKpi.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.pnlKpi.Size = new System.Drawing.Size(1180, 85);
            this.pnlKpi.TabIndex = 2;
            // 
            // pnlKpi3
            // 
            this.pnlKpi3.BackColor = System.Drawing.Color.White;
            this.pnlKpi3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi3.Controls.Add(this.lblTongHoaDon);
            this.pnlKpi3.Controls.Add(this.lblTitleKpi3);
            this.pnlKpi3.Location = new System.Drawing.Point(770, 8);
            this.pnlKpi3.Name = "pnlKpi3";
            this.pnlKpi3.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlKpi3.Size = new System.Drawing.Size(350, 70);
            this.pnlKpi3.TabIndex = 2;
            // 
            // lblTongHoaDon
            // 
            this.lblTongHoaDon.AutoSize = true;
            this.lblTongHoaDon.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTongHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTongHoaDon.Location = new System.Drawing.Point(15, 30);
            this.lblTongHoaDon.Name = "lblTongHoaDon";
            this.lblTongHoaDon.Size = new System.Drawing.Size(73, 35);
            this.lblTongHoaDon.TabIndex = 1;
            this.lblTongHoaDon.Text = "0 HĐ";
            // 
            // lblTitleKpi3
            // 
            this.lblTitleKpi3.AutoSize = true;
            this.lblTitleKpi3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleKpi3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblTitleKpi3.Location = new System.Drawing.Point(15, 8);
            this.lblTitleKpi3.Name = "lblTitleKpi3";
            this.lblTitleKpi3.Size = new System.Drawing.Size(227, 20);
            this.lblTitleKpi3.TabIndex = 0;
            this.lblTitleKpi3.Text = "📋 TỔNG SỐ HÓA ĐƠN ĐÃ LẬP";
            // 
            // pnlKpi2
            // 
            this.pnlKpi2.BackColor = System.Drawing.Color.White;
            this.pnlKpi2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi2.Controls.Add(this.lblTongVeBan);
            this.pnlKpi2.Controls.Add(this.lblTitleKpi2);
            this.pnlKpi2.Name = "pnlKpi2";
            this.pnlKpi2.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlKpi2.Size = new System.Drawing.Size(350, 70);
            this.pnlKpi2.TabIndex = 1;
            // 
            // lblTongVeBan
            // 
            this.lblTongVeBan.AutoSize = true;
            this.lblTongVeBan.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTongVeBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTongVeBan.Location = new System.Drawing.Point(15, 30);
            this.lblTongVeBan.Name = "lblTongVeBan";
            this.lblTongVeBan.Size = new System.Drawing.Size(65, 35);
            this.lblTongVeBan.TabIndex = 1;
            this.lblTongVeBan.Text = "0 vé";
            // 
            // lblTitleKpi2
            // 
            this.lblTitleKpi2.AutoSize = true;
            this.lblTitleKpi2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleKpi2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTitleKpi2.Location = new System.Drawing.Point(15, 8);
            this.lblTitleKpi2.Name = "lblTitleKpi2";
            this.lblTitleKpi2.Size = new System.Drawing.Size(186, 20);
            this.lblTitleKpi2.TabIndex = 0;
            this.lblTitleKpi2.Text = "🎟️ TỔNG SỐ VÉ ĐÃ BÁN";
            // 
            // pnlKpi1
            // 
            this.pnlKpi1.BackColor = System.Drawing.Color.White;
            this.pnlKpi1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi1.Controls.Add(this.lblTongDoanhThu);
            this.pnlKpi1.Controls.Add(this.lblTitleKpi1);
            this.pnlKpi1.Name = "pnlKpi1";
            this.pnlKpi1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlKpi1.Size = new System.Drawing.Size(350, 70);
            this.pnlKpi1.TabIndex = 0;
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblTongDoanhThu.Location = new System.Drawing.Point(15, 30);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(89, 35);
            this.lblTongDoanhThu.TabIndex = 1;
            this.lblTongDoanhThu.Text = "0 VNĐ";
            // 
            // lblTitleKpi1
            // 
            this.lblTitleKpi1.AutoSize = true;
            this.lblTitleKpi1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleKpi1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblTitleKpi1.Location = new System.Drawing.Point(15, 8);
            this.lblTitleKpi1.Name = "lblTitleKpi1";
            this.lblTitleKpi1.Size = new System.Drawing.Size(262, 20);
            this.lblTitleKpi1.TabIndex = 0;
            this.lblTitleKpi1.Text = "💰 TỔNG DOANH THU KỲ BÁO CÁO";
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 210);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.grpTimeline);
            this.split.Panel1.Padding = new System.Windows.Forms.Padding(15, 10, 10, 15);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.grpCategory);
            this.split.Panel2.Padding = new System.Windows.Forms.Padding(10, 10, 15, 15);
            this.split.Size = new System.Drawing.Size(1180, 510);
            this.split.SplitterDistance = 680;
            this.split.TabIndex = 3;
            // 
            // grpTimeline
            // 
            this.grpTimeline.BackColor = System.Drawing.Color.White;
            this.grpTimeline.Controls.Add(this.dgvRevenue);
            this.grpTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTimeline.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTimeline.Location = new System.Drawing.Point(15, 10);
            this.grpTimeline.Name = "grpTimeline";
            this.grpTimeline.Padding = new System.Windows.Forms.Padding(10);
            this.grpTimeline.Size = new System.Drawing.Size(655, 485);
            this.grpTimeline.TabIndex = 0;
            this.grpTimeline.TabStop = false;
            this.grpTimeline.Text = "Doanh Thu Chi Tiết Theo Thời Gian";
            // 
            // dgvRevenue
            // 
            this.dgvRevenue.AllowUserToAddRows = false;
            this.dgvRevenue.AllowUserToDeleteRows = false;
            this.dgvRevenue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRevenue.BackgroundColor = System.Drawing.Color.White;
            this.dgvRevenue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRevenue.ColumnHeadersHeight = 36;
            this.dgvRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRevenue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvRevenue.Location = new System.Drawing.Point(10, 33);
            this.dgvRevenue.Name = "dgvRevenue";
            this.dgvRevenue.ReadOnly = true;
            this.dgvRevenue.RowHeadersVisible = false;
            this.dgvRevenue.RowHeadersWidth = 51;
            this.dgvRevenue.RowTemplate.Height = 32;
            this.dgvRevenue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRevenue.Size = new System.Drawing.Size(635, 442);
            this.dgvRevenue.TabIndex = 0;
            // 
            // grpCategory
            // 
            this.grpCategory.BackColor = System.Drawing.Color.White;
            this.grpCategory.Controls.Add(this.dgvCategory);
            this.grpCategory.Controls.Add(this.pnlCatSwitch);
            this.grpCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpCategory.Location = new System.Drawing.Point(10, 10);
            this.grpCategory.Name = "grpCategory";
            this.grpCategory.Padding = new System.Windows.Forms.Padding(10);
            this.grpCategory.Size = new System.Drawing.Size(471, 485);
            this.grpCategory.TabIndex = 0;
            this.grpCategory.TabStop = false;
            this.grpCategory.Text = "Doanh Thu Theo Hạng Mục";
            // 
            // dgvCategory
            // 
            this.dgvCategory.AllowUserToAddRows = false;
            this.dgvCategory.AllowUserToDeleteRows = false;
            this.dgvCategory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategory.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategory.ColumnHeadersHeight = 36;
            this.dgvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvCategory.Location = new System.Drawing.Point(10, 75);
            this.dgvCategory.Name = "dgvCategory";
            this.dgvCategory.ReadOnly = true;
            this.dgvCategory.RowHeadersVisible = false;
            this.dgvCategory.RowHeadersWidth = 51;
            this.dgvCategory.RowTemplate.Height = 32;
            this.dgvCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategory.Size = new System.Drawing.Size(451, 400);
            this.dgvCategory.TabIndex = 1;
            // 
            // pnlCatSwitch
            // 
            this.pnlCatSwitch.Controls.Add(this.cboCategoryType);
            this.pnlCatSwitch.Controls.Add(this.lblCatSelect);
            this.pnlCatSwitch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCatSwitch.Location = new System.Drawing.Point(10, 33);
            this.pnlCatSwitch.Name = "pnlCatSwitch";
            this.pnlCatSwitch.Size = new System.Drawing.Size(451, 42);
            this.pnlCatSwitch.TabIndex = 0;
            // 
            // cboCategoryType
            // 
            this.cboCategoryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoryType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboCategoryType.FormattingEnabled = true;
            this.cboCategoryType.Items.AddRange(new object[] {
            "Tất cả",
            "Vé",
            "Dịch Vụ",
            "Trò Chơi"});
            this.cboCategoryType.Location = new System.Drawing.Point(85, 6);
            this.cboCategoryType.Name = "cboCategoryType";
            this.cboCategoryType.Size = new System.Drawing.Size(140, 28);
            this.cboCategoryType.TabIndex = 1;
            this.cboCategoryType.SelectedIndexChanged += new System.EventHandler(this.CboCategoryType_SelectedIndexChanged);
            // 
            // lblCatSelect
            // 
            this.lblCatSelect.AutoSize = true;
            this.lblCatSelect.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCatSelect.Location = new System.Drawing.Point(5, 10);
            this.lblCatSelect.Name = "lblCatSelect";
            this.lblCatSelect.Size = new System.Drawing.Size(74, 20);
            this.lblCatSelect.TabIndex = 0;
            this.lblCatSelect.Text = "Xem theo:";
            // 
            // FormBaoCaoDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.split);
            this.Controls.Add(this.pnlKpi);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormBaoCaoDoanhThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THỐNG KÊ DOANH THU (ClosedXML)";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlKpi.ResumeLayout(false);
            this.pnlKpi3.ResumeLayout(false);
            this.pnlKpi3.PerformLayout();
            this.pnlKpi2.ResumeLayout(false);
            this.pnlKpi2.PerformLayout();
            this.pnlKpi1.ResumeLayout(false);
            this.pnlKpi1.PerformLayout();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.grpTimeline.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).EndInit();
            this.grpCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).EndInit();
            this.pnlCatSwitch.ResumeLayout(false);
            this.pnlCatSwitch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblKy;
        private System.Windows.Forms.ComboBox cboKyBaoCao;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel pnlKpi;
        private System.Windows.Forms.Panel pnlKpi1;
        private System.Windows.Forms.Label lblTitleKpi1;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.Panel pnlKpi2;
        private System.Windows.Forms.Label lblTitleKpi2;
        private System.Windows.Forms.Label lblTongVeBan;
        private System.Windows.Forms.Panel pnlKpi3;
        private System.Windows.Forms.Label lblTitleKpi3;
        private System.Windows.Forms.Label lblTongHoaDon;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.GroupBox grpTimeline;
        private System.Windows.Forms.DataGridView dgvRevenue;
        private System.Windows.Forms.GroupBox grpCategory;
        private System.Windows.Forms.Panel pnlCatSwitch;
        private System.Windows.Forms.Label lblCatSelect;
        private System.Windows.Forms.ComboBox cboCategoryType;
        private System.Windows.Forms.DataGridView dgvCategory;
    }
}
