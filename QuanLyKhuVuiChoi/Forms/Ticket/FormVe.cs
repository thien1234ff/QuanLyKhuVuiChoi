using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Ticket
{
    public class FormVe : Form
    {
        private readonly TicketService _ticketService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI Controls
        private DataGridView dgvVe = null!;
        private TextBox txtMaVe = null!;
        private TextBox txtTenVe = null!;
        private ComboBox cboLoaiVe = null!;
        private NumericUpDown numGiaVe = null!;
        private TextBox txtThoiHan = null!;
        private ComboBox cboTrangThai = null!;
        private TextBox txtMoTa = null!;

        private TextBox txtSearch = null!;
        private ComboBox cboFilterLoai = null!;
        private ComboBox cboFilterTrangThai = null!;
        private Label lblTotal = null!;

        public FormVe(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _ticketService = new TicketService(_context, _excelService);

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "THÀNH VIÊN 3: QUẢN LÝ VÉ";
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
                Text = "QUẢN LÝ CÁC LOẠI VÉ VÀ BẢNG GIÁ",
                Font = UITheme.HeaderFont,
                ForeColor = Color.FromArgb(79, 70, 229),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            lblTotal = new Label
            {
                Text = "Tổng số loại vé: 0",
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
                Text = "Thông Tin Loại Vé",
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

            // MaVe
            pnlInput.Controls.Add(CreateLabel("Mã vé:", y));
            txtMaVe = new TextBox { Location = new Point(135, y), Size = new Size(130, 25), Font = UITheme.MainFont, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };
            var btnAutoId = new Button { Text = "Mới", Location = new Point(275, y), Size = new Size(65, 26), Font = UITheme.MainFont, Cursor = Cursors.Hand };
            UITheme.StyleButton(btnAutoId, UITheme.Primary, Color.White);
            btnAutoId.Click += (s, e) => { ClearInputs(); txtMaVe.Text = _ticketService.GenerateNextId(); };
            pnlInput.Controls.Add(txtMaVe);
            pnlInput.Controls.Add(btnAutoId);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Tên vé (*):", y));
            txtTenVe = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtTenVe);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Loại vé:", y));
            cboLoaiVe = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiVe.Items.AddRange(new object[] { "Vé ngày", "Vé trẻ em", "Vé người lớn", "Vé gia đình", "Vé VIP" });
            cboLoaiVe.SelectedIndex = 0;
            pnlInput.Controls.Add(cboLoaiVe);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Giá vé (VNĐ):", y));
            numGiaVe = new NumericUpDown { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Maximum = 10000000, Increment = 10000, ThousandsSeparator = true };
            pnlInput.Controls.Add(numGiaVe);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Thời hạn:", y));
            txtThoiHan = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Text = "Trong ngày" };
            pnlInput.Controls.Add(txtThoiHan);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Trạng thái:", y));
            cboTrangThai = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThai.Items.AddRange(new object[] { "Đang áp dụng", "Ngừng áp dụng" });
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

            var lblLoai = new Label { Text = "Loại vé:", Location = new Point(220, 18), AutoSize = true, Font = UITheme.BoldFont };
            cboFilterLoai = new ComboBox { Location = new Point(285, 15), Size = new Size(130, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboFilterLoai.Items.AddRange(new object[] { "Tất cả", "Vé ngày", "Vé trẻ em", "Vé người lớn", "Vé gia đình", "Vé VIP" });
            cboFilterLoai.SelectedIndex = 0;
            cboFilterLoai.SelectedIndexChanged += (s, e) => ApplyFilter();

            var lblTrangThai = new Label { Text = "Trạng thái:", Location = new Point(430, 18), AutoSize = true, Font = UITheme.BoldFont };
            cboFilterTrangThai = new ComboBox { Location = new Point(510, 15), Size = new Size(130, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboFilterTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang áp dụng", "Ngừng áp dụng" });
            cboFilterTrangThai.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndexChanged += (s, e) => ApplyFilter();

            var btnReset = new Button { Text = "Đặt lại", Location = new Point(655, 12), Size = new Size(75, 28) };
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
            dgvVe = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvVe);
            dgvVe.CellClick += DgvVe_CellClick;

            pnlRight.Controls.Add(dgvVe);
            pnlRight.Controls.Add(pnlFilter);

            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlHeader);

            ClearInputs();
        }

        private void LoadData()
        {
            var list = _ticketService.GetAll();
            BindGrid(list);
        }

        private void ApplyFilter()
        {
            var list = _ticketService.Filter(
                txtSearch.Text,
                cboFilterLoai.SelectedItem?.ToString() ?? "Tất cả",
                cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả"
            );
            BindGrid(list);
        }

        private void BindGrid(System.Collections.Generic.List<Ve> list)
        {
            dgvVe.DataSource = null;
            dgvVe.DataSource = list;

            if (dgvVe.Columns["MaVe"] != null) dgvVe.Columns["MaVe"].HeaderText = "Mã Vé";
            if (dgvVe.Columns["TenVe"] != null) dgvVe.Columns["TenVe"].HeaderText = "Tên Loại Vé";
            if (dgvVe.Columns["LoaiVe"] != null) dgvVe.Columns["LoaiVe"].HeaderText = "Phân Loại";
            if (dgvVe.Columns["GiaVe"] != null)
            {
                dgvVe.Columns["GiaVe"].HeaderText = "Giá Vé (VNĐ)";
                dgvVe.Columns["GiaVe"].DefaultCellStyle.Format = "#,##0";
                dgvVe.Columns["GiaVe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvVe.Columns["ThoiHan"] != null) dgvVe.Columns["ThoiHan"].HeaderText = "Thời Hạn";
            if (dgvVe.Columns["TrangThai"] != null) dgvVe.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dgvVe.Columns["MoTa"] != null) dgvVe.Columns["MoTa"].HeaderText = "Mô Tả";

            lblTotal.Text = $"Tổng số loại vé: {list.Count}";
        }

        private void DgvVe_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvVe.Rows.Count)
            {
                var row = dgvVe.Rows[e.RowIndex];
                txtMaVe.Text = row.Cells["MaVe"]?.Value?.ToString() ?? "";
                txtTenVe.Text = row.Cells["TenVe"]?.Value?.ToString() ?? "";

                string loai = row.Cells["LoaiVe"]?.Value?.ToString() ?? "Vé ngày";
                if (cboLoaiVe.Items.Contains(loai)) cboLoaiVe.SelectedItem = loai;

                if (decimal.TryParse(row.Cells["GiaVe"]?.Value?.ToString(), out decimal gv)) numGiaVe.Value = gv;

                txtThoiHan.Text = row.Cells["ThoiHan"]?.Value?.ToString() ?? "Trong ngày";

                string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? "Đang áp dụng";
                if (cboTrangThai.Items.Contains(tt)) cboTrangThai.SelectedItem = tt;

                txtMoTa.Text = row.Cells["MoTa"]?.Value?.ToString() ?? "";
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaVe.Text))
                txtMaVe.Text = _ticketService.GenerateNextId();

            var ve = new Ve
            {
                MaVe = txtMaVe.Text.Trim(),
                TenVe = txtTenVe.Text.Trim(),
                LoaiVe = cboLoaiVe.SelectedItem?.ToString() ?? "Vé ngày",
                GiaVe = numGiaVe.Value,
                ThoiHan = string.IsNullOrWhiteSpace(txtThoiHan.Text) ? "Trong ngày" : txtThoiHan.Text.Trim(),
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang áp dụng",
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
            };

            var (success, msg) = _ticketService.Add(ve);
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
            if (string.IsNullOrWhiteSpace(txtMaVe.Text))
            {
                MessageBox.Show("Vui lòng chọn loại vé cần cập nhật từ danh sách.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có muốn cập nhật thông tin loại vé '{txtTenVe.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var ve = new Ve
            {
                MaVe = txtMaVe.Text.Trim(),
                TenVe = txtTenVe.Text.Trim(),
                LoaiVe = cboLoaiVe.SelectedItem?.ToString() ?? "Vé ngày",
                GiaVe = numGiaVe.Value,
                ThoiHan = string.IsNullOrWhiteSpace(txtThoiHan.Text) ? "Trong ngày" : txtThoiHan.Text.Trim(),
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang áp dụng",
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
            };

            var (success, msg) = _ticketService.Update(ve);
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
            if (string.IsNullOrWhiteSpace(txtMaVe.Text))
            {
                MessageBox.Show("Vui lòng chọn loại vé cần xóa.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa vé '{txtTenVe.Text}' ({txtMaVe.Text})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var (success, msg) = _ticketService.Delete(txtMaVe.Text.Trim());
            if (success)
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(msg, "Không thể xóa vé", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaVe.Text = _ticketService.GenerateNextId();
            txtTenVe.Text = "";
            cboLoaiVe.SelectedIndex = 0;
            numGiaVe.Value = 100000;
            txtThoiHan.Text = "Trong ngày";
            cboTrangThai.SelectedIndex = 0;
            txtMoTa.Text = "";
            txtTenVe.Focus();
        }
    }
}
