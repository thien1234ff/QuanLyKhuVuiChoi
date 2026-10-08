using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Game
{
    public class FormTroChoi : Form
    {
        private readonly GameService _gameService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI Controls
        private DataGridView dgvTroChoi = null!;
        private TextBox txtMaTroChoi = null!;
        private TextBox txtTenTroChoi = null!;
        private ComboBox cboLoaiTroChoi = null!;
        private NumericUpDown numGiaVe = null!;
        private NumericUpDown numDoTuoi = null!;
        private NumericUpDown numSucChua = null!;
        private ComboBox cboTrangThai = null!;
        private TextBox txtMoTa = null!;

        private TextBox txtSearch = null!;
        private ComboBox cboFilterLoai = null!;
        private ComboBox cboFilterTrangThai = null!;
        private Label lblTotal = null!;

        public FormTroChoi(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _gameService = new GameService(_context, _excelService);

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "THÀNH VIÊN 2: QUẢN LÝ TRÒ CHƠI";
            this.Size = new Size(1150, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;

            // Header Panel
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };
            var lblTitle = new Label
            {
                Text = "QUẢN LÝ DANH MỤC TRÒ CHƠI KHU GIẢI TRÍ",
                Font = UITheme.HeaderFont,
                ForeColor = Color.FromArgb(194, 65, 12), // Amber/Orange Theme for Games
                AutoSize = true,
                Location = new Point(20, 15)
            };
            lblTotal = new Label
            {
                Text = "Tổng số trò chơi: 0",
                Font = UITheme.BoldFont,
                ForeColor = UITheme.TextMuted,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(930, 20)
            };
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblTotal);

            // Left Input Panel
            var pnlInput = new GroupBox
            {
                Text = "Thông Tin Trò Chơi",
                Dock = DockStyle.Left,
                Width = 370,
                BackColor = Color.White,
                Font = UITheme.SubHeaderFont,
                ForeColor = UITheme.TextMain,
                Padding = new Padding(15)
            };

            int y = 30;
            int labelW = 110;
            int inputW = 210;
            int gap = 38;

            Label CreateLabel(string text, int top) => new Label
            {
                Text = text,
                Location = new Point(15, top + 3),
                Size = new Size(labelW, 20),
                Font = UITheme.MainFont,
                ForeColor = UITheme.TextMain
            };

            // MaTroChoi
            pnlInput.Controls.Add(CreateLabel("Mã trò chơi:", y));
            txtMaTroChoi = new TextBox { Location = new Point(135, y), Size = new Size(130, 25), Font = UITheme.MainFont, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };
            var btnAutoId = new Button { Text = "Mới", Location = new Point(275, y), Size = new Size(65, 26), Font = UITheme.MainFont, Cursor = Cursors.Hand };
            UITheme.StyleButton(btnAutoId, UITheme.Primary, Color.White);
            btnAutoId.Click += (s, e) => { ClearInputs(); txtMaTroChoi.Text = _gameService.GenerateNextId(); };
            pnlInput.Controls.Add(txtMaTroChoi);
            pnlInput.Controls.Add(btnAutoId);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Tên trò chơi (*):", y));
            txtTenTroChoi = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtTenTroChoi);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Loại trò chơi:", y));
            cboLoaiTroChoi = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiTroChoi.Items.AddRange(new object[] { "Cảm giác mạnh", "Gia đình", "Trẻ em", "Dưới nước", "Phiêu lưu", "Công nghệ", "Thể thao", "Hành động" });
            cboLoaiTroChoi.SelectedIndex = 0;
            pnlInput.Controls.Add(cboLoaiTroChoi);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Giá vé (VNĐ):", y));
            numGiaVe = new NumericUpDown { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Maximum = 10000000, Increment = 5000, ThousandsSeparator = true };
            pnlInput.Controls.Add(numGiaVe);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Độ tuổi tối thiểu:", y));
            numDoTuoi = new NumericUpDown { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Maximum = 100, Value = 0 };
            pnlInput.Controls.Add(numDoTuoi);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Sức chứa (người):", y));
            numSucChua = new NumericUpDown { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Minimum = 1, Maximum = 1000, Value = 20 };
            pnlInput.Controls.Add(numSucChua);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Trạng thái:", y));
            cboTrangThai = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThai.Items.AddRange(new object[] { "Đang hoạt động", "Bảo trì", "Tạm ngưng" });
            cboTrangThai.SelectedIndex = 0;
            pnlInput.Controls.Add(cboTrangThai);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Mô tả:", y));
            txtMoTa = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 55), Font = UITheme.MainFont, Multiline = true };
            pnlInput.Controls.Add(txtMoTa);

            // Action Buttons
            y += 70;
            var btnThem = new Button { Text = "➕ Thêm", Location = new Point(15, y), Size = new Size(100, 30) };
            UITheme.StyleButton(btnThem, UITheme.Primary, Color.White, 30);
            btnThem.Click += BtnThem_Click;

            var btnSua = new Button { Text = "✏️ Sửa", Location = new Point(125, y), Size = new Size(100, 30) };
            UITheme.StyleButton(btnSua, Color.FromArgb(245, 158, 11), Color.White, 30);
            btnSua.Click += BtnSua_Click;

            var btnXoa = new Button { Text = "🗑️ Xóa", Location = new Point(235, y), Size = new Size(100, 30) };
            UITheme.StyleDangerButton(btnXoa, 30);
            btnXoa.Click += BtnXoa_Click;

            y += 38;
            var btnLamMoi = new Button { Text = "🔄 Làm Mới Dữ Liệu", Location = new Point(15, y), Size = new Size(320, 30) };
            UITheme.StyleSecondaryButton(btnLamMoi, 30);
            btnLamMoi.Click += (s, e) => ClearInputs();

            pnlInput.Controls.Add(btnThem);
            pnlInput.Controls.Add(btnSua);
            pnlInput.Controls.Add(btnXoa);
            pnlInput.Controls.Add(btnLamMoi);

            // Right Panel (Filter + DataGridView)
            var pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 10, 15, 15)
            };

            // Filter Bar Box
            var pnlFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            var lblSearch = new Label { Text = "Tìm:", Location = new Point(10, 18), AutoSize = true, Font = UITheme.BoldFont };
            txtSearch = new TextBox { Location = new Point(45, 15), Size = new Size(160, 25), Font = UITheme.MainFont };
            txtSearch.TextChanged += (s, e) => ApplyFilter();

            var lblLoai = new Label { Text = "Loại:", Location = new Point(220, 18), AutoSize = true, Font = UITheme.BoldFont };
            cboFilterLoai = new ComboBox { Location = new Point(260, 15), Size = new Size(130, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboFilterLoai.Items.AddRange(new object[] { "Tất cả", "Cảm giác mạnh", "Gia đình", "Trẻ em", "Dưới nước", "Phiêu lưu", "Công nghệ", "Thể thao", "Hành động" });
            cboFilterLoai.SelectedIndex = 0;
            cboFilterLoai.SelectedIndexChanged += (s, e) => ApplyFilter();

            var lblTrangThai = new Label { Text = "Trạng thái:", Location = new Point(405, 18), AutoSize = true, Font = UITheme.BoldFont };
            cboFilterTrangThai = new ComboBox { Location = new Point(485, 15), Size = new Size(130, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboFilterTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang hoạt động", "Bảo trì", "Tạm ngưng" });
            cboFilterTrangThai.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndexChanged += (s, e) => ApplyFilter();

            var btnReset = new Button { Text = "Đặt lại", Location = new Point(630, 12), Size = new Size(75, 28) };
            UITheme.StyleSecondaryButton(btnReset, 28);
            btnReset.Click += (s, e) => { txtSearch.Text = ""; cboFilterLoai.SelectedIndex = 0; cboFilterTrangThai.SelectedIndex = 0; LoadData(); };

            pnlFilter.Controls.Add(lblSearch);
            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(lblLoai);
            pnlFilter.Controls.Add(cboFilterLoai);
            pnlFilter.Controls.Add(lblTrangThai);
            pnlFilter.Controls.Add(cboFilterTrangThai);
            pnlFilter.Controls.Add(btnReset);

            // DataGridView
            dgvTroChoi = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvTroChoi);
            dgvTroChoi.CellClick += DgvTroChoi_CellClick;

            pnlRight.Controls.Add(dgvTroChoi);
            pnlRight.Controls.Add(pnlFilter);

            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlHeader);

            ClearInputs();
        }

        private void LoadData()
        {
            var list = _gameService.GetAll();
            BindGrid(list);
        }

        private void ApplyFilter()
        {
            var list = _gameService.Filter(
                txtSearch.Text,
                cboFilterLoai.SelectedItem?.ToString() ?? "Tất cả",
                cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả"
            );
            BindGrid(list);
        }

        private void BindGrid(System.Collections.Generic.List<TroChoi> list)
        {
            dgvTroChoi.DataSource = null;
            dgvTroChoi.DataSource = list;

            if (dgvTroChoi.Columns["MaTroChoi"] != null) dgvTroChoi.Columns["MaTroChoi"].HeaderText = "Mã Trò Chơi";
            if (dgvTroChoi.Columns["TenTroChoi"] != null) dgvTroChoi.Columns["TenTroChoi"].HeaderText = "Tên Trò Chơi";
            if (dgvTroChoi.Columns["LoaiTroChoi"] != null) dgvTroChoi.Columns["LoaiTroChoi"].HeaderText = "Loại Trò Chơi";
            if (dgvTroChoi.Columns["GiaVe"] != null)
            {
                dgvTroChoi.Columns["GiaVe"].HeaderText = "Giá Vé (VNĐ)";
                dgvTroChoi.Columns["GiaVe"].DefaultCellStyle.Format = "#,##0";
                dgvTroChoi.Columns["GiaVe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvTroChoi.Columns["DoTuoiToiThieu"] != null)
            {
                dgvTroChoi.Columns["DoTuoiToiThieu"].HeaderText = "Tuổi Tối Thiểu";
                dgvTroChoi.Columns["DoTuoiToiThieu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvTroChoi.Columns["SucChua"] != null)
            {
                dgvTroChoi.Columns["SucChua"].HeaderText = "Sức Chứa";
                dgvTroChoi.Columns["SucChua"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvTroChoi.Columns["TrangThai"] != null) dgvTroChoi.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dgvTroChoi.Columns["MoTa"] != null) dgvTroChoi.Columns["MoTa"].HeaderText = "Mô Tả";

            lblTotal.Text = $"Tổng số trò chơi: {list.Count}";
        }

        private void DgvTroChoi_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvTroChoi.Rows.Count)
            {
                var row = dgvTroChoi.Rows[e.RowIndex];
                txtMaTroChoi.Text = row.Cells["MaTroChoi"]?.Value?.ToString() ?? "";
                txtTenTroChoi.Text = row.Cells["TenTroChoi"]?.Value?.ToString() ?? "";

                string loai = row.Cells["LoaiTroChoi"]?.Value?.ToString() ?? "Cảm giác mạnh";
                if (cboLoaiTroChoi.Items.Contains(loai)) cboLoaiTroChoi.SelectedItem = loai;

                if (decimal.TryParse(row.Cells["GiaVe"]?.Value?.ToString(), out decimal gv)) numGiaVe.Value = gv;
                if (int.TryParse(row.Cells["DoTuoiToiThieu"]?.Value?.ToString(), out int dt)) numDoTuoi.Value = dt;
                if (int.TryParse(row.Cells["SucChua"]?.Value?.ToString(), out int sc)) numSucChua.Value = sc;

                string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? "Đang hoạt động";
                if (cboTrangThai.Items.Contains(tt)) cboTrangThai.SelectedItem = tt;

                txtMoTa.Text = row.Cells["MoTa"]?.Value?.ToString() ?? "";
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTroChoi.Text))
                txtMaTroChoi.Text = _gameService.GenerateNextId();

            var tc = new TroChoi
            {
                MaTroChoi = txtMaTroChoi.Text.Trim(),
                TenTroChoi = txtTenTroChoi.Text.Trim(),
                LoaiTroChoi = cboLoaiTroChoi.SelectedItem?.ToString() ?? "Cảm giác mạnh",
                GiaVe = numGiaVe.Value,
                DoTuoiToiThieu = (int)numDoTuoi.Value,
                SucChua = (int)numSucChua.Value,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang hoạt động",
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
            };

            var (success, msg) = _gameService.Add(tc);
            if (success)
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi kiểm tra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTroChoi.Text))
            {
                MessageBox.Show("Vui lòng chọn trò chơi cần cập nhật từ danh sách.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có muốn cập nhật thông tin trò chơi '{txtTenTroChoi.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var tc = new TroChoi
            {
                MaTroChoi = txtMaTroChoi.Text.Trim(),
                TenTroChoi = txtTenTroChoi.Text.Trim(),
                LoaiTroChoi = cboLoaiTroChoi.SelectedItem?.ToString() ?? "Cảm giác mạnh",
                GiaVe = numGiaVe.Value,
                DoTuoiToiThieu = (int)numDoTuoi.Value,
                SucChua = (int)numSucChua.Value,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang hoạt động",
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
            };

            var (success, msg) = _gameService.Update(tc);
            if (success)
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTroChoi.Text))
            {
                MessageBox.Show("Vui lòng chọn trò chơi cần xóa.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa trò chơi '{txtTenTroChoi.Text}' ({txtMaTroChoi.Text})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var (success, msg) = _gameService.Delete(txtMaTroChoi.Text.Trim());
            if (success)
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(msg, "Không thể xóa trò chơi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaTroChoi.Text = _gameService.GenerateNextId();
            txtTenTroChoi.Text = "";
            cboLoaiTroChoi.SelectedIndex = 0;
            numGiaVe.Value = 50000;
            numDoTuoi.Value = 6;
            numSucChua.Value = 20;
            cboTrangThai.SelectedIndex = 0;
            txtMoTa.Text = "";
            txtTenTroChoi.Focus();
        }
    }
}
