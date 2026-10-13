using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Report
{
    public partial class FormBaoCaoDoanhThu : Form
    {
        private readonly ReportService _reportService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        public FormBaoCaoDoanhThu() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormBaoCaoDoanhThu(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _reportService = new ReportService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;

            if (cboKyBaoCao.SelectedIndex == -1) cboKyBaoCao.SelectedIndex = 0;
            if (cboCategoryType.SelectedIndex == -1) cboCategoryType.SelectedIndex = 0;

            LoadReportData();
        }

        private void CboKyBaoCao_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadReportData();
        }

        private void CboCategoryType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadCategoryData();
        }

        private void BtnBaoCao_Click(object? sender, EventArgs e)
        {
            LoadReportData();
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
