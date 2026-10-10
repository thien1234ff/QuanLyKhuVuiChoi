using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Service
{
    public partial class FormDichVu : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;
        private readonly ParkServiceService _parkService;

        public FormDichVu() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormDichVu(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _excelService = excelService ?? new ExcelService();
            _parkService = new ParkServiceService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                cboLoaiDichVu.SelectedIndex = 0;
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
                var list = _parkService.GetAll();
                BindGrid(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<DichVu> list)
        {
            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = list.Select(d => new
            {
                d.MaDichVu,
                d.TenDichVu,
                d.LoaiDichVu,
                DonGia = d.DonGia.ToString("#,##0 đ"),
                SoLuongTon = $"{d.SoLuongTon} cái/phần",
                d.TrangThai,
                MoTa = d.MoTa ?? ""
            }).ToList();

            if (dgvDichVu.Columns["MaDichVu"] != null) dgvDichVu.Columns["MaDichVu"].HeaderText = "MÃ DỊCH VỤ";
            if (dgvDichVu.Columns["TenDichVu"] != null) dgvDichVu.Columns["TenDichVu"].HeaderText = "TÊN DỊCH VỤ";
            if (dgvDichVu.Columns["LoaiDichVu"] != null) dgvDichVu.Columns["LoaiDichVu"].HeaderText = "LOẠI DỊCH VỤ";
            if (dgvDichVu.Columns["DonGia"] != null) dgvDichVu.Columns["DonGia"].HeaderText = "ĐƠN GIÁ";
            if (dgvDichVu.Columns["SoLuongTon"] != null) dgvDichVu.Columns["SoLuongTon"].HeaderText = "TỒN KHO";
            if (dgvDichVu.Columns["TrangThai"] != null) dgvDichVu.Columns["TrangThai"].HeaderText = "TRẠNG THÁI";
            if (dgvDichVu.Columns["MoTa"] != null)
            {
                dgvDichVu.Columns["MoTa"].HeaderText = "MÔ TẢ";
                dgvDichVu.Columns["MoTa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            lblTotal.Text = $"Tổng số dịch vụ: {list.Count:N0}";
        }

        private void Filter_Changed(object? sender, EventArgs e)
        {
            ApplyFilter();
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

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterLoai.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndex = 0;
            LoadData();
        }

        private void ClearInputs()
        {
            txtMaDichVu.Text = _parkService.GenerateNextId();
            txtTenDichVu.Clear();
            cboLoaiDichVu.SelectedIndex = 0;
            numDonGia.Value = 30000;
            numSoLuongTon.Value = 100;
            cboTrangThai.SelectedIndex = 0;
            txtMoTa.Clear();
            txtTenDichVu.Focus();
        }

        private void BtnAutoId_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BtnPlusStock_Click(object? sender, EventArgs e)
        {
            numSoLuongTon.Value += 50;
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
                MessageBox.Show("Vui lòng chọn dịch vụ cần cập nhật từ danh sách.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có muốn cập nhật thông tin dịch vụ '{txtTenDichVu.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void DgvDichVu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDichVu.Rows.Count)
            {
                var row = dgvDichVu.Rows[e.RowIndex];
                var ma = row.Cells["MaDichVu"]?.Value?.ToString() ?? "";
                var dv = _parkService.GetById(ma);
                if (dv != null)
                {
                    txtMaDichVu.Text = dv.MaDichVu;
                    txtTenDichVu.Text = dv.TenDichVu;
                    if (cboLoaiDichVu.Items.Contains(dv.LoaiDichVu)) cboLoaiDichVu.SelectedItem = dv.LoaiDichVu;
                    numDonGia.Value = dv.DonGia;
                    numSoLuongTon.Value = dv.SoLuongTon;
                    if (cboTrangThai.Items.Contains(dv.TrangThai)) cboTrangThai.SelectedItem = dv.TrangThai;
                    txtMoTa.Text = dv.MoTa ?? "";
                }
            }
        }
    }
}
