using Newtonsoft.Json;

namespace SyncNotify
{



    internal class Settings
    {
        internal static string settingsFileName = "Settings.json";
        [JsonProperty("general")]
        internal General General { get; set; } = new General();
        [JsonProperty("message")]
        internal Messages Messages { get; set; } = new Messages();
    }
    internal class General
    {
        [JsonProperty("folderLocation")]
        internal string FolderLocation { get; set; } = @"D:\sync\通知";
        [JsonProperty("autoStartup")]
        internal bool AutoStartup { get; set; } = true;
    }

    internal class Messages
    {
        [JsonProperty("messageArrivalSound")]
        internal string MessageArrivalSound { get; set; }
    }
}
