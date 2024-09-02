using Common.Interfaces;

namespace Common.Formatters.DateTimeFormatter
{
    public interface IDateTimeFormatter : IBaseService
    {
        string ConvertToString(DateTime dateTime);
        DateTime ConvertToDateTime(string dateTime);
        DateTime ConvertToDate(string dateTime);
    }
}
