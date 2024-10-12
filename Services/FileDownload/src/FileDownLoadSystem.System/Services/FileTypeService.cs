using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.BaseProvider;
using FileDownLoadSystem.Entity.FileInfo;
using FileDownLoadSystem.System.IRepositories;
using FileDownLoadSystem.System.IServices;

namespace FileDownLoadSystem.System.Services
{
    public partial class FileTypeService  : BaseService<FileTypes, IFileTypesRepository>, IFileTypeService
    {
        public FileTypeService(IFileTypesRepository repository) : base(repository)
        {
        }
    }
}