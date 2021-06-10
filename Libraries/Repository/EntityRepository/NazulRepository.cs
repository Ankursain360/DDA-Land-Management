using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Libraries.Repository.EntityRepository
{
    public class NazulRepository : GenericRepository<Nazul>, INazulRepository
    {
        public NazulRepository(DataContext dbContext) : base(dbContext)
        {

        }






        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.ToListAsync();
            return villageList;
        }





        public async Task<List<Nazul>> GetAllNazul()
        {
            return await _dbContext.Nazul.Include(x => x.Village).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<PagedResult<Nazul>> GetPagedNazul(NazulSearchDto model)
        {
            var data = await _dbContext.Nazul
                          .Include(x => x.Village)
                         

                          .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))
                          
                          .GetPaged<Nazul>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("YEAROFCONSOLIDATION"):
                        data = null;
                        data = await _dbContext.Nazul
                                   .Include(x => x.Village)


                          .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))
                                    .OrderBy(a => a.DateOfNotification)
                                    .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                        break;

                    case ("ACQUIREDLANDVILLAGE"):
                        data = null;
                        data = await _dbContext.Nazul
                                   .Include(x => x.Village)


                          .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))
                                    .OrderBy(a => a.Village.Name)
                                    .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                        break;
                    //case ("YEAROFJAMABANDI"):
                    //    data = null;
                    //    data = await _dbContext.Nazul
                    //               .Include(x => x.Village)

                    //      .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))
              
                    //               .OrderBy(a => a.YearOfJamabandi)
                    //                .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                    //    break;
                    //case ("NUMBER"):
                    //    data = null;
                    //    data = await _dbContext.Nazul
                    //               .Include(x => x.Village)

                    //      .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))

                    //               .OrderBy(a => a.LastMutationNo)
                    //                .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                    //    break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Nazul
                                   .Include(x => x.Village)

                          .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))

                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("YEAROFCONSOLIDATION"):
                        data = null;
                        data = await _dbContext.Nazul
                                   .Include(x => x.Village)


                          .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))
                                    .OrderByDescending(a => a.DateOfNotification)
                                    .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                        break;

                    case ("ACQUIREDLANDVILLAGE"):
                        data = null;
                        data = await _dbContext.Nazul
                                   .Include(x => x.Village)


                          .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))
                                    .OrderByDescending(a => a.Village.Name)
                                    .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                        break;
                    //case ("YEAROFJAMABANDI"):
                    //    data = null;
                    //    data = await _dbContext.Nazul
                    //               .Include(x => x.Village)

                    //      .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))

                    //               .OrderByDescending(a => a.YearOfJamabandi)
                    //                .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                    //    break;
                    //case ("NUMBER"):
                    //    data = null;
                    //    data = await _dbContext.Nazul
                    //               .Include(x => x.Village)

                    //      .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))

                    //               .OrderByDescending(a => a.LastMutationNo)
                    //                .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                    //    break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Nazul
                                   .Include(x => x.Village)

                          .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)))

                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Nazul>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<PagedResult<Nazul>> GetNazulReportData(NazulVillageReportSearchDto nazulVillageReportSearchDto)
        {
            var data = await _dbContext.Nazul
                .Include(x => x.Village)

                .Where(x => (x.VillageId == (nazulVillageReportSearchDto.villageId == 0 ? x.VillageId : nazulVillageReportSearchDto.villageId))
                && (x.IsActive == 1))
              
                .OrderByDescending(x => x.Id).GetPaged(nazulVillageReportSearchDto.PageNumber, nazulVillageReportSearchDto.PageSize);

            int SortOrder = (int)nazulVillageReportSearchDto.SortOrder;
            if (SortOrder == 1)
            {
                switch (nazulVillageReportSearchDto.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Nazul
                .Include(x => x.Village)
                           .Where(x => (x.VillageId == (nazulVillageReportSearchDto.villageId == 0 ? x.VillageId : nazulVillageReportSearchDto.villageId))
                && (x.IsActive == 1)).OrderBy(x => x.Village.Name).GetPaged(nazulVillageReportSearchDto.PageNumber, nazulVillageReportSearchDto.PageSize);
                        break;

                   

                }
            }
            else if (SortOrder == 2)
            {
                switch (nazulVillageReportSearchDto.SortBy.ToUpper())
                {

                    case ("VILLAGE"):

                        data = null;
                        data = await _dbContext.Nazul
                .Include(x => x.Village)
                           .Where(x => (x.VillageId == (nazulVillageReportSearchDto.villageId == 0 ? x.VillageId : nazulVillageReportSearchDto.villageId))
                && (x.IsActive == 1)).OrderByDescending(x => x.Village.Name)
                            .GetPaged(nazulVillageReportSearchDto.PageNumber, nazulVillageReportSearchDto.PageSize);
                        break;


                }
            }
            return data;
        }





    }
}
