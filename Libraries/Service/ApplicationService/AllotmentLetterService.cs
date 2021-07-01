
using Dto.Master;
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
    public class AllotmentLetterService : EntityService<Allotmentletter>, IAllotmentLetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAllotmentLetterRepository _allotmentLetterRepository;

        public AllotmentLetterService(IUnitOfWork unitOfWork, IAllotmentLetterRepository allotmentLetterRepository)
        : base(unitOfWork, allotmentLetterRepository)
        {
            _unitOfWork = unitOfWork;
            _allotmentLetterRepository = allotmentLetterRepository;
        }
        public async Task<Allotmentletter> FetchSingleAllotmentLetterDetails(int id)
        {
            return await _allotmentLetterRepository.FetchSingleAllotmentLetterDetails(id);
        }public async Task<Allotmentletter> FetchAllotmentLetterDetails(int id)
        {
            return await _allotmentLetterRepository.FetchAllotmentLetterDetails(id);
        }
        public async Task<List<Allotmententry>> GetRefNoListforAllotmentLetter()
        {
            return await _allotmentLetterRepository.GetRefNoListforAllotmentLetter();
        }
        public async Task<bool> Create(Allotmentletter allotmentletter)
        {

            allotmentletter.CreatedDate = DateTime.Now;
            _allotmentLetterRepository.Add(allotmentletter);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<PagedResult<Allotmentletter>> GetPagedAllotmentLetter(AllotmentLetterSeearchDto model)
        {
            return await _allotmentLetterRepository.GetPagedAllotmentLetter(model);
        }
        public async Task<bool> Update(int id, Allotmentletter allotmentletter)
        {
            var result = await _allotmentLetterRepository.FindBy(a => a.Id == id);
            Allotmentletter model = result.FirstOrDefault();
            model.FilePath = allotmentletter.FilePath;
            model.AllotmentId = allotmentletter.AllotmentId;
            model.ReferenceNumber = allotmentletter.ReferenceNumber;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = allotmentletter.ModifiedBy;
            _allotmentLetterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }
        public string GetDownload(int id)
        {
            return _allotmentLetterRepository.GetDownload(id);
        }


        public async Task<List<Allotmentletter>> GetAllotmentLetterData()
        {
            return await _allotmentLetterRepository.GetAllotmentLetterData();
        }


    }
}
