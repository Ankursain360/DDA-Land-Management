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

namespace Libraries.Repository.EntityRepository
{
    public class DemandDetailsRepository :  GenericRepository<Kycform>, IDemandDetailsRepository
    {
        public DemandDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
     

        public async Task<PagedResult<Kycform>> GetPagedDemandDetails(DemandDetailsSearchDto model,string MobileNo)
        {
            var data = await _dbContext.Kycform
                                       .Include(x => x.Branch)
                                       .Include(x => x.LeaseType)
                                       .Include(x => x.Locality)
                                       .Include(x => x.PropertyType)
                                       .Include(x => x.Zone)
                                       .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) &&           x.MobileNo == MobileNo && x.KycStatus == "T" && x.ApprovedStatus ==3)
                                       .GetPaged<Kycform>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Kycform
                                               .Include(x => x.Branch)
                                               .Include(x => x.LeaseType)
                                               .Include(x => x.Locality)
                                               .Include(x => x.PropertyType)
                                               .Include(x => x.Zone)
                                               .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo && x.KycStatus == "T" && x.ApprovedStatus == 3)
                                               .OrderBy(x => x.Property)
                                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Kycform
                                               .Include(x => x.Branch)
                                               .Include(x => x.LeaseType)
                                               .Include(x => x.Locality)
                                               .Include(x => x.PropertyType)
                                               .Include(x => x.Zone)
                                               .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo && x.KycStatus == "T" && x.ApprovedStatus == 3)
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Kycform
                                               .Include(x => x.Branch)
                                               .Include(x => x.LeaseType)
                                               .Include(x => x.Locality)
                                               .Include(x => x.PropertyType)
                                               .Include(x => x.Zone)
                                             .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo && x.KycStatus == "T" && x.ApprovedStatus == 3)
                                               .OrderByDescending(x => x.Property)
                                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Kycform
                                               .Include(x => x.Branch)
                                               .Include(x => x.LeaseType)
                                               .Include(x => x.Locality)
                                               .Include(x => x.PropertyType)
                                               .Include(x => x.Zone)
                                              .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo && x.KycStatus == "T" && x.ApprovedStatus == 3)
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
         
        }

        public async Task<List<DemandPaymentDetailsDto>> GetPaymentDetails(int FileNo)
        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("GetPaymentDetails")
                                            .WithSqlParams(("P_FileNo", FileNo))
                                            .ExecuteStoredProcedureAsync<DemandPaymentDetailsDto>();

                return (List<DemandPaymentDetailsDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



      

    }
}
