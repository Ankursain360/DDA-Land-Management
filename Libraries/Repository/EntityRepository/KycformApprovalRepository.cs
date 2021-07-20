
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

    public class KycformApprovalRepository : GenericRepository<Kycform>, IKycformApprovalRepository
    {
        public KycformApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }

      
        //public async Task<List<Branch>> GetAllBranchList()
        //{
        //    List<Branch> List = await _dbContext.Branch.Where(x => x.IsActive == 1 && x.DepartmentId == 50).ToListAsync();
        //    return List;
        //}
       

        //public async Task<List<Kycform>> GetAllKycform()
        //{
        //    return await _dbContext.Kycform
        //                           .Include(x => x.Branch)
        //                           .Include(x => x.LeaseType)
        //                           .Include(x => x.Locality)
        //                           .Include(x => x.PropertyType)
        //                           .Include(x => x.Zone)
        //                           .Where(x => x.IsActive == 1)
        //                           .ToListAsync();
        //}

        //public async Task<PagedResult<Kycform>> GetPagedKycform(KycformSearchDto model)
        //{
        //    var data = await _dbContext.Kycform
        //                               .Include(x => x.Branch)
        //                               .Include(x => x.LeaseType)
        //                               .Include(x => x.Locality)
        //                               .Include(x => x.PropertyType)
        //                               .Include(x => x.Zone)
        //                               .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)))
        //                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);

        //    int SortOrder = (int)model.SortOrder;
        //    if (SortOrder == 1)
        //    {
        //        switch (model.SortBy.ToUpper())
        //        {
        //            case ("NAME"):
        //                data = null;
        //                data = await _dbContext.Kycform
        //                                       .Include(x => x.Branch)
        //                                       .Include(x => x.LeaseType)
        //                                       .Include(x => x.Locality)
        //                                       .Include(x => x.PropertyType)
        //                                       .Include(x => x.Zone)
        //                                       .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)))
        //                                       .OrderBy(x => x.Property)
        //                                       .GetPaged<Kycform>(model.PageNumber, model.PageSize);
        //                break;
        //            case ("STATUS"):
        //                data = null;
        //                data = await _dbContext.Kycform
        //                                       .Include(x => x.Branch)
        //                                       .Include(x => x.LeaseType)
        //                                       .Include(x => x.Locality)
        //                                       .Include(x => x.PropertyType)
        //                                       .Include(x => x.Zone)
        //                                       .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)))
        //                                       .OrderByDescending(x => x.IsActive)
        //                                       .GetPaged<Kycform>(model.PageNumber, model.PageSize);
        //                break;

        //        }
        //    }
        //    else if (SortOrder == 2)
        //    {
        //        switch (model.SortBy.ToUpper())
        //        {
        //            case ("NAME"):
        //                data = null;
        //                data = await _dbContext.Kycform
        //                                       .Include(x => x.Branch)
        //                                       .Include(x => x.LeaseType)
        //                                       .Include(x => x.Locality)
        //                                       .Include(x => x.PropertyType)
        //                                       .Include(x => x.Zone)
        //                                       .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)))
        //                                       .OrderByDescending(x => x.Property)
        //                                       .GetPaged<Kycform>(model.PageNumber, model.PageSize);
        //                break;
        //            case ("STATUS"):
        //                data = null;
        //                data = await _dbContext.Kycform
        //                                       .Include(x => x.Branch)
        //                                       .Include(x => x.LeaseType)
        //                                       .Include(x => x.Locality)
        //                                       .Include(x => x.PropertyType)
        //                                       .Include(x => x.Zone)
        //                                       .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)))
        //                                       .OrderBy(x => x.IsActive)
        //                                       .GetPaged<Kycform>(model.PageNumber, model.PageSize);
        //                break;
        //        }
        //    }
        //    return data;
        //    // return await _dbContext.Structure.GetPaged<Structure>(model.PageNumber, model.PageSize);
        //}

        
    }
}
