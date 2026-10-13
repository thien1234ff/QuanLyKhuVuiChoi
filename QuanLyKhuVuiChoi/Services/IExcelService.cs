using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyKhuVuiChoi.Services
{
    public interface IExcelService
    {
        void ExportDataTableToExcel(DataTable dataTable, string sheetName, string filePath, string title);
        DataTable ReadExcelToDataTable(string filePath);
        void CreateTemplateFile(string filePath, string sheetName, string[] headers, string[] sampleRow);
    }
}
