using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace mmr.share
{
    /// <summary>
    /// Ошибка парсинга
    /// </summary>
    public class ParseUtilsException : Exception
    {
        public ParseUtilsException() : base() { }
        public ParseUtilsException(string message) : base(message) { }
        public ParseUtilsException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Утилиты парсинга
    /// </summary>
    public static class ParseUtils
    {
        /// <summary>
        /// обозначение дня в шаблоне {dmy}
        /// </summary>
        private const char DAY = 'd';

        /// <summary>
        /// обозначение месяца в шаблоне {dmy}
        /// </summary>
        private const char MONTH = 'm';

        /// <summary>
        /// обозначение года в шаблоне {dmy}
        /// </summary>
        private const char YEAR = 'y';

        #region MaskCompare - сравнение строк по маске {}
        /// <summary>
        /// сравнение строк по маске {} 
        /// </summary>
        /// <param name="original">оригинальная строка</param>
        /// <param name="template">строка с маской</param>
        /// <returns>true если совпадают</returns>
        public static bool MaskCompare(string original, string template)
        {
            return ParseUtils.MaskCompare(original, template, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        }

        /// <summary>сравнение строк по маске {} </summary>
        /// <param name="original">оригинальная строка</param>
        /// <param name="template">строка с маской</param>
        /// <param name="options">опции поиска</param>
        /// <returns>true если совпадают</returns>
        public static bool MaskCompare(string original, string template, RegexOptions options)
        {
            //return Regex.IsMatch(original, template.Replace("*", @"(.+)"));
            //вместо * символ {} или {asdhsdgh}
            string templateR = Regex.Replace(template, @"\{\S*\}", @"(.*)").Trim();           
            MatchCollection matches = Regex.Matches(original.Trim(), templateR, options);
            foreach (Match m in matches)
                if (m.Value.CompareTo(original.Trim()) == 0)
                    return true;
            return false;
        }
        #endregion

        #region ParseDateInString - Извлечение даты из строки по шаблону {dmy}
        /// <summary>
        /// Извлечение даты из строки по шаблону {dmy}  
        /// </summary>
        /// <param name="strDate">строка с датой (пример bgn_23.12.2009_end)</param>
        /// <param name="template">шаблон для получения данных (пример bgn_{d.m.y}_end)</param>
        /// <param name="day">возвращаемая дата (пример 23)</param>
        /// <param name="month">возвращаемый месяц (пример 12)</param>
        /// <param name="year">возвращаемый год (пример 2009)</param>
        public static void ParseDateInString(string strDate, string template, out int day, out int month, out int year)
        {
            day = 0;
            month = 0;
            year = 0;
            if (string.IsNullOrEmpty(strDate))  throw new ParseUtilsException("Входная строка пустая");
            if (string.IsNullOrEmpty(template)) throw new ParseUtilsException("Шаблон пустой");
            //без пробелов на концах
            strDate = strDate.Trim();
            template = template.Trim();
            //берём содержмое кавычек {...} из шаблона
            MatchCollection matches = Regex.Matches(template, @"\{(?<date>\S+)\}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (matches.Count != 1)
                throw new ParseUtilsException(string.Format("Некорректный шаблон: \"{0}\"", template));
            string formatDate = matches[0].Groups["date"].ToString();
            //из шаблона делаем строку для парсинга regex с заменой
            string templateStr = Regex.Replace(template, @"\{\S+\}", @"(?<date>\S+)");
            //берём часть строки с датой из входной строки
            MatchCollection matchesDate = Regex.Matches(strDate, templateStr, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (matchesDate.Count != 1)
                throw new ParseUtilsException(string.Format("Неполучается получить дату из строки \"{0}\" по шаблону \"{1}\"", strDate, templateStr));
            string date = matchesDate[0].Groups["date"].ToString();

            //из date по формату formatDate берём день месяц и год                  
            //определяем отделяются ли значения даты друг от друга какими нить символами
            bool isNoSeparetor = true;
            for (int i = 0; i < date.Length; i++)
                if ((date[i] < '0') || date[i] > '9')
                    isNoSeparetor = false;
            if (isNoSeparetor)
            {
                //без разделителей
                int num = 0;
                foreach (char c in formatDate)
                {
                    try
                    {
                        switch (c)
                        {
                            case (ParseUtils.DAY):
                                //подразумеваем что день из 2 значений
                                day = int.Parse(date.Substring(num, 2));
                                num += 2; 
                                break;
                            case (ParseUtils.MONTH):
                                //подразумеваем что месяц из 2 значений
                                month = int.Parse(date.Substring(num, 2));
                                num += 2; 
                                break;
                            case (ParseUtils.YEAR):
                                //подразумеваем что год из 4 значений
                                year = int.Parse(date.Substring(num, 4));
                                num += 4; 
                                break;
                        }
                    }
                    catch
                    {
                        throw new ParseUtilsException(string.Format("Неудалось получить дату \"{0}\" по шаблону \"{1}\"", date, formatDate));
                    }
                }
            }
            else
            {
                //с разделителями
                //делаем беззопасными . - \ / и прочие служебные симболы
                string templateFormatDate = Regex.Escape(formatDate.ToLower());
                //ставим вместо d m y шаблон для извлечения дня месяца и года
                templateFormatDate = templateFormatDate.Replace(ParseUtils.DAY.ToString(), @"(?<d>\S+)")
                    .Replace(ParseUtils.MONTH.ToString(), @"(?<m>\S+)")
                    .Replace(ParseUtils.YEAR.ToString(), @"(?<y>\S+)");
                //выделяем значения день месяц и год
                MatchCollection matchesMDY = Regex.Matches(date, templateFormatDate, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                if (matchesMDY.Count != 1)
                    throw new ParseUtilsException(string.Format("Неполучается извлечь день месяц и год из даты \"{0}\" по шаблону \"{1}\"", date, templateFormatDate));

                if (!string.IsNullOrEmpty(matchesMDY[0].Groups["d"].Value))
                    if (!int.TryParse(matchesMDY[0].Groups["d"].Value, out day))
                        throw new ParseUtilsException(string.Format("Ошибка получения значения дня \"{0}\"", matchesDate[0].Groups["d"].Value));
                if (!string.IsNullOrEmpty(matchesMDY[0].Groups["m"].Value))
                    if (!int.TryParse(matchesMDY[0].Groups["m"].Value, out month))
                        throw new ParseUtilsException(string.Format("Ошибка получения значения месяца \"{0}\"", matchesDate[0].Groups["m"].Value));
                if (!string.IsNullOrEmpty(matchesMDY[0].Groups["y"].Value))
                    if (!int.TryParse(matchesMDY[0].Groups["y"].Value, out year))
                        throw new ParseUtilsException(string.Format("Ошибка получения значения года \"{0}\"", matchesDate[0].Groups["y"].Value));
            }
        }

        /// <summary>
        /// Извлечение даты из строки по шаблону {dmy}
        /// </summary>
        /// <param name="strDate">строка с датой (пример bgn_23.12.2009_end)</param>
        /// <param name="template">шаблон для получения данных (пример bgn_{d.m.y}_end)</param>
        /// <param name="date">возвращаемая дата (пример 23.12.2009)</param>
        public static void ParseDateInString(string strDate, string template, out DateTime date)
        {
            int day;
            int month;
            int year;
            ParseUtils.ParseDateInString(strDate, template, out day, out month, out year);
            if (day == 0 || month == 0 || year == 0) 
                throw new ParseUtilsException(string.Format("Ошибка получения даты из строки \"{0}\" по шаблону \"{1}\"", strDate, template));
            date = new DateTime(year, month, day);
        }
        #endregion

        #region PasteDateInString - вставляем в строку вместо {dmy} дату
        /// <summary>
        /// вставляем в строку вместо {dmy} дату
        /// </summary>
        /// <param name="strTemplate">строка с шаблоном даты для подстановки(пример bgn_{d.m.y}_end)</param>
        /// <param name="date">дата вставляемая в строку по шаблону</param>
        /// <returns>строка без шаблона но с датой</returns>
        public static string PasteDateInString(string strTemplate, DateTime date)
        {
            if (string.IsNullOrEmpty(strTemplate)) throw new ParseUtilsException("Входная строка пустая");
            MatchCollection matches = Regex.Matches(strTemplate, @"\{(?<date>\S+)\}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (matches.Count == 0) return strTemplate;
            if (matches.Count > 1)
                throw new ParseUtilsException(string.Format("Некорректный шаблон в строке: \"{0}\"", strTemplate));
            string res = matches[0].Groups["date"].ToString();
            res = res.Replace(ParseUtils.DAY.ToString(), date.Day.ToString().PadLeft(2, '0'));
            res = res.Replace(ParseUtils.MONTH.ToString(), date.Month.ToString().PadLeft(2, '0'));
            res = res.Replace(ParseUtils.YEAR.ToString(), date.Year.ToString());
            return Regex.Replace(strTemplate, @"\{\S+\}", res);
        }
        #endregion
    }
}
