using System;
using System.Globalization;

namespace Latchet.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly PersianCalendar persianCalendar = new PersianCalendar();

        public static DateTime ToMiladiDate(this DateTime dt)
        {
            return persianCalendar.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);
        }
        public static string GetPersianMonthName(this DateTime dt)
        {
            int intMonth = persianCalendar.GetMonth(dt);
            string strMonthName = "";
            switch (intMonth)
            {
                case 1:
                    strMonthName = "فروردین";
                    break;
                case 2:
                    strMonthName = "اردیبهشت";
                    break;
                case 3:
                    strMonthName = "خرداد";
                    break;
                case 4:
                    strMonthName = "تیر";
                    break;
                case 5:
                    strMonthName = "مرداد";
                    break;
                case 6:
                    strMonthName = "شهریور";
                    break;
                case 7:
                    strMonthName = "مهر";
                    break;
                case 8:
                    strMonthName = "آبان";
                    break;
                case 9:
                    strMonthName = "آذر";
                    break;
                case 10:
                    strMonthName = "دی";
                    break;
                case 11:
                    strMonthName = "بهمن";
                    break;
                case 12:
                    strMonthName = "اسفند";
                    break;
                default:
                    strMonthName = "";
                    break;
            }


            return strMonthName;
        }
        public static string GetPersianYear(this DateTime dt)
        {
            int intYear = persianCalendar.GetYear(dt);
            return intYear.ToString();
        }
        public static string ToStringShamsiDate(this DateTime dt)
        {
            int intYear = persianCalendar.GetYear(dt);
            int intMonth = persianCalendar.GetMonth(dt);
            int intDayOfMonth = persianCalendar.GetDayOfMonth(dt);
            DayOfWeek enDayOfWeek = persianCalendar.GetDayOfWeek(dt);
            string strMonthName = "";
            string strDayName = "";
            switch (intMonth)
            {
                case 1:
                    strMonthName = "فروردین";
                    break;
                case 2:
                    strMonthName = "اردیبهشت";
                    break;
                case 3:
                    strMonthName = "خرداد";
                    break;
                case 4:
                    strMonthName = "تیر";
                    break;
                case 5:
                    strMonthName = "مرداد";
                    break;
                case 6:
                    strMonthName = "شهریور";
                    break;
                case 7:
                    strMonthName = "مهر";
                    break;
                case 8:
                    strMonthName = "آبان";
                    break;
                case 9:
                    strMonthName = "آذر";
                    break;
                case 10:
                    strMonthName = "دی";
                    break;
                case 11:
                    strMonthName = "بهمن";
                    break;
                case 12:
                    strMonthName = "اسفند";
                    break;
                default:
                    strMonthName = "";
                    break;
            }
            switch (enDayOfWeek)
            {
                case DayOfWeek.Friday:
                    strDayName = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    strDayName = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    strDayName = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    strDayName = "یکشنبه";
                    break;
                case DayOfWeek.Thursday:
                    strDayName = "پنجشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    strDayName = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    strDayName = "چهارشنبه";
                    break;
                default:
                    strDayName = "";
                    break;
            }
            return (string.Format("{0} {1} {2} {3}", strDayName, intDayOfMonth, strMonthName, intYear));
        }
        public static string ToShamsiDate(this DateTime dt)
        {
            int intYear = persianCalendar.GetYear(dt);
            int intMonth = persianCalendar.GetMonth(dt);
            int intDay = persianCalendar.GetDayOfMonth(dt);

            return (string.Format("{0}/{1}/{2}", intYear, intMonth, intDay));
        }
        public static string GetPastTime(this DateTime dateTime, bool localTime = false)
        {
            DateTime dtNow = localTime ? DateTime.Now : DateTime.UtcNow;
            TimeSpan dt = (dtNow - dateTime);
            string text = string.Empty;

            if (dt.Days > 0)
            {
                text += dt.Days + "روز  ";
            }
            else if (dt.Hours > 0)
            {
                text += dt.Hours + "ساعت  ";
            }
            else if (dt.Minutes > 0)
            {
                text += dt.Minutes + "دقیقه  ";
            }
            text += " پیش";
            return text;

        }
        public static int GetAge(this DateTime birthday)
        {
            var today = DateTime.Today;

            // Calculate the age.
            var age = today.Year - birthday.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (birthday.Date > today.AddYears(-age)) age--;

            return age;
        }


    }
}
