using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Forms.Common;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Billing
{
    public partial class FormHoaDon : Form
    {
        private class ComboBoxItem
        {
            public string Text { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
            public override string ToString() => Text;
        }

        private readonly InvoiceService _invoiceService;
        private readonly CustomerService _customerService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        public FormHoaDon() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormHoaDon(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _invoiceService = new InvoiceService(_context, _excelService);
            _customerService = new CustomerService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;

            // Phân quyền: Chỉ Admin mới có quyền xóa hoặc hủy hóa đơn
            if (!AuthService.IsAdmin)
            {
                btnXoa.Enabled = false;
                btnHuy.Enabled = false;
            }

            LoadCustomersFilter();
            LoadData();
        }

        private void LoadCustomersFilter()
        {
            cboKhachHang.Items.Clear();
            cboKhachHang.Items.Add("Tất cả");
            var customers = _customerService.GetAll();
            foreach (var c in customers)
            {
                cboKhachHang.Items.Add(new ComboBoxItem { Text = c.HoTen, Value = c.MaKH });
            }
            cboKhachHang.SelectedIndex = 0;
        }

        private void LoadData()
        {
            var list = _invoiceService.GetAll();
            BindGrid(list);
        }

        private void BtnLoc_Click(object? sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            txtSearch.Text = "";
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;
            cboKhachHang.SelectedIndex = 0;
            LoadData();
        }

        private void ApplyFilter()
        {
            string? maKH = null;
            if (cboKhachHang.SelectedIndex > 0 && cboKhachHang.SelectedItem is ComboBoxItem item)
            {
                maKH = item.Value;
            }

            var list = _invoiceService.Filter(txtSearch.Text, dtpFrom.Value, dtpTo.Value, maKH);
            BindGrid(list);
        }

        private void BindGrid(List<HoaDon> list)
        {
            dgvHoaDon.DataSource = null;

            var displayList = list.Select(h => new
            {
                h.MaHoaDon,
                KhachHang = h.KhachHang != null ? $"{h.KhachHang.HoTen} ({h.KhachHang.SoDienThoai})" : "Khách vãng lai",
                ThuNgan = h.NhanVien?.HoTen ?? "Hệ thống",
                h.NgayLap,
                h.GiamGia,
                TongTien = h.TongTien,
                h.PhuongThucThanhToan,
                h.TrangThai,
                h.GhiChu
            }).ToList();

            dgvHoaDon.DataSource = displayList;

            if (dgvHoaDon.Columns["MaHoaDon"] != null) dgvHoaDon.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
            if (dgvHoaDon.Columns["KhachHang"] != null) dgvHoaDon.Columns["KhachHang"].HeaderText = "Khách Hàng";
            if (dgvHoaDon.Columns["ThuNgan"] != null) dgvHoaDon.Columns["ThuNgan"].HeaderText = "Thu Ngân";
            if (dgvHoaDon.Columns["NgayLap"] != null)
            {
                dgvHoaDon.Columns["NgayLap"].HeaderText = "Ngày Lập";
                dgvHoaDon.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvHoaDon.Columns["GiamGia"] != null)
            {
                dgvHoaDon.Columns["GiamGia"].HeaderText = "Giảm (%)";
                dgvHoaDon.Columns["GiamGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvHoaDon.Columns["TongTien"] != null)
            {
                dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng Tiền (VNĐ)";
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvHoaDon.Columns["PhuongThucThanhToan"] != null) dgvHoaDon.Columns["PhuongThucThanhToan"].HeaderText = "PT Thanh Toán";
            if (dgvHoaDon.Columns["TrangThai"] != null) dgvHoaDon.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dgvHoaDon.Columns["GhiChu"] != null) dgvHoaDon.Columns["GhiChu"].HeaderText = "Ghi Chú";

            decimal sumAmount = list.Where(h => h.TrangThai == "Đã thanh toán").Sum(h => h.TongTien);
            lblTotal.Text = $"Tổng số: {list.Count} hóa đơn";
            lblTotalAmount.Text = $"Tổng doanh thu hợp lệ: {sumAmount:#,##0} VNĐ";
        }

        private string? GetSelectedInvoiceId()
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                return dgvHoaDon.SelectedRows[0].Cells["MaHoaDon"]?.Value?.ToString();
            }
            return null;
        }

        private void BtnXemChiTiet_Click(object? sender, EventArgs e)
        {
            OpenSelectedDetail();
        }

        private void DgvHoaDon_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpenSelectedDetail();
            }
        }

        private void OpenSelectedDetail()
        {
            var maHD = GetSelectedInvoiceId();
            if (string.IsNullOrWhiteSpace(maHD))
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn từ bảng để xem chi tiết.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var hd = _invoiceService.GetById(maHD);
            if (hd != null)
            {
                var f = new FormChiTietHoaDon(hd);
                f.ShowDialog(this);
            }
        }

        private void BtnHuy_Click(object? sender, EventArgs e)
        {
            if (!AuthService.IsAdmin)
            {
                MessageBox.Show("Quyền hạn không đủ: Chỉ Quản trị viên (Admin) mới có quyền HỦY hóa đơn!", "Yêu cầu quyền Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maHD = GetSelectedInvoiceId();
            if (string.IsNullOrWhiteSpace(maHD))
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn cần hủy.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy hóa đơn '{maHD}' không?\nThao tác này sẽ hoàn lại số lượng hàng hóa tồn kho (nếu có).", "Xác nhận hủy hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var (success, msg) = _invoiceService.CancelInvoice(maHD, "Người dùng yêu cầu hủy trên phần mềm");
            MessageBox.Show(msg, success ? "Thông báo" : "Lỗi", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) ApplyFilter();
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (!AuthService.IsAdmin)
            {
                MessageBox.Show("Quyền hạn không đủ: Chỉ Quản trị viên (Admin) mới có quyền XÓA hóa đơn!", "Yêu cầu quyền Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maHD = GetSelectedInvoiceId();
            if (string.IsNullOrWhiteSpace(maHD))
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn cần xóa.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn XÓA VĨNH VIỄN hóa đơn '{maHD}' khỏi CSDL?\nToàn bộ chi tiết hóa đơn liên quan cũng sẽ bị xóa.", "Cảnh báo xóa vĩnh viễn", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (confirm != DialogResult.Yes) return;

            var (success, msg) = _invoiceService.DeleteInvoice(maHD);
            MessageBox.Show(msg, success ? "Thông báo" : "Lỗi", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) ApplyFilter();
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            string? maKH = null;
            if (cboKhachHang.SelectedIndex > 0 && cboKhachHang.SelectedItem is ComboBoxItem item) maKH = item.Value;

            var list = _invoiceService.Filter(txtSearch.Text, dtpFrom.Value, dtpTo.Value, maKH);
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hóa đơn nào để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"DanhSachHoaDon_{DateTime.Now:yyyyMMdd}.xlsx",
                Title = "Chọn nơi lưu file Excel danh sách hóa đơn"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _invoiceService.ExportInvoiceListToExcel(list, sfd.FileName);
                    MessageBox.Show($"Xuất file Excel thành công!\nĐường dẫn: {sfd.FileName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
