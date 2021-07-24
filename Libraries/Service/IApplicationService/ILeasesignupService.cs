using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
   public interface ILeasesignupService : IEntityService<Leasesignup>
    {
        Task<bool> Create(Leasesignup leasesignup);
        Task<bool> ValidateMobileEmail(string mobile, string email);
        Task<List<Kycform>> GetAllKycformList(string Mobileno);
        Task<PagedResult<Kycform>> AllKycformList(Leasesignuplist model);
    }
}
