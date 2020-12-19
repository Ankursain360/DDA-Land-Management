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
    public class SelfAssessmentDamageService : EntityService<Damagepayeeregister>, ISelfAssessmentDamageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelfAssessmentDamageRepository _selfAssessmentDamageRepository;

        public SelfAssessmentDamageService(IUnitOfWork unitOfWork, ISelfAssessmentDamageRepository selfAssessmentDamageRepository)
        : base(unitOfWork, selfAssessmentDamageRepository)
        {
            _unitOfWork = unitOfWork;
            _selfAssessmentDamageRepository = selfAssessmentDamageRepository;
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> localityList = await _selfAssessmentDamageRepository.GetLocalityList();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            List<District> districtList = await _selfAssessmentDamageRepository.GetDistrictList();
            return districtList;
        }
        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregister()
        {
            return await _selfAssessmentDamageRepository.GetAllDamagepayeeregister();
        }



        public async Task<List<Damagepayeeregister>> GetDamagepayeeregisterUsingRepo()
        {
            return await _selfAssessmentDamageRepository.GetAllDamagepayeeregister();
        }

        public async Task<Damagepayeeregister> FetchSingleResult(int id)
        {
            var result = await _selfAssessmentDamageRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Damagepayeeregister damagepayeeregister)
        {
            var result = await _selfAssessmentDamageRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = result.FirstOrDefault();
            model.FileNo = damagepayeeregister.FileNo;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _selfAssessmentDamageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Damagepayeeregister damagepayeeregister)
        {
            damagepayeeregister.CreatedBy = 1;
            damagepayeeregister.CreatedDate = DateTime.Now;
            _selfAssessmentDamageRepository.Add(damagepayeeregister);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _selfAssessmentDamageRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = form.FirstOrDefault();
            model.IsActive = 0;
            _selfAssessmentDamageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregisterSearchDto model)
        {
            return await _selfAssessmentDamageRepository.GetPagedDamagepayeeregister(model);
        }

        //********* rpt 1 Persolnal info of damage assesse ***********
        public async Task<bool> SavePayeePersonalInfo(Damagepayeepersonelinfo damagepayeepersonelinfo)
        {
            damagepayeepersonelinfo.CreatedBy = 1;
            damagepayeepersonelinfo.CreatedDate = DateTime.Now;
            damagepayeepersonelinfo.IsActive = 1;
            return await _selfAssessmentDamageRepository.SavePayeePersonalInfo(damagepayeepersonelinfo);
        }

        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id)
        {
            return await _selfAssessmentDamageRepository.GetPersonalInfo(id);
        }
        public async Task<bool> DeletePayeePersonalInfo(int Id)
        {
            return await _selfAssessmentDamageRepository.DeletePayeePersonalInfo(Id);
        }


        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteType(List<Allottetype> allottetype)
        {
            allottetype.ForEach(x => x.CreatedBy = 1);
            allottetype.ForEach(x => x.CreatedDate = DateTime.Now);
            allottetype.ForEach(x => x.IsActive = 1);
            return await _selfAssessmentDamageRepository.SaveAllotteType(allottetype);
        }
        public async Task<List<Allottetype>> GetAllottetype(int id)
        {
            return await _selfAssessmentDamageRepository.GetAllottetype(id);
        }
        public async Task<bool> DeleteAllotteType(int Id)
        {
            return await _selfAssessmentDamageRepository.DeleteAllotteType(Id);
        }



        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistory(List<Damagepaymenthistory> damagepaymenthistory)
        {
            damagepaymenthistory.ForEach(x => x.CreatedBy = 1);
            damagepaymenthistory.ForEach(x => x.CreatedDate = DateTime.Now);
            damagepaymenthistory.ForEach(x => x.IsActive = 1);
            return await _selfAssessmentDamageRepository.SavePaymentHistory(damagepaymenthistory);
        }
        public async Task<List<Damagepaymenthistory>> GetPaymentHistory(int id)
        {
            return await _selfAssessmentDamageRepository.GetPaymentHistory(id);
        }
        public async Task<bool> DeletePaymentHistory(int Id)
        {
            return await _selfAssessmentDamageRepository.DeletePaymentHistory(Id);
        }

    }
}
