using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Windows.Devices.Sensors;

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
        public void receiveMessage(SyncNotify.Message file)
        {
            savedFile = file;
            notificationTextBlock.Dispatcher.Invoke(() => {
                
                notificationTextBlock.Text = savedFile.Property.FileContent;
                Send_Time_TextBlock.Text = savedFile.Property.FileCreatingTime;
                Display_Time_TextBlock.Text = savedFile.Property.FileCreatingTime;
            });

        }

        public void setMessage(string  messageContent,string creatingTime) 
        { 
            if(messageContent != null) 
            {
                notificationTextBlock.FontSize = 20;
                notificationTextBlock.Text = messageContent;
                 Send_Time_TextBlock.Text = creatingTime;
                Display_Time_TextBlock.Text = creatingTime;
            }
        }
    }
}
