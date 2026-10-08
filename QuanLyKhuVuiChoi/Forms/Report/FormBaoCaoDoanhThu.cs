using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Report
{
    public class FormBaoCaoDoanhThu : Form
    {
        private readonly ReportService _reportService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI Controls
        private ComboBox cboKyBaoCao = null!;
        private DateTimePicker dtpFrom = null!;
        private DateTimePicker dtpTo = null!;
        private DataGridView dgvRevenue = null!;
        private DataGridView dgvCategory = null!;
        private ComboBox cboCategoryType = null!;

        // Summary Labels
        private Label lblTongDoanhThu = null!;
        private Label lblTongVeBan = null!;
        private Label lblTongHoaDon = null!;

        public FormBaoCaoDoanhThu(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _reportService = new ReportService(_context, _excelService);

            InitializeComponent();
            LoadReportData();
        }

        private void InitializeComponent()
        {
            this.Text = "THÀNH VIÊN 4: BÁO CÁO DOANH THU & THỐNG KÊ (ClosedXML)";
            this.Size = new Size(1180, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;

            // Header Panel
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 12, 20, 10)
            };
            var lblTitle = new Label
            {
                Text = "BÁO CÁO THỐNG KÊ DOANH THU KHU VUI CHƠI GIẢI TRÍ",
                Font = UITheme.HeaderFont,
                ForeColor = Color.FromArgb(16, 185, 129),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);

            // Filter Bar
            var pnlFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.White,
                Padding = new Padding(20, 12, 20, 12)
            };

            var lblKy = new Label { Text = "Kỳ báo cáo:", Location = new Point(20, 20), AutoSize = true, Font = UITheme.BoldFont };
            cboKyBaoCao = new ComboBox { Location = new Point(105, 17), Size = new Size(125, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cboKyBaoCao.Items.AddRange(new object[] { "Theo Ngày", "Theo Tháng", "Theo Năm" });
            cboKyBaoCao.SelectedIndex = 0;
            cboKyBaoCao.SelectedIndexChanged += (s, e) => LoadReportData();

            var lblFrom = new Label { Text = "Từ ngày:", Location = new Point(245, 20), AutoSize = true, Font = UITheme.BoldFont };
            dtpFrom = new DateTimePicker { Location = new Point(310, 17), Size = new Size(120, 25), Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddMonths(-1) };

            var lblTo = new Label { Text = "Đến ngày:", Location = new Point(445, 20), AutoSize = true, Font = UITheme.BoldFont };
            dtpTo = new DateTimePicker { Location = new Point(515, 17), Size = new Size(120, 25), Format = DateTimePickerFormat.Short, Value = DateTime.Today };

            var btnBaoCao = new Button { Text = "📊 Xem Báo Cáo", Location = new Point(650, 15), Size = new Size(120, 28) };
            UITheme.StyleButton(btnBaoCao, UITheme.Primary, Color.White, 28);
            btnBaoCao.Click += (s, e) => LoadReportData();

            var btnExport = new Button { Text = "📤 Xuất Excel", Location = new Point(780, 15), Size = new Size(115, 28) };
            UITheme.StyleButton(btnExport, Color.FromArgb(16, 185, 129), Color.White, 28);
            btnExport.Click += BtnExport_Click;

            pnlFilter.Controls.Add(lblKy);
            pnlFilter.Controls.Add(cboKyBaoCao);
            pnlFilter.Controls.Add(lblFrom);
            pnlFilter.Controls.Add(dtpFrom);
            pnlFilter.Controls.Add(lblTo);
            pnlFilter.Controls.Add(dtpTo);
            pnlFilter.Controls.Add(btnBaoCao);
            pnlFilter.Controls.Add(btnExport);

            // KPI Cards Summary Panel
            var pnlKpi = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = UITheme.Background,
                Padding = new Padding(20, 10, 20, 10)
            };

            Panel CreateKpiCard(string title, out Label valLabel, Color titleColor, int left)
            {
                var card = new Panel
                {
                    Location = new Point(left, 8),
                    Size = new Size(350, 70),
                    BackColor = Color.White,
                    Padding = new Padding(15, 10, 15, 10)
                };
                var lblT = new Label { Text = title, Location = new Point(15, 8), AutoSize = true, Font = UITheme.BoldFont, ForeColor = titleColor };
                valLabel = new Label { Text = "0", Location = new Point(15, 30), AutoSize = true, Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = UITheme.TextMain };
                card.Controls.Add(lblT);
                card.Controls.Add(valLabel);
                return card;
            }

            pnlKpi.Controls.Add(CreateKpiCard("💰 TỔNG DOANH THU KỲ BÁO CÁO", out lblTongDoanhThu, Color.FromArgb(220, 38, 38), 20));
            pnlKpi.Controls.Add(CreateKpiCard("🎟️ TỔNG SỐ VÉ ĐÃ BÁN", out lblTongVeBan, Color.FromArgb(37, 99, 235), 390));
            pnlKpi.Controls.Add(CreateKpiCard("📋 TỔNG SỐ HÓA ĐƠN ĐÃ LẬP", out lblTongHoaDon, Color.FromArgb(16, 185, 129), 760));

            // Main Content Area: SplitContainer
            // Left: Revenue Timeline Grid
            // Right: Category Breakdown Grid (Vé, Dịch Vụ, Trò Chơi)
            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 650,
                Padding = new Padding(20, 10, 20, 15)
            };

            // Left Box
            var grpTimeline = new GroupBox
            {
                Text = "Doanh Thu Chi Tiết Theo Thời Gian",
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Font = UITheme.SubHeaderFont,
                Padding = new Padding(10)
            };
            dgvRevenue = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvRevenue);
            grpTimeline.Controls.Add(dgvRevenue);
            split.Panel1.Controls.Add(grpTimeline);

            // Right Box
            var grpCategory = new GroupBox
            {
                Text = "Doanh Thu Theo Hạng Mục",
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Font = UITheme.SubHeaderFont,
                Padding = new Padding(10)
            };

            var pnlCatSwitch = new Panel { Dock = DockStyle.Top, Height = 40 };
            var lblCatSelect = new Label { Text = "Xem theo:", Location = new Point(5, 10), AutoSize = true, Font = UITheme.MainFont };
            cboCategoryType = new ComboBox { Location = new Point(80, 7), Size = new Size(130, 25), DropDownStyle = ComboBoxStyle.DropDownList, Font = UITheme.MainFont };
            cboCategoryType.Items.AddRange(new object[] { "Tất cả", "Vé", "Dịch Vụ", "Trò Chơi" });
            cboCategoryType.SelectedIndex = 0;
            cboCategoryType.SelectedIndexChanged += (s, e) => LoadCategoryData();

            pnlCatSwitch.Controls.Add(lblCatSelect);
            pnlCatSwitch.Controls.Add(cboCategoryType);

            dgvCategory = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvCategory);

            grpCategory.Controls.Add(dgvCategory);
            grpCategory.Controls.Add(pnlCatSwitch);
            split.Panel2.Controls.Add(grpCategory);

            this.Controls.Add(split);
            this.Controls.Add(pnlKpi);
            this.Controls.Add(pnlFilter);
            this.Controls.Add(pnlHeader);
        }

        private void LoadReportData()
        {
            string ky = cboKyBaoCao.SelectedItem?.ToString() ?? "Theo Ngày";
            var timelineData = _reportService.GetRevenueByPeriod(ky, dtpFrom.Value, dtpTo.Value);

            dgvRevenue.DataSource = null;
            dgvRevenue.DataSource = timelineData;

            if (dgvRevenue.Columns["ThoiGian"] != null) dgvRevenue.Columns["ThoiGian"].HeaderText = "Thời Gian";
            if (dgvRevenue.Columns["SoHoaDon"] != null)
            {
                dgvRevenue.Columns["SoHoaDon"].HeaderText = "Số HĐ";
                dgvRevenue.Columns["SoHoaDon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvRevenue.Columns["SoVeBan"] != null)
            {
                dgvRevenue.Columns["SoVeBan"].HeaderText = "Vé Bán";
                dgvRevenue.Columns["SoVeBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvRevenue.Columns["DoanhThuVe"] != null)
            {
                dgvRevenue.Columns["DoanhThuVe"].HeaderText = "DT Vé";
                dgvRevenue.Columns["DoanhThuVe"].DefaultCellStyle.Format = "#,##0";
                dgvRevenue.Columns["DoanhThuVe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvRevenue.Columns["DoanhThuDichVu"] != null)
            {
                dgvRevenue.Columns["DoanhThuDichVu"].HeaderText = "DT Dịch Vụ";
                dgvRevenue.Columns["DoanhThuDichVu"].DefaultCellStyle.Format = "#,##0";
                dgvRevenue.Columns["DoanhThuDichVu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvRevenue.Columns["DoanhThuTroChoi"] != null)
            {
                dgvRevenue.Columns["DoanhThuTroChoi"].HeaderText = "DT Trò Chơi";
                dgvRevenue.Columns["DoanhThuTroChoi"].DefaultCellStyle.Format = "#,##0";
                dgvRevenue.Columns["DoanhThuTroChoi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvRevenue.Columns["TongDoanhThu"] != null)
            {
                dgvRevenue.Columns["TongDoanhThu"].HeaderText = "Tổng DT (VNĐ)";
                dgvRevenue.Columns["TongDoanhThu"].DefaultCellStyle.Format = "#,##0";
                dgvRevenue.Columns["TongDoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Update KPIs
            decimal sumRevenue = timelineData.Sum(t => t.TongDoanhThu);
            int sumTickets = timelineData.Sum(t => t.SoVeBan);
            int sumInvoices = timelineData.Sum(t => t.SoHoaDon);

            lblTongDoanhThu.Text = $"{sumRevenue:#,##0} VNĐ";
            lblTongVeBan.Text = $"{sumTickets:#,##0} vé";
            lblTongHoaDon.Text = $"{sumInvoices:#,##0} HĐ";

            LoadCategoryData();
        }

        private void LoadCategoryData()
        {
            string cat = cboCategoryType.SelectedItem?.ToString() ?? "Tất cả";
            var catData = _reportService.GetRevenueByCategory(cat, dtpFrom.Value, dtpTo.Value);

            dgvCategory.DataSource = null;
            dgvCategory.DataSource = catData;

            if (dgvCategory.Columns["TenHangMuc"] != null) dgvCategory.Columns["TenHangMuc"].HeaderText = "Tên Hạng Mục";
            if (dgvCategory.Columns["Loai"] != null) dgvCategory.Columns["Loai"].HeaderText = "Loại";
            if (dgvCategory.Columns["SoLuongDaBan"] != null)
            {
                dgvCategory.Columns["SoLuongDaBan"].HeaderText = "Số Lượng";
                dgvCategory.Columns["SoLuongDaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvCategory.Columns["DoanhThu"] != null)
            {
                dgvCategory.Columns["DoanhThu"].HeaderText = "Doanh Thu (VNĐ)";
                dgvCategory.Columns["DoanhThu"].DefaultCellStyle.Format = "#,##0";
                dgvCategory.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvCategory.Columns["TiLePhanTram"] != null)
            {
                dgvCategory.Columns["TiLePhanTram"].HeaderText = "Tỷ Lệ (%)";
                dgvCategory.Columns["TiLePhanTram"].DefaultCellStyle.Format = "0.00'%'";
                dgvCategory.Columns["TiLePhanTram"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            string ky = cboKyBaoCao.SelectedItem?.ToString() ?? "Theo Ngày";
            var timelineData = _reportService.GetRevenueByPeriod(ky, dtpFrom.Value, dtpTo.Value);

            if (timelineData == null || timelineData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu doanh thu trong khoảng thời gian này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyy_MM}.xlsx",
                Title = "Chọn nơi lưu file Báo Cáo Doanh Thu"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        _reportService.ExportRevenueReportToExcel(timelineData, ky, sfd.FileName);
                        Cursor = Cursors.Default;

                        var res = MessageBox.Show($"Xuất file Excel báo cáo doanh thu thành công!\nĐường dẫn: {sfd.FileName}\n\nBạn có muốn mở file ngay không?", "Thành Công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (res == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = sfd.FileName,
                                UseShellExecute = true
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
