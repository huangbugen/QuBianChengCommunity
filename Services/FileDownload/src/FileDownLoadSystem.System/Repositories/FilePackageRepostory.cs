using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.BaseProvider;
using FileDownLoadSystem.Core.EfDbContext;
using FileDownLoadSystem.Entity.FileInfo;
using FileDownLoadSystem.System.IRepositories;

namespace FileDownLoadSystem.System.Repositories
{
    public class FilePackageRepostory : BaseRepository<FilePackages>, IFilePackageRepository
    {
        public FilePackageRepostory(FileDownloadSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}