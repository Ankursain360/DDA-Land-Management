using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{

    public class CurrentstatusoflandhistoryService : EntityService<Currentstatusoflandhistory>, ICurrentstatusoflandhistoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentstatusoflandhistoryRepository _currentstatusoflandhistoryRepository;
        public CurrentstatusoflandhistoryService(IUnitOfWork unitOfWork, ICurrentstatusoflandhistoryRepository currentstatusoflandhistoryRepository)
        : base(unitOfWork, currentstatusoflandhistoryRepository)
        {
            _unitOfWork = unitOfWork;
            _currentstatusoflandhistoryRepository = currentstatusoflandhistoryRepository;
        }

        public async Task<bool> Create(Currentstatusoflandhistory model)
        {
            model.CreatedBy = 1;
            model.CreatedDate = DateTime.Now;
            _currentstatusoflandhistoryRepository.Add(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Currentstatusoflandhistory>> GetCurrentstatusoflandhistory(int id)
        {
            return await _currentstatusoflandhistoryRepository.GetCurrentstatusoflandhistory(id);
        }
       
        public async Task<Currentstatusoflandhistory> FetchSingleResult(int id)
        {
            return await _currentstatusoflandhistoryRepository.FetchSingleResult(id);
        }

        public async Task<PagedResult<Currentstatusoflandhistory>> GetPagedCurrentstatusoflandhistory(CurrentstatusoflandhistorySearchDto model)
        {
            return await _currentstatusoflandhistoryRepository.GetPagedCurrentstatusoflandhistory(model);
        }
    }

}
