using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
using Dto.Master;
using AutoMapper;

namespace Libraries.Service.ApplicationService
{

    public class ProceedingEvictionLetterService : EntityService<Requestforproceeding>, IProceedingEvictionLetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProceedingEvictionLetterRepository _proceedingEvictionLetterRepository;
        private readonly IMapper _mapper;
        public ProceedingEvictionLetterService(IUnitOfWork unitOfWork,
            IProceedingEvictionLetterRepository proceedingEvictionLetterRepository,
            IMapper mapper)
        : base(unitOfWork, proceedingEvictionLetterRepository)
        {
            _unitOfWork = unitOfWork;
            _proceedingEvictionLetterRepository = proceedingEvictionLetterRepository;
            _mapper = mapper;
        }

        public async Task<ProceedingEvictionLetterViewLetterDataDto> BindProceedingConvictionLetterData(int id)
        {
            return await _proceedingEvictionLetterRepository.BindProceedingConvictionLetterData(id);
        }

        public async Task<List<RefNoNameDto>> BindRefNoNameList()
        {
            return await _proceedingEvictionLetterRepository.BindRefNoNameList();
        }

        public async Task<Requestforproceeding> FetchProceedingConvictionLetterData(int id)
        {
            return await _proceedingEvictionLetterRepository.FetchProceedingConvictionLetterData(id);
        }


        public async Task<string> GetLetterRefNo(int id)
        {
            return await _proceedingEvictionLetterRepository.GetLetterRefNo(id);
        }

        public async Task<bool> UpdateRequestProceeding(ProceedingEvictionLetterSearchDto data, int UserId)
        {
            var result = await _proceedingEvictionLetterRepository.FindBy(a => a.Id == data.RefNoNameId);
            Requestforproceeding model = result.FirstOrDefault();
            model.IsGenerate = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = UserId;
            _proceedingEvictionLetterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateRequestProceedingIsSend(Requestforproceeding data, int UserId)
        {
            var result = await _proceedingEvictionLetterRepository.FindBy(a => a.Id == data.Id);
            Requestforproceeding model = result.FirstOrDefault();
            model.IsSend = 1;
            model.PendingAt = data.UserId;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = UserId;
            _proceedingEvictionLetterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateRequestProceedingUpload(int id, Requestforproceeding requestforproceeding)
        {
            var result = await _proceedingEvictionLetterRepository.FindBy(a => a.Id == requestforproceeding.Id);
            Requestforproceeding model = result.FirstOrDefault();
            model.ProcedingLetter = requestforproceeding.ProcedingLetter;
            model.IsUpload = 1;
            model.ModifiedDate = DateTime.Now;
            _proceedingEvictionLetterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
