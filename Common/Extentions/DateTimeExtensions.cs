using Common.Exceptions;
using System.Globalization;

namespace Common.Extentions
{
    public static class DateTimeExtensions
    {

        #region Get PersianDate Time
        public static int GetPersianYear(this DateTime dt) => (new PersianCalendar()).GetYear(dt);
        public static int GetPersianMonth(this DateTime dt) => (new PersianCalendar()).GetMonth(dt);
        public static int GetPersianDay(this DateTime dt) => (new PersianCalendar()).GetDayOfMonth(dt);
        public static int GetPersianHour(this DateTime dt) => (new PersianCalendar()).GetHour(dt);
        public static int GetPersianMinute(this DateTime dt) => (new PersianCalendar()).GetMinute(dt);
        public static int GetPersianSecond(this DateTime dt) => (new PersianCalendar()).GetSecond(dt);

        public static string GetPersianDate(this DateTime dt, string seperator = "/") =>
            $"{GetPersianYear(dt).ToString("0000")}{seperator}" +
            $"{GetPersianMonth(dt).ToString("00")}{seperator}" +
            $"{GetPersianDay(dt).ToString("00")}";
        public static string GetPersianTime(this DateTime dt, string seperator = ":") =>
            $"{GetPersianHour(dt).ToString("00")}{seperator}" +
            $"{GetPersianMinute(dt).ToString("00")}";
        public static string GetPersianDateTime(this DateTime dt) => GetPersianDateTime(dt, "/", ":");
        public static string GetPersianDateTime(this DateTime? dt) => dt.HasValue ? GetPersianDateTime(dt.Value, "/", ":") : "";
        public static string GetPersianDateTime(this DateTime dt, string seperatorDate = "/", string seperatorTime = ":", string seperatorDateAndtime = " ") =>
            $"{GetPersianDate(dt, seperatorDate)}{seperatorDateAndtime}" +
            $"{GetPersianTime(dt, seperatorTime)}";


        #endregion

        public static DateTime StartDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }
        public static DateTime EndDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }


        #region Get String Format
        public static string GetStringDateTime(this DateTime dt, string dateSeperator = "/", string betweenDateAndTime = " ", string timeSeperator = ":")
        {
            return $"{dt.GetStringDate(dateSeperator)}{betweenDateAndTime}{dt.GetStringTime(timeSeperator)}";
        }
        public static string GetStringDate(this DateTime dt, string dateSeperator = "/")
        {
            return $"{dt.Year.ToString("0000")}{dateSeperator}" +
                $"{dt.Month.ToString("00")}{dateSeperator}" +
                $"{dt.Day.ToString("00")}";
        }
        public static string GetStringTime(this DateTime dt, string timeSeperator = ":")
        {
            return $"{dt.Hour.ToString("00")}{timeSeperator}" +
                $"{dt.Minute.ToString("00")}{timeSeperator}" +
                $"00";
            //$"{dt.Second.ToString("00")}";
        }
        #endregion

        #region Convert to Date time
        public static DateTime ConvertToDateTimeFromMiladiDateTime(this string template, string betweenDateAndTime = " ",
           string dateSeperator = "/", string timeSeperator = ":")
        {
            int year = 0, month = 0, day = 0, hour = 0, minute = 0;

            try
            {
                if (template.Contains(betweenDateAndTime))
                {
                    var data = template.Trim().Split(betweenDateAndTime);
                    var date = data[0].Split(dateSeperator);
                    var time = data[1].Split(timeSeperator);
                    year = int.Parse(date[0]);
                    month = int.Parse(date[1]);
                    day = int.Parse(date[2]);

                    hour = int.Parse(time[0]);
                    minute = int.Parse(time[1]);

                    var result = new DateTime(year: year, month: month, day: day, hour, minute, 0);
                    return result;
                }
                else
                {
                    var date = template.Split(dateSeperator);
                    year = int.Parse(date[0]);
                    month = int.Parse(date[1]);
                    day = int.Parse(date[2]);
                    var result = new DateTime(year: year, month: month, day: day, 0, 0, 0);
                    return result;
                }
            }
            catch
            {
                throw new NotValidDataException($"Can  not convert this {template} to dateTime => TEMPLATE : yyyy{dateSeperator}mm{dateSeperator}dd{betweenDateAndTime}hh{timeSeperator}mm");
            }

        }
        public static DateTime ConvertToDateFromMiladiDate(this string template, string dateSeperator = "/",
            string betweenDateAndTime = "T", string timeSeperator = ":")
        {
            return template.ConvertToDateTimeFromMiladiDateTime(betweenDateAndTime, dateSeperator, timeSeperator).Date;

        }
        public static DateTime ConvertToDateTimeFromPersianDateTime(this string template, string speratorDateAndTime = " ",
            string speratorDate = "/", string speratorTime = ":")
        {
            var oPersianCalender = new PersianCalendar();
            int year = 0, month = 0, day = 0, hour = 0, minute = 0;
            var data = template.Trim().Split(speratorDateAndTime);
            var date = data[0].Split(speratorDate);
            var time = data[1].Split(speratorTime);
            year = int.Parse(date[0]);
            month = int.Parse(date[1]);
            day = int.Parse(date[2]);

            hour = int.Parse(time[0]);
            minute = int.Parse(time[1]);

            if (data.Length == 3)
            {
                if (data[2].ToLower().Trim() == "pm")
                    if (hour < 12)
                        hour += 12;
            }

            var result = new DateTime(year: year, month: month, day: day, hour, minute, 0, 0, oPersianCalender, DateTimeKind.Local);
            return result;
        }

        public static DateTime ConvertToDateFromPersianDate(this string template, string speratorDate = "/")
        {
            var oPersianCalender = new PersianCalendar();
            int year = 0, month = 0, day = 0, hour = 0, minute = 0;
            var date = template.Trim().Split(speratorDate);
            year = int.Parse(date[0]);
            month = int.Parse(date[1]);
            day = int.Parse(date[2]);

            var result = new DateTime(year: year, month: month, day: day, hour, minute, 0, oPersianCalender);
            return result;
        }
        public static double ConvertToUnixTimestamp(this DateTime date)
        {
            return (int)date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public static double? ConvertToUnixTimestamp(this DateTime? date, DateTime? defaultValue = null)
        {
            if (!date.HasValue)
            {
                if (!defaultValue.HasValue) return null;
                else return defaultValue.Value.ConvertToUnixTimestamp();
            }
            else return date.Value.ConvertToUnixTimestamp();
        }
        public static DateTime ConvertToDateTimeFromUnixTimestamp(this string timeStamp)
        {
            if (string.IsNullOrEmpty(timeStamp) || string.IsNullOrEmpty(timeStamp.Trim()))
                throw new Exception($"timeStamp can not be null or empty for converting");
            try
            {
                return ConvertToDateTimeFromUnixTimestamp(double.Parse(timeStamp));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in convert to dateTime : {timeStamp}", ex);
            }
        }
        public static DateTime ConvertToDateTimeFromUnixTimestamp(this double timeStamp)
        {
            try
            {
                //System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
                //dtDateTime = dtDateTime.ToUniversalTime().AddSeconds(timeStamp).ToLocalTime();
                //return dtDateTime;
                return (new DateTime(1970, 1, 1)).AddSeconds(timeStamp).ToLocalTime();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in convert to dateTime : {timeStamp}", ex);
            }
        }
        /// <summary>
        /// این متد دیگر خطایی فایر نمیکند
        /// و اگر نتواند تبدیل کند
        /// نوع پیشرفضی که قرار گرفته است را بر میگرداند
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTimeFromUnixTimestampIfCanNotConvertThenReturnDefaultValue(this string? timeStamp, DateTime? defaultValue = null)
        {
            try
            {
                return timeStamp.ConvertToDateTimeFromUnixTimestamp();
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }


        #endregion

        #region Change Date Time

        public static DateTime WithTime(this DateTime dt, string time, string separator = ":")
        {
            try
            {
                var splitesDate = time.Split(separator);
                int hour = int.Parse(splitesDate[0]);
                int minute = int.Parse(splitesDate[1]);

                return new DateTime(dt.Year, dt.Month, dt.Day, hour, minute, 0);
            }
            catch (Exception ex)
            {
                throw new NotValidDataException($"This time can not convert to a date time : {time}");
            }
        }

        #endregion

        #region Checking Date time

        public static bool IsToday(this DateTime dt)
        {
            return dt.IsThisDay(DateTime.Now);
        }
        public static bool IsThisDay(this DateTime dt, DateTime specialDay)
        {
            if (dt.Date == specialDay.Date)
                return true;
            return false;
        }

        #endregion
    }
}
