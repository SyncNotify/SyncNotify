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
        private static Settings settings = new Settings();
        private static string settingsFileName = "Settings.json";
        private FileSystemWatcher txtWatcher;
        private FileSystemWatcher jsonWatcher;
        private static NotificationFileManager notificationFileManager = new NotificationFileManager();
        private static Message message = new Message();
        SettingsManager settingsManager = new SettingsManager();
        public void init()
        {
            settings = settingsManager.GetSettingsByFile(Settings.settingsFileName);
            //txt监听器
            txtWatcher = new FileSystemWatcher();
            //监听路径
            txtWatcher.Path = settings.General.FolderLocation;
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
            jsonWatcher.Path = settings.General.FolderLocation;
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
        }



        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileName = $"{e.Name}";

            //获取文件后缀名
            string extension = fileName.Substring(fileName.IndexOf("."));
            //对file进行属性设置

            SyncNotify.Message file = notificationFileManager.getMessageObjectByFile($"{e.FullPath}");
            file.Property.FileName = $"{e.Name}";
            file.Property.FileLocation = $"{e.FullPath}";
            file.Property.FileType = Path.GetExtension(fileName);

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
