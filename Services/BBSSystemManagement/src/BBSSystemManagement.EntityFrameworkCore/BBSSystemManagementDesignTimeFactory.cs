using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youshow.Ace.EntityFrameworkCore;
using Youshow.Ace.EntityFrameworkCore.MySql;

namespace BBSSystemManagement.EntityFrameworkCore
{
    public class BBSSystemManagementDesignTimeFactory : AceMySqlDesignTimeDbContextFactory<BBSSystemManagementDbContext>
    {
        public override AceDesignTimeDbContextOptions Options => new()
        {
            StartupProjectPath = @"../BBSSystemManagement.Web" //appsetting.json所在目录
        };
    }
}
