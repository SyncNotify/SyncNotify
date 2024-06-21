using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace SyncNotify
{
    internal class EditWatcher
    {
        public static Settings Settings = new Settings();
        public static string settingsFileName = "Settings.json";
        public static string content;
        public static NotificationFileManager notificationFileManager = new NotificationFileManager();
        public static Message message = new Message();
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
                    displayJsonMessage(file);
                }
                else if (file.Property.FileType == ".txt")
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
                StreamReader reader = new StreamReader(file.Property.FileLocation);
                // 读取文件内容
                content = reader.ReadToEnd();
                // 关闭流
                reader.Close();
                InternalProper.RecentText = content;
                //RealTimeMessagePage.Instance.responseGetter(content);
                //TODO REMOVAL IN THE FUTURE
                InternalProper.RecentTime = file.Property.FileCreatingTime;
                file.Property.FileContent = content;
                RealTimeMessagePage.Instance.refeshMessage(file);
            }).Start();
        }

        private static void displayJsonMessage(SyncNotify.Message file)
        {
            message = notificationFileManager.getMessageByFile(file.Property.FileLocation);
            switch (message.Display.FileDisplayMode)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    //FIGURED
                    setTimer();

                    break;
            }
            System.Windows.MessageBox.Show(message.Display.FileDisplayMode.ToString());
        }

        private static void setTimer()
        {
            DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
            dateTimeFormatInfo.ShortDatePattern = "yyyy-MM-dd-HH-mm-ss";
            DateTime now = DateTime.Now;
            DateTime targetTime = Convert.ToDateTime(message.Display.FileDisplayTime, dateTimeFormatInfo);
            int msUntilFour = (int)((targetTime - now).TotalMilliseconds);
            //System.Threading.Timer t = new System.Threading.Timer();
            //t.Change(msUntilFour, Timeout.Infinite);
        }
    }
}
