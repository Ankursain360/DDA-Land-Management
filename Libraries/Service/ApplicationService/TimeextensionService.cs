
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

    public class TimeextensionService : EntityService<Timeextension>, ITimeextensionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeextensionRepository _timeextensionRepository;

        public TimeextensionService(IUnitOfWork unitOfWork, ITimeextensionRepository timeextensionRepository)
        : base(unitOfWork, timeextensionRepository)
        {
            _unitOfWork = unitOfWork;
            _timeextensionRepository = timeextensionRepository;
        }

        public async Task<List<Timeextension>> GetAllTimeextension()
        {
            return await _timeextensionRepository.GetAllTimeextension();
        }

       

        public async Task<Timeextension> FetchSingleResult(int id)
        {
            var result = await _timeextensionRepository.FindBy(a => a.Id == id);
            Timeextension model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Timeextension rate)
        {
            var result = await _timeextensionRepository.FindBy(a => a.Id == id);
            Timeextension model = result.FirstOrDefault();
          
           
            model.ExtensionFees = rate.ExtensionFees;
            model.FromDate = rate.FromDate;
            model.ToDate = rate.ToDate;
            model.IsActive = rate.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = rate.ModifiedBy;
            _timeextensionRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Timeextension rate)
        {
            rate.CreatedBy = rate.CreatedBy;
            rate.CreatedDate = DateTime.Now;
            _timeextensionRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _timeextensionRepository.FindBy(a => a.Id == id);
            Timeextension model = form.FirstOrDefault();
            model.IsActive = 0;
            _timeextensionRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Timeextension>> GetPagedTimeextension(TimeextensionSearchDto model)
        {
            return await _timeextensionRepository.GetPagedTimeextension(model);
        }


    }
}
