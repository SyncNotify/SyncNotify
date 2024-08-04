using iNKORE.UI.WPF.Modern.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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
            settings = settingsManager.GetSettingsByFile(Settings.settingsFileName);
            displayHistoryMessage();
        }

        private async void displayHistoryMessage()
        {
            // 显示加载状态
            progress_ring.Visibility = Visibility.Visible;
            // 异步加载控件
            List<UIElement> controls = await Task.Run(() => LoadControls());

            // 隐藏加载状态
            progress_ring.Visibility = Visibility.Collapsed;

            // 添加控件到UI
            foreach (var control in controls)
            {
                // 使用Dispatcher在UI线程中添加控件
                await Dispatcher.InvokeAsync(() => multiple_message_stack_panel.Children.Add(control));
            }


            //await Task.Run(() =>
            //{
            //    NotificationFileManager notificationFileManager = new NotificationFileManager();
            //    string[] messageContent = notificationFileManager.ReadTop50JsonFilesContent(settings.General.FolderLocation);
            //    string[] messageCreatingTime = notificationFileManager.ReadTop50TxtFilesCreatingTime(settings.General.FolderLocation);
                
            //        this.Dispatcher.Invoke(() =>
            //        {
            //        for (int i = 0; i < messageContent.Length; i++)
            //        {
            //            Grid grid = new Grid();
            //            StackPanel stackPanel = new StackPanel();
            //            ScrollViewer scrollViewer = new ScrollViewer();
            //            MessageDisplayControl messageDisplayControl = new MessageDisplayControl();
            //            messageDisplayControl.setMessage(messageContent[i], messageCreatingTime[i]);
            //            stackPanel.Children.Add(messageDisplayControl);
            //            grid.Children.Add(scrollViewer);
            //            scrollViewer.Content = stackPanel;
            //            multiple_message_stack_panel.Children.Add(grid);
            //            }
            //        });
                   
                
            //});
        }



        //private async void OnLoadButtonClick(object sender, RoutedEventArgs e)
        //{
        //    // 清空控件容器
        //    Container.Children.Clear();

        //    // 显示加载状态
        //    LoadingTextBlock.Visibility = Visibility.Visible;

        //    // 异步加载控件
        //    List<UIElement> controls = await Task.Run(() => LoadControls());

        //    // 隐藏加载状态
        //    LoadingTextBlock.Visibility = Visibility.Collapsed;

        //    // 添加控件到UI
        //    foreach (var control in controls)
        //    {
        //        // 使用Dispatcher在UI线程中添加控件
        //        await Dispatcher.InvokeAsync(() => Container.Children.Add(control));
        //    }
        //}

        private List<UIElement> LoadControls()
        {
            // 模拟加载控件的耗时操作
            List<UIElement> controls = new List<UIElement>();
            NotificationFileManager notificationFileManager = new NotificationFileManager();
            string[] messageContent = notificationFileManager.ReadTop50JsonFilesContent(settings.General.FolderLocation);
            string[] messageCreatingTime = notificationFileManager.ReadTop50TxtFilesCreatingTime(settings.General.FolderLocation);
            //for (int i = 0; i < 10; i++)
            //{
            //    // 模拟耗时任务
            //    Task.Delay(500).Wait();

            //    // 创建控件（例如Button）
            //    Button button = new Button
            //    {
            //        Content = $"Button {i + 1}",
            //        Margin = new Thickness(5),
            //        Width = 100,
            //        Height = 30
            //    };

            //    controls.Add(button);
            //}


            for (int i = 0; i < messageContent.Length; i++)
            {
                Dispatcher.Invoke(() =>
                {
                    Grid grid = new Grid();
                    StackPanel stackPanel = new StackPanel();
                    ScrollViewer scrollViewer = new ScrollViewer();
                    MessageDisplayControl messageDisplayControl = new MessageDisplayControl();
                    messageDisplayControl.setMessage(messageContent[i], messageCreatingTime[i]);
                    stackPanel.Children.Add(messageDisplayControl);
                    grid.Children.Add(scrollViewer);
                    scrollViewer.Content = stackPanel;
                    controls.Add(grid);
                });
            }
            return controls;
        }   

    }
}
