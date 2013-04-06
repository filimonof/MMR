using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.plugins
{
    /// <summary>
    /// Интерфейс для MDI окон расчётов
    /// </summary>
    public interface ICalculationsMDIForm : IPluginsToMenu
    {     
        /// <summary>
        /// Обновить данные
        /// </summary>
        void CalculateData();
    }
}
