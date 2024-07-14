using AutoUpdaterDotNET;
using iNKORE.UI.WPF.Modern.Controls;
using SyncNotify.Helper;
using SyncNotify.Pages;
using SyncNotify.Pages.DiaglogPages;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
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


        //#region ISSUE
        //private static Settings settings = new Settings();
        //public static string settingsFileName = "Settings.json";
        //private FileSystemWatcher txtWatcher;
        //private FileSystemWatcher jsonWatcher;
        //private static NotificationFileManager notificationFileManager = new NotificationFileManager();
        //public static Message message = new Message();
        //SettingsManager settingsManager = new SettingsManager();
        //public void init()
        //{
        //    settings = settingsManager.GetSettingsByFile(Settings.settingsFileName);
        //    //txt监听器
        //    txtWatcher = new FileSystemWatcher();
        //    //监听路径
        //    txtWatcher.Path = settings.General.FolderLocation;
        //    //设置监听的属性
        //    txtWatcher.NotifyFilter = NotifyFilters.Attributes
        //                         | NotifyFilters.CreationTime
        //                         | NotifyFilters.DirectoryName
        //                         | NotifyFilters.FileName
        //                         | NotifyFilters.LastAccess
        //                         | NotifyFilters.LastWrite
        //                         | NotifyFilters.Security;

        //    //绑定事件
        //    txtWatcher.Created += new FileSystemEventHandler(Watcher_Created);
        //    //仅监听txt文件
        //    txtWatcher.Filter = "*.txt";
        //    txtWatcher.EnableRaisingEvents = true;




        //    //json监听器
        //    jsonWatcher = new FileSystemWatcher();
        //    jsonWatcher.Path = settings.General.FolderLocation;
        //    //监听json文件
        //    //设置监听的属性
        //    jsonWatcher.NotifyFilter = NotifyFilters.Attributes
        //                         | NotifyFilters.CreationTime
        //                         | NotifyFilters.DirectoryName
        //                         | NotifyFilters.FileName
        //                         | NotifyFilters.LastAccess
        //                         | NotifyFilters.LastWrite
        //                         | NotifyFilters.Security;
        //    //绑定事件
        //    jsonWatcher.Created += new FileSystemEventHandler(Watcher_Created);
        //    jsonWatcher.Filter = "*.json";
        //    jsonWatcher.EnableRaisingEvents = true;
        //}



        //private void Watcher_Created(object sender, FileSystemEventArgs e)
        //{
        //    string fileName = $"{e.Name}";

        //    //获取文件后缀名
        //    string extension = fileName.Substring(fileName.IndexOf("."));
        //    //对file进行属性设置

        //    SyncNotify.Message file = new Message();
        //    file.Property.FileName = $"{e.Name}";
        //    file.Property.FileLocation = $"{e.FullPath}";
        //    file.Property.FileCreatingTime = notificationFileManager.getFileCreatingDate($"{e.FullPath}"); ;
        //    file.Property.FileType = Path.GetExtension(fileName);
        //    file.Property.FileLocation = $"{e.FullPath}";

        //    if (file.Property.FileType.Length > 0)
        //    {
        //        if (file.Property.FileType == ".json")
        //        {
        //            new MessageDisplayHelper().displayJsonMessage(file);

        //        }
        //        else if (file.Property.FileType == ".txt")
        //        {
        //            new MessageDisplayHelper().displayTxtMessage(file);
        //        }
        //    }
        //}
        //#endregion

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


            //初始化默认页面
            MainFrame.Navigate(new RealTimeMessagePage());
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
        public void popUp()
        {
            //WindowState = WindowState.Normal;
            //Visibility = Visibility.Hidden;
            //Thread.Sleep(100);
            //Visibility = Visibility.Visible;
            if (!IsVisible)
            {
                Show();
            }

            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

            Activate();
            Topmost = true;  // important
            Topmost = false; // important
            Focus();         // important
        }
    }
}
