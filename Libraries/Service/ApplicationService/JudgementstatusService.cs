using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class JudgementstatusService : EntityService<Judgementstatus>, IJudgementstatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJudgementstatusRepository _JudgementstatusRepository;

        public JudgementstatusService(IUnitOfWork unitOfWork, IJudgementstatusRepository JudgementstatusRepository) : base(unitOfWork, JudgementstatusRepository)
        {
            _unitOfWork = unitOfWork;
            _JudgementstatusRepository = JudgementstatusRepository;
        }

        public async Task<List<Judgementstatus>> GetAllJudgementstatus()
        {
            return await _JudgementstatusRepository.GetAllJudgementstatus();
        }


        public async Task<Judgementstatus> FetchSingleResult(int id)
        {
            var result = await _JudgementstatusRepository.FindBy(a => a.Id == id);
            Judgementstatus model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Judgementstatus rent)
        {
            var result = await _JudgementstatusRepository.FindBy(a => a.Id == id);
            Judgementstatus model = result.FirstOrDefault();
            model.Status = rent.Status;


            model.IsActive = rent.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = rent.ModifiedBy;
            _JudgementstatusRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Judgementstatus rate)
        {
            rate.CreatedBy = rate.CreatedBy;

            rate.CreatedDate = DateTime.Now;
            _JudgementstatusRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _JudgementstatusRepository.FindBy(a => a.Id == id);
            Judgementstatus model = form.FirstOrDefault();
            model.IsActive = 0;
            _JudgementstatusRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Judgementstatus>> GetPagedJudgementstatus(JudgementstatusSearchDto model)
        {
            return await _JudgementstatusRepository.GetPagedJudgementstatus(model);
        }
    }
}
