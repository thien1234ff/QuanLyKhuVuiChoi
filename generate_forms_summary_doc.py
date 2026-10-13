# -*- coding: utf-8 -*-
"""
Script to generate Tom_Tat_Kien_Truc_Cac_Form.docx
Summarizes all 18 Forms in QuanLyKhuVuiChoi project:
Parameters, Dependencies, Fields, and Main Methods.
"""

import os
import sys
import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.enum.table import WD_TABLE_ALIGNMENT
from docx.oxml import parse_xml
from docx.oxml.ns import nsdecls

def create_forms_document():
    doc = docx.Document()

    # Margin settings
    for section in doc.sections:
        section.top_margin = Inches(0.8)
        section.bottom_margin = Inches(0.8)
        section.left_margin = Inches(0.85)
        section.right_margin = Inches(0.85)

    COLOR_PRIMARY = RGBColor(31, 78, 120)     # #1F4E78 Deep Navy
    COLOR_SECONDARY = RGBColor(46, 117, 182)  # #2E75B6 Steel Blue
    COLOR_ACCENT = RGBColor(16, 185, 129)     # #10B981 Emerald
    COLOR_TEXT = RGBColor(30, 41, 59)         # #1E293B Dark Slate
    COLOR_MUTED = RGBColor(100, 116, 139)     # #64748B Muted Slate
    COLOR_WARN = RGBColor(217, 119, 6)        # #D97706 Amber

    style_normal = doc.styles['Normal']
    style_normal.font.name = 'Segoe UI'
    style_normal.font.size = Pt(10)
    style_normal.font.color.rgb = COLOR_TEXT
    style_normal.paragraph_format.line_spacing = 1.15
    style_normal.paragraph_format.space_after = Pt(3)

    def set_cell_background(cell, hex_color):
        shading = parse_xml(f'<w:shd {nsdecls("w")} w:fill="{hex_color}"/>')
        cell._tc.get_or_add_tcPr().append(shading)

    def set_cell_margins(cell, top=100, bottom=100, left=130, right=130):
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

    def add_callout(text, title=None, alert_type="note"):
        bg_color = "EFF6FF"
        border_color = "2563EB"
        icon = "📌"
        if alert_type == "success":
            bg_color = "ECFDF5"
            border_color = "10B981"
            icon = "✅"
        elif alert_type == "warning":
            bg_color = "FFFBEB"
            border_color = "F59E0B"
            icon = "⚠️"

        table = doc.add_table(rows=1, cols=1)
        table.alignment = WD_TABLE_ALIGNMENT.CENTER
        cell = table.cell(0, 0)
        cell.width = Inches(6.8)
        set_cell_background(cell, bg_color)
        set_cell_margins(cell, top=120, bottom=120, left=180, right=140)

        tcPr = cell._tc.get_or_add_tcPr()
        borders = parse_xml(f'''
            <w:tcBorders {nsdecls("w")}>
                <w:left w:val="single" w:sz="24" w:space="0" w:color="{border_color}"/>
                <w:top w:val="none"/>
                <w:right w:val="none"/>
                <w:bottom w:val="none"/>
            </w:tcBorders>
        ''')
        tcPr.append(borders)

        p = cell.paragraphs[0]
        p.paragraph_format.space_before = Pt(0)
        p.paragraph_format.space_after = Pt(2)
        if title:
            r_title = p.add_run(f"{icon} {title}\n")
            r_title.bold = True
            r_title.font.name = 'Segoe UI'
            r_title.font.size = Pt(10)
            r_title.font.color.rgb = RGBColor(15, 23, 42)
        r_text = p.add_run(text)
        r_text.font.name = 'Segoe UI'
        r_text.font.size = Pt(9.5)
        r_text.font.color.rgb = RGBColor(30, 41, 59)
        doc.add_paragraph().paragraph_format.space_after = Pt(3)

    def add_heading_1(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(14)
        p.paragraph_format.space_after = Pt(6)
        p.paragraph_format.keep_with_next = True
        run = p.add_run(text)
        run.bold = True
        run.font.name = 'Segoe UI'
        run.font.size = Pt(15)
        run.font.color.rgb = COLOR_PRIMARY
        return p

    def add_heading_2(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(10)
        p.paragraph_format.space_after = Pt(4)
        p.paragraph_format.keep_with_next = True
        run = p.add_run(text)
        run.bold = True
        run.font.name = 'Segoe UI'
        run.font.size = Pt(12)
        run.font.color.rgb = COLOR_SECONDARY
        return p

    def add_heading_3(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(6)
        p.paragraph_format.space_after = Pt(2)
        p.paragraph_format.keep_with_next = True
        run = p.add_run(text)
        run.bold = True
        run.font.name = 'Segoe UI'
        run.font.size = Pt(10.5)
        run.font.color.rgb = COLOR_TEXT
        return p

    # --- TITLE COVER HEADER ---
    title_p = doc.add_paragraph()
    title_p.paragraph_format.space_before = Pt(10)
    title_p.paragraph_format.space_after = Pt(2)
    title_p.alignment = WD_ALIGN_PARAGRAPH.CENTER
    r_main = title_p.add_run("TỔNG HỢP KIẾN TRÚC TẤT CẢ CÁC FORM\nHỆ THỐNG QUẢN LÝ KHU VUI CHƠI")
    r_main.bold = True
    r_main.font.name = 'Segoe UI'
    r_main.font.size = Pt(20)
    r_main.font.color.rgb = COLOR_PRIMARY

    sub_p = doc.add_paragraph()
    sub_p.paragraph_format.space_before = Pt(2)
    sub_p.paragraph_format.space_after = Pt(14)
    sub_p.alignment = WD_ALIGN_PARAGRAPH.CENTER
    r_sub = sub_p.add_run("Bản tài liệu chi tiết: Tham số khởi tạo, Biến phụ thuộc (Dependencies) và Danh sách hàm chính của 18 Form trong dự án")
    r_sub.font.name = 'Segoe UI'
    r_sub.font.size = Pt(11)
    r_sub.font.color.rgb = COLOR_MUTED

    add_callout(
        "Tài liệu này tổng hợp toàn bộ 18 Form giao diện trong ứng dụng Windows Forms Quản Lý Khu Vui Chơi (.NET 8 C#). "
        "Mỗi form được phân tích chi tiết gồm: (1) Tham số khởi tạo Constructor & Constructor Chaining, (2) Các biến trường phụ thuộc (Fields & Services), "
        "và (3) Bảng danh sách các hàm/phương thức xử lý nghiệp vụ chính cùng ý nghĩa chi tiết.",
        "MỤC ĐÍCH TÀI LIỆU",
        "note"
    )

    # --- PHẦN 1: BẢNG TỔNG QUAN 18 FORM ---
    add_heading_1("Phần 1: Bảng Thống Kê Tổng Quan 18 Form Giao Diện")

    forms_summary_data = [
        ("1", "FormDangNhap", "Auth", "Đăng nhập hệ thống, phân quyền người dùng", "context"),
        ("2", "FormDangKy", "Auth", "Đăng ký tài khoản nhân viên mới", "context"),
        ("3", "FormQuenMatKhau", "Auth", "Khôi phục / đặt lại mật khẩu nhân viên", "context"),
        ("4", "FormMain", "Main Shell", "Màn hình điều hướng chính, mở các tab nghiệp vụ", "context, excelService"),
        ("5", "FormBanVe", "Billing", "POS bán vé & dịch vụ, quản lý giỏ hàng, thanh toán", "context, excelService"),
        ("6", "FormHoaDon", "Billing", "Quản lý danh sách hóa đơn, lọc, hủy, xem chi tiết", "context, excelService"),
        ("7", "FormChiTietHoaDon", "Common", "Xem chi tiết một hóa đơn (danh sách vé & dịch vụ)", "hoaDon"),
        ("8", "FormKhachHang", "Customer", "Quản lý thông tin khách hàng, thẻ thành viên, tích điểm", "context, excelService"),
        ("9", "FormImportKhachHang", "Customer", "Nhập danh sách khách hàng hàng loạt từ file Excel", "context, excelService"),
        ("10", "FormExportKhachHang", "Customer", "Xuất danh sách khách hàng ra Excel theo tiêu chí lọc", "context, excelService"),
        ("11", "FormVe", "Ticket", "Quản lý danh mục vé, giá vé, áp dụng cho trò chơi", "context, excelService"),
        ("12", "FormDichVu", "Service", "Quản lý dịch vụ ăn uống, quà lưu niệm, quản lý kho", "context, excelService"),
        ("13", "FormTroChoi", "Game", "Quản lý danh mục trò chơi, độ tuổi, sức chứa", "context, excelService"),
        ("14", "FormNhanVien", "Employee", "Quản lý hồ sơ nhân viên, phân quyền, trạng thái làm việc", "context, excelService"),
        ("15", "FormDashboard", "Dashboard", "Bảng điều khiển tổng quan KPI, biểu đồ doanh thu", "context, excelService"),
        ("16", "FormBaoCaoDoanhThu", "Report", "Báo cáo doanh thu theo kỳ, cơ cấu danh mục, xuất Excel", "context, excelService"),
        ("17", "FormImportExportNhanVienVe", "Import/Export", "Trung tâm Nhập/Xuất Excel cho Nhân viên & Vé", "context, excelService"),
        ("18", "FormImportExportTroChoiDichVu", "Import/Export", "Trung tâm Nhập/Xuất Excel cho Trò chơi & Dịch vụ", "context, excelService"),
    ]

    t_sum = doc.add_table(rows=len(forms_summary_data) + 1, cols=5)
    t_sum.alignment = WD_TABLE_ALIGNMENT.CENTER
    set_table_borders(t_sum)

    col_widths = [Inches(0.4), Inches(1.8), Inches(1.0), Inches(2.4), Inches(1.2)]
    headers = ["STT", "Tên Form", "Phân Hệ", "Chức Năng Chính", "Tham Số Khởi Tạo"]

    for i, h in enumerate(headers):
        cell = t_sum.cell(0, i)
        cell.width = col_widths[i]
        set_cell_background(cell, "1F4E78")
        set_cell_margins(cell, top=120, bottom=120, left=100, right=100)
        p = cell.paragraphs[0]
        p.alignment = WD_ALIGN_PARAGRAPH.CENTER
        r = p.add_run(h)
        r.bold = True
        r.font.name = 'Segoe UI'
        r.font.size = Pt(9.5)
        r.font.color.rgb = RGBColor(255, 255, 255)

    for row_idx, data in enumerate(forms_summary_data, start=1):
        bg = "F8FAFC" if row_idx % 2 == 1 else "FFFFFF"
        for col_idx, val in enumerate(data):
            cell = t_sum.cell(row_idx, col_idx)
            cell.width = col_widths[col_idx]
            set_cell_background(cell, bg)
            set_cell_margins(cell, top=80, bottom=80, left=100, right=100)
            p = cell.paragraphs[0]
            if col_idx in (0, 2):
                p.alignment = WD_ALIGN_PARAGRAPH.CENTER
            else:
                p.alignment = WD_ALIGN_PARAGRAPH.LEFT
            r = p.add_run(val)
            r.font.name = 'Segoe UI'
            r.font.size = Pt(9)
            if col_idx == 1:
                r.bold = True
                r.font.color.rgb = COLOR_PRIMARY

    doc.add_paragraph().paragraph_format.space_after = Pt(8)

    # --- PHẦN 2: CHI TIẾT TỪNG PHÂN HỆ VÀ FORM ---
    add_heading_1("Phần 2: Chi Tiết Kiến Trúc, Tham Số & Các Hàm Chính Của Từng Form")

    # Helper function to render a Form block
    def render_form_section(form_name, file_path, module_name, desc, ctors, fields, methods):
        add_heading_2(f"{form_name} ({module_name})")
        
        p_info = doc.add_paragraph()
        p_info.paragraph_format.space_after = Pt(2)
        r_file = p_info.add_run(f"📁 Tệp nguồn: {file_path}\n")
        r_file.italic = True
        r_file.font.size = Pt(9)
        r_file.font.color.rgb = COLOR_MUTED
        
        r_desc = p_info.add_run(f"🎯 Mô tả chức năng: {desc}")
        r_desc.font.size = Pt(9.5)

        # 1. Constructors & Parameters
        add_heading_3("1. Phương Thức Khởi Tạo (Constructors) & Tham Số Đầu Vào:")
        for c in ctors:
            p_c = doc.add_paragraph()
            p_c.paragraph_format.left_indent = Inches(0.2)
            p_c.paragraph_format.space_after = Pt(2)
            r_c_code = p_c.add_run(f"• {c['sig']}: ")
            r_c_code.bold = True
            r_c_code.font.name = 'Consolas'
            r_c_code.font.size = Pt(9.5)
            r_c_code.font.color.rgb = COLOR_PRIMARY
            r_c_desc = p_c.add_run(c['desc'])
            r_c_desc.font.size = Pt(9.5)

        # 2. Fields / Dependencies
        add_heading_3("2. Biến Thành Viên Phụ Thuộc (Fields & Services):")
        for f in fields:
            p_f = doc.add_paragraph()
            p_f.paragraph_format.left_indent = Inches(0.2)
            p_f.paragraph_format.space_after = Pt(2)
            r_f_code = p_f.add_run(f"• {f['type']} {f['name']}: ")
            r_f_code.bold = True
            r_f_code.font.name = 'Consolas'
            r_f_code.font.size = Pt(9.5)
            r_f_code.font.color.rgb = COLOR_SECONDARY
            r_f_desc = p_f.add_run(f['desc'])
            r_f_desc.font.size = Pt(9.5)

        # 3. Main Methods Table
        add_heading_3("3. Danh Sách Các Hàm / Phương Thức Chính (Methods & Event Handlers):")
        
        t_m = doc.add_table(rows=len(methods) + 1, cols=3)
        t_m.alignment = WD_TABLE_ALIGNMENT.CENTER
        set_table_borders(t_m)

        m_widths = [Inches(2.2), Inches(1.3), Inches(3.3)]
        m_headers = ["Tên Phương Thức", "Tham Số / Kiểu Trả Về", "Nhiệm Vụ Nghiệp Vụ Chính"]

        for i, h in enumerate(m_headers):
            cell = t_m.cell(0, i)
            cell.width = m_widths[i]
            set_cell_background(cell, "2E75B6")
            set_cell_margins(cell, top=100, bottom=100, left=90, right=90)
            p = cell.paragraphs[0]
            p.alignment = WD_ALIGN_PARAGRAPH.CENTER
            r = p.add_run(h)
            r.bold = True
            r.font.name = 'Segoe UI'
            r.font.size = Pt(9)
            r.font.color.rgb = RGBColor(255, 255, 255)

        for row_idx, m in enumerate(methods, start=1):
            bg = "F8FAFC" if row_idx % 2 == 1 else "FFFFFF"
            cell0 = t_m.cell(row_idx, 0)
            cell0.width = m_widths[0]
            set_cell_background(cell0, bg)
            set_cell_margins(cell0, top=60, bottom=60, left=90, right=90)
            p0 = cell0.paragraphs[0]
            r0 = p0.add_run(m['name'])
            r0.bold = True
            r0.font.name = 'Consolas'
            r0.font.size = Pt(8.5)
            r0.font.color.rgb = COLOR_PRIMARY

            cell1 = t_m.cell(row_idx, 1)
            cell1.width = m_widths[1]
            set_cell_background(cell1, bg)
            set_cell_margins(cell1, top=60, bottom=60, left=90, right=90)
            p1 = cell1.paragraphs[0]
            r1 = p1.add_run(m['params'])
            r1.font.name = 'Segoe UI'
            r1.font.size = Pt(8.5)
            r1.font.color.rgb = COLOR_MUTED

            cell2 = t_m.cell(row_idx, 2)
            cell2.width = m_widths[2]
            set_cell_background(cell2, bg)
            set_cell_margins(cell2, top=60, bottom=60, left=90, right=90)
            p2 = cell2.paragraphs[0]
            r2 = p2.add_run(m['desc'])
            r2.font.name = 'Segoe UI'
            r2.font.size = Pt(8.5)

        doc.add_paragraph().paragraph_format.space_after = Pt(8)

    # ----------------------------------------------------
    # 1. FormDangNhap
    # ----------------------------------------------------
    render_form_section(
        form_name="1. FormDangNhap",
        file_path="QuanLyKhuVuiChoi\\Forms\\Auth\\FormDangNhap.cs",
        module_name="Phân hệ Xác Thực",
        desc="Cửa sổ đăng nhập hệ thống, kiểm tra xác thực người dùng, hỗ trợ ghi nhớ phiên đăng nhập và phân quyền.",
        ctors=[
            {"sig": "FormDangNhap()", "desc": "Constructor mặc định, tự động gọi chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext()) để tương thích với Windows Forms Designer."},
            {"sig": "FormDangNhap(QuanLyKhuVuiChoiDbContext context)", "desc": "Constructor có tham số context, khởi tạo AuthService(_context) và hàm InitializeComponent()."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL Entity Framework Core để truy vấn tài khoản."},
            {"type": "AuthService", "name": "_authService", "desc": "Tầng dịch vụ xử lý nghiệp vụ kiểm tra đăng nhập, mã hóa mật khẩu, kiểm tra quyền hạn."}
        ],
        methods=[
            {"name": "BtnDangNhap_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Lấy username/password, kiểm tra rỗng, gọi _authService.Login(). Nếu thành công gán DialogResult.OK và đóng Form, ngược lại báo lỗi."},
            {"name": "BtnThoat_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Gán DialogResult.Cancel và đóng Form đăng nhập để thoát toàn bộ ứng dụng."},
            {"name": "ChkHienMatKhau_CheckedChanged", "params": "(object? sender, EventArgs e) -> void", "desc": "Bật/tắt thuộc tính PasswordChar của TextBox mật khẩu để hiển thị hoặc ẩn mật khẩu."},
            {"name": "LnkDangKy_LinkClicked", "params": "(object sender, LinkLabel... e) -> void", "desc": "Khởi tạo và hiển thị FormDangKy dưới dạng ShowDialog() để nhân viên mới đăng ký tài khoản."},
            {"name": "LnkQuenMatKhau_LinkClicked", "params": "(object sender, LinkLabel... e) -> void", "desc": "Mở FormQuenMatKhau để hỗ trợ đặt lại mật khẩu cho tài khoản."}
        ]
    )

    # ----------------------------------------------------
    # 2. FormDangKy
    # ----------------------------------------------------
    render_form_section(
        form_name="2. FormDangKy",
        file_path="QuanLyKhuVuiChoi\\Forms\\Auth\\FormDangKy.cs",
        module_name="Phân hệ Xác Thực",
        desc="Màn hình đăng ký tài khoản nhân viên mới vào hệ thống.",
        ctors=[
            {"sig": "FormDangKy()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext())."},
            {"sig": "FormDangKy(QuanLyKhuVuiChoiDbContext context)", "desc": "Khởi tạo context và AuthService, thiết lập giao diện đăng ký."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL Entity Framework Core."},
            {"type": "AuthService", "name": "_authService", "desc": "Service xử lý kiểm tra trùng lặp tài khoản và lưu thông tin người dùng mới."}
        ],
        methods=[
            {"name": "BtnDangKy_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Kiểm tra dữ liệu hợp lệ (tên đăng nhập, mật khẩu, xác nhận mật khẩu, họ tên). Gọi AuthService.Register() để tạo tài khoản mới."},
            {"name": "BtnHuy_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Hủy bỏ thao tác đăng ký và đóng form."},
            {"name": "txtTenDangNhap_TextChanged", "params": "(object sender, EventArgs e) -> void", "desc": "Kiểm tra định dạng hoặc độ dài tên đăng nhập trong thời gian thực khi người dùng gõ."}
        ]
    )

    # ----------------------------------------------------
    # 3. FormQuenMatKhau
    # ----------------------------------------------------
    render_form_section(
        form_name="3. FormQuenMatKhau",
        file_path="QuanLyKhuVuiChoi\\Forms\\Auth\\FormQuenMatKhau.cs",
        module_name="Phân hệ Xác Thực",
        desc="Màn hình hỗ trợ nhân viên thiết lập lại mật khẩu khi quên.",
        ctors=[
            {"sig": "FormQuenMatKhau()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext())."},
            {"sig": "FormQuenMatKhau(QuanLyKhuVuiChoiDbContext context)", "desc": "Nhận context từ ngoài, khởi tạo _authService và nạp components."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Đối tượng DbContext dùng kết nối dữ liệu."},
            {"type": "AuthService", "name": "_authService", "desc": "Service kiểm tra thông tin định danh và cập nhật lại mật khẩu mới."}
        ],
        methods=[
            {"name": "BtnXacNhan_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xác minh thông tin tài khoản và số điện thoại/email nhân viên, gọi cập nhật mật khẩu mới."},
            {"name": "BtnHuy_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Đóng form và quay về màn hình đăng nhập."}
        ]
    )

    # ----------------------------------------------------
    # 4. FormMain
    # ----------------------------------------------------
    render_form_section(
        form_name="4. FormMain",
        file_path="QuanLyKhuVuiChoi\\Forms\\FormMain.cs",
        module_name="Giao Diện Chính (Main Shell)",
        desc="Màn hình khung điều phối chính của toàn bộ hệ thống, tích hợp sidebar điều hướng, thanh trạng thái người dùng đăng nhập và panel hiển thị form con (MDI/Tab container).",
        ctors=[
            {"sig": "FormMain()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormMain(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Nhận context và excelService, khởi tạo hệ thống, thiết lập giao diện và mở tab mặc định (Dashboard)."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL dùng chung để truyền xuống tất cả các tab con."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ Excel dùng chung để truyền xuống tất cả các tab con."},
            {"type": "Form?", "name": "_currentChildForm", "desc": "Lưu trữ form con đang được nhúng và hiển thị trong pnlContent."}
        ],
        methods=[
            {"name": "InitSystem", "params": "() -> void", "desc": "Khởi tạo hệ thống, kiểm tra quyền đăng nhập, tải thông tin người dùng và phân quyền hiển thị các nút chức năng (Admin vs Nhân viên)."},
            {"name": "UpdateUserStatus", "params": "() -> void", "desc": "Cập nhật nhãn thông tin nhân viên đang đăng nhập, vai trò (Admin / Thu ngân) và ngày giờ làm việc."},
            {"name": "OpenChildTab", "params": "(Form childForm, Button activeTabBtn) -> void", "desc": "Đóng form con cũ, nhúng form con mới vào Panel chính (TopLevel = false, Dock = Fill), gọi Show() và highlight nút tab tương ứng."},
            {"name": "HighlightTab", "params": "(Button activeBtn) -> void", "desc": "Thay đổi màu sắc nút tab được chọn và đưa các tab khác về màu mặc định."},
            {"name": "BtnTabDashboard_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormDashboard (bảng điều khiển tổng quan doanh thu & KPI)."},
            {"name": "BtnTabBanVe_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormBanVe (màn hình bán vé & dịch vụ POS)."},
            {"name": "BtnTabHoaDon_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormHoaDon (quản lý lịch sử hóa đơn)."},
            {"name": "BtnTabTroChoi_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormTroChoi (danh mục trò chơi)."},
            {"name": "BtnTabDichVu_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormDichVu (danh mục dịch vụ, ẩm thực)."},
            {"name": "BtnTabKhachHang_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormKhachHang (quản lý khách hàng)."},
            {"name": "BtnTabVe_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormVe (quản lý danh mục loại vé)."},
            {"name": "BtnTabNhanVien_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormNhanVien (quản lý nhân sự - yêu cầu quyền Admin)."},
            {"name": "BtnBaoCaoDoanhThu_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormBaoCaoDoanhThu (phân tích báo cáo chi tiết)."},
            {"name": "BtnImportExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở trung tâm Nhập/Xuất Excel dữ liệu hàng loạt."},
            {"name": "BtnDangXuat_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xóa thông tin đăng nhập hiện tại, đóng FormMain và quay lại FormDangNhap."},
            {"name": "BtnThoat_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xác nhận và đóng toàn bộ chương trình."}
        ]
    )

    # ----------------------------------------------------
    # 5. FormBanVe
    # ----------------------------------------------------
    render_form_section(
        form_name="5. FormBanVe",
        file_path="QuanLyKhuVuiChoi\\Forms\\Billing\\FormBanVe.cs",
        module_name="Phân hệ Bán Hàng & POS",
        desc="Màn hình bán vé và dịch vụ tại quầy: hiển thị danh sách vé/dịch vụ dạng card tương tác, quản lý giỏ hàng trực quan, tính toán giảm giá và xuất hóa đơn thanh toán.",
        ctors=[
            {"sig": "FormBanVe()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormBanVe(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo các Service phụ thuộc (InvoiceService, CustomerService, TicketService, ParkServiceService) và nạp dữ liệu từ CSDL."}
        ],
        fields=[
            {"type": "InvoiceService", "name": "_invoiceService", "desc": "Service xử lý tạo hóa đơn và chi tiết hóa đơn vào CSDL."},
            {"type": "CustomerService", "name": "_customerService", "desc": "Service lấy danh sách khách hàng để chọn người mua."},
            {"type": "TicketService", "name": "_ticketService", "desc": "Service lấy danh sách các loại vé còn hiệu lực bán."},
            {"type": "ParkServiceService", "name": "_parkService", "desc": "Service lấy danh sách dịch vụ ăn uống, vui chơi phụ trợ."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Instance ExcelService nhận vào từ constructor để khởi tạo các Service con."},
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "List<CartItemDto>", "name": "_cartItems", "desc": "Danh sách các mặt hàng (vé/dịch vụ) hiện đang có trong giỏ hàng."}
        ],
        methods=[
            {"name": "LoadDataFromDatabase", "params": "() -> void", "desc": "Tải danh sách khách hàng vào ComboBox, nạp danh sách Vé và Dịch Vụ từ database vào bộ nhớ đệm."},
            {"name": "BtnCategory_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Lọc hiển thị danh mục mặt hàng: 'Tất cả', 'Loại vé', hoặc 'Dịch vụ'."},
            {"name": "RenderTicketCards", "params": "() -> void", "desc": "Duyệt danh sách mặt hàng và vẽ động các thẻ (Cards) giao diện với hình ảnh, giá tiền và nút (+) thêm giỏ hàng."},
            {"name": "AddToCart", "params": "(string id, string name, string type, decimal price, int changeQty) -> void", "desc": "Thêm món vào giỏ hàng hoặc tăng giảm số lượng nếu món đã tồn tại trong giỏ."},
            {"name": "RefreshCartDisplay", "params": "() -> void", "desc": "Vẽ lại danh sách các món trong giỏ hàng trên giao diện, hiển thị tổng tiền từng món và nút xóa."},
            {"name": "UpdateTotals", "params": "() -> void", "desc": "Tính tổng tiền tạm tính, số tiền giảm giá (% chiết khấu), tổng tiền thanh toán cuối cùng và bật/tắt nút Thanh toán."},
            {"name": "NumDiscountPercent_ValueChanged", "params": "(object? sender, EventArgs e) -> void", "desc": "Sự kiện thay đổi tỷ lệ % giảm giá -> tự động tính lại tổng tiền thanh toán."},
            {"name": "LnkClearCart_LinkClicked", "params": "(object? sender, LinkLabel... e) -> void", "desc": "Xóa toàn bộ các món trong giỏ hàng hiện tại."},
            {"name": "BtnCheckout_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Kiểm tra giỏ hàng, xác nhận thông tin thanh toán, gọi _invoiceService.CreateInvoice() để lưu Hóa đơn & Chi tiết hóa đơn vào CSDL và xóa giỏ hàng."}
        ]
    )

    # ----------------------------------------------------
    # 6. FormHoaDon
    # ----------------------------------------------------
    render_form_section(
        form_name="6. FormHoaDon",
        file_path="QuanLyKhuVuiChoi\\Forms\\Billing\\FormHoaDon.cs",
        module_name="Phân hệ Bán Hàng & POS",
        desc="Quản lý lịch sử hóa đơn bán vé/dịch vụ: tìm kiếm, lọc theo ngày/khách hàng, xem chi tiết hóa đơn, hủy hóa đơn và xuất Excel.",
        ctors=[
            {"sig": "FormHoaDon()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormHoaDon(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo InvoiceService, CustomerService, cấu hình giao diện lọc và nạp dữ liệu."}
        ],
        fields=[
            {"type": "InvoiceService", "name": "_invoiceService", "desc": "Service truy vấn và quản lý hóa đơn."},
            {"type": "CustomerService", "name": "_customerService", "desc": "Service lấy danh mục khách hàng cho bộ lọc."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ xuất danh sách hóa đơn ra file Excel."},
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."}
        ],
        methods=[
            {"name": "LoadCustomersFilter", "params": "() -> void", "desc": "Nạp danh sách khách hàng vào ComboBox bộ lọc tìm kiếm."},
            {"name": "LoadData", "params": "() -> void", "desc": "Lấy toàn bộ danh sách hóa đơn từ CSDL (kèm thông tin Nhân viên, Khách hàng) và hiển thị lên lưới DataGridView."},
            {"name": "BtnLoc_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Kích hoạt lọc dữ liệu theo khoảng ngày (Từ ngày - Đến ngày), khách hàng và từ khóa mã hóa đơn."},
            {"name": "BtnReset_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xóa các điều kiện lọc và tải lại danh sách hóa đơn đầy đủ."},
            {"name": "ApplyFilter", "params": "() -> void", "desc": "Thực thi thuật toán lọc trên tập dữ liệu và cập nhật lại lưới hiển thị."},
            {"name": "BindGrid", "params": "(List<HoaDon> list) -> void", "desc": "Đổ danh sách hóa đơn vào DataGridView kèm định dạng cột ngày, tiền tệ."},
            {"name": "BtnXemChiTiet_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Lấy hóa đơn được chọn và mở FormChiTietHoaDon dưới dạng Modal Dialog."},
            {"name": "DgvHoaDon_CellDoubleClick", "params": "(object? sender, DataGridViewCell... e) -> void", "desc": "Bắt sự kiện click đúp chuột vào dòng hóa đơn để mở xem chi tiết nhanh."},
            {"name": "OpenSelectedDetail", "params": "() -> void", "desc": "Hàm dùng chung để kiểm tra dòng đang chọn và hiển thị FormChiTietHoaDon."},
            {"name": "BtnHuy_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xác nhận và gọi _invoiceService.CancelInvoice() để hủy hóa đơn được chọn."},
            {"name": "BtnXoa_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xóa vĩnh viễn hóa đơn (dành cho Admin khi hóa đơn đã bị hủy)."},
            {"name": "BtnExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xuất toàn bộ danh sách hóa đơn đang hiển thị trên lưới ra file Excel (.xlsx)."}
        ]
    )

    # ----------------------------------------------------
    # 7. FormChiTietHoaDon
    # ----------------------------------------------------
    render_form_section(
        form_name="7. FormChiTietHoaDon",
        file_path="QuanLyKhuVuiChoi\\Forms\\Common\\FormChiTietHoaDon.cs",
        module_name="Phân hệ Bán Hàng & POS",
        desc="Cửa sổ hiển thị thông tin chi tiết một hóa đơn cụ thể: danh sách các vé, dịch vụ đã mua, đơn giá, số lượng, thành tiền, chiết khấu và tổng thanh toán.",
        ctors=[
            {"sig": "FormChiTietHoaDon()", "desc": "Constructor mặc định không tham số (dành cho Designer)."},
            {"sig": "FormChiTietHoaDon(HoaDon hoaDon)", "desc": "Nhận đối tượng HoaDon cần hiển thị chi tiết và gọi nạp dữ liệu lên Form."}
        ],
        fields=[
            {"type": "HoaDon", "name": "_hoaDon", "desc": "Đối tượng hóa đơn chứa đầy đủ thông tin khách hàng, nhân viên và danh sách ChiTietHoaDons."}
        ],
        methods=[
            {"name": "LoadHoaDonDetail", "params": "() -> void", "desc": "Điền thông tin mã HĐ, ngày tạo, nhân viên lập, khách hàng và nạp các dòng chi tiết vào DataGridView."},
            {"name": "BtnClose_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Đóng cửa sổ chi tiết hóa đơn."}
        ]
    )

    # ----------------------------------------------------
    # 8. FormKhachHang
    # ----------------------------------------------------
    render_form_section(
        form_name="8. FormKhachHang",
        file_path="QuanLyKhuVuiChoi\\Forms\\Customer\\FormKhachHang.cs",
        module_name="Phân hệ Quản Lý Khách Hàng",
        desc="Quản lý hồ sơ khách hàng: Thêm, Sửa, Xóa, Tìm kiếm, sinh mã tự động, tích điểm thành viên và tích hợp Nhập/Xuất Excel.",
        ctors=[
            {"sig": "FormKhachHang()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo CustomerService và nạp dữ liệu khách hàng lên DataGridView."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ Excel phục vụ gọi FormImport/ExportKhachHang."},
            {"type": "CustomerService", "name": "_customerService", "desc": "Service xử lý nghiệp vụ khách hàng (CRUD, sinh mã, kiểm tra SĐT)."}
        ],
        methods=[
            {"name": "SetupDataGridViewColumns", "params": "() -> void", "desc": "Định cấu hình các cột hiển thị (Mã KH, Họ tên, SĐT, Email, Điểm tích lũy, Loại thành viên)."},
            {"name": "LoadData", "params": "() -> void", "desc": "Tải toàn bộ danh sách khách hàng từ CSDL và đổ vào DataGridView."},
            {"name": "ClearInputs", "params": "() -> void", "desc": "Xóa trắng các ô nhập liệu trên form và đưa trạng thái về thêm mới."},
            {"name": "BtnAutoId_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Tự động sinh mã khách hàng tiếp theo theo định dạng chuẩn (KHxxx)."},
            {"name": "BtnThem_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Kiểm tra dữ liệu nhập (họ tên, SĐT hợp lệ), gọi _customerService.AddCustomer() để lưu khách hàng mới."},
            {"name": "BtnSua_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Cập nhật thông tin khách hàng đang chọn vào CSDL."},
            {"name": "BtnXoa_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xác nhận và xóa khách hàng (kiểm tra ràng buộc lịch sử hóa đơn)."},
            {"name": "BtnLamMoi_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Làm mới dữ liệu và xóa ô nhập liệu."},
            {"name": "BtnMoImport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormImportKhachHang để nhập hàng loạt từ Excel."},
            {"name": "BtnMoExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở FormExportKhachHang để xuất danh sách ra Excel."},
            {"name": "BtnTimKiem_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Tìm kiếm khách hàng theo tên hoặc số điện thoại."},
            {"name": "BtnLamMoiTimKiem_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Hủy bỏ tìm kiếm và hiển thị lại toàn bộ khách hàng."},
            {"name": "DgvKhachHang_CellClick", "params": "(object? sender, DataGridViewCell... e) -> void", "desc": "Đổ thông tin dòng khách hàng đang chọn lên các ô nhập liệu tương ứng."}
        ]
    )

    # ----------------------------------------------------
    # 9. FormImportKhachHang
    # ----------------------------------------------------
    render_form_section(
        form_name="9. FormImportKhachHang",
        file_path="QuanLyKhuVuiChoi\\Forms\\Customer\\FormImportKhachHang.cs",
        module_name="Phân hệ Quản Lý Khách Hàng",
        desc="Form nhập danh sách khách hàng từ file Excel (.xlsx), hỗ trợ xem trước (Preview), kiểm tra lỗi dữ liệu (Validation) và tải file mẫu chuẩn.",
        ctors=[
            {"sig": "FormImportKhachHang()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormImportKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo CustomerService và IExcelService để đọc file Excel."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ đọc dữ liệu bảng từ file Excel."},
            {"type": "CustomerService", "name": "_customerService", "desc": "Service kiểm tra trùng mã/SĐT và lưu danh sách khách hàng."},
            {"type": "ImportResult<KhachHang>?", "name": "currentResult", "desc": "Kết quả phân tích file Excel (gồm danh sách hợp lệ và danh sách lỗi)."}
        ],
        methods=[
            {"name": "BtnBrowse_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở hộp thoại OpenFileDialog để chọn tệp Excel (.xlsx)."},
            {"name": "ProcessExcelFile", "params": "(string filePath) -> void", "desc": "Đọc dữ liệu từ file Excel, phân tích từng dòng, kiểm tra tính hợp lệ và hiển thị xem trước lên lưới."},
            {"name": "BtnConfirmImport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xác nhận và lưu toàn bộ danh sách khách hàng hợp lệ vào CSDL."},
            {"name": "BtnDownloadTemplate_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Tạo và tải về file Excel mẫu có định dạng chuẩn để người dùng nhập liệu."},
            {"name": "BtnClose_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Đóng cửa sổ nhập dữ liệu."}
        ]
    )

    # ----------------------------------------------------
    # 10. FormExportKhachHang
    # ----------------------------------------------------
    render_form_section(
        form_name="10. FormExportKhachHang",
        file_path="QuanLyKhuVuiChoi\\Forms\\Customer\\FormExportKhachHang.cs",
        module_name="Phân hệ Quản Lý Khách Hàng",
        desc="Form xuất dữ liệu khách hàng ra file Excel tùy biến theo điều kiện: Tất cả, Khách hàng VIP, hoặc theo khoảng điểm tích lũy.",
        ctors=[
            {"sig": "FormExportKhachHang()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormExportKhachHang(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Nhận context và excelService, nạp danh sách ban đầu để xem trước."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ xuất dữ liệu DataTable ra file Excel."},
            {"type": "CustomerService", "name": "_customerService", "desc": "Service truy vấn danh sách khách hàng theo điều kiện lọc."},
            {"type": "List<KhachHang>", "name": "_currentFilteredList", "desc": "Danh sách khách hàng sau khi lọc đang sẵn sàng xuất."}
        ],
        methods=[
            {"name": "RadOption_CheckedChanged", "params": "(object? sender, EventArgs e) -> void", "desc": "Thay đổi tùy chọn lọc xuất: Toàn bộ, Khách VIP, Điểm cao."},
            {"name": "BtnApplyFilter_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Áp dụng bộ lọc và làm mới lưới dữ liệu xem trước."},
            {"name": "RefreshPreview", "params": "() -> void", "desc": "Cập nhật dữ liệu xem trước trên DataGridView trước khi bấm nút Xuất."},
            {"name": "BtnExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Mở hộp thoại SaveFileDialog và xuất danh sách khách hàng ra file Excel."},
            {"name": "BtnClose_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Đóng cửa sổ xuất dữ liệu."}
        ]
    )

    # ----------------------------------------------------
    # 11. FormVe
    # ----------------------------------------------------
    render_form_section(
        form_name="11. FormVe",
        file_path="QuanLyKhuVuiChoi\\Forms\\Ticket\\FormVe.cs",
        module_name="Phân hệ Quản Lý Vé",
        desc="Quản lý danh mục các loại vé vui chơi: Vé trẻ em, Vé người lớn, Vé trọn gói, Vé theo trò chơi; quản lý đơn giá, quy định độ tuổi và trạng thái bán.",
        ctors=[
            {"sig": "FormVe()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormVe(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo TicketService và tải danh sách vé lên giao diện."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ xuất danh mục vé ra Excel."},
            {"type": "TicketService", "name": "_ticketService", "desc": "Service xử lý CRUD vé, sinh mã vé, kiểm tra liên kết trò chơi."}
        ],
        methods=[
            {"name": "LoadData", "params": "() -> void", "desc": "Lấy danh sách tất cả các vé từ CSDL và hiển thị lên DataGridView."},
            {"name": "BindGrid", "params": "(List<Ve> list) -> void", "desc": "Đổ danh sách vé vào DataGridView kèm định dạng đơn giá tiền tệ."},
            {"name": "Filter_Changed", "params": "(object? sender, EventArgs e) -> void", "desc": "Sự kiện khi người dùng thay đổi từ khóa tìm kiếm hoặc lọc loại vé."},
            {"name": "ApplyFilter", "params": "() -> void", "desc": "Lọc danh sách vé theo loại vé và từ khóa tìm kiếm."},
            {"name": "BtnReset_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xóa điều kiện tìm kiếm và tải lại toàn bộ vé."},
            {"name": "ClearInputs", "params": "() -> void", "desc": "Xóa trắng các ô nhập thông tin vé."},
            {"name": "BtnAutoId_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Tự động sinh mã loại vé tiếp theo (VE_xxx)."},
            {"name": "BtnThem_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Thêm loại vé mới vào CSDL sau khi kiểm tra hợp lệ tên vé và giá vé."},
            {"name": "BtnSua_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Cập nhật thông tin và đơn giá vé đang chọn."},
            {"name": "BtnXoa_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xóa loại vé khỏi hệ thống (kiểm tra ràng buộc hóa đơn đã bán)."},
            {"name": "DgvVe_CellClick", "params": "(object? sender, DataGridViewCell... e) -> void", "desc": "Hiển thị thông tin vé đang chọn lên form chỉnh sửa."},
            {"name": "BtnExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xuất bảng danh sách vé ra file Excel."}
        ]
    )

    # ----------------------------------------------------
    # 12. FormDichVu
    # ----------------------------------------------------
    render_form_section(
        form_name="12. FormDichVu",
        file_path="QuanLyKhuVuiChoi\\Forms\\Service\\FormDichVu.cs",
        module_name="Phân hệ Quản Lý Dịch Vụ & Kho",
        desc="Quản lý danh mục dịch vụ khu vui chơi: Đồ ăn, Nước uống, Quà lưu niệm, Thuê tủ đồ; quản lý số lượng tồn kho và cảnh báo hết hàng.",
        ctors=[
            {"sig": "FormDichVu()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormDichVu(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo ParkServiceService và tải dữ liệu dịch vụ."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ xuất danh mục dịch vụ ra Excel."},
            {"type": "ParkServiceService", "name": "_parkService", "desc": "Service xử lý CRUD dịch vụ, cập nhật tồn kho."}
        ],
        methods=[
            {"name": "LoadData", "params": "() -> void", "desc": "Tải toàn bộ danh sách dịch vụ và hiển thị lên lưới."},
            {"name": "BindGrid", "params": "(List<DichVu> list) -> void", "desc": "Đổ dữ liệu dịch vụ vào DataGridView, tô màu cảnh báo nếu số lượng tồn = 0."},
            {"name": "Filter_Changed", "params": "(object? sender, EventArgs e) -> void", "desc": "Bắt sự kiện gõ tìm kiếm hoặc chọn phân loại dịch vụ."},
            {"name": "ApplyFilter", "params": "() -> void", "desc": "Lọc danh sách dịch vụ theo tên và trạng thái còn hàng."},
            {"name": "BtnReset_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Làm mới lại dữ liệu ban đầu."},
            {"name": "ClearInputs", "params": "() -> void", "desc": "Xóa trắng các ô nhập liệu."},
            {"name": "BtnAutoId_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Tự động sinh mã dịch vụ mới (DVxxx)."},
            {"name": "BtnPlusStock_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Nút cộng nhanh số lượng tồn kho khi nhập thêm hàng."},
            {"name": "BtnThem_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Thêm dịch vụ mới vào CSDL."},
            {"name": "BtnSua_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Cập nhật tên, đơn giá và số lượng tồn của dịch vụ."},
            {"name": "BtnXoa_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xóa dịch vụ khỏi hệ thống."},
            {"name": "DgvDichVu_CellClick", "params": "(object? sender, DataGridViewCell... e) -> void", "desc": "Đổ thông tin dòng dịch vụ được click lên các ô nhập liệu."},
            {"name": "BtnExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xuất bảng danh sách dịch vụ ra file Excel."}
        ]
    )

    # ----------------------------------------------------
    # 13. FormTroChoi
    # ----------------------------------------------------
    render_form_section(
        form_name="13. FormTroChoi",
        file_path="QuanLyKhuVuiChoi\\Forms\\Game\\FormTroChoi.cs",
        module_name="Phân hệ Quản Lý Trò Chơi",
        desc="Quản lý danh mục các trò chơi trong công viên: Khu mạo hiểm, Khu thiếu nhi, Khu công nghệ; quy định độ tuổi tối thiểu, sức chứa và trạng thái hoạt động / bảo trì.",
        ctors=[
            {"sig": "FormTroChoi()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormTroChoi(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo GameService và nạp dữ liệu trò chơi."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ xuất danh mục trò chơi ra Excel."},
            {"type": "GameService", "name": "_gameService", "desc": "Service xử lý CRUD trò chơi, quản lý trạng thái bảo trì."}
        ],
        methods=[
            {"name": "LoadData", "params": "() -> void", "desc": "Tải danh mục tất cả trò chơi từ CSDL."},
            {"name": "BindGrid", "params": "(List<TroChoi> list) -> void", "desc": "Đổ danh sách trò chơi vào DataGridView kèm trạng thái hoạt động/bảo trì."},
            {"name": "Filter_Changed", "params": "(object? sender, EventArgs e) -> void", "desc": "Sự kiện lọc khi người dùng đổi từ khóa hoặc trạng thái trò chơi."},
            {"name": "ApplyFilter", "params": "() -> void", "desc": "Thực hiện lọc trò chơi theo tên, độ tuổi và tình trạng thiết bị."},
            {"name": "BtnReset_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xóa điều kiện tìm kiếm và tải lại danh mục."},
            {"name": "ClearInputs", "params": "() -> void", "desc": "Xóa trắng các trường nhập dữ liệu trò chơi."},
            {"name": "BtnAutoId_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Tự động sinh mã trò chơi mới (TCxxx)."},
            {"name": "BtnThem_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Thêm trò chơi mới vào hệ thống."},
            {"name": "BtnSua_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Cập nhật thông tin trò chơi, điều chỉnh trạng thái bảo trì."},
            {"name": "BtnXoa_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xóa trò chơi khỏi hệ thống."},
            {"name": "DgvTroChoi_CellClick", "params": "(object? sender, DataGridViewCell... e) -> void", "desc": "Đổ thông tin trò chơi đang chọn lên các ô nhập liệu."},
            {"name": "BtnExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xuất danh sách trò chơi ra file Excel."}
        ]
    )

    # ----------------------------------------------------
    # 14. FormNhanVien
    # ----------------------------------------------------
    render_form_section(
        form_name="14. FormNhanVien",
        file_path="QuanLyKhuVuiChoi\\Forms\\Employee\\FormNhanVien.cs",
        module_name="Phân hệ Quản Lý Nhân Sự",
        desc="Quản lý hồ sơ nhân sự và phân quyền: Quản lý thông tin cá nhân, chức vụ, số điện thoại, quyền truy cập (Admin / Nhân viên) và trạng thái hoạt động.",
        ctors=[
            {"sig": "FormNhanVien()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormNhanVien(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo EmployeeService và nạp dữ liệu nhân viên."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ xuất danh sách nhân viên ra Excel."},
            {"type": "EmployeeService", "name": "_employeeService", "desc": "Service xử lý CRUD nhân viên, quản lý quyền hạn tài khoản."}
        ],
        methods=[
            {"name": "SetupDataGridViewColumns", "params": "() -> void", "desc": "Cấu hình các cột hiển thị trên lưới nhân viên (Mã NV, Họ tên, Chức vụ, SĐT, Email, Quyền hạn, Trạng thái)."},
            {"name": "LoadData", "params": "() -> void", "desc": "Tải toàn bộ hồ sơ nhân viên từ CSDL."},
            {"name": "ClearInputs", "params": "() -> void", "desc": "Xóa các ô nhập liệu để chuẩn bị thêm mới nhân viên."},
            {"name": "BtnAutoId_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Tự động sinh mã nhân viên tiếp theo (NVxxx)."},
            {"name": "BtnThem_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Thêm nhân viên mới vào CSDL kèm tài khoản mặc định."},
            {"name": "BtnSua_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Cập nhật hồ sơ, chức vụ và phân quyền của nhân viên đang chọn."},
            {"name": "BtnXoa_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Khóa hoặc xóa nhân viên khỏi hệ thống."},
            {"name": "BtnLamMoi_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Làm mới dữ liệu nhân viên."},
            {"name": "BtnLoc_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Lọc nhân viên theo chức vụ hoặc quyền hạn (Admin / Thu ngân)."},
            {"name": "BtnLamMoiLoc_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Hủy bộ lọc tìm kiếm nhân sự."},
            {"name": "DgvNhanVien_CellClick", "params": "(object? sender, DataGridViewCell... e) -> void", "desc": "Đổ thông tin nhân viên được chọn lên form chi tiết."},
            {"name": "BtnExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xuất bảng danh sách nhân viên ra file Excel."}
        ]
    )

    # ----------------------------------------------------
    # 15. FormDashboard
    # ----------------------------------------------------
    render_form_section(
        form_name="15. FormDashboard",
        file_path="QuanLyKhuVuiChoi\\Forms\\Dashboard\\FormDashboard.cs",
        module_name="Phân hệ Bảng Điều Khiển & Thống Kê",
        desc="Bảng điều khiển tổng quan (Executive Dashboard): Thống kê doanh thu hôm nay, tổng số vé bán ra, số khách hàng mới, vẽ biểu đồ doanh thu trực quan bằng GDI+ và danh sách top trò chơi/dịch vụ ăn khách.",
        ctors=[
            {"sig": "FormDashboard()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormDashboard(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo ReportService, InvoiceService và nạp các chỉ số KPI tổng hợp."}
        ],
        fields=[
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ Excel (dự phòng)."},
            {"type": "ReportService", "name": "_reportService", "desc": "Service tổng hợp chỉ số thống kê, doanh thu và dữ liệu biểu đồ."},
            {"type": "InvoiceService", "name": "_invoiceService", "desc": "Service lấy danh sách các giao dịch bán vé gần đây nhất."}
        ],
        methods=[
            {"name": "SetupRecentTransactionsColumns", "params": "() -> void", "desc": "Cấu hình cột cho lưới hiển thị các đơn hàng bán vé gần đây nhất."},
            {"name": "LoadDashboardData", "params": "() -> void", "desc": "Gọi _reportService để tính toán doanh thu hôm nay, tăng trưởng so với hôm qua, tổng vé bán, và nạp các giao dịch gần đây."},
            {"name": "PaintRevenueChart", "params": "(object? sender, PaintEventArgs e) -> void", "desc": "Sử dụng GDI+ để vẽ biểu đồ đường/cột doanh thu tuần qua trên Panel với hiệu ứng gradient và nhãn giá trị."},
            {"name": "BuildAttractionsList", "params": "(List<TopItemDto> topItems) -> void", "desc": "Tạo danh sách các trò chơi và dịch vụ được yêu thích nhất kèm thanh tiến trình (progress bar) trực quan."}
        ]
    )

    # ----------------------------------------------------
    # 16. FormBaoCaoDoanhThu
    # ----------------------------------------------------
    render_form_section(
        form_name="16. FormBaoCaoDoanhThu",
        file_path="QuanLyKhuVuiChoi\\Forms\\Report\\FormBaoCaoDoanhThu.cs",
        module_name="Phân hệ Báo Cáo Doanh Thu",
        desc="Phân tích báo cáo tài chính chuyên sâu: Xem báo cáo theo Ngày, Tuần, Tháng, Quý, Năm; cơ cấu doanh thu theo từng danh mục và xuất báo cáo tài chính ra Excel.",
        ctors=[
            {"sig": "FormBaoCaoDoanhThu()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormBaoCaoDoanhThu(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo ReportService và nạp dữ liệu kỳ báo cáo mặc định."}
        ],
        fields=[
            {"type": "ReportService", "name": "_reportService", "desc": "Service tính toán số liệu doanh thu, tổng chiết khấu, tỷ trọng danh mục."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ xuất bảng báo cáo ra file Excel hoàn chỉnh."},
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."}
        ],
        methods=[
            {"name": "CboKyBaoCao_SelectedIndexChanged", "params": "(object? sender, EventArgs e) -> void", "desc": "Xử lý khi người dùng chọn kỳ báo cáo (Hôm nay, 7 ngày qua, Tháng này, Quý này, Năm nay) -> cập nhật DateTimePicker tương ứng."},
            {"name": "CboCategoryType_SelectedIndexChanged", "params": "(object? sender, EventArgs e) -> void", "desc": "Thay đổi góc nhìn phân tích (Cơ cấu theo Loại vé hay Cơ cấu theo Dịch vụ ăn uống)."},
            {"name": "BtnBaoCao_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Thực thi tính toán và nạp lại toàn bộ dữ liệu báo cáo theo khoảng thời gian đã chọn."},
            {"name": "LoadReportData", "params": "() -> void", "desc": "Gọi Service lấy tổng doanh thu, số lượng đơn hàng, giá trị trung bình/đơn và hiển thị lên các thẻ KPI."},
            {"name": "LoadCategoryData", "params": "() -> void", "desc": "Đổ số liệu chi tiết phân bổ doanh thu theo từng mặt hàng vào DataGridView."},
            {"name": "BtnExport_Click", "params": "(object? sender, EventArgs e) -> void", "desc": "Xuất toàn bộ bảng báo cáo doanh thu và cơ cấu chi tiết ra file Excel chuyên nghiệp kèm tiêu đề và thời gian lập."}
        ]
    )

    # ----------------------------------------------------
    # 17. FormImportExportNhanVienVe
    # ----------------------------------------------------
    render_form_section(
        form_name="17. FormImportExportNhanVienVe",
        file_path="QuanLyKhuVuiChoi\\Forms\\EmployeeTicketImportExport\\FormImportExportNhanVienVe.cs",
        module_name="Phân hệ Nhập / Xuất Excel Nâng Cao",
        desc="Trung tâm Nhập và Xuất dữ liệu Excel tích hợp 2 tab: Tab Nhân Viên và Tab Vé, hỗ trợ kiểm tra lỗi dòng, xem trước và tải mẫu template.",
        ctors=[
            {"sig": "FormImportExportNhanVienVe()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormImportExportNhanVienVe(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo EmployeeService, TicketService, dựng giao diện TabControl bằng mã lệnh C#."}
        ],
        fields=[
            {"type": "EmployeeService", "name": "_employeeService", "desc": "Service xử lý đọc, kiểm tra và lưu danh sách Nhân viên từ Excel."},
            {"type": "TicketService", "name": "_ticketService", "desc": "Service xử lý đọc, kiểm tra và lưu danh sách Vé từ Excel."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ tạo file template và chuyển đổi DataTable <-> Excel."},
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."}
        ],
        methods=[
            {"name": "InitializeComponent", "params": "() -> void", "desc": "Dựng động TabControl gồm 2 Tab: 'Nhân Viên' và 'Vé Bán', cấu hình các nút chọn file, xem trước và lưu."},
            {"name": "BuildEmployeeTab", "params": "() -> void", "desc": "Khởi tạo các control cho Tab Quản lý Nhập/Xuất Nhân Viên."},
            {"name": "BuildTicketTab", "params": "() -> void", "desc": "Khởi tạo các control cho Tab Quản lý Nhập/Xuất Vé."},
            {"name": "BrowseEmployeeFile", "params": "() -> void", "desc": "Chọn file Excel nhân viên, gọi Service kiểm tra tính hợp lệ và hiển thị xem trước lên lưới."},
            {"name": "SaveEmployeeImport", "params": "() -> void", "desc": "Lưu các bản ghi nhân viên hợp lệ vào CSDL."},
            {"name": "ExportEmployeeToExcel", "params": "() -> void", "desc": "Xuất toàn bộ danh sách nhân viên trong CSDL ra file Excel."},
            {"name": "DownloadEmployeeTemplate", "params": "() -> void", "desc": "Tải về file Excel mẫu định dạng chuẩn cho Nhân viên."},
            {"name": "BrowseTicketFile", "params": "() -> void", "desc": "Chọn file Excel vé, kiểm tra dữ liệu và hiển thị lên lưới."},
            {"name": "SaveTicketImport", "params": "() -> void", "desc": "Lưu các bản ghi vé hợp lệ vào CSDL."},
            {"name": "ExportTicketToExcel", "params": "() -> void", "desc": "Xuất toàn bộ danh mục vé ra file Excel."},
            {"name": "DownloadTicketTemplate", "params": "() -> void", "desc": "Tải về file Excel mẫu định dạng chuẩn cho Vé."}
        ]
    )

    # ----------------------------------------------------
    # 18. FormImportExportTroChoiDichVu
    # ----------------------------------------------------
    render_form_section(
        form_name="18. FormImportExportTroChoiDichVu",
        file_path="QuanLyKhuVuiChoi\\Forms\\GameServiceImportExport\\FormImportExportTroChoiDichVu.cs",
        module_name="Phân hệ Nhập / Xuất Excel Nâng Cao",
        desc="Trung tâm Nhập và Xuất dữ liệu Excel tích hợp 2 tab: Tab Trò Chơi và Tab Dịch Vụ, hỗ trợ đồng bộ danh mục từ Excel vào hệ thống.",
        ctors=[
            {"sig": "FormImportExportTroChoiDichVu()", "desc": "Constructor mặc định, chuyển tiếp : this(new QuanLyKhuVuiChoiDbContext(), new ExcelService())."},
            {"sig": "FormImportExportTroChoiDichVu(QuanLyKhuVuiChoiDbContext context, IExcelService excelService)", "desc": "Khởi tạo GameService, ParkServiceService, dựng giao diện TabControl bằng mã lệnh."}
        ],
        fields=[
            {"type": "GameService", "name": "_gameService", "desc": "Service xử lý đọc, kiểm tra và lưu danh mục Trò chơi từ Excel."},
            {"type": "ParkServiceService", "name": "_parkService", "desc": "Service xử lý đọc, kiểm tra và lưu danh mục Dịch vụ từ Excel."},
            {"type": "IExcelService", "name": "_excelService", "desc": "Dịch vụ tạo file template và xử lý file Excel."},
            {"type": "QuanLyKhuVuiChoiDbContext", "name": "_context", "desc": "Phiên làm việc CSDL."}
        ],
        methods=[
            {"name": "InitializeComponent", "params": "() -> void", "desc": "Dựng động TabControl gồm 2 Tab: 'Trò Chơi' và 'Dịch Vụ' với các nút duyệt file, xuất dữ liệu và tải mẫu."},
            {"name": "BuildGameTab", "params": "() -> void", "desc": "Khởi tạo các control cho Tab Quản lý Nhập/Xuất Trò Chơi."},
            {"name": "BuildServiceTab", "params": "() -> void", "desc": "Khởi tạo các control cho Tab Quản lý Nhập/Xuất Dịch Vụ."},
            {"name": "BrowseGameFile", "params": "() -> void", "desc": "Chọn file Excel trò chơi, kiểm tra dữ liệu và hiển thị lên lưới xem trước."},
            {"name": "SaveGameImport", "params": "() -> void", "desc": "Lưu các bản ghi trò chơi hợp lệ vào CSDL."},
            {"name": "ExportGameToExcel", "params": "() -> void", "desc": "Xuất toàn bộ danh sách trò chơi ra file Excel."},
            {"name": "DownloadGameTemplate", "params": "() -> void", "desc": "Tải về file Excel mẫu định dạng chuẩn cho Trò chơi."},
            {"name": "BrowseServiceFile", "params": "() -> void", "desc": "Chọn file Excel dịch vụ, kiểm tra dữ liệu tồn kho và hiển thị lên lưới."},
            {"name": "SaveServiceImport", "params": "() -> void", "desc": "Lưu các bản ghi dịch vụ hợp lệ vào CSDL."},
            {"name": "ExportServiceToExcel", "params": "() -> void", "desc": "Xuất toàn bộ danh mục dịch vụ ra file Excel."},
            {"name": "DownloadServiceTemplate", "params": "() -> void", "desc": "Tải về file Excel mẫu định dạng chuẩn cho Dịch vụ."}
        ]
    )

    # --- PHẦN 3: ĐÁNH GIÁ KIẾN TRÚC VÀ CÁC MẪU THIẾT KẾ ÁP DỤNG ---
    add_heading_1("Phần 3: Tổng Kết Các Mẫu Thiết Kế Áp Dụng Cho Các Form")

    design_patterns_data = [
        ("Constructor Chaining", "Áp dụng trên 100% các Form", "Tất cả các form đều có constructor không đối số gọi chuyển tiếp : this(...) sang constructor nhận DbContext và IExcelService. Giúp form vừa kéo thả được trong WinForms Designer, vừa hỗ trợ Dependency Injection."),
        ("Dependency Injection (DI)", "Áp dụng qua Constructor Injection", "Các form không tự khởi tạo database tùy tiện trong code sự kiện mà nhận DbContext và Service từ FormMain truyền xuống, đảm bảo tính đồng bộ dữ liệu và dễ dàng kiểm thử."),
        ("Phân tầng UI - Service - DAL", "Tách biệt rõ ràng 3 tầng", "Các Form chỉ đảm nhận việc bắt sự kiện giao diện và hiển thị dữ liệu (UI). Toàn bộ nghiệp vụ (tính tiền, kiểm tra ràng buộc, lưu trữ) đều được ủy thác cho các Service (InvoiceService, TicketService, CustomerService,...)."),
        ("Single Responsibility (SRP)", "Mỗi Form một nhiệm vụ cụ thể", "FormBanVe chuyên trách POS bán hàng, FormHoaDon quản lý lịch sử đơn, các form Import/Export tách riêng giúp code gọn gàng, dễ bảo trì và mở rộng.")
    ]

    t_dp = doc.add_table(rows=len(design_patterns_data) + 1, cols=3)
    t_dp.alignment = WD_TABLE_ALIGNMENT.CENTER
    set_table_borders(t_dp)

    dp_widths = [Inches(1.8), Inches(1.8), Inches(3.2)]
    dp_headers = ["Mẫu Thiết Kế (Pattern)", "Phạm Vi Áp Dụng", "Lợi Ích Thực Tế Trong Dự Án"]

    for i, h in enumerate(dp_headers):
        cell = t_dp.cell(0, i)
        cell.width = dp_widths[i]
        set_cell_background(cell, "1F4E78")
        set_cell_margins(cell, top=100, bottom=100, left=90, right=90)
        p = cell.paragraphs[0]
        p.alignment = WD_ALIGN_PARAGRAPH.CENTER
        r = p.add_run(h)
        r.bold = True
        r.font.name = 'Segoe UI'
        r.font.size = Pt(9)
        r.font.color.rgb = RGBColor(255, 255, 255)

    for row_idx, (p_name, scope, benefit) in enumerate(design_patterns_data, start=1):
        bg = "F8FAFC" if row_idx % 2 == 1 else "FFFFFF"
        cell0 = t_dp.cell(row_idx, 0)
        cell0.width = dp_widths[0]
        set_cell_background(cell0, bg)
        set_cell_margins(cell0, top=70, bottom=70, left=90, right=90)
        p0 = cell0.paragraphs[0]
        r0 = p0.add_run(p_name)
        r0.bold = True
        r0.font.name = 'Segoe UI'
        r0.font.size = Pt(9)
        r0.font.color.rgb = COLOR_PRIMARY

        cell1 = t_dp.cell(row_idx, 1)
        cell1.width = dp_widths[1]
        set_cell_background(cell1, bg)
        set_cell_margins(cell1, top=70, bottom=70, left=90, right=90)
        p1 = cell1.paragraphs[0]
        r1 = p1.add_run(scope)
        r1.font.name = 'Segoe UI'
        r1.font.size = Pt(8.5)
        r1.font.color.rgb = COLOR_MUTED

        cell2 = t_dp.cell(row_idx, 2)
        cell2.width = dp_widths[2]
        set_cell_background(cell2, bg)
        set_cell_margins(cell2, top=70, bottom=70, left=90, right=90)
        p2 = cell2.paragraphs[0]
        r2 = p2.add_run(benefit)
        r2.font.name = 'Segoe UI'
        r2.font.size = Pt(8.5)

    doc.add_paragraph().paragraph_format.space_after = Pt(12)

    # Save output file
    output_path = r"c:\Users\KIM THIEN\OneDrive\Desktop\Playground\Tom_Tat_Kien_Truc_Cac_Form.docx"
    doc.save(output_path)
    print(f"Document created successfully at: {output_path}")

if __name__ == "__main__":
    create_forms_document()
