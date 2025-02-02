using AutoUpdaterDotNET;
using iNKORE.UI.WPF.Modern.Controls;
using SyncNotify.Pages;
using SyncNotify.Pages.DiaglogPages;
using System;
using System.Collections.Generic;
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
        private EditWatcher watcher;
        //使用泛型定义dictionary，先初始化page对象，防止切换页面时page重新加载导致实时消息丢失
        private Dictionary<string, iNKORE.UI.WPF.Modern.Controls.Page> _pagesCache = new Dictionary<string, Page>();
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

            //构造函数中为静态属性赋值
            Instance = this;

            //初始化监听服务
            watcher = new EditWatcher();
            watcher.init();

            //展示任务栏图标
            showNotifyIcon();

            //开启更新服务
            AutoUpdater.Start("https://githubraw.com/onear233/SyncNotify/master/updateInfo.xml");


            
            //缓存页面
            _pagesCache["RealTimeMessagePage"] = new RealTimeMessagePage();
            _pagesCache["HistoryPage"] = new HistoryPage();
            _pagesCache["SettingsPage"] = new SettingsPage();
            _pagesCache["AboutPage"] = new AboutPage();
            //初始化默认页面
            MainFrame.Navigate(_pagesCache["RealTimeMessagePage"]);
            Title = "SyncNotify" + " " + InternalProper.getVersion();
            navigationView.PaneTitle = Title;


        }

        private void showNotifyIcon()
        {
            //this.notifyIcon = new NotifyIcon();
            //this.notifyIcon.Text = "SyncNotify";
            //this.notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            //this.notifyIcon.Visible = true;
            ////打开菜单项
            //System.Windows.Controls.MenuItem open = new System.Windows.Controls.MenuItem();
            //open.Click += new EventHandler(Show);
            ////退出菜单项
            //System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            //exit.Click += new EventHandler(Close);
            ////关联托盘控件
            //System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { open, exit };
            //notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            //this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler((o, e) =>
            //{
            //    if (e.Button == MouseButtons.Left) this.Show(o, e);
            //});
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
                    MainFrame.Navigate(_pagesCache["RealTimeMessagePage"]);
                    break;
                case "announcement":
                    MainFrame.Navigate(new ComingSoon());
                    break;
                case "settings":
                    MainFrame.Navigate(_pagesCache["SettingsPage"]);
                    break;
                case "about":
                    MainFrame.Navigate(_pagesCache["AboutPage"]);
                    break;
                case "historyMessage":
                    MainFrame.Navigate(_pagesCache["HistoryPage"]);
                    break;
            }
        }

        //窗口弹出到所有窗口之前的逻辑
        public void popUp()
        {
            if (!IsVisible)
            {
                Show();
            }

            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

            Activate();
            Topmost = true;
            Topmost = false; 
            Focus();         
        }
    }
}
