using iNKORE.UI.WPF.Modern.Controls;
using SyncNotify.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using IWshRuntimeLibrary;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyncNotify.Pages
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    //使用“iNKORE.UI.WPF.Modern.Controls.Page”是因为wpf也自带了一个page，需要有明确的引用
    //关于xaml中的文件：可以把schemas.inkore.net设置为默认命名空间，这样子组件库的组件就不要前缀，但是wpf默认的东西就要前缀了
    public partial class SettingsPage : iNKORE.UI.WPF.Modern.Controls.Page
    {
        public static Settings settings = new Settings();

        public SettingsPage()
        {
            InitializeComponent();
            restoreSettings();
        }

        private void restoreSettings()
        {
            //把本页面的settings对象进行赋值
            settings = SettingsManager.GetSettingsByFile(Settings.settingsFileName, settings);
            //读取属性
            if (settings != null)
            {
                AutoStartup_Toggle.IsOn = settings.General.AutoStartup;

            }
        }

        private void Auto_Startup_Toggle(object sender, RoutedEventArgs e)
        {
            settings.General.AutoStartup = AutoStartup_Toggle.IsOn;
            SettingsManager.SaveSettingsToFile(settings);
            AutoStartupHelper helper = new AutoStartupHelper();
            if (AutoStartup_Toggle.IsOn)
            {
                helper.StartAutomaticallyCreate(Application.ResourceAssembly.GetName().Version.ToString() + ".exe");
            }
            else
            {
                helper.StartAutomaticallyDel(Application.ResourceAssembly.GetName().Version.ToString() + ".exe");
            }

        }

    }
}
