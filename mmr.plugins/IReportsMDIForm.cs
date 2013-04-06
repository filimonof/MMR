using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.plugins
{
    /// <summary>
    /// Интерфейс для MDI окон отчётов
    /// </summary>
    public interface IReportsMDIForm : IPluginsToMenu
    {
        /// <summary>
        /// Обновить данные
        /// </summary>
        void ReloadData();
    }
}
