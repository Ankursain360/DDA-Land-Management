using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
using Dto.Master;
using AutoMapper;

namespace Libraries.Service.ApplicationService
{

    public class PermissionsService : EntityService<Menuactionrolemap>, IPermissionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IMapper _mapper;
        public PermissionsService(IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IMapper mapper)
        : base(unitOfWork, permissionsRepository)
        {
            _unitOfWork = unitOfWork;
            _permissionsRepository = permissionsRepository;
            _mapper = mapper;
        }
       
        public async Task<List<Department>> GetDropDownList()
        {
            List<Department> departmentList = await _permissionsRepository.GetDepartmentList();
            return departmentList;
        }
       
    }
}
