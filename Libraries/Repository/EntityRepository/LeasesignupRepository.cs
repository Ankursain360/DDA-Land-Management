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


using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class LeasesignupRepository : GenericRepository<Leasesignup>, ILeasesignupRepository
    {
        public LeasesignupRepository(DataContext dbContext) : base(dbContext)
        {

        }

        //public async Task<Leasesignup> Sendotpf(int? khasraId)
        //{
        //    return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        //}
        public async Task<List<Kycform>> GetAllKycformList(string Mobileno)
        {
            try {
            var data =  await _dbContext.Kycform
                                   .Include(x => x.Branch)
                                   .Include(x => x.LeaseType)
                                   .Include(x => x.Locality)
                                   .Include(x => x.PropertyType)
                                   .Include(x => x.Zone)
                                   .Where(x => x.IsActive == 1 && x.MobileNo==Mobileno)
                                   .ToListAsync();
            return data;
            }
            catch(Exception ex)
            {
                 throw;
            
            }
        }

        public async Task<PagedResult<Kycform>> AllKycformList(Leasesignuplist model)
        {
            try
            {
                var data = await _dbContext.Kycform
                                       .Include(x => x.Branch)
                                       .Include(x => x.LeaseType)
                                       .Include(x => x.Locality)
                                       .Include(x => x.PropertyType)
                                       .Include(x => x.Zone)
                                       .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                       && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))

                                       )
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
                                                   .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                       && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))

                                       ).OrderBy(x => x.Property)
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
                                                 .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                       && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))

                                       ).OrderByDescending(x => x.IsActive)
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
                                                   .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                       && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))

                                       ).OrderByDescending(x => x.Property)
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
                                                 .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                       && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))

                                       ).OrderBy(x => x.IsActive)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;
                    }
                }
                return data;




              
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public async Task<bool> ValidateMobileEmail(string mobile, string email)
        {
            return await _dbContext.Leasesignup.AnyAsync(t => t.MobileNo == mobile || t.EmailId.ToLower() == email.ToLower());
        }






    }
}
