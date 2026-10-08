using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Forms.Common;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Billing
{
    public class FormHoaDon : Form
    {
        private readonly InvoiceService _invoiceService;
        private readonly CustomerService _customerService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // UI Controls
        private DataGridView dgvHoaDon = null!;
        private TextBox txtSearch = null!;
        private DateTimePicker dtpFrom = null!;
        private DateTimePicker dtpTo = null!;
        private ComboBox cboKhachHang = null!;
        private Label lblTotal = null!;
        private Label lblTotalAmount = null!;

        public FormHoaDon(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _invoiceService = new InvoiceService(_context, _excelService);
            _customerService = new CustomerService(_context, _excelService);

            InitializeComponent();
            LoadCustomersFilter();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "THÀNH VIÊN 4: QUẢN LÝ HÓA ĐƠN";
            this.Size = new Size(1150, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;

            // Top Header Panel
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };
            var lblTitle = new Label
            {
                Text = "QUẢN LÝ DANH SÁCH HÓA ĐƠN VÀ LỊCH SỬ GIAO DỊCH",
                Font = UITheme.HeaderFont,
                ForeColor = Color.FromArgb(16, 185, 129),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            lblTotal = new Label
            {
                Text = "Tổng số: 0 hóa đơn",
                Font = UITheme.BoldFont,
                ForeColor = UITheme.TextMuted,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(950, 20)
            };
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblTotal);

            // Filter Bar Panel
            var pnlFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };

            var lblFrom = new Label { Text = "Từ ngày:", Location = new Point(15, 15), AutoSize = true, Font = UITheme.BoldFont };
            dtpFrom = new DateTimePicker { Location = new Point(80, 12), Size = new Size(120, 25), Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddMonths(-1) };

            var lblTo = new Label { Text = "Đến ngày:", Location = new Point(215, 15), AutoSize = true, Font = UITheme.BoldFont };
            dtpTo = new DateTimePicker { Location = new Point(285, 12), Size = new Size(120, 25), Format = DateTimePickerFormat.Short, Value = DateTime.Today };

            var lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(420, 15), AutoSize = true, Font = UITheme.BoldFont };
            txtSearch = new TextBox { Location = new Point(490, 12), Size = new Size(180, 25) };

            var lblKh = new Label { Text = "Khách hàng:", Location = new Point(685, 15), AutoSize = true, Font = UITheme.BoldFont };
            cboKhachHang = new ComboBox { Location = new Point(770, 12), Size = new Size(180, 25), DropDownStyle = ComboBoxStyle.DropDownList };

            // Buttons row 2 in filter
            var btnLoc = new Button { Text = "🔍 Lọc", Location = new Point(80, 48), Size = new Size(80, 28) };
            UITheme.StyleButton(btnLoc, UITheme.Primary, Color.White, 28);
            btnLoc.Click += (s, e) => ApplyFilter();

            var btnReset = new Button { Text = "Đặt lại", Location = new Point(168, 48), Size = new Size(75, 28) };
            UITheme.StyleSecondaryButton(btnReset, 28);
            btnReset.Click += (s, e) =>
            {
                txtSearch.Text = "";
                dtpFrom.Value = DateTime.Today.AddMonths(-1);
                dtpTo.Value = DateTime.Today;
                cboKhachHang.SelectedIndex = 0;
                LoadData();
            };

            var btnXemChiTiet = new Button { Text = "👁️ Xem Chi Tiết", Location = new Point(251, 48), Size = new Size(120, 28) };
            UITheme.StyleButton(btnXemChiTiet, Color.FromArgb(2, 132, 199), Color.White, 28);
            btnXemChiTiet.Click += BtnXemChiTiet_Click;

            var btnHuy = new Button { Text = "🚫 Hủy Hóa Đơn", Location = new Point(379, 48), Size = new Size(120, 28) };
            UITheme.StyleButton(btnHuy, Color.FromArgb(245, 158, 11), Color.White, 28);
            btnHuy.Click += BtnHuy_Click;

            var btnXoa = new Button { Text = "🗑️ Xóa HĐ", Location = new Point(507, 48), Size = new Size(100, 28) };
            UITheme.StyleDangerButton(btnXoa, 28);
            btnXoa.Click += BtnXoa_Click;

            var btnExport = new Button { Text = "📤 Xuất Excel", Location = new Point(615, 48), Size = new Size(110, 28) };
            UITheme.StyleButton(btnExport, Color.FromArgb(16, 185, 129), Color.White, 28);
            btnExport.Click += BtnExport_Click;

            pnlFilter.Controls.Add(lblFrom);
            pnlFilter.Controls.Add(dtpFrom);
            pnlFilter.Controls.Add(lblTo);
            pnlFilter.Controls.Add(dtpTo);
            pnlFilter.Controls.Add(lblSearch);
            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(lblKh);
            pnlFilter.Controls.Add(cboKhachHang);
            pnlFilter.Controls.Add(btnLoc);
            pnlFilter.Controls.Add(btnReset);
            pnlFilter.Controls.Add(btnXemChiTiet);
            pnlFilter.Controls.Add(btnHuy);
            pnlFilter.Controls.Add(btnXoa);
            pnlFilter.Controls.Add(btnExport);

            // Bottom Summary Panel
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };
            lblTotalAmount = new Label
            {
                Text = "Tổng doanh thu danh sách: 0 VNĐ",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(20, 14),
                AutoSize = true
            };
            pnlBottom.Controls.Add(lblTotalAmount);

            // Center DataGridView
            var pnlGrid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            dgvHoaDon = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvHoaDon);
            dgvHoaDon.CellDoubleClick += (s, e) => OpenSelectedDetail();
            pnlGrid.Controls.Add(dgvHoaDon);

            this.Controls.Add(pnlGrid);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlFilter);
            this.Controls.Add(pnlHeader);
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

        private void BindGrid(System.Collections.Generic.List<HoaDon> list)
        {
            dgvHoaDon.DataSource = null;

            // Chuyển sang dạng hiển thị sạch
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

        private class ComboBoxItem
        {
            public string Text { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
            public override string ToString() => Text;
        }
    }
}
