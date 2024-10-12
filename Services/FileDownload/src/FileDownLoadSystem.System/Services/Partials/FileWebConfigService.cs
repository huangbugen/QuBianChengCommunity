using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Enums;
using FileDownLoadSystem.Core.Untity;
using Microsoft.EntityFrameworkCore;

namespace FileDownLoadSystem.System.Services
{
    public partial class FileWebConfigService
    {
        public async Task<WebResponseContent> GetFileWebConfig()
        {

            var query = await _repository.FindAsIQueryable(m => m.Id != 0, m => new Dictionary<object, Core.Enums.QueryOrderBy>{
                    {m.PublishDate, QueryOrderBy.Asc}
                }).FirstOrDefaultAsync();
            var response = new WebResponseContent();
            response = response.OK();
            response.Data = query;
            return response;

        }
    }
}