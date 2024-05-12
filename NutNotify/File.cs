namespace SyncNotify
{
    public class File
    {
        //文件的实体类
        private string fileLocation;
        private string fileName;
        private string fileCreatingDate;
        private string fileContent;
        private string fileType;
        //getter和setter
        public string FileLocation { get => fileLocation; set => fileLocation = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public string FileCreatingTime { get => fileCreatingDate; set => fileCreatingDate = value; }
        public string FileContent { get => fileContent; set => fileContent = value; }
        public string FileType { get => fileType; set => fileType = value; }
    }
}
