using AutoUpdaterDotNET;
using iNKORE.UI.WPF.Modern.Controls;
using SyncNotify.Pages;
using SyncNotify.Pages.DiaglogPages;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace SyncNotify
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static MainWindow Instance { get; private set; }
        private static Visibility _mainWindowVisibility;
        private static string _notificationText;
        private NotifyIcon notifyIcon;
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

            showNotifyIcon();
            AutoUpdater.Start("https://cdn.githubraw.com/onear233/SyncNotify/master/updateInfo.xml");
            //_mainWindowVisibility = Visibility.Hidden;
            // 初始化默认页面
            MainFrame.Navigate(new RealTimeMessagePage());
            Title = "SyncNotify" + " " + InternalProper.getVersion();
            //测试用
            //Visibility = Visibility.Hidden;

            navigationView.PaneTitle = Title;

        }

        private void showNotifyIcon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.Text = "SyncNotify";
            this.notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.notifyIcon.Visible = true;
            //打开菜单项
            System.Windows.Forms.MenuItem open = new System.Windows.Forms.MenuItem("打开主界面");
            open.Click += new EventHandler(Show);
            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += new EventHandler(Close);
            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { open, exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler((o, e) =>
            {
                if (e.Button == MouseButtons.Left) this.Show(o, e);
            });
        }



        private void Show(object sender, EventArgs e)
        {

            this.Visibility = System.Windows.Visibility.Visible;
            this.Activate();
        }
        protected override void OnClosed(EventArgs e)
        {
            this.ShowInTaskbar = false;
        }

        private void Hide(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Close(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public static void bridgeForResponse(string value)
        {
            if (value != null)
            {

            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "关闭窗口已被禁用";
            dialog.PrimaryButtonText = "转为悬浮窗";
            dialog.SecondaryButtonText = "取消";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.ShowAsync();
            //this.Visibility = Visibility.Hidden;
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
                    MainFrame.Navigate(new ComingSoon());
                    break;
                case "settings":
                    MainFrame.Navigate(new SettingsPage());
                    break;
                case "about":
                    MainFrame.Navigate(new AboutPage());
                    break;
                case "historyMessage":
                    //MainFrame.Navigate(new HistoryPage());
                    MainFrame.Navigate(new HistoryPage());
                    break;
            }
        }
    }
}
