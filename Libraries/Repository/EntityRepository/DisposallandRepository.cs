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

     public class DisposallandRepository : GenericRepository<Disposalland>, IDisposallandRepository
    {
        public DisposallandRepository(DataContext dbContext) : base(dbContext)
        {

        }

        //public async Task<List<Disposalland>> GetDisposalland()
        //{
        //    return await _dbContext.Disposalland.ToListAsync();
        //}
        public async Task<List<Disposalland>> GetAllDisposalland()
        {
            //return await _dbContext.Disposalland.Include(x => x.Village).Include(x => x.Khasra).Include(x => x.Utilizationtype).Include(x => x.TransferToWhichDept).Include(x => x.AreaDisposed).Include(x => x.DateOfDisposed).Include(x => x.TransferTo).Include(x => x.TransferBy).Include(x => x.FileNoRefNo).Include(x => x.Remarks).ToListAsync();
            return await _dbContext.Disposalland.Include(x => x.Village).Include(x => x.Khasra).Include(x => x.Utilizationtype).OrderByDescending(x => x.Id).ToListAsync();
            //return await _dbContext.Disposalland.OrderByDescending(x => x.Id).ToListAsync();

        }
        public async Task<List<Utilizationtype>> GetAllUtilizationtype()
        {
            List<Utilizationtype> utilizationtypeList = await _dbContext.Utilizationtype.Where(x => x.IsActive == 1).ToListAsync();
            return utilizationtypeList;
        }
        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villageList = await _dbContext.Village.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }

        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        public async Task<PagedResult<Disposalland>> GetPagedDisposalLand(DisposalLandSearchDto model)
        {
            var data = await _dbContext.Disposalland
                                      .Include(x => x.Village)
                                      .Include(x => x.Khasra)
                                      .Include(x => x.Utilizationtype)

                                      .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                      && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra))
                                      //&& (string.IsNullOrEmpty(model.utilizationtype) || x.Utilizationtype.Name.Contains(model.utilizationtype))
                                      )
                                      .GetPaged<Disposalland>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Disposalland
                                      .Include(x => x.Village)
                                      .Include(x => x.Khasra)
                                      .Include(x => x.Utilizationtype)
                                      .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                       && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra))
                                      //&& (string.IsNullOrEmpty(model.utilizationtype) || x.Utilizationtype.Name.Contains(model.utilizationtype))
                                      )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "VILLAGE" ? ( s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRA" ? (s.Khasra == null ? null : s.Khasra.Name)
                                : s.Village.Name)
                                )
                                .GetPaged<Disposalland>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Disposalland
                                      .Include(x => x.Village)
                                      .Include(x => x.Khasra)
                                      .Include(x => x.Utilizationtype)
                                      .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                       && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra))
                                      //&& (string.IsNullOrEmpty(model.utilizationtype) || x.Utilizationtype.Name.Contains(model.utilizationtype))
                                      )
                                .OrderByDescending(s =>
                                (model.SortBy.ToUpper() == "VILLAGE" ? (s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRA" ? (s.Khasra == null ? null : s.Khasra.Name) : s.Village.Name)
                                )
                                .GetPaged<Disposalland>(model.PageNumber, model.PageSize);
            }
            return data;
        }
    }
}
