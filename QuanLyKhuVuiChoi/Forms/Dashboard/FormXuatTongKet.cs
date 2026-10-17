using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi.Forms.Dashboard
{
    public class FormXuatTongKet : Form
    {
        private readonly ReportService _reportService;

        private ComboBox cboPresets = null!;
        private DateTimePicker dtpFrom = null!;
        private DateTimePicker dtpTo = null!;
        private Label lblRevVal = null!;
        private Label lblTicketVal = null!;
        private Label lblServiceVal = null!;
        private Label lblInvoiceVal = null!;
        private Label lblCustomerVal = null!;
        private Button btnExport = null!;
        private Button btnClose = null!;

        public FormXuatTongKet(ReportService reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            BuildUI();
            InitializePresets();
        }

        private void BuildUI()
        {
            Text = "XUẤT BÁO CÁO TỔNG KẾT THEO THỜI GIAN";
            Size = new Size(580, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(248, 250, 252);
            Font = new Font("Segoe UI", 9F);

            // 1. Header
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };
            pnlHeader.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(226, 232, 240));
                e.Graphics.DrawLine(p, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            var lblTitle = new Label
            {
                Text = "📊 BÁO CÁO TỔNG KẾT HOẠT ĐỘNG",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 138),
                Location = new Point(18, 10),
                AutoSize = true
            };
            var lblSub = new Label
            {
                Text = "Tổng hợp toàn diện doanh thu, vé bán, dịch vụ và khách hàng theo thời gian",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(20, 38),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSub);

            // 2. Body Container
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 15, 20, 15)
            };

            // Group: Chọn thời gian
            var grpTime = new GroupBox
            {
                Text = "KHOẢNG THỜI GIAN TỔNG KẾT",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(20, 10),
                Size = new Size(520, 130),
                BackColor = Color.White
            };

            var lblPreset = new Label
            {
                Text = "Kỳ báo cáo nhanh:",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(16, 32),
                AutoSize = true
            };
            cboPresets = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(140, 28),
                Size = new Size(355, 28)
            };
            cboPresets.Items.AddRange(new object[]
            {
                "Hôm nay",
                "7 ngày gần nhất",
                "Tháng này (Mặc định)",
                "Tháng trước",
                "Quý này",
                "Năm nay",
                "Tùy chọn khoảng ngày"
            });
            cboPresets.SelectedIndexChanged += CboPresets_SelectedIndexChanged;

            var lblFrom = new Label
            {
                Text = "Từ ngày:",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(16, 78),
                AutoSize = true
            };
            dtpFrom = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(140, 74),
                Size = new Size(130, 27)
            };
            dtpFrom.ValueChanged += (s, e) => UpdatePreviewMetrics();

            var lblTo = new Label
            {
                Text = "Đến ngày:",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(290, 78),
                AutoSize = true
            };
            dtpTo = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(365, 74),
                Size = new Size(130, 27)
            };
            dtpTo.ValueChanged += (s, e) => UpdatePreviewMetrics();

            grpTime.Controls.Add(lblPreset);
            grpTime.Controls.Add(cboPresets);
            grpTime.Controls.Add(lblFrom);
            grpTime.Controls.Add(dtpFrom);
            grpTime.Controls.Add(lblTo);
            grpTime.Controls.Add(dtpTo);

            // Group: Xem trước số liệu
            var grpPreview = new GroupBox
            {
                Text = "TỔNG HỢP NHANH TRONG KỲ ĐÃ CHỌN",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(20, 155),
                Size = new Size(520, 210),
                BackColor = Color.White
            };

            // Cards inside preview
            lblRevVal = CreateMetricCard(grpPreview, "💵 TỔNG DOANH THU", "0 đ", Color.FromArgb(16, 185, 129), 16, 30, 488, 50);
            lblTicketVal = CreateMetricCard(grpPreview, "🎫 SỐ VÉ BÁN RA", "0 vé", Color.FromArgb(79, 70, 229), 16, 90, 235, 50);
            lblServiceVal = CreateMetricCard(grpPreview, "🛍️ DỊCH VỤ ĐÃ BÁN", "0 lượt", Color.FromArgb(245, 158, 11), 269, 90, 235, 50);
            lblInvoiceVal = CreateMetricCard(grpPreview, "🧾 SỐ HÓA ĐƠN", "0 đơn", Color.FromArgb(14, 165, 233), 16, 148, 235, 50);
            lblCustomerVal = CreateMetricCard(grpPreview, "👥 KHÁCH HÀNG", "0 khách", Color.FromArgb(168, 85, 247), 269, 148, 235, 50);

            pnlBody.Controls.Add(grpPreview);
            pnlBody.Controls.Add(grpTime);

            // 3. Bottom Action Bar
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.White,
                Padding = new Padding(20, 12, 20, 12)
            };
            pnlBottom.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(226, 232, 240));
                e.Graphics.DrawLine(p, 0, 0, pnlBottom.Width, 0);
            };

            btnExport = new Button
            {
                Text = "📥 Xuất File Excel Tổng Hợp",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Size = new Size(230, 38),
                Location = new Point(180, 13)
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += BtnExport_Click;

            btnClose = new Button
            {
                Text = "Đóng",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                BackColor = Color.FromArgb(241, 245, 249),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Size = new Size(100, 38),
                Location = new Point(420, 13)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Close();

            pnlBottom.Controls.Add(btnExport);
            pnlBottom.Controls.Add(btnClose);

            Controls.Add(pnlBody);
            Controls.Add(pnlBottom);
            Controls.Add(pnlHeader);
        }

        private Label CreateMetricCard(Control parent, string title, string initialValue, Color accentColor, int x, int y, int w, int h)
        {
            var pnl = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = Color.FromArgb(248, 250, 252)
            };
            pnl.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(226, 232, 240));
                e.Graphics.DrawRectangle(p, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            var lblT = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(10, 6),
                AutoSize = true
            };

            var lblV = new Label
            {
                Text = initialValue,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(10, 24),
                AutoSize = true
            };

            pnl.Controls.Add(lblT);
            pnl.Controls.Add(lblV);
            parent.Controls.Add(pnl);

            return lblV;
        }

        private void InitializePresets()
        {
            // Default to "Tháng này"
            cboPresets.SelectedIndex = 2;
        }

        private void CboPresets_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var today = DateTime.Today;

            switch (cboPresets.SelectedIndex)
            {
                case 0: // Hôm nay
                    dtpFrom.Value = today;
                    dtpTo.Value = today;
                    break;
                case 1: // 7 ngày gần nhất
                    dtpFrom.Value = today.AddDays(-6);
                    dtpTo.Value = today;
                    break;
                case 2: // Tháng này
                    dtpFrom.Value = new DateTime(today.Year, today.Month, 1);
                    dtpTo.Value = today;
                    break;
                case 3: // Tháng trước
                    var lastMonth = today.AddMonths(-1);
                    var startOfLastMonth = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                    var endOfLastMonth = startOfLastMonth.AddMonths(1).AddDays(-1);
                    dtpFrom.Value = startOfLastMonth;
                    dtpTo.Value = endOfLastMonth;
                    break;
                case 4: // Quý này
                    int quarter = (today.Month - 1) / 3 + 1;
                    var startOfQuarter = new DateTime(today.Year, (quarter - 1) * 3 + 1, 1);
                    dtpFrom.Value = startOfQuarter;
                    dtpTo.Value = today;
                    break;
                case 5: // Năm nay
                    dtpFrom.Value = new DateTime(today.Year, 1, 1);
                    dtpTo.Value = today;
                    break;
                case 6: // Tùy chọn
                    // Keep current
                    break;
            }

            UpdatePreviewMetrics();
        }

        private void UpdatePreviewMetrics()
        {
            try
            {
                var (revenue, tickets, services, invoices, customers) = _reportService.GetSummaryMetrics(dtpFrom.Value, dtpTo.Value);

                lblRevVal.Text = $"{revenue:N0} VNĐ";
                lblTicketVal.Text = $"{tickets:N0} vé";
                lblServiceVal.Text = $"{services:N0} lượt";
                lblInvoiceVal.Text = $"{invoices:N0} đơn";
                lblCustomerVal.Text = $"{customers:N0} khách";
            }
            catch
            {
                lblRevVal.Text = "0 VNĐ";
                lblTicketVal.Text = "0 vé";
                lblServiceVal.Text = "0 lượt";
                lblInvoiceVal.Text = "0 đơn";
                lblCustomerVal.Text = "0 khách";
            }
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            if (dtpFrom.Value.Date > dtpTo.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var defaultFileName = $"BaoCaoTongKet_{dtpFrom.Value:yyyyMMdd}_{dtpTo.Value:yyyyMMdd}.xlsx";

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = defaultFileName,
                Title = "Lưu Báo Cáo Tổng Kết Excel"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnExport.Enabled = false;

                _reportService.ExportComprehensiveSummaryToExcel(dtpFrom.Value, dtpTo.Value, sfd.FileName);

                Cursor = Cursors.Default;
                btnExport.Enabled = true;

                var result = MessageBox.Show(
                    $"Xuất báo cáo tổng kết thành công!\n\nFile lưu tại: {sfd.FileName}\n\nBạn có muốn mở file Excel vừa tạo ngay không?",
                    "Xuất File Thành Công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể mở file tự động: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                btnExport.Enabled = true;
                MessageBox.Show($"Lỗi khi xuất file Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
