using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
  public  interface ILeasesignupRepository : IGenericRepository<Leasesignup>
    {
        //Task<List<Leasesignup>> GetAllLeasesignup();
        Task<PagedResult<Kycform>> AllKycformList(Leasesignuplist model);
        Task<bool> ValidateMobileEmail(string mobile, string email);
        Task<List<Kycform>> GetAllKycformList(string Mobileno);
        Task<List<Leasesignup>> GetEmailAndMobile(string mobile, string email);
    }
}
