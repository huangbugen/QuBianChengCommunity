using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.PostInfo;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace BBSSystem.Domain.Managers
{
    public class SectionManager : DomainService
    {
        public IRepository<Section> SectionRepo { get; set; }
        public IRepository<SectionLordUserMapping> SectionLordRepo { get; set; }

        public async Task<List<Section>> GetSectionsByAreaIdAsync(List<string> areaIds, int skip = 0, int take = 30)
        {
            var query = await SectionRepo.GetQueryableAsync();
            if (areaIds != null)
            {
                query = query.Where(m => areaIds.Contains(m.AreaId));
            }
            var sections = query.Skip(skip).Take(take).ToList();

            var sectionIds = sections.Select(m => m.Id);

            var sectionLords = await SectionLordRepo.GetListAsync(m => sectionIds.Contains(m.SectionId));

            sections.ForEach(m =>
            {
                m.SectionLords = sectionLords.FindAll(n => n.SectionId == m.Id);
            });

            return sections;
        }

        public async Task<(Section section, List<string> lordNames)> GetSimpleSectionsAsync(string sectionId)
        {
            var section = await SectionRepo.GetAsync(m => m.Id == sectionId);
            var sectionLords = await SectionLordRepo.GetListAsync(m => m.SectionId == sectionId);
            var lordNames = sectionLords.Select(m => m.UserName).ToList();

            return (section, lordNames);
        }

        // public async Task<Section> GetSectionByIdAsync(string sectionId)
        // {
        //     return await SectionRepo.GetAsync(m => m.Id == sectionId);
        // }
    }
}