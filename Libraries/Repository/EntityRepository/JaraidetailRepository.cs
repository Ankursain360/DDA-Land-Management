using Dto.Search;
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


namespace Libraries.Repository.EntityRepository
{
    public class JaraidetailRepository : GenericRepository<Jaraidetails>, IJaraidetailRepository
    {
        public JaraidetailRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Jaraidetails>> GetPagedJaraidetail(JaraiDetailsSearchDto model)
        {
            var data = await _dbContext.Jaraidetails
                                   .Include(x => x.Village)
                                   .Include(x => x.Khasra)
                                   .Where(x =>(string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                    && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                   .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Jaraidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.Village.Name)
                                                .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);
                        
                                    
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Jaraidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.Khasra.Name)
                                                .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);
                       
                        break;
                    case ("YEAR"):
                        data = null;
                        data = await _dbContext.Jaraidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.YearOfjamabandi)
                                                .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Jaraidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.IsActive)
                                                .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);
                        
                                  
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Jaraidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.Village.Name)
                                                .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Jaraidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.Khasra.Name)
                                                .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("YEAR"):
                        data = null;
                        data = await _dbContext.Jaraidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderByDescending(a => a.YearOfjamabandi)
                                                .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Jaraidetails
                                                .Include(x => x.Village)
                                                .Include(x => x.Khasra)
                                                .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                                .OrderBy(a => a.IsActive)
                                                .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);


                        break;

                }
            }
            return data;
        }

      
        public async Task<List<Jaraidetails>> GetAllJaraidetail()
        {
            return await _dbContext.Jaraidetails.Include(x => x.Village).Include(x => x.Khasra).ToListAsync();
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

        public async Task<bool> SaveOwner(Jaraiowner Jaraiowner)
        {
            _dbContext.Jaraiowner.Add(Jaraiowner);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Jaraiowner>> GetAllOwner(int id)
        {
            return await _dbContext.Jaraiowner.Where(x => x.JaraiDetailId == id && x.IsActive == 1).ToListAsync();
        }
    
        public async Task<bool> DeleteOwner(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Jaraiowner.Where(x => x.JaraiDetailId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* rpt ! Lessee Details **********

        public async Task<bool> SaveJarailessee(Jarailessee Jarailessee)
        {
            _dbContext.Jarailessee.Add(Jarailessee);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Jarailessee>> GetAllJarailessee(int id)
        {
            return await _dbContext.Jarailessee.Where(x => x.JaraiDetailId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteJarailessee(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Jarailessee.Where(x => x.JaraiDetailId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* rpt ! Farmer Details **********

        public async Task<bool> Savefarmer(Jaraifarmer farmer)
        {
            _dbContext.Jaraifarmer.Add(farmer);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Jaraifarmer>> GetAllFarmer(int id)
        {
            return await _dbContext.Jaraifarmer.Where(x => x.JaraiDetailId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteFarmer(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Jaraifarmer.Where(x => x.JaraiDetailId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


    }
}
