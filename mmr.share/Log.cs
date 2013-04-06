using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace mmr.share
{
    /// <summary>
    /// Класс ведения логов
    /// </summary>
    public static class Log
    {

        /// <summary>вывод в журнал сообщений (Event Viewer)</summary>
        /// <param name="message">Строка с сообщением</param>
        public static void ToEventViewer(string message)
        {
            ToEventViewer(message, EventLogEntryType.Information);
        }

        /// <summary>вывод в журнал сообщений (Event Viewer)</summary>
        /// <param name="message">Строка с сообщением</param>
        /// <param name="type">тип сообщения</param>
        public static void ToEventViewer(string message, EventLogEntryType type)
        {
            if(!EventLog.SourceExists("MMR"))
                EventLog.CreateEventSource("MMR", "Макеты Мордовского РДУ");
            
            EventLog.WriteEntry("MMR", message, type);
        }

        /// <summary>вывод в файл</summary>
        /// <param name="message">строка с сообщением</param>        
        public static void ToFile(string message)
        {
            StreamWriter sw = null;
            try
            {
                string filename = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "log.txt");
                sw = File.AppendText(filename);
                //todo : вывести  дату и число в логи 
                sw.WriteLine(message);
            }
            catch { }
            finally
            {
                if (sw != null) sw.Close();
            }
        }
    }
}
