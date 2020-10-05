using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class JaraidetailService : EntityService<Jaraidetail>, IJaraidetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJaraidetailRepository _jaraidetailRepository;

        public async Task<PagedResult<Jaraidetail>> GetPagedJaraidetail(JaraiDetailsSearchDto model)
        {
            return await _jaraidetailRepository.GetPagedJaraidetail(model);
        }
        public JaraidetailService(IUnitOfWork unitOfWork, IJaraidetailRepository jaraidetailRepository)
: base(unitOfWork, jaraidetailRepository)
        {
            _unitOfWork = unitOfWork;
            _jaraidetailRepository = jaraidetailRepository;
        }








        public async Task<bool> Delete(int id)
        {
            var form = await _jaraidetailRepository.FindBy(a => a.Id == id);
            Jaraidetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _jaraidetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Jaraidetail> FetchSingleResult(int id)
        {
            var result = await _jaraidetailRepository.FindBy(a => a.Id == id);
            Jaraidetail model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Taraf>> GetAllTaraf()
        {
            List<Taraf> TarafList = await _jaraidetailRepository.GetAllTaraf();
            return TarafList;
        }



        public async Task<List<Khatauni>> GetAllKhatauni()
        {
            List<Khatauni> KhatauniList = await _jaraidetailRepository.GetAllKhatauni();
            return KhatauniList;
        }



        public async Task<List<Khewat>> GetAllKhewat()
        {
            List<Khewat> khewatList = await _jaraidetailRepository.GetAllKhewat();
            return khewatList;
        }
        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> khasraList = await _jaraidetailRepository.BindKhasra();
            return khasraList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _jaraidetailRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Jaraidetail>> GetJaraidetail()
        {

            return await _jaraidetailRepository.GetJaraidetail();
        }



        public async Task<List<Jaraidetail>> GetJaraidetailUsingRepo()
        {
            return await _jaraidetailRepository.GetJaraidetail();
        }

        public async Task<bool> Update(int id, Jaraidetail jaraidetail)
        {
            var result = await _jaraidetailRepository.FindBy(a => a.Id == id);
            Jaraidetail model = result.FirstOrDefault();
            model.KhewatId = jaraidetail.KhewatId;
            model.VillageId = jaraidetail.VillageId;
            model.KhasraId = jaraidetail.KhasraId;
            model.KhatauniId = jaraidetail.KhatauniId;
            model.TarafId = jaraidetail.TarafId;
            model.OwnerDetails = jaraidetail.OwnerDetails;
            model.FarmerDetails = jaraidetail.FarmerDetails;
            model.Kaifiyat = jaraidetail.Kaifiyat;

            model.Ahwal = jaraidetail.Ahwal;
            model.Revenue = jaraidetail.Revenue;
            model.OldMutationNo = jaraidetail.OldMutationNo;
          
            model.Remarks = jaraidetail.Remarks;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _jaraidetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Jaraidetail jaraidetail)
        {
            jaraidetail.CreatedBy = 1;
            jaraidetail.CreatedDate = DateTime.Now;
            jaraidetail.IsActive = 1;

            _jaraidetailRepository.Add(jaraidetail);
            return await _unitOfWork.CommitAsync() > 0;
        }
















    }
}
