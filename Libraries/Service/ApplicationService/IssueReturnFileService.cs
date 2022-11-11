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
   public class IssueReturnFileService : EntityService<Issuereturnfile>, IIssueReturnFileService
   {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIssueReturnFileRepository _issueReturnFileRepository;
        private readonly IDataStorageRepository _datastoragedetailRepository;
        public IssueReturnFileService(IUnitOfWork unitOfWork, IIssueReturnFileRepository issueReturnFileRepository,
            IDataStorageRepository datastoragedetailRepository)
        : base(unitOfWork, issueReturnFileRepository)
        {
            _unitOfWork = unitOfWork;
            _issueReturnFileRepository = issueReturnFileRepository;
            _datastoragedetailRepository = datastoragedetailRepository;
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _issueReturnFileRepository.GetAllDepartment();
            return departmentList;
        }
        public async Task<List<Branch>> GetAllBranch()
        {
            List<Branch> branchList = await _issueReturnFileRepository.GetAllBranch();
            return branchList;
        }
        public async Task<List<Designation>> GetAllDesignation()
        {
            List<Designation> designationList = await _issueReturnFileRepository.GetAllDesignation();
            return designationList;
        }

        public async Task<List<Datastoragedetails>> GetFileNoList()
        {
            List<Datastoragedetails> fileNoList = await _issueReturnFileRepository.GetFileNoList();
            return fileNoList;
        }
        public async Task<bool> Create(Issuereturnfile model)
        {
        
            model.CreatedDate = DateTime.Now;
            _issueReturnFileRepository.Add(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<PagedResult<Datastoragedetails>> GetPagedIssueReturnFile(IssueReturnFileSearchDto model)
        {
            return await _issueReturnFileRepository.GetPagedIssueReturnFile(model);
        }
        public async Task<List<Datastoragedetails>> GetIssuereturnfile()
        {
            return await _issueReturnFileRepository.GetIssuereturnfile();

        }
        public async Task<List<Datastoragedetails>> GetAllIssueReturnFileList(IssueReturnFileSearchDto model)
        {
            return await _issueReturnFileRepository.GetAllIssueReturnFileList(model);
        }

        public async Task<bool> UpdateIssueFileStatus(int id)
        {
            var form = await _datastoragedetailRepository.FindBy(a => a.Id == id);
            Datastoragedetails model = form.FirstOrDefault();
            model.FileStatus = "Issued";
            _datastoragedetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> UpdateReturnFileStatus(int id)
        {
            var form = await _datastoragedetailRepository.FindBy(a => a.Id == id);
            Datastoragedetails model = form.FirstOrDefault();
            model.FileStatus = "Return";
            _datastoragedetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Issuereturnfile> FetchSingleResult(int id)
        {
            var result = await _issueReturnFileRepository.FindBy(a => a.Id == id);
            Issuereturnfile model = result.FirstOrDefault();
            return model;
        }
        public async Task<Issuereturnfile> FetchSingleReceiptResult(int id)
        {
            Issuereturnfile model = await _issueReturnFileRepository.FetchSingleReceiptResult(id);
            return model;
        }
        public async Task<Issuereturnfile> FetchReturnReceiptResult(int id)
        {
            Issuereturnfile model = await _issueReturnFileRepository.FetchReturnReceiptResult(id);
            return model;
        }
        public async Task<Issuereturnfile> FetchfiletResult(int id) 
        {
            Issuereturnfile model = await _issueReturnFileRepository.FetchfiletResult(id);
            return model;
        }
        public async Task<bool> Update(int id, Issuereturnfile issuereturnfile)
        {

            Issuereturnfile model =  await _issueReturnFileRepository.FetchfiletResult(id);
            model.ReturnedDate = issuereturnfile.ReturnedDate;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = issuereturnfile.ModifiedBy;
            _issueReturnFileRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;


        }
    }
}
