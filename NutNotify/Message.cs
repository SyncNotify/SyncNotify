using Newtonsoft.Json;

namespace SyncNotify
{
    public class Message
    {
        //文件的实体类
        private string messageFileLocation;
        private string messageFileName;
        private string messageFileCreatingDate;
        private string messageFileContent;
        private string messageFileType;
        private string messageFileDisplayTime;
        private string messageFilePriority;
        private string messageSender;
        //getter和setter
        [JsonProperty("fileLocation")]
        public string FileLocation { get => messageFileLocation; set => messageFileLocation = value; }

        [JsonProperty("fileName")]
        public string FileName { get => messageFileName; set => messageFileName = value; }

        [JsonProperty("fileCreatingTime")]
        public string FileCreatingTime { get => messageFileCreatingDate; set => messageFileCreatingDate = value; }

        [JsonProperty("fileDisplayTime")]
        public string FileDisplayTime { get => messageFileDisplayTime; set => messageFileDisplayTime = value; }

        [JsonProperty("fileContent")]
        public string FileContent { get => messageFileContent; set => messageFileContent = value; }

        [JsonProperty("fileType")]
        public string FileType { get => messageFileType; set => messageFileType = value; }

        [JsonProperty("fileSender")]
        public string FileSender { get => messageSender; set => messageSender = value; }
    }
}
