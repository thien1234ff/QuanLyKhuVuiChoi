using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Employee
{
    public partial class FormNhanVien : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;
        private readonly EmployeeService _employeeService;

        public FormNhanVien() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormNhanVien(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _excelService = excelService ?? new ExcelService();
            _employeeService = new EmployeeService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                cboGioiTinh.SelectedIndex = 0;
                cboChucVu.SelectedIndex = 1;
                cboTrangThai.SelectedIndex = 0;
                cboFilterChucVu.SelectedIndex = 0;
                cboFilterTrangThai.SelectedIndex = 0;
                SetupDataGridViewColumns();
                ClearInputs();
                LoadData();
            }
        }

        private void SetupDataGridViewColumns()
        {
            dgvNhanVien.Columns.Clear();
            dgvNhanVien.AutoGenerateColumns = false;

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNV", HeaderText = "MÃ NV", DataPropertyName = "MaNV", Width = 90 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "HỌ VÀ TÊN", DataPropertyName = "HoTen", Width = 160 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgaySinh", HeaderText = "NGÀY SINH", DataPropertyName = "NgaySinhStr", Width = 110 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "GioiTinh", HeaderText = "GIỚI TÍNH", DataPropertyName = "GioiTinh", Width = 80 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDienThoai", HeaderText = "SỐ ĐIỆN THOẠI", DataPropertyName = "SoDienThoai", Width = 120 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "ChucVu", HeaderText = "QUYỀN HẠN", DataPropertyName = "ChucVu", Width = 120 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "TRẠNG THÁI", DataPropertyName = "TrangThai", Width = 120 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiaChi", HeaderText = "ĐỊA CHỈ", DataPropertyName = "DiaChi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        public void LoadData()
        {
            try
            {
                var list = _employeeService.GetAll()
                    .Select(n => new
                    {
                        n.MaNV,
                        n.HoTen,
                        NgaySinhStr = n.NgaySinh.HasValue ? n.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                        n.GioiTinh,
                        n.SoDienThoai,
                        n.ChucVu,
                        n.TrangThai,
                        Email = n.Email ?? "",
                        DiaChi = n.DiaChi ?? "",
                        NgayVaoLamStr = n.NgayVaoLam.ToString("dd/MM/yyyy")
                    })
                    .ToList();

                dgvNhanVien.DataSource = list;
                lblTotal.Text = $"Tổng số nhân viên: {list.Count:N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaNV.Text = _employeeService.GenerateNextId();
            txtHoTen.Clear();
            dtpNgaySinh.Value = new DateTime(1995, 1, 1);
            cboGioiTinh.SelectedIndex = 0;
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            cboChucVu.SelectedIndex = 1;
            dtpNgayVaoLam.Value = DateTime.Today;
            cboTrangThai.SelectedIndex = 0;
            txtDiaChi.Clear();
            txtHoTen.Focus();
        }

        private void BtnAutoId_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            var nv = new NhanVien
            {
                MaNV = string.IsNullOrWhiteSpace(txtMaNV.Text) ? _employeeService.GenerateNextId() : txtMaNV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                ChucVu = cboChucVu.SelectedItem?.ToString() ?? "Nhân viên",
                NgayVaoLam = dtpNgayVaoLam.Value.Date,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang làm việc",
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim()
            };

            var (success, message) = _employeeService.Add(nv);
            if (success)
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(message, "Cảnh báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            var maNV = txtMaNV.Text.Trim();
            if (string.IsNullOrWhiteSpace(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nv = new NhanVien
            {
                MaNV = maNV,
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                ChucVu = cboChucVu.SelectedItem?.ToString() ?? "Nhân viên",
                NgayVaoLam = dtpNgayVaoLam.Value.Date,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang làm việc",
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim()
            };

            var (success, message) = _employeeService.Update(nv);
            if (success)
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show(message, "Cảnh báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            var maNV = txtMaNV.Text.Trim();
            if (string.IsNullOrWhiteSpace(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{txtHoTen.Text}' ({maNV})?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var (success, message) = _employeeService.Delete(maNV);
                if (success)
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BtnLoc_Click(object? sender, EventArgs e)
        {
            PerformFilter();
        }

        private void BtnLamMoiLoc_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterChucVu.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndex = 0;
            LoadData();
        }

        private void PerformFilter()
        {
            var kw = txtSearch.Text.Trim();
            var chucVu = cboFilterChucVu.SelectedItem?.ToString() ?? "Tất cả";
            var trangThai = cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả";

            var list = _employeeService.Filter(kw, chucVu, trangThai)
                .Select(n => new
                {
                    n.MaNV,
                    n.HoTen,
                    NgaySinhStr = n.NgaySinh.HasValue ? n.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                    n.GioiTinh,
                    n.SoDienThoai,
                    n.ChucVu,
                    n.TrangThai,
                    Email = n.Email ?? "",
                    DiaChi = n.DiaChi ?? "",
                    NgayVaoLamStr = n.NgayVaoLam.ToString("dd/MM/yyyy")
                })
                .ToList();

            dgvNhanVien.DataSource = list;
            lblTotal.Text = $"Kết quả lọc: {list.Count:N0}";
        }

        private void DgvNhanVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhanVien.Rows.Count) return;

            var row = dgvNhanVien.Rows[e.RowIndex];
            var maNV = row.Cells["MaNV"].Value?.ToString() ?? "";
            var nv = _employeeService.GetById(maNV);
            if (nv != null)
            {
                txtMaNV.Text = nv.MaNV;
                txtHoTen.Text = nv.HoTen;
                if (nv.NgaySinh.HasValue) dtpNgaySinh.Value = nv.NgaySinh.Value;
                cboGioiTinh.SelectedItem = nv.GioiTinh;
                txtSoDienThoai.Text = nv.SoDienThoai;
                txtEmail.Text = nv.Email ?? "";
                if (nv.ChucVu == "Admin" || nv.ChucVu == "Quản lý")
                    cboChucVu.SelectedItem = "Admin";
                else
                    cboChucVu.SelectedItem = "Nhân viên";
                dtpNgayVaoLam.Value = nv.NgayVaoLam;
                cboTrangThai.SelectedItem = nv.TrangThai;
                txtDiaChi.Text = nv.DiaChi ?? "";
            }
        }
    }
}
