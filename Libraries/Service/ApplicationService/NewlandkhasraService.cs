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
using Service.IApplicationService;

namespace Libraries.Service.ApplicationService
{
        public class NewlandkhasraService : EntityService<Newlandkhasra>, INewlandkhasraService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandkhasraRepository _NewlandkhasraRepository;
        public NewlandkhasraService(IUnitOfWork unitOfWork, INewlandkhasraRepository NewlandkhasraRepository)
      : base(unitOfWork, NewlandkhasraRepository)
        {
            _unitOfWork = unitOfWork;
            _NewlandkhasraRepository = NewlandkhasraRepository;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _NewlandkhasraRepository.FindBy(a => a.Id == id);
            Newlandkhasra model = form.FirstOrDefault();
            model.IsActive = 0;
            _NewlandkhasraRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Newlandkhasra> FetchSingleResult(int id)
        {
            var result = await _NewlandkhasraRepository.FindBy(a => a.Id == id);
            Newlandkhasra model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Newlandkhasra>> GetAllKhasra()
        {

            return await _NewlandkhasraRepository.GetAllKhasra();
        }

        public async Task<List<LandCategory>> GetAllLandCategory()
        {
            List<LandCategory> landcategoryList = await _NewlandkhasraRepository.GetAllLandCategory();
            return landcategoryList;
        }

        public async Task<List<Newlandvillage>> GetAllVillageList()
        {
            List<Newlandvillage> villageList = await _NewlandkhasraRepository.GetAllVillageList();
            return villageList;
        }

        public async Task<List<Newlandkhasra>> GetKhasraUsingRepo()
        {
            return await _NewlandkhasraRepository.GetAllKhasra();
        }
        public async Task<PagedResult<Newlandkhasra>> GetPagedKhasra(NewlandkhasraSearchDto model)
        {
            return await _NewlandkhasraRepository.GetPagedKhasra(model);
        }

        public async Task<bool> Update(int id, Newlandkhasra khasra)
        {
            var result = await _NewlandkhasraRepository.FindBy(a => a.Id == id);
            Newlandkhasra model = result.FirstOrDefault();
            model.NewLandvillageId = khasra.NewLandvillageId;
            model.Name = khasra.Name;
            model.LandCategoryId = khasra.LandCategoryId;
            model.RectNo = khasra.RectNo;
            model.Bigha = khasra.Bigha;
            model.Biswa = khasra.Biswa;
            model.Biswanshi = khasra.Biswanshi;

            model.Description = khasra.Description;

            model.IsActive = khasra.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _NewlandkhasraRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Newlandkhasra khasra)
        {
            khasra.CreatedBy = 1;
            khasra.CreatedDate = DateTime.Now;

            _NewlandkhasraRepository.Add(khasra);
            return await _unitOfWork.CommitAsync() > 0;
        }
       
        public async Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _NewlandkhasraRepository.GetAllKhasraList(villageId);
            return khasraList;
        }

        public async Task<PagedResult<Newlandkhasra>> GetPagednewlandVillageKhasraReport(NewlandVillageDetailsKhasraWiseReportSearchDto model)
        {
            return await _NewlandkhasraRepository.GetPagednewlandVillageKhasraReport(model);

        }


    }
}

