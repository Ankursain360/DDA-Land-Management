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
    public class KhasraService : EntityService<Khasra>, IKhasraService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKhasraRepository _khasraRepository;
        public KhasraService(IUnitOfWork unitOfWork, IKhasraRepository khasraRepository)
      : base(unitOfWork, khasraRepository)
        {
            _unitOfWork = unitOfWork;
            _khasraRepository = khasraRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _khasraRepository.FindBy(a => a.Id == id);
            Khasra model = form.FirstOrDefault();
            model.IsActive = 0;
            _khasraRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Khasra> FetchSingleResult(int id)
        {
            var result = await _khasraRepository.FindBy(a => a.Id == id);
            Khasra model = result.FirstOrDefault();
            return model;
        }

       

        public async Task<List<Khasra>> GetAllKhasra()
        {

            return await _khasraRepository.GetAllKhasra();
        }

        public async Task<List<LandCategory>> GetAllLandCategory()
        {
            List<LandCategory> landcategoryList = await _khasraRepository.GetAllLandCategory();
            return landcategoryList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _khasraRepository.GetAllVillageList();
            return villageList;
        }

        public async Task<List<Khasra>> GetKhasraUsingRepo()
        {
            return await _khasraRepository.GetAllKhasra();
        }
        public async Task<PagedResult<Khasra>> GetPagedKhasra(KhasraMasterSearchDto model)
        {
            return await _khasraRepository.GetPagedKhasra(model);
        }

        public async Task<bool> Update(int id, Khasra khasra)
        {
            var result = await _khasraRepository.FindBy(a => a.Id == id);
            Khasra model = result.FirstOrDefault();
            model.AcquiredlandvillageId = khasra.AcquiredlandvillageId;
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
            _khasraRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Khasra khasra)
        {
            khasra.CreatedBy = 1;
            khasra.CreatedDate = DateTime.Now;

            _khasraRepository.Add(khasra);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<PagedResult<Khasra>> GetPagedVillageKhasraReport(VillageDetailsKhasraWiseReportSearchDto model)
        {
            return await _khasraRepository.GetPagedVillageKhasraReport(model);

        }
        public async Task<List<Khasra>> GetAllKhasraList(int? villageId)
        {
            List<Khasra> khasraList = await _khasraRepository.GetAllKhasraList(villageId);
            return khasraList;
        }


    }
}
