using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.AreaApp.Dtos;
using Volo.Abp.Application.Services;

namespace BBSSystem.Application.Contracts.AreaApp
{
    public interface IAreaService : IApplicationService
    {
        Task<List<AreaDto>> GetAreaDtoAsync(int pageIndex, int pageSize);
    }
}