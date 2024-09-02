using Common.Extentions;
using Common.Extentions;

namespace Common.Formatters.DateTimeFormatter
{
    internal class Iso8610DateTimeFormatter : IDateTimeFormatter
    {
        public DateTime ConvertToDate(string dateTime)
        {
            return dateTime.ConvertToDateFromMiladiDate(dateSeperator: "-");
        }

        public DateTime ConvertToDateTime(string dateTime)
        {
            return dateTime.ConvertToDateTimeFromMiladiDateTime(betweenDateAndTime: "T", dateSeperator: "-", timeSeperator: ":");
        }

        public string ConvertToString(DateTime dateTime)
        {
            return dateTime.GetStringDateTime("-", "T", ":");
        }
    }
}
