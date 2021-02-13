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
    public class JaraidetailService : EntityService<Jaraidetails>, IJaraidetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJaraidetailRepository _jaraidetailRepository;

        public async Task<PagedResult<Jaraidetails>> GetPagedJaraidetail(JaraiDetailsSearchDto model)
        {
            return await _jaraidetailRepository.GetPagedJaraidetail(model);
        }
        public JaraidetailService(IUnitOfWork unitOfWork, IJaraidetailRepository jaraidetailRepository): base(unitOfWork, jaraidetailRepository)
        {
            _unitOfWork = unitOfWork;
            _jaraidetailRepository = jaraidetailRepository;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _jaraidetailRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Khasra>> GetAllKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _jaraidetailRepository.GetAllKhasra(villageId);
            return khasraList;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _jaraidetailRepository.FindBy(a => a.Id == id);
            Jaraidetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _jaraidetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Jaraidetails> FetchSingleResult(int id)
        {
            var result = await _jaraidetailRepository.FindBy(a => a.Id == id);
            Jaraidetails model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Jaraidetails>> GetAllJaraidetail()
        {

            return await _jaraidetailRepository.GetAllJaraidetail();
        }

        public async Task<List<Jaraidetails>> GetJaraidetailUsingRepo()
        {
            return await _jaraidetailRepository.GetAllJaraidetail();
        }

        public async Task<bool> Update(int id, Jaraidetails jarai)
        {
            var result = await _jaraidetailRepository.FindBy(a => a.Id == id);
            Jaraidetails model = result.FirstOrDefault();
            
            model.VillageId = jarai.VillageId;
            model.KhasraId = jarai.KhasraId;
            model.Revenue = jarai.Revenue;
            model.OldMutationNo = jarai.OldMutationNo;
            model.Remarks = jarai.Remarks;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _jaraidetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Jaraidetails jarai)
        {
            jarai.CreatedBy = jarai.CreatedBy;
            jarai.CreatedDate = DateTime.Now;
            jarai.IsActive = 1;

            _jaraidetailRepository.Add(jarai);
            return await _unitOfWork.CommitAsync() > 0;
        }

        //********* rpt ! Owner Details **********

        public async Task<bool> SaveOwner(Jaraiowner Jaraiowner)
        {
            Jaraiowner.CreatedBy = Jaraiowner.CreatedBy;
            Jaraiowner.CreatedDate = DateTime.Now;
            Jaraiowner.IsActive = 1;
            return await _jaraidetailRepository.SaveOwner(Jaraiowner);
        }
        public async Task<List<Jaraiowner>> GetAllOwner(int id)
        {
            return await _jaraidetailRepository.GetAllOwner(id);
        }
        public async Task<bool> DeleteOwner(int Id)
        {
            return await _jaraidetailRepository.DeleteOwner(Id);
        }

        //********* rpt ! Lessee Details **********

        public async Task<bool> SaveJarailessee(Jarailessee Jarailessee)
        {
            Jarailessee.CreatedBy = Jarailessee.CreatedBy;
            Jarailessee.CreatedDate = DateTime.Now;
            Jarailessee.IsActive = 1;
            return await _jaraidetailRepository.SaveJarailessee(Jarailessee);
        }
        public async Task<List<Jarailessee>> GetAllJarailessee(int id)
        {
            return await _jaraidetailRepository.GetAllJarailessee(id);
        }
        public async Task<bool> DeleteJarailessee(int Id)
        {
            return await _jaraidetailRepository.DeleteJarailessee(Id);
        }

        //********* rpt ! Farmer Details **********

        public async Task<bool> Savefarmer(Jaraifarmer farmer)
        {
            farmer.CreatedBy = farmer.CreatedBy;
            farmer.CreatedDate = DateTime.Now;
            farmer.IsActive = 1;
            return await _jaraidetailRepository.Savefarmer(farmer);
        }
        public async Task<List<Jaraifarmer>> GetAllFarmer(int id)
        {
            return await _jaraidetailRepository.GetAllFarmer(id);
        }
        public async Task<bool> DeleteFarmer(int Id)
        {
            return await _jaraidetailRepository.DeleteFarmer(Id);
        }
    }
}
