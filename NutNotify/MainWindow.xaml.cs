using iNKORE.UI.WPF.Modern.Common;
using iNKORE.UI.WPF.Modern.Controls;
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
using AutoUpdaterDotNET;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

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

            AutoUpdater.AppCastURL = "https://raw.gitmirror.com/onear233/NutNotify/master/updateInfo.xml";
            AutoUpdater.Start("https://raw.gitmirror.com/onear233/NutNotify/master/updateInfo.xml");
            //_mainWindowVisibility = Visibility.Hidden;
            // 初始化默认页面
            MainFrame.Navigate(new RealTimeMessagePage());

            //测试用
            //Visibility = Visibility.Hidden;
        }
        public static void bridgeForResponse(string value)
        {
            if(value != null)
            {
                
            }
        }

        
        

        private void navigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            // 获取选中项的标签
            string selectedItemTag = args.InvokedItemContainer.Tag.ToString();
            // 根据标签切换页面
            switch (selectedItemTag)
            {
                case "realTimeMessage":
                    MainFrame.Navigate(new RealTimeMessagePage());
                    break;
                case "announcement":
                    MainFrame.Navigate(new AnnounceMentPage());
                    break;
                    // 添加其他页面的处理
            }
        }
    }
}
