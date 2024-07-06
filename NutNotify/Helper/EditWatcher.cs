using FileWatcherEx;
using SyncNotify.Helper;
using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace SyncNotify
{
    internal class EditWatcher
    {
        public static Settings settings = new Settings();
        public static string settingsFileName = "Settings.json";
        private FileSystemWatcher txtWatcher;
        private FileSystemWatcher jsonWatcher;
        public static NotificationFileManager notificationFileManager = new NotificationFileManager();
        public static Message message = new Message();
        SettingsManager settingsManager = new SettingsManager();
        public void init()
        {
            settings = settingsManager.GetSettingsByFile(Settings.settingsFileName);
            //txt监听器
            txtWatcher = new FileSystemWatcher();
            //监听路径
            txtWatcher.Path = "D:\\sync\\通知";
            //设置监听的属性
            txtWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security;
            
            //绑定事件
            txtWatcher.Created += new FileSystemEventHandler(Watcher_Created);
            //仅监听txt文件
            txtWatcher.Filter = "*.txt";
            txtWatcher.EnableRaisingEvents = true;




            //json监听器
            jsonWatcher = new FileSystemWatcher();
            jsonWatcher.Path = "D:\\sync\\通知";
            //监听json文件
            //设置监听的属性
            jsonWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security;
            //绑定事件
            jsonWatcher.Created += new FileSystemEventHandler(Watcher_Created);
            jsonWatcher.Filter = "*.json";
            jsonWatcher.EnableRaisingEvents = true;
            jsonWatcher.Error += OnError;
        }
        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
                MessageBox.Show(ex.Message );
            }
        }


        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileName = $"{e.Name}";

            //获取文件后缀名
            string extension = fileName.Substring(fileName.IndexOf("."));
            //对file进行属性设置

            SyncNotify.Message file = new Message();
            file.Property.FileName = $"{e.Name}";
            file.Property.FileLocation = $"{e.FullPath}";
            file.Property.FileCreatingTime = notificationFileManager.getFileCreatingDate($"{e.FullPath}"); ;
            file.Property.FileType = Path.GetExtension(fileName);
            file.Property.FileLocation = $"{e.FullPath}";

            if (file.Property.FileType.Length > 0)
            {
                if (file.Property.FileType == ".json")
                {
                    new MessageDisplayHelper().displayJsonMessage(file);

                }
                else if (file.Property.FileType == ".txt")
                {
                    new MessageDisplayHelper().displayTxtMessage(file);
                }
            }
        }






    }
}
