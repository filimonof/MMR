using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

#region Пример использования
/* 
 
    vit.progress.ProgressBgClass dlg; 
    int _end = 100;
    public void StartTest()
    {
        dlg = new vit.progress.ProgressBgClass();
        dlg.Title = @" Выполнение задачи ...";
        dlg.Caption = @"Тестируем компоненту на пригодность";
        dlg.Cancelled = true;
        dlg.Extended = true;
        dlg.AsyncDialog = true;       

        dlg.DoWork += new DoWorkEventHandler(Work);
        dlg.OnComplete += new RunWorkerCompletedEventHandler(Complete);
        dlg.DoCancel += new DoWorkEventHandler(Cancele);

        dlg.Start();
    }
    public void Work(object sender, DoWorkEventArgs e)
    {
        //задача работает в потоке и любое взаимодействие с оконным интерфейсом должно быть потоко безопасным (Invoke
        dlg.WriteComment("Старт выполнение задания");
        int i;
        for (i = 0; i < 50; i++)
        {
            //выполнение задачи , если ошибка dlg.IsError = true;
            System.Threading.Thread.Sleep(300);
            dlg.WriteComment("Значение " + i.ToString()); 
            if (dlg.IsCancellAndProgress(i * 2, ref e))
            {
                _end = i;
                return;
            }
        }
        e.Result = i;    
    }
    void Complete(object sender, RunWorkerCompletedEventArgs e)
    {
        //выполняется после завершения потока но с открытым диалоговым окном
        if (dlg.IsError)
            dlg.WriteComment("Выполнено с ошибками");
        dlg.WriteComment("Код по завершению " + e.Result);
    }
    void Cancele(object sender, DoWorkEventArgs e)
    {
        //задача работает в потоке и любое взаимодействие с оконным интерфейсом должно быть потоко безопасным (Invoke
        dlg.WriteComment("Процедура отмены задания запущена");
        System.Threading.Thread.Sleep(3000);
        for (int i = _pc; i > 0; i--)
        {
            dlg.WriteComment("Отмена Значения " + i.ToString());
            //какое то действие по отмене            
            dlg.ChangeProgress(i * 2);
        }
    }  
 */
#endregion

namespace vit.progress
{
    /// <summary>
    /// Класс Асинхронного прогресса с использование backgroundWorker
    /// </summary>
    public class ProgressBgClass : IDisposable
    {
        /// <summary>Выполняемая задача</summary>
        public event DoWorkEventHandler DoWork;

        /// <summary>Событие - Отменено выполнение</summary>
        public event DoWorkEventHandler DoCancel;

        /// <summary>Событие - Задача выполнена</summary>
        public event RunWorkerCompletedEventHandler OnComplete;

        /// <summary>Форма с прогрессбаром и расширенным режимом</summary>
        private ProgressBgDlg _progressBgDlg;

        /// <summary>Класс асинхронного выполнения задачи</summary>
        private BackgroundWorker _bgWorker;

        /// <summary>Класс асинхронной отмены задачи</summary>
        private BackgroundWorker _bgCancel;

        /// <summary>Заголовок диалога прогреса</summary>
        public string Title
        {
            get { return this._progressBgDlg != null ? this._progressBgDlg.Title : string.Empty; }
            set { if (this._progressBgDlg != null) this._progressBgDlg.Title = value; }
        }

        /// <summary>Описание прогреса</summary>
        public string Caption
        {
            get { return this._progressBgDlg != null ? this._progressBgDlg.Caption : string.Empty; }
            set { if (this._progressBgDlg != null) this._progressBgDlg.Caption = value; }
        }

        /// <summary>Возможность отмены процесса</summary>
        public bool Cancelled
        {
            get { return this._progressBgDlg != null ? this._progressBgDlg.Cancelled : true; }
            set
            {
                if (this._progressBgDlg != null)
                    this._progressBgDlg.Cancelled = value;
                this._bgWorker.WorkerSupportsCancellation = value;
            }
        }

        /// <summary>флаг нажатия клавиши Отмена</summary>
        private bool _pushCancel;

        /// <summary>Возможность расширенного режима</summary>
        public bool Extended
        {
            get { return this._progressBgDlg != null ? this._progressBgDlg.Extended : true; }
            set { if (this._progressBgDlg != null) this._progressBgDlg.Extended = value; }
        }

        /// <summary>Были ли ошибки</summary>
        public bool IsError
        {
            get { return this._progressBgDlg != null ? this._progressBgDlg.IsError : true; }
            set { if (this._progressBgDlg != null) this._progressBgDlg.IsError = value; }
        }

        /// <summary>Асинхронный вывод диалогового окна (или модально)</summary>
        public bool AsyncDialog
        {
            get { return this._progressBgDlg != null ? this._progressBgDlg.AsyncDialog : true; }
            set { if (this._progressBgDlg != null) this._progressBgDlg.AsyncDialog = value; }
        }


        /// <summary>Конструктор</summary>
        public ProgressBgClass()
        {
            this._progressBgDlg = new ProgressBgDlg();
            this._progressBgDlg.OnPushCancel += new EventHandler(_progressBgDlg_OnPushCancel);

            this._bgWorker = new BackgroundWorker();
            this._bgWorker.WorkerReportsProgress = true;
            this._bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            this._bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            this._bgWorker.ProgressChanged += new ProgressChangedEventHandler(_bgWorker_ProgressChanged);

            this._bgCancel = new BackgroundWorker();
            this._bgCancel.WorkerReportsProgress = false;
            this._bgCancel.WorkerSupportsCancellation = false;
            this._bgCancel.DoWork += new DoWorkEventHandler(_bgCancel_DoWork);
            this._bgCancel.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgCancel_RunWorkerCompleted);

            this.Cancelled = true;
            this._pushCancel = false;
            this.AsyncDialog = false;
        }

        /// <summary>Запуск выполнения задачи</summary>
        public void Start()
        {
            if (!this._bgWorker.IsBusy)
            {
                if (this._progressBgDlg != null)
                    this._progressBgDlg.Show();
                this._bgWorker.RunWorkerAsync();
            }
        }

        /// <summary>Выполнение задачи</summary>
        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //задача работает в потоке 
            //любое взаимодействие с оконным интерфейсом должно быть потокобезопасным (Invoke)
            if (this.DoWork != null)
            {
                try
                {
                    this.DoWork(this, e);
                }
                catch (Exception ex)
                {
                    this.WriteError(@"RunWorkCompleted", ex.ToString());
                }
            }
            else
            {
                this.IsError = true;
                this.WriteComment(@"Задача не определена");
            }
        }

        /// <summary>Задача завершена</summary>
        private void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this._pushCancel)
            {
                if (this._progressBgDlg != null)
                    this._progressBgDlg.ChangeIcon(ProgressBgState.Cancel);

                //выполнить клиентский скрипт                
                if (this.DoCancel != null && !this._bgCancel.IsBusy)
                    this._bgCancel.RunWorkerAsync();
                else
                    if (this._progressBgDlg != null)
                        this._progressBgDlg.CloseAfterWork(true);
            }
            else
            {
                this.ChangeProgress(100);

                if (this._progressBgDlg != null)
                {
                    if (this.IsError)
                        this._progressBgDlg.ChangeIcon(ProgressBgState.Error);
                    else
                        this._progressBgDlg.ChangeIcon(ProgressBgState.Ok);

                    this.Title = @" Выполнено";
                }

                if (this.OnComplete != null)
                    try
                    {
                        this.OnComplete(this, e);
                    }
                    catch (Exception ex)
                    {
                        this.WriteError(@"RunWorkCompleted", ex.ToString());
                    }

                if (this._progressBgDlg != null)
                    this._progressBgDlg.CloseAfterWork(false);
            }
        }

        /// <summary>На форме нажата кнопка отмены</summary>
        private void _progressBgDlg_OnPushCancel(object sender, EventArgs e)
        {
            if (this._bgWorker.IsBusy)
            {
                //сделать кнопку отмены неактивной
                if (this._progressBgDlg != null)
                    this._progressBgDlg.Cancelled = false;

                //вывести предупреждение об отмене
                this.Title = @" Отмена процесса...";

                //установить флаг нажатия кнопки отмена
                this._pushCancel = true;

                //завершение выполнение задачи в backgroundWorker
                this._bgWorker.CancelAsync();
            }
            else
                if (this._progressBgDlg != null)
                    this._progressBgDlg.CloseAfterWork(true);
        }

        /// <summary>Запуск задачи отмены</summary>
        private void _bgCancel_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.DoCancel(sender, e);
            }
            catch (Exception ex)
            {
                this.WriteError(@"DoWork", ex.ToString());
            }
        }

        /// <summary>Завершение отмены</summary>
        private void _bgCancel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ChangeProgress(0);
            if (this._progressBgDlg != null)
                this._progressBgDlg.CloseAfterWork(true);
        }

        /// <summary>Вывод коментарий</summary>
        /// <param name="s">сообщение</param>
        public void WriteComment(string s)
        {
            if (this._progressBgDlg != null)
                this._progressBgDlg.WriteComment(s);
        }

        /// <summary>Вывод сообщения об ошибке</summary>
        /// <param name="Source">Место возникнования ошибки</param>
        /// <param name="ErrorMsg">Текст ошибки</param>        
        private void WriteError(string source, string errorMsg)
        {
            this.IsError = true;
            this.WriteComment(Environment.NewLine + @"ОШИБКА в ProgressBgClass->" + source + Environment.NewLine + errorMsg + Environment.NewLine);
        }

        /// <summary>Проверка в основной задаче на отмену и вывод процентов основного рабочего потока</summary>
        /// <param name="percent">проценты сделанной работы</param>
        /// <param name="e">параметр</param>
        /// <returns>true если работа отменена</returns>
        public bool IsCancellAndProgress(int percent, ref DoWorkEventArgs e)
        {
            if (percent <= 0)
                this._bgWorker.ReportProgress(0);
            else if (percent >= 100)
                this._bgWorker.ReportProgress(100);
            else
                this._bgWorker.ReportProgress(percent);

            if (this._bgWorker.CancellationPending)
            {
                e.Cancel = true;
                return true;
            }
            else return false;
        }

        /// <summary>Смена прогресса из любого потока</summary>
        /// <param name="percent">проценты</param>
        public void ChangeProgress(int percent)
        {
            if (this._progressBgDlg != null)
                try
                {
                    if (percent <= 0)
                        this._progressBgDlg.PercentProgress = 0;
                    else if (percent >= 100)
                        this._progressBgDlg.PercentProgress = 100;
                    else
                        this._progressBgDlg.PercentProgress = percent;
                }
                catch (Exception ex)
                {
                    this.WriteError(@"ChangeProgress", ex.ToString());
                }
        }

        /// <summary>изменение хода выполнения основной фоновой задачи</summary>
        private void _bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //вывод процентов
            this.ChangeProgress(e.ProgressPercentage);
        }

        #region IDisposable Members

        /// <summary>поршло удаление ресурсов</summary>
        private bool _disposed = false;

        /// <summary>ручная очистка ресурсов</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>деструктор - завершитель запускаемый сборщиком мусора</summary>
        ~ProgressBgClass()
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
                    this._bgWorker.Dispose();
                    this._bgWorker = null;
                    this._progressBgDlg.Dispose();
                    this._progressBgDlg = null;
                    this._bgCancel.Dispose();
                    this._bgCancel = null;
                }
                //особождение неуправляемых ресурсов -> CloseHandle(handle);handle = IntPtr.Zero;
                this._disposed = true;
            }
        }

        #endregion

    }
}

