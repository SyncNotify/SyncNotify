using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncNotify
{
    public class File
    {
        //文件的实体类
        private string fileLocation;
        private string fileName;
        private string fileCreationDate;
        private string fileContent;
        //getter和setter
        public string FileLocation { get => fileLocation; set => fileLocation = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public string FileCreationDate { get => fileCreationDate; set => fileCreationDate = value; }
        public string FileContent { get => fileContent; set => fileContent = value; }
    }
}
