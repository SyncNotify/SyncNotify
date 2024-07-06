using SyncNotify.Helper;
using System.Windows;

namespace SyncNotify
{
    /// <summary>
    /// WelcomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        internal static Settings settings = new Settings();
        SettingsManager settingsManager = new SettingsManager();

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void Listen_Button_Click(object sender, RoutedEventArgs e)
        {
            //保存到文件
            FileSelectHelper fileSelectHelper = new FileSelectHelper();
            string filePath = fileSelectHelper.getFolderPath("选择要监听的文件夹（暂时只支持一个）");
            //保存设置到文件
            if (settings == null)
            {
                Settings settings = new Settings();
                settings.General.FolderLocation = filePath;
                settingsManager.SaveSettingsToFile(settings);
            }
            else
            {
                settings.General.FolderLocation = filePath;
                settingsManager.SaveSettingsToFile(settings);
            }

        }
    }
}
