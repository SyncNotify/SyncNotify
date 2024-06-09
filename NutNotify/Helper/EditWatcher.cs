using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace SyncNotify
{
    internal class EditWatcher
    {
        public static Settings Settings = new Settings();
        public static string settingsFileName = "Settings.json";
        public static string content;
        public void init()
        {
            //txt监听器
            //监听路径
            FileSystemWatcher txtWatcher = new FileSystemWatcher(Settings.General.FolderLocation);
            txtWatcher.IncludeSubdirectories = true;
            txtWatcher.EnableRaisingEvents = true;
            //设置监听的属性
            txtWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security;
            //绑定事件
            txtWatcher.Created += Watcher_Created;
            //仅监听txt文件
            txtWatcher.Filter = "*.txt";



            //json监听器
            FileSystemWatcher jsonWatcher = new FileSystemWatcher(Settings.General.FolderLocation);
            jsonWatcher.IncludeSubdirectories = true;
            jsonWatcher.EnableRaisingEvents = true;
            //设置监听的属性
            jsonWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security;
            //绑定事件
            jsonWatcher.Created += Watcher_Created;
            //监听json文件
            jsonWatcher.Filter = "*.json";
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {

        }

        private static void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {

        }

        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileName = $"{e.Name}";
            //获取文件后缀名
            string extension = fileName.Substring(fileName.IndexOf("."));
            //对file进行属性设置
            NotificationFileManager notificationFileManager = new NotificationFileManager();

            SyncNotify.Message file = new Message();
            file.FileName = $"{e.Name}";
            file.FileLocation = $"{e.FullPath}"; 
            file.FileCreatingTime = notificationFileManager.getFileCreatingDate($"{e.FullPath}"); ;
            file.FileType = Path.GetExtension(fileName);
            file.FileLocation = $"{e.FullPath}";

            if (file.FileType.Length > 0)
            {
                if (file.FileType == ".json")
                {
                    displayJsonMessage(file);
                }else if(file.FileType == ".txt")
                {
                    displayTxtMessage(file);
                }
            }

            
            

        }

        private static void displayTxtMessage(SyncNotify.Message file)
        {
            new Thread(() =>
            {
                Thread.Sleep(2000);
                // 打开文件并创建 StreamReader 对象
                StreamReader reader = new StreamReader(file.FileLocation);
                // 读取文件内容
                content = reader.ReadToEnd();
                // 关闭流
                reader.Close();
                InternalProper.RecentText = content;
                //RealTimeMessagePage.Instance.responseGetter(content);
                //TODO REMOVAL IN THE FUTURE
                InternalProper.RecentTime = file.FileCreatingTime;
                file.FileContent = content;
                RealTimeMessagePage.Instance.refeshMessage(file);
            }).Start();
        }

        private static void displayJsonMessage(SyncNotify.Message file)
        {
            MessageBox.Show("这是一条json消息");
        }
    }
}
