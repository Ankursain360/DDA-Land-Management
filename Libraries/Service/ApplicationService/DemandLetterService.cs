using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
  public  class DemandLetterService : EntityService<Demandletter>, IDemandLetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemandLetterRepository _demandLetterRepository;

        public DemandLetterService(IUnitOfWork unitOfWork, IDemandLetterRepository demandLetterRepository)
    : base(unitOfWork, demandLetterRepository)
        {
            _unitOfWork = unitOfWork;
            _demandLetterRepository = demandLetterRepository;
        }

        public async Task<List<Demandletter>> GetAllDemandletter()
        {
            List<Demandletter> DamageList = await _demandLetterRepository.GetAllDemandletter();
            return DamageList;
        }

        public async Task<PagedResult<Demandletter>> GetPagedDemandletter(DemandletterSearchDto model)
        {
            return await _demandLetterRepository.GetPagedDemandletter(model);
        }


        public async Task<bool> Create(Demandletter demandletter)
        {
            demandletter.CreatedBy = 1;
            demandletter.CreatedDate = DateTime.Now;
            demandletter.IsActive = 1;


            _demandLetterRepository.Add(demandletter);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Demandletter> FetchSingleResult(int id)
        {
            var result = await _demandLetterRepository.FindBy(a => a.Id == id);
            Demandletter model = result.FirstOrDefault();
            return model;
        }




        public async Task<bool> Update(int id, Demandletter demandletter)
        {
            var result = await _demandLetterRepository.FindBy(a => a.Id == id);
            Demandletter model = result.FirstOrDefault();
            model.FileNo = demandletter.FileNo;
          
            model.ModifiedBy = 1;
            _demandLetterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }






    }
}
