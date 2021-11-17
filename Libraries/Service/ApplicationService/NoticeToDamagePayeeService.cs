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
    public class NoticeToDamagePayeeService : EntityService<Noticetodamagepayee>, INoticeToDamagePayeeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly INoticeToDamagePayeeRepository _noticeToDamagePayeeRepository;


        public NoticeToDamagePayeeService(IUnitOfWork unitOfWork, INoticeToDamagePayeeRepository noticeToDamagePayeeRepository)
        : base(unitOfWork, noticeToDamagePayeeRepository)
        {
            _unitOfWork = unitOfWork;
            _noticeToDamagePayeeRepository = noticeToDamagePayeeRepository;
        }


        public async Task<List<Noticetodamagepayee>> GetAllNoticetoDamagePayee()
        {
            List<Noticetodamagepayee> DamageList = await _noticeToDamagePayeeRepository.GetAllNoticetoDamagePayee();
            return DamageList;
        }

        public async Task<List<Noticetodamagepayee>> GetsingleData(int id)
        {

            var result = await _noticeToDamagePayeeRepository.FindBy(a => a.Id == id);
            return result;
        }

        public async Task<bool> Create(Noticetodamagepayee noticetodamagepayee)
        {
            noticetodamagepayee.CreatedBy = 1;
            noticetodamagepayee.CreatedDate = DateTime.Now;
            noticetodamagepayee.IsActive = 1;


            _noticeToDamagePayeeRepository.Add(noticetodamagepayee);
            return await _unitOfWork.CommitAsync() > 0;
        }


       
        public async Task<PagedResult<Noticetodamagepayee>> GetPagedNoticeGenerationReport(NoticeGenerationReportSearchDto model)
        {
            return await _noticeToDamagePayeeRepository.GetPagedNoticeGenerationReport(model);
        }
        public async Task<PagedResult<Noticetodamagepayee>> GetPagedNoticetodamagepayee(NoticetodamagepayeeSearchDto model)
        {
            return await _noticeToDamagePayeeRepository.GetPagedNoticetodamagepayee(model);
        }


        public async Task<List<Noticetodamagepayee>> GetFileNoList()
        {
            List<Noticetodamagepayee> fileNoList = await _noticeToDamagePayeeRepository.GetFileNoList();
            return fileNoList;
        }
        public async Task<Noticetodamagepayee> FetchSingleResult(int id)
        {
            var result = await _noticeToDamagePayeeRepository.FindBy(a => a.Id == id);
            Noticetodamagepayee model = result.FirstOrDefault();
            return model;
        }


     
        public Decimal GetRebateCharges()
        {
            return _noticeToDamagePayeeRepository.GetRebateCharges();
        }


        public async Task<bool> Update(int id, Noticetodamagepayee noticetodamagepayee)
        {
            var result = await _noticeToDamagePayeeRepository.FindBy(a => a.Id == id);
            Noticetodamagepayee model = result.FirstOrDefault();
            model.FileNo = noticetodamagepayee.FileNo;
            model.GenerateDate = DateTime.Now;
            model.Name = noticetodamagepayee.Name;
            model.Address = noticetodamagepayee.Address;
            model.PropertyDetails = noticetodamagepayee.PropertyDetails;
            model.Area = noticetodamagepayee.Area;
            model.InterestPercentage = noticetodamagepayee.InterestPercentage;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _noticeToDamagePayeeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



    }
}
