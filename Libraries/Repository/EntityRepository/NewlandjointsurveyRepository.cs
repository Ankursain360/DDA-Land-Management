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

using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class NewlandjointsurveyRepository : GenericRepository<Newlandjointsurvey>, INewlandjointsurveyRepository
    {
        public NewlandjointsurveyRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return zoneList;
        }

      

        public async Task<List<Newlandvillage>> GetAllVillage(int zoneId)
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage.Where(x =>x.ZoneId==zoneId && x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Newlandkhasra>> GetAllKhasra(int villageId)
        {
            List<Newlandkhasra> khasraList = await _dbContext.Newlandkhasra.Where(x => x.NewLandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }



        public async Task<List<Newlandjointsurvey>> GetAllJointSurvey()
        {
            return await _dbContext.Newlandjointsurvey.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }



        public async Task<Newlandkhasra> FetchSingleKhasraResult(int khasraId)
        {
            return await _dbContext.Newlandkhasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }




        //public async Task<PagedResult<Newlandjointsurvey>> GetPagedJointsurvey(JointSurveySearchDto model)
        //{
        //    var data = await _dbContext.Jointsurvey
        //                                .Include(x => x.Village)
        //                                .Include(x => x.Khasra)
        //                                .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
        //                                && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))
        //                                ).GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
        //    int SortOrder = (int)model.SortOrder;
        //    if (SortOrder == 1)
        //    {
        //        data = null;
        //        data = await _dbContext.Jointsurvey
        //                                .Include(x => x.Village)
        //                                .Include(x => x.Khasra)
        //                                .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
        //                                && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))
        //                                )
        //                        .OrderBy(s =>
        //                        (
        //                          model.SortBy.ToUpper() == "VILLAGENAME" ? (s.Village != null ? s.Village.Name : null)
        //                        : model.SortBy.ToUpper() == "KHASRANAME" ? (s.Khasra != null ? s.Khasra.Name : null) : s.Village.Name)
        //                        )
        //                        .GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
        //    }

        //    else if (SortOrder == 2)
        //    {
        //        data = null;
        //        data = await _dbContext.Jointsurvey
        //                                .Include(x => x.Village)
        //                                .Include(x => x.Khasra)
        //                                .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
        //                                && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))
        //                                )
        //                        .OrderByDescending(s =>
        //                        (
        //                        model.SortBy.ToUpper() == "villagename" ? (s.Village != null ? s.Village.Name : null)
        //                        : model.SortBy.ToUpper() == "khasraname" ? (s.Khasra != null ? s.Khasra.Name : null) : s.Village.Name)
        //                        )
        //                        .GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
        //    }

        //    return data;



        //    //return await _dbContext.Jointsurvey.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
        //}



    }
}
