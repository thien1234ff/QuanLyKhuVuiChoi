using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Data
{
    public class QuanLyKhuVuiChoiDbContext : DbContext
    {
        public static bool UseSqliteFallback { get; set; } = false;
        public static string SqliteDbPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuanLyKhuVuiChoi.db");
        private static bool? _isSqlServerAvailable = null;

        public static bool IsSqlServerAvailable()
        {
            if (_isSqlServerAvailable.HasValue)
            {
                return _isSqlServerAvailable.Value;
            }

            try
            {
                string connStr = GetConnectionString();
                var builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connStr)
                {
                    ConnectTimeout = 3
                };

                using var conn = new Microsoft.Data.SqlClient.SqlConnection(builder.ConnectionString);
                conn.Open();
                _isSqlServerAvailable = true;
                return true;
            }
            catch
            {
                _isSqlServerAvailable = false;
                return false;
            }
        }

        public DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public DbSet<NhanVien> NhanViens { get; set; } = null!;
        public DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public DbSet<TroChoi> TroChois { get; set; } = null!;
        public DbSet<DichVu> DichVus { get; set; } = null!;
        public DbSet<Ve> Ves { get; set; } = null!;
        public DbSet<HoaDon> HoaDons { get; set; } = null!;
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; } = null!;

        public QuanLyKhuVuiChoiDbContext()
        {
        }

        public QuanLyKhuVuiChoiDbContext(DbContextOptions<QuanLyKhuVuiChoiDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (UseSqliteFallback || !IsSqlServerAvailable())
                {
                    UseSqliteFallback = true;
                    optionsBuilder.UseSqlite($"Data Source={SqliteDbPath}");
                }
                else
                {
                    string connectionString = GetConnectionString();
                    optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                    {
                        sqlOptions.CommandTimeout(5);
                    });
                }
            }
        }

        public static string GetConnectionString()
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var builder = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                var configuration = builder.Build();
                var connStr = configuration.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrWhiteSpace(connStr))
                {
                    return connStr;
                }
            }
            catch
            {
                // Fallback to default local connection string if reading config fails
            }

            return "Server=localhost;Database=QuanLyKhuVuiChoi;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Indexes
            modelBuilder.Entity<KhachHang>()
                .HasIndex(k => k.SoDienThoai)
                .IsUnique();

            modelBuilder.Entity<KhachHang>()
                .HasIndex(k => k.HoTen);

            modelBuilder.Entity<NhanVien>()
                .HasIndex(n => n.HoTen);

            modelBuilder.Entity<TroChoi>()
                .HasIndex(t => t.TenTroChoi);

            modelBuilder.Entity<DichVu>()
                .HasIndex(d => d.TenDichVu);

            modelBuilder.Entity<Ve>()
                .HasIndex(v => v.TenVe);

            modelBuilder.Entity<HoaDon>()
                .HasIndex(h => h.NgayLap);

            // Relationships
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.KhachHang)
                .WithMany(k => k.HoaDons)
                .HasForeignKey(h => h.MaKH)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.NhanVien)
                .WithMany(n => n.HoaDons)
                .HasForeignKey(h => h.MaNV)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ChiTietHoaDon>()
                .HasOne(c => c.HoaDon)
                .WithMany(h => h.ChiTietHoaDons)
                .HasForeignKey(c => c.MaHoaDon)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.NhanVien)
                .WithOne(n => n.TaiKhoan)
                .HasForeignKey<TaiKhoan>(t => t.MaNV)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
