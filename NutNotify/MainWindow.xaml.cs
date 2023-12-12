using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static MainWindow Instance { get; private set; }
        private static Visibility _mainWindowVisibility;
        private static string _notificationText;
        public Visibility mainWindowVisibility
        {
            get
            {
                return _mainWindowVisibility;
            }
            set
            {
                _mainWindowVisibility = value;

                RaisePropertyChanged("mainWindowVisibility");
            }
        }

        public string notificationText
        {
            get
            {
                return _notificationText;
            }
            set
            {
                _notificationText = value;

                RaisePropertyChanged("notificationText");
            }
        }

        //public string mainWindowVisibility
        //{
        //    get
        //    {
        //        return mainWindowVisibility;
        //    }
        //    set
        //    {
        //        mainWindowVisibility = value;

        //        RaisePropertyChanged("mainWindowVisibility");
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            // 构造函数中为静态属性赋值
            Instance = this;
            EditWatcher watcher = new EditWatcher();
            watcher.init();
            //_mainWindowVisibility = Visibility.Hidden;
            Visibility = Visibility.Hidden;
        }
        public static void bridgeForResponse(string value)
        {
            if(value != null)
            {
                
            }
        }

        public void responseGetter(string value)
        {
            //_mainWindowVisibility = Visibility.Visible;
            notificationTextBlock.Dispatcher.Invoke(() =>
            {
                notificationTextBlock.Text = value;
            });
            this.Dispatcher.Invoke(() =>
            {
                WindowState = WindowState.Normal;
                this.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Visible;

            });
            new ToastContentBuilder()
                   .AddArgument("action", "viewConversation")
                   .AddArgument("conversationId", 9813)
                   .AddText("您有一条新消息")
                   .AddText("打开主界面查看")
                   .AddAudio(new Uri(Environment.GetEnvironmentVariable("SYSTEMROOT") + "\\Media\\Windows Ding.wav") )
                   .Show();
        }
    }
}
