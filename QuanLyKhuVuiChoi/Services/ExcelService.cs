using System;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace QuanLyKhuVuiChoi.Services
{
    public class ExcelService : IExcelService
    {
        public void ExportDataTableToExcel(DataTable dataTable, string sheetName, string filePath, string title)
        {
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("Đường dẫn file không hợp lệ.", nameof(filePath));

            using (var workbook = new XLWorkbook())
            {
                var safeSheetName = string.IsNullOrWhiteSpace(sheetName) ? "Sheet1" : sheetName.Substring(0, Math.Min(30, sheetName.Length));
                var worksheet = workbook.Worksheets.Add(safeSheetName);

                int colCount = dataTable.Columns.Count;
                if (colCount == 0) colCount = 1;

                // 1. Tiêu đề chính
                worksheet.Cell(1, 1).Value = title.ToUpper();
                var titleRange = worksheet.Range(1, 1, 1, colCount);
                titleRange.Merge();
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.FontSize = 16;
                titleRange.Style.Font.FontColor = XLColor.White;
                titleRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
                titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Row(1).Height = 35;

                // 2. Thông tin xuất file (Ngày xuất, Tổng số dòng)
                worksheet.Cell(2, 1).Value = $"Thời gian xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Tổng số bản ghi: {dataTable.Rows.Count}";
                var infoRange = worksheet.Range(2, 1, 2, colCount);
                infoRange.Merge();
                infoRange.Style.Font.Italic = true;
                infoRange.Style.Font.FontSize = 10;
                infoRange.Style.Font.FontColor = XLColor.FromHtml("#595959");
                infoRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Row(2).Height = 20;

                // 3. Header bảng dữ liệu (Dòng 4)
                int startRow = 4;
                for (int c = 0; c < dataTable.Columns.Count; c++)
                {
                    var cell = worksheet.Cell(startRow, c + 1);
                    cell.Value = dataTable.Columns[c].ColumnName;
                    cell.Style.Font.Bold = true;
                    cell.Style.Font.FontSize = 11;
                    cell.Style.Font.FontColor = XLColor.White;
                    cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#2F5597");
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.OutsideBorderColor = XLColor.FromHtml("#D9D9D9");
                }
                worksheet.Row(startRow).Height = 25;

                // 4. Dữ liệu bảng
                for (int r = 0; r < dataTable.Rows.Count; r++)
                {
                    int currentRow = startRow + 1 + r;
                    for (int c = 0; c < dataTable.Columns.Count; c++)
                    {
                        var cell = worksheet.Cell(currentRow, c + 1);
                        var val = dataTable.Rows[r][c];

                        if (val != DBNull.Value && val != null)
                        {
                            if (val is decimal || val is double || val is float)
                            {
                                cell.Value = Convert.ToDouble(val);
                                cell.Style.NumberFormat.Format = "#,##0";
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            }
                            else if (val is int || val is long)
                            {
                                cell.Value = Convert.ToInt64(val);
                                cell.Style.NumberFormat.Format = "#,##0";
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            }
                            else if (val is DateTime dt)
                            {
                                cell.Value = dt;
                                cell.Style.NumberFormat.Format = "dd/MM/yyyy";
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }
                            else
                            {
                                cell.Value = val.ToString();
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            }
                        }
                        else
                        {
                            cell.Value = string.Empty;
                        }

                        // Zebra striping
                        if (r % 2 == 1)
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#F9FAFB");
                        }

                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.OutsideBorderColor = XLColor.FromHtml("#E0E0E0");
                        cell.Style.Font.FontSize = 10;
                    }
                    worksheet.Row(currentRow).Height = 20;
                }

                // 5. Auto-fit cột
                worksheet.Columns().AdjustToContents(8.0, 45.0);

                // Lưu file
                var dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                workbook.SaveAs(filePath);
            }
        }

        public DataTable ReadExcelToDataTable(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("Không tìm thấy file Excel.", filePath);

            var dt = new DataTable();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheets.FirstOrDefault();
                if (worksheet == null) return dt;

                var firstRow = worksheet.FirstRowUsed();
                if (firstRow == null) return dt;

                int startCol = firstRow.FirstCellUsed().Address.ColumnNumber;
                int endCol = firstRow.LastCellUsed().Address.ColumnNumber;

                // Đọc Header
                for (int c = startCol; c <= endCol; c++)
                {
                    string colName = firstRow.Cell(c).GetString().Trim();
                    if (string.IsNullOrWhiteSpace(colName)) colName = $"Column_{c}";
                    
                    // Tránh trùng tên cột trong DataTable
                    int duplicateCount = 1;
                    string uniqueColName = colName;
                    while (dt.Columns.Contains(uniqueColName))
                    {
                        uniqueColName = $"{colName}_{duplicateCount++}";
                    }

                    dt.Columns.Add(uniqueColName);
                }

                // Đọc các dòng dữ liệu
                var rows = worksheet.RowsUsed().Skip(firstRow.RowNumber());
                foreach (var row in rows)
                {
                    // Bỏ qua dòng trống
                    if (row.IsEmpty()) continue;

                    var newRow = dt.NewRow();
                    bool hasData = false;

                    for (int c = startCol; c <= endCol; c++)
                    {
                        int dtColIdx = c - startCol;
                        var cellVal = row.Cell(c).GetString().Trim();
                        newRow[dtColIdx] = cellVal;
                        if (!string.IsNullOrWhiteSpace(cellVal)) hasData = true;
                    }

                    if (hasData)
                    {
                        dt.Rows.Add(newRow);
                    }
                }
            }

            return dt;
        }

        public void CreateTemplateFile(string filePath, string sheetName, string[] headers, string[] sampleRow)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                // Header
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = worksheet.Cell(1, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Font.FontSize = 11;
                    cell.Style.Font.FontColor = XLColor.White;
                    cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }
                worksheet.Row(1).Height = 25;

                // Sample row
                if (sampleRow != null && sampleRow.Length > 0)
                {
                    for (int i = 0; i < sampleRow.Length; i++)
                    {
                        var cell = worksheet.Cell(2, i + 1);
                        cell.Value = sampleRow[i];
                        cell.Style.Font.Italic = true;
                        cell.Style.Font.FontColor = XLColor.FromHtml("#595959");
                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                    worksheet.Row(2).Height = 22;
                }

                worksheet.Columns().AdjustToContents(12.0, 35.0);

                var dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                workbook.SaveAs(filePath);
            }
        }
    }
}
