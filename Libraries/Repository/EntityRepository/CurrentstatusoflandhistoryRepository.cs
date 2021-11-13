using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class CurrentstatusoflandhistoryRepository : GenericRepository<Currentstatusoflandhistory>, ICurrentstatusoflandhistoryRepository

    {

        public CurrentstatusoflandhistoryRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Currentstatusoflandhistory>> GetCurrentstatusoflandhistory(int id)
        {
            return await _dbContext.Currentstatusoflandhistory
                .Where(x => x.LandTransferId == id).Include(x => x.LandTransfer).ToListAsync();
        }

        public async Task<Currentstatusoflandhistory> FetchSingleResult(int id)
        {
            return await _dbContext.Currentstatusoflandhistory
               .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<PagedResult<Currentstatusoflandhistory>> GetPagedCurrentstatusoflandhistory(CurrentstatusoflandhistorySearchDto model)
        {
            try
            {
                if (model.fromDate != null && model.toDate != null)
                {
                    var data = await _dbContext.Currentstatusoflandhistory
                        .Include(x => x.LandTransfer)
                        .Include(x => x.LandTransfer.PropertyRegistration)
                        .Include(x => x.LandTransfer.PropertyRegistration.Department)
                        .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                        .Include(x => x.LandTransfer.PropertyRegistration.Division)
                        .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                        .Where(x => x.LandTransferId == model.landtransferId
                         && (x.CreatedDate >= model.fromDate && x.CreatedDate <= model.toDate))
                        .GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);



                    int SortOrder = (int)model.SortOrder;
                    if (SortOrder == 1)
                    {
                        switch (model.SortBy.ToUpper())
                        {

                            case ("DEPARTMENT"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                        .Include(x => x.LandTransfer)
                        .Include(x => x.LandTransfer.PropertyRegistration)
                        .Include(x => x.LandTransfer.PropertyRegistration.Department)
                        .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                        .Include(x => x.LandTransfer.PropertyRegistration.Division)
                        .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                        .Where(x => x.LandTransferId == model.landtransferId
                         && (x.CreatedDate >= model.fromDate && x.CreatedDate <= model.toDate))
                       .OrderBy(x => x.LandTransfer.PropertyRegistration.Department.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;

                            case ("ZONE"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                       .Include(x => x.LandTransfer)
                       .Include(x => x.LandTransfer.PropertyRegistration)
                       .Include(x => x.LandTransfer.PropertyRegistration.Department)
                       .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                       .Include(x => x.LandTransfer.PropertyRegistration.Division)
                       .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                       .Where(x => x.LandTransferId == model.landtransferId
                        && (x.CreatedDate >= model.fromDate && x.CreatedDate <= model.toDate))
                      .OrderBy(x => x.LandTransfer.PropertyRegistration.Zone.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;


                            case ("DIVISION"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                        .Include(x => x.LandTransfer)
                        .Include(x => x.LandTransfer.PropertyRegistration)
                        .Include(x => x.LandTransfer.PropertyRegistration.Department)
                        .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                        .Include(x => x.LandTransfer.PropertyRegistration.Division)
                        .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                        .Where(x => x.LandTransferId == model.landtransferId
                         && (x.CreatedDate >= model.fromDate && x.CreatedDate <= model.toDate))
                       .OrderBy(x => x.LandTransfer.PropertyRegistration.Division.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;





                        }
                    }
                    else if (SortOrder == 2)
                    {
                        switch (model.SortBy.ToUpper())
                        {

                            case ("DEPARTMENT"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                       .Include(x => x.LandTransfer)
                       .Include(x => x.LandTransfer.PropertyRegistration)
                       .Include(x => x.LandTransfer.PropertyRegistration.Department)
                       .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                       .Include(x => x.LandTransfer.PropertyRegistration.Division)
                       .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                       .Where(x => x.LandTransferId == model.landtransferId
                        && (x.CreatedDate >= model.fromDate && x.CreatedDate <= model.toDate))
                      .OrderByDescending(x => x.LandTransfer.PropertyRegistration.Department.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;

                            case ("ZONE"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                       .Include(x => x.LandTransfer)
                       .Include(x => x.LandTransfer.PropertyRegistration)
                       .Include(x => x.LandTransfer.PropertyRegistration.Department)
                       .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                       .Include(x => x.LandTransfer.PropertyRegistration.Division)
                       .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                       .Where(x => x.LandTransferId == model.landtransferId
                        && (x.CreatedDate >= model.fromDate && x.CreatedDate <= model.toDate))
                      .OrderByDescending(x => x.LandTransfer.PropertyRegistration.Zone.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;


                            case ("DIVISION"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                        .Include(x => x.LandTransfer)
                        .Include(x => x.LandTransfer.PropertyRegistration)
                        .Include(x => x.LandTransfer.PropertyRegistration.Department)
                        .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                        .Include(x => x.LandTransfer.PropertyRegistration.Division)
                        .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                        .Where(x => x.LandTransferId == model.landtransferId
                         && (x.CreatedDate >= model.fromDate && x.CreatedDate <= model.toDate))
                       .OrderByDescending(x => x.LandTransfer.PropertyRegistration.Division.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;

                        }
                    }










                    return data;
                }
                else
                {
                    var data = await _dbContext.Currentstatusoflandhistory
                        .Include(x => x.LandTransfer)
                        .Include(x => x.LandTransfer.PropertyRegistration)
                        .Include(x => x.LandTransfer.PropertyRegistration.Department)
                        .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                        .Include(x => x.LandTransfer.PropertyRegistration.Division)
                        .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                        .Where(x => x.LandTransferId == model.landtransferId)
                          .GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);







                    int SortOrder = (int)model.SortOrder;
                    if (SortOrder == 1)
                    {
                        switch (model.SortBy.ToUpper())
                        {

                            case ("DEPARTMENT"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                        .Include(x => x.LandTransfer)
                        .Include(x => x.LandTransfer.PropertyRegistration)
                        .Include(x => x.LandTransfer.PropertyRegistration.Department)
                        .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                        .Include(x => x.LandTransfer.PropertyRegistration.Division)
                        .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                        .Where(x => x.LandTransferId == model.landtransferId
                         )
                       .OrderBy(x => x.LandTransfer.PropertyRegistration.Department.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;

                            case ("ZONE"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                       .Include(x => x.LandTransfer)
                       .Include(x => x.LandTransfer.PropertyRegistration)
                       .Include(x => x.LandTransfer.PropertyRegistration.Department)
                       .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                       .Include(x => x.LandTransfer.PropertyRegistration.Division)
                       .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                       .Where(x => x.LandTransferId == model.landtransferId
                        )
                      .OrderBy(x => x.LandTransfer.PropertyRegistration.Zone.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;


                            case ("DIVISION"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                        .Include(x => x.LandTransfer)
                        .Include(x => x.LandTransfer.PropertyRegistration)
                        .Include(x => x.LandTransfer.PropertyRegistration.Department)
                        .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                        .Include(x => x.LandTransfer.PropertyRegistration.Division)
                        .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                        .Where(x => x.LandTransferId == model.landtransferId
                        )
                       .OrderBy(x => x.LandTransfer.PropertyRegistration.Division.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;





                        }
                    }
                    else if (SortOrder == 2)
                    {
                        switch (model.SortBy.ToUpper())
                        {

                            case ("DEPARTMENT"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                       .Include(x => x.LandTransfer)
                       .Include(x => x.LandTransfer.PropertyRegistration)
                       .Include(x => x.LandTransfer.PropertyRegistration.Department)
                       .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                       .Include(x => x.LandTransfer.PropertyRegistration.Division)
                       .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                       .Where(x => x.LandTransferId == model.landtransferId
                       )
                      .OrderByDescending(x => x.LandTransfer.PropertyRegistration.Department.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;

                            case ("ZONE"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                       .Include(x => x.LandTransfer)
                       .Include(x => x.LandTransfer.PropertyRegistration)
                       .Include(x => x.LandTransfer.PropertyRegistration.Department)
                       .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                       .Include(x => x.LandTransfer.PropertyRegistration.Division)
                       .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                       .Where(x => x.LandTransferId == model.landtransferId
                       )
                      .OrderByDescending(x => x.LandTransfer.PropertyRegistration.Zone.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;


                            case ("DIVISION"):
                                data = null;
                                data = await _dbContext.Currentstatusoflandhistory
                        .Include(x => x.LandTransfer)
                        .Include(x => x.LandTransfer.PropertyRegistration)
                        .Include(x => x.LandTransfer.PropertyRegistration.Department)
                        .Include(x => x.LandTransfer.PropertyRegistration.Zone)
                        .Include(x => x.LandTransfer.PropertyRegistration.Division)
                        .Include(x => x.LandTransfer.PropertyRegistration.Locality)
                        .Where(x => x.LandTransferId == model.landtransferId
                        )
                       .OrderByDescending(x => x.LandTransfer.PropertyRegistration.Division.Name).GetPaged<Currentstatusoflandhistory>(model.PageNumber, model.PageSize);
                                break;

                        }
                    }








                    return data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
