using iNKORE.UI.WPF.Modern.Controls;
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
    public partial class RealTimeMessagePage : iNKORE.UI.WPF.Modern.Controls.Page
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
                    Send_Time_TextBlock.Text = InternalProper.RecentTime;
                    Display_Time_TextBlock.Text = InternalProper.RecentTime;
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

        private void DoNotDisturb_Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = ContentDialog;
            dialog.Title = "确定要这么做吗？";
            dialog.PrimaryButtonText = "是（本功能仍在开发中 暂时无效）";
            dialog.SecondaryButtonText = "否";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.ShowAsync();
            ContentDialog_TextBlock.Text = "该操作会导致消息不再弹出到所有窗口之前，可能导致消息遗漏！\r您可能要为可能造成的损失负责！\r（注意：本设置不影响消息收取，打开主面板仍能看到最新消息，且会被json消息中的“立即弹出”覆盖）";



        }
    }
}
