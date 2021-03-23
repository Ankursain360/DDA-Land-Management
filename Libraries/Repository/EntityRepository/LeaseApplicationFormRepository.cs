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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class LeaseApplicationFormRepository : GenericRepository<Leaseapplication>, ILeaseApplicationFormRepository
    {

        public LeaseApplicationFormRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Leaseapplication> FetchLeaseApplicationDetails(int id)
        {
            var data = await _dbContext.Leaseapplication
                                        .Include(x => x.Leaseapplicationdocuments)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid)
        {
            return await _dbContext.Documentchecklist
                                        .Include(x => x.ServiceType)
                                        .Where(x => x.ServiceTypeId == servicetypeid)
                                        .OrderByDescending(x => x.IsMandatory)
                                        .ToListAsync();
        }

        public async Task<List<Leaseapplicationdocuments>> LeaseApplicationDocumentDetails(int id)
        {
            return await _dbContext.Leaseapplicationdocuments
                                       .Include(x => x.DocumentChecklist)
                                       .Where(x => x.LeaseApplicationId == id)
                                       .ToListAsync();
        }

        public async Task<bool> SaveLeaseApplicationDocuments(List<Leaseapplicationdocuments> leaseapplicationdocuments)
        {
            await _dbContext.Leaseapplicationdocuments.AddRangeAsync(leaseapplicationdocuments);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<PagedResult<Leaseapplication>> GetPagedAllotmentLetter(DocumentChecklistSearchDto model)
        {
            var data = await _dbContext.Leaseapplication
                                                  .Where(x => x.IsActive==1)
                                        .GetPaged<Leaseapplication>(model.PageNumber, model.PageSize);
            return data;
        }

        public async Task<Leaseapplicationdocuments> FetchLeaseApplicationDocumentDetails(int id)
        {
            return await _dbContext.Leaseapplicationdocuments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Leaseapplication>> GetRefNoListforAllotmentLetter()
        {
            return await _dbContext.Leaseapplication
                .Where(x=> x.IsActive == 1)
                .OrderByDescending(x => x.RefNo).ToListAsync();
        }
    }


}
