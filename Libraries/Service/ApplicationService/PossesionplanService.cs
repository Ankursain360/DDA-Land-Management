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
   public class PossesionplanService : EntityService<Possesionplan>, IPossesionplanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPossesionplanRepository _possesionplanRepository;

        public PossesionplanService(IUnitOfWork unitOfWork, IPossesionplanRepository possesionplanRepository)
: base(unitOfWork, possesionplanRepository)
        {
            _unitOfWork = unitOfWork;
            _possesionplanRepository = possesionplanRepository;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _possesionplanRepository.FindBy(a => a.Id == id);
            Possesionplan model = form.FirstOrDefault();
            model.IsActive = 0;
            _possesionplanRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Possesionplan> FetchSingleResult(int id)
        {
            var result = await _possesionplanRepository.FindBy(a => a.Id == id);
            Possesionplan model = result.FirstOrDefault();
            return model;
        }


        public async Task<List<Leaseapplication>> BindLeaseApplicationDetails(int? appId)
        {
            List<Leaseapplication> leaseapplicationList = await _possesionplanRepository.BindLeaseApplicationDetails(appId);
            return leaseapplicationList;
        }

        public async Task<List<Allotmententry>> BindAllotmentDetails(int? AllotmentId)
        {
            List<Allotmententry> allotmentList = await _possesionplanRepository.BindAllotmentDetails(AllotmentId);
            return allotmentList;
        }
        

        public async Task<List<Allotmententry>> GetAllAllotmententry()
        {

            return await _possesionplanRepository.GetAllAllotmententry();
        }

        public async Task<List<Leaseapplication>> GetAllLeaseApplication()
        {

            return await _possesionplanRepository.GetAllLeaseApplication();
        }
        public async Task<List<Possesionplan>> GetAllPossesionplan()
        {

            return await _possesionplanRepository.GetAllPossesionplan();
        }

        public async Task<bool> Update(int id, Possesionplan possesionplan)
        {
            var result = await _possesionplanRepository.FindBy(a => a.Id == id);
            Possesionplan model = result.FirstOrDefault();

            model.AllotedArea = possesionplan.AllotedArea;
            //model.AllotmentId = possesionplan.AllotmentId;
            model.AllotmentId = possesionplan.AllotmentId;
            model.DiffernceArea = possesionplan.DiffernceArea;
            model.IsActive = possesionplan.IsActive;
            model.NorthEast = possesionplan.NorthEast;
            model.NorthWest = possesionplan.NorthWest;
            model.SouthEast = possesionplan.SouthEast;
            model.SouthWest = possesionplan.SouthWest;
            // model.SitePlanFilePath = possesionplan.SitePlanFilePath;
           // model.StayInterimGrantedDocument = legalmanagementsystem.StayInterimGranted != null ? legalmanagementsystem.StayInterimGrantedDocument : model.StayInterimGrantedDocument;

            model.SitePlanFilePath = possesionplan.SitePlanFilePath != null ? possesionplan.SitePlanFilePath : model.SitePlanFilePath;

            model.Remark = possesionplan.Remark;
            model.PossessionTakenName = possesionplan.PossessionTakenName;
            model.PossessionTakenDate = possesionplan.PossessionTakenDate;
            model.PossessionArea = possesionplan.PossessionArea;
            model.PossesionHandOverName = possesionplan.PossesionHandOverName;
            model.PossesionHandOverDate = possesionplan.PossesionHandOverDate;
           
            
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _possesionplanRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Possesionplan possesionplan)
        {
            possesionplan.CreatedBy = possesionplan.CreatedBy;
            possesionplan.CreatedDate = DateTime.Now;


            _possesionplanRepository.Add(possesionplan);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Possesionplan>> GetPagedPossesionPlan(PossesionplanSearchDto model)
        {
            return await _possesionplanRepository.GetPagedPossesionPlan(model);
        }


        public string GetDownload(int id)
        {
            return _possesionplanRepository.GetDownload(id);
        }





    }
}
