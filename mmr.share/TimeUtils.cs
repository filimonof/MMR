using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.share
{
    /// <summary>
    /// Класс для работы с ID времени (таблица tblTime в базе)
    /// </summary>
    public static class TimeUtils
    {
        #region массивы с значенями ID 

        /// <summary>
        /// Массив часовых значение в базе, БЕЗ значения перехода времени
        /// 8 - перехоод времени, его нету
        /// </summary>
        public static readonly int[] HourID = new int[] 
        { 2, 4, 6, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50 };

        /// <summary>
        /// Массив часовых значение в базе, с значениями перехода времени
        /// 8 - перехоод времени
        /// </summary>
        public static readonly int[] HourIDany = new int[] 
        { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50 };

        /// <summary>
        /// Массив получасовых значение в базе, БЕЗ значение перехода времени
        /// 8,7 - перехоод времени, его нету
        /// </summary>
        public static readonly int[] HalfeHourID = new int[] 
            {1, 2, 3, 4, 5, 6, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27,
            28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
       
        /// <summary>
        /// Массив получасовых значение в базе, с значениями перехода времени
        /// 8,7 - перехоод времени
        /// </summary>
        public static readonly int[] HalfeHourIDany = new int[] 
            {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27,
            28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
        
        #endregion

        #region GetTimeIDToHour - Получение ID по значению часа, без перехода времени
        /// <summary>
        /// Получение ID по значению часа
        /// без перехода времени
        /// </summary>
        /// <param name="hour">час от 1 до 24</param>
        /// <returns>ID для tblTime</returns>
        public static int GetTimeIDToHour(int hour)
        {
            //можно содрать таблицу tbl_Time
            if ((hour < 1) || (hour > 24))
                return 0;
            else
                return HourID[hour - 1];
        }
        #endregion

        #region GetStringAllHourID - получить все ID часовых значений в строку
        /// <summary>
        /// получить все ID часовых значений в строку
        /// </summary>
        /// <param name="separator">разделитель</param>
        /// <param name="perehodTime">учитывать или нет переход времени</param>
        /// <returns>строка вида 2,4,6,...</returns>
        public static string GetStringAllHourID(string separator, bool perehodTime)
        {
            string strID = string.Empty;
            int[] hourses = perehodTime ? TimeUtils.HourIDany : TimeUtils.HourID;
            foreach (int id in hourses)
                strID += id.ToString() + separator;
            if (strID.EndsWith(separator))
                strID = strID.Remove(strID.Length - separator.Length);
            return strID;
        }
        #endregion

        #region GetStringAllHalfeHourID - получить все ID получасовых значений в строку
        /// <summary>
        /// получить все ID получасовых значений в строку
        /// </summary>
        /// <param name="separator">разделитель</param>
        /// <param name="perehodTime">учитывать или нет переход времени</param>
        /// <returns>строка вида 1,2,3,4,5,6,...</returns>
        public static string GetStringAllHalfeHourID(string separator, bool perehodTime)
        {
            string strID = string.Empty;
            int[] halfHourses = perehodTime ? TimeUtils.HalfeHourIDany : TimeUtils.HalfeHourID;
            foreach (int id in halfHourses)
                strID += id.ToString() + separator;
            if (strID.EndsWith(separator))
                strID = strID.Remove(strID.Length - separator.Length);
            return strID;
        }
        #endregion


    }
}
