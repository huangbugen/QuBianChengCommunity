using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileDownLoadSystem.Core.BaseProvider;
using FileDownLoadSystem.Entity.FileInfo;
using FileDownLoadSystem.System.IRepositories;
using FileDownLoadSystem.System.IServices;

namespace FileDownLoadSystem.System.Services
{
    public partial class FileWebConfigService : BaseService<FileWebConfigs, IFileWebConfigRepository>, IFileWebConfigService
    {
        private readonly IMapper mapper;

        public FileWebConfigService(IFileWebConfigRepository repository, IMapper mapper) : base(repository)
        {
            this.mapper = mapper;
        }
    }
}