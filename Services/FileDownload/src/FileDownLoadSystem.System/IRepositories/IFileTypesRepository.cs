using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.BaseProvider;
using FileDownLoadSystem.Core.Extensions;
using FileDownLoadSystem.Core.Untity;
using FileDownLoadSystem.Entity.FileInfo;

namespace FileDownLoadSystem.System.IRepositories
{
    public interface IFileTypesRepository : IRepository<FileTypes>, IDependency
    {
       
    }
}