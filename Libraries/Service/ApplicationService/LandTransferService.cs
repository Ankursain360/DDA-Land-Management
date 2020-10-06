using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.EntityRepository;
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
    public class LandTransferService: EntityService<LandTransfer>, ILandTransferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILandTransferRepository _landTransferRepository;
        public LandTransferService(IUnitOfWork unitOfWork, LandTransferRepository landTransferRepository)
        : base(unitOfWork, landTransferRepository)
        {
            _unitOfWork = unitOfWork;
            _landTransferRepository = landTransferRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _landTransferRepository.FindBy(a => a.Id == id);
            LandTransfer model = form.FirstOrDefault();
            model.IsActive = 0;
            _landTransferRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<LandTransfer> FetchSingleResult(int id)
        {
            var result = await _landTransferRepository.FindBy(a => a.Id == id);
            LandTransfer model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _landTransferRepository.GetAllDepartment();
            return departmentList;
        }

        public async Task<List<Division>> GetAllDivisionList(int zone)
        {
            List<Division> divisionList = await _landTransferRepository.GetAllDivision(zone);
            return divisionList;
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _landTransferRepository.GetAllZone(departmentId);
            return zoneList;
        }

        public async Task<List<LandTransfer>> GetLandTransferUsingRepo()
        {
            List<LandTransfer> landTransfer = await _landTransferRepository.GetAllLandTransfer();
            return landTransfer;
        }
        public async Task<bool> Update(int id, LandTransfer landTransfer)
        {
            var result = await _landTransferRepository.FindBy(a => a.Id == id);
            LandTransfer model = result.FirstOrDefault();
            model.Address = landTransfer.Address;
            model.CopyofOrderDocPath = landTransfer.CopyofOrderDocPath;
            model.DateofTakenOver = landTransfer.DateofTakenOver;
            model.DivisionId = landTransfer.DivisionId;
            model.FileName = landTransfer.FileName;
            model.HandedOverByNameDesingnation = landTransfer.HandedOverByNameDesingnation;
            model.HandedOverDate = landTransfer.HandedOverDate;
            model.HandedOverDepartmentId = landTransfer.HandedOverDepartmentId;
            model.KhasraNo = landTransfer.KhasraNo;
            model.OrderNo = landTransfer.OrderNo;
            model.Remarks = landTransfer.Remarks;
            model.TakenOverByNameDesingnation = landTransfer.TakenOverByNameDesingnation;
            model.TakenOverDepartmentId = landTransfer.TakenOverDepartmentId;
            model.TransferorderIssueAuthority = landTransfer.TransferorderIssueAuthority;
            model.VillageId = landTransfer.VillageId;
            model.ZoneId = landTransfer.ZoneId;
            model.IsActive = landTransfer.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _landTransferRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(LandTransfer landTransfer)
        {
            landTransfer.CreatedBy = 1;
            landTransfer.CreatedDate = DateTime.Now;
            _landTransferRepository.Add(landTransfer);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<LandTransfer>> GetPagedLandTransfer(LandTransferSearchDto model)
        {
            return await _landTransferRepository.GetPagedLandTransfer(model);
        }
    }
}
