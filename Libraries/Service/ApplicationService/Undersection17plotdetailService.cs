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
namespace Libraries.Service.ApplicationService
{
    public class Undersection17plotdetailService : EntityService<Undersection17plotdetail>, IUndersection17plotdetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUndersection17plotdetailRepository _undersection17plotdetailRepository;
        public Undersection17plotdetailService(IUnitOfWork unitOfWork, IUndersection17plotdetailRepository undersection17plotdetailRepository)
      : base(unitOfWork, undersection17plotdetailRepository)
        {
            _unitOfWork = unitOfWork;
            _undersection17plotdetailRepository = undersection17plotdetailRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _undersection17plotdetailRepository.FindBy(a => a.Id == id);
            Undersection17plotdetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _undersection17plotdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Undersection17plotdetail> FetchSingleResult(int id)
        {
            var result = await _undersection17plotdetailRepository.FindBy(a => a.Id == id);
            Undersection17plotdetail model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetail()
        {

            return await _undersection17plotdetailRepository.GetAllUndersection17plotdetail();
        }

       
        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _undersection17plotdetailRepository.GetAllVillageList();
            return villageList;
        }
        public async Task<List<Khasra>> GetAllKhasraList()
        {
            List<Khasra> khasraList = await _undersection17plotdetailRepository.GetAllKhasraList();
            return khasraList;
        }
        
             public async Task<List<Undersection17>> GetAllUndersection17List()

        {
            List<Undersection17> undersection17List = await _undersection17plotdetailRepository.GetAllUndersection17List();
            return undersection17List;
        }

        public async Task<List<Undersection17plotdetail>> GetUndersection17plotdetailUsingRepo()
        {
            return await _undersection17plotdetailRepository.GetAllUndersection17plotdetail();
        }
        public async Task<PagedResult<Undersection17plotdetail>> GetPagedUndersection17plotdetail(Undersection17plotdetailSearchDto model)
        {
            return await _undersection17plotdetailRepository.GetPagedUndersection17plotdetail(model);
        }

        public async Task<bool> Update(int id, Undersection17plotdetail undersection17plotdetail)
        {
            var result = await _undersection17plotdetailRepository.FindBy(a => a.Id == id);
            Undersection17plotdetail model = result.FirstOrDefault();
            model.VillageId = undersection17plotdetail.VillageId;
           
            model.KhasraId = undersection17plotdetail.KhasraId;
            model.UnderSection17Id = undersection17plotdetail.UnderSection17Id;
            model.Bigha = undersection17plotdetail.Bigha;
            model.Biswa = undersection17plotdetail.Biswa;
            model.Biswanshi = undersection17plotdetail.Biswanshi;

            model.Remarks = undersection17plotdetail.Remarks;

            model.IsActive = undersection17plotdetail.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _undersection17plotdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Undersection17plotdetail Undersection17plotdetail)
        {
            Undersection17plotdetail.CreatedBy = 1;
            Undersection17plotdetail.CreatedDate = DateTime.Now;

            _undersection17plotdetailRepository.Add(Undersection17plotdetail);
            return await _unitOfWork.CommitAsync() > 0;
        }


    }
}

