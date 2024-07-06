using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncNotify.Helper
{
    internal class MessageDisplayHelper
    {
        public string content;
        public static Message message = new Message();
        public static NotificationFileManager notificationFileManager = new NotificationFileManager();


        public void displayTxtMessage(SyncNotify.Message file)
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


        public void displayJsonMessage(SyncNotify.Message file)
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
            dateTimeFormatInfo.LongDatePattern = "yyyy-MM-dd-HH-mm-ss";
            DateTime now = DateTime.Now;
            DateTime targetTime = DateTime.ParseExact(message.Display.FileDisplayTime, "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
            int msUntilFour = (int)((targetTime - now).TotalMilliseconds);
            System.Threading.Timer t = new System.Threading.Timer(new TimerCallback(callBack));
            t.Change(msUntilFour, Timeout.Infinite);
        }

        private static void callBack(object state)
        {
            RealTimeMessagePage.Instance.refeshMessage(message);
        }
    }
}
