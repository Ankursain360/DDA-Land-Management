
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class JudgementService : EntityService<Judgement>, IJudgementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJudgementRepository _judgementRepository;

        public JudgementService(IUnitOfWork unitOfWork, IJudgementRepository judgementRepository)
        : base(unitOfWork, judgementRepository)
        {
            _unitOfWork = unitOfWork;
            _judgementRepository = judgementRepository;
        }
        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model)
        {
            return await _judgementRepository.GetPagedRequestForProceeding(model);
        }

    }
}
