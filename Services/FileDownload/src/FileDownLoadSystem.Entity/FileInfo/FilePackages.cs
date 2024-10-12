using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Entity.FileInfo
{
    public class FilePackages : BaseModel
    {
        public int FilesId { get; set; }
        public string PackageUrl { get; set; }
        public string PackageName { get; set; }
        public DateTime PublishTime { get; set; }
    }
}