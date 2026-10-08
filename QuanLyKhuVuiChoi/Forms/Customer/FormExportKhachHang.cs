using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Customer
{
    public class FormExportKhachHang : Form
    {
        private readonly CustomerService _customerService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        private RadioButton radAll = null!;
        private RadioButton radFilter = null!;
        private TextBox txtKeyword = null!;
        private ComboBox cboGioiTinh = null!;
        private DataGridView dgvPreview = null!;
        private Label lblCount = null!;
        private Button btnExport = null!;

        public FormExportKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _customerService = new CustomerService(_context, _excelService);

            InitializeComponent();
            RefreshPreview();
        }

        private void InitializeComponent()
        {
            this.Text = "FORM 3 – XUẤT DANH SÁCH KHÁCH HÀNG (ClosedXML)";
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;

            // Header
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 10)
            };
            var lblTitle = new Label
            {
                Text = "XUẤT BÁO CÁO DANH SÁCH KHÁCH HÀNG RA EXCEL (.XLSX)",
                Font = UITheme.HeaderFont,
                ForeColor = UITheme.Primary,
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);

            // Filter Options GroupBox
            var grpOption = new GroupBox
            {
                Text = "Tùy Chọn Xuất Dữ Liệu",
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.White,
                Font = UITheme.SubHeaderFont,
                ForeColor = UITheme.TextMain,
                Padding = new Padding(15, 10, 15, 10)
            };

            radAll = new RadioButton
            {
                Text = "Xuất toàn bộ khách hàng trong hệ thống",
                Location = new Point(20, 25),
                AutoSize = true,
                Font = UITheme.BoldFont,
                Checked = true
            };
            radAll.CheckedChanged += (s, e) => { ToggleFilterControls(); RefreshPreview(); };

            radFilter = new RadioButton
            {
                Text = "Xuất theo điều kiện tìm kiếm và bộ lọc:",
                Location = new Point(20, 55),
                AutoSize = true,
                Font = UITheme.BoldFont
            };
            radFilter.CheckedChanged += (s, e) => { ToggleFilterControls(); RefreshPreview(); };

            var lblKw = new Label { Text = "Từ khóa (Tên/SĐT/Email):", Location = new Point(300, 58), AutoSize = true, Font = UITheme.MainFont };
            txtKeyword = new TextBox { Location = new Point(480, 55), Size = new Size(180, 25), Font = UITheme.MainFont, Enabled = false };
            txtKeyword.TextChanged += (s, e) => RefreshPreview();

            var lblGt = new Label { Text = "Giới tính:", Location = new Point(680, 58), AutoSize = true, Font = UITheme.MainFont };
            cboGioiTinh = new ComboBox { Location = new Point(745, 55), Size = new Size(100, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList, Enabled = false };
            cboGioiTinh.Items.AddRange(new object[] { "Tất cả", "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
            cboGioiTinh.SelectedIndexChanged += (s, e) => RefreshPreview();

            grpOption.Controls.Add(radAll);
            grpOption.Controls.Add(radFilter);
            grpOption.Controls.Add(lblKw);
            grpOption.Controls.Add(txtKeyword);
            grpOption.Controls.Add(lblGt);
            grpOption.Controls.Add(cboGioiTinh);

            // Bottom Action Panel
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.White,
                Padding = new Padding(20, 12, 20, 12)
            };

            lblCount = new Label
            {
                Text = "Số lượng bản ghi sẽ xuất: 0",
                Font = UITheme.BoldFont,
                ForeColor = UITheme.TextMuted,
                Location = new Point(20, 22),
                AutoSize = true
            };

            btnExport = new Button
            {
                Text = "📤 Xuất File Excel (.xlsx)",
                Location = new Point(700, 12),
                Size = new Size(210, 40)
            };
            UITheme.StyleButton(btnExport, UITheme.Primary, Color.White);
            btnExport.Click += BtnExport_Click;

            pnlBottom.Controls.Add(lblCount);
            pnlBottom.Controls.Add(btnExport);

            // Center Preview DataGridView
            var pnlGrid = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15)
            };
            dgvPreview = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvPreview);
            pnlGrid.Controls.Add(dgvPreview);

            this.Controls.Add(pnlGrid);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(grpOption);
            this.Controls.Add(pnlHeader);
        }

        private void ToggleFilterControls()
        {
            txtKeyword.Enabled = radFilter.Checked;
            cboGioiTinh.Enabled = radFilter.Checked;
        }

        private System.Collections.Generic.List<KhachHang> GetFilteredList()
        {
            if (radAll.Checked)
            {
                return _customerService.GetAll();
            }

            var query = _customerService.Search(txtKeyword.Text, "Contains");
            string selectedGt = cboGioiTinh.SelectedItem?.ToString() ?? "Tất cả";
            if (selectedGt != "Tất cả")
            {
                query = query.Where(k => k.GioiTinh == selectedGt).ToList();
            }

            return query;
        }

        private void RefreshPreview()
        {
            var list = GetFilteredList();
            dgvPreview.DataSource = null;
            dgvPreview.DataSource = list;

            if (dgvPreview.Columns["HoaDons"] != null) dgvPreview.Columns["HoaDons"].Visible = false;

            lblCount.Text = $"Số lượng bản ghi sẽ xuất: {list.Count}";
            btnExport.Enabled = list.Count > 0;
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            var list = GetFilteredList();
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu khách hàng nào để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                sfd.FileName = $"DanhSachKhachHang_{DateTime.Now.Year}.xlsx";
                sfd.Title = "Chọn thư mục và tên file để lưu file Excel";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        _customerService.ExportToExcel(list, sfd.FileName);
                        Cursor = Cursors.Default;

                        var result = MessageBox.Show($"Xuất file Excel thành công!\nĐường dẫn: {sfd.FileName}\n\nBạn có muốn mở file ngay bây giờ không?", "Thành Công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
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
                        MessageBox.Show($"Lỗi xuất file Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
