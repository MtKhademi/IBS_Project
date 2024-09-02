using Common.Exceptions;
using OfficeOpenXml;

namespace Common.Extentions
{
    public static class ExcellFileExtentions
    {
        public static string? GetStringValue(this ExcelWorksheet ewk, int row, int col)
        {
            if (ewk.Cells[row, col] == null || ewk.Cells[row, col].Value == null)
            {
                throw new NotValidDataException($"Cell[${row},${col}] is null");
            }
            return ewk.Cells[row, col].Value.ToString();

        }
        public static int GetIntValue(this ExcelWorksheet ewk, int row, int col)
        {
            var value = ewk.GetStringValue(row, col);
            if (string.IsNullOrWhiteSpace(value))
                throw new NotValidDataException($"Cell[${row},${col}] is empty,");
            try
            {
                return int.Parse(value);
            }
            catch
            {
                throw new NotValidDataException($"Cell[${row},${col}] can not convert to int : {value}");
            }
        }
        public static double GetDoubleValue(this ExcelWorksheet ewk, int row, int col)
        {
            var value = ewk.GetStringValue(row, col);
            if (string.IsNullOrWhiteSpace(value))
                throw new NotValidDataException($"Cell[${row},${col}] is empty,");
            try
            {
                return double.Parse(value);
            }
            catch
            {
                throw new NotValidDataException($"Cell[${row},${col}] can not convert to double : {value}");
            }
        }
        public static DateTime GetDateTimeValue(this ExcelWorksheet ewk, int row, int col, Func<string, DateTime> convertFunction)
        {
            var value = ewk.GetStringValue(row, col);
            if (string.IsNullOrWhiteSpace(value))
                throw new NotValidDataException($"Cell[${row},${col}] is empty,");
            
            return convertFunction(value);
        }


    }
}
