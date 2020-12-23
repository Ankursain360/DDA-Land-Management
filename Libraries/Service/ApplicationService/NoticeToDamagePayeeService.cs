using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
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




        public async Task<PagedResult<Noticetodamagepayee>> GetPagedNoticetodamagepayee(NoticetodamagepayeeSearchDto model)
        {
            return await _noticeToDamagePayeeRepository.GetPagedNoticetodamagepayee(model);
        }



    }
}
