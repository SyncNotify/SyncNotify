using System.Windows;
using System.Windows.Controls;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace SyncNotify.Pages.DiaglogPages
{
    /// <summary>
    /// HistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryPage : Page
    {
        //需要一个settings对象
        internal static Settings settings = new Settings();

        public HistoryPage()
        {
            InitializeComponent();
            //把本页面的settings对象进行赋值
         SettingsManager settingsManager = new SettingsManager();

        settings = settingsManager.GetSettingsByFile(Settings.settingsFileName, settings);
            displayHistoryMessage();
        }

        private void displayHistoryMessage()
        {
            NotificationFileManager notificationFileManager = new NotificationFileManager();
            string[] messageContent = notificationFileManager.ReadTop10TxtFilesContent(settings.General.FolderLocation);
            string[] messageCreatingTime = notificationFileManager.ReadTop10TxtFilesCreatingTime(settings.General.FolderLocation);
            for(int i=0 ; i < messageContent.Length; i++)
            {
                Grid grid = new Grid();
                StackPanel stackPanel = new StackPanel();
                ScrollViewer scrollViewer = new ScrollViewer();
                MessageDisplayControl messageDisplayControl = new MessageDisplayControl();
                messageDisplayControl.setMessage(messageContent[i], messageCreatingTime[i]);
                stackPanel.Children.Add(messageDisplayControl);
                grid.Children.Add(scrollViewer);
                scrollViewer.Content = stackPanel;
                multiple_message_stack_panel.Children.Add(grid);
            }
        }
        
    }
}
