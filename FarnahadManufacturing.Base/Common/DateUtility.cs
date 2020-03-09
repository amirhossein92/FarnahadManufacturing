using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FarnahadManufacturing.Base.Common
{
    public static class DateUtility
    {
        public static string ToShamsi(this DateTime dateTime)
        {
            var pt = new PersianCalendar();
            var year = pt.GetYear(dateTime);
            var month = pt.GetMonth(dateTime);
            var day = pt.GetDayOfMonth(dateTime);
            var daysOfWeek = pt.GetDayOfWeek(dateTime);

            return $"{DaysStrings[(int)daysOfWeek]} {day}-{MonthStrings[month - 1]}-{year}";
        }

        public static readonly string[] MonthStrings = new string[]
        {
            "فروردین", "اردیبهشت", "خرداد",
            "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر",
            "دی", "بهمن", "اسفند"
        };

        public static readonly string[] DaysStrings = new string[]
        {
            "یک شنبه",
            "دوشنبه",
            "سه شنبه",
            "چهارشنبه",
            "پنج شنبه",
            "جمعه",
            "شنبه",
        };
    }
}
