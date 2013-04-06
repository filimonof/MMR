using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vit.progress
{
    /// <summary>
    /// Интерфейс диалога ProgressDlg
    /// </summary>
    public interface IProgressable
    {
        /// <summary>
        /// вывод сообщения в диалоговое окно ProgressDlg
        /// </summary>
        /// <param name="txt">Текст сообщения</param>
        void WriteComment(string txt);

        /// <summary>
        /// Движение прогресса
        /// </summary>
        void Next();
    }
}
