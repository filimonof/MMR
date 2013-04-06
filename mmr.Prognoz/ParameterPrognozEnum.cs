using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.Prognoz
{
    public enum ParameterPrognozEnum
    {
        /// <summary>
        /// Схема прогноза
        /// </summary>
        PrognozOtkl_PP_Shema,

        /// <summary>
        /// Зона прогноза
        /// </summary>
        PrognozOtkl_PP_Area,

        /// <summary>
        /// Статус прогноза
        /// </summary>
        PrognozOtkl_PP_Status,

        /// <summary>
        /// ТИ в СК
        /// </summary>
        PrognozOtkl_CK_TI,

        /// <summary>
        /// Категория ТИ
        /// </summary>
        PrognozOtkl_CK_Cat,

        /// <summary>
        /// Произвести усреднение
        /// </summary>
        PrognozOtkl_Avg,

        /// <summary>
        /// Название колонки с ТИ
        /// </summary>
        PrognozOtkl_ColumnName,

        /// <summary>
        /// Название колонки с отклонениями
        /// </summary>
        PrognozOtkl_ColumnOtklName,
    }
}
