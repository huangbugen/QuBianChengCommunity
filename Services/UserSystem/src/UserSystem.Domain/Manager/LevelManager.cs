using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSystem.Domain.Account;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace UserSystem.Domain.Manager
{
    public class LevelManager : DomainService
    {
        private readonly IRepository<Level> _levelRepo;

        public LevelManager(
            IRepository<Level> levelRepo
        )
        {
            this._levelRepo = levelRepo;
        }

        public async Task<List<Level>> GetTop2LevelAsync()
        {
            var levels = (await _levelRepo.GetQueryableAsync()).OrderBy(m => m.NeedIntegral).Take(2).ToList();
            return levels;
        }
    }
}