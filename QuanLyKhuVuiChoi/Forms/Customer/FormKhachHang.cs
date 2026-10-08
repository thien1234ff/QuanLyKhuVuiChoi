using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Customer
{
    public class FormKhachHang : Form
    {
        private readonly CustomerService _customerService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI Controls
        private DataGridView dgvKhachHang = null!;
        private TextBox txtMaKH = null!;
        private TextBox txtHoTen = null!;
        private DateTimePicker dtpNgaySinh = null!;
        private ComboBox cboGioiTinh = null!;
        private TextBox txtSoDienThoai = null!;
        private TextBox txtEmail = null!;
        private TextBox txtDiaChi = null!;
        private TextBox txtSearch = null!;
        private ComboBox cboSearchType = null!;
        private Label lblTotal = null!;

        public FormKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _customerService = new CustomerService(_context, _excelService);

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "THÀNH VIÊN 1: QUẢN LÝ KHÁCH HÀNG";
            this.Size = new Size(1100, 700);
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
                Text = "QUẢN LÝ THÔNG TIN KHÁCH HÀNG",
                Font = UITheme.HeaderFont,
                ForeColor = UITheme.Primary,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            lblTotal = new Label
            {
                Text = "Tổng số khách hàng: 0",
                Font = UITheme.BoldFont,
                ForeColor = UITheme.TextMuted,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(880, 20)
            };
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblTotal);

            // Left Input Panel
            var pnlInput = new GroupBox
            {
                Text = "Thông Tin Khách Hàng",
                Dock = DockStyle.Left,
                Width = 360,
                BackColor = Color.White,
                Font = UITheme.SubHeaderFont,
                ForeColor = UITheme.TextMain,
                Padding = new Padding(15)
            };

            int y = 30;
            int labelW = 100;
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

            // MaKH
            pnlInput.Controls.Add(CreateLabel("Mã khách hàng:", y));
            txtMaKH = new TextBox { Location = new Point(125, y), Size = new Size(130, 25), Font = UITheme.MainFont, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };
            var btnAutoId = new Button { Text = "Mới", Location = new Point(265, y), Size = new Size(65, 26), Font = UITheme.MainFont, Cursor = Cursors.Hand };
            UITheme.StyleButton(btnAutoId, UITheme.Primary, Color.White);
            btnAutoId.Click += (s, e) => { ClearInputs(); txtMaKH.Text = _customerService.GenerateNextId(); };
            pnlInput.Controls.Add(txtMaKH);
            pnlInput.Controls.Add(btnAutoId);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Họ và tên (*):", y));
            txtHoTen = new TextBox { Location = new Point(125, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtHoTen);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Ngày sinh:", y));
            dtpNgaySinh = new DateTimePicker { Location = new Point(125, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Format = DateTimePickerFormat.Short };
            pnlInput.Controls.Add(dtpNgaySinh);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Giới tính:", y));
            cboGioiTinh = new ComboBox { Location = new Point(125, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
            pnlInput.Controls.Add(cboGioiTinh);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Số điện thoại (*):", y));
            txtSoDienThoai = new TextBox { Location = new Point(125, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtSoDienThoai);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Email:", y));
            txtEmail = new TextBox { Location = new Point(125, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtEmail);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Địa chỉ:", y));
            txtDiaChi = new TextBox { Location = new Point(125, y), Size = new Size(inputW, 55), Font = UITheme.MainFont, Multiline = true };
            pnlInput.Controls.Add(txtDiaChi);

            // Action Buttons
            y += 70;
            var btnThem = new Button { Text = "➕ Thêm", Location = new Point(15, y), Size = new Size(95, 30) };
            UITheme.StyleButton(btnThem, UITheme.Primary, Color.White, 30);
            btnThem.Click += BtnThem_Click;

            var btnSua = new Button { Text = "✏️ Sửa", Location = new Point(120, y), Size = new Size(95, 30) };
            UITheme.StyleButton(btnSua, Color.FromArgb(245, 158, 11), Color.White, 30);
            btnSua.Click += BtnSua_Click;

            var btnXoa = new Button { Text = "🗑️ Xóa", Location = new Point(225, y), Size = new Size(95, 30) };
            UITheme.StyleDangerButton(btnXoa, 30);
            btnXoa.Click += BtnXoa_Click;

            y += 38;
            var btnLamMoi = new Button { Text = "🔄 Làm Mới Form", Location = new Point(15, y), Size = new Size(305, 30) };
            UITheme.StyleSecondaryButton(btnLamMoi, 30);
            btnLamMoi.Click += (s, e) => ClearInputs();

            y += 42;
            var btnMoImport = new Button { Text = "📥 Nhập Excel", Location = new Point(15, y), Size = new Size(145, 32) };
            UITheme.StyleButton(btnMoImport, Color.FromArgb(13, 148, 136), Color.White, 32);
            btnMoImport.Click += (s, e) =>
            {
                var f = new FormImportKhachHang(_context, _excelService);
                f.ShowDialog(this);
                LoadData();
            };

            var btnMoExport = new Button { Text = "📤 Xuất Excel", Location = new Point(175, y), Size = new Size(145, 32) };
            UITheme.StyleButton(btnMoExport, Color.FromArgb(2, 132, 199), Color.White, 32);
            btnMoExport.Click += (s, e) =>
            {
                var f = new FormExportKhachHang(_context, _excelService);
                f.ShowDialog(this);
            };

            pnlInput.Controls.Add(btnThem);
            pnlInput.Controls.Add(btnSua);
            pnlInput.Controls.Add(btnXoa);
            pnlInput.Controls.Add(btnLamMoi);
            pnlInput.Controls.Add(btnMoImport);
            pnlInput.Controls.Add(btnMoExport);

            // Right Panel (Search + DataGridView)
            var pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 10, 15, 15)
            };

            // Search Bar Box
            var pnlSearch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            var lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(10, 15), AutoSize = true, Font = UITheme.BoldFont };
            txtSearch = new TextBox { Location = new Point(85, 12), Size = new Size(250, 25), Font = UITheme.MainFont };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) PerformSearch(); };

            cboSearchType = new ComboBox { Location = new Point(345, 12), Size = new Size(130, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboSearchType.Items.AddRange(new object[] { "Chứa (Contains)", "Bắt đầu (Starts)", "Chính xác (Exact)" });
            cboSearchType.SelectedIndex = 0;

            var btnSearch = new Button { Text = "🔍 Tìm", Location = new Point(485, 10), Size = new Size(80, 28) };
            UITheme.StyleButton(btnSearch, UITheme.Primary, Color.White, 28);
            btnSearch.Click += (s, e) => PerformSearch();

            var btnHienTatCa = new Button { Text = "Tất cả", Location = new Point(575, 10), Size = new Size(80, 28) };
            UITheme.StyleSecondaryButton(btnHienTatCa, 28);
            btnHienTatCa.Click += (s, e) => { txtSearch.Text = ""; LoadData(); };

            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(cboSearchType);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(btnHienTatCa);

            // DataGridView
            dgvKhachHang = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvKhachHang);
            dgvKhachHang.CellClick += DgvKhachHang_CellClick;

            pnlRight.Controls.Add(dgvKhachHang);
            pnlRight.Controls.Add(pnlSearch);

            // Add to Form
            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlHeader);

            ClearInputs();
        }

        private void LoadData()
        {
            var list = _customerService.GetAll();
            BindGrid(list);
        }

        private void PerformSearch()
        {
            string type = "Contains";
            if (cboSearchType.SelectedIndex == 1) type = "StartsWith";
            else if (cboSearchType.SelectedIndex == 2) type = "Exact";

            var list = _customerService.Search(txtSearch.Text, type);
            BindGrid(list);
        }

        private void BindGrid(System.Collections.Generic.List<KhachHang> list)
        {
            dgvKhachHang.DataSource = null;
            dgvKhachHang.DataSource = list;

            if (dgvKhachHang.Columns["HoaDons"] != null)
                dgvKhachHang.Columns["HoaDons"].Visible = false;

            if (dgvKhachHang.Columns["MaKH"] != null) dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            if (dgvKhachHang.Columns["HoTen"] != null) dgvKhachHang.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvKhachHang.Columns["NgaySinh"] != null)
            {
                dgvKhachHang.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                dgvKhachHang.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvKhachHang.Columns["GioiTinh"] != null) dgvKhachHang.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgvKhachHang.Columns["SoDienThoai"] != null) dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            if (dgvKhachHang.Columns["Email"] != null) dgvKhachHang.Columns["Email"].HeaderText = "Email";
            if (dgvKhachHang.Columns["DiaChi"] != null) dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            if (dgvKhachHang.Columns["NgayDangKy"] != null)
            {
                dgvKhachHang.Columns["NgayDangKy"].HeaderText = "Ngày Đăng Ký";
                dgvKhachHang.Columns["NgayDangKy"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            lblTotal.Text = $"Tổng số khách hàng: {list.Count}";
        }

        private void DgvKhachHang_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count)
            {
                var row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"]?.Value?.ToString() ?? "";
                txtHoTen.Text = row.Cells["HoTen"]?.Value?.ToString() ?? "";

                if (row.Cells["NgaySinh"]?.Value is DateTime dt)
                    dtpNgaySinh.Value = dt;

                string gt = row.Cells["GioiTinh"]?.Value?.ToString() ?? "Nam";
                cboGioiTinh.SelectedItem = gt;

                txtSoDienThoai.Text = row.Cells["SoDienThoai"]?.Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"]?.Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"]?.Value?.ToString() ?? "";
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
                txtMaKH.Text = _customerService.GenerateNextId();

            var kh = new KhachHang
            {
                MaKH = txtMaKH.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                NgayDangKy = DateTime.Now
            };

            var (success, msg) = _customerService.Add(kh);
            if (success)
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần chỉnh sửa từ bảng.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn cập nhật thông tin khách hàng '{txtHoTen.Text}' ({txtMaKH.Text}) không?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var kh = new KhachHang
            {
                MaKH = txtMaKH.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim()
            };

            var (success, msg) = _customerService.Update(kh);
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
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa từ bảng.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng '{txtHoTen.Text}' ({txtMaKH.Text})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var (success, msg) = _customerService.Delete(txtMaKH.Text.Trim());
            if (success)
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(msg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaKH.Text = _customerService.GenerateNextId();
            txtHoTen.Text = "";
            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            cboGioiTinh.SelectedIndex = 0;
            txtSoDienThoai.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            txtHoTen.Focus();
        }
    }
}
