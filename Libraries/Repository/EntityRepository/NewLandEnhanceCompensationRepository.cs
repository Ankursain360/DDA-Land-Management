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
    public class NewLandEnhanceCompensationRepository : GenericRepository<Newlandenhancecompensation>, INewLandEnhanceCompensationRepository
    {
        public NewLandEnhanceCompensationRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandenhancecompensation>> GetPagedNewlandenhancecompensation(NewlandenhancecompensationSearchDto model)
        {
           
            var data = await _dbContext.Newlandenhancecompensation
                     
                       .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))

                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                        .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEMANDLISTNO"):
                        data = null;
                        data = await _dbContext.Newlandenhancecompensation

                       .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                           .OrderBy(s => s.DemandListNo)
                           .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);

                        break;
                    
                    
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandenhancecompensation
                                    
                       .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                           && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Khasra.Name)
                                    .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandenhancecompensation

                      .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Village.Name)
                                    .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);
                        break;
                   


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandenhancecompensation

                      .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {


                    case ("DEMANDLISTNO"):
                        data = null;
                        data = await _dbContext.Newlandenhancecompensation

                       .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                           .OrderByDescending(s => s.DemandListNo)
                           .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);

                        break;
                   
                    
                    case ("Khasra"):
                        data = null;
                        data = await _dbContext.Newlandenhancecompensation

                       .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Khasra.Name)
                                    .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandenhancecompensation

                      .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Village.Name)
                                    .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);
                        break;
                  


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandenhancecompensation

                      .Include(x => x.Khasra)
                       .Include(x => x.Village)

                        .Where(x => (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Newlandenhancecompensation>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;

        }


       

        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _dbContext.Newlandkhasra.Where(x => x.NewLandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Newlandkhasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }



        public async Task<List<Newlandenhancecompensation>> GetAllNewlandenhancecompensation()
        {
            return await _dbContext.Newlandenhancecompensation.Include(x => x.Khasra).Include(x => x.Village).OrderByDescending(x => x.Id).ToListAsync();
        }

        



    }
}

