using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.PostInfo;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace BBSSystem.Domain.Managers
{
    public class AreaManager : DomainService
    {
        public IRepository<Area> AreaRepo { get; set; }

        public async Task<List<Area>> GetAreasAsync(int skip = 0, int take = 30)
        {
            var query = await AreaRepo.GetQueryableAsync();
            var areas = query.Skip(skip).Take(take).ToList();

            return areas;
        }
    }
}