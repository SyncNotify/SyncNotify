using iNKORE.UI.WPF.Modern;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace SyncNotify.Pages
{
    /// <summary>
    /// MessageDisplayControl.xaml 的交互逻辑
    /// </summary>
    public partial class MessageDisplayControl : UserControl
    {
        private Message savedFile;
        private string accentButtonStyleKey;
        private string defaultButtonStyleKey;
        private int textSize;
        private bool isTextHighlighted;
        public MessageDisplayControl()
        {
            InitializeComponent();

            //初始化按钮资源样式

            Dispatcher.Invoke(() =>
            {
                accentButtonStyleKey = ThemeKeys.AccentButtonStyleKey;
                defaultButtonStyleKey = ThemeKeys.DefaultButtonStyleKey;
                isTextHighlighted = false;
                refreshFontState();
            });



        }

        //接收消息的方法（实时消息）
        public void receiveMessage(SyncNotify.Message message)
        {
            savedFile = message;
            notificationTextBlock.Dispatcher.Invoke(() =>
            {
                //设置消息内容
                notificationTextBlock.Text = savedFile.Property.FileContent;
                Send_Time_TextBlock.Text = savedFile.Property.FileCreatingTime;
                Display_Time_TextBlock.Text = savedFile.Display.FileDisplayTime;
                //显示消息类型
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
                //高亮字体大小按钮
                font_size_button.SetValue(StyleProperty, Application.Current.Resources[accentButtonStyleKey]);
                notificationTextBlock.FontSize = 55;
            });
        }

        //手动设置消息内容的方法
        //可以用于历史消息
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

        private void Font_Size_Button_Click(object sender, RoutedEventArgs e)
        {
            refreshFontState();
        }

        private void setFontClickedStyle()
        {
            font_size_button.SetValue(StyleProperty, Application.Current.Resources[defaultButtonStyleKey]);
            notificationTextBlock.FontSize = 20;
        }

        private void setFontDefaultStyle()
        {
            font_size_button.SetValue(StyleProperty, Application.Current.Resources[accentButtonStyleKey]);
            notificationTextBlock.FontSize = 55;
        }

        private void refreshFontState()
        {
            textSize = (int)notificationTextBlock.FontSize;
            switch (textSize)
            {
                case 55:
                    setFontClickedStyle();
                    break;
                case 20:
                    setFontDefaultStyle();
                    break;
                default:
                    break;

            }
        }

        private void Hightlight_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isTextHighlighted)
            {
                highlight_button.SetValue(StyleProperty, Application.Current.Resources[accentButtonStyleKey]);
                notificationTextBlock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF9C4"));
                isTextHighlighted = true;
            }
            else
            {
                highlight_button.SetValue(StyleProperty, Application.Current.Resources[defaultButtonStyleKey]);
                notificationTextBlock.Background = new SolidColorBrush(Colors.Transparent);
                isTextHighlighted = false;
            }
        }
    }
}
