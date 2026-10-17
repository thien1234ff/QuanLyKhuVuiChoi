using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Game
{
    public partial class FormTroChoi : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;
        private readonly GameService _gameService;

        public FormTroChoi() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormTroChoi(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _excelService = excelService ?? new ExcelService();
            _gameService = new GameService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                cboLoaiTroChoi.SelectedIndex = 0;
                cboTrangThai.SelectedIndex = 0;
                cboFilterLoai.SelectedIndex = 0;
                cboFilterTrangThai.SelectedIndex = 0;
                ClearInputs();
                LoadData();
            }
        }

        public void LoadData()
        {
            try
            {
                var list = _gameService.GetAll();
                BindGrid(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục trò chơi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<TroChoi> list)
        {
            dgvTroChoi.DataSource = null;
            dgvTroChoi.DataSource = list.Select(t => new
            {
                t.MaTroChoi,
                t.TenTroChoi,
                t.LoaiTroChoi,
                DoTuoiToiThieu = $"{t.DoTuoiToiThieu} tuổi",
                SucChua = $"{t.SucChua} người",
                t.TrangThai,
                MoTa = t.MoTa ?? ""
            }).ToList();

            if (dgvTroChoi.Columns["MaTroChoi"] != null) dgvTroChoi.Columns["MaTroChoi"].HeaderText = "MÃ TRÒ CHƠI";
            if (dgvTroChoi.Columns["TenTroChoi"] != null) dgvTroChoi.Columns["TenTroChoi"].HeaderText = "TÊN TRÒ CHƠI";
            if (dgvTroChoi.Columns["LoaiTroChoi"] != null) dgvTroChoi.Columns["LoaiTroChoi"].HeaderText = "LOẠI TRÒ CHƠI";
            if (dgvTroChoi.Columns["DoTuoiToiThieu"] != null) dgvTroChoi.Columns["DoTuoiToiThieu"].HeaderText = "TUỔI TỐI THIỂU";
            if (dgvTroChoi.Columns["SucChua"] != null) dgvTroChoi.Columns["SucChua"].HeaderText = "SỨC CHỨA";
            if (dgvTroChoi.Columns["TrangThai"] != null) dgvTroChoi.Columns["TrangThai"].HeaderText = "TRẠNG THÁI";
            if (dgvTroChoi.Columns["MoTa"] != null)
            {
                dgvTroChoi.Columns["MoTa"].HeaderText = "MÔ TẢ";
                dgvTroChoi.Columns["MoTa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            lblTotal.Text = $"Tổng số trò chơi: {list.Count:N0}";
        }

        private void Filter_Changed(object? sender, EventArgs e)
        {
            ApplyFilter();
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

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterLoai.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndex = 0;
            LoadData();
        }

        private void ClearInputs()
        {
            txtMaTroChoi.Text = _gameService.GenerateNextId();
            txtTenTroChoi.Clear();
            cboLoaiTroChoi.SelectedIndex = 0;
            numDoTuoi.Value = 6;
            numSucChua.Value = 20;
            cboTrangThai.SelectedIndex = 0;
            txtMoTa.Clear();
            txtTenTroChoi.Focus();
        }

        private void BtnAutoId_Click(object? sender, EventArgs e)
        {
            ClearInputs();
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
                GiaVe = 0,
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
                GiaVe = 0,
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

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void DgvTroChoi_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvTroChoi.Rows.Count)
            {
                var row = dgvTroChoi.Rows[e.RowIndex];
                var ma = row.Cells["MaTroChoi"]?.Value?.ToString() ?? "";
                var tc = _gameService.GetById(ma);
                if (tc != null)
                {
                    txtMaTroChoi.Text = tc.MaTroChoi;
                    txtTenTroChoi.Text = tc.TenTroChoi;
                    if (cboLoaiTroChoi.Items.Contains(tc.LoaiTroChoi)) cboLoaiTroChoi.SelectedItem = tc.LoaiTroChoi;
                    numDoTuoi.Value = tc.DoTuoiToiThieu;
                    numSucChua.Value = tc.SucChua;
                    if (cboTrangThai.Items.Contains(tc.TrangThai)) cboTrangThai.SelectedItem = tc.TrangThai;
                    txtMoTa.Text = tc.MoTa ?? "";
                }
            }
        }
    }
}
