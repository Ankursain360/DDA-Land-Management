﻿using Dto.Search;
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
using Libraries.Model;

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
        public async Task<Landtransfer> FetchSingleResultWithPropertyRegistration(int id)
        {
            Landtransfer model =await _landTransferRepository.FetchSingleResultWithPropertyRegistration(id);
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
            model.CopyofOrderDocPath = Landtransfer.CopyofOrder != null ? Landtransfer.CopyofOrderDocPath : model.CopyofOrderDocPath;
            model.ActionTakenReportPath = Landtransfer.ActionTakenReportPath != null ? Landtransfer.ActionTakenReportPath : model.ActionTakenReportPath;
            model.HandedOverFile = Landtransfer.HandedOverFile != null ? Landtransfer.HandedOverFile : model.HandedOverFile;
            model.TakenOverDocument = Landtransfer.TakenOverDocument != null ? Landtransfer.TakenOverDocument : model.TakenOverDocument;
            model.HandedOverDepartmentId = Landtransfer.HandedOverDepartmentId;
            model.HandedOverZoneId = Landtransfer.HandedOverZoneId;
            model.HandedOverDivisionId = Landtransfer.HandedOverDivisionId;
            model.HandedOverByNameDesingnation = Landtransfer.HandedOverByNameDesingnation;
            model.HandedOverDate = Landtransfer.HandedOverDate;
            model.HandedOverEmailId = Landtransfer.HandedOverEmailId;
            model.HandedOverMobileNo = Landtransfer.HandedOverMobileNo;
            model.HandedOverLandLineNo = Landtransfer.HandedOverLandLineNo;
            model.HandedOverCommments = Landtransfer.HandedOverCommments;
            model.OrderNo = Landtransfer.OrderNo;
            model.TransferorderIssueAuthority = Landtransfer.TransferorderIssueAuthority;
            model.TakenOverDepartmentId = Landtransfer.TakenOverDepartmentId;
            model.TakenOverZoneId = Landtransfer.TakenOverZoneId;
            model.TakenOverDivisionId = Landtransfer.TakenOverDivisionId;
            model.TakenOverByNameDesingnation = Landtransfer.TakenOverByNameDesingnation;
            model.TakenOverEmailId = Landtransfer.TakenOverEmailId;
            model.TakenOverMobileNo = Landtransfer.TakenOverMobileNo;
            model.TakenOverLandLineNo = Landtransfer.TakenOverLandLineNo;
            model.TakenOverCommments = Landtransfer.TakenOverCommments;
            model.DateofTakenOver = Landtransfer.DateofTakenOver;
            model.Encroachment = Landtransfer.Encroachment;
            model.EncroachmentStatus = Landtransfer.EncroachmentStatus;
            model.EncroachementArea = Landtransfer.EncroachementArea;
            model.BuildUpInEncroachementArea = Landtransfer.BuildUpInEncroachementArea;
            model.ActionOnEncroachment = Landtransfer.ActionOnEncroachment;
            model.EncroachmentDetails = Landtransfer.EncroachmentDetails;
            model.Remarks = Landtransfer.Remarks;
            model.IsValidate = Landtransfer.IsValidate;
            model.IsActive = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _landTransferRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Landtransfer Landtransfer)
        {
             
            Landtransfer.CreatedDate = DateTime.Now;
            _landTransferRepository.Add(Landtransfer);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Landtransfer>> GetPagedLandTransfer(LandTransferSearchDto model)
        {
            return await _landTransferRepository.GetPagedLandtransfer(model);
        }


        public async Task<PagedResult<Landtransfer>> GetPagedLandtransferReportDeptWise(LandTransferSearchDto model)//added by ishu
        {
            return await _landTransferRepository.GetPagedLandtransferReportDeptWise(model);
        }
        public async Task<PagedResult<Landtransfer>> GetPagedLandTransferReportData(LandTransferReportDivisionLocalitySearchDto model)//added by shalini

        {
            return await _landTransferRepository.GetPagedLandtransferReportData(model);
        }


        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _landTransferRepository.GetAllLocalityList(divisionId);
        }

        public async Task<List<Landtransfer>> GetHistoryDetails(string khasraNo)
        {
            return await _landTransferRepository.GetHistoryDetails(khasraNo);
        }

        public async Task<List<Landtransfer>> GetAllLandTransfer(int propertyRegistrationId)
        {
            return await _landTransferRepository.GetAllLandTransfer(propertyRegistrationId);
        }
        public async Task<List<Landtransfer>> GetAllLandTransfer()
        {
            return await _landTransferRepository.GetAllLandtransfer();
        }
        public async Task<List<Landtransfer>> GetLandTransferReportData(int department, int zone, int division, int locality)
        {
            return await _landTransferRepository.GetLandTransferReportData(department, zone, division, locality);
        }

        //public async Task<List<Landtransfer>> GetLandTransferReportDepartmentwise(int handedover)
        //{
        //    return await _landTransferRepository.GetLandTransferReportDepartmentwise(handedover);
        //}

        //public async Task<List<Landtransfer>> GetLandTransferReportDataKhasraNumberWise(int id)
        //{
        //    return await _landTransferRepository.GetLandTransferReportDataKhasraNumberWise(id);
        //}



        public async Task<PagedResult<Landtransfer>> GetLandTransferReportDataKhasraNumberWise(LandtrasferreportkhasranowiseDto model)//added by ishu
        {
            return await _landTransferRepository.GetLandTransferReportDataKhasraNumberWise(model);
        }

        public async Task<List<Landtransfer>> GetLandTransferReportDataDepartmentWise(int reportType, int departmentId)//added by ishu
        {
            return await _landTransferRepository.GetLandTransferReportDataDepartmentWise(reportType, departmentId);
        }

        public async Task<List<Landtransfer>> GetAllLandTransferList()
        {
            return await _landTransferRepository.GetAllLandTransferList();
        }

        public async Task<List<Landtransfer>> GetLandTransferReportdataHandover(int id)
        {
            return await _landTransferRepository.GetLandTransferReportdataHandover(id);
        }

        // current status of land history :
        public async Task<bool> SaveCurrentstatusoflandhistory(Currentstatusoflandhistory model)
        {
            model.CreatedBy = 1;
            model.CreatedDate = DateTime.Now;
            model.IsActive = 1;
            return await _landTransferRepository.SaveCurrentstatusoflandhistory(model);

        }
        public async Task<List<Currentstatusoflandhistory>> GetCurrentstatusoflandhistory(int landtransferId)
        {
            return await _landTransferRepository.GetCurrentstatusoflandhistory(landtransferId);
        }
        public async Task<PagedResult<Landtransfer>> GetPagedCurrentStatusLandtransfer(LandTransferSearchDto model) //added by ishu
        {
            return await _landTransferRepository.GetPagedCurrentStatusLandtransfer(model);
        }

        public async Task<PagedResult<Propertyregistration>> GetPropertyRegisterationDataForLandTransfer(LandTransferSearchDto model)
        {
            return await _landTransferRepository.GetPropertyRegisterationDataForLandTransfer(model);
        }
        public async Task<PagedResult<Propertyregistration>> GetPropertyRegisterationUnverifiedDataForLandTransfer(LandTransferSearchDto model)
        {
            return await _landTransferRepository.GetPropertyRegisterationUnverifiedDataForLandTransfer(model);
        }

        public async Task<bool> CreateHistory(PropertyRegistrationHistory propertyRegistrationHistory)
        {
            return await _landTransferRepository.CreateHistory(propertyRegistrationHistory);
        }



        public async Task<List<Propertyregistration>> GetAllHandOverTakeOverList()
        {
            return await _landTransferRepository.GetAllHandOverTakeOverList();
        }
        public async Task<List<Propertyregistration>> GetAllPropertyRegisterationDataForLandTransferList(LandTransferSearchDto model)
        {
            return await _landTransferRepository.GetAllPropertyRegisterationDataForLandTransferList(model);
        }
        public async Task<List<Propertyregistration>> GetAllUnverifiedTransferRecordList()
        {
            return await _landTransferRepository.GetAllUnverifiedTransferRecordList();
        }

    }

}