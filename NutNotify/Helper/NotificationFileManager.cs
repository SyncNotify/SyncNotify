using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncNotify
{
    internal class NotificationFileManager
    {
        public string[] ReadTop10TxtFiles(string directoryPath)
        {
            // 获取目录中的所有 txt 文件，并按创建时间排序
            var txtFiles = Directory.GetFiles(directoryPath, "*.txt")
                                    .OrderBy(f => new FileInfo(f).CreationTime)
                                    .Take(10);

            // 读取前 10 个 txt 文件的内容并保存到数组中
            string[] fileContents = new string[10];
            int index = 0;
            foreach (var file in txtFiles)
            {
                fileContents[index++] = System.IO.File.ReadAllText(file, Encoding.UTF8);
            }

            return fileContents;
        }

        public string getFileCreatingDate()
        {
            DateTime dt = DateTime.Now;
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return time;
        }
    }
}
