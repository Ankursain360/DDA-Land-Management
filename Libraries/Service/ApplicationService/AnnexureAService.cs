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
  public  class AnnexureAService : EntityService<Demolitionchecklist>, IAnnexureAService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnnexureARepository _annexureARepository;
        public AnnexureAService(IUnitOfWork unitOfWork, IAnnexureARepository annexureARepository)
      : base(unitOfWork, annexureARepository)
        {
            _unitOfWork = unitOfWork;
            _annexureARepository = annexureARepository;
        }


        public async Task<List<Demolitionchecklist>> GetDemolitionchecklist()
        {
            return await _annexureARepository.GetDemolitionchecklist();
        }


    }
}
