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

        //********* rpt 1 Persolnal info of damage assesse ***********
        public async Task<bool> SavePayeePersonalInfo(Damagepayeepersonelinfo damagepayeepersonelinfo)
        {
            damagepayeepersonelinfo.CreatedBy = 1;
            damagepayeepersonelinfo.CreatedDate = DateTime.Now;
            damagepayeepersonelinfo.IsActive = 1;
            return await _damagepayeeregisterRepository.SavePayeePersonalInfo(damagepayeepersonelinfo);
        }

        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id)
        {
            return await _damagepayeeregisterRepository.GetPersonalInfo(id);
        }
        public async Task<bool> DeletePayeePersonalInfo(int Id)
        {
            return await _damagepayeeregisterRepository.DeletePayeePersonalInfo(Id);
        }


        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteType(List<Allottetype> allottetype)
        {
            allottetype.ForEach(x => x.CreatedBy = 1);
            allottetype.ForEach(x => x.CreatedDate =DateTime.Now);
            allottetype.ForEach(x => x.IsActive = 1);
            return await _damagepayeeregisterRepository.SaveAllotteType(allottetype);
        }
        public async Task<List<Allottetype>> GetAllottetype(int id)
        {
            return await _damagepayeeregisterRepository.GetAllottetype(id);
        }
        public async Task<bool> DeleteAllotteType(int Id)
        {
            return await _damagepayeeregisterRepository.DeleteAllotteType(Id);
        }



        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistory(Damagepaymenthistory Damagepaymenthistory)
        {
            Damagepaymenthistory.CreatedBy = 1;
            Damagepaymenthistory.CreatedDate = DateTime.Now;
            Damagepaymenthistory.IsActive = 1;
            return await _damagepayeeregisterRepository.SavePaymentHistory(Damagepaymenthistory);
        }
        public async Task<List<Damagepaymenthistory>> GetPaymentHistory(int id)
        {
            return await _damagepayeeregisterRepository.GetPaymentHistory(id);
        }
        public async Task<bool> DeletePaymentHistory(int Id)
        {
            return await _damagepayeeregisterRepository.DeletePaymentHistory(Id);
        }

    }
}
