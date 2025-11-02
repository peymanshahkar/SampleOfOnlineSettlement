using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSettlement
{
    public static class PersianDate
    {
        public static string PersianDateToMiladiDateString(this string _date)
        {
            int year = int.Parse(_date.Substring(0, 4));
            int month = int.Parse(_date.Substring(5, 2));
            int day = int.Parse(_date.Substring(8, 2));
            PersianCalendar p = new PersianCalendar();
            DateTime date = p.ToDateTime(year, month, day, 0, 0, 0, 0);
            return date.ToShortDateString();

        }
        public static DateTime PersianToMiladiDate(this string _date)
        {
            int year = int.Parse(_date.Substring(0, 4));
            int month = int.Parse(_date.Substring(5, 2));
            int day = int.Parse(_date.Substring(8, 2));
            PersianCalendar p = new PersianCalendar();
            DateTime date = p.ToDateTime(year, month, day, 0, 0, 0, 0);
            return date;
        }
        private static PersianCalendar Persiancalender = new PersianCalendar();

        public static int GetPersianYear(this DateTime date)
        {
            return int.Parse(Persiancalender.GetYear(date).ToString().Substring(2, 2));
        }

        private static string GetPersianDateNow()
        {

            return Persiancalender.GetYear(DateTime.Now).ToString().PadLeft(4, '0') + "/" + Persiancalender.GetMonth(DateTime.Now).ToString().PadLeft(2, '0') + "/" + Persiancalender.GetDayOfMonth(DateTime.Now).ToString().PadLeft(2, '0');
        }
        public static string ToPersianDate(this DateTime dateT)
        {
            string Pdate = Persiancalender.GetYear(dateT).ToString() + "/" + Persiancalender.GetMonth(dateT).ToString() + "/" + Persiancalender.GetDayOfMonth(dateT).ToString();
            return GetPersianDateInTrueFormat(Pdate);
        }


        private static string GetPersianDateInTrueFormat(this string date)
        {

            string[] dt = date.Split('/');

            return dt[0] + "/" + dt[1].PadLeft(2, '0') + "/" + dt[2].PadLeft(2, '0');
        }


        public static string PersianDayName(this DateTime date)
        {
            if (date.DayOfWeek.ToString().ToLower() == "Saturday".ToLower())
            {
                return "شنبه";
            }
            else if (date.DayOfWeek.ToString().ToLower() == "Sunday".ToLower())
            {
                return "یکشنبه";
            }
            else if (date.DayOfWeek.ToString().ToLower() == "Monday".ToLower())
            {
                return "دوشنبه";
            }
            else if (date.DayOfWeek.ToString().ToLower() == "Tuesday".ToLower())
            {
                return "سه شنبه";
            }
            else if (date.DayOfWeek.ToString().ToLower() == "Wednesday".ToLower())
            {
                return "چهارشنبه";
            }
            else if (date.DayOfWeek.ToString().ToLower() == "Thursday".ToLower())
            {
                return "پنجشنبه";
            }
            else if (date.DayOfWeek.ToString().ToLower() == "Friday".ToLower())
            {
                return "جمعه";
            }
            return "";
        }
        public static string GetPersianDateTimeNowInTrueFormat()
        {
            return GetPersianDateNow() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }
        public static string GetPersianDateTimeInTrueFormat(this DateTime dateT)
        {
            if (dateT != null)
                return ToPersianDate(dateT) + " " + dateT.Hour.ToString().PadLeft(2, '0') + ":" + dateT.Minute.ToString().PadLeft(2, '0') + ":" + dateT.Second.ToString().PadLeft(2, '0');
            else
                return GetPersianDateTimeNowInTrueFormat();
        }

    }
}
