import os
import sys
import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.enum.table import WD_TABLE_ALIGNMENT
from docx.oxml import parse_xml
from docx.oxml.ns import nsdecls

def generate_report():
    doc = docx.Document()

    # Set page margins (0.8 in top/bottom, 0.85 in left/right)
    for section in doc.sections:
        section.top_margin = Inches(0.8)
        section.bottom_margin = Inches(0.8)
        section.left_margin = Inches(0.85)
        section.right_margin = Inches(0.85)

    COLOR_PRIMARY = RGBColor(31, 78, 120)     # #1F4E78 Navy
    COLOR_SECONDARY = RGBColor(46, 117, 182)  # #2E75B6 Blue
    COLOR_TEXT = RGBColor(30, 41, 59)         # #1E293B Slate Dark
    COLOR_MUTED = RGBColor(100, 116, 139)     # #64748B Slate Muted
    COLOR_CODE = RGBColor(37, 99, 235)        # #2563EB Code Blue

    style_normal = doc.styles['Normal']
    style_normal.font.name = 'Segoe UI'
    style_normal.font.size = Pt(10)
    style_normal.font.color.rgb = COLOR_TEXT
    style_normal.paragraph_format.line_spacing = 1.15
    style_normal.paragraph_format.space_after = Pt(3)

    def set_cell_background(cell, hex_color):
        shading = parse_xml(f'<w:shd {nsdecls("w")} w:fill="{hex_color}"/>')
        cell._tc.get_or_add_tcPr().append(shading)

    def set_cell_margins(cell, top=80, bottom=80, left=110, right=110):
        tcMar = parse_xml(f'<w:tcMar {nsdecls("w")}><w:top w:w="{top}" w:type="dxa"/><w:bottom w:w="{bottom}" w:type="dxa"/><w:left w:w="{left}" w:type="dxa"/><w:right w:w="{right}" w:type="dxa"/></w:tcMar>')
        cell._tc.get_or_add_tcPr().append(tcMar)

    def set_table_borders(table, color="CBD5E1"):
        borders = parse_xml(f'''
            <w:tblBorders {nsdecls("w")}>
                <w:top w:val="single" w:sz="6" w:space="0" w:color="{color}"/>
                <w:bottom w:val="single" w:sz="6" w:space="0" w:color="{color}"/>
                <w:left w:val="single" w:sz="4" w:space="0" w:color="{color}"/>
                <w:right w:val="single" w:sz="4" w:space="0" w:color="{color}"/>
                <w:insideH w:val="single" w:sz="4" w:space="0" w:color="{color}"/>
                <w:insideV w:val="single" w:sz="4" w:space="0" w:color="{color}"/>
            </w:tblBorders>
        ''')
        table._tbl.tblPr.append(borders)

    def add_callout(text, title=None):
        tbl = doc.add_table(rows=1, cols=1)
        tbl.alignment = WD_TABLE_ALIGNMENT.CENTER
        tbl.autofit = False
        cell = tbl.cell(0, 0)
        cell.width = Inches(6.8)
        set_cell_background(cell, "EFF6FF")
        set_cell_margins(cell, top=100, bottom=100, left=150, right=150)

        borders = parse_xml(f'''
            <w:tcBorders {nsdecls("w")}>
                <w:left w:val="single" w:sz="24" w:space="0" w:color="2563EB"/>
                <w:top w:val="none"/>
                <w:bottom w:val="none"/>
                <w:right w:val="none"/>
            </w:tcBorders>
        ''')
        cell._tc.get_or_add_tcPr().append(borders)

        p = cell.paragraphs[0]
        p.paragraph_format.space_before = Pt(0)
        p.paragraph_format.space_after = Pt(2)
        if title:
            r_title = p.add_run(f"📌 {title}\n")
            r_title.bold = True
            r_title.font.name = "Segoe UI"
            r_title.font.size = Pt(10)
            r_title.font.color.rgb = RGBColor(30, 58, 138)
        r_text = p.add_run(text)
        r_text.font.name = "Segoe UI"
        r_text.font.size = Pt(9.5)
        r_text.font.color.rgb = COLOR_TEXT

        p_sp = doc.add_paragraph()
        p_sp.paragraph_format.space_after = Pt(2)

    def add_code_block(code_str):
        tbl = doc.add_table(rows=1, cols=1)
        tbl.alignment = WD_TABLE_ALIGNMENT.CENTER
        tbl.autofit = False
        cell = tbl.cell(0, 0)
        cell.width = Inches(6.8)
        set_cell_background(cell, "F8FAFC")
        set_cell_margins(cell, top=80, bottom=80, left=120, right=120)

        borders = parse_xml(f'''
            <w:tcBorders {nsdecls("w")}>
                <w:left w:val="single" w:sz="12" w:space="0" w:color="94A3B8"/>
                <w:top w:val="single" w:sz="4" w:space="0" w:color="E2E8F0"/>
                <w:bottom w:val="single" w:sz="4" w:space="0" w:color="E2E8F0"/>
                <w:right w:val="single" w:sz="4" w:space="0" w:color="E2E8F0"/>
            </w:tcBorders>
        ''')
        cell._tc.get_or_add_tcPr().append(borders)

        p = cell.paragraphs[0]
        p.paragraph_format.space_before = Pt(0)
        p.paragraph_format.space_after = Pt(0)
        p.paragraph_format.line_spacing = 1.05
        r = p.add_run(code_str)
        r.font.name = "Consolas"
        r.font.size = Pt(8.5)
        r.font.color.rgb = RGBColor(15, 23, 42)

        p_sp = doc.add_paragraph()
        p_sp.paragraph_format.space_after = Pt(2)

    def add_h1(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(16)
        p.paragraph_format.space_after = Pt(4)
        p.paragraph_format.keep_with_next = True
        run = p.add_run(text)
        run.bold = True
        run.font.name = 'Segoe UI'
        run.font.size = Pt(14)
        run.font.color.rgb = COLOR_PRIMARY
        return p

    def add_h2(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(11)
        p.paragraph_format.space_after = Pt(3)
        p.paragraph_format.keep_with_next = True
        run = p.add_run(text)
        run.bold = True
        run.font.name = 'Segoe UI'
        run.font.size = Pt(11.5)
        run.font.color.rgb = COLOR_SECONDARY
        return p

    def add_h3(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(8)
        p.paragraph_format.space_after = Pt(2)
        p.paragraph_format.keep_with_next = True
        run = p.add_run(text)
        run.bold = True
        run.font.name = 'Segoe UI'
        run.font.size = Pt(10)
        run.font.color.rgb = RGBColor(51, 65, 85)
        return p

    def add_p(text, bold_prefix=None, italic=False):
        p = doc.add_paragraph()
        p.paragraph_format.space_after = Pt(3)
        p.paragraph_format.line_spacing = 1.15
        if bold_prefix:
            r_pre = p.add_run(bold_prefix)
            r_pre.bold = True
            r_pre.font.name = 'Segoe UI'
            r_pre.font.size = Pt(10)
            r_pre.font.color.rgb = COLOR_TEXT
        r = p.add_run(text)
        r.font.name = 'Segoe UI'
        r.font.size = Pt(10)
        r.font.italic = italic
        r.font.color.rgb = COLOR_TEXT
        return p

    def add_bullet(bold_prefix, text):
        p = doc.add_paragraph(style='List Bullet')
        p.paragraph_format.space_before = Pt(1)
        p.paragraph_format.space_after = Pt(2)
        p.paragraph_format.line_spacing = 1.15
        if bold_prefix:
            r_pre = p.add_run(bold_prefix)
            r_pre.bold = True
            r_pre.font.name = 'Segoe UI'
            r_pre.font.size = Pt(10)
            r_pre.font.color.rgb = COLOR_TEXT
        r = p.add_run(text)
        r.font.name = 'Segoe UI'
        r.font.size = Pt(10)
        r.font.color.rgb = COLOR_TEXT
        return p

    def render_table(headers, col_widths, data, align_cols=None):
        table = doc.add_table(rows=len(data) + 1, cols=len(headers))
        table.alignment = WD_TABLE_ALIGNMENT.CENTER
        table.autofit = False
        set_table_borders(table)

        # Header
        hdr_cells = table.rows[0].cells
        for i, h in enumerate(headers):
            hdr_cells[i].width = Inches(col_widths[i])
            set_cell_background(hdr_cells[i], "1F4E78")
            set_cell_margins(hdr_cells[i], top=90, bottom=90, left=90, right=90)
            p = hdr_cells[i].paragraphs[0]
            p.paragraph_format.space_before = Pt(0)
            p.paragraph_format.space_after = Pt(0)
            p.alignment = WD_ALIGN_PARAGRAPH.CENTER
            r = p.add_run(h)
            r.bold = True
            r.font.name = 'Segoe UI'
            r.font.size = Pt(9)
            r.font.color.rgb = RGBColor(255, 255, 255)

        # Rows
        for r_idx, row in enumerate(data):
            row_cells = table.rows[r_idx + 1].cells
            bg_col = "F8FAFC" if (r_idx % 2 == 1) else "FFFFFF"
            for c_idx, val in enumerate(row):
                row_cells[c_idx].width = Inches(col_widths[c_idx])
                set_cell_background(row_cells[c_idx], bg_col)
                set_cell_margins(row_cells[c_idx], top=70, bottom=70, left=90, right=90)
                p = row_cells[c_idx].paragraphs[0]
                p.paragraph_format.space_before = Pt(0)
                p.paragraph_format.space_after = Pt(0)
                p.paragraph_format.line_spacing = 1.15
                if align_cols and c_idx in align_cols:
                    p.alignment = align_cols[c_idx]
                else:
                    p.alignment = WD_ALIGN_PARAGRAPH.LEFT
                r = p.add_run(str(val))
                r.font.name = 'Segoe UI'
                r.font.size = Pt(8.5)
                r.font.color.rgb = COLOR_TEXT

        p_sp = doc.add_paragraph()
        p_sp.paragraph_format.space_after = Pt(3)

    # =========================================================================
    # TIÊU ĐỀ BÁO CÁO
    # =========================================================================
    p_title = doc.add_paragraph()
    p_title.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_title.paragraph_format.space_before = Pt(10)
    p_title.paragraph_format.space_after = Pt(4)
    r1 = p_title.add_run("BÁO CÁO BÀI TẬP LỚN MÔN LẬP TRÌNH CƠ SỞ DỮ LIỆU\n")
    r1.font.name = "Segoe UI"
    r1.font.size = Pt(11)
    r1.bold = True
    r1.font.color.rgb = COLOR_SECONDARY

    r2 = p_title.add_run("ĐỀ TÀI: HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ (WONDERLAND PARK)")
    r2.font.name = "Segoe UI"
    r2.font.size = Pt(15)
    r2.bold = True
    r2.font.color.rgb = COLOR_PRIMARY

    p_info = doc.add_paragraph()
    p_info.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_info.paragraph_format.space_before = Pt(2)
    p_info.paragraph_format.space_after = Pt(14)
    r_sub = p_info.add_run("Ngôn ngữ: C# (.NET 8 WinForms) | Hệ quản trị CSDL: Microsoft SQL Server | ORM: Entity Framework Core 8 | ClosedXML")
    r_sub.italic = True
    r_sub.font.name = "Segoe UI"
    r_sub.font.size = Pt(9.5)
    r_sub.font.color.rgb = COLOR_MUTED

    # =========================================================================
    # CHƯƠNG 1: KHẢO SÁT VÀ PHÂN TÍCH THIẾT KẾ HỆ THỐNG
    # =========================================================================
    add_h1("CHƯƠNG 1: KHẢO SÁT VÀ PHÂN TÍCH THIẾT KẾ HỆ THỐNG")

    add_h2("1. Khảo sát và phân tích bài toán (đề tài)")
    
    add_h3("a) Xác định yêu cầu bài toán:")
    add_p("Trong quá trình vận hành khu vui chơi giải trí phức hợp (quy mô trò chơi cảm giác mạnh, trò chơi gia đình, khu ẩm thực và quầy lưu niệm), hệ thống cần giải quyết các bài toán nghiệp vụ sau một cách tự động, chính xác và tức thời:")
    add_bullet("Quản lý thông tin khách hàng: ", "Lưu trữ hồ sơ khách hàng, tra cứu nhanh theo số điện thoại (10 số, duy nhất) để tích lũy lịch sử và áp dụng khuyến mãi.")
    add_bullet("Quản lý danh mục trò chơi & An toàn: ", "Theo dõi các loại trò chơi, giá vé lẻ từng trò, sức chứa tối đa mỗi lượt và quy định độ tuổi tối thiểu nhằm đảm bảo an toàn.")
    add_bullet("Quản lý dịch vụ & Kiểm soát tồn kho: ", "Quản lý mặt hàng ăn uống, cho thuê phụ kiện; tự động trừ tồn kho khi bán và cảnh báo chuyển trạng thái 'Hết hàng' khi tồn về 0.")
    add_bullet("Quản lý bảng giá vé: ", "Thiết lập các loại vé vào cổng (Vé ngày, Vé trẻ em, Vé người lớn, Combo gia đình, Vé VIP FastPass) và thời hạn áp dụng.")
    add_bullet("Bán vé và thanh toán tại quầy POS: ", "Tạo đơn nhanh, giỏ hàng trực quan, tính chiết khấu giảm giá (0-100%), hỗ trợ đa phương thức thanh toán (Tiền mặt, Chuyển khoản QR, Quẹt thẻ), tự động in hóa đơn và cập nhật kho trong Transaction.")
    add_bullet("Quản lý hóa đơn & Hủy đơn an toàn: ", "Tra cứu lịch sử giao dịch, hỗ trợ hủy hóa đơn có nhập lý do và tự động hoàn trả số lượng hàng tồn kho vào CSDL.")
    add_bullet("Báo cáo tài chính & Tích hợp Excel: ", "Thống kê doanh thu theo ngày/tháng/năm, phân tích tỷ trọng theo nhóm hàng, xuất và nhập dữ liệu hàng loạt qua file Excel chuẩn ClosedXML.")

    add_h3("b) Xác định tác nhân (Actors):")
    add_bullet("Quản trị viên (Admin): ", "Người có quyền hạn cao nhất: Xem Dashboard tổng quan; quản lý hồ sơ nhân viên; thiết lập bảng giá vé và danh mục trò chơi; xem báo cáo doanh thu tài chính; nhập/xuất Excel toàn bộ hệ thống.")
    add_bullet("Thu ngân / Nhân viên (Cashier/Staff): ", "Nhân viên làm việc tại quầy POS: Đăng nhập ca làm việc, thao tác tạo đơn bán vé và dịch vụ, tra cứu khách hàng, xem danh sách hóa đơn đã bán trong ca, in lại hóa đơn cho khách.")
    add_bullet("Khách hàng (Customer - Tác nhân ngoài): ", "Đối tượng đến vui chơi, mua vé vào cổng, gọi dịch vụ ăn uống và nhận phiếu thanh toán hóa đơn.")

    add_h2("2. Xây dựng cơ sở dữ liệu")
    add_h3("a) Sơ đồ cơ sở dữ liệu (Database Schema / ERD):")
    add_p("Cơ sở dữ liệu hệ thống được đặt tên là QuanLyKhuVuiChoi, gồm 8 bảng có quan hệ ràng buộc chặt chẽ:")
    add_p("• NhanVien (1) ── (1) TaiKhoan\n"
          "• NhanVien (1) ── (N) HoaDon (Thu ngân lập hóa đơn)\n"
          "• KhachHang (1) ── (N) HoaDon (Khách hàng thanh toán)\n"
          "• HoaDon (1) ── (N) ChiTietHoaDon (Hóa đơn có nhiều mặt hàng)\n"
          "• Ve / DichVu / TroChoi ── (Tham chiếu thông tin) ── ChiTietHoaDon", italic=True)

    add_h3("b) Chi tiết các bảng:")

    # Bảng 1: KhachHang
    add_p("Bảng 1: KhachHang (Lưu trữ hồ sơ thông tin khách hàng)", bold_prefix="• ")
    tb_kh = [
        ["MaKH", "Nvarchar(20)", "Chính (PK)", "Mã định danh khách hàng", "Tự sinh dạng KHxxx"],
        ["HoTen", "Nvarchar(100)", "", "Họ và tên khách hàng", "Bắt buộc"],
        ["NgaySinh", "Date", "", "Ngày tháng năm sinh", ""],
        ["GioiTinh", "Nvarchar(10)", "", "Giới tính (Nam/Nữ)", "Mặc định 'Nam'"],
        ["SoDienThoai", "Nvarchar(20)", "Unique", "Số điện thoại liên lạc", "10 chữ số, không trùng"],
        ["Email", "Nvarchar(100)", "", "Địa chỉ thư điện tử", "Đúng định dạng email"],
        ["DiaChi", "Nvarchar(200)", "", "Địa chỉ cư trú", ""],
        ["NgayDangKy", "DateTime", "", "Ngày giờ mở hồ sơ", "Mặc định DateTime.Now"]
    ]
    render_table(["Tên trường", "Kiểu dữ liệu", "Khóa", "Mô tả", "Ghi chú"], [1.3, 1.2, 1.1, 1.8, 1.4], tb_kh, {2: WD_ALIGN_PARAGRAPH.CENTER})

    # Bảng 2: NhanVien
    add_p("Bảng 2: NhanVien (Quản lý hồ sơ cán bộ nhân viên công viên)", bold_prefix="• ")
    tb_nv = [
        ["MaNV", "Nvarchar(20)", "Chính (PK)", "Mã định danh nhân viên", "Tự sinh dạng NVxxx"],
        ["HoTen", "Nvarchar(100)", "", "Họ và tên nhân viên", "Bắt buộc"],
        ["NgaySinh", "Date", "", "Ngày sinh", ""],
        ["GioiTinh", "Nvarchar(10)", "", "Giới tính", "Mặc định 'Nam'"],
        ["SoDienThoai", "Nvarchar(20)", "", "Số điện thoại", "10 số"],
        ["Email", "Nvarchar(100)", "", "Email liên lạc", ""],
        ["DiaChi", "Nvarchar(200)", "", "Địa chỉ nơi ở", ""],
        ["ChucVu", "Nvarchar(50)", "", "Vị trí công tác", "Quản lý, Thu ngân, Soát vé..."],
        ["NgayVaoLam", "Date", "", "Ngày bắt đầu làm việc", ""],
        ["TrangThai", "Nvarchar(50)", "", "Tình trạng làm việc", "Đang làm việc / Đã nghỉ"]
    ]
    render_table(["Tên trường", "Kiểu dữ liệu", "Khóa", "Mô tả", "Ghi chú"], [1.3, 1.2, 1.1, 1.8, 1.4], tb_nv, {2: WD_ALIGN_PARAGRAPH.CENTER})

    # Bảng 3: TaiKhoan
    add_p("Bảng 3: TaiKhoan (Tài khoản người dùng đăng nhập hệ thống)", bold_prefix="• ")
    tb_tk = [
        ["TenDangNhap", "Nvarchar(50)", "Chính (PK)", "Tên đăng nhập hệ thống", "Duy nhất"],
        ["MatKhau", "Nvarchar(100)", "", "Mật khẩu xác thực", "Bảo mật"],
        ["MaNV", "Nvarchar(20)", "Ngoại (FK)", "Mã nhân viên sở hữu", "Khóa ngoại tham chiếu NhanVien"],
        ["Quyen", "Nvarchar(20)", "", "Quyền hạn tài khoản", "Admin hoặc NhanVien"],
        ["TrangThai", "Bit", "", "Trạng thái kích hoạt", "1: Hoạt động, 0: Khóa"]
    ]
    render_table(["Tên trường", "Kiểu dữ liệu", "Khóa", "Mô tả", "Ghi chú"], [1.3, 1.2, 1.1, 1.8, 1.4], tb_tk, {2: WD_ALIGN_PARAGRAPH.CENTER})

    # Bảng 4: TroChoi
    add_p("Bảng 4: TroChoi (Danh mục trò chơi và quy chuẩn an toàn)", bold_prefix="• ")
    tb_tc = [
        ["MaTroChoi", "Nvarchar(20)", "Chính (PK)", "Mã định danh trò chơi", "Tự sinh dạng TCxxx"],
        ["TenTroChoi", "Nvarchar(100)", "", "Tên trò chơi", "Bắt buộc"],
        ["LoaiTroChoi", "Nvarchar(50)", "", "Phân loại trò chơi", "Cảm giác mạnh, Trẻ em, Gia đình..."],
        ["GiaVe", "Decimal(18,2)", "", "Giá vé tham gia trò chơi", ">= 0"],
        ["DoTuoiToiThieu", "Int", "", "Quy định độ tuổi nhỏ nhất", ">= 0"],
        ["SucChua", "Int", "", "Sức chứa tối đa (người/lượt)", "> 0"],
        ["TrangThai", "Nvarchar(50)", "", "Tình trạng vận hành", "Đang hoạt động / Bảo trì"],
        ["MoTa", "Nvarchar(255)", "", "Mô tả chi tiết trò chơi", ""]
    ]
    render_table(["Tên trường", "Kiểu dữ liệu", "Khóa", "Mô tả", "Ghi chú"], [1.3, 1.2, 1.1, 1.8, 1.4], tb_tc, {2: WD_ALIGN_PARAGRAPH.CENTER})

    # Bảng 5: DichVu
    add_p("Bảng 5: DichVu (Danh mục ẩm thực, giải khát và dịch vụ tiện ích)", bold_prefix="• ")
    tb_dv = [
        ["MaDichVu", "Nvarchar(20)", "Chính (PK)", "Mã định danh dịch vụ", "Tự sinh dạng DVxxx"],
        ["TenDichVu", "Nvarchar(100)", "", "Tên món/dịch vụ", "Bắt buộc"],
        ["LoaiDichVu", "Nvarchar(50)", "", "Phân loại dịch vụ", "Ẩm thực, Nước uống, Thuê đồ..."],
        ["DonGia", "Decimal(18,2)", "", "Đơn giá bán niêm yết", ">= 0"],
        ["SoLuongTon", "Int", "", "Số lượng hàng tồn kho", ">= 0, trừ khi bán, hoàn khi hủy"],
        ["TrangThai", "Nvarchar(50)", "", "Tình trạng phục vụ", "Đang phục vụ / Hết hàng"],
        ["MoTa", "Nvarchar(255)", "", "Mô tả chi tiết", ""]
    ]
    render_table(["Tên trường", "Kiểu dữ liệu", "Khóa", "Mô tả", "Ghi chú"], [1.3, 1.2, 1.1, 1.8, 1.4], tb_dv, {2: WD_ALIGN_PARAGRAPH.CENTER})

    # Bảng 6: Ve
    add_p("Bảng 6: Ve (Bảng giá các loại vé vào cổng và vé trọn gói)", bold_prefix="• ")
    tb_ve = [
        ["MaVe", "Nvarchar(20)", "Chính (PK)", "Mã loại vé", "Tự sinh dạng VExxx"],
        ["TenVe", "Nvarchar(100)", "", "Tên loại vé", "Bắt buộc"],
        ["LoaiVe", "Nvarchar(50)", "", "Phân loại vé", "Vé ngày, Trẻ em, Người lớn, VIP..."],
        ["GiaVe", "Decimal(18,2)", "", "Giá tiền niêm yết", ">= 0"],
        ["ThoiHan", "Nvarchar(50)", "", "Thời hạn hiệu lực của vé", "Trong ngày / 1 tháng..."],
        ["MoTa", "Nvarchar(255)", "", "Quyền lợi của vé", ""],
        ["TrangThai", "Nvarchar(50)", "", "Trạng thái áp dụng", "Đang áp dụng / Ngừng áp dụng"]
    ]
    render_table(["Tên trường", "Kiểu dữ liệu", "Khóa", "Mô tả", "Ghi chú"], [1.3, 1.2, 1.1, 1.8, 1.4], tb_ve, {2: WD_ALIGN_PARAGRAPH.CENTER})

    # Bảng 7: HoaDon
    add_p("Bảng 7: HoaDon (Hóa đơn bán hàng thanh toán)", bold_prefix="• ")
    tb_hd = [
        ["MaHoaDon", "Nvarchar(20)", "Chính (PK)", "Mã hóa đơn thanh toán", "Tự sinh dạng HDxxx"],
        ["MaKH", "Nvarchar(20)", "Ngoại (FK)", "Mã khách hàng mua vé", "Tham chiếu KhachHang, có thể NULL"],
        ["MaNV", "Nvarchar(20)", "Ngoại (FK)", "Mã thu ngân lập đơn", "Tham chiếu NhanVien"],
        ["NgayLap", "DateTime", "", "Ngày giờ thanh toán", "Mặc định DateTime.Now"],
        ["TongTien", "Decimal(18,2)", "", "Tổng tiền thực thu", ">= 0 (đã trừ chiết khấu)"],
        ["GiamGia", "Decimal(5,2)", "", "Tỷ lệ chiết khấu (%)", "Từ 0 đến 100"],
        ["PhuongThucThanhToan", "Nvarchar(50)", "", "Hình thức thanh toán", "Tiền mặt / Chuyển khoản / Thẻ"],
        ["TrangThai", "Nvarchar(50)", "", "Tình trạng hóa đơn", "Đã thanh toán / Đã hủy"],
        ["GhiChu", "Nvarchar(255)", "", "Ghi chú / Lý do hủy đơn", ""]
    ]
    render_table(["Tên trường", "Kiểu dữ liệu", "Khóa", "Mô tả", "Ghi chú"], [1.3, 1.2, 1.1, 1.8, 1.4], tb_hd, {2: WD_ALIGN_PARAGRAPH.CENTER})

    # Bảng 8: ChiTietHoaDon
    add_p("Bảng 8: ChiTietHoaDon (Chi tiết các mặt hàng trong hóa đơn)", bold_prefix="• ")
    tb_cthd = [
        ["MaCTHD", "Int", "Chính (PK)", "Mã tự tăng chi tiết hóa đơn", "Identity(1,1)"],
        ["MaHoaDon", "Nvarchar(20)", "Ngoại (FK)", "Mã hóa đơn chứa mặt hàng", "Tham chiếu HoaDon (Cascade)"],
        ["LoaiHang", "Nvarchar(20)", "", "Phân loại mặt hàng", "Ve / DichVu / TroChoi"],
        ["MaHang", "Nvarchar(20)", "", "Mã của mặt hàng tương ứng", "MaVe / MaDichVu / MaTroChoi"],
        ["TenHang", "Nvarchar(100)", "", "Tên mặt hàng tại thời điểm bán", ""],
        ["SoLuong", "Int", "", "Số lượng mua", "> 0"],
        ["DonGia", "Decimal(18,2)", "", "Đơn giá bán tại thời điểm lập", ">= 0"],
        ["ThanhTien", "Decimal(18,2)", "", "Thành tiền (Số lượng * Đơn giá)", ">= 0"]
    ]
    render_table(["Tên trường", "Kiểu dữ liệu", "Khóa", "Mô tả", "Ghi chú"], [1.3, 1.2, 1.1, 1.8, 1.4], tb_cthd, {2: WD_ALIGN_PARAGRAPH.CENTER})

    add_h2("3. Lập tiến độ thực hiện: Phân bổ nhân sự và thời gian thực hiện")
    add_p("Dự án được phân chia thành 5 giai đoạn (tuần) rõ ràng cho 4 thành viên theo 12 form chính:")

    tb_progress = [
        ["Tuần 1", "Khảo sát yêu cầu, phân tích bài toán, thiết kế ERD, viết script CSDL và Seed Data mẫu.", "Cả 4 thành viên"],
        ["Tuần 2", "Xây dựng Tầng DAL (EF Core DbContext), viết các Services nghiệp vụ và các DTOs dùng chung.", "Cả 4 thành viên"],
        ["Tuần 3", "Xây dựng Giao diện Presentation 12 Forms theo phân công chi tiết:\n• TV1: FormKhachHang, FormImportKhachHang, FormExportKhachHang\n• TV2: FormTroChoi, FormDichVu, FormImportExportTroChoiDichVu\n• TV3: FormNhanVien, FormVe, FormImportExportNhanVienVe\n• TV4: FormBanVe (POS), FormHoaDon, FormBaoCaoDoanhThu", "Phân chia độc lập theo Form"],
        ["Tuần 4", "Ghép nối hệ thống vào FormMain, tích hợp ClosedXML Import/Export Excel, tối ưu hóa Transaction và kiểm tra validation.", "Cả 4 thành viên"],
        ["Tuần 5", "Kiểm thử toàn diện, viết tài liệu đặc tả, đóng gói phần mềm và chuẩn bị báo cáo bảo vệ.", "Cả 4 thành viên"]
    ]
    render_table(["Thời gian", "Nội dung công việc thực hiện", "Phân bổ nhân sự"], [1.1, 4.2, 1.5], tb_progress, {0: WD_ALIGN_PARAGRAPH.CENTER})

    # =========================================================================
    # CHƯƠNG 2: CƠ SỞ NGHIÊN CỨU LÝ THUYẾT
    # =========================================================================
    add_h1("CHƯƠNG 2: CƠ SỞ NGHIÊN CỨU LÝ THUYẾT")

    add_h2("1. Ngôn ngữ truy vấn SQL (Structured Query Language)")
    
    # Insert
    add_p("a) INSERT (Thêm mới dữ liệu):", bold_prefix="")
    add_bullet("Mục đích: ", "Dùng để chèn thêm một hoặc nhiều dòng dữ liệu mới vào bảng trong CSDL.")
    add_bullet("Cú pháp: ", "INSERT INTO TenBang (Cot1, Cot2, ...) VALUES (GiaTri1, GiaTri2, ...);")
    add_bullet("Ví dụ minh họa: ", "Thêm một khách hàng mới vào bảng KhachHang:")
    add_code_block("INSERT INTO KhachHang (MaKH, HoTen, SoDienThoai, Email, NgayDangKy)\nVALUES ('KH021', N'Trần Minh Hoàng', '0987654321', 'hoang@gmail.com', GETDATE());")

    # Update
    add_p("b) UPDATE (Cập nhật dữ liệu):", bold_prefix="")
    add_bullet("Mục đích: ", "Dùng để sửa đổi dữ liệu của các bản ghi đã tồn tại thỏa mãn điều kiện nhất định.")
    add_bullet("Cú pháp: ", "UPDATE TenBang SET Cot1 = GiaTri1, Cot2 = GiaTri2 WHERE DieuKien;")
    add_bullet("Ví dụ minh họa: ", "Trừ 2 sản phẩm tồn kho của dịch vụ 'DV001' khi bán hàng:")
    add_code_block("UPDATE DichVu\nSET SoLuongTon = SoLuongTon - 2\nWHERE MaDichVu = 'DV001';")

    # Delete
    add_p("c) DELETE (Xóa dữ liệu):", bold_prefix="")
    add_bullet("Mục đích: ", "Dùng để xóa bỏ một hoặc nhiều bản ghi thỏa mãn điều kiện ra khỏi bảng dữ liệu.")
    add_bullet("Cú pháp: ", "DELETE FROM TenBang WHERE DieuKien;")
    add_bullet("Ví dụ minh họa: ", "Xóa một loại vé chưa từng được bán khỏi hệ thống:")
    add_code_block("DELETE FROM Ve\nWHERE MaVe = 'VE099' AND NOT EXISTS (\n    SELECT 1 FROM ChiTietHoaDon WHERE LoaiHang = 'Ve' AND MaHang = 'VE099'\n);")

    # Select
    add_p("d) SELECT (Truy vấn dữ liệu):", bold_prefix="")
    add_bullet("Mục đích: ", "Dùng để trích xuất dữ liệu từ một hoặc nhiều bảng theo các điều kiện lọc và sắp xếp.")
    add_bullet("Cú pháp: ", "SELECT Cot1, Cot2 FROM TenBang WHERE DieuKien ORDER BY CotSapXep;")
    add_bullet("Ví dụ minh họa: ", "Lấy danh sách các trò chơi đang hoạt động có giá vé trên 50.000 VNĐ:")
    add_code_block("SELECT MaTroChoi, TenTroChoi, LoaiTroChoi, GiaVe\nFROM TroChoi\nWHERE TrangThai = N'Đang hoạt động' AND GiaVe > 50000\nORDER BY GiaVe ASC;")

    # Stored Procedure
    add_p("e) STORED PROCEDURE (Thủ tục lưu trữ):", bold_prefix="")
    add_bullet("Mục đích: ", "Tập hợp các câu lệnh SQL được biên dịch sẵn và lưu trữ trực tiếp trên SQL Server. Giúp tái sử dụng mã nguồn, tăng tốc độ thực thi và nâng cao tính bảo mật (chống SQL Injection).")
    add_bullet("Cú pháp: ", "CREATE PROCEDURE TenThuTuc @ThamSo KieuDuLieu AS BEGIN ... END;")
    add_bullet("Ví dụ minh họa: ", "Thủ tục tính tổng doanh thu bán hàng theo ngày lập:")
    add_code_block("CREATE PROCEDURE sp_ThongKeDoanhThuNgay\n    @NgayLap DATE\nAS\nBEGIN\n    SELECT COUNT(MaHoaDon) AS TongSoDon, ISNULL(SUM(TongTien), 0) AS TongDoanhThu\n    FROM HoaDon\n    WHERE CAST(NgayLap AS DATE) = @NgayLap AND TrangThai = N'Đã thanh toán';\nEND;")

    add_h2("2. Ngôn ngữ lập trình C# và Công nghệ Windows Forms")
    
    add_h3("a) Form trong Windows Forms:")
    add_p("Form là thành phần giao diện đồ họa cốt lõi trong WinForms, đóng vai trò là cửa sổ làm việc chứa các điều khiển (Controls). Form cung cấp các thuộc tính quản lý vị trí, kích thước (`Size`, `StartPosition`), trạng thái đóng mở (`Show()`, `ShowDialog()`), và các sự kiện vòng đời (`Load`, `FormClosing`). Trong dự án, hệ thống áp dụng kỹ thuật 'Child Form Embedding' - nhúng các Form con vào Panel trung tâm của `FormMain` để tạo giao diện điều hướng mượt mà, không mở cửa sổ rời rạc.")

    add_h3("b) Các Control (điều khiển cơ bản) sử dụng trong dự án:")
    add_bullet("Label: ", "Hiển thị văn bản tĩnh, tiêu đề các mục nhập liệu, nhãn hiển thị tiền tệ và thống kê KPI.")
    add_bullet("TextBox: ", "Cho phép người dùng nhập chuỗi văn bản (Mã, Họ tên, SĐT, Email, Tìm kiếm). Hỗ trợ thuộc tính `ReadOnly` để khóa mã ID.")
    add_bullet("ComboBox: ", "Danh sách thả xuống cho phép chọn 1 giá trị: Loại trò chơi, Chức vụ nhân viên, Phương thức thanh toán.")
    add_bullet("ListBox: ", "Hiển thị danh sách nhiều dòng: Ứng dụng trong các Form Import Excel để liệt kê chi tiết các dòng dữ liệu bị lỗi.")
    add_bullet("Button: ", "Nút bấm kích hoạt các hành động người dùng: Thêm, Sửa, Xóa, Bán vé, Xuất Excel, Tải template.")
    add_bullet("RadioButton & CheckBox: ", "Cho phép chọn 1 trong nhiều phương án (chọn phạm vi xuất Excel) hoặc bật/tắt hiển thị mật khẩu.")
    add_bullet("DataGridView: ", "Bảng lưới hiển thị dữ liệu nhiều cột nhiều dòng: Cho phép chọn dòng (`SelectionMode = FullRowSelect`), tự co giãn cột (`AutoSizeColumnsMode = Fill`) và liên kết dữ liệu trực quan.")
    add_bullet("NumericUpDown & DateTimePicker: ", "Điều khiển nhập số lượng, đơn giá an toàn (không bị gõ nhầm chữ) và chọn ngày tháng chuẩn lịch.")

    add_h3("c) ADO.NET và Cơ chế kết nối dữ liệu:")
    add_bullet("SqlConnection: ", "Đối tượng quản lý kết nối vật lý với SQL Server qua chuỗi `ConnectionString`.")
    add_bullet("SqlCommand: ", "Đối tượng thực thi câu lệnh SQL (Text hoặc Stored Procedure) và nhận kết quả.")
    add_bullet("SqlDataAdapter: ", "Cầu nối trung gian giữa Database và DataSet/DataTable, hỗ trợ nạp dữ liệu bằng phương thức `Fill()` và đồng bộ thay đổi bằng `Update()`.")
    add_bullet("DataSet & DataTable: ", "Cấu trúc dữ liệu ngắt kết nối (Disconnected Architecture) lưu trữ dữ liệu dạng bảng trong bộ nhớ RAM.")
    add_bullet("Entity Framework Core 8 (Nâng cao): ", "Dự án sử dụng EF Core 8 - thế hệ ORM hiện đại nhất của Microsoft để ánh xạ trực tiếp các bảng CSDL thành các đối tượng C#, tự động quản lý Transaction và câu lệnh LINQ an toàn kiểu.")

    # =========================================================================
    # CHƯƠNG 3: XÂY DỰNG PHẦN MỀM
    # =========================================================================
    add_h1("CHƯƠNG 3: XÂY DỰNG PHẦN MỀM")

    add_h2("1. Sơ đồ chức năng của phần mềm (Functional Hierarchy)")
    add_p("Hệ thống phần mềm Quản lý Khu vui chơi Giải trí Wonderland Park được cấu trúc thành 5 phân hệ lớn gồm 12 Forms nghiệp vụ hoàn chỉnh:")
    add_p("HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ (WONDERLAND PARK)\n"
          "│\n"
          "├── 1. Phân hệ Xác thực & Điều hành: FormDangNhap, FormMain, FormDashboard\n"
          "├── 2. Phân hệ Quản lý Khách hàng: FormKhachHang, FormImportKhachHang, FormExportKhachHang\n"
          "├── 3. Phân hệ Trò chơi & Dịch vụ: FormTroChoi, FormDichVu, FormImportExportTroChoiDichVu\n"
          "├── 4. Phân hệ Nhân sự & Bảng giá Vé: FormNhanVien, FormVe, FormImportExportNhanVienVe\n"
          "└── 5. Phân hệ Bán vé POS & Doanh thu: FormBanVe (POS), FormHoaDon, FormBaoCaoDoanhThu", italic=True)

    add_h2("2. Một số chức năng chính")

    # Chức năng 1: FormKhachHang
    add_h3("2.1. Chức năng 1: Quản lý Khách hàng (FormKhachHang - Thành viên 1)")
    add_bullet("a) Mô tả: ", "Quản lý toàn bộ thông tin khách hàng đến vui chơi. Hỗ trợ tự sinh mã thông minh dạng KHxxx, kiểm tra số điện thoại bắt buộc đúng 10 số (0xxxxxxxxx), kiểm tra email hợp lệ, kiểm tra trùng số điện thoại trong CSDL. Chức năng xóa có kiểm tra toàn vẹn tham chiếu: nếu khách hàng đã có hóa đơn bán hàng thì từ chối xóa để bảo vệ chứng từ.")
    add_bullet("b) Giao diện: ", "Giao diện chia làm 2 phần: Bên trái là nhóm điều khiển nhập liệu (Mã KH, Họ tên, Ngày sinh, Giới tính, SĐT, Email, Địa chỉ) và các nút Thêm, Sửa, Xóa, Làm mới. Bên phải là thanh tìm kiếm đa chế độ (Contains, StartsWith, Exact) và DataGridView hiển thị danh sách khách hàng.")
    add_bullet("c) Code then chốt: ", "Đoạn mã C# thực hiện Thêm và Xóa khách hàng an toàn trong CustomerService.cs:")
    add_code_block(
        "// Thêm khách hàng mới kèm kiểm tra ràng buộc\n"
        "public (bool Success, string Message) Add(KhachHang khachHang)\n"
        "{\n"
        "    if (string.IsNullOrWhiteSpace(khachHang.HoTen))\n"
        "        return (false, \"Họ tên khách hàng không được để trống!\");\n"
        "    if (!ValidationHelper.IsValidPhoneNumber(khachHang.SoDienThoai))\n"
        "        return (false, \"Số điện thoại không hợp lệ (phải gồm 10 chữ số)!\");\n"
        "    if (_context.KhachHangs.Any(k => k.SoDienThoai == khachHang.SoDienThoai))\n"
        "        return (false, \"Số điện thoại này đã được sử dụng!\");\n"
        "\n"
        "    _context.KhachHangs.Add(khachHang);\n"
        "    _context.SaveChanges();\n"
        "    return (true, \"Thêm khách hàng thành công!\");\n"
        "}\n\n"
        "// Xóa an toàn có kiểm tra ràng buộc hóa đơn (FK Check)\n"
        "public (bool Success, string Message) Delete(string maKH)\n"
        "{\n"
        "    var existing = _context.KhachHangs.Include(k => k.HoaDons).FirstOrDefault(k => k.MaKH == maKH);\n"
        "    if (existing == null) return (false, \"Không tìm thấy khách hàng!\");\n"
        "    if (existing.HoaDons != null && existing.HoaDons.Any())\n"
        "        return (false, $\"Không thể xóa vì khách hàng này đang có {existing.HoaDons.Count} hóa đơn liên quan!\");\n"
        "\n"
        "    _context.KhachHangs.Remove(existing);\n"
        "    _context.SaveChanges();\n"
        "    return (true, \"Đã xóa khách hàng thành công!\");\n"
        "}"
    )

    # Chức năng 2: FormImportKhachHang
    add_h3("2.2. Chức năng 2: Nhập Khách hàng từ file Excel (FormImportKhachHang - Thành viên 1)")
    add_bullet("a) Mô tả: ", "Cho phép nạp danh sách hàng trăm khách hàng từ file Excel .xlsx bằng thư viện ClosedXML. Hệ thống duyệt từng dòng: Dòng hợp lệ hiển thị lên bảng Preview xem trước; Dòng lỗi (thiếu tên, sai SĐT, trùng mã) được liệt kê chi tiết trên ListBox. Người dùng bấm 'Xác nhận nạp vào CSDL' để lưu hàng loạt.")
    add_bullet("b) Giao diện: ", "Gồm nút 'Tải file mẫu Excel', nút 'Chọn file Excel (.xlsx)', DataGridView Preview dữ liệu hợp lệ, ListBox thông báo chi tiết lỗi từng hàng và nút 'Xác nhận nạp vào CSDL'.")
    add_bullet("c) Code then chốt: ", "Đoạn mã ClosedXML đọc file và validate từng dòng:")
    add_code_block(
        "public ImportResult<KhachHang> ReadCustomersFromExcel(string filePath)\n"
        "{\n"
        "    var result = new ImportResult<KhachHang>();\n"
        "    using var workbook = new XLWorkbook(filePath);\n"
        "    var worksheet = workbook.Worksheet(1);\n"
        "    var rows = worksheet.RowsUsed().Skip(1); // Bỏ qua dòng tiêu đề\n\n"
        "    int rowNum = 2;\n"
        "    foreach (var row in rows)\n"
        "    {\n"
        "        string hoTen = row.Cell(1).GetString().Trim();\n"
        "        string sdt = row.Cell(4).GetString().Trim();\n"
        "        if (string.IsNullOrEmpty(hoTen)) {\n"
        "            result.Errors.Add($\"Dòng {rowNum}: Họ tên khách hàng không được để trống.\");\n"
        "            continue;\n"
        "        }\n"
        "        if (!ValidationHelper.IsValidPhoneNumber(sdt)) {\n"
        "            result.Errors.Add($\"Dòng {rowNum}: Số điện thoại '{sdt}' không đúng 10 số.\");\n"
        "            continue;\n"
        "        }\n"
        "        result.ValidItems.Add(new KhachHang { HoTen = hoTen, SoDienThoai = sdt });\n"
        "        rowNum++;\n"
        "    }\n"
        "    return result;\n"
        "}"
    )

    # Chức năng 3: FormBanVe (POS)
    add_h3("2.3. Chức năng 3: Quầy Bán vé & Thanh toán POS (FormBanVe - Thành viên 4)")
    add_bullet("a) Mô tả: ", "Trọng tâm vận hành của công viên. Hỗ trợ chọn mặt hàng (Vé, Trò chơi, Dịch vụ ẩm thực) vào giỏ hàng, kiểm tra số lượng tồn kho tức thời (không cho bán vượt tồn), tính tổng tiền và chiết khấu giảm giá thời gian thực. Toàn bộ thao tác lưu hóa đơn, lưu chi tiết hóa đơn và trừ tồn kho dịch vụ được thực hiện trong Database Transaction an toàn tuyệt đối.")
    add_bullet("b) Giao diện: ", "Phía bên trái là danh mục mặt hàng dạng Thẻ Card trực quan chia theo Tab; Phía bên phải là Giỏ hàng hiển thị số lượng, nút tăng/giảm (+/-), ô chọn khách hàng, ô nhập % giảm giá, chọn phương thức thanh toán và nút lớn 'THANH TOÁN & IN HÓA ĐƠN'.")
    add_bullet("c) Code then chốt: ", "Đoạn mã C# thực hiện thanh toán trong Database Transaction tại InvoiceService.cs:")
    add_code_block(
        "public (bool Success, string Message, string? MaHoaDon) CreateInvoice(\n"
        "    string? maKH, string? maNV, List<CartItemDto> items, decimal giamGiaPercent, string phuongThuc)\n"
        "{\n"
        "    string maHD = GenerateNextId(); // Sinh mã dạng HD031\n"
        "    using var transaction = _context.Database.BeginTransaction();\n"
        "    try {\n"
        "        var hoaDon = new HoaDon {\n"
        "            MaHoaDon = maHD, MaKH = maKH, MaNV = maNV, NgayLap = DateTime.Now,\n"
        "            TongTien = items.Sum(i => i.ThanhTien) * (1 - giamGiaPercent / 100m),\n"
        "            GiamGia = giamGiaPercent, PhuongThucThanhToan = phuongThuc, TrangThai = \"Đã thanh toán\"\n"
        "        };\n"
        "        _context.HoaDons.Add(hoaDon);\n\n"
        "        foreach (var item in items) {\n"
        "            _context.ChiTietHoaDons.Add(new ChiTietHoaDon {\n"
        "                MaHoaDon = maHD, LoaiHang = item.LoaiHang, MaHang = item.MaHang,\n"
        "                TenHang = item.TenHang, SoLuong = item.SoLuong, DonGia = item.DonGia, ThanhTien = item.ThanhTien\n"
        "            });\n"
        "            // Tự động trừ tồn kho nếu là Dịch vụ ẩm thực/tiện ích\n"
        "            if (item.LoaiHang == \"DichVu\") {\n"
        "                var dv = _context.DichVus.First(d => d.MaDichVu == item.MaHang);\n"
        "                dv.SoLuongTon = Math.Max(0, dv.SoLuongTon - item.SoLuong);\n"
        "                if (dv.SoLuongTon == 0) dv.TrangThai = \"Hết hàng\";\n"
        "            }\n"
        "        }\n"
        "        _context.SaveChanges();\n"
        "        transaction.Commit(); // Hoàn tất an toàn\n"
        "        return (true, $\"Thanh toán thành công! Mã HĐ: {maHD}\", maHD);\n"
        "    } catch (Exception ex) {\n"
        "        transaction.Rollback(); // Hoàn tác nếu xảy ra sự cố\n"
        "        return (false, ex.Message, null);\n"
        "    }\n"
        "}"
    )

    # Chức năng 4: FormBaoCaoDoanhThu
    add_h3("2.4. Chức năng 4: Báo cáo Thống kê Doanh thu (FormBaoCaoDoanhThu - Thành viên 4)")
    add_bullet("a) Mô tả: ", "Cung cấp báo cáo tài chính cho Ban Quản lý: Lọc theo kỳ (Hôm nay, Tuần này, Tháng này, Năm nay, Tùy chọn); Hiển thị 3 thẻ KPI (Tổng doanh thu, Tổng số vé bán, Tổng số hóa đơn); Bảng 1 thống kê doanh thu theo từng mốc thời gian; Bảng 2 phân tích cơ cấu tỷ trọng doanh thu theo từng nhóm (Vé vào cổng, Dịch vụ ẩm thực, Trò chơi); Xuất báo cáo tài chính ra file Excel chuẩn ClosedXML.")
    add_bullet("b) Giao diện: ", "Gồm thanh lọc kỳ báo cáo, 3 thẻ Card KPI nổi bật, DataGridView doanh thu theo mốc thời gian, DataGridView phân tích cơ cấu mặt hàng và nút 'Xuất báo cáo Doanh thu ra Excel'.")
    add_bullet("c) Code then chốt: ", "Đoạn mã LINQ truy vấn doanh thu theo kỳ tại ReportService.cs:")
    add_code_block(
        "public List<RevenueReportDto> GetRevenueByPeriod(DateTime fromDate, DateTime toDate)\n"
        "{\n"
        "    return _context.HoaDons\n"
        "        .Where(h => h.TrangThai == \"Đã thanh toán\" &&\n"
        "                    h.NgayLap.Date >= fromDate.Date && h.NgayLap.Date <= toDate.Date)\n"
        "        .GroupBy(h => h.NgayLap.Date)\n"
        "        .Select(g => new RevenueReportDto {\n"
        "            MocThoiGian = g.Key.ToString(\"dd/MM/yyyy\"),\n"
        "            SoHoaDon = g.Count(),\n"
        "            SoVeBan = g.SelectMany(h => h.ChiTietHoaDons).Where(c => c.LoaiHang == \"Ve\").Sum(c => c.SoLuong),\n"
        "            TongDoanhThu = g.Sum(h => h.TongTien)\n"
        "        })\n"
        "        .OrderByDescending(r => r.MocThoiGian)\n"
        "        .ToList();\n"
        "}"
    )

    # =========================================================================
    # LƯU FILE
    # =========================================================================
    out_target = r"c:\Users\KIM THIEN\OneDrive\Desktop\Playground\CautrucBaoCaoBTL copy.docx"
    doc.save(out_target)
    print(f"Report completed and saved successfully to: {out_target}")

if __name__ == "__main__":
    generate_report()
