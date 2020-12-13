using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class DemolitionPoliceAssistenceLetterService : EntityService<Fixingdemolition>, IDemolitionPoliceAssistenceLetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemolitionPoliceAssistenceLetterRepository _demolitionPoliceAssistenceLetterRepository;
        public DemolitionPoliceAssistenceLetterService(IUnitOfWork unitOfWork, IDemolitionPoliceAssistenceLetterRepository demolitionPoliceAssistenceLetterRepository)
        : base(unitOfWork, demolitionPoliceAssistenceLetterRepository)
        {
            _unitOfWork = unitOfWork;
            _demolitionPoliceAssistenceLetterRepository = demolitionPoliceAssistenceLetterRepository;
        }

        public async Task<PagedResult<Fixingdemolition>> GetPagedApprovedAnnexureA(DemolitionPoliceAssistenceLetterSearchDto model, int userId)
        {
            return await _demolitionPoliceAssistenceLetterRepository.GetPagedApprovedAnnexureA(model, userId);
        }
    }
}
