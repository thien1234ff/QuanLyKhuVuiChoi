using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Forms.Common;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Billing
{
    public partial class FormBanVe : Form
    {
        private class ComboItem
        {
            public string Text { get; set; } = string.Empty;
            public string? Value { get; set; }
            public override string ToString() => Text;
        }

        private readonly InvoiceService _invoiceService;
        private readonly CustomerService _customerService;
        private readonly TicketService _ticketService;
        private readonly ParkServiceService _parkService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // Giỏ hàng
        private readonly List<CartItemDto> _cartItems = new List<CartItemDto>();

        // Danh sách mặt hàng từ CSDL
        private List<Ve> _availableTickets = new List<Ve>();
        private List<DichVu> _availableServices = new List<DichVu>();
        private string _activeCategory = "TatCa";

        public FormBanVe() : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())
        {
        }

        public FormBanVe(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _invoiceService = new InvoiceService(_context, _excelService);
            _customerService = new CustomerService(_context, _excelService);
            _ticketService = new TicketService(_context, _excelService);
            _parkService = new ParkServiceService(_context, _excelService);

            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                _availableTickets = _ticketService.GetAll().Where(v => v.TrangThai == "Đang áp dụng").ToList();
                _availableServices = _parkService.GetAll().Where(d => d.TrangThai == "Đang phục vụ").ToList();

                // Load Customers
                cboCustomer.Items.Clear();
                cboCustomer.Items.Add(new ComboItem { Text = "👤 Khách vãng lai tại quầy", Value = "" });
                var customers = _customerService.GetAll();
                foreach (var c in customers)
                {
                    cboCustomer.Items.Add(new ComboItem { Text = $"{c.HoTen} - {c.SoDienThoai}", Value = c.MaKH });
                }
                cboCustomer.SelectedIndex = 0;

                if (cboPaymentMethod.Items.Count > 0 && cboPaymentMethod.SelectedIndex == -1)
                {
                    cboPaymentMethod.SelectedIndex = 0;
                }

                RenderTicketCards();
                RefreshCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp danh mục vé: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCategory_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string cat)
            {
                _activeCategory = cat;

                btnCatAll.BackColor = (cat == "TatCa") ? Color.FromArgb(79, 70, 229) : Color.White;
                btnCatAll.ForeColor = (cat == "TatCa") ? Color.White : Color.FromArgb(71, 85, 105);

                btnCatTicket.BackColor = (cat == "Ve") ? Color.FromArgb(79, 70, 229) : Color.White;
                btnCatTicket.ForeColor = (cat == "Ve") ? Color.White : Color.FromArgb(71, 85, 105);

                btnCatService.BackColor = (cat == "DichVu") ? Color.FromArgb(79, 70, 229) : Color.White;
                btnCatService.ForeColor = (cat == "DichVu") ? Color.White : Color.FromArgb(71, 85, 105);

                RenderTicketCards();
            }
        }

        private void RenderTicketCards()
        {
            pnlTicketCards.Controls.Clear();

            // 1. Tickets
            if (_activeCategory == "TatCa" || _activeCategory == "Ve")
            {
                foreach (var ticket in _availableTickets)
                {
                    pnlTicketCards.Controls.Add(CreateTicketCard(ticket.MaVe, ticket.TenVe, ticket.GiaVe, ticket.MoTa ?? "Vé tham quan tiêu chuẩn", "Ve", "🎟️"));
                }
            }

            // 2. Services
            if (_activeCategory == "TatCa" || _activeCategory == "DichVu")
            {
                foreach (var s in _availableServices)
                {
                    pnlTicketCards.Controls.Add(CreateTicketCard(s.MaDichVu, s.TenDichVu, s.DonGia, $"Tồn kho: {s.SoLuongTon}", "DichVu", "🍔"));
                }
            }
        }

        private Panel CreateTicketCard(string id, string name, decimal price, string desc, string type, string icon)
        {
            var card = new Panel
            {
                Size = new Size(225, 140),
                BackColor = Color.White,
                Margin = new Padding(6),
                Padding = new Padding(12)
            };
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            // Icon Badge
            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI Emoji", 14F),
                Location = new Point(12, 10),
                Size = new Size(32, 28),
                BackColor = Color.Transparent
            };

            // Name
            var lblName = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(48, 10),
                Size = new Size(160, 20),
                AutoEllipsis = true
            };

            // Price
            var lblPrice = new Label
            {
                Text = price.ToString("N0") + "đ",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                Location = new Point(48, 30),
                AutoSize = true
            };

            // Description
            var lblDesc = new Label
            {
                Text = desc,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(14, 58),
                Size = new Size(195, 28),
                AutoEllipsis = true
            };

            // Quantity Stepper: [-]  0  [+]
            var pnlStepper = new Panel
            {
                Location = new Point(14, 94),
                Size = new Size(195, 32),
                BackColor = Color.Transparent
            };

            var existingItem = _cartItems.FirstOrDefault(c => c.LoaiHang == type && c.MaHang == id);
            int currentQty = existingItem?.SoLuong ?? 0;

            var lblQty = new Label
            {
                Text = currentQty.ToString(),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(80, 5),
                Size = new Size(35, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var btnMinus = new Button
            {
                Text = "−",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(26, 26),
                Location = new Point(52, 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(15, 23, 42),
                Cursor = Cursors.Hand
            };
            btnMinus.FlatAppearance.BorderSize = 0;

            var btnPlus = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(26, 26),
                Location = new Point(117, 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnPlus.FlatAppearance.BorderSize = 0;

            btnPlus.Click += (s, e) =>
            {
                int q = int.Parse(lblQty.Text) + 1;
                lblQty.Text = q.ToString();
                AddToCart(id, name, type, price, 1);
            };

            btnMinus.Click += (s, e) =>
            {
                int q = int.Parse(lblQty.Text);
                if (q > 0)
                {
                    q--;
                    lblQty.Text = q.ToString();
                    AddToCart(id, name, type, price, -1);
                }
            };

            pnlStepper.Controls.Add(btnMinus);
            pnlStepper.Controls.Add(lblQty);
            pnlStepper.Controls.Add(btnPlus);

            card.Controls.Add(lblIcon);
            card.Controls.Add(lblName);
            card.Controls.Add(lblPrice);
            card.Controls.Add(lblDesc);
            card.Controls.Add(pnlStepper);

            return card;
        }

        private void AddToCart(string id, string name, string type, decimal price, int changeQty)
        {
            var item = _cartItems.FirstOrDefault(c => c.LoaiHang == type && c.MaHang == id);
            if (item != null)
            {
                item.SoLuong += changeQty;
                if (item.SoLuong <= 0)
                {
                    _cartItems.Remove(item);
                }
            }
            else if (changeQty > 0)
            {
                _cartItems.Add(new CartItemDto
                {
                    LoaiHang = type,
                    MaHang = id,
                    TenHang = name,
                    DonGia = price,
                    SoLuong = changeQty
                });
            }

            RefreshCartDisplay();
        }

        private void RefreshCartDisplay()
        {
            pnlCartItems.Controls.Clear();

            if (!_cartItems.Any())
            {
                var lblEmpty = new Label
                {
                    Text = "Giỏ hàng trống.\nVui lòng bấm dấu (+) trên loại vé để thêm vào đơn.",
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(350, 100),
                    Margin = new Padding(10, 40, 10, 0)
                };
                pnlCartItems.Controls.Add(lblEmpty);
            }
            else
            {
                foreach (var item in _cartItems.ToList())
                {
                    var row = new Panel
                    {
                        Size = new Size(350, 48),
                        BackColor = Color.White,
                        Margin = new Padding(0, 2, 0, 2)
                    };
                    row.Paint += (s, e) =>
                    {
                        using var pen = new Pen(Color.FromArgb(248, 250, 252), 1);
                        e.Graphics.DrawLine(pen, 0, row.Height - 1, row.Width, row.Height - 1);
                    };

                    var lblName = new Label
                    {
                        Text = item.TenHang,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(15, 23, 42),
                        Location = new Point(4, 4),
                        Size = new Size(190, 18),
                        AutoEllipsis = true
                    };

                    var lblDetail = new Label
                    {
                        Text = $"{item.DonGia:N0}đ  x  {item.SoLuong}",
                        Font = new Font("Segoe UI", 8F),
                        ForeColor = Color.FromArgb(100, 116, 139),
                        Location = new Point(4, 24),
                        AutoSize = true
                    };

                    var lblTotalItem = new Label
                    {
                        Text = item.ThanhTien.ToString("N0") + "đ",
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(15, 23, 42),
                        Location = new Point(205, 14),
                        Width = 105,
                        TextAlign = ContentAlignment.MiddleRight
                    };

                    var btnRemove = new Button
                    {
                        Text = "✕",
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        Size = new Size(24, 24),
                        Location = new Point(320, 12),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.FromArgb(254, 242, 242),
                        ForeColor = Color.FromArgb(239, 68, 68),
                        Cursor = Cursors.Hand
                    };
                    btnRemove.FlatAppearance.BorderSize = 0;
                    btnRemove.Click += (s, e) =>
                    {
                        _cartItems.Remove(item);
                        RefreshCartDisplay();
                        RenderTicketCards();
                    };

                    row.Controls.Add(lblName);
                    row.Controls.Add(lblDetail);
                    row.Controls.Add(lblTotalItem);
                    row.Controls.Add(btnRemove);

                    pnlCartItems.Controls.Add(row);
                }
            }

            UpdateTotals();
        }

        private void NumDiscountPercent_ValueChanged(object? sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void LnkClearCart_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_cartItems.Any())
            {
                _cartItems.Clear();
                RefreshCartDisplay();
                RenderTicketCards();
            }
        }

        private void UpdateTotals()
        {
            decimal subtotal = _cartItems.Sum(i => i.ThanhTien);
            decimal discountPct = numDiscountPercent.Value;
            decimal discountVal = subtotal * (discountPct / 100m);
            decimal finalTotal = subtotal - discountVal;

            lblSubtotal.Text = subtotal.ToString("N0") + " đ";
            lblTotal.Text = finalTotal.ToString("N0") + " đ";

            btnCheckout.Enabled = _cartItems.Any();
            btnCheckout.BackColor = _cartItems.Any() ? Color.FromArgb(16, 185, 129) : Color.FromArgb(203, 213, 225);
        }

        private void BtnCheckout_Click(object? sender, EventArgs e)
        {
            if (!_cartItems.Any())
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 loại vé hoặc dịch vụ trước khi thanh toán!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? maKH = (cboCustomer.SelectedItem as ComboItem)?.Value;
            string? maNV = AuthService.CurrentEmployee?.MaNV ?? "NV001";
            decimal discountPct = numDiscountPercent.Value;
            string phuongThuc = cboPaymentMethod.SelectedItem?.ToString() ?? "Tiền mặt";
            decimal finalTotal = _cartItems.Sum(i => i.ThanhTien) * (1 - discountPct / 100m);

            var confirm = MessageBox.Show(
                $"XÁC NHẬN THANH TOÁN ĐƠN HÀNG:\n\n" +
                $"• Khách hàng: {cboCustomer.Text}\n" +
                $"• Số lượng món: {_cartItems.Count} mục ({_cartItems.Sum(c => c.SoLuong)} vé/dịch vụ)\n" +
                $"• Phương thức: {phuongThuc}\n" +
                $"• TỔNG TIỀN THANH TOÁN: {finalTotal:N0} VNĐ\n\n" +
                $"Bạn có muốn hoàn tất và xuất hóa đơn ngay?",
                "Xác Nhận Thanh Toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    var (success, msg, maHD) = _invoiceService.CreateInvoice(maKH, maNV, _cartItems, discountPct, phuongThuc, "Thanh toán tại quầy POS");
                    if (success && !string.IsNullOrEmpty(maHD))
                    {
                        MessageBox.Show(
                            $"Thanh toán thành công!\n\n" +
                            $"Mã Hóa Đơn: {maHD}\n" +
                            $"Tổng tiền: {finalTotal:N0} VNĐ\n" +
                            $"Trạng thái: Đã thanh toán",
                            "Hoàn Tất Đơn Hàng",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        _cartItems.Clear();
                        RefreshCartDisplay();
                        RenderTicketCards();
                    }
                    else
                    {
                        MessageBox.Show(msg, "Lỗi Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi tạo hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
