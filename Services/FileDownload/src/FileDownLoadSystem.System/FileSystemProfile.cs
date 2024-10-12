using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileDownLoadSystem.Entity.FileInfo;
using FileDownLoadSystem.System.Services.Dtos;

namespace FileDownLoadSystem.System
{
    public class FileSystemProfile : Profile
    {
        public FileSystemProfile()
        {
            CreateMap<Files, FileDto>();
        }
    }
}