using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vit.CK7
{
    /// <summary>
    /// Категории телеизмерений (таблица OIK.dbo.OICat)
    /// </summary>
    public enum Category
    {
        /// <summary>ТИ - Телеизмерения</summary>
        [Comment("Телеизмерения"), Abbr("ТИ")]
        TI = 'I',

        /// <summary>ТС - Телесигналы</summary>
        [Comment("Телесигналы"), Abbr("ТС")]
        TS = 'S',

        /// <summary>ИС - Интегралы и средние</summary>
        [Comment("Интегралы и средние"), Abbr("ИС")]
        IS = 'J',

        /// <summary>СВ - СВ-1(мгновенная,СДВ)</summary>
        [Comment("СВ-1 (мгновенная,СДВ)"), Abbr("СВ")]
        SV = 'W',

        /// <summary>ПЛ - Планы</summary>
        [Comment("Планы"), Abbr("ПЛ")]
        PL = 'P',

        /// <summary>ЕИ - Ежедневная информация</summary>
        [Comment("Ежедневная информация"), Abbr("ЕИ")]
        EI = 'U',

        /// <summary>СП - Специальные параметры вещественные</summary>
        [Comment("Специальные параметры вещественные"), Abbr("СП")]
        SP = 'C',

        /// <summary>ПВ - СВ-2(усредненная)</summary>
        [Comment("СВ-2 (усредненная)"), Abbr("ПВ")]
        PV = 'H',

        /// <summary>СБТ - События</summary>
        [Comment("События"), Abbr("СБТ")]
        SBT = 'E',

        /// <summary>ФТИ - Телеизмерения фильтрованные</summary>
        [Comment("Телеизмерения фильтрованные"), Abbr("ФТИ")]
        FTI = 'L',

        /// <summary>ОТИ - Телеизмерения оцененные</summary>
        [Comment("Телеизмерения оцененные"), Abbr("ОТИ")]
        OTI = 'O',

        /// <summary>МСК - Специальные параметры целочисленные</summary>
        [Comment("Специальные параметры целочисленные"), Abbr("МСК")]
        MSK = 'M',

        /// <summary>ТИС - Телеизмерения сырые</summary>
        [Comment("Телеизмерения сырые"), Abbr("ТИС")]
        TIS = 'A',

        /// <summary>ТСС - Телесигналы сырые</summary>
        [Comment("Телесигналы сырые"), Abbr("ТСС")]
        TSS = 'B',

        /// <summary>МИН - Универсальные хранилища 1 мин</summary>
        [Comment("Универсальные хранилища 1 мин"), Abbr("МИН")]
        Min = 'Б',

        /// <summary>ПМИН - Универсальные хранилища 5 мин</summary>
        [Comment("Универсальные хранилища 5 мин"), Abbr("ПМИН")]
        Min5 = 'Г',

        /// <summary>ДМИН - Универсальные хранилища 10 мин</summary>
        [Comment("Универсальные хранилища 10 мин"), Abbr("ДМИН")]
        Min10 = 'З',

        /// <summary>ЧЧАС - Универсальные хранилища 15 мин</summary>
        [Comment("Универсальные хранилища 15 мин"), Abbr("ЧЧАС")]
        Min15 = 'И',

        /// <summary>ПЧАС - Универсальные хранилища 30 мин</summary>
        [Comment("Универсальные хранилища 30 мин"), Abbr("ПЧАС")]
        Min30 = 'К',

        /// <summary>ЧАС - Универсальные хранилища 1 час</summary>
        [Comment("Универсальные хранилища 1 час"), Abbr("ЧАС")]
        Hour = 'Л',

        /// <summary>СУТ - Универсальные хранилища 1 день</summary>
        [Comment("Универсальные хранилища 1 день"), Abbr("СУТ")]
        Day = 'П',

        /// <summary>МЕС - Универсальные хранилища 1 месяц</summary>
        [Comment("Универсальные хранилища 1 месяц"), Abbr("МЕС")]
        Mount = 'У',

        /// <summary>ТМИН - Универсальные хранилища 3 мин</summary>
        [Comment("Универсальные хранилища 3 мин"), Abbr("ТМИН")]
        Min3 = 'Ф',

        /// <summary>СД - Статич.данные для локальных дорасчетов (const)</summary>
        [Comment("Статич.данные для локальных дорасчетов (const)"), Abbr("СД")]
        SD = 'Ъ',

        /// <summary>ЛД - Локальный дорасчет на формах</summary>
        [Comment("Локальный дорасчет на формах"), Abbr("ЛД")]
        LD = 'Д',

        /// <summary> Д- Временная локальная переменная дорасчета</summary>
        [Comment("Временная локальная переменная дорасчета"), Abbr("Д")]
        D = 'D',

        /// <summary>Т - Период (временной интервал из таблицы Period)</summary>
        [Comment("Период (временной интервал из таблицы Period)"), Abbr("Т")]
        T = 'R',

        /// <summary>ЕИТ - Текстовая ежедневная информация</summary>
        [Comment("Текстовая ежедневная информация"), Abbr("ЕИТ")]
        EIT = 'T'
    }

    /// <summary>
    /// Название категории телеизмерения
    /// </summary>
    class CommentAttribute : Attribute
    {
        /// <summary>поле для описания</summary>
        private readonly string _comment;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="comment">описание</param>
        public CommentAttribute(string comment)
        {
            this._comment = comment;
        }

        /// <summary>
        /// перевести в строку
        /// </summary>
        /// <returns>строковое значение атрибута</returns>
        public override string ToString()
        {
            return this._comment.ToString();
        }
    }

    /// <summary>
    /// Аббревиатура категории телеизмерения
    /// </summary>
    class AbbrAttribute : Attribute
    {
        /// <summary>поле для описания</summary>
        private readonly string _abbr;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="abbr">описание</param>
        public AbbrAttribute(string abbr)
        {
            this._abbr = abbr;
        }

        /// <summary>
        /// перевести в строку
        /// </summary>
        /// <returns>строковое значение атрибута</returns>
        public override string ToString()
        {
            return this._abbr.ToString();
        }
    }
    
    /// <summary>
    /// Работа с категориями телеизмерений
    /// </summary>
    public static class CategoryUtils
    {

        /// <summary>Получения аттрибута Comment у перечисления CategoryTI</summary>
        /// <param name="category">значение перечисления</param>
        /// <returns>значение аттрибута</returns>
        public static string GetComment(this Enum category)
        {
            if (category == null)
                return string.Empty;
            //throw new ArgumentNullException("category");            

            // получение значение аттрибута Description у пеерчисления
            System.Reflection.FieldInfo fi = category.GetType().GetField(category.ToString());
            CommentAttribute attr = (CommentAttribute)Attribute.GetCustomAttribute(fi, typeof(CommentAttribute));

            if (attr != null)
                //если есть аттрибут Comment то выводим значение аттрибута
                return attr.ToString();
            else
                // если нет аттрибута то имя перечисления
                return category.ToString();
        }


        /// <summary>Получения аттрибута Abbr у перечисления CategoryTI</summary>
        /// <param name="category">значение перечисления</param>
        /// <returns>значение аттрибута</returns>
        public static string GetAbbr(this Enum category)
        {
            if (category == null)
                return string.Empty;
            //throw new ArgumentNullException("category");            

            // получение значение аттрибута Description у пеерчисления
            System.Reflection.FieldInfo fi = category.GetType().GetField(category.ToString());
            AbbrAttribute attr = (AbbrAttribute)Attribute.GetCustomAttribute(fi, typeof(AbbrAttribute));

            if (attr != null)
                //если есть аттрибут Abbr то выводим значение аттрибута
                return attr.ToString();
            else
                // если нет аттрибута то имя перечисления
                return category.ToString();
        }


        /// <summary>Список категорий</summary>
        /// <param name="type">тип</param>
        /// <param name="sorted">сортировать</param>
        /// <returns>список</returns>
        public static IList ToList(this Type type, bool sorted)
        {
            if (type == null)
                return null;
            //throw new ArgumentNullException("type");

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);
            foreach (Enum value in enumValues)
                list.Add(new KeyValuePair<Enum, string>(value, string.Format("{0} ({1})", CategoryUtils.GetComment(value), CategoryUtils.GetAbbr(value))));
            if (sorted)
                list.Sort(new CompareEnumStringClass());
            return list;
        }
    }

    /// <summary>
    /// Класс сравнения KeyValuePair<Enum, string>
    /// </summary>
    public class CompareEnumStringClass : IComparer
    {
        /// <summary>переопределяем функцию сравнения в интерфейче IComparer</summary>
        /// <param name="x">первое значение</param>
        /// <param name="y">второе значение</param>
        /// <returns>результат сравнения</returns>
        int IComparer.Compare(Object x, Object y)
        {
            string xValue = ((KeyValuePair<Enum, string>)x).Value;
            string yValue = ((KeyValuePair<Enum, string>)y).Value;
            return (xValue.CompareTo(yValue));
        }
    }
}
