using System.IO;

namespace QuanLyKhuVuiChoi.Services
{
    public static class TemplateGenerator
    {
        public static void EnsureTemplatesExist(string baseDirectory, IExcelService excelService)
        {
            var templateDir = Path.Combine(baseDirectory, "ExcelTemplates");
            if (!Directory.Exists(templateDir))
            {
                Directory.CreateDirectory(templateDir);
            }

            // 1. KhachHang_Template.xlsx
            var khFile = Path.Combine(templateDir, "KhachHang_Template.xlsx");
            if (!File.Exists(khFile))
            {
                excelService.CreateTemplateFile(
                    khFile,
                    "KhachHang",
                    new[] { "MaKH", "HoTen", "NgaySinh", "GioiTinh", "SoDienThoai", "Email", "DiaChi" },
                    new[] { "KH999", "Nguyễn Văn Mẫu", "1998-05-15", "Nam", "0912345999", "mau.nguyen@example.com", "Hà Nội" }
                );
            }

            // 2. NhanVien_Template.xlsx
            var nvFile = Path.Combine(templateDir, "NhanVien_Template.xlsx");
            if (!File.Exists(nvFile))
            {
                excelService.CreateTemplateFile(
                    nvFile,
                    "NhanVien",
                    new[] { "MaNV", "HoTen", "NgaySinh", "GioiTinh", "SoDienThoai", "Email", "DiaChi", "ChucVu", "TrangThai" },
                    new[] { "NV999", "Trần Văn Mẫu", "1995-10-20", "Nam", "0901234999", "mau.tran@example.com", "Hà Nội", "Thu ngân", "Đang làm việc" }
                );
            }

            // 3. TroChoi_Template.xlsx
            var tcFile = Path.Combine(templateDir, "TroChoi_Template.xlsx");
            if (!File.Exists(tcFile))
            {
                excelService.CreateTemplateFile(
                    tcFile,
                    "TroChoi",
                    new[] { "MaTroChoi", "TenTroChoi", "LoaiTroChoi", "DoTuoiToiThieu", "SucChua", "TrangThai", "MoTa" },
                    new[] { "TC999", "Trò chơi siêu tốc Mẫu", "Cảm giác mạnh", "12", "20", "Đang hoạt động", "Mô tả trò chơi mẫu" }
                );
            }

            // 4. DichVu_Template.xlsx
            var dvFile = Path.Combine(templateDir, "DichVu_Template.xlsx");
            if (!File.Exists(dvFile))
            {
                excelService.CreateTemplateFile(
                    dvFile,
                    "DichVu",
                    new[] { "MaDichVu", "TenDichVu", "LoaiDichVu", "DonGia", "SoLuongTon", "TrangThai", "MoTa" },
                    new[] { "DV999", "Combo ẩm thực Mẫu", "Ẩm thực", "45000", "100", "Đang phục vụ", "Bao gồm bánh và nước" }
                );
            }

            // 5. Ve_Template.xlsx
            var veFile = Path.Combine(templateDir, "Ve_Template.xlsx");
            if (!File.Exists(veFile))
            {
                excelService.CreateTemplateFile(
                    veFile,
                    "Ve",
                    new[] { "MaVe", "TenVe", "LoaiVe", "GiaVe", "ThoiHan", "TrangThai", "MoTa" },
                    new[] { "VE999", "Vé trọn gói mẫu", "Vé ngày", "200000", "Trong ngày", "Đang áp dụng", "Áp dụng cho mọi lứa tuổi" }
                );
            }
        }
    }
}
