namespace QuanLyKhuVuiChoi.Forms.Dashboard
{
    partial class FormDashboard
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
            this.pnlTopHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlKpiContainer = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKpi1 = new System.Windows.Forms.Panel();
            this.lblKpi1Trend = new System.Windows.Forms.Label();
            this.lblTotalCustomer = new System.Windows.Forms.Label();
            this.lblKpi1Title = new System.Windows.Forms.Label();
            this.lblKpi1Icon = new System.Windows.Forms.Label();
            this.pnlKpi2 = new System.Windows.Forms.Panel();
            this.lblKpi2Trend = new System.Windows.Forms.Label();
            this.lblTicketsSold = new System.Windows.Forms.Label();
            this.lblKpi2Title = new System.Windows.Forms.Label();
            this.lblKpi2Icon = new System.Windows.Forms.Label();
            this.pnlKpi3 = new System.Windows.Forms.Panel();
            this.lblKpi3Trend = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.lblKpi3Title = new System.Windows.Forms.Label();
            this.lblKpi3Icon = new System.Windows.Forms.Label();
            this.pnlKpi4 = new System.Windows.Forms.Panel();
            this.lblKpi4Trend = new System.Windows.Forms.Label();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.lblKpi4Title = new System.Windows.Forms.Label();
            this.lblKpi4Icon = new System.Windows.Forms.Label();
            this.splitMiddle = new System.Windows.Forms.SplitContainer();
            this.grpChart = new System.Windows.Forms.GroupBox();
            this.pnlChartContainer = new System.Windows.Forms.Panel();
            this.grpAttractions = new System.Windows.Forms.GroupBox();
            this.pnlAttractions = new System.Windows.Forms.Panel();
            this.grpTransactions = new System.Windows.Forms.GroupBox();
            this.dgvRecentTransactions = new System.Windows.Forms.DataGridView();
            this.pnlTopHeader.SuspendLayout();
            this.pnlKpiContainer.SuspendLayout();
            this.pnlKpi1.SuspendLayout();
            this.pnlKpi2.SuspendLayout();
            this.pnlKpi3.SuspendLayout();
            this.pnlKpi4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMiddle)).BeginInit();
            this.splitMiddle.Panel1.SuspendLayout();
            this.splitMiddle.Panel2.SuspendLayout();
            this.splitMiddle.SuspendLayout();
            this.grpChart.SuspendLayout();
            this.grpAttractions.SuspendLayout();
            this.grpTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopHeader
            // 
            this.pnlTopHeader.BackColor = System.Drawing.Color.White;
            this.pnlTopHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTopHeader.Controls.Add(this.lblHeaderSubtitle);
            this.pnlTopHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTopHeader.Name = "pnlTopHeader";
            this.pnlTopHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 10);
            this.pnlTopHeader.Size = new System.Drawing.Size(1162, 70);
            this.pnlTopHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(22, 40);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(434, 20);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Theo dõi chỉ số kinh doanh, công suất hoạt động và doanh thu ngày";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 8);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(534, 32);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "🎡 BẢNG ĐIỀU KHIỂN TỔNG QUAN (DASHBOARD)";
            // 
            // pnlKpiContainer
            // 
            this.pnlKpiContainer.ColumnCount = 4;
            this.pnlKpiContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpiContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpiContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpiContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpiContainer.Controls.Add(this.pnlKpi1, 0, 0);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi2, 1, 0);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi3, 2, 0);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi4, 3, 0);
            this.pnlKpiContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiContainer.Location = new System.Drawing.Point(0, 70);
            this.pnlKpiContainer.Name = "pnlKpiContainer";
            this.pnlKpiContainer.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.pnlKpiContainer.RowCount = 1;
            this.pnlKpiContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKpiContainer.Size = new System.Drawing.Size(1162, 125);
            this.pnlKpiContainer.TabIndex = 1;
            // 
            // pnlKpi1
            // 
            this.pnlKpi1.BackColor = System.Drawing.Color.White;
            this.pnlKpi1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi1.Controls.Add(this.lblKpi1Trend);
            this.pnlKpi1.Controls.Add(this.lblTotalCustomer);
            this.pnlKpi1.Controls.Add(this.lblKpi1Title);
            this.pnlKpi1.Controls.Add(this.lblKpi1Icon);
            this.pnlKpi1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpi1.Location = new System.Drawing.Point(18, 15);
            this.pnlKpi1.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.pnlKpi1.Name = "pnlKpi1";
            this.pnlKpi1.Padding = new System.Windows.Forms.Padding(12);
            this.pnlKpi1.Size = new System.Drawing.Size(272, 95);
            this.pnlKpi1.TabIndex = 0;
            // 
            // lblKpi1Trend
            // 
            this.lblKpi1Trend.AutoSize = true;
            this.lblKpi1Trend.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblKpi1Trend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblKpi1Trend.Location = new System.Drawing.Point(65, 68);
            this.lblKpi1Trend.Name = "lblKpi1Trend";
            this.lblKpi1Trend.Size = new System.Drawing.Size(149, 19);
            this.lblKpi1Trend.TabIndex = 3;
            this.lblKpi1Trend.Text = "↑ 12% So với hôm qua";
            // 
            // lblTotalCustomer
            // 
            this.lblTotalCustomer.AutoSize = true;
            this.lblTotalCustomer.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotalCustomer.Location = new System.Drawing.Point(62, 28);
            this.lblTotalCustomer.Name = "lblTotalCustomer";
            this.lblTotalCustomer.Size = new System.Drawing.Size(32, 37);
            this.lblTotalCustomer.TabIndex = 2;
            this.lblTotalCustomer.Text = "0";
            // 
            // lblKpi1Title
            // 
            this.lblKpi1Title.AutoSize = true;
            this.lblKpi1Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblKpi1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi1Title.Location = new System.Drawing.Point(65, 8);
            this.lblKpi1Title.Name = "lblKpi1Title";
            this.lblKpi1Title.Size = new System.Drawing.Size(145, 20);
            this.lblKpi1Title.TabIndex = 1;
            this.lblKpi1Title.Text = "Tổng khách hôm nay";
            // 
            // lblKpi1Icon
            // 
            this.lblKpi1Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.lblKpi1Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblKpi1Icon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblKpi1Icon.Location = new System.Drawing.Point(10, 15);
            this.lblKpi1Icon.Name = "lblKpi1Icon";
            this.lblKpi1Icon.Size = new System.Drawing.Size(46, 50);
            this.lblKpi1Icon.TabIndex = 0;
            this.lblKpi1Icon.Text = "👥";
            this.lblKpi1Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlKpi2
            // 
            this.pnlKpi2.BackColor = System.Drawing.Color.White;
            this.pnlKpi2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi2.Controls.Add(this.lblKpi2Trend);
            this.pnlKpi2.Controls.Add(this.lblTicketsSold);
            this.pnlKpi2.Controls.Add(this.lblKpi2Title);
            this.pnlKpi2.Controls.Add(this.lblKpi2Icon);
            this.pnlKpi2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpi2.Location = new System.Drawing.Point(301, 15);
            this.pnlKpi2.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.pnlKpi2.Name = "pnlKpi2";
            this.pnlKpi2.Padding = new System.Windows.Forms.Padding(12);
            this.pnlKpi2.Size = new System.Drawing.Size(272, 95);
            this.pnlKpi2.TabIndex = 1;
            // 
            // lblKpi2Trend
            // 
            this.lblKpi2Trend.AutoSize = true;
            this.lblKpi2Trend.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblKpi2Trend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblKpi2Trend.Location = new System.Drawing.Point(65, 68);
            this.lblKpi2Trend.Name = "lblKpi2Trend";
            this.lblKpi2Trend.Size = new System.Drawing.Size(141, 19);
            this.lblKpi2Trend.TabIndex = 3;
            this.lblKpi2Trend.Text = "↑ 8% So với hôm qua";
            // 
            // lblTicketsSold
            // 
            this.lblTicketsSold.AutoSize = true;
            this.lblTicketsSold.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTicketsSold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTicketsSold.Location = new System.Drawing.Point(62, 28);
            this.lblTicketsSold.Name = "lblTicketsSold";
            this.lblTicketsSold.Size = new System.Drawing.Size(32, 37);
            this.lblTicketsSold.TabIndex = 2;
            this.lblTicketsSold.Text = "0";
            // 
            // lblKpi2Title
            // 
            this.lblKpi2Title.AutoSize = true;
            this.lblKpi2Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblKpi2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi2Title.Location = new System.Drawing.Point(65, 8);
            this.lblKpi2Title.Name = "lblKpi2Title";
            this.lblKpi2Title.Size = new System.Drawing.Size(79, 20);
            this.lblKpi2Title.TabIndex = 1;
            this.lblKpi2Title.Text = "Vé đã bán";
            // 
            // lblKpi2Icon
            // 
            this.lblKpi2Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblKpi2Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblKpi2Icon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(85)))), ((int)(((byte)(247)))));
            this.lblKpi2Icon.Location = new System.Drawing.Point(10, 15);
            this.lblKpi2Icon.Name = "lblKpi2Icon";
            this.lblKpi2Icon.Size = new System.Drawing.Size(46, 50);
            this.lblKpi2Icon.TabIndex = 0;
            this.lblKpi2Icon.Text = "🎟️";
            this.lblKpi2Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlKpi3
            // 
            this.pnlKpi3.BackColor = System.Drawing.Color.White;
            this.pnlKpi3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi3.Controls.Add(this.lblKpi3Trend);
            this.pnlKpi3.Controls.Add(this.lblRevenue);
            this.pnlKpi3.Controls.Add(this.lblKpi3Title);
            this.pnlKpi3.Controls.Add(this.lblKpi3Icon);
            this.pnlKpi3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpi3.Location = new System.Drawing.Point(584, 15);
            this.pnlKpi3.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.pnlKpi3.Name = "pnlKpi3";
            this.pnlKpi3.Padding = new System.Windows.Forms.Padding(12);
            this.pnlKpi3.Size = new System.Drawing.Size(272, 95);
            this.pnlKpi3.TabIndex = 2;
            // 
            // lblKpi3Trend
            // 
            this.lblKpi3Trend.AutoSize = true;
            this.lblKpi3Trend.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblKpi3Trend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblKpi3Trend.Location = new System.Drawing.Point(65, 68);
            this.lblKpi3Trend.Name = "lblKpi3Trend";
            this.lblKpi3Trend.Size = new System.Drawing.Size(149, 19);
            this.lblKpi3Trend.TabIndex = 3;
            this.lblKpi3Trend.Text = "↑ 18% So với hôm qua";
            // 
            // lblRevenue
            // 
            this.lblRevenue.AutoSize = true;
            this.lblRevenue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblRevenue.Location = new System.Drawing.Point(62, 28);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(65, 37);
            this.lblRevenue.TabIndex = 2;
            this.lblRevenue.Text = "0 đ";
            // 
            // lblKpi3Title
            // 
            this.lblKpi3Title.AutoSize = true;
            this.lblKpi3Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblKpi3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi3Title.Location = new System.Drawing.Point(65, 8);
            this.lblKpi3Title.Name = "lblKpi3Title";
            this.lblKpi3Title.Size = new System.Drawing.Size(78, 20);
            this.lblKpi3Title.TabIndex = 1;
            this.lblKpi3Title.Text = "Doanh thu";
            // 
            // lblKpi3Icon
            // 
            this.lblKpi3Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.lblKpi3Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblKpi3Icon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblKpi3Icon.Location = new System.Drawing.Point(10, 15);
            this.lblKpi3Icon.Name = "lblKpi3Icon";
            this.lblKpi3Icon.Size = new System.Drawing.Size(46, 50);
            this.lblKpi3Icon.TabIndex = 0;
            this.lblKpi3Icon.Text = "💰";
            this.lblKpi3Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlKpi4
            // 
            this.pnlKpi4.BackColor = System.Drawing.Color.White;
            this.pnlKpi4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi4.Controls.Add(this.lblKpi4Trend);
            this.pnlKpi4.Controls.Add(this.lblCapacity);
            this.pnlKpi4.Controls.Add(this.lblKpi4Title);
            this.pnlKpi4.Controls.Add(this.lblKpi4Icon);
            this.pnlKpi4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpi4.Location = new System.Drawing.Point(867, 15);
            this.pnlKpi4.Name = "pnlKpi4";
            this.pnlKpi4.Padding = new System.Windows.Forms.Padding(12);
            this.pnlKpi4.Size = new System.Drawing.Size(277, 95);
            this.pnlKpi4.TabIndex = 3;
            // 
            // lblKpi4Trend
            // 
            this.lblKpi4Trend.AutoSize = true;
            this.lblKpi4Trend.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblKpi4Trend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblKpi4Trend.Location = new System.Drawing.Point(65, 68);
            this.lblKpi4Trend.Name = "lblKpi4Trend";
            this.lblKpi4Trend.Size = new System.Drawing.Size(141, 19);
            this.lblKpi4Trend.TabIndex = 3;
            this.lblKpi4Trend.Text = "↑ 5% So với hôm qua";
            // 
            // lblCapacity
            // 
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCapacity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblCapacity.Location = new System.Drawing.Point(62, 28);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(68, 37);
            this.lblCapacity.TabIndex = 2;
            this.lblCapacity.Text = "72%";
            // 
            // lblKpi4Title
            // 
            this.lblKpi4Title.AutoSize = true;
            this.lblKpi4Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblKpi4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi4Title.Location = new System.Drawing.Point(65, 8);
            this.lblKpi4Title.Name = "lblKpi4Title";
            this.lblKpi4Title.Size = new System.Drawing.Size(155, 20);
            this.lblKpi4Title.TabIndex = 1;
            this.lblKpi4Title.Text = "Công suất khu vui chơi";
            // 
            // lblKpi4Icon
            // 
            this.lblKpi4Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.lblKpi4Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblKpi4Icon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblKpi4Icon.Location = new System.Drawing.Point(10, 15);
            this.lblKpi4Icon.Name = "lblKpi4Icon";
            this.lblKpi4Icon.Size = new System.Drawing.Size(46, 50);
            this.lblKpi4Icon.TabIndex = 0;
            this.lblKpi4Icon.Text = "🎡";
            this.lblKpi4Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitMiddle
            // 
            this.splitMiddle.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitMiddle.Location = new System.Drawing.Point(0, 195);
            this.splitMiddle.Name = "splitMiddle";
            // 
            // splitMiddle.Panel1
            // 
            this.splitMiddle.Panel1.Controls.Add(this.grpChart);
            this.splitMiddle.Panel1.Padding = new System.Windows.Forms.Padding(15, 5, 8, 5);
            // 
            // splitMiddle.Panel2
            // 
            this.splitMiddle.Panel2.Controls.Add(this.grpAttractions);
            this.splitMiddle.Panel2.Padding = new System.Windows.Forms.Padding(8, 5, 15, 5);
            this.splitMiddle.Size = new System.Drawing.Size(1162, 290);
            this.splitMiddle.SplitterDistance = 680;
            this.splitMiddle.TabIndex = 2;
            // 
            // grpChart
            // 
            this.grpChart.BackColor = System.Drawing.Color.White;
            this.grpChart.Controls.Add(this.pnlChartContainer);
            this.grpChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpChart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpChart.Location = new System.Drawing.Point(15, 5);
            this.grpChart.Name = "grpChart";
            this.grpChart.Padding = new System.Windows.Forms.Padding(10);
            this.grpChart.Size = new System.Drawing.Size(657, 280);
            this.grpChart.TabIndex = 0;
            this.grpChart.TabStop = false;
            this.grpChart.Text = "📈 Biểu Đồ Doanh Thu 7 Ngày Gần Nhất";
            // 
            // pnlChartContainer
            // 
            this.pnlChartContainer.BackColor = System.Drawing.Color.White;
            this.pnlChartContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartContainer.Location = new System.Drawing.Point(10, 33);
            this.pnlChartContainer.Name = "pnlChartContainer";
            this.pnlChartContainer.Size = new System.Drawing.Size(637, 237);
            this.pnlChartContainer.TabIndex = 0;
            // 
            // grpAttractions
            // 
            this.grpAttractions.BackColor = System.Drawing.Color.White;
            this.grpAttractions.Controls.Add(this.pnlAttractions);
            this.grpAttractions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAttractions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpAttractions.Location = new System.Drawing.Point(8, 5);
            this.grpAttractions.Name = "grpAttractions";
            this.grpAttractions.Padding = new System.Windows.Forms.Padding(10);
            this.grpAttractions.Size = new System.Drawing.Size(455, 280);
            this.grpAttractions.TabIndex = 0;
            this.grpAttractions.TabStop = false;
            this.grpAttractions.Text = "🎪 Tình Trạng Khu Vui Chơi";
            // 
            // pnlAttractions
            // 
            this.pnlAttractions.AutoScroll = true;
            this.pnlAttractions.BackColor = System.Drawing.Color.White;
            this.pnlAttractions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAttractions.Location = new System.Drawing.Point(10, 33);
            this.pnlAttractions.Name = "pnlAttractions";
            this.pnlAttractions.Size = new System.Drawing.Size(435, 237);
            this.pnlAttractions.TabIndex = 0;
            // 
            // grpTransactions
            // 
            this.grpTransactions.BackColor = System.Drawing.Color.White;
            this.grpTransactions.Controls.Add(this.dgvRecentTransactions);
            this.grpTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTransactions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTransactions.Location = new System.Drawing.Point(0, 485);
            this.grpTransactions.Margin = new System.Windows.Forms.Padding(15);
            this.grpTransactions.Name = "grpTransactions";
            this.grpTransactions.Padding = new System.Windows.Forms.Padding(15);
            this.grpTransactions.Size = new System.Drawing.Size(1162, 268);
            this.grpTransactions.TabIndex = 3;
            this.grpTransactions.TabStop = false;
            this.grpTransactions.Text = "🧾 Giao Dịch Gần Đây";
            // 
            // dgvRecentTransactions
            // 
            this.dgvRecentTransactions.AllowUserToAddRows = false;
            this.dgvRecentTransactions.AllowUserToDeleteRows = false;
            this.dgvRecentTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentTransactions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvRecentTransactions.Location = new System.Drawing.Point(15, 38);
            this.dgvRecentTransactions.Name = "dgvRecentTransactions";
            this.dgvRecentTransactions.ReadOnly = true;
            this.dgvRecentTransactions.RowHeadersVisible = false;
            this.dgvRecentTransactions.RowHeadersWidth = 51;
            this.dgvRecentTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentTransactions.Size = new System.Drawing.Size(1132, 215);
            this.dgvRecentTransactions.TabIndex = 0;
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1162, 753);
            this.Controls.Add(this.grpTransactions);
            this.Controls.Add(this.splitMiddle);
            this.Controls.Add(this.pnlKpiContainer);
            this.Controls.Add(this.pnlTopHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BẢNG ĐIỀU KHIỂN TỔNG QUAN (DASHBOARD)";
            this.pnlTopHeader.ResumeLayout(false);
            this.pnlTopHeader.PerformLayout();
            this.pnlKpiContainer.ResumeLayout(false);
            this.pnlKpi1.ResumeLayout(false);
            this.pnlKpi1.PerformLayout();
            this.pnlKpi2.ResumeLayout(false);
            this.pnlKpi2.PerformLayout();
            this.pnlKpi3.ResumeLayout(false);
            this.pnlKpi3.PerformLayout();
            this.pnlKpi4.ResumeLayout(false);
            this.pnlKpi4.PerformLayout();
            this.splitMiddle.Panel1.ResumeLayout(false);
            this.splitMiddle.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMiddle)).EndInit();
            this.splitMiddle.ResumeLayout(false);
            this.grpChart.ResumeLayout(false);
            this.grpAttractions.ResumeLayout(false);
            this.grpTransactions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentTransactions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.TableLayoutPanel pnlKpiContainer;
        private System.Windows.Forms.Panel pnlKpi1;
        private System.Windows.Forms.Label lblKpi1Icon;
        private System.Windows.Forms.Label lblKpi1Title;
        private System.Windows.Forms.Label lblTotalCustomer;
        private System.Windows.Forms.Label lblKpi1Trend;
        private System.Windows.Forms.Panel pnlKpi2;
        private System.Windows.Forms.Label lblKpi2Icon;
        private System.Windows.Forms.Label lblKpi2Title;
        private System.Windows.Forms.Label lblTicketsSold;
        private System.Windows.Forms.Label lblKpi2Trend;
        private System.Windows.Forms.Panel pnlKpi3;
        private System.Windows.Forms.Label lblKpi3Icon;
        private System.Windows.Forms.Label lblKpi3Title;
        private System.Windows.Forms.Label lblRevenue;
        private System.Windows.Forms.Label lblKpi3Trend;
        private System.Windows.Forms.Panel pnlKpi4;
        private System.Windows.Forms.Label lblKpi4Icon;
        private System.Windows.Forms.Label lblKpi4Title;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Label lblKpi4Trend;
        private System.Windows.Forms.SplitContainer splitMiddle;
        private System.Windows.Forms.GroupBox grpChart;
        private System.Windows.Forms.Panel pnlChartContainer;
        private System.Windows.Forms.GroupBox grpAttractions;
        private System.Windows.Forms.Panel pnlAttractions;
        private System.Windows.Forms.GroupBox grpTransactions;
        private System.Windows.Forms.DataGridView dgvRecentTransactions;
    }
}
