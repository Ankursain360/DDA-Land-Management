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
    public class SakanidetailRepository : GenericRepository<Saknidetails>, ISakanidetailRepository
    {

        public SakanidetailRepository(DataContext dbContext) : base(dbContext)
        {

        }
       
        public async Task<PagedResult<Saknidetails>> GetPagedSaknidetail(SakaniDetailsSearchDto model)
        {
            var data = await _dbContext.Saknidetails
                                   .Include(x => x.Village)
                                   .Include(x => x.Khasra)
                                   .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                    && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                   .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Saknidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.Village.Name)
                                                .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Saknidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.Khasra.Name)
                                                .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("YEAR"):
                        data = null;
                        data = await _dbContext.Saknidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.YearOfjamabandi)
                                                .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Saknidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.IsActive)
                                                .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);


                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Saknidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.Village.Name)
                                                .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Saknidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.Khasra.Name)
                                                .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("YEAR"):
                        data = null;
                        data = await _dbContext.Saknidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.YearOfjamabandi)
                                                .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Saknidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.IsActive)
                                                .GetPaged<Saknidetails>(model.PageNumber, model.PageSize);


                        break;

                }
            }
            return data;
        }


        public async Task<List<Saknidetails>> GetAllSaknidetail()
        {
            return await _dbContext.Saknidetails.Include(x => x.Village).Include(x => x.Khasra).ToListAsync();
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }

        public async Task<List<Khasra>> GetAllKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }




        //********* rpt ! Owner Details **********
        public async Task<bool> SaveOwner(Sakniowner owner)
        {
            _dbContext.Sakniowner.Add(owner);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Sakniowner>> GetAllOwner(int id)
        {
            return await _dbContext.Sakniowner.Where(x => x.SakniDetailId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeleteOwner(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Sakniowner.Where(x => x.SakniDetailId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* rpt ! Lessee Details **********
        public async Task<bool> Savelessee(Saknilessee lessee)
        {
            _dbContext.Saknilessee.Add(lessee);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Saknilessee>> GetAllSaknilessee(int id)
        {
            return await _dbContext.Saknilessee.Where(x => x.SakniDetailId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> Deletelessee(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Saknilessee.Where(x => x.SakniDetailId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* rpt ! Tenant Details **********

        public async Task<bool> SaveTenant(Saknitenant tenant)
        {
            _dbContext.Saknitenant.Add(tenant);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Saknitenant>> GetAllTenant(int id)
        {
            return await _dbContext.Saknitenant.Where(x => x.SakniDetailId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteTenant(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Saknitenant.Where(x => x.SakniDetailId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


        //*********  sakni Khasra Details **********

        public async Task<bool> SaveSaknikhasra(Saknikhasra sakniKhasra)
        {
            _dbContext.Saknikhasra.Add(sakniKhasra);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> UpdateKhasra(int id, Saknikhasra skh)
        {
            var result = await FetchSingleSaknikhasra(id);
            Saknikhasra model = result;
            model.KhasraId = skh.KhasraId;
            model.PlotNo = skh.PlotNo;
            model.Category = skh.Category;
            model.AreaSqYard = skh.AreaSqYard;
            model.LeaseAmount = skh.LeaseAmount;
            model.RenewalDate = skh.RenewalDate;


            model.IsActive = skh.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = skh.ModifiedBy;
          
            _dbContext.Saknikhasra.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<Saknikhasra>> GetAllSaknikhasra(int id)
        {
            return await _dbContext.Saknikhasra.Where(x => x.SakniDetailId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeleteSaknikhasra(int Id)
        {
            _dbContext.Remove(_dbContext.Sakniowner.Where(x => x.SakniDetailId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Saknikhasra> FetchSingleSaknikhasra(int id)
        {
            return await _dbContext.Saknikhasra.Where(x => x.SakniDetailId == id)
                                   .OrderByDescending(s => s.Id)
                                   .FirstOrDefaultAsync();
        }
       
       
    }
}
