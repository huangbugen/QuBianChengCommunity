using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Enums;
using FileDownLoadSystem.Core.Untity;
using Microsoft.EntityFrameworkCore;

namespace FileDownLoadSystem.System.Services
{
    public partial class FileTypeService
    {
        public async Task<WebResponseContent> GetFileTypes()
        {
            var res = await _repository.FindAsIQueryable(m=>m.Id!=0 , m=>new Dictionary<object, Core.Enums.QueryOrderBy>
            {
                {m.Id, QueryOrderBy.Asc}
            }).ToListAsync();
            Response.OK();
            Response.Data = res;
            return Response;
        }
    }
}