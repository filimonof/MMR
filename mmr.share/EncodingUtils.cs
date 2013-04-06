using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.share
{
    /// <summary>
    /// Кодировка макета
    /// </summary>
    public enum EncodingMaketEnum
    {
        /// <summary>Windows-1251</summary>
        Windows1251,

        /// <summary>UTF-8</summary>
        UTF8,   

        /// <summary>ASCII</summary>
        ASCII
    }    

    /// <summary>
    /// Утилиты кодировки
    /// </summary>
    public static class EncodingUtils
    {
        #region GetEncoding - получение  кодировки по перечеслению
        /// <summary>
        /// получение системной кодировки по кодировке макета 
        /// </summary>
        /// <param name="encode">кодировка макета</param>
        /// <returns>системная кодировка</returns>
        public static Encoding GetEncoding(EncodingMaketEnum encode)
        {
            switch (encode)
            {
                case EncodingMaketEnum.ASCII: return Encoding.ASCII;
                case EncodingMaketEnum.UTF8: return Encoding.UTF8;
                case EncodingMaketEnum.Windows1251: return Encoding.GetEncoding(1251);
                default: return Encoding.GetEncoding(1251);
            }
        }
        #endregion

        #region GetEncodingMaket - получение кодировки макета из строки
        /// <summary>
        /// получение кодировки макета из строки
        /// </summary>
        /// <param name="encode">строка с именем кодировки</param>
        /// <returns>кодировка макета или null если нет совпадений</returns>
        public static EncodingMaketEnum? GetEncodingMaket(string encode)
        {
            //Enum.Parse(typeof(EncodingMaketEnum), encode, true);            
            switch (encode.ToLower().Trim())
            {
                case "windows1251": return EncodingMaketEnum.Windows1251;
                case "utf8": return EncodingMaketEnum.UTF8;
                case "ascii": return EncodingMaketEnum.ASCII;
                default: return null;
            }
        }
        #endregion
                 
        #region преобразования кодировок (commented)
        /*
        public static string Koi8r_To_Win1251(string source)
        {
            Encoding win1251 = Encoding.GetEncoding("windows-1251");
            Encoding koi8r = Encoding.GetEncoding("koi8-r");
            byte[] srcBytes = win1251.GetBytes(source);
            byte[] dstBytes = Encoding.Convert(koi8r, win1251, srcBytes);
            return win1251.GetString(dstBytes);
        }

        public static string Win1251_To_Koi8r(string source)
        {
            Encoding win1251 = Encoding.GetEncoding("windows-1251");
            Encoding koi8r = Encoding.GetEncoding("koi8-r");
            byte[] srcBytes = koi8r.GetBytes(source);
            byte[] dstBytes = Encoding.Convert(win1251, koi8r, srcBytes);
            return koi8r.GetString(dstBytes);
        }

        public static string Utf8_To_Win1251(string source)
        {
            Encoding utf8 = Encoding.UTF8;
            Encoding win1251 = Encoding.GetEncoding("windows-1251");
            byte[] srcBytes = utf8.GetBytes(source);
            byte[] dstBytes = Encoding.Convert(utf8, win1251, srcBytes);
            return win1251.GetString(dstBytes);
        }

        public static string Win1251_To_Utf8(string source)
        {
            Encoding utf8 = Encoding.UTF8;
            Encoding win1251 = Encoding.GetEncoding("windows-1251");
            byte[] srcBytes = win1251.GetBytes(source);
            byte[] dstBytes = Encoding.Convert(win1251, utf8, srcBytes);
            return utf8.GetString(dstBytes);
        }
         */
        #endregion
        
    }
}
