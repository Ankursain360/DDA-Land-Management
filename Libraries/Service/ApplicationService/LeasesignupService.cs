using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class LeasesignupService : EntityService<Leasesignup>, ILeasesignupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeasesignupRepository _leasesignupRepository;
        public LeasesignupService(IUnitOfWork unitOfWork, ILeasesignupRepository leasesignupRepository)
        : base(unitOfWork, leasesignupRepository)
        {
            _unitOfWork = unitOfWork;
            _leasesignupRepository = leasesignupRepository;
        }



        public async Task<bool> Create(Leasesignup leasesignup)
        {

            leasesignup.CreatedBy = 1;
            leasesignup.CreatedDate = DateTime.Now;
            _leasesignupRepository.Add(leasesignup);
            return await _unitOfWork.CommitAsync() > 0;
        }


    }
}
