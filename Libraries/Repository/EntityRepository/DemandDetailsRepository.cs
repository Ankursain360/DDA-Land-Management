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
    public class DemandDetailsRepository :  GenericRepository<Kycdemandpaymentdetails>, IDemandDetailsRepository
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
                                       .Include(x => x.ApprovedStatusNavigation)
                                       .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                            &&  x.MobileNo == MobileNo  && x.ApprovedStatus ==3) //&& x.KycStatus == "T"
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
                                               .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                               && x.MobileNo == MobileNo  && x.ApprovedStatus == 3) //&& x.KycStatus == "T"
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
                                               .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                        && x.MobileNo == MobileNo && x.ApprovedStatus == 3) //&& x.KycStatus == "T" 
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
                                             .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) 
                                                    && x.MobileNo == MobileNo  && x.ApprovedStatus == 3) //&& x.KycStatus == "T"
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
                                              .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) 
                                                        && x.MobileNo == MobileNo &&  x.ApprovedStatus == 3) //x.KycStatus == "T" &&
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
         
        }



        public async Task<PagedResult<Kycform>> GetDemandPaymentDetails(DemandDetailsSearchDto model, string MobileNo)
        {
            var data = await _dbContext.Kycform
                                       .Include(x => x.Branch)
                                       .Include(x => x.LeaseType)
                                       .Include(x => x.Locality)
                                       .Include(x => x.PropertyType)
                                       .Include(x => x.Zone)
                                       .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo  && x.ApprovedStatus == 3)
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
                                               .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo  && x.ApprovedStatus == 3)
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
                                               .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo && x.ApprovedStatus == 3)
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
                                             .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo && x.ApprovedStatus == 3)
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
                                              .Where(x => (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property)) && x.MobileNo == MobileNo  && x.ApprovedStatus == 3)
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }


        public async Task<List<DemandPaymentDetailsDto>> GetPaymentDetails(int Id)
        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("GetPaymentDetails")
                                            .WithSqlParams(("P_Id", Id))
                                            .ExecuteStoredProcedureAsync<DemandPaymentDetailsDto>();

                return (List<DemandPaymentDetailsDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<List<LeasePaymentDemandLetterDetailsSearchDto>> GetPaymentDemandLetter(int Id)
        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("GetPaymentDemandLetterDetails")
                                            .WithSqlParams(("P_Id", Id))
                                            .ExecuteStoredProcedureAsync<LeasePaymentDemandLetterDetailsSearchDto>();

                return (List<LeasePaymentDemandLetterDetailsSearchDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
