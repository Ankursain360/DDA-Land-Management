using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Libraries.Repository.EntityRepository
{
    public class CourtRepository : GenericRepository<Court>, ICourtRepository
    {

        public CourtRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Court>> GetAllCourt()
        {
            return await _dbContext.Court.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Court>> GetPagedCourt(CourtSearchDto model)
        {
            //return await _dbContext.Court.OrderByDescending(x => x.Id).GetPaged<Court>(model.PageNumber, model.PageSize);
            var data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .OrderBy(x => x.Name)
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
                        break;
                   
                    case ("ADDRESS"):
                        data = null;
                        data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .OrderBy(x => x.Address)
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
                        break;
                    case ("PHONENO"):
                        data = null;
                        data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .OrderBy(x => x.PhoneNo)
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .OrderByDescending(x => x.Name)
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
                        break;

                    case ("ADDRESS"):
                        data = null;
                        data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .OrderByDescending(x => x.Address)
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
                        break;
                    case ("PHONENO"):
                        data = null;
                        data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .OrderByDescending(x => x.PhoneNo)
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Court
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                              && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                              && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Court>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }
    }
    }

