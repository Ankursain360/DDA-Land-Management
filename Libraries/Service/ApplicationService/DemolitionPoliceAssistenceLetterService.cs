using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class DemolitionPoliceAssistenceLetterService : EntityService<Demolitionpoliceassistenceletter>, IDemolitionPoliceAssistenceLetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemolitionPoliceAssistenceLetterRepository _demolitionPoliceAssistenceLetterRepository;
        public DemolitionPoliceAssistenceLetterService(IUnitOfWork unitOfWork, IDemolitionPoliceAssistenceLetterRepository demolitionPoliceAssistenceLetterRepository)
        : base(unitOfWork, demolitionPoliceAssistenceLetterRepository)
        {
            _unitOfWork = unitOfWork;
            _demolitionPoliceAssistenceLetterRepository = demolitionPoliceAssistenceLetterRepository;
        }

        public async Task<PagedResult<Fixingdemolition>> GetPagedApprovedAnnexureA(DemolitionPoliceAssistenceLetterSearchDto model, int userId, int approved)
        {
            return await _demolitionPoliceAssistenceLetterRepository.GetPagedApprovedAnnexureA(model, userId, approved);
        }

        public async Task<bool> Create(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        {
            demolitionpoliceassistenceletter.CreatedDate = DateTime.Now;
            _demolitionPoliceAssistenceLetterRepository.Add(demolitionpoliceassistenceletter);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Demolitionpoliceassistenceletter>> GetPagedApprovedAnnexureAListedit(DemolitionPoliceAssistenceLetterSearchDto model, int userId)
        {
            return await _demolitionPoliceAssistenceLetterRepository.GetPagedApprovedAnnexureAListedit(model, userId);
        }

        public async Task<bool> Update(int id, Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        {
            var result = await _demolitionPoliceAssistenceLetterRepository.FindBy(a => a.Id == id);
            Demolitionpoliceassistenceletter model = result.FirstOrDefault();
            model.FixingDemolitionId = demolitionpoliceassistenceletter.FixingDemolitionId;
            //if(demolitionpoliceassistenceletter.GenerateUpload == 0)
            //{
            model.OfficeName = demolitionpoliceassistenceletter.OfficeName;
            model.OfficeDepartment = demolitionpoliceassistenceletter.OfficeDepartment;
            model.OfficeZone = demolitionpoliceassistenceletter.OfficeZone;
            model.OfficeAddress = demolitionpoliceassistenceletter.OfficeAddress;
            model.FileNo = demolitionpoliceassistenceletter.FileNo;
            model.LetterDate = demolitionpoliceassistenceletter.LetterDate;
            model.DyCommOffcAddress = demolitionpoliceassistenceletter.DyCommOffcAddress;
            model.KhasraNo = demolitionpoliceassistenceletter.KhasraNo;
            model.VillageName = demolitionpoliceassistenceletter.VillageName;
            model.KhasraAddress = demolitionpoliceassistenceletter.KhasraAddress;
            model.PoliceStationName = demolitionpoliceassistenceletter.PoliceStationName;
            model.OperationDate = demolitionpoliceassistenceletter.OperationDate;
            model.OperationDay = demolitionpoliceassistenceletter.OperationDay;
            model.RevenueOfficerZone = demolitionpoliceassistenceletter.RevenueOfficerZone;
            model.RevenueOfficerWing = demolitionpoliceassistenceletter.RevenueOfficerWing;
            model.RevenueOfficerBranch = demolitionpoliceassistenceletter.RevenueOfficerBranch;
            model.MeetingDate = demolitionpoliceassistenceletter.MeetingDate;
            model.MeetingTime = demolitionpoliceassistenceletter.MeetingTime;
            model.ChiefEngineerAddress = demolitionpoliceassistenceletter.ChiefEngineerAddress;
            model.Shoaddress = demolitionpoliceassistenceletter.Shoaddress;
            model.GeneralConditions = demolitionpoliceassistenceletter.GeneralConditions;
           
            ////}
            ////else
           // model.FilePath = demolitionpoliceassistenceletter.FilePath;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = demolitionpoliceassistenceletter.ModifiedBy;
            _demolitionPoliceAssistenceLetterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Demolitionpoliceassistenceletter> FetchSingleResult(int id)
        {
            return await _demolitionPoliceAssistenceLetterRepository.FetchSingleResult(id);
        }

        public async Task<Demolitionpoliceassistenceletter> FetchSingleResultButOnAneexureId(int id)
        {
            return await _demolitionPoliceAssistenceLetterRepository.FetchSingleResultButOnAneexureId(id);
        }

        public async Task<Fixingdemolition> FetchSingleResultOfFixingDemolition(int id)
        {
            return await _demolitionPoliceAssistenceLetterRepository.FetchSingleResultOfFixingDemolition(id);
        }
    }
}
