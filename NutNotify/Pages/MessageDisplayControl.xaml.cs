using System.Windows;
using System.Windows.Controls;

namespace SyncNotify.Pages
{
    /// <summary>
    /// MessageDisplayControl.xaml 的交互逻辑
    /// </summary>
    public partial class MessageDisplayControl : UserControl
    {
        private Message savedFile;

        public MessageDisplayControl()
        {
            InitializeComponent();
        }

        //接收消息的方法
        public void receiveMessage(SyncNotify.Message message)
        {
            savedFile = message;
            notificationTextBlock.Dispatcher.Invoke(() =>
            {
                notificationTextBlock.Text = savedFile.Property.FileContent;
                Send_Time_TextBlock.Text = savedFile.Property.FileCreatingTime;
                Display_Time_TextBlock.Text = savedFile.Display.FileDisplayTime;
                if (savedFile.Display.FileDisplayTime != null)
                {
                    if (savedFile.Display.FileDisplayTime.Equals(savedFile.Property.FileCreatingTime))
                    {
                        RealTime_Message.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Sheduled_Message.Visibility = Visibility.Visible;
                    }
                }
                else{
                    RealTime_Message.Visibility = Visibility.Visible;
                }

            });

        }

        //手动设置消息内容的方法
        public void setMessage(string messageContent, string creatingTime)
        {
            if (messageContent != null)
            {
                notificationTextBlock.FontSize = 20;
                notificationTextBlock.Text = messageContent;
                Send_Time_TextBlock.Text = creatingTime;
                Display_Time_TextBlock.Text = creatingTime;
            }
        }
    }
}
