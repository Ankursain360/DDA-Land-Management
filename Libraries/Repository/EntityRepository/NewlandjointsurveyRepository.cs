using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class NewLandJointSurveyRepository : GenericRepository<Newlandjointsurvey>, INewLandJointSurveyRepository
    {

        public NewLandJointSurveyRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Newlandjointsurvey>> GetPagedNewLandJointSurvey(NewLandJointSurveySearchDto model)
        {
            var data = await _dbContext.Newlandjointsurvey
                .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                    
                                   .Include(x => x.Khasra)
                                   .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                   .GetPaged<Newlandjointsurvey>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandjointsurvey
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                 .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.Village.Name)
                                                .GetPaged<Newlandjointsurvey>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandjointsurvey
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                 .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.Khasra.Name)
                                                .GetPaged<Newlandjointsurvey>(model.PageNumber, model.PageSize);

                        break;
                   

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandjointsurvey
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.IsActive)
                                                .GetPaged<Newlandjointsurvey>(model.PageNumber, model.PageSize);


                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandjointsurvey
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.Village.Name)
                                                .GetPaged<Newlandjointsurvey>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandjointsurvey
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                 .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.Khasra.Name)
                                                .GetPaged<Newlandjointsurvey>(model.PageNumber, model.PageSize);

                        break;
                   

                       

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandjointsurvey
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village)) && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.IsActive)
                                                .GetPaged<Newlandjointsurvey>(model.PageNumber, model.PageSize);


                        break;

                }
            }
            return data;
        }


        public async Task<List<Newlandjointsurvey>> GetAllNewLandJointSurvey()
        {
            return await _dbContext.Newlandjointsurvey.Include(x => x.Village).Include(x => x.Khasra).ToListAsync();
        }
        public async Task<List<Newlandjointsurvey>> GetAllNewLandJointSurveyList(NewLandJointSurveySearchDto model)
        {
            var data = await _dbContext.Newlandjointsurvey
                .Include(x => x.Zone)
                                   .Include(x => x.Village)

                                   .Include(x => x.Khasra)
                                   .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra))).ToListAsync();
            return data;
        }
        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return zoneList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage(int zoneId)
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
            return villageList;
        }

        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _dbContext.Newlandkhasra.Where(x => x.NewLandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }


        //********* rpt ! Attendance Details **********

        public async Task<bool> SaveAttendance(Newjointsurveyattendancedetail attendance)
        {
            _dbContext.Newjointsurveyattendancedetail.Add(attendance);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Newjointsurveyattendancedetail>> GetAllattendance(int id)
        {
            return await _dbContext.Newjointsurveyattendancedetail.Where(x => x.JointSurveyId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteAttendance(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Newjointsurveyattendancedetail.Where(x => x.JointSurveyId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* rpt ! Survey Report **********

      
        public async Task<bool> SaveSurveyReport(Newjointsurveyreportdetail newjointsurveyreportdetail)
        {
            _dbContext.Newjointsurveyreportdetail.Add(newjointsurveyreportdetail);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Newjointsurveyreportdetail>> GetNewjointsurveyreportdetail(int id)
        {
            return await _dbContext.Newjointsurveyreportdetail.Where(x => x.JointSurveyId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteSurveyReport(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Newjointsurveyreportdetail.Where(x => x.JointSurveyId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<Newjointsurveyreportdetail> GetNewjointsurveyreportdetailFilePath(int Id)
        {
            return await _dbContext.Newjointsurveyreportdetail.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Newjointsurveyreportdetail> GetUploadDocumentFilePath(int Id)
        {
            return await _dbContext.Newjointsurveyreportdetail.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Newlandkhasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }


    }
}

