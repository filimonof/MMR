using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;


namespace mmr.plugins
{
    /// <summary>
    /// Класс для работы с плагинами
    /// </summary>
    [Serializable]
    public class PluginsClass : IDisposable
    {
        //MarshalByRefObject 

        /// <summary>Список плагинов настройки</summary>        
        public List<IOptionsMDIForm> PluginsOptions { get; private set; }

        /// <summary>Список плагинов отчётов</summary>        
        public List<IReportsMDIForm> PluginsReports { get; private set; }

        /// <summary>Список плагинов расчётов</summary>        
        public List<ICalculationsMDIForm> PluginsCalculations { get; private set; }

        /// <summary>Конструктор</summary>
        public PluginsClass()
        {
            this.PluginsOptions = new List<IOptionsMDIForm>();
            this.PluginsReports = new List<IReportsMDIForm>();
            this.PluginsCalculations = new List<ICalculationsMDIForm>();
        }

        /// <summary>Загрузка модулей в другом домене</summary>
        public void LoadPluginsNewDomain()
        {
            AppDomain domain = AppDomain.CreateDomain("mmr.plugins.loader");
            PluginsClass finder = (PluginsClass)domain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().GetName().Name + @".dll", typeof(mmr.plugins.PluginsClass).FullName);
            finder.GetPluginsFromCurrentDir();
            this.PluginsOptions = finder.PluginsOptions;
            this.PluginsReports = finder.PluginsReports;
            this.PluginsCalculations = finder.PluginsCalculations;
            AppDomain.Unload(domain);
        }

        /// <summary>Загрузка модулей в текущем домене</summary>
        public void LoadPlugins()
        {
            this.GetPluginsFromCurrentDir();
        }

        /// <summary>Загрузка модулей</summary>
        private void GetPluginsFromCurrentDir()
        {            
            DirectoryInfo dir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
            foreach (FileInfo files in dir.GetFiles(@"*.dll", SearchOption.TopDirectoryOnly))
            {
                //проверить в каждой dll, кроме mmr.plugins.dll, есть интерфейсы для выполнения
                if (!files.Name.Equals(@"mmr.plugins.dll"))
                {
                    Assembly dllAssembly = Assembly.LoadFrom(files.FullName);
                    foreach (Type type in dllAssembly.GetTypes())
                    {
                        if (type.GetInterface(typeof(IOptionsMDIForm).FullName) != null)
                        {
                            IOptionsMDIForm form = (IOptionsMDIForm)Activator.CreateInstance(type);
                            this.PluginsOptions.Add(form);
                        }
                        if (type.GetInterface(typeof(IReportsMDIForm).FullName) != null)
                        {
                            IReportsMDIForm form = (IReportsMDIForm)Activator.CreateInstance(type);
                            this.PluginsReports.Add(form);
                        }
                        if (type.GetInterface(typeof(ICalculationsMDIForm).FullName) != null)
                        {
                            ICalculationsMDIForm form = (ICalculationsMDIForm)Activator.CreateInstance(type);
                            this.PluginsCalculations.Add(form);
                        } 
                    }

                }
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                //}
            }
        }

        #region реализация интерфейса IDisposable
        /// <summary>поршло удаление ресурсов</summary>
        private bool _disposed = false;

        /// <summary>ручная очистка ресурсов</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>деструктор - завершитель запускаемый сборщиком мусора</summary>
        ~PluginsClass()
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
                    this.PluginsOptions.Clear();
                    this.PluginsOptions = null;
                    this.PluginsReports.Clear();
                    this.PluginsReports = null;
                    this.PluginsCalculations.Clear();
                    this.PluginsCalculations = null;
                }
                //особождение неуправляемых ресурсов -> CloseHandle(handle);handle = IntPtr.Zero;
                this._disposed = true;
            }
        }
        #endregion

    }
}
