using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class FeedbackService : EntityService<tblfeedback>, IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedbackRepository _repository;

        public FeedbackService(IUnitOfWork unitOfWork, IFeedbackRepository repository) : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public Task<bool> Any(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedResult<tblfeedback>> GetPagedResult(FeedbackSearchDto model)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<tblfeedback>> GetTblfeedbacks()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Create(tblfeedback tblfeedback)
        {
            tblfeedback.CreatedBy = 1;
            tblfeedback.CreatedDate = DateTime.Now;
            _repository.Add(tblfeedback);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
