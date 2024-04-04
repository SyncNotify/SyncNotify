using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
    public partial class SettingsPage : Page
    {
        public static Settings Settings = new Settings();

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Auto_Startup_Toggle(object sender, RoutedEventArgs e)
        {
            Settings.General.AutoStartup = AutoStartup_Toggle.IsOn;
            SettingsManager.SaveSettingsToFile(Settings);
        }
    }
}
