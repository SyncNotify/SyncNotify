using System;
using System.Linq;
using System.Windows;

namespace SyncNotify
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        System.Threading.Mutex mutex;
        public static string[] StartArgs = null;
        public static string RootPath = Environment.GetEnvironmentVariable("APPDATA") + "\\SyncNotify\\";
        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
        }

        //异常处理（临时解决方法）
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Stop);
            e.Handled = true;
        }

        //重复打开的提示
        void App_Startup(object sender, StartupEventArgs e)
        {
            bool ret;
            mutex = new System.Threading.Mutex(true, "SyncNotify", out ret);

            if (!ret && !e.Args.Contains("-m")) //-m multiple
            {
                MessageBox.Show("已有一个程序实例正在运行","Exception", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0);
            }

            StartArgs = e.Args;

        }
    }
}
