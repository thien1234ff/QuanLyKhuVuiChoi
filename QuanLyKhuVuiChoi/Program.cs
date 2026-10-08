using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyKhuVuiChoi.Data;
using QuanLyKhuVuiChoi.Forms;
using QuanLyKhuVuiChoi.Forms.Auth;
using QuanLyKhuVuiChoi.Services;

namespace QuanLyKhuVuiChoi
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var excelService = new ExcelService();

            if (args != null && args.Length > 0 && args[0] == "--generate-templates")
            {
                TemplateGenerator.EnsureTemplatesExist(Directory.GetCurrentDirectory(), excelService);
                Console.WriteLine("All 5 Excel templates generated successfully in ExcelTemplates folder!");
                return;
            }

            if (args != null && args.Length > 0 && args[0] == "--test-db")
            {
                try
                {
                    using var db = new QuanLyKhuVuiChoiDbContext();
                    Console.WriteLine($"Provider: {db.Database.ProviderName}");
                    DbInitializer.Initialize(db);
                    Console.WriteLine($"NhanVien count: {db.NhanViens.Count()}");
                    Console.WriteLine($"KhachHang count: {db.KhachHangs.Count()}");
                    Console.WriteLine($"TroChoi count: {db.TroChois.Count()}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"DB error: {ex}");
                }
                return;
            }

            ApplicationConfiguration.Initialize();

            // Global exception handling to prevent crashes
            Application.ThreadException += (sender, e) =>
            {
                MessageBox.Show($"Đã xảy ra sự cố trong ứng dụng: {e.Exception.Message}", "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                MessageBox.Show($"Lỗi hệ thống: {ex?.Message ?? "Không xác định"}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            // Đảm bảo các file mẫu Excel tồn tại
            try
            {
                TemplateGenerator.EnsureTemplatesExist(AppDomain.CurrentDomain.BaseDirectory, excelService);
            }
            catch
            {
                // Ignore if creating templates in restricted folder
            }

            // Khởi tạo CSDL & Seed Data (Hỗ trợ cả SQL Server và SQLite tự động)
            try
            {
                using (var dbContext = new QuanLyKhuVuiChoiDbContext())
                {
                    DbInitializer.Initialize(dbContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo CSDL: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Mở màn hình đăng nhập
            using (var dbContext = new QuanLyKhuVuiChoiDbContext())
            {
                var loginForm = new FormDangNhap(dbContext);
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new FormMain(dbContext, excelService));
                }
            }
        }
    }
}