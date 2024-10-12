using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Entity.FileInfo;

namespace FileDownLoadSystem.System.Services.Dtos
{
    public class FileDto
    {
        public int Id { get; set; }
        public int FileTypeId { get; set; }
        public string FileName { get; set; }
        public string? FileIconUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public long ClickTimes { get; set; }
        public long DownloadTimes { get; set; }
        public string? Notification { get; set; }
        public List<FilePackages> filePackages { get; set; }
    }
}