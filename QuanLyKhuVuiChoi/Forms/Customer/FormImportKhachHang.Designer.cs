namespace QuanLyKhuVuiChoi.Forms.Customer
{
    partial class FormImportKhachHang
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnDownloadTemplate = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConfirmImport = new System.Windows.Forms.Button();
            this.lblSummary = new System.Windows.Forms.Label();
            this.splitMiddle = new System.Windows.Forms.SplitContainer();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.grpErrors = new System.Windows.Forms.GroupBox();
            this.lstErrors = new System.Windows.Forms.ListBox();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMiddle)).BeginInit();
            this.splitMiddle.Panel1.SuspendLayout();
            this.splitMiddle.Panel2.SuspendLayout();
            this.splitMiddle.SuspendLayout();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.grpErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.btnBrowse);
            this.pnlTop.Controls.Add(this.btnDownloadTemplate);
            this.pnlTop.Controls.Add(this.txtFilePath);
            this.pnlTop.Controls.Add(this.lblPath);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1000, 105);
            this.pnlTop.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(620, 52);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(130, 32);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = " Chọn File...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // btnDownloadTemplate
            // 
            this.btnDownloadTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnDownloadTemplate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownloadTemplate.FlatAppearance.BorderSize = 0;
            this.btnDownloadTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadTemplate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDownloadTemplate.ForeColor = System.Drawing.Color.White;
            this.btnDownloadTemplate.Location = new System.Drawing.Point(760, 52);
            this.btnDownloadTemplate.Name = "btnDownloadTemplate";
            this.btnDownloadTemplate.Size = new System.Drawing.Size(180, 32);
            this.btnDownloadTemplate.TabIndex = 3;
            this.btnDownloadTemplate.Text = " Tải File Mẫu Excel";
            this.btnDownloadTemplate.UseVisualStyleBackColor = false;
            this.btnDownloadTemplate.Click += new System.EventHandler(this.BtnDownloadTemplate_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtFilePath.Location = new System.Drawing.Point(170, 54);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(435, 29);
            this.txtFilePath.TabIndex = 2;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPath.Location = new System.Drawing.Point(20, 58);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(147, 20);
            this.lblPath.TabIndex = 1;
            this.lblPath.Text = "Đường dẫn file .xlsx:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(564, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = " NHẬP DỮ LIỆU KHÁCH HÀNG HÀNG LOẠT TỪ EXCEL";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnConfirmImport);
            this.pnlBottom.Controls.Add(this.lblSummary);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 580);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1000, 70);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(850, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 38);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnConfirmImport
            // 
            this.btnConfirmImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnConfirmImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmImport.Enabled = false;
            this.btnConfirmImport.FlatAppearance.BorderSize = 0;
            this.btnConfirmImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmImport.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnConfirmImport.ForeColor = System.Drawing.Color.White;
            this.btnConfirmImport.Location = new System.Drawing.Point(620, 16);
            this.btnConfirmImport.Name = "btnConfirmImport";
            this.btnConfirmImport.Size = new System.Drawing.Size(210, 38);
            this.btnConfirmImport.TabIndex = 1;
            this.btnConfirmImport.Text = " Xác Nhận Lưu CSDL";
            this.btnConfirmImport.UseVisualStyleBackColor = false;
            this.btnConfirmImport.Click += new System.EventHandler(this.BtnConfirmImport_Click);
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSummary.Location = new System.Drawing.Point(20, 24);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(222, 23);
            this.lblSummary.TabIndex = 0;
            this.lblSummary.Text = "Chưa có file nào được nạp.";
            // 
            // splitMiddle
            // 
            this.splitMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMiddle.Location = new System.Drawing.Point(0, 105);
            this.splitMiddle.Name = "splitMiddle";
            this.splitMiddle.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMiddle.Panel1
            // 
            this.splitMiddle.Panel1.Controls.Add(this.grpPreview);
            this.splitMiddle.Panel1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 5);
            // 
            // splitMiddle.Panel2
            // 
            this.splitMiddle.Panel2.Controls.Add(this.grpErrors);
            this.splitMiddle.Panel2.Padding = new System.Windows.Forms.Padding(15, 5, 15, 10);
            this.splitMiddle.Size = new System.Drawing.Size(1000, 475);
            this.splitMiddle.SplitterDistance = 295;
            this.splitMiddle.TabIndex = 2;
            // 
            // grpPreview
            // 
            this.grpPreview.BackColor = System.Drawing.Color.White;
            this.grpPreview.Controls.Add(this.dgvPreview);
            this.grpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPreview.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpPreview.Location = new System.Drawing.Point(15, 10);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.Size = new System.Drawing.Size(970, 280);
            this.grpPreview.TabIndex = 0;
            this.grpPreview.TabStop = false;
            this.grpPreview.Text = "Dữ Liệu Hợp Lệ (Xem Trước Trước Khi Lưu)";
            // 
            // dgvPreview
            // 
            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.AllowUserToDeleteRows = false;
            this.dgvPreview.BackgroundColor = System.Drawing.Color.White;
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPreview.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvPreview.Location = new System.Drawing.Point(3, 25);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.RowHeadersVisible = false;
            this.dgvPreview.RowHeadersWidth = 51;
            this.dgvPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPreview.Size = new System.Drawing.Size(964, 252);
            this.dgvPreview.TabIndex = 0;
            // 
            // grpErrors
            // 
            this.grpErrors.BackColor = System.Drawing.Color.White;
            this.grpErrors.Controls.Add(this.lstErrors);
            this.grpErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpErrors.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpErrors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.grpErrors.Location = new System.Drawing.Point(15, 5);
            this.grpErrors.Name = "grpErrors";
            this.grpErrors.Size = new System.Drawing.Size(970, 161);
            this.grpErrors.TabIndex = 0;
            this.grpErrors.TabStop = false;
            this.grpErrors.Text = "Chi Tiết Dòng Bị Lỗi (Không Thể Import)";
            // 
            // lstErrors
            // 
            this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstErrors.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstErrors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lstErrors.FormattingEnabled = true;
            this.lstErrors.ItemHeight = 20;
            this.lstErrors.Location = new System.Drawing.Point(3, 25);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.Size = new System.Drawing.Size(964, 133);
            this.lstErrors.TabIndex = 0;
            // 
            // FormImportKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitMiddle);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormImportKhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NHẬP KHÁCH HÀNG TỪ FILE EXCEL";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.splitMiddle.Panel1.ResumeLayout(false);
            this.splitMiddle.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMiddle)).EndInit();
            this.splitMiddle.ResumeLayout(false);
            this.grpPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.grpErrors.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnDownloadTemplate;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Button btnConfirmImport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitMiddle;
        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.GroupBox grpErrors;
        private System.Windows.Forms.ListBox lstErrors;
    }
}
