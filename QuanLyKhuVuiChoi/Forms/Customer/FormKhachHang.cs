using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Customer
{
    public partial class FormKhachHang : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;
        private readonly CustomerService _customerService;

        public FormKhachHang() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _excelService = excelService ?? new ExcelService();
            _customerService = new CustomerService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                cboSearchType.SelectedIndex = 0;
                cboGioiTinh.SelectedIndex = 0;
                SetupDataGridViewColumns();
                ClearInputs();
                LoadData();
            }
        }

        private void SetupDataGridViewColumns()
        {
            dgvKhachHang.Columns.Clear();
            dgvKhachHang.AutoGenerateColumns = false;

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKH", HeaderText = "MÃ KH", DataPropertyName = "MaKH", Width = 90 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "HỌ VÀ TÊN", DataPropertyName = "HoTen", Width = 160 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgaySinh", HeaderText = "NGÀY SINH", DataPropertyName = "NgaySinhStr", Width = 110 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "GioiTinh", HeaderText = "GIỚI TÍNH", DataPropertyName = "GioiTinh", Width = 90 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDienThoai", HeaderText = "SỐ ĐIỆN THOẠI", DataPropertyName = "SoDienThoai", Width = 130 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "EMAIL", DataPropertyName = "Email", Width = 160 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiaChi", HeaderText = "ĐỊA CHỈ", DataPropertyName = "DiaChi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        public void LoadData()
        {
            try
            {
                var list = _customerService.GetAll()
                    .Select(k => new
                    {
                        k.MaKH,
                        k.HoTen,
                        NgaySinhStr = k.NgaySinh.HasValue ? k.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                        k.GioiTinh,
                        k.SoDienThoai,
                        Email = k.Email ?? "",
                        DiaChi = k.DiaChi ?? ""
                    })
                    .ToList();

                dgvKhachHang.DataSource = list;
                lblTotal.Text = $"Tổng số khách hàng: {list.Count:N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaKH.Text = _customerService.GenerateNextId();
            txtHoTen.Clear();
            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            cboGioiTinh.SelectedIndex = 0;
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtHoTen.Focus();
        }

        private void BtnAutoId_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            var kh = new KhachHang
            {
                MaKH = string.IsNullOrWhiteSpace(txtMaKH.Text) ? _customerService.GenerateNextId() : txtMaKH.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                NgayDangKy = DateTime.Now
            };

            var (success, message) = _customerService.Add(kh);
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
            var maKH = txtMaKH.Text.Trim();
            if (string.IsNullOrWhiteSpace(maKH))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kh = new KhachHang
            {
                MaKH = maKH,
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim()
            };

            var (success, message) = _customerService.Update(kh);
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
            var maKH = txtMaKH.Text.Trim();
            if (string.IsNullOrWhiteSpace(maKH))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng '{txtHoTen.Text}' ({maKH})?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var (success, message) = _customerService.Delete(maKH);
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

        private void BtnMoImport_Click(object? sender, EventArgs e)
        {
            using var f = new FormImportKhachHang(_context, _excelService);
            f.ShowDialog(this);
            LoadData();
        }

        private void BtnMoExport_Click(object? sender, EventArgs e)
        {
            using var f = new FormExportKhachHang(_context, _excelService);
            f.ShowDialog(this);
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            PerformSearch();
        }

        private void BtnHienTatCa_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }

        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            var kw = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(kw))
            {
                LoadData();
                return;
            }

            var searchType = cboSearchType.SelectedIndex switch
            {
                1 => "StartsWith",
                2 => "Exact",
                _ => "Contains"
            };

            var list = _customerService.Search(kw, searchType)
                .Select(k => new
                {
                    k.MaKH,
                    k.HoTen,
                    NgaySinhStr = k.NgaySinh.HasValue ? k.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                    k.GioiTinh,
                    k.SoDienThoai,
                    Email = k.Email ?? "",
                    DiaChi = k.DiaChi ?? ""
                })
                .ToList();

            dgvKhachHang.DataSource = list;
            lblTotal.Text = $"Kết quả tìm kiếm: {list.Count:N0}";
        }

        private void DgvKhachHang_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKhachHang.Rows.Count) return;

            var row = dgvKhachHang.Rows[e.RowIndex];
            txtMaKH.Text = row.Cells["MaKH"].Value?.ToString() ?? "";
            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";

            var nsStr = row.Cells["NgaySinh"].Value?.ToString();
            if (DateTime.TryParseExact(nsStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var ns))
            {
                dtpNgaySinh.Value = ns;
            }

            var gt = row.Cells["GioiTinh"].Value?.ToString() ?? "Nam";
            cboGioiTinh.SelectedItem = gt;

            txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
        }
    }
}
