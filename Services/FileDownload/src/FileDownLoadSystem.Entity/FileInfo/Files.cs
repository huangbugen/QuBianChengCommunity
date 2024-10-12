using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Entity.AttributeManager;

namespace FileDownLoadSystem.Entity.FileInfo
{
    [Entity(DetailTable = new Type[] { typeof(FilePackages) }, ForeignKeyName = "FileId")]
    public class Files : BaseModel
    {
        // private int _fileTypeId;
        // public int FileTypeId
        // {
        //     get
        //     {
        //         return _fileTypeId;
        //     }
        //     set
        //     {
        //         if (value == 1 || value == 2)
        //         {
        //             _fileTypeId = value;
        //         }
        //     }
        // }
        public int FileTypeId
        {
            get;
            set;
        }
        public string FileName { get; set; }
        public string? FileIconUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public long ClickTimes { get; set; }
        public long DownloadTimes { get; set; }
        public string? Notification { get; set; }
        
        [ForeignKey("FilesId")]
        public List<FilePackages> filePackages { get; set; }
    }
}