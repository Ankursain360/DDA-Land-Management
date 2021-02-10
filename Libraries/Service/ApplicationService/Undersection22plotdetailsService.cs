using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class Undersection22plotdetailsService : EntityService<Undersection22plotdetails>, IUndersection22plotdetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUndersection22plotdetailsRepository _undersection22plotdetailsRepository;

        public Undersection22plotdetailsService(IUnitOfWork unitOfWork, IUndersection22plotdetailsRepository undersection22plotdetailsRepository)
        : base(unitOfWork, undersection22plotdetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _undersection22plotdetailsRepository = undersection22plotdetailsRepository;
        }

        public async Task<List<Undersection22plotdetails>> GetAllUS22PlotDetails()
        {
            return await _undersection22plotdetailsRepository.GetAllUS22PlotDetails();
        }
        public async Task<List<Acquiredlandvillage>> GetAllAcquiredlandvillage()
        {
            List<Acquiredlandvillage> acqvillageList = await _undersection22plotdetailsRepository.GetAllAcquiredlandvillage();
            return acqvillageList;
        }
        public async Task<List<Khasra>> GetAllKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _undersection22plotdetailsRepository.GetAllKhasra(villageId);
            return khasraList;
        }
        public async Task<List<Undersection4>> GetAllUndersection4()
        {
            List<Undersection4> us4List = await _undersection22plotdetailsRepository.GetAllUndersection4();
            return us4List;
        }
        public async Task<List<Undersection6>> GetAllUndersection6()
        {
            List<Undersection6> us6List = await _undersection22plotdetailsRepository.GetAllUndersection6();
            return us6List;
        }
        public async Task<List<Undersection17>> GetAllUndersection17()
        {
            List<Undersection17> us17List = await _undersection22plotdetailsRepository.GetAllUndersection17();
            return us17List;
        }
        public async Task<List<Undersection22>> GetAllUndersection22()
        {
            List<Undersection22> us22List = await _undersection22plotdetailsRepository.GetAllUndersection22();
            return us22List;
        }

        //public async Task<PagedResult<Undersection22plotdetails>> GetPagedUndersection22plotdetails(Undersection22plotdetailsSearchDto model);
        public async Task<bool> Create(Undersection22plotdetails us22plot)
        {
            us22plot.CreatedBy = us22plot.CreatedBy;
            us22plot.CreatedDate = DateTime.Now;
            _undersection22plotdetailsRepository.Add(us22plot);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
