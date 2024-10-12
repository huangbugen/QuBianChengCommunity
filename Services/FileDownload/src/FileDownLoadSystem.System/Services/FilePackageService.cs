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
    public class FilePackageService : BaseService<FilePackages, IFilePackageRepository>, IFilePackageService
    {
        public FilePackageService(IFilePackageRepository repository) : base(repository)
        {
        }
    }
}