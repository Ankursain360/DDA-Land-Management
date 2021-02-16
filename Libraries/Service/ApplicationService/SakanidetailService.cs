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


        public async Task<PagedResult<Saknidetails>> GetPagedSakanidetail(SakaniDetailsSearchDto model)
        {
            return await _sakanidetailRepository.GetPagedSakanidetail(model);
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


        public async Task<List<Khewat>> GetAllKhewat()
        {
            List<Khewat> khewatList = await _sakanidetailRepository.GetAllKhewat();
            return khewatList;
        }
        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> khasraList = await _sakanidetailRepository.BindKhasra();
            return khasraList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _sakanidetailRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Saknidetails>> GetSakanidetail()
        {

            return await _sakanidetailRepository.GetSakanidetail();
        }



        public async Task<List<Saknidetails>> GetSakanidetailUsingRepo()
        {
            return await _sakanidetailRepository.GetSakanidetail();
        }

        public async Task<bool> Update(int id, Saknidetails sakanidetail)
        {
            var result = await _sakanidetailRepository.FindBy(a => a.Id == id);
            Saknidetails model = result.FirstOrDefault();
           
            model.VillageId = sakanidetail.VillageId;
            model.KhasraId = sakanidetail.KhasraId;
           
            model.Location = sakanidetail.Location;
           
            model.Remarks = sakanidetail.Remarks;
          
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _sakanidetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Saknidetails sakanidetail)
        {
            sakanidetail.CreatedBy = 1;
            sakanidetail.CreatedDate = DateTime.Now;
            sakanidetail.IsActive = 1;

            _sakanidetailRepository.Add(sakanidetail);
            return await _unitOfWork.CommitAsync() > 0;
        }














    }
}
