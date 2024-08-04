using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace SyncNotify
{
    internal class NotificationFileManager
    {
        Message message = new Message();
        public string[] ReadTop50TxtFilesContent(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {

                IEnumerable<string> txtFilesIEnum = Directory.GetFiles(folderPath, "*.txt")
                        .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                        .Take(50);
                string[] txtFiles = txtFilesIEnum.ToArray();
                // 读取前 50 个 txt 文件的内容并保存到数组中
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
        public string[] ReadTop50JsonFilesContent(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {

                IEnumerable<string> txtFilesIEnum = Directory.GetFiles(folderPath, "*.json")
                        .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                        .Take(50);
                string[] txtFiles = txtFilesIEnum.ToArray();
                // 读取前 50 个 json 文件的内容并保存到数组中
                string[] fileContents = new string[txtFiles.Length];
                string[] messageContent = new string[txtFiles.Length];
                for (int i = 0; i < txtFiles.Length; i++)
                {
                    fileContents[i] = System.IO.File.ReadAllText(txtFiles[i]);
                    message = JsonConvert.DeserializeObject<Message>(fileContents[i]);
                    messageContent[i] = message.Property.FileContent;
                }

                return messageContent;
            }
            else
            {
                MessageBox.Show(folderPath);
                return null;
            };
        }
        public string[] ReadTop50TxtFilesCreatingTime(string folderPath)
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
        //public string getFileDisplayDate(string fileLocation)
        //{
        //    if (System.IO.File.Exists(fileLocation))
        //    {
        //        string text = System.IO.File.ReadAllText(fileLocation);
        //        return JsonConvert.DeserializeObject<Display>(text);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

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
        public SyncNotify.Message getMessageObjectByFile(string fileLocation)
        {
            if (System.IO.File.Exists(fileLocation))
            {
                switch (Path.GetExtension(fileLocation)){
                    case ".json":
                        string text = System.IO.File.ReadAllText(fileLocation);
                        return JsonConvert.DeserializeObject<Message>(text);
                    case ".txt":
                        Message message = new Message();
                        // 打开文件并创建 StreamReader 对象
                        StreamReader reader = new StreamReader(fileLocation);
                        message.Property.FileContent = reader.ReadToEnd() ;
                        message.Property.FileCreatingTime = getFileCreatingDate(fileLocation);
                        // 关闭流
                        reader.Close();
                        return message;
                    default: return null;
                };
            }
            else
            {
                return null;
            }
        }

    }
}
