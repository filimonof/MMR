using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vit.progress
{
    /// <summary>
    /// Базовый класс для классов работающих с ProgressDlg
    /// </summary>
    public class ProgressBaseClass : IProgressable, IDisposable
    {
        /// <summary>поле содержащее ссылку на прогресс этого класса</summary>
        protected ProgressDlg _progress;

        #region Конструктор
        /// <summary>конструктор</summary>
        /// <param name="progress">ссылка на прогрессбар который будет работать с классом</param>
        public ProgressBaseClass(ProgressDlg progress)
        {
            this._progress = progress;
        }

        /// <summary>конструтор null</summary>
        //public ProgressBaseClass() : this(null) { }

        #endregion

        #region IProgressable Members

        /// <summary>вывод сообщения в диалоговое окно _progress</summary>
        /// <param name="txt">Текст сообщения</param>
        public void WriteComment(string txt)
        {
            if (this._progress != null)
                this._progress.WriteComment(txt);
        }

        /// <summary>Движение прогресса</summary>
        public void Next()
        {
            if (this._progress != null)
                this._progress.Next();
        }

        #endregion

        #region IDisposable Members

        /// <summary>прошло удаление ресурсов</summary>
        private bool _disposed = false;

        /// <summary>ручная очистка ресурсов</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// деструктор - завершитель запускаемый сборщиком мусора
        /// </summary>
        ~ProgressBaseClass()
        {
            this.Dispose(false);
        }

        /// <summary>освобождение занятых ресурсов</summary>
        /// <param name="disposing">true - ручная очистка; false - сборщиком мусора</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //освобождаем управляемые ресурсы -> component.Dispose();
                    this._progress = null;
                }
                //особождение неуправляемых ресурсов -> CloseHandle(handle);handle = IntPtr.Zero;
                this._disposed = true;
            }
        }
        #endregion

        #region пример класса наследника для IDisposable
        //public class SupremeParameterClass : ProgressBaseClass
        //{
        //    private bool _disposed = false;
        //    protected override void Dispose(bool disposing)
        //    {
        //        if (!this._disposed)
        //        {
        //            if (disposing)
        //            {
        //                // освобождаем управляемые ресурсы
        //            }
        //            // освобождаем неуправляемые ресурсы
        //            base.Dispose(disposing);
        //            this._disposed = true;
        //        }
        //    }
        //}
        #endregion
    }
}
