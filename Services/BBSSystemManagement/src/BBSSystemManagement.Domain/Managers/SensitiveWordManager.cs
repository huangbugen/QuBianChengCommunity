using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystemManagement.Domain.PostInfo;
using Youshow.Ace.Domain.Repository;
using Youshow.Ace.Domain.Services;
using Youshow.Ace.File.Upload;
using static BBSSystemManagement.Domain.PostInfo.SensitiveWordsLibrary;

namespace BBSSystemManagement.Domain.Managers
{
    public class SensitiveWordManager : DomainServicer
    {
        public IRepository<SensitiveWordsLibrary> SensitiveWordsLibraryRepo { get; set; }

        public async Task AddSensitiveWordsLibrary(FileUploadResult fileUploadResult)
        {
            try
            {
                var model = new SensitiveWordsLibrary(SortGuid.NewGuid().ToString("N"));
                model.InitData(fileUploadResult.FileUrl, fileUploadResult.FileName);
                await SensitiveWordsLibraryRepo.InsertAsync(model);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task UpdateStatusAsync(string libraryId, SensitiveStatus status)
        {
            await SensitiveWordsLibraryRepo
                .UpdateAsync(m => m.Id == libraryId, p => new Dictionary<object, object> { { p.Status, status } });
        }

        public async Task<List<SensitiveWordsLibrary>> GetSensitiveWordsLibrariesInActiveAsync()
        {
            return await SensitiveWordsLibraryRepo.GetListAsync(m => m.Status == SensitiveStatus.Enable);
        }
    }
}