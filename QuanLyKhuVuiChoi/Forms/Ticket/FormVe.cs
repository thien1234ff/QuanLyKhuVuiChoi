using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Ticket
{
    public partial class FormVe : Form
    {
        private readonly QuanLyKhuVuiChoiDbContext _context;
        private readonly IExcelService _excelService;
        private readonly TicketService _ticketService;

        public FormVe() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormVe(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context ?? new QuanLyKhuVuiChoiDbContext();
            _excelService = excelService ?? new ExcelService();
            _ticketService = new TicketService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                cboLoaiVe.SelectedIndex = 0;
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
                var list = _ticketService.GetAll();
                BindGrid(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải bảng giá vé: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<Ve> list)
        {
            dgvVe.DataSource = null;
            dgvVe.DataSource = list.Select(v => new
            {
                v.MaVe,
                v.TenVe,
                v.LoaiVe,
                GiaVe = v.GiaVe.ToString("#,##0 đ"),
                ThoiHan = v.ThoiHan ?? "Trong ngày",
                v.TrangThai,
                MoTa = v.MoTa ?? ""
            }).ToList();

            if (dgvVe.Columns["MaVe"] != null) dgvVe.Columns["MaVe"].HeaderText = "MÃ VÉ";
            if (dgvVe.Columns["TenVe"] != null) dgvVe.Columns["TenVe"].HeaderText = "TÊN LOẠI VÉ";
            if (dgvVe.Columns["LoaiVe"] != null) dgvVe.Columns["LoaiVe"].HeaderText = "NHÓM VÉ";
            if (dgvVe.Columns["GiaVe"] != null) dgvVe.Columns["GiaVe"].HeaderText = "GIÁ VÉ";
            if (dgvVe.Columns["ThoiHan"] != null) dgvVe.Columns["ThoiHan"].HeaderText = "THỜI HẠN";
            if (dgvVe.Columns["TrangThai"] != null) dgvVe.Columns["TrangThai"].HeaderText = "TRẠNG THÁI";
            if (dgvVe.Columns["MoTa"] != null)
            {
                dgvVe.Columns["MoTa"].HeaderText = "MÔ TẢ";
                dgvVe.Columns["MoTa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            lblTotal.Text = $"Tổng số loại vé: {list.Count:N0}";
        }

        private void Filter_Changed(object? sender, EventArgs e)
        {
            ApplyFilter();
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

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterLoai.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndex = 0;
            LoadData();
        }

        private void ClearInputs()
        {
            txtMaVe.Text = _ticketService.GenerateNextId();
            txtTenVe.Clear();
            cboLoaiVe.SelectedIndex = 0;
            numGiaVe.Value = 100000;
            txtThoiHan.Text = "Trong ngày";
            cboTrangThai.SelectedIndex = 0;
            txtMoTa.Clear();
            txtTenVe.Focus();
        }

        private void BtnAutoId_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaVe.Text))
                txtMaVe.Text = _ticketService.GenerateNextId();

            var v = new Ve
            {
                MaVe = txtMaVe.Text.Trim(),
                TenVe = txtTenVe.Text.Trim(),
                LoaiVe = cboLoaiVe.SelectedItem?.ToString() ?? "Vé ngày",
                GiaVe = numGiaVe.Value,
                ThoiHan = string.IsNullOrWhiteSpace(txtThoiHan.Text) ? "Trong ngày" : txtThoiHan.Text.Trim(),
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang áp dụng",
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
            };

            var (success, msg) = _ticketService.Add(v);
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

            var v = new Ve
            {
                MaVe = txtMaVe.Text.Trim(),
                TenVe = txtTenVe.Text.Trim(),
                LoaiVe = cboLoaiVe.SelectedItem?.ToString() ?? "Vé ngày",
                GiaVe = numGiaVe.Value,
                ThoiHan = string.IsNullOrWhiteSpace(txtThoiHan.Text) ? "Trong ngày" : txtThoiHan.Text.Trim(),
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang áp dụng",
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
            };

            var (success, msg) = _ticketService.Update(v);
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
                MessageBox.Show("Vui lòng chọn vé cần xóa.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa loại vé '{txtTenVe.Text}' ({txtMaVe.Text})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void DgvVe_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvVe.Rows.Count)
            {
                var row = dgvVe.Rows[e.RowIndex];
                var ma = row.Cells["MaVe"]?.Value?.ToString() ?? "";
                var v = _ticketService.GetById(ma);
                if (v != null)
                {
                    txtMaVe.Text = v.MaVe;
                    txtTenVe.Text = v.TenVe;
                    if (cboLoaiVe.Items.Contains(v.LoaiVe)) cboLoaiVe.SelectedItem = v.LoaiVe;
                    numGiaVe.Value = v.GiaVe;
                    txtThoiHan.Text = v.ThoiHan ?? "Trong ngày";
                    if (cboTrangThai.Items.Contains(v.TrangThai)) cboTrangThai.SelectedItem = v.TrangThai;
                    txtMoTa.Text = v.MoTa ?? "";
                }
            }
        }
    }
}
