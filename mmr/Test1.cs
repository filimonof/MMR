using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace mmr
{
    class Test1
    {
        vit.progress.ProgressBgClass dlg;

        public Test1()
        {
            dlg = new vit.progress.ProgressBgClass();
        }

        public void StartTest()
        {
            dlg.Title = @" Выполнение задачи ...";
            dlg.Caption = @"Тестируем компоненту на пригодность";
            dlg.Cancelled = true;
            dlg.Extended = true;
            dlg.AsyncDialog = true;            

            dlg.DoWork += new DoWorkEventHandler(Work);
            dlg.OnComplete += new RunWorkerCompletedEventHandler(Complete);
            dlg.DoCancel += new DoWorkEventHandler(Cancelate);

            dlg.Start();
        }

        int _pc = 100;

        public void Work(object sender, DoWorkEventArgs e)
        {
            //задача работает в потоке и любое взаимодействие с оконным интерфейсом должно быть Invoke
            dlg.WriteComment("Выполнение задания");
            int i;
            for (i = 0; i < 50; i++)
            {
                dlg.IsError = true;
                dlg.WriteComment("Значение " + i.ToString());
                System.Threading.Thread.Sleep(500);
                if (dlg.IsCancellAndProgress(i * 2, ref e))
                {
                    _pc = i;
                    return;
                }
            }
            e.Result = i;    // будет передано в RunWorkerComрleted
        }

        void Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            //выполняется после завершения потока но с открытым окном
            if (dlg.IsError)
                dlg.WriteComment("Выполнено с ошибками");

            dlg.WriteComment("Код по завершению " + e.Result);
        }

        void Cancelate(object sender, DoWorkEventArgs e)
        {
            //задача работает в потоке и любое взаимодействие с оконным интерфейсом должно быть Invoke
            dlg.WriteComment("Код по отмене");
            System.Threading.Thread.Sleep(3000);
            for (int i = _pc; i > 0; i--)
            {
                dlg.WriteComment("Отмена Значения " + i.ToString());
                System.Threading.Thread.Sleep(200);
                dlg.ChangeProgress(i * 2);
            }
        }


    }
}
