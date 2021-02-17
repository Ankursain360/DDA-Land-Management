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
    public class SakanidetailService : EntityService<Saknidetails>, ISakanidetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISakanidetailRepository _sakanidetailRepository;

        public SakanidetailService(IUnitOfWork unitOfWork, ISakanidetailRepository sakanidetailRepository)
: base(unitOfWork, sakanidetailRepository)
        {
            _unitOfWork = unitOfWork;
            _sakanidetailRepository = sakanidetailRepository;
        }
       
        public async Task<PagedResult<Saknidetails>> GetPagedSaknidetail(SakaniDetailsSearchDto model)
        {
            return await _sakanidetailRepository.GetPagedSaknidetail(model);
        }


        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _sakanidetailRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Khasra>> GetAllKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _sakanidetailRepository.GetAllKhasra(villageId);
            return khasraList;
        }
        public async Task<List<Saknidetails>> GetAllSaknidetail()
        {

            return await _sakanidetailRepository.GetAllSaknidetail();
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _sakanidetailRepository.FindBy(a => a.Id == id);
            Saknidetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _sakanidetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Saknidetails> FetchSingleResult(int id)
        {
            var result = await _sakanidetailRepository.FindBy(a => a.Id == id);
            Saknidetails model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Update(int id, Saknidetails sakni)
        {
            var result = await _sakanidetailRepository.FindBy(a => a.Id == id);
            Saknidetails model = result.FirstOrDefault();

            model.VillageId = sakni.VillageId;
            model.KhasraId = sakni.KhasraId;
            model.YearOfjamabandi = sakni.YearOfjamabandi;
            model.NoOfKhewat = sakni.NoOfKhewat;
            model.NoOfKhatauni = sakni.NoOfKhatauni;
            model.Location = sakni.Location;
            model.Mortgage = sakni.Mortgage;
            model.RentAmount = sakni.RentAmount;
            model.OldMutationNo = sakni.OldMutationNo;
            model.Remarks = sakni.Remarks;
            model.IsActive = sakni.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = sakni.ModifiedBy;
            _sakanidetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Saknidetails sakni)
        {
            sakni.CreatedBy = sakni.CreatedBy;
            sakni.CreatedDate = DateTime.Now;
            sakni.IsActive = 1;

            _sakanidetailRepository.Add(sakni);
            return await _unitOfWork.CommitAsync() > 0;
        }

        //********* rpt ! Owner Details **********
        public async Task<bool> SaveOwner(Sakniowner owner)
        {
            owner.CreatedBy = owner.CreatedBy;
            owner.CreatedDate = DateTime.Now;
            owner.IsActive = 1;
            return await _sakanidetailRepository.SaveOwner(owner);
        }
        public async Task<List<Sakniowner>> GetAllOwner(int id)
        {
            return await _sakanidetailRepository.GetAllOwner(id);
        }
        public async Task<bool> DeleteOwner(int Id)
        {
            return await _sakanidetailRepository.DeleteOwner(Id);
        }

        //********* rpt ! Lessee Details **********

        public async Task<bool> Savelessee(Saknilessee lessee)
        {
            lessee.CreatedBy = lessee.CreatedBy;
            lessee.CreatedDate = DateTime.Now;
            lessee.IsActive = 1;
            return await _sakanidetailRepository.Savelessee(lessee);
        }
        public async Task<List<Saknilessee>> GetAllSaknilessee(int id)
        {
            return await _sakanidetailRepository.GetAllSaknilessee(id);
        }
        public async Task<bool> Deletelessee(int Id)
        {
            return await _sakanidetailRepository.Deletelessee(Id);
        }

        //********* rpt ! Tenant Details **********

        public async Task<bool> SaveTenant(Saknitenant tenant)
        {
            tenant.CreatedBy = tenant.CreatedBy;
            tenant.CreatedDate = DateTime.Now;
            tenant.IsActive = 1;
            return await _sakanidetailRepository.SaveTenant(tenant);
        }
        public async Task<List<Saknitenant>> GetAllTenant(int id)
        {
            return await _sakanidetailRepository.GetAllTenant(id);
        }
        public async Task<bool> DeleteTenant(int Id)
        {
            return await _sakanidetailRepository.DeleteTenant(Id);
        }



    }
}
