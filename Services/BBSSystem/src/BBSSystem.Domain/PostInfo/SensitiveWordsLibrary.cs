using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace BBSSystemManagement.Domain.PostInfo
{
    public class SensitiveWordsLibrary : Entity<string>
    {
        public string LibraryFileName { get; set; }
        public DateTime CreateTime { get; set; }
        public string LibraryFileUrl { get; set; }
        public SensitiveStatus Status { get; set; }

        public SensitiveWordsLibrary()
        {

        }

        public SensitiveWordsLibrary(string id) : base(id)
        {

        }

        public void InitData(string libraryFileUrl, string fileName)
        {
            Status = SensitiveStatus.Enable;
            LibraryFileUrl = libraryFileUrl;
            LibraryFileName = fileName;
        }

        public enum SensitiveStatus
        {
            Disable,
            Enable,
        }
    }
}