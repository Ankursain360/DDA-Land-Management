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



namespace Libraries.Service.ApplicationService
{
    public class CourtService : EntityService<Court>, ICourtService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtRepository _courtRepository;

        public CourtService(IUnitOfWork unitOfWork, ICourtRepository courtRepository)
    : base(unitOfWork, courtRepository)
        {
            _unitOfWork = unitOfWork;
            _courtRepository = courtRepository;
        }






        public async Task<List<Court>> GetAllCourt()
        {
            List<Court> DamageList = await _courtRepository.GetAllCourt();
            return DamageList;
        }

        public async Task<bool> Create(Court court)
        {
            court.CreatedBy = 1;
            court.CreatedDate = DateTime.Now;


            _courtRepository.Add(court);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Court>> GetPagedCourt(CourtSearchDto model)
        {
            return await _courtRepository.GetPagedCourt(model);
        }


        public async Task<Court> FetchSingleResult(int id)
        {
            var result = await _courtRepository.FindBy(a => a.Id == id);
            Court model = result.FirstOrDefault();
            return model;
        }


        public async Task<bool> Update(int id, Court court)
        {
            var result = await _courtRepository.FindBy(a => a.Id == id);
            Court model = result.FirstOrDefault();

            model.Name = court.Name;
            model.Address = court.Address;
            model.PhoneNo = court.PhoneNo;
            model.IsActive = court.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _courtRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _courtRepository.FindBy(a => a.Id == id);
            Court model = form.FirstOrDefault();
            model.IsActive = 0;
            _courtRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



    }
}
