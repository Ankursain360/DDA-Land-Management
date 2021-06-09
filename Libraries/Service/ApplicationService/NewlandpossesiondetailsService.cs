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
    public class NewlandpossesiondetailsService : EntityService<Newlandpossessiondetails>, INewlandpossessiondetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandpossesiondetailsRepository _possessiondetailsRepository;

        public NewlandpossesiondetailsService(IUnitOfWork unitOfWork, INewlandpossesiondetailsRepository possessiondetailsRepository)
: base(unitOfWork, possessiondetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _possessiondetailsRepository = possessiondetailsRepository;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _possessiondetailsRepository.FindBy(a => a.Id == id);
            Newlandpossessiondetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _possessiondetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Newlandpossessiondetails> FetchSingleResult(int id)
        {
            var result = await _possessiondetailsRepository.FindBy(a => a.Id == id);
            Newlandpossessiondetails model = result.FirstOrDefault();
            return model;
        }


        public async Task<List<Newlandkhasra>> BindKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _possessiondetailsRepository.BindKhasra(villageId);
            return khasraList;
        }


        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _possessiondetailsRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllPossKhasra()
        {
            List<Newlandkhasra> posskhasraList = await _possessiondetailsRepository.GetAllPossKhasra();
            return posskhasraList;
        }
        public async Task<List<Undersection17>> GetAllus17()
        {
            List<Undersection17> us17List = await _possessiondetailsRepository.GetAllus17();
            return us17List;
        }
        public async Task<List<Undersection4>> GetAllus4()
        {
            List<Undersection4> us4List = await _possessiondetailsRepository.GetAllus4();
            return us4List;
        }
        public async Task<List<Undersection6>> GetAllus6()
        {
            List<Undersection6> us6List = await _possessiondetailsRepository.GetAllus6();
            return us6List;
        }

        public async Task<List<Newlandpossessiondetails>> GetAllPossessiondetails()
        {

            return await _possessiondetailsRepository.GetAllPossessiondetails();
        }



        public async Task<List<Newlandpossessiondetails>> GetPossessiondetailsUsingRepo()
        {
            return await _possessiondetailsRepository.GetAllPossessiondetails();
        }

        public async Task<bool> Update(int id, Newlandpossessiondetails possessiondetails)
        {
            var result = await _possessiondetailsRepository.FindBy(a => a.Id == id);
            Newlandpossessiondetails model = result.FirstOrDefault();

            model.VillageId = possessiondetails.VillageId;
            model.KhasraId = possessiondetails.KhasraId;
            model.IsActive = possessiondetails.IsActive;
            //model.PossKhasraId = possessiondetails.PossKhasraId;
            //model.PossDate = possessiondetails.PossDate;
            model.PossType = possessiondetails.PossType;
            model.Bigha = possessiondetails.Bigha;
            model.Biswa = possessiondetails.Biswa;
            model.Biswanshi = possessiondetails.Biswanshi;
            model.Us17id = possessiondetails.Us17id;
            model.Us4id = possessiondetails.Us4id;
            model.Us6id = possessiondetails.Us6id;
            model.ReasonNonPoss = possessiondetails.ReasonNonPoss;
            model.IsActive = possessiondetails.IsActive;
            model.Remarks = possessiondetails.Remarks;
            model.Reason = possessiondetails.Reason;
            model.DocumentName = possessiondetails.DocumentName;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _possessiondetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Newlandpossessiondetails newlandpossessiondetails)
        {
            newlandpossessiondetails.CreatedBy = 1;
            newlandpossessiondetails.CreatedDate = DateTime.Now;


            _possessiondetailsRepository.Add(newlandpossessiondetails);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Newlandpossessiondetails>> GetPagedNoPossessiondetails(NewlandpossesiondetailsSearchDto model)
        {
            return await _possessiondetailsRepository.GetPagedNoPossessiondetails(model);
        }





        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _possessiondetailsRepository.FetchSingleKhasraResult(khasraId);
        }

       
    }
}
