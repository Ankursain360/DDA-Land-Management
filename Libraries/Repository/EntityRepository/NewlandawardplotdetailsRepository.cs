using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class NewlandawardplotdetailsRepository : GenericRepository<Newlandawardplotdetails>, InewlandawardplotdetailsRepository
    {
        public NewlandawardplotdetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Newlandawardmasterdetail>> GetAllAWardmaster()
        {
            List<Newlandawardmasterdetail> awardList = await _dbContext.Newlandawardmasterdetail.Where(x => x.IsActive == 1).ToListAsync();
            return awardList;
        }
        public async Task<List<Newlandawardplotdetails>> GetAwardplotdetails()
        {
            return await _dbContext.Newlandawardplotdetails.Include(x => x.NewlandAwardMaster).Include(x => x.NewlandVillage).Include(x => x.NewlandKhasra)
                .OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage
                                                                        .Where(x => x.IsActive == 1)
                                                                        .ToListAsync();
            return villageList;
        }



        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _dbContext.Newlandkhasra.Where(x => x.NewLandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        public async Task<PagedResult<Newlandawardplotdetails>> GetPagedAwardplotdetails(NewlandawardplotdetailsSearchDto model)
        {
     

            var data = await _dbContext.Newlandawardplotdetails
                                    .Include(x => x.NewlandAwardMaster)
                                    .Include(x => x.NewlandVillage)
                                    .Include(x => x.NewlandKhasra)
                                    .OrderByDescending(x => x.Id)
                          .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("AWARD"):
                        data = null;
                        data = await _dbContext.Newlandawardplotdetails
                                    .Include(x => x.NewlandAwardMaster)
                                    .Include(x => x.NewlandVillage)
                                    .Include(x => x.NewlandKhasra)
                                     .Where(x => string.IsNullOrEmpty(model.name) || x.NewlandAwardMaster.AwardNumber.Contains(model.name))
                                    .OrderBy(x => x.NewlandAwardMaster.AwardNumber)
                          .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);
                       
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandawardplotdetails
                                    .Include(x => x.NewlandAwardMaster)
                                    .Include(x => x.NewlandVillage)
                                    .Include(x => x.NewlandKhasra)
                                     .Where(x => string.IsNullOrEmpty(model.name) || x.NewlandAwardMaster.AwardNumber.Contains(model.name))
                                    .OrderBy(x => x.NewlandVillage.Name)
                          .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);
                       

                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandawardplotdetails
                                    .Include(x => x.NewlandAwardMaster)
                                    .Include(x => x.NewlandVillage)
                                    .Include(x => x.NewlandKhasra)
                                     .Where(x => string.IsNullOrEmpty(model.name) || x.NewlandAwardMaster.AwardNumber.Contains(model.name))
                                    .OrderBy(x => x.NewlandKhasra.Name)
                          .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);

                      
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandawardplotdetails
                                    .Include(x => x.NewlandAwardMaster)
                                    .Include(x => x.NewlandVillage)
                                    .Include(x => x.NewlandKhasra)
                                     .Where(x => string.IsNullOrEmpty(model.name) || x.NewlandAwardMaster.AwardNumber.Contains(model.name))
                                    .OrderByDescending(x => x.IsActive)
                          .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("AWARD"):
                        data = null;
                        data = await _dbContext.Newlandawardplotdetails
                                    .Include(x => x.NewlandAwardMaster)
                                    .Include(x => x.NewlandVillage)
                                    .Include(x => x.NewlandKhasra)
                                     .Where(x => string.IsNullOrEmpty(model.name) || x.NewlandAwardMaster.AwardNumber.Contains(model.name))
                                    .OrderByDescending(x => x.NewlandAwardMaster.AwardNumber)
                          .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandawardplotdetails
                                     .Include(x => x.NewlandAwardMaster)
                                     .Include(x => x.NewlandVillage)
                                     .Include(x => x.NewlandKhasra)
                                      .Where(x => string.IsNullOrEmpty(model.name) || x.NewlandAwardMaster.AwardNumber.Contains(model.name))
                                     .OrderByDescending(x => x.NewlandVillage.Name)
                           .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandawardplotdetails
                                    .Include(x => x.NewlandAwardMaster)
                                    .Include(x => x.NewlandVillage)
                                    .Include(x => x.NewlandKhasra)
                                     .Where(x => string.IsNullOrEmpty(model.name) || x.NewlandAwardMaster.AwardNumber.Contains(model.name))
                                    .OrderByDescending(x => x.NewlandKhasra.Name)
                          .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandawardplotdetails
                                    .Include(x => x.NewlandAwardMaster)
                                    .Include(x => x.NewlandVillage)
                                    .Include(x => x.NewlandKhasra)
                                     .Where(x => string.IsNullOrEmpty(model.name) || x.NewlandAwardMaster.AwardNumber.Contains(model.name))
                                    .OrderBy(x => x.IsActive)
                          .GetPaged<Newlandawardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }



        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Newlandkhasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }




    }
}
