# DỰ ÁN: HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ (WONDERLAND PARK)

> **Báo cáo & Phần mềm Bài tập lớn môn: Lập trình Cơ sở dữ liệu**  
> **Ngôn ngữ & Công nghệ:** C# (.NET 8 WinForms), Microsoft SQL Server, Entity Framework Core 8, ClosedXML (Excel).  
> **Kiến trúc:** 3-Layer Architecture (Presentation - Business Logic - Data Access).

---

## 1. CÔNG NGHỆ VÀ THƯ VIỆN SỬ DỤNG (NUGET)

- **Ngôn ngữ:** C# 12
- **Framework:** .NET 8.0 (`net8.0-windows`)
- **Giao diện:** Windows Forms (WinForms) hiện đại, Flat style, tối ưu UX/UI, màu sắc phân cấp chuyên nghiệp.
- **Hệ quản trị CSDL:** Microsoft SQL Server 2019/2022 / LocalDB / SQL Server Express.
- **ORM / Data Access:**
  - `Microsoft.EntityFrameworkCore.SqlServer` (v8.0.11)
  - `Microsoft.EntityFrameworkCore.Design` (v8.0.11)
- **Xử lý Excel (Đọc, Ghi, Định dạng, Mẫu):**
  - `ClosedXML` (v0.102.3)
- **Cấu hình ứng dụng:**
  - `Microsoft.Extensions.Configuration.Json` (v8.0.1)

---

## 2. KIẾN TRÚC HỆ THỐNG (3-LAYER ARCHITECTURE)

```text
┌────────────────────────────────────────────────────────────────────────┐
│                      PRESENTATION LAYER (WinForms)                     │
│  - FormMain (Sidebar, Topbar, Content Embed Host)                      │
│  - FormDangNhap (Xác thực, Phân quyền Admin/Thu ngân)                  │
│  - FormDashboard (KPIs, Doanh thu, Đơn gần đây, Lối tắt)              │
│  - Member 1: FormKhachHang, FormImportKhachHang, FormExportKhachHang    │
│  - Member 2: FormTroChoi, FormDichVu, FormImportExportTroChoiDichVu    │
│  - Member 3: FormNhanVien, FormVe, FormImportExportNhanVienVe          │
│  - Member 4: FormBanVe (POS), FormHoaDon, FormBaoCaoDoanhThu           │
│  - Common: FormChiTietHoaDon                                           │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │
┌───────────────────────────────────▼────────────────────────────────────┐
│                   BUSINESS LOGIC LAYER (Services & DTOs)                │
│  - CustomerService, GameService, ParkServiceService                    │
│  - EmployeeService, TicketService, InvoiceService                      │
│  - ReportService, AuthService, ValidationHelper                        │
│  - IExcelService & ExcelService (ClosedXML generic exporter/importer)  │
│  - DTOs: CartItemDto, RevenueReportDto, ImportResult<T>, DashboardDto   │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │
┌───────────────────────────────────▼────────────────────────────────────┐
│                   DATA ACCESS LAYER (DAL & EF Core)                    │
│  - QuanLyKhuVuiChoiDbContext (DbSets, Fluent API, Constraints, Indexes)│
│  - Generic Repository Pattern (IRepository<T>, Repository<T>)          │
│  - DbInitializer (Tự động Seed dữ liệu mẫu nếu database trống)         │
│  - Entities: KhachHang, NhanVien, TaiKhoan, TroChoi, DichVu, Ve,       │
│              HoaDon, ChiTietHoaDon                                     │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │
┌───────────────────────────────────▼────────────────────────────────────┐
│                        MICROSOFT SQL SERVER                            │
│  - Database: QuanLyKhuVuiChoi                                          │
│  - Script: Database/Database_Setup.sql                                 │
└────────────────────────────────────────────────────────────────────────┘
```

---

## 3. PHÂN CHIA CÔNG VIỆC CHO 4 THÀNH VIÊN (12 FORMS BẮT BUỘC)

| Thành viên | Nhiệm vụ chính | Các Form phụ trách | Chức năng chi tiết |
|:---|:---|:---|:---|
| **THÀNH VIÊN 1** | **Quản lý Khách hàng** | 1. `FormKhachHang`<br>2. `FormImportKhachHang`<br>3. `FormExportKhachHang` | - CRUD khách hàng, tìm kiếm đa tiêu chí (Contains, StartsWith, Exact Match).<br>- Nhập khách hàng từ Excel (`.xlsx`), kiểm tra lỗi từng dòng (SĐT, trùng mã, thiếu tên), Preview, xác nhận lưu.<br>- Xuất danh sách khách hàng ra Excel theo bộ lọc hoặc toàn bộ, định dạng ClosedXML. |
| **THÀNH VIÊN 2** | **Quản lý Trò chơi & Dịch vụ** | 4. `FormTroChoi`<br>5. `FormDichVu`<br>6. `FormImportExportTroChoiDichVu` | - CRUD trò chơi, lọc loại trò chơi (Cảm giác mạnh, Trẻ em, Gia đình...), lọc trạng thái (Đang hoạt động, Bảo trì).<br>- CRUD dịch vụ, quản lý số lượng tồn kho (ẩm thực, đồ uống, thuê tủ, chòi nghỉ...), cập nhật tồn nhanh.<br>- Nhập/Xuất Excel cho cả Trò chơi và Dịch vụ (có preview, validation, file mẫu). |
| **THÀNH VIÊN 3** | **Quản lý Nhân viên & Vé** | 7. `FormNhanVien`<br>8. `FormVe`<br>9. `FormImportExportNhanVienVe` | - CRUD nhân viên, phân loại chức vụ (Quản lý, Thu ngân, Soát vé, Kỹ thuật), trạng thái làm việc.<br>- CRUD bảng giá vé: Vé ngày, Vé trẻ em, Vé người lớn, Vé gia đình, Vé VIP.<br>- Nhập/Xuất Excel cho Nhân viên và Loại vé bằng ClosedXML kèm file template. |
| **THÀNH VIÊN 4** | **Bán vé, Hóa đơn & Doanh thu** | 10. `FormBanVe`<br>11. `FormHoaDon`<br>12. `FormBaoCaoDoanhThu` | - Quầy POS bán vé: chọn khách hàng, chọn vé/dịch vụ/trò chơi, tính tiền tự động, áp mã giảm giá, phương thức thanh toán, in/xem hóa đơn.<br>- Quản lý hóa đơn: xem chi tiết, lọc ngày, lọc khách hàng, hủy hóa đơn an toàn (hoàn kho), xuất Excel.<br>- Báo cáo doanh thu theo ngày, tháng, năm, biểu đồ tỷ lệ doanh thu theo loại vé/dịch vụ/trò chơi, xuất Excel báo cáo. |

---

## 4. ĐÁP ỨNG ĐỦ 6 THAO TÁC BẮT BUỘC

1. **LƯU / THÊM (Create):** Nhập liệu → Kiểm tra ValidationHelper (không rỗng, SĐT 10 số, email đúng định dạng, giá >= 0) → Tự động tạo mã ID kế tiếp → Lưu CSDL → Refresh DataGridView.
2. **SỬA (Update):** Click chọn dòng trên DataGridView → Dữ liệu tự điền lên form điều khiển → Chỉnh sửa thông tin → Hỏi hộp thoại xác nhận → Cập nhật CSDL an toàn.
3. **XÓA (Delete):** Chọn dòng → Hiển thị hộp thoại cảnh báo xác nhận `MessageBoxButtons.YesNo` → Kiểm tra ràng buộc khóa ngoại (không xóa nếu đã phát sinh hóa đơn bán hàng) → Thông báo lỗi thân thiện, không bao giờ để crash chương trình.
4. **TÌM KIẾM (Search):** Hỗ trợ tìm kiếm theo Mã, Tên, Số điện thoại, Loại, Trạng thái. Tùy chọn kiểu tìm kiếm: `Chứa (Contains)`, `Bắt đầu bằng (StartsWith)`, `Chính xác (Exact Match)`.
5. **XUẤT FILE EXCEL (Export):** Sử dụng thư viện `ClosedXML`. Xuất file có:
   - Header chuyên nghiệp nền xanh `#1F4E78`, chữ trắng in hoa 16pt.
   - Dòng phụ: Thời gian xuất file, tổng số bản ghi.
   - Cột tiêu đề đậm, viền mỏng ngoài.
   - Định dạng tiền tệ (`#,##0`), ngày tháng (`dd/MM/yyyy`).
   - Tự động căn chỉnh độ rộng cột (`worksheet.Columns().AdjustToContents()`).
   - Hộp thoại hỏi mở file ngay sau khi xuất.
6. **NHẬP FILE EXCEL (Import):** Pipeline hoàn chỉnh:
   - `Chọn file .xlsx` → `Đọc dữ liệu bằng ClosedXML` → `Validate từng dòng` → `Hiển thị bảng Preview dòng hợp lệ` → `Liệt kê chi tiết lỗi từng dòng (Ví dụ: Dòng 5: Số điện thoại không hợp lệ; Dòng 8: Mã đã tồn tại)` → `Người dùng bấm Xác nhận` → `Lưu vào SQL Server` → `Báo cáo số dòng thành công/thất bại`.
   - Có nút **[Tải file mẫu Excel]** để xuất sẵn file template chuẩn.

---

## 5. THIẾT KẾ CƠ SỞ DỮ LIỆU (`QuanLyKhuVuiChoi`)

### Danh sách các bảng
1. **KhachHang**: `MaKH` (PK), `HoTen`, `NgaySinh`, `GioiTinh`, `SoDienThoai` (Unique), `Email`, `DiaChi`, `NgayDangKy`
2. **NhanVien**: `MaNV` (PK), `HoTen`, `NgaySinh`, `GioiTinh`, `SoDienThoai`, `Email`, `DiaChi`, `ChucVu`, `NgayVaoLam`, `TrangThai`
3. **TaiKhoan**: `TenDangNhap` (PK), `MatKhau`, `MaNV` (FK), `Quyen` (Admin, NhanVien), `TrangThai`
4. **TroChoi**: `MaTroChoi` (PK), `TenTroChoi`, `LoaiTroChoi`, `GiaVe` (>=0), `DoTuoiToiThieu`, `SucChua` (>0), `TrangThai`, `MoTa`
5. **DichVu**: `MaDichVu` (PK), `TenDichVu`, `LoaiDichVu`, `DonGia` (>=0), `SoLuongTon` (>=0), `TrangThai`, `MoTa`
6. **Ve**: `MaVe` (PK), `TenVe`, `LoaiVe`, `GiaVe` (>=0), `ThoiHan`, `MoTa`, `TrangThai`
7. **HoaDon**: `MaHoaDon` (PK), `MaKH` (FK), `MaNV` (FK), `NgayLap`, `TongTien` (>=0), `GiamGia` (0-100), `PhuongThucThanhToan`, `TrangThai`, `GhiChu`
8. **ChiTietHoaDon**: `MaCTHD` (PK Identity), `MaHoaDon` (FK), `LoaiHang` (Ve, DichVu, TroChoi), `MaHang`, `TenHang`, `SoLuong` (>0), `DonGia`, `ThanhTien`

### Khóa chính, Khóa ngoại & Indexes
- Các index tối ưu tìm kiếm: `IX_KhachHang_SoDienThoai`, `IX_KhachHang_HoTen`, `IX_NhanVien_HoTen`, `IX_TroChoi_TenTroChoi`, `IX_DichVu_TenDichVu`, `IX_Ve_TenVe`, `IX_HoaDon_NgayLap`.
- Khóa ngoại có hành vi `ON DELETE SET NULL` hoặc `CASCADE` bảo đảm toàn vẹn dữ liệu.

---

## 6. DỮ LIỆU MẪU ĐẦY ĐỦ (SEED DATA)

Dự án cung cấp sẵn bộ dữ liệu mẫu phong phú và thực tế (trong file `Database/Database_Setup.sql` và `Data/DbInitializer.cs`):
- **20 khách hàng:** Họ tên, số điện thoại, ngày sinh, địa chỉ tại các quận Hà Nội.
- **10 nhân viên:** Đầy đủ chức vụ: Quản lý, Thu ngân, Soát vé, Kỹ thuật, Bảo vệ, Chăm sóc KH.
- **15 trò chơi:** Tàu lượn siêu tốc Cyclone, Đu quay Wonder Wheel, Xe điện đụng, Tháp rơi tự do, Ngôi nhà ma, Đường trượt nước, Rạp phim 9D...
- **15 dịch vụ:** Combo Burger, Gà rán cay, Pizza xúc xích, Coca-Cola, Trà sữa trân châu, Thuê tủ gửi đồ thông minh, Thuê chòi nghỉ VIP...
- **10 loại vé:** Vé tiêu chuẩn, Vé người lớn, Vé trẻ em, Vé công viên nước, Combo gia đình, Vé VIP FastPass, Vé tháng Member Platinum...
- **30 hóa đơn:** Đầy đủ chi tiết hóa đơn, ngày lập trong tháng 9 và tháng 10 năm 2026, các phương thức thanh toán tiền mặt, chuyển khoản, thẻ.

---

## 7. TÀI KHOẢN ĐĂNG NHẬP DEMO

| Tên đăng nhập | Mật khẩu | Quyền | Nhân viên đại diện | Chức vụ |
|:---|:---|:---|:---|:---|
| **admin** | `123456` | **Admin** | Trương Gia Bình | Quản lý toàn quyền |
| **thungan1** | `123456` | **NhanVien** | Lê Thị Hương | Thu ngân bán vé |
| **thungan2** | `123456` | **NhanVien** | Nguyễn Quang Hải | Thu ngân bán vé |
| **soatve** | `123456` | **NhanVien** | Phạm Hải Yến | Soát vé |

---

## 8. HƯỚNG DẪN CẤU HÌNH VÀ CHẠY DỰ ÁN

### Cách 1: Chạy bằng Visual Studio 2022
1. Mở file solution hoặc mở thư mục `QuanLyKhuVuiChoi` trong Visual Studio 2022.
2. Kiểm tra chuỗi kết nối trong file `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=QuanLyKhuVuiChoi;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
   *(Nếu bạn dùng SQL Server Express, hãy đổi server thành `Server=.\\SQLEXPRESS;...` hoặc `Server=(localdb)\\mssqllocaldb;...`)*
3. Chạy file SQL `Database/Database_Setup.sql` trong SQL Server Management Studio (SSMS) để tạo database và seed data.  
   *(Lưu ý: Nếu không chạy SSMS trước, EF Core sẽ tự động gọi `DbInitializer` tạo bảng và seed dữ liệu tự động khi chương trình khởi chạy lần đầu!)*
4. Nhấn **F5** hoặc **Start** để khởi chạy phần mềm.

### Cách 2: Chạy bằng Command Line (`dotnet CLI`)
Mở PowerShell tại thư mục dự án và chạy:
```powershell
cd QuanLyKhuVuiChoi
dotnet run
```

---

## 9. CÁC FILE TEMPLATE EXCEL MẪU TRONG THƯ MỤC `ExcelTemplates/`

- `KhachHang_Template.xlsx`: Mẫu nhập danh sách khách hàng
- `NhanVien_Template.xlsx`: Mẫu nhập danh sách nhân viên
- `TroChoi_Template.xlsx`: Mẫu nhập trò chơi
- `DichVu_Template.xlsx`: Mẫu nhập dịch vụ
- `Ve_Template.xlsx`: Mẫu nhập loại vé

*(Các file này được sinh tự động bằng ClosedXML với cấu trúc cột khớp chính xác với database).*
