using Dto.Search;
using Libraries.Model.Common;
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

namespace Libraries.Service.ApplicationService
{
    public class DemolitionstructuredetailsService : EntityService<Demolitionstructuredetails>, IDemolitionstructuredetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemolitionstructuredetailsRepository _demolitionstructuredetailsRepository;
        public DemolitionstructuredetailsService(IUnitOfWork unitOfWork, IDemolitionstructuredetailsRepository demolitionstructuredetailsRepository)
      : base(unitOfWork, demolitionstructuredetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _demolitionstructuredetailsRepository = demolitionstructuredetailsRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _demolitionstructuredetailsRepository.FindBy(a => a.Id == id);
            Demolitionstructuredetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _demolitionstructuredetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Demolitionstructuredetails> FetchSingleResult(int id)
        {
            return await _demolitionstructuredetailsRepository.FetchSingleResult(id);
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            return await _demolitionstructuredetailsRepository.GetAllDepartment();
        }
        public async Task<List<Division>> GetAllDivisionList(int zone)
        {
            return await _demolitionstructuredetailsRepository.GetAllDivision(zone);
        }
        public async Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetails()
        {
            return await _demolitionstructuredetailsRepository.GetAllDemolitionstructuredetails();
        }

        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _demolitionstructuredetailsRepository.GetAllLocalityList(divisionId);
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _demolitionstructuredetailsRepository.GetAllZone(departmentId);
        }
        public async Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionstructuredetails(DemolitionstructuredetailsDto model)
        {
            return await _demolitionstructuredetailsRepository.GetPagedDemolitionstructuredetails(model);
        }
        public async Task<bool> Update(int id, Demolitionstructuredetails demolitionstructuredetails)
        {
            var result = await _demolitionstructuredetailsRepository.FindBy(a => a.Id == id);
            Demolitionstructuredetails model = result.FirstOrDefault();
            model.DepartmentId = demolitionstructuredetails.DepartmentId;
            model.ZoneId = demolitionstructuredetails.ZoneId;
            model.DivisionId = demolitionstructuredetails.DivisionId;
            model.LocalityId = demolitionstructuredetails.LocalityId;
            model.FileNo = demolitionstructuredetails.FileNo;
            model.Date = demolitionstructuredetails.Date;
            model.Area = demolitionstructuredetails.Area;
            model.PoliceStation = demolitionstructuredetails.PoliceStation;
            model.PoliceStation = demolitionstructuredetails.PoliceStation;
            model.NameOfAreaSite = demolitionstructuredetails.NameOfAreaSite;
            model.EncroachmentSinceDate = demolitionstructuredetails.EncroachmentSinceDate;
            model.DateOfApprovalDemolition = demolitionstructuredetails.DateOfApprovalDemolition;
            model.NameOfEncroacherIfAny = demolitionstructuredetails.NameOfEncroacherIfAny;
            model.Remarks = demolitionstructuredetails.Remarks;
            model.StartOfDemolitionActionDate = demolitionstructuredetails.StartOfDemolitionActionDate;
            model.EndOfDemolitionActionDate = demolitionstructuredetails.EndOfDemolitionActionDate;
            model.AreaReclaimed = demolitionstructuredetails.AreaReclaimed;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            model.IsActive = 1;
            _demolitionstructuredetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Demolitionstructuredetails demolitionstructuredetails)
        {
            demolitionstructuredetails.CreatedBy = 1;
            demolitionstructuredetails.CreatedDate = DateTime.Now;
            _demolitionstructuredetailsRepository.Add(demolitionstructuredetails);
            return await _unitOfWork.CommitAsync() > 0;
        }
        //public async Task<List<Khasra>> GetAllKhasraList(int localityId)
        //{
        //    return await _encroachmentRegisterationRepository.GetAllKhasraList(localityId);
        //}
        public async Task<bool> SaveDemolitionstructure(Demolitionstructure demolitionstructure)
        {
            demolitionstructure.CreatedBy = 1;
            demolitionstructure.CreatedDate = DateTime.Now;
            demolitionstructure.IsActive = 1;
            return await _demolitionstructuredetailsRepository.SaveDemolitionstructure(demolitionstructure);
        }
        public async Task<bool> DeleteDemolitionstructure(int Id)
        {
            return await _demolitionstructuredetailsRepository.DeleteDemolitionstructure(Id);
        }
      

        public async Task<bool> SaveDemolitionstructureafterdemolitionphotofiledetails(Demolitionstructureafterdemolitionphotofiledetails demolitionstructureafterdemolitionphotofiledetails)
        {
            demolitionstructureafterdemolitionphotofiledetails.CreatedBy = 1;
            demolitionstructureafterdemolitionphotofiledetails.CreatedDate = DateTime.Now;
            demolitionstructureafterdemolitionphotofiledetails.IsActive = 1;
            return await _demolitionstructuredetailsRepository.SaveDemolitionstructureafterdemolitionphotofiledetails(demolitionstructureafterdemolitionphotofiledetails);
        }

        public async Task<bool> DeleteDemolitionstructureafterdemolitionphotofiledetails(int Id)
        {
            return await _demolitionstructuredetailsRepository.DeleteDemolitionstructureafterdemolitionphotofiledetails(Id);
        }
        public async Task<bool> SaveDemolitionstructurebeforedemolitionphotofiledetails(Demolitionstructurebeforedemolitionphotofiledetails demolitionstructurebeforedemolitionphotofiledetails)
        {
            demolitionstructurebeforedemolitionphotofiledetails.CreatedBy = 1;
            demolitionstructurebeforedemolitionphotofiledetails.CreatedDate = DateTime.Now;
            demolitionstructurebeforedemolitionphotofiledetails.IsActive = 1;
            return await _demolitionstructuredetailsRepository.SaveDemolitionstructurebeforedemolitionphotofiledetails(demolitionstructurebeforedemolitionphotofiledetails);
        }

        public async Task<bool> DeleteDemolitionstructurebeforedemolitionphotofiledetails(int Id)
        {
            return await _demolitionstructuredetailsRepository.DeleteDemolitionstructurebeforedemolitionphotofiledetails(Id);
        }

       

        public async Task<List<Demolitionstructure>> GetDemolitionstructure(int Id)
        {
            return await _demolitionstructuredetailsRepository.GetDemolitionstructure(Id);
        }

        public async Task<Demolitionstructureafterdemolitionphotofiledetails> GetDemolitionstructureafterdemolitionphotofiledetails(int Id)
        {
            return await _demolitionstructuredetailsRepository.GetDemolitionstructureafterdemolitionphotofiledetails(Id);
        }

        public async Task<Demolitionstructurebeforedemolitionphotofiledetails> GetDemolitionstructurebeforedemolitionphotofiledetails(int Id)
        {
            return await _demolitionstructuredetailsRepository.GetDemolitionstructurebeforedemolitionphotofiledetails(Id);
        }

        public async Task<List<Structure>> GetStructure()
        {
            return await _demolitionstructuredetailsRepository.GetStructure();
        }
    }
}