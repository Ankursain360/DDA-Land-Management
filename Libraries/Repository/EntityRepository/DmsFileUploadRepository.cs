using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class DmsFileUploadRepository : GenericRepository<Dmsfileupload>, IDmsFileUploadRepository
    {
        public DmsFileUploadRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model)
        {
            return await _dbContext.Damagepayeeregister
                .Where(x => x.IsActive == 1)
                .Include(x => x.Locality)
                .Include(x => x.District)
                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregisterTemp()
        {
            return await _dbContext.Damagepayeeregister
           .Where(x => x.IsActive == 1)
           .Include(x => x.Locality)
           .Include(x => x.District)
           .ToListAsync();
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            var districtList = await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
            return districtList;
        }
        //********* rpt 1 Persolnal info of damage assesse ***********
        public async Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfo damagepayeepersonelinfotemp)
        {
            _dbContext.Damagepayeepersonelinfo.Add(damagepayeepersonelinfotemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfoTemp(int id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeletePayeePersonalInfoTemp(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Damagepayeepersonelinfo> GetPersonelInfoFilePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteTypeTemp(List<Allottetype> allottetypetemp)
        {
            await _dbContext.Allottetype.AddRangeAsync(allottetypetemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Allottetype>> GetAllottetypeTemp(int id)
        {
            return await _dbContext.Allottetype.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteAllotteTypeTemp(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Allottetype.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Allottetype> GetAllotteTypeSingleResult(int id)
        {
            return await _dbContext.Allottetype.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistory> damagepaymenthistorytemp)
        {
            await _dbContext.Damagepaymenthistory.AddRangeAsync(damagepaymenthistorytemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Damagepaymenthistory>> GetPaymentHistoryTemp(int id)
        {
            return await _dbContext.Damagepaymenthistory.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeletePaymentHistoryTemp(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Damagepaymenthistory.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<Damagepayeeregister> FetchSelfAssessmentUserId(int userId)
        {
            return await _dbContext.Damagepayeeregister
                                    .Include(x => x.Damagepayeepersonelinfo)
                                    .Include(x => x.Damagepaymenthistory)
                                    .Include(x => x.Allottetype)
                                    .Where(x => x.UserId == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<Rebate> GetRebateValue()
        {
            return await _dbContext.Rebate
                              .Where(x => x.IsActive == 1 && x.IsRebateOn == 1
                              && x.FromDate <= DateTime.Now && x.ToDate >= DateTime.Now
                              )
                              .FirstOrDefaultAsync();
        }
        public async Task<Damagepaymenthistory> GetPaymentHistorySingleResult(int id)
        {
            return await _dbContext.Damagepaymenthistory.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public string GetLocalityName(int? localityId)
        {
            var File = (from f in _dbContext.Locality
                        where f.Id == localityId
                        select f.LocalityCode).First();

            return File;
        }

        public async Task<bool> UpdateBeforeApproval(int id, Damagepayeeregister damagepayeeregister)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Department>> GetDepartmentList()
        {
            return await _dbContext.Department
                                     .Where(x => x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Propertyregistration>> GetKhasraNoList()
        {
            return await _dbContext.Propertyregistration
                                     .Where(x => x.IsActive == 1 && x.IsDeleted != 0 && x.IsValidate == 1 && x.IsDisposed != 0
                                    // && (x.KhasraNo != DBNull.Value || x.KhasraNo != null || x.KhasraNo != string.Empty)
                                     )
                                     .ToListAsync();
        }

        public async Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model)
        {
            var data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId)
                                        && (x.LocalityId == (model.localityId == 0 ? x.LocalityId : model.localityId))
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                        .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId)
                                        && (x.LocalityId == (model.localityId == 0 ? x.LocalityId : model.localityId))
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                : model.SortBy.ToUpper() == "DEPARTMENT" ? (s.Department == null ? null : s.Department.Name)
                                : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality != null ? s.Locality.Name : null)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.KhasraNo : null) : s.FileNo)
                                )
                                .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId)
                                        && (x.LocalityId == (model.localityId == 0 ? x.LocalityId : model.localityId))
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                .OrderByDescending(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                : model.SortBy.ToUpper() == "DEPARTMENT" ? (s.Department == null ? null : s.Department.Name)
                                : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality != null ? s.Locality.Name : null)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.KhasraNo : null) : s.FileNo)
                                )
                                .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            }
            return data;
        }

        public async Task<Dmsfileupload> FetchSingleResult(int id)
        {
            return await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();


        }
    }
}
