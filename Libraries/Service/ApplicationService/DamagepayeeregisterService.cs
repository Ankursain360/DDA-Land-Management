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

namespace Service.ApplicationService
{
     public class DamagepayeeregisterService : EntityService<Damagepayeeregister>, IDamagepayeeregisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDamagepayeeregisterRepository _damagepayeeregisterRepository;

        public DamagepayeeregisterService(IUnitOfWork unitOfWork, IDamagepayeeregisterRepository damagepayeeregisterRepository)
        : base(unitOfWork, damagepayeeregisterRepository)
        {
            _unitOfWork = unitOfWork;
            _damagepayeeregisterRepository = damagepayeeregisterRepository;
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> localityList = await _damagepayeeregisterRepository.GetLocalityList();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            List<District> districtList = await _damagepayeeregisterRepository.GetDistrictList();
            return districtList;
        }
        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregister()
        {
            return await _damagepayeeregisterRepository.GetAllDamagepayeeregister();
        }



        public async Task<List<Damagepayeeregister>> GetDamagepayeeregisterUsingRepo()
        {
            return await _damagepayeeregisterRepository.GetAllDamagepayeeregister();
        }

        public async Task<Damagepayeeregister> FetchSingleResult(int id)
        {
            var result = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Damagepayeeregister damagepayeeregister)
        {
            var result = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = result.FirstOrDefault();
            model.FileNo = damagepayeeregister.FileNo;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _damagepayeeregisterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Damagepayeeregister damagepayeeregister)
        {
            damagepayeeregister.CreatedBy = 1;
            damagepayeeregister.CreatedDate = DateTime.Now;
            _damagepayeeregisterRepository.Add(damagepayeeregister);
            return await _unitOfWork.CommitAsync() > 0;
        }

      

        public async Task<bool> Delete(int id)
        {
            var form = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = form.FirstOrDefault();
            model.IsActive = 0;
            _damagepayeeregisterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregisterSearchDto model)
        {
            return await _damagepayeeregisterRepository.GetPagedDamagepayeeregister(model);
        }


    }
}
