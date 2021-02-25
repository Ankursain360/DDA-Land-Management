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
using Dto.Master;

namespace Libraries.Service.ApplicationService
{
    public class NewlandawardplotdetailsService : EntityService<Newlandawardplotdetails>, INewlandawardplotdetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly InewlandawardplotdetailsRepository _newlandawardplotdetailsRepository;
        public NewlandawardplotdetailsService(IUnitOfWork unitOfWork, InewlandawardplotdetailsRepository newalandawardplotDetailsRepository)
 : base(unitOfWork, newalandawardplotDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandawardplotdetailsRepository = newalandawardplotDetailsRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _newlandawardplotdetailsRepository.FindBy(a => a.Id == id);
            Newlandawardplotdetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandawardplotdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Newlandawardplotdetails> FetchSingleResult(int id)
        {
            var result = await _newlandawardplotdetailsRepository.FindBy(a => a.Id == id);
            Newlandawardplotdetails model = result.FirstOrDefault();
            return model;
        }


        public async Task<List<Newlandawardmasterdetail>> GetAllAWardmaster()
        {
            List<Newlandawardmasterdetail> awardList = await _newlandawardplotdetailsRepository.GetAllAWardmaster();
            return awardList;
        }
        public async Task<List<Newlandkhasra>> BindKhasra()
        {
            List<Newlandkhasra> khasraList = await _newlandawardplotdetailsRepository.BindKhasra();
            return khasraList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _newlandawardplotdetailsRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Newlandawardplotdetails>> GetAwardplotdetails()
        {

            return await _newlandawardplotdetailsRepository.GetAwardplotdetails();
        }



        public async Task<List<Newlandawardplotdetails>> GetAwardplotdetailsUsingRepo()
        {
            return await _newlandawardplotdetailsRepository.GetAwardplotdetails();
        }

        public async Task<bool> Update(int id, Newlandawardplotdetails awardplotdetails)
        {
            var result = await _newlandawardplotdetailsRepository.FindBy(a => a.Id == id);
            Newlandawardplotdetails model = result.FirstOrDefault();
            model.AwardMasterId = awardplotdetails.AwardMasterId;
            model.VillageId = awardplotdetails.VillageId;
            model.KhasraId = awardplotdetails.KhasraId;
            model.Bigha = awardplotdetails.Bigha;
            model.Biswa = awardplotdetails.Biswa;
            model.Biswanshi = awardplotdetails.Biswanshi;
            model.Remarks = awardplotdetails.Remarks;
            model.IsActive = awardplotdetails.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _newlandawardplotdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Newlandawardplotdetails awardplotdetails)
        {

            awardplotdetails.CreatedBy = 1;
            awardplotdetails.CreatedDate = DateTime.Now;
            awardplotdetails.IsActive = 1;

            _newlandawardplotdetailsRepository.Add(awardplotdetails);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Newlandawardplotdetails>> GetPagedAwardplotdetails(NewlandawardplotdetailsSearchDto model)
        {
            return await _newlandawardplotdetailsRepository.GetPagedAwardplotdetails(model);
        }

       
    }
}

