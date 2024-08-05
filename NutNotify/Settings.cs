using Newtonsoft.Json;

namespace SyncNotify
{



    internal class Settings
    {
        internal static string settingsFileName = "Settings.json";
        //Settings下的三个类
        [JsonProperty("general")]
        internal General General { get; set; } = new General();
        [JsonProperty("message")]
        internal Messages Messages { get; set; } = new Messages();
        [JsonProperty("display")]
        internal MessageDisplay MessageDisplay { get; set; } = new MessageDisplay();
    }
    internal class General
    {
        //监听目录
        [JsonProperty("folderLocation")]
        internal string FolderLocation { get; set; }
        //是否自动启动
        [JsonProperty("autoStartup")]
        internal bool AutoStartup { get; set; } = true;
    }

    internal class Messages
    {
        //消息到达的提示音（路径）
        [JsonProperty("messageArrivalSound")]
        internal string MessageArrivalSound { get; set; }
    }

    internal class MessageDisplay
    {
        //窗口防关闭策略
        [JsonProperty("messageDisplayWindowStrategy")]
        internal string MessageArrivalSound { get; set; }
        //消息防顶策略
        [JsonProperty("messageDisplayNumStrategy")]
        internal string messageDisplayNumStrategy { get; set; }
    }
}
