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
    public class DamagepayeeregisterRepository : GenericRepository<Damagepayeeregister>, IDamagepayeeregisterRepository
    {
        public DamagepayeeregisterRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model)
        {
            var data = await _dbContext.Damagepayeeregister
                
                .Include(x => x.Locality)
                .Include(x => x.District)
                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderBy(x => x.FileNo)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("TYPEOFASSESSEE"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderBy(x => x.TypeOfDamageAssessee)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPERTYNO"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderBy(x => x.PropertyNo)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderBy(x => x.Locality.Name)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISDDADAMAGEPAYEE"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderBy(x => x.IsDdadamagePayee)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                           
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderByDescending(x => x.FileNo)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("TYPEOFASSESSEE"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderByDescending(x => x.TypeOfDamageAssessee)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPERTYNO"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderByDescending(x => x.PropertyNo)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderByDescending(x => x.Locality.Name)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISDDADAMAGEPAYEE"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderByDescending(x => x.IsDdadamagePayee)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Damagepayeeregister
                                               .Include(x => x.Locality)
                                               .Include(x => x.District)
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            return data;
        }

        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregister()
        {
                 return await _dbContext.Damagepayeeregister
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
        public async Task<Damagepayeeregister> GetPropertyPhotoPath(int Id)
        {
            return await _dbContext.Damagepayeeregister.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }


        //********* rpt 1 Persolnal info of damage assesse ***********
        public async Task<bool> SavePayeePersonalInfo(Damagepayeepersonelinfo damagepayeepersonelinfo)
        {
            _dbContext.Damagepayeepersonelinfo.Add(damagepayeepersonelinfo);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeletePayeePersonalInfo(int Id)
         {
            _dbContext.RemoveRange(_dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
       
        public async Task<Damagepayeepersonelinfo> GetPersonelInfoFilePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Damagepayeepersonelinfo> GetAadharFilePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Damagepayeepersonelinfo> GetPanFilePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Damagepayeepersonelinfo> GetPhotographPath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Damagepayeepersonelinfo> GetSignaturePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<List<Damagepayeepersonelinfo>> GetPreviousAssesseRepeater(int id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }

        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteType(List<Allottetype> allottetype)
        {
            await _dbContext.Allottetype.AddRangeAsync(allottetype);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Allottetype>> GetAllottetype(int id)
        { 
             return await _dbContext.Allottetype.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteAllotteType(int Id)
          {
            _dbContext.RemoveRange(_dbContext.Allottetype.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Allottetype> GetATSFilePath(int Id)
        {
            return await _dbContext.Allottetype.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<List<Allottetype>> GetNewAlloteeRepeater(int id)
        {
            return await _dbContext.Allottetype.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }


        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistory(List<Damagepaymenthistory> damagepaymenthistory)
        {
            await _dbContext.Damagepaymenthistory.AddRangeAsync(damagepaymenthistory);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Damagepaymenthistory>> GetPaymentHistory(int id)
        {
            return await _dbContext.Damagepaymenthistory.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeletePaymentHistory(int Id)
         {
            _dbContext.RemoveRange(_dbContext.Damagepaymenthistory.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<Damagepaymenthistory> GetReceiptFilePath(int Id)
        {
            return await _dbContext.Damagepaymenthistory.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<Damagepayeeregister> FetchSingleResult(int id)
        {
            return await _dbContext.Damagepayeeregister
                                     .Include(x => x.Damagepayeepersonelinfo)
                                     .Include(x => x.Damagepaymenthistory)
                                     .Include(x => x.Allottetype)
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateApprovedDamagepayeeRegister(Damagepayeeregister model)
        {
            _dbContext.Damagepayeeregister.Add(model);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> SavePersonelInfo(List<Damagepayeepersonelinfo> data)
        {
            await _dbContext.Damagepayeepersonelinfo.AddRangeAsync(data);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

      
      
    }
}
