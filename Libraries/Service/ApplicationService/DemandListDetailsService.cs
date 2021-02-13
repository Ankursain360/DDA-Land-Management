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
    public class DemandListDetailsService : EntityService<Demandlistdetails>, IDemandListDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemandListDetailsRepository _demandListDetailsRepository;

        public DemandListDetailsService(IUnitOfWork unitOfWork, IDemandListDetailsRepository demandListDetailsRepository)
        : base(unitOfWork, demandListDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _demandListDetailsRepository = demandListDetailsRepository;
        }

        public async Task<List<Department>> GetDepartmentList()
        {
            return await _demandListDetailsRepository.GetDepartmentList();
        }

        public async Task<List<Propertyregistration>> GetKhasraNoList()
        {
            return await _demandListDetailsRepository.GetKhasraNoList();
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            return await _demandListDetailsRepository.GetLocalityList();
        }

        public async Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model)
        {
            return await _demandListDetailsRepository.GetPagedDMSFileUploadList(model);
        }

        //public async Task<bool> Create(Dmsfileupload dmsfileupload)
        //{
        //    try
        //    {
        //        Dmsfileupload model = new Dmsfileupload();
        //        model.FileNo = dmsfileupload.FileNo;
        //        model.IsFileBulkUpload = dmsfileupload.IsFileBulkUpload;
        //        model.AlloteeName = dmsfileupload.AlloteeName;
        //        model.DepartmentId = dmsfileupload.DepartmentId;
        //        model.LocalityId = dmsfileupload.LocalityId;
        //        model.KhasraNoId = dmsfileupload.KhasraNoId;
        //        model.PropertyNoAddress = dmsfileupload.PropertyNoAddress;
        //        model.AlmirahNo = dmsfileupload.AlmirahNo;
        //        model.Title = dmsfileupload.Title;
        //        model.FileName = dmsfileupload.FileName;
        //        model.FilePath = dmsfileupload.FilePath;
        //        model.IsActive = 1;
        //        model.CreatedDate = DateTime.Now;
        //        _demandListDetailsRepository.Add(model);
        //        return await _unitOfWork.CommitAsync() > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}

        public async Task<Dmsfileupload> FetchSingleResult(int id)
        {
            return await _demandListDetailsRepository.FetchSingleResult(id);
        }

        //public async Task<bool> Update(int id, Dmsfileupload dmsfileupload)
        //{
        //    var result = await _demandListDetailsRepository.FindBy(a => a.Id == id);
        //    Dmsfileupload model = result.FirstOrDefault();
        //    model.FileNo = dmsfileupload.FileNo;
        //    model.IsFileBulkUpload = dmsfileupload.IsFileBulkUpload;
        //    model.AlloteeName = dmsfileupload.AlloteeName;
        //    model.DepartmentId = dmsfileupload.DepartmentId;
        //    model.LocalityId = dmsfileupload.LocalityId;
        //    model.KhasraNoId = dmsfileupload.KhasraNoId;
        //    model.PropertyNoAddress = dmsfileupload.PropertyNoAddress;
        //    model.AlmirahNo = dmsfileupload.AlmirahNo;
        //    model.Title = dmsfileupload.Title;
        //    model.FileName = dmsfileupload.FileName;
        //    model.FilePath = dmsfileupload.FilePath;
        //    model.IsActive = dmsfileupload.IsActive;
        //    model.ModifiedDate = DateTime.Now;
        //    _demandListDetailsRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}

        //public async Task<bool> Delete(int id, int userId)
        //{
        //    var result = await _demandListDetailsRepository.FindBy(a => a.Id == id);
        //    Dmsfileupload model = result.FirstOrDefault();
        //    model.IsActive = 0;
        //    model.ModifiedDate = DateTime.Now;
        //    model.ModifiedBy = userId;
        //    _demandListDetailsRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}

        public int GetLocalityByName(string name)
        {
            return _demandListDetailsRepository.GetLocalityByName(name);
        }

        public int GetKhasraByName(string name)
        {
            return _demandListDetailsRepository.GetKhasraByName(name);
        }

        public async Task<bool> CheckUniqueName(int id, string fileNo)
        {
            return await _demandListDetailsRepository.Any(id, fileNo);
        }

        public async Task<PagedResult<Dmsfileupload>> GetPagedDMSRetriveFileReport(DMSRetriveFileSearchDto model)
        {
            return await _demandListDetailsRepository.GetPagedDMSRetriveFileReport(model);
        }

        public async Task<Dmsfileright> GetDMSUserRights(int userId)
        {
            return await _demandListDetailsRepository.GetDMSUserRights(userId);
        }
    }
}
