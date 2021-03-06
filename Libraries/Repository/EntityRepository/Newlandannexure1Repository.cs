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
   public class Newlandannexure1Repository : GenericRepository<Newlandannexure1>, INewlandannexure1Repository
    {
        public Newlandannexure1Repository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandannexure1>> GetPagedNewlandannexure1(Newlandannexure1SearchDto model)
        {
            var data = await _dbContext.Newlandannexure1
                                   .Include(x => x.District)
                                   .Include(x => x.Municipality)
                                   //.Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                   // && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                   .GetPaged<Newlandannexure1>(model.PageNumber, model.PageSize);

            //int SortOrder = (int)model.SortOrder;
            //if (SortOrder == 1)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("LOCALITY"):
            //            data = null;
            //            data = await _dbContext.Jaraidetails
            //                                    .Include(x => x.Village)
            //                                    .Include(x => x.Khasra)
            //                                    .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
            //                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
            //                                    .OrderBy(a => a.Village.Name)
            //                                    .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);


            //            break;
            //        case ("KHASRA"):
            //            data = null;
            //            data = await _dbContext.Jaraidetails
            //                                    .Include(x => x.Village)
            //                                    .Include(x => x.Khasra)
            //                                    .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
            //                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
            //                                    .OrderBy(a => a.Khasra.Name)
            //                                    .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);

            //            break;
            //        case ("YEAR"):
            //            data = null;
            //            data = await _dbContext.Jaraidetails
            //                                    .Include(x => x.Village)
            //                                    .Include(x => x.Khasra)
            //                                    .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
            //                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
            //                                    .OrderBy(a => a.YearOfjamabandi)
            //                                    .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);

            //            break;

            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Jaraidetails
            //                                    .Include(x => x.Village)
            //                                    .Include(x => x.Khasra)
            //                                    .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
            //                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
            //                                    .OrderByDescending(a => a.IsActive)
            //                                    .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);


            //            break;


            //    }
            //}
            //else if (SortOrder == 2)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("LOCALITY"):
            //            data = null;
            //            data = await _dbContext.Jaraidetails
            //                                    .Include(x => x.Village)
            //                                    .Include(x => x.Khasra)
            //                                    .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
            //                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
            //                                    .OrderByDescending(a => a.Village.Name)
            //                                    .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);


            //            break;
            //        case ("KHASRA"):
            //            data = null;
            //            data = await _dbContext.Jaraidetails
            //                                    .Include(x => x.Village)
            //                                    .Include(x => x.Khasra)
            //                                    .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
            //                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
            //                                    .OrderByDescending(a => a.Khasra.Name)
            //                                    .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);

            //            break;
            //        case ("YEAR"):
            //            data = null;
            //            data = await _dbContext.Jaraidetails
            //                                    .Include(x => x.Village)
            //                                    .Include(x => x.Khasra)
            //                                    .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
            //                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
            //                                    .OrderByDescending(a => a.YearOfjamabandi)
            //                                    .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);

            //            break;

            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Jaraidetails
            //                                    .Include(x => x.Village)
            //                                    .Include(x => x.Khasra)
            //                                    .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
            //                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
            //                                    .OrderBy(a => a.IsActive)
            //                                    .GetPaged<Jaraidetails>(model.PageNumber, model.PageSize);


            //            break;

            //    }
            //}
            return data;
        }
        public async Task<List<Newlandannexure1>> GetAllNewlandannexure1()
        {
            return await _dbContext.Newlandannexure1.Include(x => x.District).Include(x => x.Municipality).ToListAsync();
        }
        public async Task<Newlandannexure1> FetchSingleResult(int id)
        {
            return await _dbContext.Newlandannexure1
                                     .Include(x => x.District)
                                     .Include(x => x.Municipality)
                                   .Where(x => x.RequestId == id)
                                   .OrderByDescending(s => s.Id)
                                   .FirstOrDefaultAsync();
        }
        public async Task<List<Muncipality>> GetAllMunicipality()
        {
            List<Muncipality> list = await _dbContext.Muncipality.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<District>> GetAllDistrict()
        {
            List<District> list = await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }


        //********* rpt ! Khasra Details **********

        public async Task<bool> SaveKhasraRpt(Newlandannexure1khasrarpt Khasra)
        {
            _dbContext.Newlandannexure1khasrarpt.Add(Khasra);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Newlandannexure1khasrarpt>> GetAllKhasraRpt(int id)
        {
            return await _dbContext.Newlandannexure1khasrarpt.Where(x => x.NewLandAnnexure1Id == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteKhasraRpt(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Newlandannexure1khasrarpt.Where(x => x.NewLandAnnexure1Id == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
    }
}
