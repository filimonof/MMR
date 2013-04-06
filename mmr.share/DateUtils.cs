using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.share
{
    /// <summary>
    /// Статический класс с функциями работы с датой
    /// </summary>
    public static class DateUtils
    {

        #region NameMonthRus[] - масяцы по руууски и в родительном падеже
        /// <summary>Название месяцов</summary>
        public static readonly string[] NameMonthRus = new string[] {
            "январь", 
            "февраль",
            "март",
            "апрель",
            "май",
            "июнь",
            "июль",
            "август",
            "сентябрь",
            "октябрь",
            "ноябрь",
            "декабрь"
        };

        /// <summary>Название месяцов в родительном падеже</summary>
        public static readonly string[] NameMonthRusRp = new string[] {
            "января", 
            "февраля",
            "марта",
            "апреля",
            "мая",
            "июня",
            "июля",
            "августа",
            "сентября",
            "октября",
            "ноября",
            "декабря"
        };
        #endregion

        /// <summary>День недели по русски</summary>
        /// <param name="day">день недели</param>
        /// <returns>русское название дня недели</returns>
        public static string DayOfWeekToRus(DayOfWeek day)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ru-RU");
            return culture.DateTimeFormat.GetDayName(day);
        }

        /// <summary>Месяц по русски в родительном падеже</summary>
        /// <param name="month">номер месяца 1..12</param>
        /// <returns>название месяца по русски с маленькой буквы  в родительном падеже</returns>
        public static string MonthToRusRp(int month)
        {
            if (1 <= month && month <= 12)
                return DateUtils.NameMonthRusRp[month - 1];
            else
                return string.Empty;
        }

        /// <summary>Преобразовать дату в строку 01 месяца  в р.п.</summary>
        /// <param name="dt">дата</param>
        /// <returns>строка с датой в родительном падеже</returns>
        public static string DayMonthRPToStr(DateTime dt)
        {
            return string.Format("{0} {1}", dt.Day, DateUtils.MonthToRusRp(dt.Month));
        }

    }

}
