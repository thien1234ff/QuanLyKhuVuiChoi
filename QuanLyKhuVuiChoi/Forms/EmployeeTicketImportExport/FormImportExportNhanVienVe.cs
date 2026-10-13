using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.EmployeeTicketImportExport
{
    public class FormImportExportNhanVienVe : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly TicketService _ticketService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI
        private TabControl tabControl = null!;

        // Tab Employee
        private TextBox txtEmployeeFile = null!;
        private DataGridView dgvEmployeePreview = null!;
        private ListBox lstEmployeeErrors = null!;
        private Label lblEmployeeSummary = null!;
        private Button btnConfirmEmployeeImport = null!;
        private ImportResult<NhanVien>? currentEmployeeResult;

        // Tab Ticket
        private TextBox txtTicketFile = null!;
        private DataGridView dgvTicketPreview = null!;
        private ListBox lstTicketErrors = null!;
        private Label lblTicketSummary = null!;
        private Button btnConfirmTicketImport = null!;
        private ImportResult<Ve>? currentTicketResult;

        public FormImportExportNhanVienVe() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormImportExportNhanVienVe(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _employeeService = new EmployeeService(_context, _excelService);
            _ticketService = new TicketService(_context, _excelService);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "FORM 9 – NHẬP/XUẤT DỮ LIỆU NHÂN VIÊN VÀ VÉ (ClosedXML)";
            this.Size = new Size(1050, 700);
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
                Text = "TRÌNH NHẬP & XUẤT EXCEL CHO NHÂN VIÊN VÀ VÉ",
                Font = UITheme.HeaderFont,
                ForeColor = Color.FromArgb(79, 70, 229),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);

            // TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = UITheme.SubHeaderFont
            };

            // TAB 1: NHÂN VIÊN
            var tabEmployee = new TabPage(" Nhập / Xuất Nhân Viên");
            BuildEmployeeTab(tabEmployee);
            tabControl.TabPages.Add(tabEmployee);

            // TAB 2: VÉ
            var tabTicket = new TabPage(" Nhập / Xuất Loại Vé");
            BuildTicketTab(tabTicket);
            tabControl.TabPages.Add(tabTicket);

            this.Controls.Add(tabControl);
            this.Controls.Add(pnlHeader);
        }

        private void BuildEmployeeTab(TabPage tab)
        {
            tab.BackColor = UITheme.Background;

            // Top Bar
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.White, Padding = new Padding(15) };
            var lbl = new Label { Text = "File Excel Nhân Viên:", Location = new Point(15, 18), AutoSize = true, Font = UITheme.BoldFont };
            txtEmployeeFile = new TextBox { Location = new Point(165, 15), Size = new Size(410, 25), ReadOnly = true, Font = UITheme.MainFont };

            var btnBrowse = new Button { Text = " Chọn File", Location = new Point(585, 13), Size = new Size(110, 30) };
            UITheme.StyleButton(btnBrowse, UITheme.Primary, Color.White);
            btnBrowse.Click += (s, e) => BrowseEmployeeFile();

            var btnExport = new Button { Text = " Xuất Danh Sách Ra Excel", Location = new Point(705, 13), Size = new Size(190, 30) };
            UITheme.StyleButton(btnExport, Color.FromArgb(16, 185, 129), Color.White);
            btnExport.Click += (s, e) => ExportEmployeeToExcel();

            var btnTemplate = new Button { Text = " File Mẫu", Location = new Point(905, 13), Size = new Size(100, 30) };
            UITheme.StyleButton(btnTemplate, Color.FromArgb(100, 116, 139), Color.White);
            btnTemplate.Click += (s, e) => DownloadEmployeeTemplate();

            lblEmployeeSummary = new Label { Text = "Chọn file để xem trước và kiểm tra.", Location = new Point(165, 52), AutoSize = true, Font = UITheme.BoldFont, ForeColor = UITheme.TextMuted };

            pnlTop.Controls.Add(lbl);
            pnlTop.Controls.Add(txtEmployeeFile);
            pnlTop.Controls.Add(btnBrowse);
            pnlTop.Controls.Add(btnExport);
            pnlTop.Controls.Add(btnTemplate);
            pnlTop.Controls.Add(lblEmployeeSummary);

            // Bottom Bar
            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White, Padding = new Padding(15) };
            btnConfirmEmployeeImport = new Button { Text = " Lưu Nhân Viên Vào SQL Server", Location = new Point(710, 12), Size = new Size(280, 36), Enabled = false };
            UITheme.StyleButton(btnConfirmEmployeeImport, UITheme.Success, Color.White);
            btnConfirmEmployeeImport.Click += (s, e) => SaveEmployeeImport();
            pnlBottom.Controls.Add(btnConfirmEmployeeImport);

            // Center Split
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 280 };

            dgvEmployeePreview = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvEmployeePreview);
            split.Panel1.Controls.Add(dgvEmployeePreview);

            lstEmployeeErrors = new ListBox { Dock = DockStyle.Fill, Font = UITheme.MainFont, ForeColor = Color.FromArgb(185, 28, 28) };
            split.Panel2.Controls.Add(lstEmployeeErrors);

            tab.Controls.Add(split);
            tab.Controls.Add(pnlBottom);
            tab.Controls.Add(pnlTop);
        }

        private void BuildTicketTab(TabPage tab)
        {
            tab.BackColor = UITheme.Background;

            // Top Bar
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.White, Padding = new Padding(15) };
            var lbl = new Label { Text = "File Excel Loại Vé:", Location = new Point(15, 18), AutoSize = true, Font = UITheme.BoldFont };
            txtTicketFile = new TextBox { Location = new Point(155, 15), Size = new Size(420, 25), ReadOnly = true, Font = UITheme.MainFont };

            var btnBrowse = new Button { Text = " Chọn File", Location = new Point(585, 13), Size = new Size(110, 30) };
            UITheme.StyleButton(btnBrowse, UITheme.Primary, Color.White);
            btnBrowse.Click += (s, e) => BrowseTicketFile();

            var btnExport = new Button { Text = " Xuất Danh Sách Ra Excel", Location = new Point(705, 13), Size = new Size(190, 30) };
            UITheme.StyleButton(btnExport, Color.FromArgb(16, 185, 129), Color.White);
            btnExport.Click += (s, e) => ExportTicketToExcel();

            var btnTemplate = new Button { Text = " File Mẫu", Location = new Point(905, 13), Size = new Size(100, 30) };
            UITheme.StyleButton(btnTemplate, Color.FromArgb(100, 116, 139), Color.White);
            btnTemplate.Click += (s, e) => DownloadTicketTemplate();

            lblTicketSummary = new Label { Text = "Chọn file để xem trước và kiểm tra.", Location = new Point(155, 52), AutoSize = true, Font = UITheme.BoldFont, ForeColor = UITheme.TextMuted };

            pnlTop.Controls.Add(lbl);
            pnlTop.Controls.Add(txtTicketFile);
            pnlTop.Controls.Add(btnBrowse);
            pnlTop.Controls.Add(btnExport);
            pnlTop.Controls.Add(btnTemplate);
            pnlTop.Controls.Add(lblTicketSummary);

            // Bottom Bar
            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White, Padding = new Padding(15) };
            btnConfirmTicketImport = new Button { Text = " Lưu Vé Vào SQL Server", Location = new Point(720, 12), Size = new Size(270, 36), Enabled = false };
            UITheme.StyleButton(btnConfirmTicketImport, UITheme.Success, Color.White);
            btnConfirmTicketImport.Click += (s, e) => SaveTicketImport();
            pnlBottom.Controls.Add(btnConfirmTicketImport);

            // Center Split
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 280 };

            dgvTicketPreview = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvTicketPreview);
            split.Panel1.Controls.Add(dgvTicketPreview);

            lstTicketErrors = new ListBox { Dock = DockStyle.Fill, Font = UITheme.MainFont, ForeColor = Color.FromArgb(185, 28, 28) };
            split.Panel2.Controls.Add(lstTicketErrors);

            tab.Controls.Add(split);
            tab.Controls.Add(pnlBottom);
            tab.Controls.Add(pnlTop);
        }

        // Methods for Employee
        private void BrowseEmployeeFile()
        {
            using (var ofd = new OpenFileDialog { Filter = "Excel Files (*.xlsx)|*.xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtEmployeeFile.Text = ofd.FileName;
                    lstEmployeeErrors.Items.Clear();
                    currentEmployeeResult = _employeeService.ValidateAndParseExcel(ofd.FileName);

                    dgvEmployeePreview.DataSource = null;
                    dgvEmployeePreview.DataSource = currentEmployeeResult.ValidItems;
                    if (dgvEmployeePreview.Columns["HoaDons"] != null) dgvEmployeePreview.Columns["HoaDons"].Visible = false;
                    if (dgvEmployeePreview.Columns["TaiKhoan"] != null) dgvEmployeePreview.Columns["TaiKhoan"].Visible = false;

                    foreach (var err in currentEmployeeResult.Errors) lstEmployeeErrors.Items.Add(err);

                    lblEmployeeSummary.Text = $"Tổng: {currentEmployeeResult.TotalRows}  |  Hợp lệ: {currentEmployeeResult.SuccessCount}  |  Lỗi: {currentEmployeeResult.ErrorCount}";
                    lblEmployeeSummary.ForeColor = currentEmployeeResult.ErrorCount > 0 ? UITheme.Danger : UITheme.Success;
                    btnConfirmEmployeeImport.Enabled = currentEmployeeResult.SuccessCount > 0;
                }
            }
        }

        private void SaveEmployeeImport()
        {
            if (currentEmployeeResult == null || currentEmployeeResult.ValidItems.Count == 0) return;
            var (success, msg) = _employeeService.SaveImportedEmployees(currentEmployeeResult.ValidItems);
            MessageBox.Show(msg, success ? "Thành Công" : "Lỗi", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) btnConfirmEmployeeImport.Enabled = false;
        }

        private void ExportEmployeeToExcel()
        {
            var list = _employeeService.GetAll();
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = $"DanhSachNhanVien_{DateTime.Now.Year}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _employeeService.ExportToExcel(list, sfd.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DownloadEmployeeTemplate()
        {
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "NhanVien_Template.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _excelService.CreateTemplateFile(
                        sfd.FileName,
                        "NhanVien",
                        new[] { "MaNV", "HoTen", "NgaySinh", "GioiTinh", "SoDienThoai", "Email", "DiaChi", "ChucVu", "TrangThai" },
                        new[] { "NV101", "Trần Văn Mẫu", "1995-10-20", "Nam", "0901234101", "mau.tran@example.com", "Hà Nội", "Thu ngân", "Đang làm việc" }
                    );
                    MessageBox.Show("Đã tạo file mẫu Excel Nhân Viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Methods for Ticket
        private void BrowseTicketFile()
        {
            using (var ofd = new OpenFileDialog { Filter = "Excel Files (*.xlsx)|*.xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtTicketFile.Text = ofd.FileName;
                    lstTicketErrors.Items.Clear();
                    currentTicketResult = _ticketService.ValidateAndParseExcel(ofd.FileName);

                    dgvTicketPreview.DataSource = null;
                    dgvTicketPreview.DataSource = currentTicketResult.ValidItems;

                    foreach (var err in currentTicketResult.Errors) lstTicketErrors.Items.Add(err);

                    lblTicketSummary.Text = $"Tổng: {currentTicketResult.TotalRows}  |  Hợp lệ: {currentTicketResult.SuccessCount}  |  Lỗi: {currentTicketResult.ErrorCount}";
                    lblTicketSummary.ForeColor = currentTicketResult.ErrorCount > 0 ? UITheme.Danger : UITheme.Success;
                    btnConfirmTicketImport.Enabled = currentTicketResult.SuccessCount > 0;
                }
            }
        }

        private void SaveTicketImport()
        {
            if (currentTicketResult == null || currentTicketResult.ValidItems.Count == 0) return;
            var (success, msg) = _ticketService.SaveImportedTickets(currentTicketResult.ValidItems);
            MessageBox.Show(msg, success ? "Thành Công" : "Lỗi", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) btnConfirmTicketImport.Enabled = false;
        }

        private void ExportTicketToExcel()
        {
            var list = _ticketService.GetAll();
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = $"DanhSachLoaiVe_{DateTime.Now.Year}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _ticketService.ExportToExcel(list, sfd.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DownloadTicketTemplate()
        {
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "Ve_Template.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _excelService.CreateTemplateFile(
                        sfd.FileName,
                        "Ve",
                        new[] { "MaVe", "TenVe", "LoaiVe", "GiaVe", "ThoiHan", "TrangThai", "MoTa" },
                        new[] { "VE101", "Vé Mẫu Đặc Biệt", "Vé ngày", "150000", "Trong ngày", "Đang áp dụng", "Mô tả mẫu" }
                    );
                    MessageBox.Show("Đã tạo file mẫu Excel Loại Vé thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
