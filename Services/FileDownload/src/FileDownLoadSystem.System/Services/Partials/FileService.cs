using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Enums;
using FileDownLoadSystem.Core.Untity;
using FileDownLoadSystem.Entity.FileInfo;
using FileDownLoadSystem.System.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FileDownLoadSystem.System.Services
{
    public partial class FileService
    {
        public async Task<WebResponseContent> GetFilesByTypeId(int fileTypeId)
        {
            var files = await _repository.FindAsIQueryable(m => m.FileTypeId == fileTypeId, m => new Dictionary<object, Core.Enums.QueryOrderBy>{
                {m.PublishDate , QueryOrderBy.Desc}
            }).ToListAsync();
            var dtos = _mapper.Map<List<Files>, List<FileDto>>(files);
            Response.Data = dtos;
            Response.OK();
            return Response;
        }
        public async Task<WebResponseContent> GetFilesById(int fileId)
        {
            var file = await _repository.FindAsIQueryable(m => m.Id == fileId).Include(m=>m.filePackages).FirstOrDefaultAsync();
            var dto = _mapper.Map<Files, FileDto>(file);
            Response.Data = dto;
            Response.OK();
            return Response;
        }
    }
}