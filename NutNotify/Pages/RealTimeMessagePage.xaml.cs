using Microsoft.Toolkit.Uwp.Notifications;
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

namespace NutNotify
{
    /// <summary>
    /// RealTimeMessagePage.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimeMessagePage : Page
    {
        public static RealTimeMessagePage Instance { get; private set; }
        public RealTimeMessagePage()
        {
            // 构造函数中为静态属性赋值
            Instance = this;
            InitializeComponent();
            MessageBox.Show("这应该是个构造函数吧");
        }


        public void responseGetter(string value)
        {
            //_mainWindowVisibility = Visibility.Visible;

            notificationTextBlock.Dispatcher.Invoke(() =>
            {
                notificationTextBlock.Text = value;
            });
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
                   .AddText("打开主界面查看")
                   .AddAudio(new Uri(Environment.GetEnvironmentVariable("SYSTEMROOT") + "\\Media\\Windows Ding.wav"))
                   .Show();
        }
    }
}
