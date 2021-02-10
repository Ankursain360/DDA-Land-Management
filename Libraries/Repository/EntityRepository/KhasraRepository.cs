using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
    public class KhasraRepository : GenericRepository<Khasra>, IKhasraRepository
    {
        public KhasraRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Khasra>> GetPagedKhasra(KhasraMasterSearchDto model)
        {
            return await _dbContext.Khasra.
                Where(x => x.IsActive == 1).Include(x => x.Acquiredlandvillage).
                Include(x => x.LandCategory).GetPaged<Khasra>(model.PageNumber, model.PageSize);
            //var data = await _dbContext.Khasra.Include(s => s.Locality)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))

            //            .GetPaged<Khasra>(model.PageNumber, model.PageSize);
            //int SortOrder = (int)model.SortOrder;
            //if (SortOrder == 1)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("DEPARTMENT"):
            //            data = null;
            //            data = await _dbContext.Khasra
            //                .Include(s => s.Locality)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))

            //                .OrderBy(x => x.Locality.Name)
            //                .GetPaged<Khasra>(model.PageNumber, model.PageSize);
            //            break;


            //        case ("NAME"):
            //            data = null;
            //            data = await _dbContext.Khasra
            //                .Include(s => s.Locality)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))

            //                .OrderBy(x => x.Name)
            //                .GetPaged<Khasra>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("CODE"):
            //            data = null;
            //            data = await _dbContext.Branch
            //                .Include(s => s.Department)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
            //                .OrderBy(x => x.Code)
            //                .GetPaged<Branch>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("ISACTIVE"):
            //            data = null;
            //            data = await _dbContext.Branch
            //                .Include(s => s.Department)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
            //                .OrderByDescending(x => x.IsActive)
            //                .GetPaged<Branch>(model.PageNumber, model.PageSize);
            //            break;


            //    }
            //}
            //else if (SortOrder == 2)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("DEPARTMENT"):
            //            data = null;
            //            data = await _dbContext.Branch
            //                .Include(s => s.Department)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
            //                .OrderByDescending(x => x.Department.Name)
            //                .GetPaged<Branch>(model.PageNumber, model.PageSize);
            //            break;


            //        case ("NAME"):
            //            data = null;
            //            data = await _dbContext.Branch
            //                .Include(s => s.Department)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
            //                .OrderByDescending(x => x.Name)
            //                .GetPaged<Branch>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("CODE"):
            //            data = null;
            //            data = await _dbContext.Branch
            //                .Include(s => s.Department)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
            //                .OrderByDescending(x => x.Code)
            //                .GetPaged<Branch>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("ISACTIVE"):
            //            data = null;
            //            data = await _dbContext.Branch
            //                .Include(s => s.Department)
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
            //                .OrderBy(x => x.IsActive)
            //                .GetPaged<Branch>(model.PageNumber, model.PageSize);
            //            break;


            //    }
            //}
            //return data;
        }



        public async Task<List<LandCategory>> GetAllLandCategory()
        {
            List<LandCategory> landcategoryList = await _dbContext.LandCategory.ToListAsync();
            return landcategoryList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.ToListAsync();
            return villageList;
        }




        public async Task<List<Khasra>> GetAllKhasra()
        {
            return await _dbContext.Khasra.Include(x => x.LandCategory).Include(x => x.Acquiredlandvillage).OrderByDescending(x => x.Id).ToListAsync();
        }






    }
}
