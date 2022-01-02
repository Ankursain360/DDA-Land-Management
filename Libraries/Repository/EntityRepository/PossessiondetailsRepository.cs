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
   public class PossessiondetailsRepository : GenericRepository<Possessiondetails>, IPossessiondetailsRepository
    {

        public PossessiondetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }


        public async Task<List<Possessiondetails>> GetAllPossessiondetails()
        {
            return await _dbContext.Possessiondetails.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<PagedResult<Possessiondetails>> GetPagedNoPossessiondetails(PossessiondetailsSearchDto model)
        {
            var data = await _dbContext.Possessiondetails
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))
                 

               ).




                GetPaged<Possessiondetails>(model.PageNumber, model.PageSize);









            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Possessiondetails
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                  )
                                    .OrderBy(s => s.Village.Name)
                                .GetPaged<Possessiondetails>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Possessiondetails
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))


              )
                           .OrderByDescending(s => s.IsActive)
                                .GetPaged<Possessiondetails>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Possessiondetails
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                  )
                                    .OrderByDescending(s => s.Village.Name)
                                .GetPaged<Possessiondetails>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Possessiondetails
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))


              )
                           .OrderBy(s => s.IsActive)
                                .GetPaged<Possessiondetails>(model.PageNumber, model.PageSize);
                        break;

                }
            }


            return data;


        }



        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }

        public async Task<PagedResult<Possessiondetails>> GetPagedPossessionReport(PossessionReportSearchDto model)
        {
            var data = await _dbContext.Possessiondetails
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.PossDate ==( model.PossessionDate == "0" ? x.PossDate : Convert.ToDateTime(model.PossessionDate))
                                        && x.IsActive == 1
                                        )
                                        .GetPaged<Possessiondetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Possessiondetails
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.PossDate == (model.PossessionDate == "0" ? x.PossDate : Convert.ToDateTime(model.PossessionDate))
                                        && x.IsActive == 1
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "VILLAGE" ? (s.Village == null ? null : s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRA" ? (s.Khasra != null ? s.Khasra.Name : null) : (s.Village == null ? null : s.Village.Name))
                                )
                                .GetPaged<Possessiondetails>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Possessiondetails
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.PossDate == (model.PossessionDate == "0" ? x.PossDate : Convert.ToDateTime(model.PossessionDate))
                                        && x.IsActive == 1
                                        )
                                .OrderByDescending(s =>
                                 (model.SortBy.ToUpper() == "VILLAGE" ? (s.Village == null ? null : s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRA" ? (s.Khasra != null ? s.Khasra.Name : null) : (s.Village == null ? null : s.Village.Name))
                                )
                                .GetPaged<Possessiondetails>(model.PageNumber, model.PageSize);
            }
            return data;
        }

        public async Task<List<PossessionReportDtoProfile>> BindPossessionDateList()
        {
            try
            {
                //int SortOrder = (int)model.SortOrder;
                var data = await _dbContext.LoadStoredProcedure("BindDropdownForPossessionDate")
                                            .WithOutParams()
                                            .ExecuteStoredProcedureAsync<PossessionReportDtoProfile>();

                return (List<PossessionReportDtoProfile>)data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        public async Task<List<VillageAndKhasraDetailListDto>> GetPagedvillageAndKhasradetailsList(VillageAndKhasraDetailsSearchDto model)
         {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("villageandkhasradetails")
                                            .WithSqlParams(("P_khasraId", model.Khasraid))



                                            .ExecuteStoredProcedureAsync<VillageAndKhasraDetailListDto>();

                return (List<VillageAndKhasraDetailListDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<AcquiredLandVillageListSearchDto>> GetPagedKhasraDetails(VillageAndKhasraDetailsSearchDto model)
        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("villageandkhasradetails")
                                            .WithSqlParams(("P_khasraId", model.Khasraid))
                                            .ExecuteStoredProcedureAsync<AcquiredLandVillageListSearchDto>();

                return (List<AcquiredLandVillageListSearchDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }






    }
}
