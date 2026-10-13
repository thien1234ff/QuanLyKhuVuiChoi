using System.Collections.Generic;

namespace QuanLyKhuVuiChoi.DTOs
{
    public class ImportResult<T>
    {
        public int TotalRows { get; set; } = 0;
        public int SuccessCount { get; set; } = 0;
        public int ErrorCount => Errors.Count;
        public List<string> Errors { get; set; } = new List<string>();
        public List<T> ValidItems { get; set; } = new List<T>();

        public bool IsSuccess => ErrorCount == 0 && TotalRows > 0;
    }
}
