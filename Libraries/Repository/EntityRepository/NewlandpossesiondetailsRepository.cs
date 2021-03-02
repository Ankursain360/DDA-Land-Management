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
using Dto.Master;
using Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public class NewlandpossesiondetailsRepository : GenericRepository<Newlandpossessiondetails>, INewlandpossesiondetailsRepository
    {

        public NewlandpossesiondetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllPossKhasra()
        {
            List<Newlandkhasra> posskhasraList = await _dbContext.Newlandkhasra.Where(x => x.IsActive == 1).ToListAsync();
            return posskhasraList;
        }
        public async Task<List<Undersection17>> GetAllus17()
        {
            List<Undersection17> us17List = await _dbContext.Undersection17.Where(x => x.IsActive == 1).ToListAsync();
            return us17List;
        }
        public async Task<List<Undersection4>> GetAllus4()
        {
            List<Undersection4> us4List = await _dbContext.Undersection4.Where(x => x.IsActive == 1).ToListAsync();
            return us4List;
        }
        public async Task<List<Undersection6>> GetAllus6()
        {
            List<Undersection6> us6List = await _dbContext.Undersection6.Where(x => x.IsActive == 1).ToListAsync();
            return us6List;
        }

        public async Task<List<Newlandkhasra>> BindKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _dbContext.Newlandkhasra
                .Where(x => x.NewLandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }


        public async Task<List<Newlandpossessiondetails>> GetAllPossessiondetails()
        {
            return await _dbContext.Newlandpossessiondetails.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<PagedResult<Newlandpossessiondetails>> GetPagedNoPossessiondetails(NewlandpossesiondetailsSearchDto model)
        {
            var data = await _dbContext.Newlandpossessiondetails
                .Include(x => x.Village).Include(x => x.Khasra)
                .Include(x => x.Us17).Include(x => x.Us4)
                .Include(x => x.Us6)
                
                .Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))
                               ). GetPaged<Newlandpossessiondetails>(model.PageNumber, model.PageSize);









            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandpossessiondetails
                .Include(x => x.Village).Include(x => x.Khasra)
                 .Include(x => x.Us17).Include(x => x.Us4)
                .Include(x => x.Us6)
                .Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                  )
                                    .OrderBy(s => s.Village.Name)
                                .GetPaged<Newlandpossessiondetails>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandpossessiondetails
                .Include(x => x.Village).Include(x => x.Khasra)
                 .Include(x => x.Us17).Include(x => x.Us4)
                .Include(x => x.Us6)
                .Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                              )
                           .OrderByDescending(s => s.IsActive)
                                .GetPaged<Newlandpossessiondetails>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandpossessiondetails
                .Include(x => x.Village).Include(x => x.Khasra)
                 .Include(x => x.Us17).Include(x => x.Us4)
                .Include(x => x.Us6).Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                  )
                                    .OrderByDescending(s => s.Village.Name)
                                .GetPaged<Newlandpossessiondetails>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandpossessiondetails
                .Include(x => x.Village).Include(x => x.Khasra).Include(x => x.Us17).Include(x => x.Us4)
                .Include(x => x.Us6).Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))
                        )          .OrderBy(s => s.IsActive)
                                .GetPaged<Newlandpossessiondetails>(model.PageNumber, model.PageSize);
                        break;

                }
            }


            return data;


        }



        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Newlandkhasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }

          }
}
