using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NutNotify
{
    internal class EditWatcher
    {
        public static string content;
        public void init()
        {
            //监听路径
            FileSystemWatcher watcher = new FileSystemWatcher(@"D:\sync\通知");
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            //设置监听的属性
            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;
            //绑定事件
            watcher.Created += Watcher_Created;
            //仅监听txt文件
            watcher.Filter = "*.txt";
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
            string filePath = $"{e.FullPath}";
            new Thread(() => {
                Thread.Sleep(2000);
                // 打开文件并创建 StreamReader 对象
                StreamReader reader = new StreamReader(filePath);
                // 读取文件内容
                content = reader.ReadToEnd();

                // 关闭流
                reader.Close();
                InternalProper.RecentText = content;
                RealTimeMessagePage.Instance.responseGetter(content);

            }).Start();



        }

    }
}
