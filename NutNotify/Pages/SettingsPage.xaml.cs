using SyncNotify.Helper;
using System.Windows;

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
        SettingsManager settingsManager = new SettingsManager();

        public SettingsPage()
        {
            InitializeComponent();
            restoreSettings();
        }

        private void restoreSettings()
        {
            //把本页面的settings对象进行赋值
            settings = settingsManager.GetSettingsByFile(Settings.settingsFileName, settings);
            //读取属性
            if (settings != null)
            {
                AutoStartup_Toggle.IsOn = settings.General.AutoStartup;
                //首先在页面加载时移除监听事件
                ComboboxItem_SelectFile.Selected -= ComboBoxItem_FileSelect_Selected;
                ComboboxItem_SelectFile.Content = settings.Message.MessageArrivalSound;
                ComboboxItem_SelectFile.IsSelected = true;
                //重新添加监听事件
                ComboboxItem_SelectFile.Selected += ComboBoxItem_FileSelect_Selected;
            }
        }

        private void Auto_Startup_Toggle(object sender, RoutedEventArgs e)
        {
            //保存设置到文件
            settings.General.AutoStartup = AutoStartup_Toggle.IsOn;
            settingsManager.SaveSettingsToFile(settings);
            //创建启动项
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

        //Combobox的选择监听方法
        private void ComboBoxItem_FileSelect_Selected(object sender, RoutedEventArgs e)
        {

            FileSelectHelper fileSelectHelper = new FileSelectHelper();
            string filePath = fileSelectHelper.getFilePath("MP3文件|*.mp3|WAV文件|*.wav", "打开音频文件");
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            ComboboxItem_SelectFile.Content = fileName;
            //保存设置到文件
            settings.Message.MessageArrivalSound = filePath;
            settingsManager.SaveSettingsToFile(settings);
        }

        private void Listen_Button_Click(object sender, RoutedEventArgs e)
        {
            //保存到文件
            FileSelectHelper fileSelectHelper = new FileSelectHelper();
            string filePath = fileSelectHelper.getFolderPath("选择要监听的文件夹（暂时只支持一个）");
            //保存设置到文件
            if(settings == null)
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
