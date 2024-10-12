using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youshow.Ace.EntityFrameworkCore;
using Youshow.Ace.EntityFrameworkCore.MySql;

namespace UploadSystem.EntityFrameworkCore
{
    public class UploadSystemDesignTimeFactory : AceMySqlDesignTimeDbContextFactory<UploadSystemDbContext>
{
    public override AceDesignTimeDbContextOptions Options => new()
    {
        StartupProjectPath = @"../UploadSystem.Web" //appsetting.json所在目录
    };
}
}
