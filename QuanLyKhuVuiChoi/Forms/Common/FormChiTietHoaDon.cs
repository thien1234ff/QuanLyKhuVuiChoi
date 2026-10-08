using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Forms.Common
{
    public class FormChiTietHoaDon : Form
    {
        private readonly HoaDon _hoaDon;

        public FormChiTietHoaDon(HoaDon hoaDon)
        {
            _hoaDon = hoaDon ?? throw new ArgumentNullException(nameof(hoaDon));
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = $"CHI TIẾT HÓA ĐƠN: {_hoaDon.MaHoaDon}";
            this.Size = new Size(780, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Box
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 10)
            };

            var lblTitle = new Label
            {
                Text = $"HÓA ĐƠN THANH TOÁN: {_hoaDon.MaHoaDon}",
                Font = UITheme.HeaderFont,
                ForeColor = UITheme.Primary,
                Location = new Point(20, 10),
                AutoSize = true
            };

            var lblTime = new Label
            {
                Text = $"Thời gian lập: {_hoaDon.NgayLap:dd/MM/yyyy HH:mm:ss}   |   Trạng thái: {_hoaDon.TrangThai}",
                Font = UITheme.BoldFont,
                ForeColor = _hoaDon.TrangThai == "Đã thanh toán" ? UITheme.Success : UITheme.Danger,
                Location = new Point(20, 42),
                AutoSize = true
            };

            var lblKhachHang = new Label
            {
                Text = $"Khách hàng: {_hoaDon.KhachHang?.HoTen ?? "Khách vãng lai"} ({_hoaDon.KhachHang?.SoDienThoai ?? "N/A"})   |   Thu ngân: {_hoaDon.NhanVien?.HoTen ?? "Hệ thống"}",
                Font = UITheme.MainFont,
                ForeColor = UITheme.TextMain,
                Location = new Point(20, 70),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblTime);
            pnlHeader.Controls.Add(lblKhachHang);

            // Bottom Summary Panel
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 90,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            var lblPayment = new Label
            {
                Text = $"Phương thức thanh toán: {_hoaDon.PhuongThucThanhToan}    |    Giảm giá: {_hoaDon.GiamGia}%",
                Font = UITheme.BoldFont,
                Location = new Point(20, 15),
                AutoSize = true
            };

            var lblTotal = new Label
            {
                Text = $"TỔNG TIỀN THANH TOÁN: {_hoaDon.TongTien:#,##0} VNĐ",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38), // Red
                Location = new Point(20, 42),
                AutoSize = true
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                Location = new Point(640, 30),
                Size = new Size(100, 36)
            };
            UITheme.StyleButton(btnClose, Color.FromArgb(100, 116, 139), Color.White);
            btnClose.Click += (s, e) => this.Close();

            pnlBottom.Controls.Add(lblPayment);
            pnlBottom.Controls.Add(lblTotal);
            pnlBottom.Controls.Add(btnClose);

            // Center DataGridView
            var pnlGrid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            var dgvItems = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleDataGridView(dgvItems);

            // Configure Columns
            dgvItems.Columns.Add("STT", "STT");
            dgvItems.Columns.Add("LoaiHang", "Loại");
            dgvItems.Columns.Add("MaHang", "Mã");
            dgvItems.Columns.Add("TenHang", "Tên Hàng Hóa / Dịch Vụ");
            dgvItems.Columns.Add("SoLuong", "Số Lượng");
            dgvItems.Columns.Add("DonGia", "Đơn Giá (VNĐ)");
            dgvItems.Columns.Add("ThanhTien", "Thành Tiền (VNĐ)");

            dgvItems.Columns["STT"].Width = 45;
            dgvItems.Columns["LoaiHang"].Width = 80;
            dgvItems.Columns["MaHang"].Width = 70;
            dgvItems.Columns["SoLuong"].Width = 75;
            dgvItems.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItems.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItems.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
            dgvItems.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItems.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";

            int stt = 1;
            foreach (var item in _hoaDon.ChiTietHoaDons)
            {
                dgvItems.Rows.Add(
                    stt++,
                    item.LoaiHang,
                    item.MaHang,
                    item.TenHang,
                    item.SoLuong,
                    item.DonGia,
                    item.ThanhTien
                );
            }

            pnlGrid.Controls.Add(dgvItems);

            this.Controls.Add(pnlGrid);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlHeader);
        }
    }
}
