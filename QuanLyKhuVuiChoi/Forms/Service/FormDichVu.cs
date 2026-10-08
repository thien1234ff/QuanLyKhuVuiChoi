using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Service
{
    public class FormDichVu : Form
    {
        private readonly ParkServiceService _parkService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI Controls
        private DataGridView dgvDichVu = null!;
        private TextBox txtMaDichVu = null!;
        private TextBox txtTenDichVu = null!;
        private ComboBox cboLoaiDichVu = null!;
        private NumericUpDown numDonGia = null!;
        private NumericUpDown numSoLuongTon = null!;
        private ComboBox cboTrangThai = null!;
        private TextBox txtMoTa = null!;

        private TextBox txtSearch = null!;
        private ComboBox cboFilterLoai = null!;
        private ComboBox cboFilterTrangThai = null!;
        private Label lblTotal = null!;

        public FormDichVu(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _parkService = new ParkServiceService(_context, _excelService);

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "THÀNH VIÊN 2: QUẢN LÝ DỊCH VỤ";
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
                Text = "QUẢN LÝ DANH MỤC DỊCH VỤ & TIỆN ÍCH",
                Font = UITheme.HeaderFont,
                ForeColor = Color.FromArgb(13, 148, 136), // Teal theme for Services
                AutoSize = true,
                Location = new Point(20, 15)
            };
            lblTotal = new Label
            {
                Text = "Tổng số dịch vụ: 0",
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
                Text = "Thông Tin Dịch Vụ",
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

            // MaDichVu
            pnlInput.Controls.Add(CreateLabel("Mã dịch vụ:", y));
            txtMaDichVu = new TextBox { Location = new Point(135, y), Size = new Size(130, 25), Font = UITheme.MainFont, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };
            var btnAutoId = new Button { Text = "Mới", Location = new Point(275, y), Size = new Size(65, 26), Font = UITheme.MainFont, Cursor = Cursors.Hand };
            UITheme.StyleButton(btnAutoId, UITheme.Primary, Color.White);
            btnAutoId.Click += (s, e) => { ClearInputs(); txtMaDichVu.Text = _parkService.GenerateNextId(); };
            pnlInput.Controls.Add(txtMaDichVu);
            pnlInput.Controls.Add(btnAutoId);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Tên dịch vụ (*):", y));
            txtTenDichVu = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtTenDichVu);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Loại dịch vụ:", y));
            cboLoaiDichVu = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiDichVu.Items.AddRange(new object[] { "Ẩm thực", "Nước uống", "Thuê đồ", "Khu nghỉ ngơi", "Ăn vặt", "Dịch vụ khác" });
            cboLoaiDichVu.SelectedIndex = 0;
            pnlInput.Controls.Add(cboLoaiDichVu);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Đơn giá (VNĐ):", y));
            numDonGia = new NumericUpDown { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Maximum = 10000000, Increment = 5000, ThousandsSeparator = true };
            pnlInput.Controls.Add(numDonGia);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Số lượng tồn:", y));
            numSoLuongTon = new NumericUpDown { Location = new Point(135, y), Size = new Size(120, 25), Font = UITheme.MainFont, Maximum = 100000, Value = 100 };
            var btnPlusStock = new Button { Text = "+50", Location = new Point(265, y), Size = new Size(75, 26), Font = UITheme.MainFont };
            UITheme.StyleButton(btnPlusStock, Color.FromArgb(59, 130, 246), Color.White);
            btnPlusStock.Click += (s, e) => numSoLuongTon.Value += 50;
            pnlInput.Controls.Add(numSoLuongTon);
            pnlInput.Controls.Add(btnPlusStock);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Trạng thái:", y));
            cboTrangThai = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThai.Items.AddRange(new object[] { "Đang phục vụ", "Hết hàng", "Tạm ngưng" });
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
            cboFilterLoai.Items.AddRange(new object[] { "Tất cả", "Ẩm thực", "Nước uống", "Thuê đồ", "Khu nghỉ ngơi", "Ăn vặt", "Dịch vụ khác" });
            cboFilterLoai.SelectedIndex = 0;
            cboFilterLoai.SelectedIndexChanged += (s, e) => ApplyFilter();

            var lblTrangThai = new Label { Text = "Trạng thái:", Location = new Point(405, 18), AutoSize = true, Font = UITheme.BoldFont };
            cboFilterTrangThai = new ComboBox { Location = new Point(485, 15), Size = new Size(130, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboFilterTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang phục vụ", "Hết hàng", "Tạm ngưng" });
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
            dgvDichVu = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvDichVu);
            dgvDichVu.CellClick += DgvDichVu_CellClick;

            pnlRight.Controls.Add(dgvDichVu);
            pnlRight.Controls.Add(pnlFilter);

            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlHeader);

            ClearInputs();
        }

        private void LoadData()
        {
            var list = _parkService.GetAll();
            BindGrid(list);
        }

        private void ApplyFilter()
        {
            var list = _parkService.Filter(
                txtSearch.Text,
                cboFilterLoai.SelectedItem?.ToString() ?? "Tất cả",
                cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả"
            );
            BindGrid(list);
        }

        private void BindGrid(System.Collections.Generic.List<DichVu> list)
        {
            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = list;

            if (dgvDichVu.Columns["MaDichVu"] != null) dgvDichVu.Columns["MaDichVu"].HeaderText = "Mã Dịch Vụ";
            if (dgvDichVu.Columns["TenDichVu"] != null) dgvDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            if (dgvDichVu.Columns["LoaiDichVu"] != null) dgvDichVu.Columns["LoaiDichVu"].HeaderText = "Loại Dịch Vụ";
            if (dgvDichVu.Columns["DonGia"] != null)
            {
                dgvDichVu.Columns["DonGia"].HeaderText = "Đơn Giá (VNĐ)";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDichVu.Columns["SoLuongTon"] != null)
            {
                dgvDichVu.Columns["SoLuongTon"].HeaderText = "Số Lượng Tồn";
                dgvDichVu.Columns["SoLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDichVu.Columns["TrangThai"] != null) dgvDichVu.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dgvDichVu.Columns["MoTa"] != null) dgvDichVu.Columns["MoTa"].HeaderText = "Mô Tả";

            lblTotal.Text = $"Tổng số dịch vụ: {list.Count}";
        }

        private void DgvDichVu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDichVu.Rows.Count)
            {
                var row = dgvDichVu.Rows[e.RowIndex];
                txtMaDichVu.Text = row.Cells["MaDichVu"]?.Value?.ToString() ?? "";
                txtTenDichVu.Text = row.Cells["TenDichVu"]?.Value?.ToString() ?? "";

                string loai = row.Cells["LoaiDichVu"]?.Value?.ToString() ?? "Ẩm thực";
                if (cboLoaiDichVu.Items.Contains(loai)) cboLoaiDichVu.SelectedItem = loai;

                if (decimal.TryParse(row.Cells["DonGia"]?.Value?.ToString(), out decimal dg)) numDonGia.Value = dg;
                if (int.TryParse(row.Cells["SoLuongTon"]?.Value?.ToString(), out int sl)) numSoLuongTon.Value = sl;

                string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? "Đang phục vụ";
                if (cboTrangThai.Items.Contains(tt)) cboTrangThai.SelectedItem = tt;

                txtMoTa.Text = row.Cells["MoTa"]?.Value?.ToString() ?? "";
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDichVu.Text))
                txtMaDichVu.Text = _parkService.GenerateNextId();

            var dv = new DichVu
            {
                MaDichVu = txtMaDichVu.Text.Trim(),
                TenDichVu = txtTenDichVu.Text.Trim(),
                LoaiDichVu = cboLoaiDichVu.SelectedItem?.ToString() ?? "Ẩm thực",
                DonGia = numDonGia.Value,
                SoLuongTon = (int)numSoLuongTon.Value,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang phục vụ",
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
            };

            var (success, msg) = _parkService.Add(dv);
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
            if (string.IsNullOrWhiteSpace(txtMaDichVu.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần cập nhật từ bảng.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có muốn cập nhật dịch vụ '{txtTenDichVu.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var dv = new DichVu
            {
                MaDichVu = txtMaDichVu.Text.Trim(),
                TenDichVu = txtTenDichVu.Text.Trim(),
                LoaiDichVu = cboLoaiDichVu.SelectedItem?.ToString() ?? "Ẩm thực",
                DonGia = numDonGia.Value,
                SoLuongTon = (int)numSoLuongTon.Value,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang phục vụ",
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
            };

            var (success, msg) = _parkService.Update(dv);
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
            if (string.IsNullOrWhiteSpace(txtMaDichVu.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ '{txtTenDichVu.Text}' ({txtMaDichVu.Text})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var (success, msg) = _parkService.Delete(txtMaDichVu.Text.Trim());
            if (success)
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(msg, "Không thể xóa dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaDichVu.Text = _parkService.GenerateNextId();
            txtTenDichVu.Text = "";
            cboLoaiDichVu.SelectedIndex = 0;
            numDonGia.Value = 30000;
            numSoLuongTon.Value = 100;
            cboTrangThai.SelectedIndex = 0;
            txtMoTa.Text = "";
            txtTenDichVu.Focus();
        }
    }
}
