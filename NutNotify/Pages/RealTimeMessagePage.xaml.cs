using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json.Linq;
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

namespace SyncNotify
{
    /// <summary>
    /// RealTimeMessagePage.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimeMessagePage : Page
    {
        public static RealTimeMessagePage Instance { get; private set; }
        private File savedFile;
        public RealTimeMessagePage()
        {
            // 构造函数中为静态属性赋值
            Instance = this;
            InitializeComponent();
            recoverText();
        }

        private void recoverText()
        {
            if (InternalProper.RecentText != null)
            {
                notificationTextBlock.Dispatcher.Invoke(() =>
                {
                    notificationTextBlock.Text = InternalProper.RecentText;
                });
            }
        }

        //刷新消息用，由别的类来通知来消息，然后根据File里的东西进行刷新
        public void refeshMessage(SyncNotify.File file)
        {
            savedFile = file;
            notificationTextBlock.Dispatcher.Invoke(() =>
            {
                notificationTextBlock.Text = savedFile.FileContent;
                Send_Time_TextBlock.Text = savedFile.FileCreationDate;
                Display_Time_TextBlock.Text = savedFile.FileCreationDate;
            });
            popUp();
        }

        private void popUp()
        {
            MainWindow.Instance.Dispatcher.Invoke(() =>
            {

                MainWindow.Instance.WindowState = WindowState.Normal;
                MainWindow.Instance.Visibility = Visibility.Hidden;
                MainWindow.Instance.Visibility = Visibility.Visible;

            });
            new ToastContentBuilder()
                   .AddArgument("action", "viewConversation")
                   .AddArgument("conversationId", 9813)
                   .AddText("您有一条新消息")
                   .AddText(savedFile.FileContent)
                   .Show();
        }
    }
}
