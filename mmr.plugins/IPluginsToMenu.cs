using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.plugins
{
    /// <summary>
    /// Интерфейс плагинов предназначеныех для интеграции в меню основной формы
    /// </summary>
    public interface IPluginsToMenu
    {
        /// <summary>
        /// Название формы в меню (должно быть уникально)
        /// </summary>
        string NameForm { get; }

        /// <summary>
        /// родительское меню в настройках
        /// </summary>
        string ParentMenu { get; }

        /// <summary>
        /// Иконка
        /// </summary>
        System.Drawing.Icon Icon { get; }
    }
}
