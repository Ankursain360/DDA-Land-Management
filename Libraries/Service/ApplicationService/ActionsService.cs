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
using Dto.Search;

namespace Libraries.Service.ApplicationService
{

    public class ActionsService : EntityService<Actions>, IActionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActionsRepository _actionsRepository;

        public ActionsService(IUnitOfWork unitOfWork, IActionsRepository actionsRepository)
        : base(unitOfWork, actionsRepository)
        {
            _unitOfWork = unitOfWork;
            _actionsRepository = actionsRepository;
        }

        public async Task<List<Actions>> GetAllActions()
        {
            return await _actionsRepository.GetAll();
        }

        public async Task<PagedResult<Actions>> GetPagedActions(ActionsSearchDto model)
        {
            return await _actionsRepository.GetPagedActions(model);
        }

        public async Task<Actions> FetchSingleResult(int id)
        {
            var result = await _actionsRepository.FindBy(a => a.Id == id);
            Actions model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Actions actions)
        {
            var result = await _actionsRepository.FindBy(a => a.Id == id);
            Actions model = result.FirstOrDefault();
            model.Name = actions.Name;
            model.Icon = actions.Icon;
            model.Color = actions.Color;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = actions.IsActive;
            model.ModifiedBy = 1;
            _actionsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Actions actions)
        {

            actions.CreatedBy = 1;
            actions.CreatedDate = DateTime.Now;
            _actionsRepository.Add(actions);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string actions)
        {
            bool result = await _actionsRepository.Any(id, actions);
            //  var result1 = _dbContext.Actions.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _actionsRepository.FindBy(a => a.Id == id);
            Actions model = form.FirstOrDefault();
            model.IsActive = 0;
            _actionsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
