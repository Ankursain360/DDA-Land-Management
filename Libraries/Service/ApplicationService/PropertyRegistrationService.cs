using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;

namespace Libraries.Service.ApplicationService
{

    public class PropertyRegistrationService : EntityService<Propertyregistration>, IPropertyRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyRegistrationRepository _propertyregistrationRepository;

        public PropertyRegistrationService(IUnitOfWork unitOfWork, IPropertyRegistrationService propertyregistrationRepository)
        : base(unitOfWork, propertyregistrationRepository)
        {
            _unitOfWork = unitOfWork;
            _propertyregistrationRepository = (IPropertyRegistrationRepository)propertyregistrationRepository;

        }

      
    }
}
