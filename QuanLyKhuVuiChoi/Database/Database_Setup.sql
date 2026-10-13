-- ==============================================================================
-- HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ
-- DATABASE SCRIPT: QuanLyKhuVuiChoi
-- CÔNG NGHỆ: SQL Server 2019/2022 / LocalDB
-- ==============================================================================

USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'QuanLyKhuVuiChoi')
BEGIN
    CREATE DATABASE QuanLyKhuVuiChoi;
END
GO

USE QuanLyKhuVuiChoi;
GO

-- 1. BẢNG KHÁCH HÀNG
IF OBJECT_ID(N'dbo.ChiTietHoaDon', N'U') IS NOT NULL DROP TABLE dbo.ChiTietHoaDon;
IF OBJECT_ID(N'dbo.HoaDon', N'U') IS NOT NULL DROP TABLE dbo.HoaDon;
IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NOT NULL DROP TABLE dbo.TaiKhoan;
IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NOT NULL DROP TABLE dbo.KhachHang;
IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NOT NULL DROP TABLE dbo.NhanVien;
IF OBJECT_ID(N'dbo.TroChoi', N'U') IS NOT NULL DROP TABLE dbo.TroChoi;
IF OBJECT_ID(N'dbo.DichVu', N'U') IS NOT NULL DROP TABLE dbo.DichVu;
IF OBJECT_ID(N'dbo.Ve', N'U') IS NOT NULL DROP TABLE dbo.Ve;
GO

CREATE TABLE dbo.KhachHang (
    MaKH NVARCHAR(20) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(10) NOT NULL DEFAULT N'Nam',
    SoDienThoai NVARCHAR(20) NOT NULL UNIQUE,
    Email NVARCHAR(100) NULL,
    DiaChi NVARCHAR(200) NULL,
    NgayDangKy DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- 2. BẢNG NHÂN VIÊN
CREATE TABLE dbo.NhanVien (
    MaNV NVARCHAR(20) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(10) NOT NULL DEFAULT N'Nam',
    SoDienThoai NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NULL,
    DiaChi NVARCHAR(200) NULL,
    ChucVu NVARCHAR(50) NOT NULL,
    NgayVaoLam DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang làm việc'
);
GO

-- 3. BẢNG TÀI KHOẢN HỆ THỐNG
CREATE TABLE dbo.TaiKhoan (
    TenDangNhap NVARCHAR(50) NOT NULL PRIMARY KEY,
    MatKhau NVARCHAR(100) NOT NULL,
    MaNV NVARCHAR(20) NULL,
    Quyen NVARCHAR(20) NOT NULL DEFAULT N'NhanVien', -- Admin, NhanVien
    TrangThai BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE SET NULL
);
GO

-- 4. BẢNG TRÒ CHƠI
CREATE TABLE dbo.TroChoi (
    MaTroChoi NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenTroChoi NVARCHAR(100) NOT NULL,
    LoaiTroChoi NVARCHAR(50) NOT NULL, -- Cảm giác mạnh, Trẻ em, Gia đình, Dưới nước, Phiêu lưu
    GiaVe DECIMAL(18, 2) NOT NULL DEFAULT 0 CHECK (GiaVe >= 0),
    DoTuoiToiThieu INT NOT NULL DEFAULT 0 CHECK (DoTuoiToiThieu >= 0),
    SucChua INT NOT NULL DEFAULT 1 CHECK (SucChua > 0),
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang hoạt động', -- Đang hoạt động, Bảo trì, Tạm ngưng
    MoTa NVARCHAR(255) NULL
);
GO

-- 5. BẢNG DỊCH VỤ
CREATE TABLE dbo.DichVu (
    MaDichVu NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenDichVu NVARCHAR(100) NOT NULL,
    LoaiDichVu NVARCHAR(50) NOT NULL, -- Ẩm thực, Nước uống, Thuê tủ đồ, Khu nghỉ ngơi, Khác
    DonGia DECIMAL(18, 2) NOT NULL DEFAULT 0 CHECK (DonGia >= 0),
    SoLuongTon INT NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0),
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang phục vụ', -- Đang phục vụ, Hết hàng, Tạm ngưng
    MoTa NVARCHAR(255) NULL
);
GO

-- 6. BẢNG VÉ
CREATE TABLE dbo.Ve (
    MaVe NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenVe NVARCHAR(100) NOT NULL,
    LoaiVe NVARCHAR(50) NOT NULL, -- Vé ngày, Vé trẻ em, Vé người lớn, Vé gia đình, Vé VIP
    GiaVe DECIMAL(18, 2) NOT NULL DEFAULT 0 CHECK (GiaVe >= 0),
    ThoiHan NVARCHAR(50) NOT NULL DEFAULT N'Trong ngày',
    MoTa NVARCHAR(255) NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang áp dụng' -- Đang áp dụng, Ngừng áp dụng
);
GO

-- 7. BẢNG HÓA ĐƠN
CREATE TABLE dbo.HoaDon (
    MaHoaDon NVARCHAR(20) NOT NULL PRIMARY KEY,
    MaKH NVARCHAR(20) NULL,
    MaNV NVARCHAR(20) NULL,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18, 2) NOT NULL DEFAULT 0 CHECK (TongTien >= 0),
    GiamGia DECIMAL(5, 2) NOT NULL DEFAULT 0 CHECK (GiamGia >= 0 AND GiamGia <= 100),
    PhuongThucThanhToan NVARCHAR(50) NOT NULL DEFAULT N'Tiền mặt', -- Tiền mặt, Chuyển khoản, Thẻ
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đã thanh toán', -- Đã thanh toán, Đã hủy
    GhiChu NVARCHAR(255) NULL,
    CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH) ON DELETE SET NULL,
    CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE SET NULL
);
GO

-- 8. BẢNG CHI TIẾT HÓA ĐƠN
CREATE TABLE dbo.ChiTietHoaDon (
    MaCTHD INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    MaHoaDon NVARCHAR(20) NOT NULL,
    LoaiHang NVARCHAR(20) NOT NULL, -- Ve, DichVu, TroChoi
    MaHang NVARCHAR(20) NOT NULL,
    TenHang NVARCHAR(100) NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1 CHECK (SoLuong > 0),
    DonGia DECIMAL(18, 2) NOT NULL DEFAULT 0 CHECK (DonGia >= 0),
    ThanhTien DECIMAL(18, 2) NOT NULL DEFAULT 0 CHECK (ThanhTien >= 0),
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES dbo.HoaDon(MaHoaDon) ON DELETE CASCADE
);
GO

-- INDEXES CHO CÁC TRƯỜNG THƯỜNG TÌM KIẾM
CREATE NONCLUSTERED INDEX IX_KhachHang_SoDienThoai ON dbo.KhachHang(SoDienThoai);
CREATE NONCLUSTERED INDEX IX_KhachHang_HoTen ON dbo.KhachHang(HoTen);
CREATE NONCLUSTERED INDEX IX_NhanVien_HoTen ON dbo.NhanVien(HoTen);
CREATE NONCLUSTERED INDEX IX_TroChoi_TenTroChoi ON dbo.TroChoi(TenTroChoi);
CREATE NONCLUSTERED INDEX IX_DichVu_TenDichVu ON dbo.DichVu(TenDichVu);
CREATE NONCLUSTERED INDEX IX_Ve_TenVe ON dbo.Ve(TenVe);
CREATE NONCLUSTERED INDEX IX_HoaDon_NgayLap ON dbo.HoaDon(NgayLap);
CREATE NONCLUSTERED INDEX IX_HoaDon_MaKH ON dbo.HoaDon(MaKH);
GO

-- ==============================================================================
-- SEED DATA (DỮ LIỆU MẪU ĐẦY ĐỦ VÀ CHÍNH XÁC)
-- ==============================================================================

-- 1. SEED KHÁCH HÀNG (20 khách hàng)
INSERT INTO dbo.KhachHang (MaKH, HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, NgayDangKy) VALUES
('KH001', N'Nguyễn Văn An', '1995-04-12', N'Nam', '0912345678', 'an.nguyen@gmail.com', N'123 Cầu Giấy, Hà Nội', '2026-01-10 08:30:00'),
('KH002', N'Trần Thị Mai', '1998-08-23', N'Nữ', '0987654321', 'mai.tran@gmail.com', N'45 Hai Bà Trưng, Hà Nội', '2026-01-15 09:15:00'),
('KH003', N'Lê Hoàng Long', '1992-11-05', N'Nam', '0903123456', 'long.le@yahoo.com', N'78 Nguyễn Trãi, Thanh Xuân, Hà Nội', '2026-02-01 10:00:00'),
('KH004', N'Phạm Thu Trang', '2000-02-14', N'Nữ', '0934567890', 'trang.pham@gmail.com', N'12 Kim Mã, Ba Đình, Hà Nội', '2026-02-14 14:20:00'),
('KH005', N'Vũ Đình Hưng', '1988-06-30', N'Nam', '0978112233', 'hung.vu@gmail.com', N'56 Đội Cấn, Ba Đình, Hà Nội', '2026-02-20 16:45:00'),
('KH006', N'Đỗ Bích Phương', '1997-09-18', N'Nữ', '0965223344', 'phuong.do@outlook.com', N'89 Xã Đàn, Đống Đa, Hà Nội', '2026-03-05 11:10:00'),
('KH007', N'Hoàng Tuấn Kiệt', '2001-12-01', N'Nam', '0943334455', 'kiet.hoang@gmail.com', N'34 Chùa Bộc, Đống Đa, Hà Nội', '2026-03-12 13:00:00'),
('KH008', N'Ngô Thanh Vân', '1994-03-25', N'Nữ', '0919445566', 'van.ngo@gmail.com', N'67 Láng Hạ, Đống Đa, Hà Nội', '2026-03-18 15:30:00'),
('KH009', N'Bùi Minh Trí', '1990-07-19', N'Nam', '0982556677', 'tri.bui@gmail.com', N'90 Hoàng Hoa Thám, Ba Đình, Hà Nội', '2026-04-02 09:40:00'),
('KH010', N'Đinh Thùy Linh', '2003-05-09', N'Nữ', '0936667788', 'linh.dinh@gmail.com', N'11 Thái Hà, Đống Đa, Hà Nội', '2026-04-15 10:50:00'),
('KH011', N'Lý Quang Diệu', '1985-10-15', N'Nam', '0974778899', 'dieu.ly@gmail.com', N'22 Tây Sơn, Đống Đa, Hà Nội', '2026-05-01 08:20:00'),
('KH012', N'Dương Cẩm Lynh', '1996-01-28', N'Nữ', '0908889900', 'lynh.duong@gmail.com', N'44 Giải Phóng, Hai Bà Trưng, Hà Nội', '2026-05-10 14:10:00'),
('KH013', N'Hà Anh Tuấn', '1993-12-12', N'Nam', '0917990011', 'tuan.ha@gmail.com', N'55 Phố Huế, Hoàn Kiếm, Hà Nội', '2026-06-01 16:00:00'),
('KH014', N'Phan Như Quỳnh', '1999-04-04', N'Nữ', '0981001122', 'quynh.phan@gmail.com', N'88 Trần Duy Hưng, Cầu Giấy, Hà Nội', '2026-06-15 10:25:00'),
('KH015', N'Trịnh Thăng Bình', '1991-08-08', N'Nam', '0935112233', 'binh.trinh@gmail.com', N'15 Lê Văn Lương, Thanh Xuân, Hà Nội', '2026-07-04 11:35:00'),
('KH016', N'Tạ Mỹ Linh', '2002-10-20', N'Nữ', '0962223344', 'linh.ta@gmail.com', N'39 Xuân Thủy, Cầu Giấy, Hà Nội', '2026-07-20 15:15:00'),
('KH017', N'Võ Tấn Phát', '1995-05-30', N'Nam', '0949334455', 'phat.vo@gmail.com', N'73 Hoàng Đạo Thúy, Cầu Giấy, Hà Nội', '2026-08-05 09:05:00'),
('KH018', N'Cao Thái Hà', '1998-07-07', N'Nữ', '0916445566', 'ha.cao@gmail.com', N'101 Nguyễn Thị Định, Cầu Giấy, Hà Nội', '2026-08-18 13:40:00'),
('KH019', N'Đoàn Văn Hậu', '2000-09-14', N'Nam', '0985556677', 'hau.doan@gmail.com', N'28 Nguyễn Chí Thanh, Ba Đình, Hà Nội', '2026-09-02 14:50:00'),
('KH020', N'Mai Phương Thúy', '1994-11-22', N'Nữ', '0938667788', 'thuy.mai@gmail.com', N'63 Liễu Giai, Ba Đình, Hà Nội', '2026-09-20 16:30:00');
GO

-- 2. SEED NHÂN VIÊN (10 nhân viên)
INSERT INTO dbo.NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, ChucVu, NgayVaoLam, TrangThai) VALUES
('NV001', N'Trương Gia Bình', '1982-05-19', N'Nam', '0901234001', 'binh.truong@amusement.vn', N'Quận Ba Đình, Hà Nội', N'Quản lý', '2024-01-01', N'Đang làm việc'),
('NV002', N'Lê Thị Hương', '1990-09-12', N'Nữ', '0901234002', 'huong.le@amusement.vn', N'Quận Cầu Giấy, Hà Nội', N'Thu ngân', '2024-03-15', N'Đang làm việc'),
('NV003', N'Nguyễn Quang Hải', '1996-04-12', N'Nam', '0901234003', 'hai.nguyen@amusement.vn', N'Quận Đống Đa, Hà Nội', N'Thu ngân', '2024-06-01', N'Đang làm việc'),
('NV004', N'Phạm Hải Yến', '1998-11-09', N'Nữ', '0901234004', 'yen.pham@amusement.vn', N'Quận Thanh Xuân, Hà Nội', N'Soát vé', '2024-07-10', N'Đang làm việc'),
('NV005', N'Trần Minh Vương', '1995-03-28', N'Nam', '0901234005', 'vuong.tran@amusement.vn', N'Quận Nam Từ Liêm, Hà Nội', N'Kỹ thuật', '2024-02-20', N'Đang làm việc'),
('NV006', N'Đỗ Hùng Dũng', '1993-09-08', N'Nam', '0901234006', 'dung.do@amusement.vn', N'Quận Hoàng Mai, Hà Nội', N'Kỹ thuật', '2024-04-05', N'Đang làm việc'),
('NV007', N'Vũ Văn Thanh', '1996-04-14', N'Nam', '0901234007', 'thanh.vu@amusement.vn', N'Quận Hà Đông, Hà Nội', N'Bảo vệ', '2024-05-12', N'Đang làm việc'),
('NV008', N'Nguyễn Thị Tuyết Dung', '1993-12-13', N'Nữ', '0901234008', 'dung.tuyet@amusement.vn', N'Quận Cầu Giấy, Hà Nội', N'Chăm sóc khách hàng', '2024-08-01', N'Đang làm việc'),
('NV009', N'Bùi Tiến Dũng', '1997-02-28', N'Nam', '0901234009', 'dung.bui@amusement.vn', N'Quận Tây Hồ, Hà Nội', N'Soát vé', '2024-09-01', N'Đang làm việc'),
('NV010', N'Hoàng Đức Tùng', '1999-07-21', N'Nam', '0901234010', 'tung.hoang@amusement.vn', N'Quận Long Biên, Hà Nội', N'Thu ngân', '2024-10-15', N'Đang làm việc');
GO

-- 3. SEED TÀI KHOẢN HỆ THỐNG
INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, MaNV, Quyen, TrangThai) VALUES
('admin', '123456', 'NV001', N'Admin', 1),
('thungan1', '123456', 'NV002', N'NhanVien', 1),
('thungan2', '123456', 'NV003', N'NhanVien', 1),
('soatve', '123456', 'NV004', N'NhanVien', 1);
GO

-- 4. SEED TRÒ CHƠI (15 trò chơi)
INSERT INTO dbo.TroChoi (MaTroChoi, TenTroChoi, LoaiTroChoi, GiaVe, DoTuoiToiThieu, SucChua, TrangThai, MoTa) VALUES
('TC001', N'Tàu lượn siêu tốc Cyclone', N'Cảm giác mạnh', 80000, 12, 24, N'Đang hoạt động', N'Tốc độ cực đại 90km/h với 3 vòng xoắn ốc liên hoàn'),
('TC002', N'Đu quay khổng lồ Wonder Wheel', N'Gia đình', 50000, 3, 60, N'Đang hoạt động', N'Vòng quay ngắm toàn cảnh thành phố ở độ cao 50m'),
('TC003', N'Xe điện đụng Bumper Cars', N'Gia đình', 40000, 6, 20, N'Đang hoạt động', N'Sân xe điện tử hiện đại sàn cảm ứng an toàn'),
('TC004', N'Tháp rơi tự do Free Fall', N'Cảm giác mạnh', 70000, 14, 16, N'Đang hoạt động', N'Tháp thả rơi từ độ cao 40m trong 3 giây'),
('TC005', N'Ngôi nhà ma bí ẩn Phantom Manor', N'Phiêu lưu', 60000, 10, 15, N'Đang hoạt động', N'Mê cung âm thanh ánh sáng rùng rợn sống động'),
('TC006', N'Đường trượt nước Kamikaze', N'Dưới nước', 75000, 10, 10, N'Đang hoạt động', N'Máng trượt dốc 45 độ cực phấn khích'),
('TC007', N'Hồ tạo sóng Wave Paradise', N'Dưới nước', 60000, 5, 200, N'Đang hoạt động', N'Bể tạo sóng nhân tạo 6 cấp độ như bãi biển thật'),
('TC008', N'Vòng quay ngựa gỗ Carousel', N'Trẻ em', 30000, 2, 32, N'Đang hoạt động', N'Ngựa gỗ cổ tích phong cách hoàng gia Pháp'),
('TC009', N'Khu liên hoàn Kidzone', N'Trẻ em', 50000, 2, 80, N'Đang hoạt động', N'Nhà bóng, cầu trượt và vận động mềm cho bé'),
('TC010', N'Thám hiểm rừng Amazon thuyền vượt thác', N'Phiêu lưu', 65000, 8, 18, N'Đang hoạt động', N'Du ngoạn thác nước hoang dã với hiệu ứng sương mù'),
('TC011', N'Rạp chiếu phim 9D Cinema', N'Công nghệ', 55000, 6, 25, N'Đang hoạt động', N'Ghế chuyển động, mưa phùn, gió lốc sống động'),
('TC012', N'Đua xe F1 mini Karting', N'Cảm giác mạnh', 90000, 12, 12, N'Đang hoạt động', N'Đường đua chuyên nghiệp xe kart động cơ điện'),
('TC013', N'Bắn súng laser Laser Tag', N'Hành động', 50000, 8, 20, N'Bảo trì', N'Đang nâng cấp cảm biến hồng ngoại thế hệ mới'),
('TC014', N'Đĩa bay xoay cuồng Disko Coaster', N'Cảm giác mạnh', 70000, 12, 24, N'Đang hoạt động', N'Đĩa bay trượt trên ray hình chữ U xoay 360 độ'),
('TC015', N'Leo núi nhân tạo Rock Climbing', N'Thể thao', 45000, 8, 10, N'Đang hoạt động', N'Vách núi cao 15m với 4 cấp độ dây đai an toàn');
GO

-- 5. SEED DỊCH VỤ (15 dịch vụ)
INSERT INTO dbo.DichVu (MaDichVu, TenDichVu, LoaiDichVu, DonGia, SoLuongTon, TrangThai, MoTa) VALUES
('DV001', N'Combo Burger Bò + Khoai tây', N'Ẩm thực', 65000, 150, N'Đang phục vụ', N'Bánh burger bò Úc nướng kèm khoai chiên giòn'),
('DV002', N'Gà rán giòn cay phần 2 miếng', N'Ẩm thực', 55000, 120, N'Đang phục vụ', N'Gà rán sốt cay giòn rụm công thức đặc biệt'),
('DV003', N'Pizza xúc xích cỡ vừa', N'Ẩm thực', 110000, 80, N'Đang phục vụ', N'Pizza nướng phô mai Mozzarella béo ngậy'),
('DV004', N'Mì Ý sốt bò băm Spaghetti', N'Ẩm thực', 60000, 95, N'Đang phục vụ', N'Mì Ý xốt cà chua bò băm phô mai thơm ngon'),
('DV005', N'Nước suối Aquafina 500ml', N'Nước uống', 15000, 500, N'Đang phục vụ', N'Nước khoáng tinh khiết đóng chai ướp lạnh'),
('DV006', N'Nước ngọt lon Coca-Cola 320ml', N'Nước uống', 20000, 400, N'Đang phục vụ', N'Nước giải khát có ga Coca-Cola mát lạnh'),
('DV007', N'Trà sữa trân châu đường đen', N'Nước uống', 35000, 200, N'Đang phục vụ', N'Trà sữa Đài Loan trân châu đường đen dẻo dai'),
('DV008', N'Nước ép dưa hấu tươi', N'Nước uống', 30000, 150, N'Đang phục vụ', N'Nước ép trái cây tươi nguyên chất giải nhiệt'),
('DV009', N'Thuê tủ gửi đồ thông minh (Cả ngày)', N'Thuê đồ', 30000, 200, N'Đang phục vụ', N'Tủ locker khóa mã số điện tử an toàn tuyệt đối'),
('DV010', N'Thuê áo phao bơi an toàn', N'Thuê đồ', 20000, 180, N'Đang phục vụ', N'Áo phao tiêu chuẩn mọi lứa tuổi tại khu công viên nước'),
('DV011', N'Thuê xe đẩy trẻ em cao cấp', N'Thuê đồ', 50000, 40, N'Đang phục vụ', N'Xe đẩy gấp gọn êm ái cho bé dưới 4 tuổi'),
('DV012', N'Thuê chòi nghỉ mát gia đình VIP', N'Khu nghỉ ngơi', 200000, 15, N'Đang phục vụ', N'Chòi ven hồ có quạt mát, bàn ghế và sạc điện thoại'),
('DV013', N'Bắp rang bơ phô mai thơm lừng', N'Ăn vặt', 30000, 300, N'Đang phục vụ', N'Hộp bắp rang bơ vàng óng vị caramel phô mai'),
('DV014', N'Kem tươi ốc quế 2 màu', N'Ăn vặt', 20000, 250, N'Đang phục vụ', N'Kem vani socola mát lạnh vỏ ốc quế giòn tan'),
('DV015', N'Chụp ảnh kỷ niệm lấy ngay', N'Dịch vụ khác', 40000, 999, N'Đang phục vụ', N'In ảnh lưu niệm lấy ngay kích thước 13x18 có khung');
GO

-- 6. SEED VÉ (10 loại vé)
INSERT INTO dbo.Ve (MaVe, TenVe, LoaiVe, GiaVe, ThoiHan, MoTa, TrangThai) VALUES
('VE001', N'Vé vào cổng tiêu chuẩn', N'Vé ngày', 100000, N'Trong ngày', N'Vào cổng tham quan và chơi các trò chơi miễn phí cơ bản', N'Đang áp dụng'),
('VE002', N'Vé trọn gói người lớn (All-in-one)', N'Vé người lớn', 250000, N'Trong ngày', N'Chơi không giới hạn tất cả các trò chơi khô và nước', N'Đang áp dụng'),
('VE003', N'Vé trọn gói trẻ em (Cao < 1m3)', N'Vé trẻ em', 150000, N'Trong ngày', N'Áp dụng cho trẻ em, miễn phí toàn bộ khu Kidzone và nước', N'Đang áp dụng'),
('VE004', N'Vé công viên nước Paradise', N'Vé ngày', 120000, N'Trong ngày', N'Vé tham gia toàn bộ cụm trò chơi công viên nước', N'Đang áp dụng'),
('VE005', N'Vé combo Gia đình (2 Lớn + 1 Bé)', N'Vé gia đình', 550000, N'Trong ngày', N'Gói tiết kiệm cho gia đình 3 người trọn gói tất cả trò chơi', N'Đang áp dụng'),
('VE006', N'Vé combo Gia đình VIP (2 Lớn + 2 Bé)', N'Vé gia đình', 700000, N'Trong ngày', N'Trọn gói trò chơi + tặng 4 voucher nước uống 20k', N'Đang áp dụng'),
('VE007', N'Vé VIP FastPass ưu tiên', N'Vé VIP', 450000, N'Trong ngày', N'Trọn gói + lối đi ưu tiên không cần xếp hàng chờ đợi', N'Đang áp dụng'),
('VE008', N'Vé dạ hội vui chơi sau 17h00', N'Vé ngày', 120000, N'Buổi tối', N'Áp dụng sau 17h00 thưởng thức biểu diễn ánh sáng và trò chơi đêm', N'Đang áp dụng'),
('VE009', N'Vé học sinh - sinh viên ưu đãi', N'Vé người lớn', 180000, N'Trong ngày', N'Yêu cầu xuất trình thẻ học sinh hoặc sinh viên', N'Đang áp dụng'),
('VE010', N'Vé thẻ tháng Member Platinum', N'Vé VIP', 1200000, N'30 ngày', N'Vào cổng và chơi không giới hạn tất cả các ngày trong tháng', N'Đang áp dụng');
GO

-- 7. SEED HÓA ĐƠN & CHI TIẾT HÓA ĐƠN (30 hóa đơn thực tế có quan hệ)
-- HD001
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD001', 'KH001', 'NV002', '2026-09-01 08:45:00', 500000, 0, N'Tiền mặt', N'Đã thanh toán', N'Khách mua 2 vé trọn gói người lớn');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD001', 'Ve', 'VE002', N'Vé trọn gói người lớn (All-in-one)', 2, 250000, 500000);

-- HD002
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD002', 'KH002', 'NV002', '2026-09-01 09:30:00', 600000, 5, N'Chuyển khoản', N'Đã thanh toán', N'Combo gia đình kèm ăn nhẹ');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD002', 'Ve', 'VE005', N'Vé combo Gia đình (2 Lớn + 1 Bé)', 1, 550000, 550000),
('HD002', 'DichVu', 'DV001', N'Combo Burger Bò + Khoai tây', 1, 65000, 65000);

-- HD003
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD003', 'KH003', 'NV003', '2026-09-02 10:15:00', 900000, 0, N'Thẻ', N'Đã thanh toán', N'2 vé VIP FastPass');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD003', 'Ve', 'VE007', N'Vé VIP FastPass ưu tiên', 2, 450000, 900000);

-- HD004
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD004', 'KH004', 'NV003', '2026-09-02 14:00:00', 360000, 0, N'Tiền mặt', N'Đã thanh toán', N'2 vé sinh viên');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD004', 'Ve', 'VE009', N'Vé học sinh - sinh viên ưu đãi', 2, 180000, 360000);

-- HD005
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD005', 'KH005', 'NV010', '2026-09-03 11:20:00', 700000, 0, N'Chuyển khoản', N'Đã thanh toán', N'Vé combo gia đình VIP');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD005', 'Ve', 'VE006', N'Vé combo Gia đình VIP (2 Lớn + 2 Bé)', 1, 700000, 700000);

-- HD006
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD006', 'KH006', 'NV002', '2026-09-04 15:30:00', 170000, 0, N'Tiền mặt', N'Đã thanh toán', N'Dịch vụ đồ ăn và nước uống');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD006', 'DichVu', 'DV003', N'Pizza xúc xích cỡ vừa', 1, 110000, 110000),
('HD006', 'DichVu', 'DV006', N'Nước ngọt lon Coca-Cola 320ml', 3, 20000, 60000);

-- HD007
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD007', 'KH007', 'NV002', '2026-09-05 09:10:00', 400000, 0, N'Tiền mặt', N'Đã thanh toán', N'Vé vào cổng và thuê tủ');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD007', 'Ve', 'VE001', N'Vé vào cổng tiêu chuẩn', 3, 100000, 300000),
('HD007', 'DichVu', 'DV009', N'Thuê tủ gửi đồ thông minh (Cả ngày)', 2, 30000, 60000),
('HD007', 'DichVu', 'DV005', N'Nước suối Aquafina 500ml', 2, 15000, 30000);

-- HD008
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD008', 'KH008', 'NV003', '2026-09-06 16:45:00', 240000, 0, N'Chuyển khoản', N'Đã thanh toán', N'Vé dạ hội buổi tối');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD008', 'Ve', 'VE008', N'Vé dạ hội vui chơi sau 17h00', 2, 120000, 240000);

-- HD009
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD009', 'KH009', 'NV010', '2026-09-07 10:00:00', 650000, 0, N'Thẻ', N'Đã thanh toán', N'Vé trọn gói người lớn và trẻ em');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD009', 'Ve', 'VE002', N'Vé trọn gói người lớn (All-in-one)', 2, 250000, 500000),
('HD009', 'Ve', 'VE003', N'Vé trọn gói trẻ em (Cao < 1m3)', 1, 150000, 150000);

-- HD010
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD010', 'KH010', 'NV002', '2026-09-08 14:15:00', 240000, 0, N'Tiền mặt', N'Đã thanh toán', N'Vé công viên nước');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD010', 'Ve', 'VE004', N'Vé công viên nước Paradise', 2, 120000, 240000);

-- HD011
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD011', 'KH011', 'NV003', '2026-09-09 09:30:00', 1200000, 10, N'Chuyển khoản', N'Đã thanh toán', N'Vé tháng Member Platinum');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD011', 'Ve', 'VE010', N'Vé thẻ tháng Member Platinum', 1, 1200000, 1200000);

-- HD012
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD012', 'KH012', 'NV010', '2026-09-10 11:00:00', 310000, 0, N'Tiền mặt', N'Đã thanh toán', N'Thuê chòi và nước uống');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD012', 'DichVu', 'DV012', N'Thuê chòi nghỉ mát gia đình VIP', 1, 200000, 200000),
('HD012', 'DichVu', 'DV007', N'Trà sữa trân châu đường đen', 2, 35000, 70000),
('HD012', 'DichVu', 'DV014', N'Kem tươi ốc quế 2 màu', 2, 20000, 40000);

-- HD013
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD013', 'KH013', 'NV002', '2026-09-11 15:40:00', 500000, 0, N'Thẻ', N'Đã thanh toán', N'2 vé người lớn');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD013', 'Ve', 'VE002', N'Vé trọn gói người lớn (All-in-one)', 2, 250000, 500000);

-- HD014
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD014', 'KH014', 'NV002', '2026-09-12 13:25:00', 340000, 0, N'Tiền mặt', N'Đã thanh toán', N'Vé trẻ em và chụp ảnh');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD014', 'Ve', 'VE003', N'Vé trọn gói trẻ em (Cao < 1m3)', 2, 150000, 300000),
('HD014', 'DichVu', 'DV015', N'Chụp ảnh kỷ niệm lấy ngay', 1, 40000, 40000);

-- HD015
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD015', 'KH015', 'NV003', '2026-09-13 16:10:00', 80000, 0, N'Tiền mặt', N'Đã thanh toán', N'Vé chơi thêm Cyclone');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD015', 'TroChoi', 'TC001', N'Tàu lượn siêu tốc Cyclone', 1, 80000, 80000);

-- HD016
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD016', 'KH016', 'NV010', '2026-09-14 10:45:00', 700000, 0, N'Chuyển khoản', N'Đã thanh toán', N'Gia đình đi chơi ngày cuối tuần');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD016', 'Ve', 'VE006', N'Vé combo Gia đình VIP (2 Lớn + 2 Bé)', 1, 700000, 700000);

-- HD017
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD017', 'KH017', 'NV002', '2026-09-15 14:30:00', 180000, 0, N'Tiền mặt', N'Đã thanh toán', N'2 vé đua xe F1');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD017', 'TroChoi', 'TC012', N'Đua xe F1 mini Karting', 2, 90000, 180000);

-- HD018
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD018', 'KH018', 'NV003', '2026-09-16 11:15:00', 450000, 0, N'Thẻ', N'Đã thanh toán', N'1 vé VIP FastPass');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD018', 'Ve', 'VE007', N'Vé VIP FastPass ưu tiên', 1, 450000, 450000);

-- HD019
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD019', 'KH019', 'NV010', '2026-09-17 15:00:00', 540000, 5, N'Chuyển khoản', N'Đã thanh toán', N'3 vé sinh viên');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD019', 'Ve', 'VE009', N'Vé học sinh - sinh viên ưu đãi', 3, 180000, 540000);

-- HD020
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD020', 'KH020', 'NV002', '2026-09-18 09:50:00', 550000, 0, N'Tiền mặt', N'Đã thanh toán', N'Vé combo gia đình');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD020', 'Ve', 'VE005', N'Vé combo Gia đình (2 Lớn + 1 Bé)', 1, 550000, 550000);

-- HD021
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD021', 'KH001', 'NV002', '2026-09-20 10:20:00', 250000, 0, N'Tiền mặt', N'Đã thanh toán', N'Vé trọn gói người lớn');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD021', 'Ve', 'VE002', N'Vé trọn gói người lớn (All-in-one)', 1, 250000, 250000);

-- HD022
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD022', 'KH002', 'NV003', '2026-09-22 14:10:00', 165000, 0, N'Chuyển khoản', N'Đã thanh toán', N'Gà rán và nước uống');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD022', 'DichVu', 'DV002', N'Gà rán giòn cay phần 2 miếng', 2, 55000, 110000),
('HD022', 'DichVu', 'DV007', N'Trà sữa trân châu đường đen', 1, 35000, 35000),
('HD022', 'DichVu', 'DV006', N'Nước ngọt lon Coca-Cola 320ml', 1, 20000, 20000);

-- HD023
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD023', 'KH003', 'NV010', '2026-09-25 16:30:00', 300000, 0, N'Tiền mặt', N'Đã thanh toán', N'3 vé vào cổng tiêu chuẩn');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD023', 'Ve', 'VE001', N'Vé vào cổng tiêu chuẩn', 3, 100000, 300000);

-- HD024
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD024', 'KH004', 'NV002', '2026-09-28 11:40:00', 360000, 0, N'Tiền mặt', N'Đã thanh toán', N'3 vé công viên nước');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD024', 'Ve', 'VE004', N'Vé công viên nước Paradise', 3, 120000, 360000);

-- HD025
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD025', 'KH005', 'NV003', '2026-10-01 09:00:00', 700000, 0, N'Chuyển khoản', N'Đã thanh toán', N'Đầu tháng 10 mua combo gia đình');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD025', 'Ve', 'VE006', N'Vé combo Gia đình VIP (2 Lớn + 2 Bé)', 1, 700000, 700000);

-- HD026
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD026', 'KH006', 'NV002', '2026-10-02 13:15:00', 500000, 0, N'Thẻ', N'Đã thanh toán', N'2 vé trọn gói người lớn');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD026', 'Ve', 'VE002', N'Vé trọn gói người lớn (All-in-one)', 2, 250000, 500000);

-- HD027
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD027', 'KH007', 'NV010', '2026-10-04 15:20:00', 360000, 0, N'Tiền mặt', N'Đã thanh toán', N'2 vé sinh viên');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD027', 'Ve', 'VE009', N'Vé học sinh - sinh viên ưu đãi', 2, 180000, 360000);

-- HD028
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD028', 'KH008', 'NV002', '2026-10-05 10:05:00', 900000, 0, N'Chuyển khoản', N'Đã thanh toán', N'2 vé VIP FastPass');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD028', 'Ve', 'VE007', N'Vé VIP FastPass ưu tiên', 2, 450000, 900000);

-- HD029
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD029', 'KH009', 'NV003', '2026-10-07 14:40:00', 140000, 0, N'Tiền mặt', N'Đã thanh toán', N'Tháp rơi tự do 2 lượt');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD029', 'TroChoi', 'TC004', N'Tháp rơi tự do Free Fall', 2, 70000, 140000);

-- HD030
INSERT INTO dbo.HoaDon (MaHoaDon, MaKH, MaNV, NgayLap, TongTien, GiamGia, PhuongThucThanhToan, TrangThai, GhiChu)
VALUES ('HD030', 'KH010', 'NV002', '2026-10-08 08:30:00', 550000, 0, N'Tiền mặt', N'Đã thanh toán', N'Hóa đơn sáng nay vé gia đình');
INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, LoaiHang, MaHang, TenHang, SoLuong, DonGia, ThanhTien) VALUES
('HD030', 'Ve', 'VE005', N'Vé combo Gia đình (2 Lớn + 1 Bé)', 1, 550000, 550000);
GO

PRINT N'Cơ sở dữ liệu QuanLyKhuVuiChoi đã được thiết lập thành công!';
GO
