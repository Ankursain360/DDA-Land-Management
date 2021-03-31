

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

    public class LeasedeedService : EntityService<Leasedeed>, ILeasedeedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeasedeedRepository _leasedeedRepository;

        public LeasedeedService(IUnitOfWork unitOfWork, ILeasedeedRepository leasedeedRepository)
        : base(unitOfWork, leasedeedRepository)
        {
            _unitOfWork = unitOfWork;
            _leasedeedRepository = leasedeedRepository;
        }

        public async Task<List<Allotmententry>> GetAllApplications()
        {
            List<Allotmententry> list = await _leasedeedRepository.GetAllApplications();
            return list;
        }
        public async Task<Allotmententry> FetchSingleDetails(int? Id)
        {
            return await _leasedeedRepository.FetchSingleDetails(Id);
        }

        public async Task<List<Leasedeed>> GetAllLeasedeed()
        {
            return await _leasedeedRepository.GetAllLeasedeed();
        }
        public async Task<PagedResult<Leasedeed>> GetPagedLeasedeed(LeasedeedSearchDto model)
        {
            return await _leasedeedRepository.GetPagedLeasedeed(model);
        }

        public async Task<bool> Update(int id, Leasedeed deed)
        {
            var result = await _leasedeedRepository.FindBy(a => a.Id == id);
            Leasedeed model = result.FirstOrDefault();

            model.AllotmentId = deed.AllotmentId;
            model.LeaseDeedDate = deed.LeaseDeedDate;
            model.DocumentPath = deed.DocumentPath;
            model.Remarks = deed.Remarks;
            model.IsActive = deed.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = deed.ModifiedBy;
            _leasedeedRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Leasedeed deed)
        {
            deed.CreatedDate = DateTime.Now;
            _leasedeedRepository.Add(deed);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Leasedeed> FetchSingleResult(int id)
        {
            var result = await _leasedeedRepository.FindBy(a => a.Id == id);
            Leasedeed model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _leasedeedRepository.FindBy(a => a.Id == id);
            Leasedeed model = form.FirstOrDefault();
            model.IsActive = 0;
            _leasedeedRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
