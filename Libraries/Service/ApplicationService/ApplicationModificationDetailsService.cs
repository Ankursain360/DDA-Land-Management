using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class ApplicationModificationDetailsService : EntityService<ApplicationModificationDetails>, IApplicationModificationDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationModificationDetailsRepository _modificationDetailsRepository; 

        public ApplicationModificationDetailsService(IUnitOfWork unitOfWork, IApplicationModificationDetailsRepository modificationDetailsRepository) : base(unitOfWork, modificationDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _modificationDetailsRepository = modificationDetailsRepository;
        }

        public DateTime? GetApplicationModificationDetails()
        {
            return _modificationDetailsRepository.GetApplicationModificationDetails();
        }

       
    }
}
