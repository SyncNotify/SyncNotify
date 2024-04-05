using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncNotify
{
    internal class NotificationFileManager
    {
        public void getFileName(StreamWriter sw, string path, int indent)
        {
            //做历史消息用 遍历所有文件（预计后面加个刷新啥的 文件太多了还是不好）
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (FileInfo f in root.GetFiles())
            {
                for (int i = 0; i < indent; i++)
                {
                    sw.Write("  ");
                }
                sw.WriteLine(f.Name);
            }
        }

        public string getFileCreatingDate()
        {
            DateTime dt = DateTime.Now;
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return time;
        }
    }
}
