using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyKhuVuiChoi.Models;

namespace QuanLyKhuVuiChoi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(QuanLyKhuVuiChoiDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if already seeded
            if (context.NhanViens.Any())
            {
                return; // DB already has data
            }

            // 1. Seed NhanVien
            var nhanViens = new List<NhanVien>
            {
                new NhanVien { MaNV = "NV001", HoTen = "Trương Gia Bình", NgaySinh = new DateTime(1982, 5, 19), GioiTinh = "Nam", SoDienThoai = "0901234001", Email = "binh.truong@amusement.vn", DiaChi = "Quận Ba Đình, Hà Nội", ChucVu = "Quản lý", NgayVaoLam = new DateTime(2024, 1, 1), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV002", HoTen = "Lê Thị Hương", NgaySinh = new DateTime(1990, 9, 12), GioiTinh = "Nữ", SoDienThoai = "0901234002", Email = "huong.le@amusement.vn", DiaChi = "Quận Cầu Giấy, Hà Nội", ChucVu = "Thu ngân", NgayVaoLam = new DateTime(2024, 3, 15), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV003", HoTen = "Nguyễn Quang Hải", NgaySinh = new DateTime(1996, 4, 12), GioiTinh = "Nam", SoDienThoai = "0901234003", Email = "hai.nguyen@amusement.vn", DiaChi = "Quận Đống Đa, Hà Nội", ChucVu = "Thu ngân", NgayVaoLam = new DateTime(2024, 6, 1), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV004", HoTen = "Phạm Hải Yến", NgaySinh = new DateTime(1998, 11, 9), GioiTinh = "Nữ", SoDienThoai = "0901234004", Email = "yen.pham@amusement.vn", DiaChi = "Quận Thanh Xuân, Hà Nội", ChucVu = "Soát vé", NgayVaoLam = new DateTime(2024, 7, 10), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV005", HoTen = "Trần Minh Vương", NgaySinh = new DateTime(1995, 3, 28), GioiTinh = "Nam", SoDienThoai = "0901234005", Email = "vuong.tran@amusement.vn", DiaChi = "Quận Nam Từ Liêm, Hà Nội", ChucVu = "Kỹ thuật", NgayVaoLam = new DateTime(2024, 2, 20), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV006", HoTen = "Đỗ Hùng Dũng", NgaySinh = new DateTime(1993, 9, 8), GioiTinh = "Nam", SoDienThoai = "0901234006", Email = "dung.do@amusement.vn", DiaChi = "Quận Hoàng Mai, Hà Nội", ChucVu = "Kỹ thuật", NgayVaoLam = new DateTime(2024, 4, 5), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV007", HoTen = "Vũ Văn Thanh", NgaySinh = new DateTime(1996, 4, 14), GioiTinh = "Nam", SoDienThoai = "0901234007", Email = "thanh.vu@amusement.vn", DiaChi = "Quận Hà Đông, Hà Nội", ChucVu = "Bảo vệ", NgayVaoLam = new DateTime(2024, 5, 12), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV008", HoTen = "Nguyễn Thị Tuyết Dung", NgaySinh = new DateTime(1993, 12, 13), GioiTinh = "Nữ", SoDienThoai = "0901234008", Email = "dung.tuyet@amusement.vn", DiaChi = "Quận Cầu Giấy, Hà Nội", ChucVu = "Chăm sóc khách hàng", NgayVaoLam = new DateTime(2024, 8, 1), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV009", HoTen = "Bùi Tiến Dũng", NgaySinh = new DateTime(1997, 2, 28), GioiTinh = "Nam", SoDienThoai = "0901234009", Email = "dung.bui@amusement.vn", DiaChi = "Quận Tây Hồ, Hà Nội", ChucVu = "Soát vé", NgayVaoLam = new DateTime(2024, 9, 1), TrangThai = "Đang làm việc" },
                new NhanVien { MaNV = "NV010", HoTen = "Hoàng Đức Tùng", NgaySinh = new DateTime(1999, 7, 21), GioiTinh = "Nam", SoDienThoai = "0901234010", Email = "tung.hoang@amusement.vn", DiaChi = "Quận Long Biên, Hà Nội", ChucVu = "Thu ngân", NgayVaoLam = new DateTime(2024, 10, 15), TrangThai = "Đang làm việc" }
            };
            context.NhanViens.AddRange(nhanViens);
            context.SaveChanges();

            // 2. Seed TaiKhoan
            var taiKhoans = new List<TaiKhoan>
            {
                new TaiKhoan { TenDangNhap = "admin", MatKhau = "123456", MaNV = "NV001", Quyen = "Admin", TrangThai = true },
                new TaiKhoan { TenDangNhap = "thungan1", MatKhau = "123456", MaNV = "NV002", Quyen = "NhanVien", TrangThai = true },
                new TaiKhoan { TenDangNhap = "thungan2", MatKhau = "123456", MaNV = "NV003", Quyen = "NhanVien", TrangThai = true },
                new TaiKhoan { TenDangNhap = "soatve", MatKhau = "123456", MaNV = "NV004", Quyen = "NhanVien", TrangThai = true }
            };
            context.TaiKhoans.AddRange(taiKhoans);

            // 3. Seed KhachHang (20)
            var khachHangs = new List<KhachHang>
            {
                new KhachHang { MaKH = "KH001", HoTen = "Nguyễn Văn An", NgaySinh = new DateTime(1995, 4, 12), GioiTinh = "Nam", SoDienThoai = "0912345678", Email = "an.nguyen@gmail.com", DiaChi = "123 Cầu Giấy, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-60) },
                new KhachHang { MaKH = "KH002", HoTen = "Trần Thị Mai", NgaySinh = new DateTime(1998, 8, 23), GioiTinh = "Nữ", SoDienThoai = "0987654321", Email = "mai.tran@gmail.com", DiaChi = "45 Hai Bà Trưng, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-55) },
                new KhachHang { MaKH = "KH003", HoTen = "Lê Hoàng Long", NgaySinh = new DateTime(1992, 11, 5), GioiTinh = "Nam", SoDienThoai = "0903123456", Email = "long.le@yahoo.com", DiaChi = "78 Nguyễn Trãi, Thanh Xuân, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-50) },
                new KhachHang { MaKH = "KH004", HoTen = "Phạm Thu Trang", NgaySinh = new DateTime(2000, 2, 14), GioiTinh = "Nữ", SoDienThoai = "0934567890", Email = "trang.pham@gmail.com", DiaChi = "12 Kim Mã, Ba Đình, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-45) },
                new KhachHang { MaKH = "KH005", HoTen = "Vũ Đình Hưng", NgaySinh = new DateTime(1988, 6, 30), GioiTinh = "Nam", SoDienThoai = "0978112233", Email = "hung.vu@gmail.com", DiaChi = "56 Đội Cấn, Ba Đình, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-40) },
                new KhachHang { MaKH = "KH006", HoTen = "Đỗ Bích Phương", NgaySinh = new DateTime(1997, 9, 18), GioiTinh = "Nữ", SoDienThoai = "0965223344", Email = "phuong.do@outlook.com", DiaChi = "89 Xã Đàn, Đống Đa, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-35) },
                new KhachHang { MaKH = "KH007", HoTen = "Hoàng Tuấn Kiệt", NgaySinh = new DateTime(2001, 12, 1), GioiTinh = "Nam", SoDienThoai = "0943334455", Email = "kiet.hoang@gmail.com", DiaChi = "34 Chùa Bộc, Đống Đa, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-30) },
                new KhachHang { MaKH = "KH008", HoTen = "Ngô Thanh Vân", NgaySinh = new DateTime(1994, 3, 25), GioiTinh = "Nữ", SoDienThoai = "0919445566", Email = "van.ngo@gmail.com", DiaChi = "67 Láng Hạ, Đống Đa, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-28) },
                new KhachHang { MaKH = "KH009", HoTen = "Bùi Minh Trí", NgaySinh = new DateTime(1990, 7, 19), GioiTinh = "Nam", SoDienThoai = "0982556677", Email = "tri.bui@gmail.com", DiaChi = "90 Hoàng Hoa Thám, Ba Đình, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-25) },
                new KhachHang { MaKH = "KH010", HoTen = "Đinh Thùy Linh", NgaySinh = new DateTime(2003, 5, 9), GioiTinh = "Nữ", SoDienThoai = "0936667788", Email = "linh.dinh@gmail.com", DiaChi = "11 Thái Hà, Đống Đa, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-20) },
                new KhachHang { MaKH = "KH011", HoTen = "Lý Quang Diệu", NgaySinh = new DateTime(1985, 10, 15), GioiTinh = "Nam", SoDienThoai = "0974778899", Email = "dieu.ly@gmail.com", DiaChi = "22 Tây Sơn, Đống Đa, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-18) },
                new KhachHang { MaKH = "KH012", HoTen = "Dương Cẩm Lynh", NgaySinh = new DateTime(1996, 1, 28), GioiTinh = "Nữ", SoDienThoai = "0908889900", Email = "lynh.duong@gmail.com", DiaChi = "44 Giải Phóng, Hai Bà Trưng, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-15) },
                new KhachHang { MaKH = "KH013", HoTen = "Hà Anh Tuấn", NgaySinh = new DateTime(1993, 12, 12), GioiTinh = "Nam", SoDienThoai = "0917990011", Email = "tuan.ha@gmail.com", DiaChi = "55 Phố Huế, Hoàn Kiếm, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-12) },
                new KhachHang { MaKH = "KH014", HoTen = "Phan Như Quỳnh", NgaySinh = new DateTime(1999, 4, 4), GioiTinh = "Nữ", SoDienThoai = "0981001122", Email = "quynh.phan@gmail.com", DiaChi = "88 Trần Duy Hưng, Cầu Giấy, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-10) },
                new KhachHang { MaKH = "KH015", HoTen = "Trịnh Thăng Bình", NgaySinh = new DateTime(1991, 8, 8), GioiTinh = "Nam", SoDienThoai = "0935112233", Email = "binh.trinh@gmail.com", DiaChi = "15 Lê Văn Lương, Thanh Xuân, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-8) },
                new KhachHang { MaKH = "KH016", HoTen = "Tạ Mỹ Linh", NgaySinh = new DateTime(2002, 10, 20), GioiTinh = "Nữ", SoDienThoai = "0962223344", Email = "linh.ta@gmail.com", DiaChi = "39 Xuân Thủy, Cầu Giấy, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-6) },
                new KhachHang { MaKH = "KH017", HoTen = "Võ Tấn Phát", NgaySinh = new DateTime(1995, 5, 30), GioiTinh = "Nam", SoDienThoai = "0949334455", Email = "phat.vo@gmail.com", DiaChi = "73 Hoàng Đạo Thúy, Cầu Giấy, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-5) },
                new KhachHang { MaKH = "KH018", HoTen = "Cao Thái Hà", NgaySinh = new DateTime(1998, 7, 7), GioiTinh = "Nữ", SoDienThoai = "0916445566", Email = "ha.cao@gmail.com", DiaChi = "101 Nguyễn Thị Định, Cầu Giấy, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-4) },
                new KhachHang { MaKH = "KH019", HoTen = "Đoàn Văn Hậu", NgaySinh = new DateTime(2000, 9, 14), GioiTinh = "Nam", SoDienThoai = "0985556677", Email = "hau.doan@gmail.com", DiaChi = "28 Nguyễn Chí Thanh, Ba Đình, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-3) },
                new KhachHang { MaKH = "KH020", HoTen = "Mai Phương Thúy", NgaySinh = new DateTime(1994, 11, 22), GioiTinh = "Nữ", SoDienThoai = "0938667788", Email = "thuy.mai@gmail.com", DiaChi = "63 Liễu Giai, Ba Đình, Hà Nội", NgayDangKy = DateTime.Now.AddDays(-1) }
            };
            context.KhachHangs.AddRange(khachHangs);

            // 4. Seed TroChoi (15)
            var troChois = new List<TroChoi>
            {
                new TroChoi { MaTroChoi = "TC001", TenTroChoi = "Tàu lượn siêu tốc Cyclone", LoaiTroChoi = "Cảm giác mạnh", GiaVe = 80000, DoTuoiToiThieu = 12, SucChua = 24, TrangThai = "Đang hoạt động", MoTa = "Tốc độ cực đại 90km/h với 3 vòng xoắn ốc liên hoàn" },
                new TroChoi { MaTroChoi = "TC002", TenTroChoi = "Đu quay khổng lồ Wonder Wheel", LoaiTroChoi = "Gia đình", GiaVe = 50000, DoTuoiToiThieu = 3, SucChua = 60, TrangThai = "Đang hoạt động", MoTa = "Vòng quay ngắm toàn cảnh thành phố ở độ cao 50m" },
                new TroChoi { MaTroChoi = "TC003", TenTroChoi = "Xe điện đụng Bumper Cars", LoaiTroChoi = "Gia đình", GiaVe = 40000, DoTuoiToiThieu = 6, SucChua = 20, TrangThai = "Đang hoạt động", MoTa = "Sân xe điện tử hiện đại sàn cảm ứng an toàn" },
                new TroChoi { MaTroChoi = "TC004", TenTroChoi = "Tháp rơi tự do Free Fall", LoaiTroChoi = "Cảm giác mạnh", GiaVe = 70000, DoTuoiToiThieu = 14, SucChua = 16, TrangThai = "Đang hoạt động", MoTa = "Tháp thả rơi từ độ cao 40m trong 3 giây" },
                new TroChoi { MaTroChoi = "TC005", TenTroChoi = "Ngôi nhà ma bí ẩn Phantom Manor", LoaiTroChoi = "Phiêu lưu", GiaVe = 60000, DoTuoiToiThieu = 10, SucChua = 15, TrangThai = "Đang hoạt động", MoTa = "Mê cung âm thanh ánh sáng rùng rợn sống động" },
                new TroChoi { MaTroChoi = "TC006", TenTroChoi = "Đường trượt nước Kamikaze", LoaiTroChoi = "Dưới nước", GiaVe = 75000, DoTuoiToiThieu = 10, SucChua = 10, TrangThai = "Đang hoạt động", MoTa = "Máng trượt dốc 45 độ cực phấn khích" },
                new TroChoi { MaTroChoi = "TC007", TenTroChoi = "Hồ tạo sóng Wave Paradise", LoaiTroChoi = "Dưới nước", GiaVe = 60000, DoTuoiToiThieu = 5, SucChua = 200, TrangThai = "Đang hoạt động", MoTa = "Bể tạo sóng nhân tạo 6 cấp độ như bãi biển thật" },
                new TroChoi { MaTroChoi = "TC008", TenTroChoi = "Vòng quay ngựa gỗ Carousel", LoaiTroChoi = "Trẻ em", GiaVe = 30000, DoTuoiToiThieu = 2, SucChua = 32, TrangThai = "Đang hoạt động", MoTa = "Ngựa gỗ cổ tích phong cách hoàng gia Pháp" },
                new TroChoi { MaTroChoi = "TC009", TenTroChoi = "Khu liên hoàn Kidzone", LoaiTroChoi = "Trẻ em", GiaVe = 50000, DoTuoiToiThieu = 2, SucChua = 80, TrangThai = "Đang hoạt động", MoTa = "Nhà bóng, cầu trượt và vận động mềm cho bé" },
                new TroChoi { MaTroChoi = "TC010", TenTroChoi = "Thám hiểm rừng Amazon thuyền vượt thác", LoaiTroChoi = "Phiêu lưu", GiaVe = 65000, DoTuoiToiThieu = 8, SucChua = 18, TrangThai = "Đang hoạt động", MoTa = "Du ngoạn thác nước hoang dã với hiệu ứng sương mù" },
                new TroChoi { MaTroChoi = "TC011", TenTroChoi = "Rạp chiếu phim 9D Cinema", LoaiTroChoi = "Công nghệ", GiaVe = 55000, DoTuoiToiThieu = 6, SucChua = 25, TrangThai = "Đang hoạt động", MoTa = "Ghế chuyển động, mưa phùn, gió lốc sống động" },
                new TroChoi { MaTroChoi = "TC012", TenTroChoi = "Đua xe F1 mini Karting", LoaiTroChoi = "Cảm giác mạnh", GiaVe = 90000, DoTuoiToiThieu = 12, SucChua = 12, TrangThai = "Đang hoạt động", MoTa = "Đường đua chuyên nghiệp xe kart động cơ điện" },
                new TroChoi { MaTroChoi = "TC013", TenTroChoi = "Bắn súng laser Laser Tag", LoaiTroChoi = "Hành động", GiaVe = 50000, DoTuoiToiThieu = 8, SucChua = 20, TrangThai = "Bảo trì", MoTa = "Đang nâng cấp cảm biến hồng ngoại thế hệ mới" },
                new TroChoi { MaTroChoi = "TC014", TenTroChoi = "Đĩa bay xoay cuồng Disko Coaster", LoaiTroChoi = "Cảm giác mạnh", GiaVe = 70000, DoTuoiToiThieu = 12, SucChua = 24, TrangThai = "Đang hoạt động", MoTa = "Đĩa bay trượt trên ray hình chữ U xoay 360 độ" },
                new TroChoi { MaTroChoi = "TC015", TenTroChoi = "Leo núi nhân tạo Rock Climbing", LoaiTroChoi = "Thể thao", GiaVe = 45000, DoTuoiToiThieu = 8, SucChua = 10, TrangThai = "Đang hoạt động", MoTa = "Vách núi cao 15m với 4 cấp độ dây đai an toàn" }
            };
            context.TroChois.AddRange(troChois);

            // 5. Seed DichVu (15)
            var dichVus = new List<DichVu>
            {
                new DichVu { MaDichVu = "DV001", TenDichVu = "Combo Burger Bò + Khoai tây", LoaiDichVu = "Ẩm thực", DonGia = 65000, SoLuongTon = 150, TrangThai = "Đang phục vụ", MoTa = "Bánh burger bò Úc nướng kèm khoai chiên giòn" },
                new DichVu { MaDichVu = "DV002", TenDichVu = "Gà rán giòn cay phần 2 miếng", LoaiDichVu = "Ẩm thực", DonGia = 55000, SoLuongTon = 120, TrangThai = "Đang phục vụ", MoTa = "Gà rán sốt cay giòn rụm công thức đặc biệt" },
                new DichVu { MaDichVu = "DV003", TenDichVu = "Pizza xúc xích cỡ vừa", LoaiDichVu = "Ẩm thực", DonGia = 110000, SoLuongTon = 80, TrangThai = "Đang phục vụ", MoTa = "Pizza nướng phô mai Mozzarella béo ngậy" },
                new DichVu { MaDichVu = "DV004", TenDichVu = "Mì Ý sốt bò băm Spaghetti", LoaiDichVu = "Ẩm thực", DonGia = 60000, SoLuongTon = 95, TrangThai = "Đang phục vụ", MoTa = "Mì Ý xốt cà chua bò băm phô mai thơm ngon" },
                new DichVu { MaDichVu = "DV005", TenDichVu = "Nước suối Aquafina 500ml", LoaiDichVu = "Nước uống", DonGia = 15000, SoLuongTon = 500, TrangThai = "Đang phục vụ", MoTa = "Nước khoáng tinh khiết đóng chai ướp lạnh" },
                new DichVu { MaDichVu = "DV006", TenDichVu = "Nước ngọt lon Coca-Cola 320ml", LoaiDichVu = "Nước uống", DonGia = 20000, SoLuongTon = 400, TrangThai = "Đang phục vụ", MoTa = "Nước giải khát có ga Coca-Cola mát lạnh" },
                new DichVu { MaDichVu = "DV007", TenDichVu = "Trà sữa trân châu đường đen", LoaiDichVu = "Nước uống", DonGia = 35000, SoLuongTon = 200, TrangThai = "Đang phục vụ", MoTa = "Trà sữa Đài Loan trân châu đường đen dẻo dai" },
                new DichVu { MaDichVu = "DV008", TenDichVu = "Nước ép dưa hấu tươi", LoaiDichVu = "Nước uống", DonGia = 30000, SoLuongTon = 150, TrangThai = "Đang phục vụ", MoTa = "Nước ép trái cây tươi nguyên chất giải nhiệt" },
                new DichVu { MaDichVu = "DV009", TenDichVu = "Thuê tủ gửi đồ thông minh (Cả ngày)", LoaiDichVu = "Thuê đồ", DonGia = 30000, SoLuongTon = 200, TrangThai = "Đang phục vụ", MoTa = "Tủ locker khóa mã số điện tử an toàn tuyệt đối" },
                new DichVu { MaDichVu = "DV010", TenDichVu = "Thuê áo phao bơi an toàn", LoaiDichVu = "Thuê đồ", DonGia = 20000, SoLuongTon = 180, TrangThai = "Đang phục vụ", MoTa = "Áo phao tiêu chuẩn mọi lứa tuổi tại khu công viên nước" },
                new DichVu { MaDichVu = "DV011", TenDichVu = "Thuê xe đẩy trẻ em cao cấp", LoaiDichVu = "Thuê đồ", DonGia = 50000, SoLuongTon = 40, TrangThai = "Đang phục vụ", MoTa = "Xe đẩy gấp gọn êm ái cho bé dưới 4 tuổi" },
                new DichVu { MaDichVu = "DV012", TenDichVu = "Thuê chòi nghỉ mát gia đình VIP", LoaiDichVu = "Khu nghỉ ngơi", DonGia = 200000, SoLuongTon = 15, TrangThai = "Đang phục vụ", MoTa = "Chòi ven hồ có quạt mát, bàn ghế và sạc điện thoại" },
                new DichVu { MaDichVu = "DV013", TenDichVu = "Bắp rang bơ phô mai thơm lừng", LoaiDichVu = "Ăn vặt", DonGia = 30000, SoLuongTon = 300, TrangThai = "Đang phục vụ", MoTa = "Hộp bắp rang bơ vàng óng vị caramel phô mai" },
                new DichVu { MaDichVu = "DV014", TenDichVu = "Kem tươi ốc quế 2 màu", LoaiDichVu = "Ăn vặt", DonGia = 20000, SoLuongTon = 250, TrangThai = "Đang phục vụ", MoTa = "Kem vani socola mát lạnh vỏ ốc quế giòn tan" },
                new DichVu { MaDichVu = "DV015", TenDichVu = "Chụp ảnh kỷ niệm lấy ngay", LoaiDichVu = "Dịch vụ khác", DonGia = 40000, SoLuongTon = 999, TrangThai = "Đang phục vụ", MoTa = "In ảnh lưu niệm lấy ngay kích thước 13x18 có khung" }
            };
            context.DichVus.AddRange(dichVus);

            // 6. Seed Ve (10)
            var ves = new List<Ve>
            {
                new Ve { MaVe = "VE001", TenVe = "Vé vào cổng tiêu chuẩn", LoaiVe = "Vé ngày", GiaVe = 100000, ThoiHan = "Trong ngày", MoTa = "Vào cổng tham quan và chơi các trò chơi miễn phí cơ bản", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE002", TenVe = "Vé trọn gói người lớn (All-in-one)", LoaiVe = "Vé người lớn", GiaVe = 250000, ThoiHan = "Trong ngày", MoTa = "Chơi không giới hạn tất cả các trò chơi khô và nước", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE003", TenVe = "Vé trọn gói trẻ em (Cao < 1m3)", LoaiVe = "Vé trẻ em", GiaVe = 150000, ThoiHan = "Trong ngày", MoTa = "Áp dụng cho trẻ em, miễn phí toàn bộ khu Kidzone và nước", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE004", TenVe = "Vé công viên nước Paradise", LoaiVe = "Vé ngày", GiaVe = 120000, ThoiHan = "Trong ngày", MoTa = "Vé tham gia toàn bộ cụm trò chơi công viên nước", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE005", TenVe = "Vé combo Gia đình (2 Lớn + 1 Bé)", LoaiVe = "Vé gia đình", GiaVe = 550000, ThoiHan = "Trong ngày", MoTa = "Gói tiết kiệm cho gia đình 3 người trọn gói tất cả trò chơi", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE006", TenVe = "Vé combo Gia đình VIP (2 Lớn + 2 Bé)", LoaiVe = "Vé gia đình", GiaVe = 700000, ThoiHan = "Trong ngày", MoTa = "Trọn gói trò chơi + tặng 4 voucher nước uống 20k", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE007", TenVe = "Vé VIP FastPass ưu tiên", LoaiVe = "Vé VIP", GiaVe = 450000, ThoiHan = "Trong ngày", MoTa = "Trọn gói + lối đi ưu tiên không cần xếp hàng chờ đợi", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE008", TenVe = "Vé dạ hội vui chơi sau 17h00", LoaiVe = "Vé ngày", GiaVe = 120000, ThoiHan = "Buổi tối", MoTa = "Áp dụng sau 17h00 thưởng thức biểu diễn ánh sáng và trò chơi đêm", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE009", TenVe = "Vé học sinh - sinh viên ưu đãi", LoaiVe = "Vé người lớn", GiaVe = 180000, ThoiHan = "Trong ngày", MoTa = "Yêu cầu xuất trình thẻ học sinh hoặc sinh viên", TrangThai = "Đang áp dụng" },
                new Ve { MaVe = "VE010", TenVe = "Vé thẻ tháng Member Platinum", LoaiVe = "Vé VIP", GiaVe = 1200000, ThoiHan = "30 ngày", MoTa = "Vào cổng và chơi không giới hạn tất cả các ngày trong tháng", TrangThai = "Đang áp dụng" }
            };
            context.Ves.AddRange(ves);
            context.SaveChanges();

            // 7. Seed HoaDon & ChiTietHoaDon (30)
            var hoaDons = new List<HoaDon>
            {
                new HoaDon { MaHoaDon = "HD001", MaKH = "KH001", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-28), TongTien = 500000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Khách mua 2 vé trọn gói người lớn" },
                new HoaDon { MaHoaDon = "HD002", MaKH = "KH002", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-27), TongTien = 600000, GiamGia = 5, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "Combo gia đình kèm ăn nhẹ" },
                new HoaDon { MaHoaDon = "HD003", MaKH = "KH003", MaNV = "NV003", NgayLap = DateTime.Now.AddDays(-26), TongTien = 900000, GiamGia = 0, PhuongThucThanhToan = "Thẻ", TrangThai = "Đã thanh toán", GhiChu = "2 vé VIP FastPass" },
                new HoaDon { MaHoaDon = "HD004", MaKH = "KH004", MaNV = "NV003", NgayLap = DateTime.Now.AddDays(-25), TongTien = 360000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "2 vé sinh viên" },
                new HoaDon { MaHoaDon = "HD005", MaKH = "KH005", MaNV = "NV010", NgayLap = DateTime.Now.AddDays(-24), TongTien = 700000, GiamGia = 0, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "Vé combo gia đình VIP" },
                new HoaDon { MaHoaDon = "HD006", MaKH = "KH006", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-23), TongTien = 170000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Dịch vụ đồ ăn và nước uống" },
                new HoaDon { MaHoaDon = "HD007", MaKH = "KH007", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-22), TongTien = 390000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Vé vào cổng và thuê tủ" },
                new HoaDon { MaHoaDon = "HD008", MaKH = "KH008", MaNV = "NV003", NgayLap = DateTime.Now.AddDays(-20), TongTien = 240000, GiamGia = 0, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "Vé dạ hội buổi tối" },
                new HoaDon { MaHoaDon = "HD009", MaKH = "KH009", MaNV = "NV010", NgayLap = DateTime.Now.AddDays(-19), TongTien = 650000, GiamGia = 0, PhuongThucThanhToan = "Thẻ", TrangThai = "Đã thanh toán", GhiChu = "Vé trọn gói người lớn và trẻ em" },
                new HoaDon { MaHoaDon = "HD010", MaKH = "KH010", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-18), TongTien = 240000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Vé công viên nước" },
                new HoaDon { MaHoaDon = "HD011", MaKH = "KH011", MaNV = "NV003", NgayLap = DateTime.Now.AddDays(-17), TongTien = 1080000, GiamGia = 10, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "Vé tháng Member Platinum" },
                new HoaDon { MaHoaDon = "HD012", MaKH = "KH012", MaNV = "NV010", NgayLap = DateTime.Now.AddDays(-16), TongTien = 310000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Thuê chòi và nước uống" },
                new HoaDon { MaHoaDon = "HD013", MaKH = "KH013", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-15), TongTien = 500000, GiamGia = 0, PhuongThucThanhToan = "Thẻ", TrangThai = "Đã thanh toán", GhiChu = "2 vé người lớn" },
                new HoaDon { MaHoaDon = "HD014", MaKH = "KH014", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-14), TongTien = 340000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Vé trẻ em và chụp ảnh" },
                new HoaDon { MaHoaDon = "HD015", MaKH = "KH015", MaNV = "NV003", NgayLap = DateTime.Now.AddDays(-13), TongTien = 80000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Vé chơi thêm Cyclone" },
                new HoaDon { MaHoaDon = "HD016", MaKH = "KH016", MaNV = "NV010", NgayLap = DateTime.Now.AddDays(-12), TongTien = 700000, GiamGia = 0, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "Gia đình đi chơi ngày cuối tuần" },
                new HoaDon { MaHoaDon = "HD017", MaKH = "KH017", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-10), TongTien = 180000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "2 vé đua xe F1" },
                new HoaDon { MaHoaDon = "HD018", MaKH = "KH018", MaNV = "NV003", NgayLap = DateTime.Now.AddDays(-9), TongTien = 450000, GiamGia = 0, PhuongThucThanhToan = "Thẻ", TrangThai = "Đã thanh toán", GhiChu = "1 vé VIP FastPass" },
                new HoaDon { MaHoaDon = "HD019", MaKH = "KH019", MaNV = "NV010", NgayLap = DateTime.Now.AddDays(-8), TongTien = 513000, GiamGia = 5, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "3 vé sinh viên giảm 5%" },
                new HoaDon { MaHoaDon = "HD020", MaKH = "KH020", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-7), TongTien = 550000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Vé combo gia đình" },
                new HoaDon { MaHoaDon = "HD021", MaKH = "KH001", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-6), TongTien = 250000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Vé trọn gói người lớn" },
                new HoaDon { MaHoaDon = "HD022", MaKH = "KH002", MaNV = "NV003", NgayLap = DateTime.Now.AddDays(-5), TongTien = 165000, GiamGia = 0, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "Gà rán và nước uống" },
                new HoaDon { MaHoaDon = "HD023", MaKH = "KH003", MaNV = "NV010", NgayLap = DateTime.Now.AddDays(-4), TongTien = 300000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "3 vé vào cổng tiêu chuẩn" },
                new HoaDon { MaHoaDon = "HD024", MaKH = "KH004", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-3), TongTien = 360000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "3 vé công viên nước" },
                new HoaDon { MaHoaDon = "HD025", MaKH = "KH005", MaNV = "NV003", NgayLap = DateTime.Now.AddDays(-2), TongTien = 700000, GiamGia = 0, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "Mua combo gia đình" },
                new HoaDon { MaHoaDon = "HD026", MaKH = "KH006", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-2), TongTien = 500000, GiamGia = 0, PhuongThucThanhToan = "Thẻ", TrangThai = "Đã thanh toán", GhiChu = "2 vé trọn gói người lớn" },
                new HoaDon { MaHoaDon = "HD027", MaKH = "KH007", MaNV = "NV010", NgayLap = DateTime.Now.AddDays(-1), TongTien = 360000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "2 vé sinh viên" },
                new HoaDon { MaHoaDon = "HD028", MaKH = "KH008", MaNV = "NV002", NgayLap = DateTime.Now.AddDays(-1), TongTien = 900000, GiamGia = 0, PhuongThucThanhToan = "Chuyển khoản", TrangThai = "Đã thanh toán", GhiChu = "2 vé VIP FastPass" },
                new HoaDon { MaHoaDon = "HD029", MaKH = "KH009", MaNV = "NV003", NgayLap = DateTime.Now, TongTien = 140000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Tháp rơi tự do 2 lượt" },
                new HoaDon { MaHoaDon = "HD030", MaKH = "KH010", MaNV = "NV002", NgayLap = DateTime.Now, TongTien = 550000, GiamGia = 0, PhuongThucThanhToan = "Tiền mặt", TrangThai = "Đã thanh toán", GhiChu = "Hóa đơn sáng nay vé gia đình" }
            };
            context.HoaDons.AddRange(hoaDons);

            // Seed ChiTietHoaDon for these 30 invoices
            var chiTiets = new List<ChiTietHoaDon>
            {
                new ChiTietHoaDon { MaHoaDon = "HD001", LoaiHang = "Ve", MaHang = "VE002", TenHang = "Vé trọn gói người lớn (All-in-one)", SoLuong = 2, DonGia = 250000, ThanhTien = 500000 },
                new ChiTietHoaDon { MaHoaDon = "HD002", LoaiHang = "Ve", MaHang = "VE005", TenHang = "Vé combo Gia đình (2 Lớn + 1 Bé)", SoLuong = 1, DonGia = 550000, ThanhTien = 550000 },
                new ChiTietHoaDon { MaHoaDon = "HD002", LoaiHang = "DichVu", MaHang = "DV001", TenHang = "Combo Burger Bò + Khoai tây", SoLuong = 1, DonGia = 65000, ThanhTien = 65000 },
                new ChiTietHoaDon { MaHoaDon = "HD003", LoaiHang = "Ve", MaHang = "VE007", TenHang = "Vé VIP FastPass ưu tiên", SoLuong = 2, DonGia = 450000, ThanhTien = 900000 },
                new ChiTietHoaDon { MaHoaDon = "HD004", LoaiHang = "Ve", MaHang = "VE009", TenHang = "Vé học sinh - sinh viên ưu đãi", SoLuong = 2, DonGia = 180000, ThanhTien = 360000 },
                new ChiTietHoaDon { MaHoaDon = "HD005", LoaiHang = "Ve", MaHang = "VE006", TenHang = "Vé combo Gia đình VIP (2 Lớn + 2 Bé)", SoLuong = 1, DonGia = 700000, ThanhTien = 700000 },
                new ChiTietHoaDon { MaHoaDon = "HD006", LoaiHang = "DichVu", MaHang = "DV003", TenHang = "Pizza xúc xích cỡ vừa", SoLuong = 1, DonGia = 110000, ThanhTien = 110000 },
                new ChiTietHoaDon { MaHoaDon = "HD006", LoaiHang = "DichVu", MaHang = "DV006", TenHang = "Nước ngọt lon Coca-Cola 320ml", SoLuong = 3, DonGia = 20000, ThanhTien = 60000 },
                new ChiTietHoaDon { MaHoaDon = "HD007", LoaiHang = "Ve", MaHang = "VE001", TenHang = "Vé vào cổng tiêu chuẩn", SoLuong = 3, DonGia = 100000, ThanhTien = 300000 },
                new ChiTietHoaDon { MaHoaDon = "HD007", LoaiHang = "DichVu", MaHang = "DV009", TenHang = "Thuê tủ gửi đồ thông minh (Cả ngày)", SoLuong = 2, DonGia = 30000, ThanhTien = 60000 },
                new ChiTietHoaDon { MaHoaDon = "HD007", LoaiHang = "DichVu", MaHang = "DV005", TenHang = "Nước suối Aquafina 500ml", SoLuong = 2, DonGia = 15000, ThanhTien = 30000 },
                new ChiTietHoaDon { MaHoaDon = "HD008", LoaiHang = "Ve", MaHang = "VE008", TenHang = "Vé dạ hội vui chơi sau 17h00", SoLuong = 2, DonGia = 120000, ThanhTien = 240000 },
                new ChiTietHoaDon { MaHoaDon = "HD009", LoaiHang = "Ve", MaHang = "VE002", TenHang = "Vé trọn gói người lớn (All-in-one)", SoLuong = 2, DonGia = 250000, ThanhTien = 500000 },
                new ChiTietHoaDon { MaHoaDon = "HD009", LoaiHang = "Ve", MaHang = "VE003", TenHang = "Vé trọn gói trẻ em (Cao < 1m3)", SoLuong = 1, DonGia = 150000, ThanhTien = 150000 },
                new ChiTietHoaDon { MaHoaDon = "HD010", LoaiHang = "Ve", MaHang = "VE004", TenHang = "Vé công viên nước Paradise", SoLuong = 2, DonGia = 120000, ThanhTien = 240000 },
                new ChiTietHoaDon { MaHoaDon = "HD011", LoaiHang = "Ve", MaHang = "VE010", TenHang = "Vé thẻ tháng Member Platinum", SoLuong = 1, DonGia = 1200000, ThanhTien = 1200000 },
                new ChiTietHoaDon { MaHoaDon = "HD012", LoaiHang = "DichVu", MaHang = "DV012", TenHang = "Thuê chòi nghỉ mát gia đình VIP", SoLuong = 1, DonGia = 200000, ThanhTien = 200000 },
                new ChiTietHoaDon { MaHoaDon = "HD012", LoaiHang = "DichVu", MaHang = "DV007", TenHang = "Trà sữa trân châu đường đen", SoLuong = 2, DonGia = 35000, ThanhTien = 70000 },
                new ChiTietHoaDon { MaHoaDon = "HD012", LoaiHang = "DichVu", MaHang = "DV014", TenHang = "Kem tươi ốc quế 2 màu", SoLuong = 2, DonGia = 20000, ThanhTien = 40000 },
                new ChiTietHoaDon { MaHoaDon = "HD013", LoaiHang = "Ve", MaHang = "VE002", TenHang = "Vé trọn gói người lớn (All-in-one)", SoLuong = 2, DonGia = 250000, ThanhTien = 500000 },
                new ChiTietHoaDon { MaHoaDon = "HD014", LoaiHang = "Ve", MaHang = "VE003", TenHang = "Vé trọn gói trẻ em (Cao < 1m3)", SoLuong = 2, DonGia = 150000, ThanhTien = 300000 },
                new ChiTietHoaDon { MaHoaDon = "HD014", LoaiHang = "DichVu", MaHang = "DV015", TenHang = "Chụp ảnh kỷ niệm lấy ngay", SoLuong = 1, DonGia = 40000, ThanhTien = 40000 },
                new ChiTietHoaDon { MaHoaDon = "HD015", LoaiHang = "TroChoi", MaHang = "TC001", TenHang = "Tàu lượn siêu tốc Cyclone", SoLuong = 1, DonGia = 80000, ThanhTien = 80000 },
                new ChiTietHoaDon { MaHoaDon = "HD016", LoaiHang = "Ve", MaHang = "VE006", TenHang = "Vé combo Gia đình VIP (2 Lớn + 2 Bé)", SoLuong = 1, DonGia = 700000, ThanhTien = 700000 },
                new ChiTietHoaDon { MaHoaDon = "HD017", LoaiHang = "TroChoi", MaHang = "TC012", TenHang = "Đua xe F1 mini Karting", SoLuong = 2, DonGia = 90000, ThanhTien = 180000 },
                new ChiTietHoaDon { MaHoaDon = "HD018", LoaiHang = "Ve", MaHang = "VE007", TenHang = "Vé VIP FastPass ưu tiên", SoLuong = 1, DonGia = 450000, ThanhTien = 450000 },
                new ChiTietHoaDon { MaHoaDon = "HD019", LoaiHang = "Ve", MaHang = "VE009", TenHang = "Vé học sinh - sinh viên ưu đãi", SoLuong = 3, DonGia = 180000, ThanhTien = 540000 },
                new ChiTietHoaDon { MaHoaDon = "HD020", LoaiHang = "Ve", MaHang = "VE005", TenHang = "Vé combo Gia đình (2 Lớn + 1 Bé)", SoLuong = 1, DonGia = 550000, ThanhTien = 550000 },
                new ChiTietHoaDon { MaHoaDon = "HD021", LoaiHang = "Ve", MaHang = "VE002", TenHang = "Vé trọn gói người lớn (All-in-one)", SoLuong = 1, DonGia = 250000, ThanhTien = 250000 },
                new ChiTietHoaDon { MaHoaDon = "HD022", LoaiHang = "DichVu", MaHang = "DV002", TenHang = "Gà rán giòn cay phần 2 miếng", SoLuong = 2, DonGia = 55000, ThanhTien = 110000 },
                new ChiTietHoaDon { MaHoaDon = "HD023", LoaiHang = "Ve", MaHang = "VE001", TenHang = "Vé vào cổng tiêu chuẩn", SoLuong = 3, DonGia = 100000, ThanhTien = 300000 },
                new ChiTietHoaDon { MaHoaDon = "HD024", LoaiHang = "Ve", MaHang = "VE004", TenHang = "Vé công viên nước Paradise", SoLuong = 3, DonGia = 120000, ThanhTien = 360000 },
                new ChiTietHoaDon { MaHoaDon = "HD025", LoaiHang = "Ve", MaHang = "VE006", TenHang = "Vé combo Gia đình VIP (2 Lớn + 2 Bé)", SoLuong = 1, DonGia = 700000, ThanhTien = 700000 },
                new ChiTietHoaDon { MaHoaDon = "HD026", LoaiHang = "Ve", MaHang = "VE002", TenHang = "Vé trọn gói người lớn (All-in-one)", SoLuong = 2, DonGia = 250000, ThanhTien = 500000 },
                new ChiTietHoaDon { MaHoaDon = "HD027", LoaiHang = "Ve", MaHang = "VE009", TenHang = "Vé học sinh - sinh viên ưu đãi", SoLuong = 2, DonGia = 180000, ThanhTien = 360000 },
                new ChiTietHoaDon { MaHoaDon = "HD028", LoaiHang = "Ve", MaHang = "VE007", TenHang = "Vé VIP FastPass ưu tiên", SoLuong = 2, DonGia = 450000, ThanhTien = 900000 },
                new ChiTietHoaDon { MaHoaDon = "HD029", LoaiHang = "TroChoi", MaHang = "TC004", TenHang = "Tháp rơi tự do Free Fall", SoLuong = 2, DonGia = 70000, ThanhTien = 140000 },
                new ChiTietHoaDon { MaHoaDon = "HD030", LoaiHang = "Ve", MaHang = "VE005", TenHang = "Vé combo Gia đình (2 Lớn + 1 Bé)", SoLuong = 1, DonGia = 550000, ThanhTien = 550000 }
            };
            context.ChiTietHoaDons.AddRange(chiTiets);
            context.SaveChanges();
        }
    }
}
