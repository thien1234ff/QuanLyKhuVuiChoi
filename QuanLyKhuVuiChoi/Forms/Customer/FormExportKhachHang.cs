using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Customer
{
    public partial class FormExportKhachHang : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;
        private readonly CustomerService _customerService;

        public FormExportKhachHang() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormExportKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _excelService = excelService ?? new ExcelService();
            _customerService = new CustomerService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                cboGioiTinh.SelectedIndex = 0;
                RefreshPreview();
            }
        }

        private void RadOption_CheckedChanged(object? sender, EventArgs e)
        {
            txtKeyword.Enabled = radFilter.Checked;
            cboGioiTinh.Enabled = radFilter.Checked;
            RefreshPreview();
        }

        private void BtnApplyFilter_Click(object? sender, EventArgs e)
        {
            RefreshPreview();
        }

        private System.Collections.Generic.List<KhachHang> GetFilteredList()
        {
            if (radAll.Checked)
            {
                return _customerService.GetAll();
            }

            var query = _customerService.GetAll().AsQueryable();
            var kw = txtKeyword.Text.Trim();
            if (!string.IsNullOrWhiteSpace(kw))
            {
                query = query.Where(k => k.MaKH.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                                         k.HoTen.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                                         k.SoDienThoai.Contains(kw));
            }

            var gt = cboGioiTinh.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(gt) && gt != "Tất cả giới tính")
            {
                query = query.Where(k => k.GioiTinh == gt);
            }

            return query.ToList();
        }

        private void RefreshPreview()
        {
            try
            {
                var list = GetFilteredList();
                dgvPreview.DataSource = list.Select(k => new
                {
                    k.MaKH,
                    k.HoTen,
                    NgaySinh = k.NgaySinh.HasValue ? k.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                    k.GioiTinh,
                    k.SoDienThoai,
                    k.Email,
                    k.DiaChi,
                    NgayDangKy = k.NgayDangKy.ToString("dd/MM/yyyy HH:mm")
                }).ToList();

                lblCount.Text = $"Số dòng chuẩn bị xuất: {list.Count:N0}";
                btnExport.Enabled = list.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xem trước: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            var list = GetFilteredList();
            if (!list.Any())
            {
                MessageBox.Show("Không có dữ liệu nào để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"DanhSach_KhachHang_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                Title = "Chọn nơi lưu file Excel danh sách khách hàng"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _customerService.ExportToExcel(list, sfd.FileName);

                    var ask = MessageBox.Show($"Xuất thành công {list.Count} dòng ra file:\n{sfd.FileName}\n\nBạn có muốn mở file ngay không?",
                        "Xuất Excel thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (ask == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
