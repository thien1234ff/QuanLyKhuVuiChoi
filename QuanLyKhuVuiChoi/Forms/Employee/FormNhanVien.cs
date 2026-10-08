using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Employee
{
    public class FormNhanVien : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI Controls
        private DataGridView dgvNhanVien = null!;
        private TextBox txtMaNV = null!;
        private TextBox txtHoTen = null!;
        private DateTimePicker dtpNgaySinh = null!;
        private ComboBox cboGioiTinh = null!;
        private TextBox txtSoDienThoai = null!;
        private TextBox txtEmail = null!;
        private TextBox txtDiaChi = null!;
        private ComboBox cboChucVu = null!;
        private DateTimePicker dtpNgayVaoLam = null!;
        private ComboBox cboTrangThai = null!;

        private TextBox txtSearch = null!;
        private ComboBox cboFilterChucVu = null!;
        private ComboBox cboFilterTrangThai = null!;
        private Label lblTotal = null!;

        public FormNhanVien(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _employeeService = new EmployeeService(_context, _excelService);

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "THÀNH VIÊN 3: QUẢN LÝ NHÂN VIÊN";
            this.Size = new Size(1150, 720);
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
                Text = "QUẢN LÝ DANH SÁCH NHÂN VIÊN CÔNG VIÊN",
                Font = UITheme.HeaderFont,
                ForeColor = Color.FromArgb(79, 70, 229), // Indigo Theme
                AutoSize = true,
                Location = new Point(20, 15)
            };
            lblTotal = new Label
            {
                Text = "Tổng số nhân viên: 0",
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
                Text = "Thông Tin Nhân Viên",
                Dock = DockStyle.Left,
                Width = 370,
                BackColor = Color.White,
                Font = UITheme.SubHeaderFont,
                ForeColor = UITheme.TextMain,
                Padding = new Padding(15)
            };

            int y = 28;
            int labelW = 110;
            int inputW = 210;
            int gap = 34;

            Label CreateLabel(string text, int top) => new Label
            {
                Text = text,
                Location = new Point(15, top + 3),
                Size = new Size(labelW, 20),
                Font = UITheme.MainFont,
                ForeColor = UITheme.TextMain
            };

            // MaNV
            pnlInput.Controls.Add(CreateLabel("Mã nhân viên:", y));
            txtMaNV = new TextBox { Location = new Point(135, y), Size = new Size(130, 25), Font = UITheme.MainFont, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };
            var btnAutoId = new Button { Text = "Mới", Location = new Point(275, y), Size = new Size(65, 26), Font = UITheme.MainFont, Cursor = Cursors.Hand };
            UITheme.StyleButton(btnAutoId, UITheme.Primary, Color.White);
            btnAutoId.Click += (s, e) => { ClearInputs(); txtMaNV.Text = _employeeService.GenerateNextId(); };
            pnlInput.Controls.Add(txtMaNV);
            pnlInput.Controls.Add(btnAutoId);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Họ và tên (*):", y));
            txtHoTen = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtHoTen);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Ngày sinh:", y));
            dtpNgaySinh = new DateTimePicker { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Format = DateTimePickerFormat.Short };
            pnlInput.Controls.Add(dtpNgaySinh);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Giới tính:", y));
            cboGioiTinh = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
            pnlInput.Controls.Add(cboGioiTinh);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Số điện thoại (*):", y));
            txtSoDienThoai = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtSoDienThoai);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Email:", y));
            txtEmail = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont };
            pnlInput.Controls.Add(txtEmail);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Chức vụ:", y));
            cboChucVu = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboChucVu.Items.AddRange(new object[] { "Quản lý", "Thu ngân", "Soát vé", "Kỹ thuật", "Bảo vệ", "Chăm sóc khách hàng" });
            cboChucVu.SelectedIndex = 1;
            pnlInput.Controls.Add(cboChucVu);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Ngày vào làm:", y));
            dtpNgayVaoLam = new DateTimePicker { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, Format = DateTimePickerFormat.Short };
            pnlInput.Controls.Add(dtpNgayVaoLam);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Trạng thái:", y));
            cboTrangThai = new ComboBox { Location = new Point(135, y), Size = new Size(inputW, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThai.Items.AddRange(new object[] { "Đang làm việc", "Đã nghỉ việc" });
            cboTrangThai.SelectedIndex = 0;
            pnlInput.Controls.Add(cboTrangThai);

            y += gap;
            pnlInput.Controls.Add(CreateLabel("Địa chỉ:", y));
            txtDiaChi = new TextBox { Location = new Point(135, y), Size = new Size(inputW, 45), Font = UITheme.MainFont, Multiline = true };
            pnlInput.Controls.Add(txtDiaChi);

            // Action Buttons
            y += 58;
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

            var lblChucVu = new Label { Text = "Chức vụ:", Location = new Point(220, 18), AutoSize = true, Font = UITheme.BoldFont };
            cboFilterChucVu = new ComboBox { Location = new Point(285, 15), Size = new Size(140, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboFilterChucVu.Items.AddRange(new object[] { "Tất cả", "Quản lý", "Thu ngân", "Soát vé", "Kỹ thuật", "Bảo vệ", "Chăm sóc khách hàng" });
            cboFilterChucVu.SelectedIndex = 0;
            cboFilterChucVu.SelectedIndexChanged += (s, e) => ApplyFilter();

            var lblTrangThai = new Label { Text = "Trạng thái:", Location = new Point(440, 18), AutoSize = true, Font = UITheme.BoldFont };
            cboFilterTrangThai = new ComboBox { Location = new Point(515, 15), Size = new Size(130, 25), Font = UITheme.MainFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cboFilterTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang làm việc", "Đã nghỉ việc" });
            cboFilterTrangThai.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndexChanged += (s, e) => ApplyFilter();

            var btnReset = new Button { Text = "Đặt lại", Location = new Point(660, 12), Size = new Size(75, 28) };
            UITheme.StyleSecondaryButton(btnReset, 28);
            btnReset.Click += (s, e) => { txtSearch.Text = ""; cboFilterChucVu.SelectedIndex = 0; cboFilterTrangThai.SelectedIndex = 0; LoadData(); };

            pnlFilter.Controls.Add(lblSearch);
            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(lblChucVu);
            pnlFilter.Controls.Add(cboFilterChucVu);
            pnlFilter.Controls.Add(lblTrangThai);
            pnlFilter.Controls.Add(cboFilterTrangThai);
            pnlFilter.Controls.Add(btnReset);

            // DataGridView
            dgvNhanVien = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvNhanVien);
            dgvNhanVien.CellClick += DgvNhanVien_CellClick;

            pnlRight.Controls.Add(dgvNhanVien);
            pnlRight.Controls.Add(pnlFilter);

            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlHeader);

            ClearInputs();
        }

        private void LoadData()
        {
            var list = _employeeService.GetAll();
            BindGrid(list);
        }

        private void ApplyFilter()
        {
            var list = _employeeService.Filter(
                txtSearch.Text,
                cboFilterChucVu.SelectedItem?.ToString() ?? "Tất cả",
                cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả"
            );
            BindGrid(list);
        }

        private void BindGrid(System.Collections.Generic.List<NhanVien> list)
        {
            dgvNhanVien.DataSource = null;
            dgvNhanVien.DataSource = list;

            if (dgvNhanVien.Columns["HoaDons"] != null) dgvNhanVien.Columns["HoaDons"].Visible = false;
            if (dgvNhanVien.Columns["TaiKhoan"] != null) dgvNhanVien.Columns["TaiKhoan"].Visible = false;

            if (dgvNhanVien.Columns["MaNV"] != null) dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            if (dgvNhanVien.Columns["HoTen"] != null) dgvNhanVien.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvNhanVien.Columns["NgaySinh"] != null)
            {
                dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvNhanVien.Columns["GioiTinh"] != null) dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgvNhanVien.Columns["SoDienThoai"] != null) dgvNhanVien.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            if (dgvNhanVien.Columns["Email"] != null) dgvNhanVien.Columns["Email"].HeaderText = "Email";
            if (dgvNhanVien.Columns["ChucVu"] != null) dgvNhanVien.Columns["ChucVu"].HeaderText = "Chức Vụ";
            if (dgvNhanVien.Columns["NgayVaoLam"] != null)
            {
                dgvNhanVien.Columns["NgayVaoLam"].HeaderText = "Ngày Vào Làm";
                dgvNhanVien.Columns["NgayVaoLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvNhanVien.Columns["TrangThai"] != null) dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dgvNhanVien.Columns["DiaChi"] != null) dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";

            lblTotal.Text = $"Tổng số nhân viên: {list.Count}";
        }

        private void DgvNhanVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count)
            {
                var row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNV"]?.Value?.ToString() ?? "";
                txtHoTen.Text = row.Cells["HoTen"]?.Value?.ToString() ?? "";

                if (row.Cells["NgaySinh"]?.Value is DateTime dt) dtpNgaySinh.Value = dt;

                string gt = row.Cells["GioiTinh"]?.Value?.ToString() ?? "Nam";
                cboGioiTinh.SelectedItem = gt;

                txtSoDienThoai.Text = row.Cells["SoDienThoai"]?.Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"]?.Value?.ToString() ?? "";

                string cv = row.Cells["ChucVu"]?.Value?.ToString() ?? "Thu ngân";
                if (cboChucVu.Items.Contains(cv)) cboChucVu.SelectedItem = cv;

                if (row.Cells["NgayVaoLam"]?.Value is DateTime dtVao) dtpNgayVaoLam.Value = dtVao;

                string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? "Đang làm việc";
                if (cboTrangThai.Items.Contains(tt)) cboTrangThai.SelectedItem = tt;

                txtDiaChi.Text = row.Cells["DiaChi"]?.Value?.ToString() ?? "";
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
                txtMaNV.Text = _employeeService.GenerateNextId();

            var nv = new NhanVien
            {
                MaNV = txtMaNV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                ChucVu = cboChucVu.SelectedItem?.ToString() ?? "Thu ngân",
                NgayVaoLam = dtpNgayVaoLam.Value,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang làm việc",
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim()
            };

            var (success, msg) = _employeeService.Add(nv);
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
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật từ danh sách.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có muốn cập nhật nhân viên '{txtHoTen.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var nv = new NhanVien
            {
                MaNV = txtMaNV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                ChucVu = cboChucVu.SelectedItem?.ToString() ?? "Thu ngân",
                NgayVaoLam = dtpNgayVaoLam.Value,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang làm việc",
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim()
            };

            var (success, msg) = _employeeService.Update(nv);
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
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{txtHoTen.Text}' ({txtMaNV.Text})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var (success, msg) = _employeeService.Delete(txtMaNV.Text.Trim());
            if (success)
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(msg, "Không thể xóa nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaNV.Text = _employeeService.GenerateNextId();
            txtHoTen.Text = "";
            dtpNgaySinh.Value = new DateTime(1995, 1, 1);
            cboGioiTinh.SelectedIndex = 0;
            txtSoDienThoai.Text = "";
            txtEmail.Text = "";
            cboChucVu.SelectedIndex = 1;
            dtpNgayVaoLam.Value = DateTime.Today;
            cboTrangThai.SelectedIndex = 0;
            txtDiaChi.Text = "";
            txtHoTen.Focus();
        }
    }
}
