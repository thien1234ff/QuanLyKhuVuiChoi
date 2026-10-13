using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Customer
{
    public partial class FormImportKhachHang : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;
        private readonly CustomerService _customerService;
        private ImportResult<KhachHang>? currentResult;

        public FormImportKhachHang() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormImportKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _excelService = excelService ?? new ExcelService();
            _customerService = new CustomerService(_context, _excelService);

            InitializeComponent();
        }

        private void BtnBrowse_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "Chọn file danh sách khách hàng Excel"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
                ProcessExcelFile(ofd.FileName);
            }
        }

        private void ProcessExcelFile(string filePath)
        {
            try
            {
                currentResult = _customerService.ValidateAndParseExcel(filePath);

                dgvPreview.DataSource = currentResult.ValidItems.Select(k => new
                {
                    k.MaKH,
                    k.HoTen,
                    NgaySinh = k.NgaySinh.HasValue ? k.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                    k.GioiTinh,
                    k.SoDienThoai,
                    k.Email,
                    k.DiaChi
                }).ToList();

                lstErrors.Items.Clear();
                foreach (var err in currentResult.Errors)
                {
                    lstErrors.Items.Add($" {err}");
                }

                lblSummary.Text = $"Tổng đọc: {currentResult.TotalRows} dòng | Hợp lệ: {currentResult.SuccessCount} dòng | Lỗi: {currentResult.ErrorCount} dòng";
                lblSummary.ForeColor = currentResult.SuccessCount > 0 ? Color.FromArgb(16, 185, 129) : Color.FromArgb(239, 68, 68);

                btnConfirmImport.Enabled = currentResult.SuccessCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đọc file Excel: {ex.Message}", "Lỗi File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnConfirmImport_Click(object? sender, EventArgs e)
        {
            if (currentResult == null || !currentResult.ValidItems.Any()) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn lưu {currentResult.SuccessCount} khách hàng hợp lệ vào Cơ sở dữ liệu?",
                "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var (success, message) = _customerService.SaveImportedCustomers(currentResult.ValidItems);
                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(message, "Lỗi lưu CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDownloadTemplate_Click(object? sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = "Mau_Nhap_KhachHang.xlsx",
                Title = "Tải file Excel mẫu"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var templateSource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExcelTemplates", "Mau_KhachHang.xlsx");
                    if (File.Exists(templateSource))
                    {
                        File.Copy(templateSource, sfd.FileName, true);
                    }
                    else
                    {
                        TemplateGenerator.EnsureTemplatesExist(AppDomain.CurrentDomain.BaseDirectory, _excelService);
                        if (File.Exists(templateSource))
                        {
                            File.Copy(templateSource, sfd.FileName, true);
                        }
                    }

                    var ask = MessageBox.Show("Đã tải file mẫu thành công! Bạn có muốn mở file ngay không?", "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (ask == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi tạo file mẫu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
