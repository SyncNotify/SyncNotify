using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;

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
            message = notificationFileManager.getMessageObjectByFile(file.Property.FileLocation);
            switch (message.Display.FileDisplayMode)
            {
                case 0:
                    //DEFAULT
                    RealTimeMessagePage.Instance.refeshMessage(message);

                    break;
                case 1:
                    break;
                case 2:
                    //FORCED_NOW
                    RealTimeMessagePage.Instance.refeshMessage(message);
                    break;
                case 3:
                    break;
                case 4:
                    //FIGURED
                    setTimer();
                    break;
            }
        }

        private static void setTimer()
        {
            DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
            dateTimeFormatInfo.LongDatePattern = "yyyy-MM-dd-HH-mm-ss";
            DateTime now = DateTime.Now;
            DateTime targetTime = DateTime.ParseExact(message.Display.FileDisplayTime, "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
            //MessageBox.Show(targetTime.ToString() + "现在是" + now.ToString());
            TimeSpan timeSpan = targetTime.Subtract(now);
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = (int)timeSpan.TotalMilliseconds; // 定时器的运行周期(ms)
            timer.Elapsed += new System.Timers.ElapsedEventHandler(callBack); // 添加调用函数
            timer.AutoReset = false;
            timer.Enabled = true;

        }

        private static void callBack(object state, System.Timers.ElapsedEventArgs e)
        {
            RealTimeMessagePage.Instance.refeshMessage(message);
        }
    }
}
