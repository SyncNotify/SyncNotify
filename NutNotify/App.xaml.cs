﻿using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        public static string RootPath = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas\\";
        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
        }


        void App_Startup(object sender, StartupEventArgs e)
        {
            bool ret;
            mutex = new System.Threading.Mutex(true, "SyncNotify", out ret);

            if (!ret && !e.Args.Contains("-m")) //-m multiple
            {
                MessageBox.Show("已有一个程序实例正在运行（请前往右下角托盘图标寻找）");
                Environment.Exit(0);
            }

            StartArgs = e.Args;

        }
    }
}
