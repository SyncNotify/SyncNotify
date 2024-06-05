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
        //getter和setter
        public string FileLocation { get => messageFileLocation; set => messageFileLocation = value; }
        public string FileName { get => messageFileName; set => messageFileName = value; }
        public string FileCreatingTime { get => messageFileCreatingDate; set => messageFileCreatingDate = value; }
        public string FileContent { get => messageFileContent; set => messageFileContent = value; }
        public string FileType { get => messageFileType; set => messageFileType = value; }
    }
}
