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
    public class LandTransferService : EntityService<Landtransfer>, ILandTransferService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILandTransferRepository _landTransferRepository;
        public LandTransferService(IUnitOfWork unitOfWork, ILandTransferRepository landTransferRepository)
        : base(unitOfWork, landTransferRepository)
        {
            _unitOfWork = unitOfWork;
            _landTransferRepository = landTransferRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _landTransferRepository.FindBy(a => a.Id == id);
            Landtransfer model = form.FirstOrDefault();
            model.IsActive = 0;
            _landTransferRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Landtransfer> FetchSingleResult(int id)
        {
            var result = await _landTransferRepository.FindBy(a => a.Id == id);
            Landtransfer model = result.FirstOrDefault();
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

        public async Task<List<Landtransfer>> GetLandTransferUsingRepo()
        {
            List<Landtransfer> Landtransfer = await _landTransferRepository.GetAllLandtransfer();
            return Landtransfer;
        }
        public async Task<bool> Update(int id, Landtransfer Landtransfer)
        {
            var result = await _landTransferRepository.FindBy(a => a.Id == id);
            Landtransfer model = result.FirstOrDefault();
            model.Address = Landtransfer.Address;
            model.CopyofOrderDocPath = Landtransfer.CopyofOrderDocPath;
            model.DateofTakenOver = Landtransfer.DateofTakenOver;
            model.DivisionId = Landtransfer.DivisionId;
            model.HandedOverByNameDesingnation = Landtransfer.HandedOverByNameDesingnation;
            model.HandedOverDate = Landtransfer.HandedOverDate;
            model.HandedOverDepartmentId = Landtransfer.HandedOverDepartmentId;
            model.KhasraNo = Landtransfer.KhasraNo;
            model.OrderNo = Landtransfer.OrderNo;
            model.Remarks = Landtransfer.Remarks;
            model.TakenOverByNameDesingnation = Landtransfer.TakenOverByNameDesingnation;
            model.TakenOverDepartmentId = Landtransfer.TakenOverDepartmentId;
            model.TransferorderIssueAuthority = Landtransfer.TransferorderIssueAuthority;
            model.LocalityId = Landtransfer.LocalityId;
            model.ZoneId = Landtransfer.ZoneId;
            model.IsActive = Landtransfer.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _landTransferRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Landtransfer Landtransfer)
        {
            Landtransfer.CreatedBy = 1;
            Landtransfer.CreatedDate = DateTime.Now;
            _landTransferRepository.Add(Landtransfer);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Landtransfer>> GetPagedLandTransfer(LandTransferSearchDto model)
        {
            return await _landTransferRepository.GetPagedLandtransfer(model);
        }

        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _landTransferRepository.GetAllLocalityList(divisionId);
        }

        public async Task<List<Landtransfer>> GetHistoryDetails(string khasraNo)
        {
            return await _landTransferRepository.GetHistoryDetails(khasraNo);
        }

        public async Task<List<Landtransfer>> GetAllLandTransfer()
        {
            return await _landTransferRepository.GetAllLandTransfer();
        }
       public async Task<List<Landtransfer>> GetLandTransferReportData(int department, int zone, int division, int primaryListNo)
        {
            return await _landTransferRepository.GetLandTransferReportData(department, zone, division, primaryListNo);
        }

        public async Task<List<Landtransfer>> GetLandTransferReportDepartmentwise(int handedover)
        {
            return await _landTransferRepository.GetLandTransferReportDepartmentwise(handedover);
        }


    }
}
