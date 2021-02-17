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
    public class AppealdetailService : EntityService<Appealdetail>, IAppealdetailService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppealdetailRepository _appealdetailRepository;
        protected readonly DataContext _dbContext;


        public AppealdetailService(IUnitOfWork unitOfWork, IAppealdetailRepository appealdetailRepository, DataContext dbContext)
       : base(unitOfWork, appealdetailRepository)
        {
            _unitOfWork = unitOfWork;
            _appealdetailRepository = appealdetailRepository;
            _dbContext = dbContext;
        }



        public async Task<List<Appealdetail>> GetAllAppealdetail()
        {
            return await _appealdetailRepository.GetAll();
        }

        public async Task<List<Appealdetail>> GetAppealdetailUsingRepo()
        {
            return await _appealdetailRepository.GetAppealdetail();
        }

        public async Task<bool> Update(int id, Appealdetail appealdetail)
        {
            var result = await _appealdetailRepository.FindBy(a => a.Id == id);
            Appealdetail model = result.FirstOrDefault();
            model.DemandListNo = appealdetail.DemandListNo;
            model.EnmSno = appealdetail.EnmSno;
            model.AppealNo = appealdetail.AppealNo;
            model.AppealByDept = appealdetail.AppealByDept;
            model.DateOfAppeal = appealdetail.DateOfAppeal;
            model.PanelLawer = appealdetail.PanelLawer;

            model.ModifiedDate = DateTime.Now;
            model.IsActive = appealdetail.IsActive;
            model.ModifiedBy = 1;
            _appealdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

       

        public async Task<Appealdetail> FetchSingleResult(int id)
        {
            var result = await _appealdetailRepository.FindBy(a => a.Id == id);
            Appealdetail model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Appealdetail appealdetail)
        {

            appealdetail.CreatedBy = 1;
            appealdetail.CreatedDate = DateTime.Now;
            _appealdetailRepository.Add(appealdetail);
            return await _unitOfWork.CommitAsync() > 0;
        }

      


        public async Task<bool> Delete(int id)
        {
            var form = await _appealdetailRepository.FindBy(a => a.Id == id);
            Appealdetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _appealdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Appealdetail>> GetPagedAppealdetail(AppealdetailSearchDto model)
        {
            return await _appealdetailRepository.GetPagedAppealdetail(model);
        }

    }
}

