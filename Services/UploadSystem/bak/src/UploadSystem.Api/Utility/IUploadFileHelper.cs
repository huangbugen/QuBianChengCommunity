namespace UploadSystem.Api.Utility
{
    public interface IUploadFileHelper
    {
        string SaveFile(IFormFile file, string savePath, string rootPath, string fileext);
    }
}