
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
    }
}
