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
    public class FileWebConfigRepository : BaseRepository<FileWebConfigs>, IFileWebConfigRepository
    {
        public FileWebConfigRepository(FileDownloadSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}