using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.DTOs;
using QuanLyKhuVuiChoi.Forms.Common;
using QuanLyKhuVuiChoi.Models;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Billing
{
    public class FormBanVe : Form
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
        private readonly GameService _gameService;
        private readonly IExcelService _excelService;
        private readonly QuanLyKhuVuiChoiDbContext _context;

        // Giỏ hàng
        private readonly List<CartItemDto> _cartItems = new List<CartItemDto>();

        // Controls
        private FlowLayoutPanel pnlTicketCards = null!;
        private FlowLayoutPanel pnlCartItems = null!;
        private Label lblSubtotal = null!;
        private Label lblTotal = null!;
        private NumericUpDown numDiscountPercent = null!;
        private ComboBox cboCustomer = null!;
        private ComboBox cboPaymentMethod = null!;
        private DateTimePicker dtpVisitDate = null!;
        private Button btnCheckout = null!;

        // Danh sách mặt hàng từ CSDL
        private List<Ve> _availableTickets = new List<Ve>();
        private List<DichVu> _availableServices = new List<DichVu>();
        private string _activeCategory = "TatCa";

        public FormBanVe(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
            _invoiceService = new InvoiceService(_context, _excelService);
            _customerService = new CustomerService(_context, _excelService);
            _ticketService = new TicketService(_context, _excelService);
            _parkService = new ParkServiceService(_context, _excelService);
            _gameService = new GameService(_context, _excelService);

            InitializeComponent();
            LoadDataFromDatabase();
        }

        private void InitializeComponent()
        {
            this.Text = "TẠO ĐƠN BÁN VÉ & DỊCH VỤ (POS)";
            this.Size = new Size(1220, 780);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.MainFont;

            // ==========================================
            // TOP HEADER: BREADCRUMB & STEP INDICATOR
            // ==========================================
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.White,
                Padding = new Padding(24, 14, 24, 10)
            };
            pnlTop.Paint += (s, e) =>
            {
                using var pen = new Pen(UITheme.CardBorder, 1);
                e.Graphics.DrawLine(pen, 0, pnlTop.Height - 1, pnlTop.Width, pnlTop.Height - 1);
            };

            var lblBreadcrumb = new Label
            {
                Text = "Bán vé / Tạo đơn",
                Font = UITheme.SmallFont,
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 12),
                AutoSize = true
            };

            var lblTitle = new Label
            {
                Text = "Tạo đơn bán vé",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = UITheme.TextMain,
                Location = new Point(22, 28),
                AutoSize = true
            };

            var lblSub = new Label
            {
                Text = "Chọn loại vé, số lượng và thông tin khách hàng",
                Font = UITheme.SmallFont,
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 58),
                AutoSize = true
            };

            // Step Indicator (Right)
            var pnlSteps = new Panel
            {
                Size = new Size(380, 48),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTop.Width - 410, 20),
                BackColor = Color.Transparent
            };
            pnlSteps.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                string[] stepNames = { "1 Chọn vé", "2 Thông tin", "3 Thanh toán", "4 Hoàn tất" };
                int startX = 10;
                int gap = 90;

                for (int i = 0; i < stepNames.Length; i++)
                {
                    int cx = startX + i * gap;
                    bool isActive = (i == 0);

                    // Circle
                    var rect = new Rectangle(cx, 8, 22, 22);
                    using var brush = new SolidBrush(isActive ? UITheme.Primary : Color.FromArgb(226, 232, 240));
                    g.FillEllipse(brush, rect);

                    // Step number
                    using var fontNum = new Font("Segoe UI", 8F, FontStyle.Bold);
                    using var brushNum = new SolidBrush(isActive ? Color.White : Color.FromArgb(100, 116, 139));
                    using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString((i + 1).ToString(), fontNum, brushNum, rect, sf);

                    // Step name
                    using var fontName = new Font("Segoe UI", 7.5F, isActive ? FontStyle.Bold : FontStyle.Regular);
                    using var brushName = new SolidBrush(isActive ? UITheme.Primary : UITheme.TextMuted);
                    g.DrawString(stepNames[i].Substring(2), fontName, brushName, cx - 4, 32);

                    // Connecting line
                    if (i < stepNames.Length - 1)
                    {
                        using var penLine = new Pen(Color.FromArgb(226, 232, 240), 2);
                        g.DrawLine(penLine, cx + 24, 19, cx + gap - 4, 19);
                    }
                }
            };

            pnlTop.Controls.Add(lblBreadcrumb);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblSub);
            pnlTop.Controls.Add(pnlSteps);

            // ==========================================
            // MAIN BODY: SPLIT LEFT (TICKETS) & RIGHT (CART)
            // ==========================================
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 16, 20, 16),
                BackColor = UITheme.Background
            };

            // ------------------------------------------
            // RIGHT CART PANEL (ĐƠN HÀNG)
            // ------------------------------------------
            var pnlCart = new Panel
            {
                Dock = DockStyle.Right,
                Width = 390,
                BackColor = Color.White,
                Padding = new Padding(18, 16, 18, 16)
            };
            pnlCart.Paint += (s, e) =>
            {
                using var pen = new Pen(UITheme.CardBorder, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlCart.Width - 1, pnlCart.Height - 1);
            };

            // Cart Header
            var pnlCartHeader = new Panel { Dock = DockStyle.Top, Height = 36, BackColor = Color.White };
            var lblCartTitle = new Label
            {
                Text = "Đơn hàng",
                Font = UITheme.SubHeaderFont,
                ForeColor = UITheme.TextMain,
                Location = new Point(0, 4),
                AutoSize = true
            };
            var lnkClearCart = new LinkLabel
            {
                Text = "Xóa tất cả",
                Font = UITheme.SmallFont,
                LinkColor = UITheme.Danger,
                ActiveLinkColor = Color.DarkRed,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlCart.Width - 95, 8),
                AutoSize = true
            };
            lnkClearCart.LinkClicked += (s, e) =>
            {
                if (_cartItems.Any())
                {
                    _cartItems.Clear();
                    RefreshCartDisplay();
                    RenderTicketCards();
                }
            };
            pnlCartHeader.Controls.Add(lblCartTitle);
            pnlCartHeader.Controls.Add(lnkClearCart);

            // Customer Select Row inside Cart
            var pnlCustomerRow = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Color.White, Padding = new Padding(0, 4, 0, 6) };
            var lblCustHeader = new Label { Text = "Khách hàng:", Font = UITheme.SmallBoldFont, ForeColor = UITheme.TextMuted, Location = new Point(0, 4), AutoSize = true };
            cboCustomer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = UITheme.MainFont,
                Location = new Point(0, 24),
                Width = 354
            };
            pnlCustomerRow.Controls.Add(lblCustHeader);
            pnlCustomerRow.Controls.Add(cboCustomer);

            // Cart Items Scrollable List
            pnlCartItems = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(0, 4, 0, 4)
            };

            // Cart Bottom Totals & Checkout
            var pnlCartBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 220,
                BackColor = Color.White,
                Padding = new Padding(0, 10, 0, 0)
            };
            pnlCartBottom.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(241, 245, 249), 1);
                e.Graphics.DrawLine(pen, 0, 0, pnlCartBottom.Width, 0);
            };

            // Subtotal Row
            var lblSubLabel = new Label { Text = "Tạm tính", Font = UITheme.MainFont, ForeColor = UITheme.TextMuted, Location = new Point(0, 14), AutoSize = true };
            lblSubtotal = new Label { Text = "0đ", Font = UITheme.BoldFont, ForeColor = UITheme.TextMain, Location = new Point(234, 14), Width = 120, TextAlign = ContentAlignment.MiddleRight };

            // Discount Row
            var lblDiscLabel = new Label { Text = "Giảm giá (%)", Font = UITheme.MainFont, ForeColor = UITheme.TextMuted, Location = new Point(0, 44), AutoSize = true };
            numDiscountPercent = new NumericUpDown
            {
                Location = new Point(284, 42),
                Width = 70,
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Font = UITheme.SmallBoldFont
            };
            numDiscountPercent.ValueChanged += (s, e) => UpdateTotals();

            // Payment method row
            var lblPayLabel = new Label { Text = "Phương thức", Font = UITheme.MainFont, ForeColor = UITheme.TextMuted, Location = new Point(0, 76), AutoSize = true };
            cboPaymentMethod = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = UITheme.SmallFont,
                Location = new Point(224, 74),
                Width = 130
            };
            cboPaymentMethod.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản (QR)", "Quẹt thẻ POS" });
            cboPaymentMethod.SelectedIndex = 0;

            // Total Row
            var lblTotalLabel = new Label { Text = "Tổng cộng", Font = UITheme.SubHeaderFont, ForeColor = UITheme.TextMain, Location = new Point(0, 112), AutoSize = true };
            lblTotal = new Label { Text = "0đ", Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = UITheme.Primary, Location = new Point(154, 106), Width = 200, TextAlign = ContentAlignment.MiddleRight };

            // Checkout Button
            btnCheckout = new Button
            {
                Text = "Tiếp tục thanh toán  →",
                Dock = DockStyle.Bottom,
                Height = 36,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            UITheme.StyleButton(btnCheckout, UITheme.Primary, Color.White, 36);
            btnCheckout.Click += BtnCheckout_Click;

            pnlCartBottom.Controls.Add(lblSubLabel);
            pnlCartBottom.Controls.Add(lblSubtotal);
            pnlCartBottom.Controls.Add(lblDiscLabel);
            pnlCartBottom.Controls.Add(numDiscountPercent);
            pnlCartBottom.Controls.Add(lblPayLabel);
            pnlCartBottom.Controls.Add(cboPaymentMethod);
            pnlCartBottom.Controls.Add(lblTotalLabel);
            pnlCartBottom.Controls.Add(lblTotal);
            pnlCartBottom.Controls.Add(btnCheckout);

            pnlCart.Controls.Add(pnlCartItems);
            pnlCart.Controls.Add(pnlCustomerRow);
            pnlCart.Controls.Add(pnlCartHeader);
            pnlCart.Controls.Add(pnlCartBottom);

            // ------------------------------------------
            // LEFT AREA: TICKET SELECTION & DATE PICKER
            // ------------------------------------------
            var pnlLeft = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 0, 16, 0),
                BackColor = UITheme.Background
            };

            // Section 1: Chọn ngày tham quan
            var pnlDateSection = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.White,
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(0, 0, 0, 12)
            };
            pnlDateSection.Paint += (s, e) =>
            {
                using var pen = new Pen(UITheme.CardBorder, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlDateSection.Width - 1, pnlDateSection.Height - 1);
            };

            var lblStep1 = new Label
            {
                Text = "1. Chọn ngày tham quan",
                Font = UITheme.BoldFont,
                ForeColor = UITheme.TextMain,
                Location = new Point(14, 10),
                AutoSize = true
            };

            dtpVisitDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = UITheme.MainFont,
                Location = new Point(16, 32),
                Width = 160,
                Value = DateTime.Today
            };
            pnlDateSection.Controls.Add(lblStep1);
            pnlDateSection.Controls.Add(dtpVisitDate);

            // Section 2: Chọn loại vé & Dịch vụ (Filter tabs + Card grid)
            var pnlTicketSection = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(0, 12, 0, 0)
            };
            pnlTicketSection.Paint += (s, e) =>
            {
                using var pen = new Pen(UITheme.CardBorder, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlTicketSection.Width - 1, pnlTicketSection.Height - 1);
            };

            var pnlFilterBar = new Panel { Dock = DockStyle.Top, Height = 42, BackColor = Color.White };
            var lblStep2 = new Label
            {
                Text = "2. Chọn loại vé & dịch vụ",
                Font = UITheme.BoldFont,
                ForeColor = UITheme.TextMain,
                Location = new Point(4, 8),
                AutoSize = true
            };

            // Category Filter Pills
            var pnlPills = new FlowLayoutPanel
            {
                Location = new Point(190, 4),
                Size = new Size(520, 36),
                FlowDirection = FlowDirection.LeftToRight
            };

            Button CreateCategoryPill(string text, string catKey)
            {
                var btn = new Button
                {
                    Text = text,
                    Font = UITheme.SmallBoldFont,
                    Height = 28,
                    AutoSize = true,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = _activeCategory == catKey ? UITheme.Primary : Color.FromArgb(241, 245, 249),
                    ForeColor = _activeCategory == catKey ? Color.White : UITheme.TextMuted,
                    Cursor = Cursors.Hand,
                    Margin = new Padding(3, 0, 3, 0)
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += (s, e) =>
                {
                    _activeCategory = catKey;
                    foreach (Control c in pnlPills.Controls)
                    {
                        if (c is Button b)
                        {
                            bool active = (b == btn);
                            b.BackColor = active ? UITheme.Primary : Color.FromArgb(241, 245, 249);
                            b.ForeColor = active ? Color.White : UITheme.TextMuted;
                        }
                    }
                    RenderTicketCards();
                };
                return btn;
            }

            pnlPills.Controls.Add(CreateCategoryPill("Tất cả", "TatCa"));
            pnlPills.Controls.Add(CreateCategoryPill("Vé vào cổng", "Ve"));
            pnlPills.Controls.Add(CreateCategoryPill("Dịch vụ & Ẩm thực", "DichVu"));

            pnlFilterBar.Controls.Add(lblStep2);
            pnlFilterBar.Controls.Add(pnlPills);

            // Ticket Cards Grid Container
            pnlTicketCards = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(4, 10, 4, 10)
            };

            pnlTicketSection.Controls.Add(pnlTicketCards);
            pnlTicketSection.Controls.Add(pnlFilterBar);

            // Left container assembly
            var pnlLeftContainer = new Panel { Dock = DockStyle.Fill };
            pnlLeftContainer.Controls.Add(pnlTicketSection);
            pnlLeftContainer.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 12 }); // spacer
            pnlLeftContainer.Controls.Add(pnlDateSection);

            pnlLeft.Controls.Add(pnlLeftContainer);

            pnlBody.Controls.Add(pnlLeft);
            pnlBody.Controls.Add(pnlCart);

            this.Controls.Add(pnlBody);
            this.Controls.Add(pnlTop);
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

                RenderTicketCards();
                RefreshCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp danh mục vé: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                using var pen = new Pen(UITheme.CardBorder, 1);
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
                Font = UITheme.BoldFont,
                ForeColor = UITheme.TextMain,
                Location = new Point(48, 10),
                Size = new Size(160, 20),
                AutoEllipsis = true
            };

            // Price
            var lblPrice = new Label
            {
                Text = price.ToString("N0") + "đ",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = UITheme.Primary,
                Location = new Point(48, 30),
                AutoSize = true
            };

            // Description
            var lblDesc = new Label
            {
                Text = desc,
                Font = UITheme.SmallFont,
                ForeColor = UITheme.TextMuted,
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
                Font = UITheme.SmallBoldFont,
                ForeColor = UITheme.TextMain,
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
                ForeColor = UITheme.TextMain,
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
                BackColor = UITheme.Primary,
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
                    Font = UITheme.SmallFont,
                    ForeColor = UITheme.TextMuted,
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
                        Font = UITheme.BoldFont,
                        ForeColor = UITheme.TextMain,
                        Location = new Point(4, 4),
                        Size = new Size(190, 18),
                        AutoEllipsis = true
                    };

                    var lblDetail = new Label
                    {
                        Text = $"{item.DonGia:N0}đ  x  {item.SoLuong}",
                        Font = UITheme.SmallFont,
                        ForeColor = UITheme.TextMuted,
                        Location = new Point(4, 24),
                        AutoSize = true
                    };

                    var lblTotal = new Label
                    {
                        Text = item.ThanhTien.ToString("N0") + "đ",
                        Font = UITheme.BoldFont,
                        ForeColor = UITheme.TextMain,
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
                        ForeColor = UITheme.Danger,
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
                    row.Controls.Add(lblTotal);
                    row.Controls.Add(btnRemove);

                    pnlCartItems.Controls.Add(row);
                }
            }

            UpdateTotals();
        }

        private void UpdateTotals()
        {
            decimal subtotal = _cartItems.Sum(i => i.ThanhTien);
            decimal discountPct = numDiscountPercent.Value;
            decimal discountVal = subtotal * (discountPct / 100m);
            decimal finalTotal = subtotal - discountVal;

            lblSubtotal.Text = subtotal.ToString("N0") + "đ";
            lblTotal.Text = finalTotal.ToString("N0") + "đ";

            btnCheckout.Enabled = _cartItems.Any();
            btnCheckout.BackColor = _cartItems.Any() ? UITheme.Primary : Color.FromArgb(203, 213, 225);
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
