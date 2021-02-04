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
    public class DmsFileUploadService : EntityService<Dmsfileupload>, IDmsFileUploadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDmsFileUploadRepository _dmsFileUploadRepository;

        public DmsFileUploadService(IUnitOfWork unitOfWork, IDmsFileUploadRepository dmsFileUploadRepository)
        : base(unitOfWork, dmsFileUploadRepository)
        {
            _unitOfWork = unitOfWork;
            _dmsFileUploadRepository = dmsFileUploadRepository;
        }

        public async Task<List<Department>> GetDepartmentList()
        {
            return await _dmsFileUploadRepository.GetDepartmentList();
        }

        public async Task<List<Propertyregistration>> GetKhasraNoList()
        {
            return await _dmsFileUploadRepository.GetKhasraNoList();
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            return await _dmsFileUploadRepository.GetLocalityList();
        }

        public async Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model)
        {
            return await _dmsFileUploadRepository.GetPagedDMSFileUploadList(model);
        }

        public async Task<bool> Create(Dmsfileupload dmsfileupload)
        {
            try
            {
                Dmsfileupload model = new Dmsfileupload();
                model.FileNo = dmsfileupload.FileNo;
                model.IsFileBulkUpload = dmsfileupload.IsFileBulkUpload;
                model.AlloteeName = dmsfileupload.AlloteeName;
                model.DepartmentId = dmsfileupload.DepartmentId;
                model.LocalityId = dmsfileupload.LocalityId;
                model.KhasraNoId = dmsfileupload.KhasraNoId;
                model.PropertyNoAddress = dmsfileupload.PropertyNoAddress;
                model.AlmirahNo = dmsfileupload.AlmirahNo;
                model.Title = dmsfileupload.Title;
                model.FileName = dmsfileupload.FileName;
                model.FilePath = dmsfileupload.FilePath;
                model.IsActive = 1;
                model.CreatedDate = DateTime.Now;
                _dmsFileUploadRepository.Add(model);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }

        public async Task<Dmsfileupload> FetchSingleResult(int id)
        {
            return await _dmsFileUploadRepository.FetchSingleResult(id);
        }

        public async Task<bool> Update(int id, Dmsfileupload dmsfileupload)
        {
            var result = await _dmsFileUploadRepository.FindBy(a => a.Id == id);
            Dmsfileupload model = result.FirstOrDefault();
            model.FileNo = dmsfileupload.FileNo;
            model.IsFileBulkUpload = dmsfileupload.IsFileBulkUpload;
            model.AlloteeName = dmsfileupload.AlloteeName;
            model.DepartmentId = dmsfileupload.DepartmentId;
            model.LocalityId = dmsfileupload.LocalityId;
            model.KhasraNoId = dmsfileupload.KhasraNoId;
            model.PropertyNoAddress = dmsfileupload.PropertyNoAddress;
            model.AlmirahNo = dmsfileupload.AlmirahNo;
            model.Title = dmsfileupload.Title;
            model.FileName = dmsfileupload.FileName;
            model.FilePath = dmsfileupload.FilePath;
            model.IsActive = dmsfileupload.IsActive;
            model.ModifiedDate = DateTime.Now;
            _dmsFileUploadRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var result = await _dmsFileUploadRepository.FindBy(a => a.Id == id);
            Dmsfileupload model = result.FirstOrDefault();
            model.IsActive = 0;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = userId;
            _dmsFileUploadRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public int GetLocalityByName(string name)
        {
            return _dmsFileUploadRepository.GetLocalityByName(name);
        }

        public int GetKhasraByName(string name)
        {
            return _dmsFileUploadRepository.GetKhasraByName(name);
        }

        public async Task<bool> CheckUniqueName(string fileNo)
        {
            return await _dmsFileUploadRepository.Any(fileNo);
        }

        public async Task<PagedResult<Dmsfileupload>> GetPagedDMSRetriveFileReport(DMSRetriveFileSearchDto model)
        {
            return await _dmsFileUploadRepository.GetPagedDMSRetriveFileReport(model);
        }

        public async Task<Dmsfileright> GetDMSUserRights(int userId)
        {
            return await _dmsFileUploadRepository.GetDMSUserRights(userId);
        }
    }
}
