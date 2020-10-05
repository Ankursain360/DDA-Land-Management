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
   public class AwardplotDetailsService : EntityService<Awardplotdetails>, IAwardplotDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAwardplotDetailsRepository _awardplotDetailsRepository;
        public AwardplotDetailsService(IUnitOfWork unitOfWork, IAwardplotDetailsRepository awardplotDetailsRepository)
 : base(unitOfWork, awardplotDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _awardplotDetailsRepository = awardplotDetailsRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _awardplotDetailsRepository.FindBy(a => a.Id == id);
            Awardplotdetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _awardplotDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Awardplotdetails> FetchSingleResult(int id)
        {
            var result = await _awardplotDetailsRepository.FindBy(a => a.Id == id);
            Awardplotdetails model = result.FirstOrDefault();
            return model;
        }


        public async Task<List<Awardmasterdetail>> GetAllAWardmaster()
        {
            List<Awardmasterdetail> awardList = await _awardplotDetailsRepository.GetAllAWardmaster();
            return awardList;
        }
        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> khasraList = await _awardplotDetailsRepository.BindKhasra();
            return khasraList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _awardplotDetailsRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Awardplotdetails>> GetAwardplotdetails()
        {

            return await _awardplotDetailsRepository.GetAwardplotdetails();
        }



        public async Task<List<Awardplotdetails>> GetAwardplotdetailsUsingRepo()
        {
            return await _awardplotDetailsRepository.GetAwardplotdetails();
        }

        public async Task<bool> Update(int id, Awardplotdetails awardplotdetails)
        {
            var result = await _awardplotDetailsRepository.FindBy(a => a.Id == id);
            Awardplotdetails model = result.FirstOrDefault();
            model.AwardMasterId = awardplotdetails.AwardMasterId;
            model.VillageId = awardplotdetails.VillageId;
             model.KhasraId = awardplotdetails.KhasraId;
            model.Bigha = awardplotdetails.Bigha;
            model.Biswa = awardplotdetails.Biswa;
            model.Biswanshi = awardplotdetails.Biswanshi;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _awardplotDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Awardplotdetails awardplotdetails)
        {
            awardplotdetails.CreatedBy = 1;
            awardplotdetails.CreatedDate = DateTime.Now;
            awardplotdetails.IsActive = 1;

            _awardplotDetailsRepository.Add(awardplotdetails);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Awardplotdetails>> GetPagedAwardplotdetails(AwardPlotDetailSearchDto model)
        {
            return await _awardplotDetailsRepository.GetPagedAwardplotdetails(model);
        }





    }
}

