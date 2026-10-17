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
        private string _searchKeyword = string.Empty;
        private readonly ToolTip _toolTip = new ToolTip();

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

            _toolTip.AutoPopDelay = 15000;
            _toolTip.InitialDelay = 350;
            _toolTip.ReshowDelay = 100;
            _toolTip.ShowAlways = true;

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
                cboCustomer.Items.Add(new ComboItem { Text = "Khách vãng lai tại quầy", Value = "" });
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
            string cat = "TatCa";
            if (sender == btnCatTicket || (sender is Button b1 && b1.Tag?.ToString() == "Ve"))
            {
                cat = "Ve";
            }
            else if (sender == btnCatService || (sender is Button b2 && b2.Tag?.ToString() == "DichVu"))
            {
                cat = "DichVu";
            }
            else
            {
                cat = "TatCa";
            }

            _activeCategory = cat;

            btnCatAll.BackColor = (cat == "TatCa") ? Color.FromArgb(79, 70, 229) : Color.FromArgb(241, 245, 249);
            btnCatAll.ForeColor = (cat == "TatCa") ? Color.White : Color.FromArgb(71, 85, 105);

            btnCatTicket.BackColor = (cat == "Ve") ? Color.FromArgb(79, 70, 229) : Color.FromArgb(241, 245, 249);
            btnCatTicket.ForeColor = (cat == "Ve") ? Color.White : Color.FromArgb(71, 85, 105);

            btnCatService.BackColor = (cat == "DichVu") ? Color.FromArgb(79, 70, 229) : Color.FromArgb(241, 245, 249);
            btnCatService.ForeColor = (cat == "DichVu") ? Color.White : Color.FromArgb(71, 85, 105);

            RenderTicketCards();
        }

        private void TxtSearchItem_TextChanged(object? sender, EventArgs e)
        {
            _searchKeyword = txtSearchItem.Text.Trim();
            RenderTicketCards();
        }

        private void RenderTicketCards()
        {
            pnlTicketCards.Controls.Clear();
            _toolTip.RemoveAll();

            int cardCount = 0;

            // 1. Tickets
            if (_activeCategory == "TatCa" || _activeCategory == "Ve")
            {
                var query = _availableTickets.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(_searchKeyword))
                {
                    query = query.Where(t => t.TenVe.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                             t.MaVe.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                             t.LoaiVe.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase));
                }

                foreach (var ticket in query)
                {
                    pnlTicketCards.Controls.Add(CreateTicketCard(ticket));
                    cardCount++;
                }
            }

            // 2. Services
            if (_activeCategory == "TatCa" || _activeCategory == "DichVu")
            {
                var query = _availableServices.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(_searchKeyword))
                {
                    query = query.Where(s => s.TenDichVu.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                             s.MaDichVu.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                             s.LoaiDichVu.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase));
                }

                foreach (var s in query)
                {
                    pnlTicketCards.Controls.Add(CreateServiceCard(s));
                    cardCount++;
                }
            }

            if (cardCount == 0)
            {
                var lblEmpty = new Label
                {
                    Text = string.IsNullOrWhiteSpace(_searchKeyword)
                        ? "Không có mặt hàng nào trong danh mục này."
                        : $"Không tìm thấy vé hoặc dịch vụ nào khớp với từ khóa '{_searchKeyword}'.",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(500, 120),
                    Margin = new Padding(30, 40, 10, 0)
                };
                pnlTicketCards.Controls.Add(lblEmpty);
            }
        }

        private Panel CreateTicketCard(Ve ticket)
        {
            var detail = TicketDetailCatalog.GetTicketDetail(ticket);

            var card = new Panel
            {
                Size = new Size(235, 142),
                BackColor = Color.White,
                Margin = new Padding(6),
                Padding = new Padding(12),
                Cursor = Cursors.Default
            };
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            // Badge Category (LoaiVe)
            Color catBg = Color.FromArgb(241, 245, 249);
            Color catFg = Color.FromArgb(71, 85, 105);
            if (ticket.LoaiVe.Contains("VIP", StringComparison.OrdinalIgnoreCase))
            {
                catBg = Color.FromArgb(254, 243, 199);
                catFg = Color.FromArgb(180, 83, 9);
            }
            else if (ticket.LoaiVe.Contains("Trẻ em", StringComparison.OrdinalIgnoreCase))
            {
                catBg = Color.FromArgb(224, 242, 254);
                catFg = Color.FromArgb(3, 105, 161);
            }
            else if (ticket.LoaiVe.Contains("Gia đình", StringComparison.OrdinalIgnoreCase))
            {
                catBg = Color.FromArgb(236, 253, 245);
                catFg = Color.FromArgb(4, 120, 87);
            }
            else if (ticket.LoaiVe.Contains("Người lớn", StringComparison.OrdinalIgnoreCase))
            {
                catBg = Color.FromArgb(238, 242, 255);
                catFg = Color.FromArgb(79, 70, 229);
            }

            var lblLoai = new Label
            {
                Text = ticket.LoaiVe,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = catBg,
                ForeColor = catFg,
                Padding = new Padding(6, 2, 6, 2),
                AutoSize = true,
                Location = new Point(12, 10)
            };

            var lblThoiHan = new Label
            {
                Text = ticket.ThoiHan ?? "Trong ngày",
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(100, 116, 139),
                Padding = new Padding(6, 2, 6, 2),
                AutoSize = true
            };
            lblThoiHan.Location = new Point(card.Width - 12 - lblThoiHan.PreferredWidth, 10);

            // Ticket Name
            var lblName = new Label
            {
                Text = ticket.TenVe,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(12, 38),
                Size = new Size(211, 24),
                AutoEllipsis = true
            };

            // Ticket Price
            var lblPrice = new Label
            {
                Text = ticket.GiaVe.ToString("N0") + " đ",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                Location = new Point(12, 64),
                AutoSize = true
            };

            // Quantity & Cart
            var existingItem = _cartItems.FirstOrDefault(c => c.LoaiHang == "Ve" && c.MaHang == ticket.MaVe);
            int currentQty = existingItem?.SoLuong ?? 0;

            var lblQty = new Label
            {
                Text = currentQty.ToString(),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(30, 6),
                Size = new Size(34, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Details Button "ℹ Chi tiết"
            var btnDetails = new Button
            {
                Text = "ℹ Chi tiết",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Size = new Size(88, 32),
                Location = new Point(12, 96),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(238, 242, 255),
                ForeColor = Color.FromArgb(79, 70, 229),
                Cursor = Cursors.Hand
            };
            btnDetails.FlatAppearance.BorderSize = 0;
            btnDetails.Click += (s, e) =>
            {
                using var dlg = new FormChiTietQuyenLoiVe(detail);
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.RequestedAddToCart)
                {
                    AddToCart(ticket.MaVe, ticket.TenVe, "Ve", ticket.GiaVe, 1);
                    int q = int.Parse(lblQty.Text) + 1;
                    lblQty.Text = q.ToString();
                }
            };

            // Stepper: [-]  0  [+]
            var pnlStepper = new Panel
            {
                Location = new Point(126, 96),
                Size = new Size(96, 32),
                BackColor = Color.Transparent
            };

            var btnMinus = new Button
            {
                Text = "−",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(28, 28),
                Location = new Point(0, 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(15, 23, 42),
                Cursor = Cursors.Hand
            };
            btnMinus.FlatAppearance.BorderSize = 0;

            var btnPlus = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(28, 28),
                Location = new Point(66, 2),
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
                AddToCart(ticket.MaVe, ticket.TenVe, "Ve", ticket.GiaVe, 1);
            };

            btnMinus.Click += (s, e) =>
            {
                int q = int.Parse(lblQty.Text);
                if (q > 0)
                {
                    q--;
                    lblQty.Text = q.ToString();
                    AddToCart(ticket.MaVe, ticket.TenVe, "Ve", ticket.GiaVe, -1);
                }
            };

            pnlStepper.Controls.Add(btnMinus);
            pnlStepper.Controls.Add(lblQty);
            pnlStepper.Controls.Add(btnPlus);

            // Rich ToolTip on hover
            string tip = TicketDetailCatalog.BuildTicketToolTipText(detail);
            _toolTip.SetToolTip(card, tip);
            _toolTip.SetToolTip(lblName, tip);
            _toolTip.SetToolTip(lblPrice, tip);
            _toolTip.SetToolTip(btnDetails, "Bấm để xem danh sách trò chơi, tiện ích và điều kiện chi tiết của vé này");

            card.Controls.Add(lblLoai);
            card.Controls.Add(lblThoiHan);
            card.Controls.Add(lblName);
            card.Controls.Add(lblPrice);
            card.Controls.Add(btnDetails);
            card.Controls.Add(pnlStepper);

            return card;
        }

        private Panel CreateServiceCard(DichVu s)
        {
            var card = new Panel
            {
                Size = new Size(235, 142),
                BackColor = Color.White,
                Margin = new Padding(6),
                Padding = new Padding(12),
                Cursor = Cursors.Default
            };
            card.Paint += (sender, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            // Badge Category
            var lblLoai = new Label
            {
                Text = s.LoaiDichVu ?? "Dịch vụ",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = Color.FromArgb(243, 232, 255),
                ForeColor = Color.FromArgb(126, 34, 206),
                Padding = new Padding(6, 2, 6, 2),
                AutoSize = true,
                Location = new Point(12, 10)
            };

            // Badge Stock
            Color stockBg = s.SoLuongTon > 10 ? Color.FromArgb(236, 253, 245) : Color.FromArgb(254, 242, 242);
            Color stockFg = s.SoLuongTon > 10 ? Color.FromArgb(4, 120, 87) : Color.FromArgb(220, 38, 38);
            var lblStock = new Label
            {
                Text = $"Còn: {s.SoLuongTon}",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = stockBg,
                ForeColor = stockFg,
                Padding = new Padding(6, 2, 6, 2),
                AutoSize = true
            };
            lblStock.Location = new Point(card.Width - 12 - lblStock.PreferredWidth, 10);

            // Name
            var lblName = new Label
            {
                Text = s.TenDichVu,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(12, 38),
                Size = new Size(211, 24),
                AutoEllipsis = true
            };

            // Price
            var lblPrice = new Label
            {
                Text = s.DonGia.ToString("N0") + " đ",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                Location = new Point(12, 64),
                AutoSize = true
            };

            // Stepper
            var existingItem = _cartItems.FirstOrDefault(c => c.LoaiHang == "DichVu" && c.MaHang == s.MaDichVu);
            int currentQty = existingItem?.SoLuong ?? 0;

            var lblQty = new Label
            {
                Text = currentQty.ToString(),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(30, 6),
                Size = new Size(34, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Details info button
            var btnDetails = new Button
            {
                Text = "ℹ Chi tiết",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Size = new Size(88, 32),
                Location = new Point(12, 96),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(243, 232, 255),
                ForeColor = Color.FromArgb(126, 34, 206),
                Cursor = Cursors.Hand
            };
            btnDetails.FlatAppearance.BorderSize = 0;
            btnDetails.Click += (sender, e) =>
            {
                MessageBox.Show(
                    $"TÊN DỊCH VỤ: {s.TenDichVu}\n" +
                    $"Mã dịch vụ: {s.MaDichVu}\n" +
                    $"Phân loại: {s.LoaiDichVu}\n" +
                    $"Đơn giá: {s.DonGia:N0} VNĐ\n" +
                    $"Tồn kho hiện có: {s.SoLuongTon}\n" +
                    $"Trạng thái: {s.TrangThai}\n\n" +
                    $"Mô tả:\n{s.MoTa ?? "Dịch vụ phục vụ tại quầy khu vui chơi"}",
                    "Chi Tiết Dịch Vụ / Hàng Hóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            };

            var pnlStepper = new Panel
            {
                Location = new Point(126, 96),
                Size = new Size(96, 32),
                BackColor = Color.Transparent
            };

            var btnMinus = new Button
            {
                Text = "−",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(28, 28),
                Location = new Point(0, 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(15, 23, 42),
                Cursor = Cursors.Hand
            };
            btnMinus.FlatAppearance.BorderSize = 0;

            var btnPlus = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(28, 28),
                Location = new Point(66, 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnPlus.FlatAppearance.BorderSize = 0;

            btnPlus.Click += (sender, e) =>
            {
                int q = int.Parse(lblQty.Text);
                if (q >= s.SoLuongTon)
                {
                    MessageBox.Show($"Mặt hàng này chỉ còn {s.SoLuongTon} sản phẩm trong kho!", "Cảnh Báo Tồn Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                q++;
                lblQty.Text = q.ToString();
                AddToCart(s.MaDichVu, s.TenDichVu, "DichVu", s.DonGia, 1);
            };

            btnMinus.Click += (sender, e) =>
            {
                int q = int.Parse(lblQty.Text);
                if (q > 0)
                {
                    q--;
                    lblQty.Text = q.ToString();
                    AddToCart(s.MaDichVu, s.TenDichVu, "DichVu", s.DonGia, -1);
                }
            };

            pnlStepper.Controls.Add(btnMinus);
            pnlStepper.Controls.Add(lblQty);
            pnlStepper.Controls.Add(btnPlus);

            string tip = $"Dịch vụ: {s.TenDichVu}\nĐơn giá: {s.DonGia:N0} đ\nLoại: {s.LoaiDichVu}\nTồn kho: {s.SoLuongTon}\n{s.MoTa}";
            _toolTip.SetToolTip(card, tip);
            _toolTip.SetToolTip(lblName, tip);
            _toolTip.SetToolTip(btnDetails, "Bấm để xem chi tiết thông tin dịch vụ này");

            card.Controls.Add(lblLoai);
            card.Controls.Add(lblStock);
            card.Controls.Add(lblName);
            card.Controls.Add(lblPrice);
            card.Controls.Add(btnDetails);
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
                        Text = "",
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
