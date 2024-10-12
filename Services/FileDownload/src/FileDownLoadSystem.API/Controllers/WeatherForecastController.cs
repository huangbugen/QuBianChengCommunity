
using FileDownLoadSystem.Core.Filters;
using FileDownLoadSystem.Core.Untity;
using FileDownLoadSystem.Entity.Core;
using FileDownLoadSystem.Entity.FileInfo;
using FileDownLoadSystem.System.IRepositories;
using FileDownLoadSystem.System.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QubianCheng.CacheManager.IService;

namespace FileDownLoadSystem.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ICacheService _cacheService;

    public WeatherForecastController(IFileService fileService, ICacheService cacheService)
    {
        this._cacheService = cacheService;
        FileService = fileService;
    }

    public IFileRepository FileRepository { get; }
    public IFileService FileService { get; }
    [Authorize]
    [HttpPost]
    public void Post(SaveModel saveModel)
    {

    }
    // [ApiActionPermission("User","1")]
    [HttpGet("GetToken")]
    public string GetToken()
    {
        var token = JwtHelper.IssueJwt(new UserClaim
        {
            Role_Id = 1,
            Enable = 1,
            RoleName = "Admin",
            Token = "",
            User_Id = 1,
            UserName = "Ace"
        });
        return token;
    }

    [Authorize]
    [HttpGet(Name = "GetWeatherForecast")]
    public string Get(string token)
    {
        var userClaim = JwtHelper.SerializerJwt(token);
        var exp = JwtHelper.GetExp(token);
        // _cacheService.Add("a","A");
        // var res1 = _cacheService.Get("a");
        // var res2 = _cacheService.Get("b");
        for (int i = 0; i < 1000; i++)
        {
            _cacheService.Add("b" + i, "B" + i, TimeSpan.FromSeconds(60));
        }

        // _cacheService.AddHash("Coder", new Dictionary<string, object>{
        //     {"Name","Ace"},
        //     {"Age",16}
        // });
        return "Ace";
        // var res = FileService.Update(new SaveModel()
        // {
        //     MainData = new Dictionary<string, object>(){
        //         {"Id","4"},
        //         {"ClickTimes" , 0},
        //         {"DownloadTimes" , 0},
        //         {"FileIconUrl" , ""},
        //         {"FileName" , "dingdingx"},
        //         {"FileTypeId" , 1},
        //         {"Notification" ,""},
        //         {"PublishDate" , DateTime.Now}
        //         },
        //     DetailData = new List<Dictionary<string, object>>(){
        //         {
        //             new Dictionary<string, object>{
        //                 {"Id","1"},
        //                 {"FileId","4"},
        //                 {"PackageUrl","1111"},
        //                 {"PackageName","aa1111aa"},
        //                 {"PublishTime",""},
        //             }
        //         }
        //     },
        //     DelKeys = new(){2,3}
        // });
        // return res;
    }
}

