using Newtonsoft.Json;

namespace SyncNotify
{



    public class Settings
    {
        public static string settingsFileName = "Settings.json";
        [JsonProperty("general")]
        public General General { get; set; } = new General();
        [JsonProperty("message")]
        public Messages Messages { get; set; } = new Messages();
    }
    public class General
    {
        [JsonProperty("folderLocation")]
        public string FolderLocation { get; set; } = @"D:\sync\通知";
        [JsonProperty("autoStartup")]
        public bool AutoStartup { get; set; } = true;
    }

    public class Messages
    {
        [JsonProperty("messageArrivalSound")]
        public string MessageArrivalSound { get; set; }
    }
}
