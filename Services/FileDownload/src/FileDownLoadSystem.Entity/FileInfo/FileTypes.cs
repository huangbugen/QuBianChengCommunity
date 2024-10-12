using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Entity.AttributeManager;

namespace FileDownLoadSystem.Entity.FileInfo
{
    [Entity(DetailTable =new Type[]{typeof(Files)},ForeignKeyName ="FileTypeId")]
    public class FileTypes : BaseModel
    {
        public string TypeName { get; set; }
    }
}