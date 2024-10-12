using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileDownLoadSystem.Core.BaseProvider;
using FileDownLoadSystem.Core.Untity;
using FileDownLoadSystem.Entity.FileInfo;
using FileDownLoadSystem.System.IRepositories;
using FileDownLoadSystem.System.IServices;
using FileDownLoadSystem.System.Repositories;

namespace FileDownLoadSystem.System.Services
{
    public partial class FileService : BaseService<Files, IFileRepository> , IFileService
    {
        private readonly IMapper _mapper;
        public FileService(IFileRepository repository, IMapper mapper) : base(repository)
        {
            this._mapper = mapper;
        }

       
    }
}