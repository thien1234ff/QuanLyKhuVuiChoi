using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Forms.Common
{
    public partial class FormChiTietHoaDon : Form
    {
        private readonly HoaDon _hoaDon;

        public FormChiTietHoaDon() : this(new HoaDon { MaHoaDon = "HD-DEMO", NgayLap = DateTime.Now, TrangThai = "Đã thanh toán", PhuongThucThanhToan = "Tiền mặt" })
        {
        }

        public FormChiTietHoaDon(HoaDon hoaDon)
        {
            _hoaDon = hoaDon ?? new HoaDon();
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            LoadHoaDonDetail();
        }

        private void LoadHoaDonDetail()
        {
            this.Text = $"CHI TIẾT HÓA ĐƠN: {_hoaDon.MaHoaDon}";
            lblTitle.Text = $" HÓA ĐƠN THANH TOÁN: {_hoaDon.MaHoaDon}";
            lblTime.Text = $"Thời gian lập: {_hoaDon.NgayLap:dd/MM/yyyy HH:mm:ss}   |   Trạng thái: {_hoaDon.TrangThai}";
            lblTime.ForeColor = _hoaDon.TrangThai == "Đã thanh toán" ? Color.FromArgb(16, 185, 129) : Color.FromArgb(239, 68, 68);

            string khach = _hoaDon.KhachHang != null ? $"{_hoaDon.KhachHang.HoTen} ({_hoaDon.KhachHang.SoDienThoai})" : "Khách vãng lai";
            string thuNgan = _hoaDon.NhanVien?.HoTen ?? "Hệ thống";
            lblKhachHang.Text = $"Khách hàng: {khach}   |   Thu ngân: {thuNgan}";

            lblPayment.Text = $"Phương thức thanh toán: {_hoaDon.PhuongThucThanhToan}    |    Giảm giá: {_hoaDon.GiamGia}%";
            lblTotal.Text = $"TỔNG TIỀN THANH TOÁN: {_hoaDon.TongTien:#,##0} VNĐ";

            dgvItems.Rows.Clear();
            if (_hoaDon.ChiTietHoaDons != null)
            {
                int stt = 1;
                foreach (var item in _hoaDon.ChiTietHoaDons)
                {
                    dgvItems.Rows.Add(
                        stt++,
                        item.LoaiHang,
                        item.MaHang,
                        item.TenHang,
                        item.SoLuong,
                        item.DonGia.ToString("#,##0"),
                        item.ThanhTien.ToString("#,##0")
                    );
                }
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
