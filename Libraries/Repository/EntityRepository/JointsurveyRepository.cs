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
namespace Libraries.Repository.EntityRepository
{
    public class JointsurveyRepository : GenericRepository<Jointsurvey>, IJointsurveyRepository
    {
        public JointsurveyRepository(DataContext dbContext) : base(dbContext)
        {

        }





        public async Task<List<Jointsurvey>> GetAllJointSurvey()
        {
            return await _dbContext.Jointsurvey.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> KhasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return KhasraList;
        }


        public async Task<PagedResult<Jointsurvey>> GetPagedJointsurvey(JointSurveySearchDto model)
        {
            var data = await _dbContext.Jointsurvey
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
                                        && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))
                                        ).GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Jointsurvey
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
                                        && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))
                                        )
                                .OrderBy(s =>
                                (
                                  model.SortBy.ToUpper() == "VILLAGENAME" ? (s.Village != null ? s.Village.Name : null)
                                : model.SortBy.ToUpper() == "KHASRANAME" ? (s.Khasra != null ? s.Khasra.Name : null) : s.Village.Name)
                                )
                                .GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
            }

            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Jointsurvey
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
                                        && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))
                                        )
                                .OrderByDescending(s =>
                                (
                                model.SortBy.ToUpper() == "villagename" ? (s.Village != null ? s.Village.Name : null)
                                : model.SortBy.ToUpper() == "khasraname" ? (s.Khasra != null ? s.Khasra.Name : null) : s.Village.Name)
                                )
                                .GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
            }

            return data;



            //return await _dbContext.Jointsurvey.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Jointsurveysitepositionmapped>> BindJointSiteMapped(int jointsurveyid)
        {
            List<Jointsurveysitepositionmapped> olist = new List<Jointsurveysitepositionmapped>();

            var Data = await (from A in _dbContext.Siteposition
                              join B in _dbContext.Jointsurveysitepositionmapped on A.Id equals B.SitePositionId
                              into combine
                              from C in combine.DefaultIfEmpty()
                              where A.IsActive == 1 && C.JointSurveyId == (jointsurveyid == 0 ? C.JointSurveyId : jointsurveyid)
                              select new
                              {
                                  Id = A.Id,
                                  SitePositionName = A.Name,
                                  SitePositionId = A.Id,
                                  JointSurveyId = C.JointSurveyId, 
                                  IsAvailable = C == null ? 0 : C.IsAvailable
                              }).OrderByDescending(x => x.Id).ToListAsync();

            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)

                {
                    olist.Add(new Jointsurveysitepositionmapped()
                    {
                        Id = Data[i].Id,
                        SitePositionName = Data[i].SitePositionName,
                        SitePositionId = Data[i].SitePositionId,
                        JointSurveyId = Data[i].JointSurveyId,
                        IsAvailable = Data[i].IsAvailable,
                        checkboxchecked = Data[i].IsAvailable == 1 ? true : false
                    });
                }
            }
            else
            {
                var sitepositiondata = await _dbContext.Siteposition.Where(x => x.IsActive == 1).ToListAsync();
                for (int i = 0; i < sitepositiondata.Count; i++)

                {
                    olist.Add(new Jointsurveysitepositionmapped()
                    {
                        Id = 0,
                        SitePositionName = sitepositiondata[i].Name,
                        SitePositionId = sitepositiondata[i].Id,
                        JointSurveyId = 0,
                        IsAvailable = 0,
                        checkboxchecked =  false
                    });
                }
            }
            return (olist);


        }

        public async Task<bool> SaveSitePosition(List<Jointsurveysitepositionmapped> jointsurveysitepositionmappeds)
        {
            int jointsurveyId = jointsurveysitepositionmappeds.FirstOrDefault().JointSurveyId;
            _dbContext.RemoveRange(_dbContext.Jointsurveysitepositionmapped.Where(x => x.JointSurveyId == jointsurveyId));
            var Result = await _dbContext.SaveChangesAsync();

            List<Jointsurveysitepositionmapped> mappeds = new List<Jointsurveysitepositionmapped>();
            for (int i = 0; i < jointsurveysitepositionmappeds.Count; i++)
            {
                mappeds.Add(new Jointsurveysitepositionmapped
                {
                    SitePositionId =jointsurveysitepositionmappeds[i].SitePositionId,
                    JointSurveyId = jointsurveysitepositionmappeds[i].JointSurveyId,
                    IsAvailable = jointsurveysitepositionmappeds[i].IsAvailable,
                    CreatedBy = jointsurveysitepositionmappeds[i].CreatedBy,
                    CreatedDate = DateTime.Now
                });
            }

            foreach(var item in mappeds)
            {
                await _dbContext.Jointsurveysitepositionmapped.AddAsync(item);

            }

           // await _dbContext.Jointsurveysitepositionmapped.AddRangeAsync(mappeds);
            var Result1 = await _dbContext.SaveChangesAsync();
            return Result1 > 0 ? true : false;
        }
    }
}
