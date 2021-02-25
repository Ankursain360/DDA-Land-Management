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
    public class NewlandAppealdetailService : EntityService<Newlandappealdetail>, INewlandAppealdetailservice
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandAppealdetailRepository _newlandAppealdetailRepository;
        protected readonly DataContext _dbContext;


        public NewlandAppealdetailService(IUnitOfWork unitOfWork, INewlandAppealdetailRepository newlandAppealdetailRepository, DataContext dbContext)
      : base(unitOfWork, newlandAppealdetailRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandAppealdetailRepository = newlandAppealdetailRepository;
            _dbContext = dbContext;
        }



        public async Task<List<Newlandappealdetail>> GetNewlandappealdetails()
        {
            return await _newlandAppealdetailRepository.GetAll();
        }

        public async Task<List<Newlandappealdetail>> GetNewLandAppealdetailUsingRepo()
        {
            return await _newlandAppealdetailRepository.GetNewlandappealdetails();
        }

        public async Task<bool> Update(int id, Newlandappealdetail newlandappealdetail)
        {
            var result = await _newlandAppealdetailRepository.FindBy(a => a.Id == id);
            Newlandappealdetail model = result.FirstOrDefault();
            model.DemandListNo = newlandappealdetail.DemandListNo;
            model.EnmSno = newlandappealdetail.EnmSno;
            model.AppealNo = newlandappealdetail.AppealNo;
            model.AppealByDept = newlandappealdetail.AppealByDept;
            model.DateOfAppeal = newlandappealdetail.DateOfAppeal;
            model.PanelLawer = newlandappealdetail.PanelLawer;

            model.ModifiedDate = DateTime.Now;
            model.IsActive = newlandappealdetail.IsActive;
            model.ModifiedBy = 1;
            _newlandAppealdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<Newlandappealdetail> FetchSingleResult(int id)
        {
            var result = await _newlandAppealdetailRepository.FindBy(a => a.Id == id);
            Newlandappealdetail model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Newlandappealdetail newlandappealdetail)
        {

            newlandappealdetail.CreatedBy = 1;
            newlandappealdetail.CreatedDate = DateTime.Now;
            _newlandAppealdetailRepository.Add(newlandappealdetail);
            return await _unitOfWork.CommitAsync() > 0;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _newlandAppealdetailRepository.FindBy(a => a.Id == id);
            Newlandappealdetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandAppealdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Newlandappealdetail>> GetPagedNewlandAppealdetail(NewlandAppealdetailSearchDto model)
        {
            return await _newlandAppealdetailRepository.GetPagedNewlandAppealdetails(model);
        }

    }
}