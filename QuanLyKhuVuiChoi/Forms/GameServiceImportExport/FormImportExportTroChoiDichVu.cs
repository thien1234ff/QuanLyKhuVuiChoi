using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.GameServiceImportExport
{
    public class FormImportExportTroChoiDichVu : Form
    {
        private readonly GameService _gameService;
        private readonly ParkServiceService _parkService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI
        private TabControl tabControl = null!;

        // Tab Game
        private TextBox txtGameFile = null!;
        private DataGridView dgvGamePreview = null!;
        private ListBox lstGameErrors = null!;
        private Label lblGameSummary = null!;
        private Button btnConfirmGameImport = null!;
        private ImportResult<TroChoi>? currentGameResult;

        // Tab Service
        private TextBox txtServiceFile = null!;
        private DataGridView dgvServicePreview = null!;
        private ListBox lstServiceErrors = null!;
        private Label lblServiceSummary = null!;
        private Button btnConfirmServiceImport = null!;
        private ImportResult<DichVu>? currentServiceResult;

        public FormImportExportTroChoiDichVu() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormImportExportTroChoiDichVu(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _gameService = new GameService(_context, _excelService);
            _parkService = new ParkServiceService(_context, _excelService);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "FORM 6 – NHẬP/XUẤT DỮ LIỆU TRÒ CHƠI VÀ DỊCH VỤ (ClosedXML)";
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
                Text = "TRÌNH NHẬP & XUẤT EXCEL CHO TRÒ CHƠI VÀ DỊCH VỤ",
                Font = UITheme.HeaderFont,
                ForeColor = Color.FromArgb(194, 65, 12),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);

            // TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = UITheme.SubHeaderFont
            };

            // TAB 1: TRÒ CHƠI
            var tabGame = new TabPage(" Nhập / Xuất Trò Chơi");
            BuildGameTab(tabGame);
            tabControl.TabPages.Add(tabGame);

            // TAB 2: DỊCH VỤ
            var tabService = new TabPage(" Nhập / Xuất Dịch Vụ");
            BuildServiceTab(tabService);
            tabControl.TabPages.Add(tabService);

            this.Controls.Add(tabControl);
            this.Controls.Add(pnlHeader);
        }

        private void BuildGameTab(TabPage tab)
        {
            tab.BackColor = UITheme.Background;

            // Top Bar
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.White, Padding = new Padding(15) };
            var lbl = new Label { Text = "File Excel Trò Chơi:", Location = new Point(15, 18), AutoSize = true, Font = UITheme.BoldFont };
            txtGameFile = new TextBox { Location = new Point(155, 15), Size = new Size(420, 25), ReadOnly = true, Font = UITheme.MainFont };

            var btnBrowse = new Button { Text = " Chọn File", Location = new Point(585, 13), Size = new Size(110, 30) };
            UITheme.StyleButton(btnBrowse, UITheme.Primary, Color.White);
            btnBrowse.Click += (s, e) => BrowseGameFile();

            var btnExport = new Button { Text = " Xuất Danh Sách Ra Excel", Location = new Point(705, 13), Size = new Size(190, 30) };
            UITheme.StyleButton(btnExport, Color.FromArgb(16, 185, 129), Color.White);
            btnExport.Click += (s, e) => ExportGameToExcel();

            var btnTemplate = new Button { Text = " File Mẫu", Location = new Point(905, 13), Size = new Size(100, 30) };
            UITheme.StyleButton(btnTemplate, Color.FromArgb(100, 116, 139), Color.White);
            btnTemplate.Click += (s, e) => DownloadGameTemplate();

            lblGameSummary = new Label { Text = "Chọn file để xem trước và kiểm tra.", Location = new Point(155, 52), AutoSize = true, Font = UITheme.BoldFont, ForeColor = UITheme.TextMuted };

            pnlTop.Controls.Add(lbl);
            pnlTop.Controls.Add(txtGameFile);
            pnlTop.Controls.Add(btnBrowse);
            pnlTop.Controls.Add(btnExport);
            pnlTop.Controls.Add(btnTemplate);
            pnlTop.Controls.Add(lblGameSummary);

            // Bottom Bar
            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White, Padding = new Padding(15) };
            btnConfirmGameImport = new Button { Text = " Lưu Trò Chơi Vào SQL Server", Location = new Point(720, 12), Size = new Size(270, 36), Enabled = false };
            UITheme.StyleButton(btnConfirmGameImport, UITheme.Success, Color.White);
            btnConfirmGameImport.Click += (s, e) => SaveGameImport();
            pnlBottom.Controls.Add(btnConfirmGameImport);

            // Center Split
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 280 };
            
            dgvGamePreview = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvGamePreview);
            split.Panel1.Controls.Add(dgvGamePreview);

            lstGameErrors = new ListBox { Dock = DockStyle.Fill, Font = UITheme.MainFont, ForeColor = Color.FromArgb(185, 28, 28) };
            split.Panel2.Controls.Add(lstGameErrors);

            tab.Controls.Add(split);
            tab.Controls.Add(pnlBottom);
            tab.Controls.Add(pnlTop);
        }

        private void BuildServiceTab(TabPage tab)
        {
            tab.BackColor = UITheme.Background;

            // Top Bar
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.White, Padding = new Padding(15) };
            var lbl = new Label { Text = "File Excel Dịch Vụ:", Location = new Point(15, 18), AutoSize = true, Font = UITheme.BoldFont };
            txtServiceFile = new TextBox { Location = new Point(155, 15), Size = new Size(420, 25), ReadOnly = true, Font = UITheme.MainFont };

            var btnBrowse = new Button { Text = " Chọn File", Location = new Point(585, 13), Size = new Size(110, 30) };
            UITheme.StyleButton(btnBrowse, UITheme.Primary, Color.White);
            btnBrowse.Click += (s, e) => BrowseServiceFile();

            var btnExport = new Button { Text = " Xuất Danh Sách Ra Excel", Location = new Point(705, 13), Size = new Size(190, 30) };
            UITheme.StyleButton(btnExport, Color.FromArgb(16, 185, 129), Color.White);
            btnExport.Click += (s, e) => ExportServiceToExcel();

            var btnTemplate = new Button { Text = " File Mẫu", Location = new Point(905, 13), Size = new Size(100, 30) };
            UITheme.StyleButton(btnTemplate, Color.FromArgb(100, 116, 139), Color.White);
            btnTemplate.Click += (s, e) => DownloadServiceTemplate();

            lblServiceSummary = new Label { Text = "Chọn file để xem trước và kiểm tra.", Location = new Point(155, 52), AutoSize = true, Font = UITheme.BoldFont, ForeColor = UITheme.TextMuted };

            pnlTop.Controls.Add(lbl);
            pnlTop.Controls.Add(txtServiceFile);
            pnlTop.Controls.Add(btnBrowse);
            pnlTop.Controls.Add(btnExport);
            pnlTop.Controls.Add(btnTemplate);
            pnlTop.Controls.Add(lblServiceSummary);

            // Bottom Bar
            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White, Padding = new Padding(15) };
            btnConfirmServiceImport = new Button { Text = " Lưu Dịch Vụ Vào SQL Server", Location = new Point(720, 12), Size = new Size(270, 36), Enabled = false };
            UITheme.StyleButton(btnConfirmServiceImport, UITheme.Success, Color.White);
            btnConfirmServiceImport.Click += (s, e) => SaveServiceImport();
            pnlBottom.Controls.Add(btnConfirmServiceImport);

            // Center Split
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 280 };

            dgvServicePreview = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvServicePreview);
            split.Panel1.Controls.Add(dgvServicePreview);

            lstServiceErrors = new ListBox { Dock = DockStyle.Fill, Font = UITheme.MainFont, ForeColor = Color.FromArgb(185, 28, 28) };
            split.Panel2.Controls.Add(lstServiceErrors);

            tab.Controls.Add(split);
            tab.Controls.Add(pnlBottom);
            tab.Controls.Add(pnlTop);
        }

        // Methods for Game
        private void BrowseGameFile()
        {
            using (var ofd = new OpenFileDialog { Filter = "Excel Files (*.xlsx)|*.xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtGameFile.Text = ofd.FileName;
                    lstGameErrors.Items.Clear();
                    currentGameResult = _gameService.ValidateAndParseExcel(ofd.FileName);

                    dgvGamePreview.DataSource = null;
                    dgvGamePreview.DataSource = currentGameResult.ValidItems;
                    if (dgvGamePreview.Columns["GiaVe"] != null)
                        dgvGamePreview.Columns["GiaVe"].Visible = false;

                    foreach (var err in currentGameResult.Errors) lstGameErrors.Items.Add(err);

                    lblGameSummary.Text = $"Tổng: {currentGameResult.TotalRows}  |  Hợp lệ: {currentGameResult.SuccessCount}  |  Lỗi: {currentGameResult.ErrorCount}";
                    lblGameSummary.ForeColor = currentGameResult.ErrorCount > 0 ? UITheme.Danger : UITheme.Success;
                    btnConfirmGameImport.Enabled = currentGameResult.SuccessCount > 0;
                }
            }
        }

        private void SaveGameImport()
        {
            if (currentGameResult == null || currentGameResult.ValidItems.Count == 0) return;
            var (success, msg) = _gameService.SaveImportedGames(currentGameResult.ValidItems);
            MessageBox.Show(msg, success ? "Thành Công" : "Lỗi", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) btnConfirmGameImport.Enabled = false;
        }

        private void ExportGameToExcel()
        {
            var list = _gameService.GetAll();
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = $"DanhSachTroChoi_{DateTime.Now.Year}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _gameService.ExportToExcel(list, sfd.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DownloadGameTemplate()
        {
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "TroChoi_Template.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _excelService.CreateTemplateFile(
                        sfd.FileName,
                        "TroChoi",
                        new[] { "MaTroChoi", "TenTroChoi", "LoaiTroChoi", "DoTuoiToiThieu", "SucChua", "TrangThai", "MoTa" },
                        new[] { "TC101", "Tàu Lượn Mẫu", "Cảm giác mạnh", "12", "24", "Đang hoạt động", "Mô tả mẫu" }
                    );
                    MessageBox.Show("Đã tạo file mẫu Excel Trò Chơi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Methods for Service
        private void BrowseServiceFile()
        {
            using (var ofd = new OpenFileDialog { Filter = "Excel Files (*.xlsx)|*.xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtServiceFile.Text = ofd.FileName;
                    lstServiceErrors.Items.Clear();
                    currentServiceResult = _parkService.ValidateAndParseExcel(ofd.FileName);

                    dgvServicePreview.DataSource = null;
                    dgvServicePreview.DataSource = currentServiceResult.ValidItems;

                    foreach (var err in currentServiceResult.Errors) lstServiceErrors.Items.Add(err);

                    lblServiceSummary.Text = $"Tổng: {currentServiceResult.TotalRows}  |  Hợp lệ: {currentServiceResult.SuccessCount}  |  Lỗi: {currentServiceResult.ErrorCount}";
                    lblServiceSummary.ForeColor = currentServiceResult.ErrorCount > 0 ? UITheme.Danger : UITheme.Success;
                    btnConfirmServiceImport.Enabled = currentServiceResult.SuccessCount > 0;
                }
            }
        }

        private void SaveServiceImport()
        {
            if (currentServiceResult == null || currentServiceResult.ValidItems.Count == 0) return;
            var (success, msg) = _parkService.SaveImportedServices(currentServiceResult.ValidItems);
            MessageBox.Show(msg, success ? "Thành Công" : "Lỗi", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) btnConfirmServiceImport.Enabled = false;
        }

        private void ExportServiceToExcel()
        {
            var list = _parkService.GetAll();
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = $"DanhSachDichVu_{DateTime.Now.Year}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _parkService.ExportToExcel(list, sfd.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DownloadServiceTemplate()
        {
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "DichVu_Template.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _excelService.CreateTemplateFile(
                        sfd.FileName,
                        "DichVu",
                        new[] { "MaDichVu", "TenDichVu", "LoaiDichVu", "DonGia", "SoLuongTon", "TrangThai", "MoTa" },
                        new[] { "DV101", "Dịch vụ mẫu", "Ẩm thực", "40000", "100", "Đang phục vụ", "Mô tả mẫu" }
                    );
                    MessageBox.Show("Đã tạo file mẫu Excel Dịch Vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
