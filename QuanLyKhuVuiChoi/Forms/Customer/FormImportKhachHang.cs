using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Customer
{
    public class FormImportKhachHang : Form
    {
        private readonly CustomerService _customerService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        private TextBox txtFilePath = null!;
        private DataGridView dgvPreview = null!;
        private ListBox lstErrors = null!;
        private Label lblSummary = null!;
        private Button btnConfirmImport = null!;
        private ImportResult<KhachHang>? currentResult;

        public FormImportKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _customerService = new CustomerService(_context, _excelService);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "FORM 2 – NHẬP KHÁCH HÀNG TỪ FILE EXCEL (ClosedXML)";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;

            // Top File Selection Panel
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 15)
            };

            var lblTitle = new Label
            {
                Text = "NHẬP DỮ LIỆU KHÁCH HÀNG HÀNG LOẠT TỪ EXCEL",
                Font = UITheme.HeaderFont,
                ForeColor = UITheme.Primary,
                Location = new Point(20, 10),
                AutoSize = true
            };

            var lblPath = new Label { Text = "Đường dẫn file .xlsx:", Location = new Point(20, 48), AutoSize = true, Font = UITheme.BoldFont };
            txtFilePath = new TextBox { Location = new Point(170, 45), Size = new Size(500, 26), Font = UITheme.MainFont, ReadOnly = true };

            var btnBrowse = new Button { Text = "📂 Chọn File Excel", Location = new Point(680, 43), Size = new Size(130, 30) };
            UITheme.StyleButton(btnBrowse, UITheme.Primary, Color.White);
            btnBrowse.Click += BtnBrowse_Click;

            var btnDownloadTemplate = new Button { Text = "📋 Tải File Mẫu", Location = new Point(820, 43), Size = new Size(130, 30) };
            UITheme.StyleButton(btnDownloadTemplate, Color.FromArgb(13, 148, 136), Color.White);
            btnDownloadTemplate.Click += BtnDownloadTemplate_Click;

            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblPath);
            pnlTop.Controls.Add(txtFilePath);
            pnlTop.Controls.Add(btnBrowse);
            pnlTop.Controls.Add(btnDownloadTemplate);

            // Bottom Action & Summary Panel
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 15)
            };

            lblSummary = new Label
            {
                Text = "Chưa có file nào được nạp. Vui lòng chọn file .xlsx để kiểm tra dữ liệu.",
                Font = UITheme.BoldFont,
                ForeColor = UITheme.TextMuted,
                Location = new Point(20, 24),
                AutoSize = true
            };

            btnConfirmImport = new Button
            {
                Text = "💾 Xác Nhận Lưu Vào SQL Server",
                Location = new Point(720, 15),
                Size = new Size(230, 38),
                Enabled = false
            };
            UITheme.StyleButton(btnConfirmImport, UITheme.Success, Color.White);
            btnConfirmImport.Click += BtnConfirmImport_Click;

            pnlBottom.Controls.Add(lblSummary);
            pnlBottom.Controls.Add(btnConfirmImport);

            // Middle SplitContainer (Top: Preview DataGridView, Bottom: Validation Error Log)
            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 320,
                BackColor = UITheme.BorderColor
            };

            // Preview Grid Panel
            var pnlGrid = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lblGridHeader = new Label { Text = "📋 BẢNG XEM TRƯỚC DỮ LIỆU HỢP LỆ (PREVIEW)", Dock = DockStyle.Top, Height = 25, Font = UITheme.SubHeaderFont, ForeColor = UITheme.PrimaryDark };
            dgvPreview = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvPreview);
            pnlGrid.Controls.Add(dgvPreview);
            pnlGrid.Controls.Add(lblGridHeader);
            split.Panel1.Controls.Add(pnlGrid);

            // Error List Panel
            var pnlErrors = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lblErrorHeader = new Label { Text = "⚠️ DANH SÁCH DÒNG LỖI / CẢNH BÁO VALIDATION", Dock = DockStyle.Top, Height = 25, Font = UITheme.SubHeaderFont, ForeColor = UITheme.Danger };
            lstErrors = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = UITheme.MainFont,
                ForeColor = Color.FromArgb(185, 28, 28),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlErrors.Controls.Add(lstErrors);
            pnlErrors.Controls.Add(lblErrorHeader);
            split.Panel2.Controls.Add(pnlErrors);

            this.Controls.Add(split);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlTop);
        }

        private void BtnBrowse_Click(object? sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                ofd.Title = "Chọn file Excel danh sách khách hàng";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = ofd.FileName;
                    ProcessExcelFile(ofd.FileName);
                }
            }
        }

        private void ProcessExcelFile(string filePath)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lstErrors.Items.Clear();

                currentResult = _customerService.ValidateAndParseExcel(filePath);

                // Hiển thị các dòng hợp lệ lên Grid
                dgvPreview.DataSource = null;
                dgvPreview.DataSource = currentResult.ValidItems;

                if (dgvPreview.Columns["HoaDons"] != null) dgvPreview.Columns["HoaDons"].Visible = false;

                // Hiển thị danh sách lỗi chi tiết
                foreach (var err in currentResult.Errors)
                {
                    lstErrors.Items.Add(err);
                }

                // Cập nhật summary
                lblSummary.Text = $"Tổng số dòng: {currentResult.TotalRows}  |  Thành công: {currentResult.SuccessCount}  |  Thất bại: {currentResult.ErrorCount}";
                lblSummary.ForeColor = currentResult.ErrorCount > 0 ? UITheme.Danger : UITheme.Success;

                btnConfirmImport.Enabled = currentResult.SuccessCount > 0;

                if (currentResult.ErrorCount > 0)
                {
                    MessageBox.Show($"Tìm thấy {currentResult.ErrorCount} dòng dữ liệu lỗi không hợp lệ.\nCác dòng hợp lệ ({currentResult.SuccessCount}) có thể được nhập vào hệ thống.", "Kết Quả Kiểm Tra Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (currentResult.TotalRows > 0)
                {
                    MessageBox.Show($"Kiểm tra thành công! Toàn bộ {currentResult.SuccessCount} dòng đều hợp lệ và sẵn sàng lưu vào CSDL.", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("File Excel trống hoặc không có dòng dữ liệu nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đọc file Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnConfirmImport_Click(object? sender, EventArgs e)
        {
            if (currentResult == null || currentResult.ValidItems.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hợp lệ để nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn lưu {currentResult.ValidItems.Count} khách hàng vào SQL Server không?", "Xác nhận Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var (success, msg) = _customerService.SaveImportedCustomers(currentResult.ValidItems);
            if (success)
            {
                MessageBox.Show(msg, "Hoàn Tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConfirmImport.Enabled = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDownloadTemplate_Click(object? sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                sfd.FileName = "KhachHang_Template.xlsx";
                sfd.Title = "Lưu file mẫu Excel Khách Hàng";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _excelService.CreateTemplateFile(
                            sfd.FileName,
                            "KhachHang",
                            new[] { "MaKH", "HoTen", "NgaySinh", "GioiTinh", "SoDienThoai", "Email", "DiaChi" },
                            new[] { "KH101", "Nguyễn Văn Mẫu", "1998-05-15", "Nam", "0912345101", "mau.nguyen@example.com", "Hà Nội" }
                        );
                        MessageBox.Show($"Đã tạo file mẫu thành công tại:\n{sfd.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tạo file mẫu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
