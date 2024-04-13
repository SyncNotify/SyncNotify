using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using System.Threading.Tasks;

namespace SyncNotify
{

    

    public class Settings
    {
        public static string settingsFileName = "Settings.json";
        [JsonProperty("general")]
        public General General { get; set; } = new General();
        [JsonProperty("message")]
        public Message Message { get; set; } = new Message();
    }
    public class General
    {
        [JsonProperty("folderLocation")]
        public string FolderLocation { get; set; } = @"D:\sync\通知";
        [JsonProperty("autoStartup")]
        public bool AutoStartup { get; set; } = true;
    }

    public class Message
    {
        [JsonProperty("messageArrivalSound")]
        public string MessageArrivalSound { get; set; }
    }
}
