using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.plugins
{
    /// <summary>
    /// Интерфейс для MDI окон настройки
    /// </summary>
    public interface IOptionsMDIForm : IPluginsToMenu
    {
        /// <summary>
        /// Загрузка данных - отмена сохранения
        /// </summary>
        void LoadData();

        /// <summary>
        /// Сохранение данных
        /// </summary>
        void SaveData();
    }
}
