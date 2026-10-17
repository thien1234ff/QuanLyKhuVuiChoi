namespace QuanLyKhuVuiChoi.Forms.Billing
{
    partial class FormBanVe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBreadcrumb = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlTicketCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFilterBar = new System.Windows.Forms.Panel();
            this.flpCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCatAll = new System.Windows.Forms.Button();
            this.btnCatTicket = new System.Windows.Forms.Button();
            this.btnCatService = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearchItem = new System.Windows.Forms.TextBox();
            this.pnlCart = new System.Windows.Forms.Panel();
            this.pnlCartItems = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCustomerRow = new System.Windows.Forms.Panel();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustHeader = new System.Windows.Forms.Label();
            this.pnlCartBottom = new System.Windows.Forms.Panel();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblPayLabel = new System.Windows.Forms.Label();
            this.numDiscountPercent = new System.Windows.Forms.NumericUpDown();
            this.lblDiscLabel = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblSubLabel = new System.Windows.Forms.Label();
            this.pnlCartHeader = new System.Windows.Forms.Panel();
            this.lnkClearCart = new System.Windows.Forms.LinkLabel();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlFilterBar.SuspendLayout();
            this.flpCategories.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlCart.SuspendLayout();
            this.pnlCustomerRow.SuspendLayout();
            this.pnlCartBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscountPercent)).BeginInit();
            this.pnlCartHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.lblSub);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.lblBreadcrumb);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(24, 14, 24, 10);
            this.pnlTop.Size = new System.Drawing.Size(1220, 85);
            this.pnlTop.TabIndex = 0;
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSub.Location = new System.Drawing.Point(24, 58);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(325, 20);
            this.lblSub.TabIndex = 2;
            this.lblSub.Text = "Chọn loại vé, dịch vụ và nhập thông tin khách hàng";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(437, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "TẠO ĐƠN BÁN VÉ & DỊCH VỤ (POS)";
            // 
            // lblBreadcrumb
            // 
            this.lblBreadcrumb.AutoSize = true;
            this.lblBreadcrumb.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBreadcrumb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBreadcrumb.Location = new System.Drawing.Point(24, 5);
            this.lblBreadcrumb.Name = "lblBreadcrumb";
            this.lblBreadcrumb.Size = new System.Drawing.Size(124, 18);
            this.lblBreadcrumb.TabIndex = 0;
            this.lblBreadcrumb.Text = "Quầy POS / Bán vé";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlBody.Controls.Add(this.pnlLeft);
            this.pnlBody.Controls.Add(this.pnlCart);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 85);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBody.Size = new System.Drawing.Size(1220, 695);
            this.pnlBody.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.pnlTicketCards);
            this.pnlLeft.Controls.Add(this.pnlFilterBar);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(15, 15);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.pnlLeft.Size = new System.Drawing.Size(790, 665);
            this.pnlLeft.TabIndex = 1;
            // 
            // pnlTicketCards
            // 
            this.pnlTicketCards.AutoScroll = true;
            this.pnlTicketCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlTicketCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTicketCards.Location = new System.Drawing.Point(0, 48);
            this.pnlTicketCards.Name = "pnlTicketCards";
            this.pnlTicketCards.Padding = new System.Windows.Forms.Padding(5);
            this.pnlTicketCards.Size = new System.Drawing.Size(775, 617);
            this.pnlTicketCards.TabIndex = 1;
            // 
            // pnlFilterBar
            // 
            this.pnlFilterBar.BackColor = System.Drawing.Color.White;
            this.pnlFilterBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilterBar.Controls.Add(this.pnlSearch);
            this.pnlFilterBar.Controls.Add(this.flpCategories);
            this.pnlFilterBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterBar.Location = new System.Drawing.Point(0, 0);
            this.pnlFilterBar.Name = "pnlFilterBar";
            this.pnlFilterBar.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pnlFilterBar.Size = new System.Drawing.Size(775, 48);
            this.pnlFilterBar.TabIndex = 0;
            // 
            // flpCategories
            // 
            this.flpCategories.AutoSize = true;
            this.flpCategories.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpCategories.BackColor = System.Drawing.Color.Transparent;
            this.flpCategories.Controls.Add(this.btnCatAll);
            this.flpCategories.Controls.Add(this.btnCatTicket);
            this.flpCategories.Controls.Add(this.btnCatService);
            this.flpCategories.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpCategories.Location = new System.Drawing.Point(8, 6);
            this.flpCategories.Margin = new System.Windows.Forms.Padding(0);
            this.flpCategories.Name = "flpCategories";
            this.flpCategories.Size = new System.Drawing.Size(282, 36);
            this.flpCategories.TabIndex = 0;
            this.flpCategories.WrapContents = false;
            // 
            // btnCatAll
            // 
            this.btnCatAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnCatAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCatAll.FlatAppearance.BorderSize = 0;
            this.btnCatAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCatAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCatAll.ForeColor = System.Drawing.Color.White;
            this.btnCatAll.Location = new System.Drawing.Point(0, 2);
            this.btnCatAll.Margin = new System.Windows.Forms.Padding(0, 2, 6, 0);
            this.btnCatAll.Name = "btnCatAll";
            this.btnCatAll.Size = new System.Drawing.Size(85, 32);
            this.btnCatAll.TabIndex = 0;
            this.btnCatAll.Tag = "TatCa";
            this.btnCatAll.Text = "Tất cả";
            this.btnCatAll.UseVisualStyleBackColor = false;
            this.btnCatAll.Click += new System.EventHandler(this.BtnCategory_Click);
            // 
            // btnCatTicket
            // 
            this.btnCatTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCatTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCatTicket.FlatAppearance.BorderSize = 0;
            this.btnCatTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCatTicket.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCatTicket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCatTicket.Location = new System.Drawing.Point(91, 2);
            this.btnCatTicket.Margin = new System.Windows.Forms.Padding(0, 2, 6, 0);
            this.btnCatTicket.Name = "btnCatTicket";
            this.btnCatTicket.Size = new System.Drawing.Size(90, 32);
            this.btnCatTicket.TabIndex = 1;
            this.btnCatTicket.Tag = "Ve";
            this.btnCatTicket.Text = "Vé Vào";
            this.btnCatTicket.UseVisualStyleBackColor = false;
            this.btnCatTicket.Click += new System.EventHandler(this.BtnCategory_Click);
            // 
            // btnCatService
            // 
            this.btnCatService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCatService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCatService.FlatAppearance.BorderSize = 0;
            this.btnCatService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCatService.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCatService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCatService.Location = new System.Drawing.Point(187, 2);
            this.btnCatService.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnCatService.Name = "btnCatService";
            this.btnCatService.Size = new System.Drawing.Size(95, 32);
            this.btnCatService.TabIndex = 2;
            this.btnCatService.Tag = "DichVu";
            this.btnCatService.Text = "Dịch Vụ";
            this.btnCatService.UseVisualStyleBackColor = false;
            this.btnCatService.Click += new System.EventHandler(this.BtnCategory_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.Controls.Add(this.txtSearchItem);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSearch.Location = new System.Drawing.Point(535, 6);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlSearch.Size = new System.Drawing.Size(230, 36);
            this.pnlSearch.TabIndex = 1;
            // 
            // txtSearchItem
            // 
            this.txtSearchItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearchItem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearchItem.Location = new System.Drawing.Point(0, 3);
            this.txtSearchItem.Name = "txtSearchItem";
            this.txtSearchItem.PlaceholderText = "Tìm vé, dịch vụ theo tên...";
            this.txtSearchItem.Size = new System.Drawing.Size(230, 29);
            this.txtSearchItem.TabIndex = 0;
            this.txtSearchItem.TextChanged += new System.EventHandler(this.TxtSearchItem_TextChanged);
            // 
            // pnlCart
            // 
            this.pnlCart.BackColor = System.Drawing.Color.White;
            this.pnlCart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCart.Controls.Add(this.pnlCartItems);
            this.pnlCart.Controls.Add(this.pnlCustomerRow);
            this.pnlCart.Controls.Add(this.pnlCartBottom);
            this.pnlCart.Controls.Add(this.pnlCartHeader);
            this.pnlCart.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCart.Location = new System.Drawing.Point(825, 15);
            this.pnlCart.Name = "pnlCart";
            this.pnlCart.Padding = new System.Windows.Forms.Padding(15);
            this.pnlCart.Size = new System.Drawing.Size(380, 665);
            this.pnlCart.TabIndex = 0;
            // 
            // pnlCartItems
            // 
            this.pnlCartItems.AutoScroll = true;
            this.pnlCartItems.BackColor = System.Drawing.Color.White;
            this.pnlCartItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCartItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlCartItems.Location = new System.Drawing.Point(15, 101);
            this.pnlCartItems.Name = "pnlCartItems";
            this.pnlCartItems.Size = new System.Drawing.Size(368, 327);
            this.pnlCartItems.TabIndex = 2;
            this.pnlCartItems.WrapContents = false;
            // 
            // pnlCustomerRow
            // 
            this.pnlCustomerRow.Controls.Add(this.cboCustomer);
            this.pnlCustomerRow.Controls.Add(this.lblCustHeader);
            this.pnlCustomerRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerRow.Location = new System.Drawing.Point(15, 45);
            this.pnlCustomerRow.Name = "pnlCustomerRow";
            this.pnlCustomerRow.Size = new System.Drawing.Size(368, 56);
            this.pnlCustomerRow.TabIndex = 1;
            // 
            // cboCustomer
            // 
            this.cboCustomer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(0, 27);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(368, 29);
            this.cboCustomer.TabIndex = 1;
            // 
            // lblCustHeader
            // 
            this.lblCustHeader.AutoSize = true;
            this.lblCustHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustHeader.Location = new System.Drawing.Point(0, 2);
            this.lblCustHeader.Name = "lblCustHeader";
            this.lblCustHeader.Size = new System.Drawing.Size(94, 20);
            this.lblCustHeader.TabIndex = 0;
            this.lblCustHeader.Text = "Khách hàng:";
            // 
            // pnlCartBottom
            // 
            this.pnlCartBottom.Controls.Add(this.btnCheckout);
            this.pnlCartBottom.Controls.Add(this.lblTotal);
            this.pnlCartBottom.Controls.Add(this.lblTotalLabel);
            this.pnlCartBottom.Controls.Add(this.cboPaymentMethod);
            this.pnlCartBottom.Controls.Add(this.lblPayLabel);
            this.pnlCartBottom.Controls.Add(this.numDiscountPercent);
            this.pnlCartBottom.Controls.Add(this.lblDiscLabel);
            this.pnlCartBottom.Controls.Add(this.lblSubtotal);
            this.pnlCartBottom.Controls.Add(this.lblSubLabel);
            this.pnlCartBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCartBottom.Location = new System.Drawing.Point(15, 428);
            this.pnlCartBottom.Name = "pnlCartBottom";
            this.pnlCartBottom.Size = new System.Drawing.Size(368, 220);
            this.pnlCartBottom.TabIndex = 3;
            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(0, 175);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(368, 45);
            this.btnCheckout.TabIndex = 8;
            this.btnCheckout.Text = "TIẾP TỤC THANH TOÁN";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.BtnCheckout_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblTotal.Location = new System.Drawing.Point(140, 125);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(225, 35);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "0 đ";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalLabel.Location = new System.Drawing.Point(0, 130);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(115, 25);
            this.lblTotalLabel.TabIndex = 6;
            this.lblTotalLabel.Text = "Tổng cộng:";
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboPaymentMethod.FormattingEnabled = true;
            this.cboPaymentMethod.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản (QR)",
            "Quẹt thẻ POS"});
            this.cboPaymentMethod.Location = new System.Drawing.Point(210, 85);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(155, 28);
            this.cboPaymentMethod.TabIndex = 5;
            // 
            // lblPayLabel
            // 
            this.lblPayLabel.AutoSize = true;
            this.lblPayLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPayLabel.Location = new System.Drawing.Point(0, 88);
            this.lblPayLabel.Name = "lblPayLabel";
            this.lblPayLabel.Size = new System.Drawing.Size(94, 20);
            this.lblPayLabel.TabIndex = 4;
            this.lblPayLabel.Text = "Phương thức:";
            // 
            // numDiscountPercent
            // 
            this.numDiscountPercent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.numDiscountPercent.Location = new System.Drawing.Point(285, 48);
            this.numDiscountPercent.Name = "numDiscountPercent";
            this.numDiscountPercent.Size = new System.Drawing.Size(80, 27);
            this.numDiscountPercent.TabIndex = 3;
            this.numDiscountPercent.ValueChanged += new System.EventHandler(this.NumDiscountPercent_ValueChanged);
            // 
            // lblDiscLabel
            // 
            this.lblDiscLabel.AutoSize = true;
            this.lblDiscLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiscLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDiscLabel.Location = new System.Drawing.Point(0, 50);
            this.lblDiscLabel.Name = "lblDiscLabel";
            this.lblDiscLabel.Size = new System.Drawing.Size(97, 20);
            this.lblDiscLabel.TabIndex = 2;
            this.lblDiscLabel.Text = "Giảm giá (%):";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.Location = new System.Drawing.Point(215, 12);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(150, 22);
            this.lblSubtotal.TabIndex = 1;
            this.lblSubtotal.Text = "0 đ";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubLabel
            // 
            this.lblSubLabel.AutoSize = true;
            this.lblSubLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubLabel.Location = new System.Drawing.Point(0, 15);
            this.lblSubLabel.Name = "lblSubLabel";
            this.lblSubLabel.Size = new System.Drawing.Size(68, 20);
            this.lblSubLabel.TabIndex = 0;
            this.lblSubLabel.Text = "Tạm tính:";
            // 
            // pnlCartHeader
            // 
            this.pnlCartHeader.Controls.Add(this.lnkClearCart);
            this.pnlCartHeader.Controls.Add(this.lblCartTitle);
            this.pnlCartHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCartHeader.Location = new System.Drawing.Point(15, 15);
            this.pnlCartHeader.Name = "pnlCartHeader";
            this.pnlCartHeader.Size = new System.Drawing.Size(368, 30);
            this.pnlCartHeader.TabIndex = 0;
            // 
            // lnkClearCart
            // 
            this.lnkClearCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkClearCart.AutoSize = true;
            this.lnkClearCart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lnkClearCart.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lnkClearCart.Location = new System.Drawing.Point(288, 5);
            this.lnkClearCart.Name = "lnkClearCart";
            this.lnkClearCart.Size = new System.Drawing.Size(77, 20);
            this.lnkClearCart.TabIndex = 1;
            this.lnkClearCart.TabStop = true;
            this.lnkClearCart.Text = "Xóa tất cả";
            this.lnkClearCart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkClearCart_LinkClicked);
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.AutoSize = true;
            this.lblCartTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCartTitle.Location = new System.Drawing.Point(0, 2);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Size = new System.Drawing.Size(188, 25);
            this.lblCartTitle.TabIndex = 0;
            this.lblCartTitle.Text = "Giỏ Hàng Vé/DV";
            // 
            // FormBanVe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1220, 780);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormBanVe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẦY BÁN VÉ & DỊCH VỤ (POS)";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlFilterBar.ResumeLayout(false);
            this.pnlFilterBar.PerformLayout();
            this.flpCategories.ResumeLayout(false);
            this.flpCategories.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlCart.ResumeLayout(false);
            this.pnlCustomerRow.ResumeLayout(false);
            this.pnlCustomerRow.PerformLayout();
            this.pnlCartBottom.ResumeLayout(false);
            this.pnlCartBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscountPercent)).EndInit();
            this.pnlCartHeader.ResumeLayout(false);
            this.pnlCartHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblBreadcrumb;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlCart;
        private System.Windows.Forms.Panel pnlCartHeader;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.LinkLabel lnkClearCart;
        private System.Windows.Forms.Panel pnlCustomerRow;
        private System.Windows.Forms.Label lblCustHeader;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.FlowLayoutPanel pnlCartItems;
        private System.Windows.Forms.Panel pnlCartBottom;
        private System.Windows.Forms.Label lblSubLabel;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscLabel;
        private System.Windows.Forms.NumericUpDown numDiscountPercent;
        private System.Windows.Forms.Label lblPayLabel;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlFilterBar;
        private System.Windows.Forms.FlowLayoutPanel flpCategories;
        private System.Windows.Forms.Button btnCatAll;
        private System.Windows.Forms.Button btnCatTicket;
        private System.Windows.Forms.Button btnCatService;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearchItem;
        private System.Windows.Forms.FlowLayoutPanel pnlTicketCards;
    }
}
