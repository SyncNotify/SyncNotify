using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace SyncNotify
{
    internal class NotificationFileManager
    {
        Message message = new Message();
        public string[] ReadTop10TxtFilesContent(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {

                IEnumerable<string> txtFilesIEnum = Directory.GetFiles(folderPath, "*.txt")
                        .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                        .Take(50);
                string[] txtFiles = txtFilesIEnum.ToArray();
                // 读取前 10 个 txt 文件的内容并保存到数组中
                string[] fileContents = new string[txtFiles.Length];
                for (int i = 0; i < txtFiles.Length; i++)
                {
                    fileContents[i] = System.IO.File.ReadAllText(txtFiles[i]);

                }

                return fileContents;
            }
            else
            {
                MessageBox.Show(folderPath);
                return null;
            };



        }
        public string[] ReadTop10TxtFilesCreatingTime(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                // 获取目录中的所有 txt 文件，并按创建时间排序
                IEnumerable<string> txtFilesIEnum = Directory.GetFiles(folderPath, "*.txt")
                                    .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                                    .Take(50);
                string[] txtFiles = txtFilesIEnum.ToArray();
                // 读取这些文件的创建时间并保存到数组中
                string[] fileCreationTime = new string[txtFiles.Length];
                for (int i = 0; i < txtFiles.Length; i++)
                {
                    fileCreationTime[i] = getFileCreatingDate(txtFiles[i]);
                }

                return fileCreationTime;
            }
            else
            {
                MessageBox.Show(folderPath);
                return null;
            };



        }

        public string getFileCreatingDate(string fileLocation)
        {
            FileInfo file = new FileInfo(fileLocation);
            DateTime dt = file.LastWriteTime;
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return time;
        }

        //public void getJsonMessageContent(string folderPath)
        //{

        //}
        //public void getJsonMessageDisplayTime(string folderPath)
        //{

        //}
        //public void getJsonMessageDisplayMode(string folderPath)
        //{

        //}
        //public void getJsonMessageCreatingTime(string folderPath)
        //{

        //}
        //public void getJsonMessageSender(string folderPath)
        //{

        //}
        public SyncNotify.Message getMessageByFile(string fileLocation)
        {
            if (System.IO.File.Exists(fileLocation))
            {
                string text = System.IO.File.ReadAllText(fileLocation);
                return JsonConvert.DeserializeObject<Message>(text);
            }
            else
            {
                return null;
            }
        }

    }
}
