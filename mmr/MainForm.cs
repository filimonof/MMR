using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using mmr.plugins;

namespace mmr
{
    #region comment
    /*
Cheburator Report 
Чебуратор отчётов
CO
CR
ChR
 System.Diagnostics.Process.Start
 * 
 * автообновление 
     using System.Reflection;
    using System.IO;

    ...

    // Get current and updated assemblies
    AssemblyName currentAssemblyName = AssemblyName.GetAssemblyName(currentAssemblyPath);
    AssemblyName updatedAssemblyName = AssemblyName.GetAssemblyName(updatedAssemblyPath);

    // Compare both versions
    if (updatedAssemblyName.Version.CompareTo(currentAssemblyName.Version) <= 0)
    {
        // There's nothing to update
        return;
    }

    // Update older version
    File.Copy(updatedAssemblyPath, currentAssemblyPath, true);
 * 
 * 
     
   
 Чтобы вернуть имя файла без расширения и даже без пути местоположения на диске, 
 то используйте метод GetFileNameWithoutExtension класса Path из пространства имён System.IO.          

 * 
 * class MyApplication 
    { 
     public static void Main() 
     { 
     if ( InstanceExists() )
     { 
     return; 
     } Console.WriteLine( "Application is running: Press enter to exit" );
     Console.ReadLine();
     } 
        static Mutex mutex; static bool InstanceExists() 
     { 
     bool createdNew; 
     mutex = new Mutex( false, "My Mutex Name", out createdNew ); 
     return !createdNew;
     } 
    }
*/

    //FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo("filename.exe");
    //AssemblyName.GetAssemblyName("filename.exe").Version

    //Directory.GetFiles(_strPath, "*.*", SearchOption.AllDirectories) 
    //System.IO.Path.GetDirectoryName("ur path");

    /*
     * 
    DirectoryInfo dir = new DirectoryInfo(Application.ExecutablePath);
    foreach(FileInfo files in dir.GetFiles("*.dll", SearchOption.TopDirectoryOnly))
    {
        //проверить есть ли код для запуска

        Assembly dllAssembly = Assembly.Load(files.FullName);
                
        //Form NewForm = myAssembly.CreateInstance("FormName", ...., FormArgsArray, .....); 

        Type dllType = dllAssembly.GetType("mmr");
        object myObject = Activator.CreateInstance(myType);
        // Предположим, метод, который мы собираемся вызвать, принимает три параметра:
        // Подготовим массив параметров
        object[] arguments = new object[] { "argument1", 2, 3.0 };

        // Один вариант вызова метода - сперва получить его описание
        MethodInfo method = myType.GetMethod("MyMethod");
        method.Invoke(myObject, arguments);

        // А можно и напрямую - с помощью метода Type.InvokeMember
        myType.InvokeMember("MyMethod", BindingFlags.InvokeMethod | BindingFlags.Public, null, myObject, arguments);




        System.Reflection.Assembly dll = System.Reflection.Assembly.LoadFrom("имя");
        Type dllClass = dll.GetType("класс с пространством имен"); ;
        System.Reflection.MethodInfo dllMethod = dllClass.GetMethod("функция");
        Object obj = Activator.CreateInstance(dllClass);
        dllMethod.Invoke(obj, args); // args - список аргументов




    }

     **/

    /* splash screen
     * 
     using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace WindowsFormsApplication1 {
  static class Program {
    [STAThread]
    static void Main(string[] args) {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      new MyApp().Run(args);
    }
  }
  class MyApp : WindowsFormsApplicationBase {
    protected override void OnCreateSplashScreen() {
      this.SplashScreen = new frmSplash();
    }
    protected override void OnCreateMainForm() {
      // Do your time consuming stuff here...
      //...
      System.Threading.Thread.Sleep(3000);
      // Then create the main form, the splash screen will close automatically
      this.MainForm = new Form1();
    }
  }
}

     */
    #endregion

    /// <summary>
    /// Главное окно программы
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Подключенные плагины
        /// </summary>
        private PluginsClass _plugins;

        /// <summary>
        /// Получение имени из AssemblyInfo
        /// </summary>        
        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Конструктор<
        /// /summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0}  (версия {1}.{2} build {3} revision {4})",
                this.AssemblyTitle,
                Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(),
                Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(),
                Assembly.GetExecutingAssembly().GetName().Version.Build.ToString(),
                Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString());

            //подключаем плагины
            this._plugins = new PluginsClass();
            this._plugins.LoadPluginsNewDomain();
            this.CreatePluginMenu<IOptionsMDIForm>(this._plugins.PluginsOptions, this.menuOptions);
            this.CreatePluginMenu<IReportsMDIForm>(this._plugins.PluginsReports, this.menuReports);
            this.CreatePluginMenu<ICalculationsMDIForm>(this._plugins.PluginsCalculations, this.menuCalculations);
            this.HideEmptyMenu();

            //Вызываем сборщик мусора
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Создание меню из плагинов
        /// </summary>
        /// <typeparam name="IType">Тип (интерфейс) создаваемого в меню плагина</typeparam>
        /// <param name="listPlugins">список плагинов данного типа (интерфейса)</param>
        /// <param name="mainMenu">пункт в меню куда добавляем</param>
        private void CreatePluginMenu<IType>(List<IType> listPlugins, ToolStripMenuItem mainMenu)
            where IType : IPluginsToMenu
        {
            foreach (IType plugin in listPlugins)
            {
                ToolStripMenuItem itemParent = mainMenu;
                if (!string.IsNullOrEmpty(plugin.ParentMenu))
                {
                    //ищем родительскте меню по названию
                    foreach (ToolStripMenuItem item in itemParent.DropDownItems)
                    {
                        if (item.Text.Equals(plugin.ParentMenu))
                            itemParent = item;
                    }
                    //если не нашли то создаём
                    if (itemParent == mainMenu)
                        itemParent = (ToolStripMenuItem)mainMenu.DropDownItems.Add(plugin.ParentMenu);
                }

                IType plugin_tmp = plugin;
                EventHandler handlerClick = (sender, args) => OnPluginClick<IType>(plugin_tmp);

                itemParent.DropDownItems.Add(plugin.NameForm, plugin.Icon != null ? plugin.Icon.ToBitmap() : null, handlerClick);
            }
        }

        /// <summary>
        /// Событие - выбор пункта меню и загрузка соответствующей формы
        /// </summary>
        /// <typeparam name="IType">интёрфейс (тип) открываемой формы</typeparam>
        /// <param name="plugin">Какая формы должна открыться</param>
        private void OnPluginClick<IType>(IType plugin)
            where IType : IPluginsToMenu
        {
            //Application.DoEvents();
            this.Refresh();

            Form pluginForm = (Form)Activator.CreateInstance(plugin.GetType());

            //если форма открыта то переходим на неё
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name.Equals(pluginForm.Name))
                {
                    frm.BringToFront();
                    pluginForm = null;
                    return;
                }
            }
            pluginForm.MdiParent = this;
            pluginForm.Show();
            pluginForm.BringToFront();
            pluginForm.WindowState = FormWindowState.Maximized;
            return;
        }

        /// <summary>
        /// Спрятать пустые пункты меню
        /// </summary>
        private void HideEmptyMenu()
        {
            this.menuOptions.Visible = this.menuOptions.DropDownItems.Count != 0;
            this.menuReports.Visible = this.menuReports.DropDownItems.Count != 0;
            this.menuCalculations.Visible = this.menuCalculations.DropDownItems.Count != 0;
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window ";
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStrip.Visible = this.toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.statusStrip.Visible = this.statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }
    }
}
