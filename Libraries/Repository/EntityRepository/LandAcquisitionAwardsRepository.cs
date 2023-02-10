using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class LandAcquisitionAwardsRepository : GenericRepository<LandAcquisitionAwards>, ILandAcquisitionAwardsRepository
    {
        public LandAcquisitionAwardsRepository(DataContext dbContext) : base(dbContext)
        {
        }

        //public async Task<List<Locality>> GetLocalityList()
        //{
        //    var result = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
        //    List<Locality> localityList = result
        //               .Select(o => new Locality
        //               {
        //                   Id = o.Id,
        //                   Name = o.Name
        //               }).ToList(); 
        //    return localityList;
        //}

        public async Task<PagedResult<LandAcquisitionAwards>> GetPagedLandAcquisitionAwards(LandAcquisitionAwardsDto model) 
        {
            var data = await _dbContext.landacquisitionawards                                           
                                            .Where(x => (string.IsNullOrEmpty(model.village)|| x.Village.Contains(model.village))
                                            &&(string.IsNullOrEmpty(model.title) || x.Title.Contains(model.title))
                                            
                                            )
                                           .GetPaged<LandAcquisitionAwards>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TITLE"):
                        data.Results = data.Results.OrderBy(x => x.Title).ToList();

                        break;

                    case ("VILLAGE"):
                        data.Results = data.Results.OrderBy(x => x.Village).ToList();

                        break;

                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();

                        break;

                }

            }
            else if (SortOrder == 2)
            {

                switch (model.SortBy.ToUpper())
                {
                    case ("FILE"):
                        data.Results = data.Results.OrderByDescending(x => x.Title).ToList();

                        break;
                    case ("VILLAGE"):
                        data.Results = data.Results.OrderByDescending(x => x.Village).ToList();

                        break;
                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();

                        break;

                }
            }
            return data;
        }
        public async Task<LandAcquisitionAwards> FetchDocumentDetails(int id)
        {
            var data = await _dbContext.landacquisitionawards.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task<List<LandAcquisitionAwards>> GetLandAcquisitionAwardsList(LandAcquisitionAwardsSearchDto model)
        {
            var data = await _dbContext.landacquisitionawards
                                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Contains(model.village))
                                        && (string.IsNullOrEmpty(model.title) || x.Title.Contains(model.title))).ToListAsync();
            return data;
        }
        public async Task<List<LandAcquisitionAwards>> GetAllAcquisitionAwards()
        {
            var data = await _dbContext.landacquisitionawards.OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }
    }
}
