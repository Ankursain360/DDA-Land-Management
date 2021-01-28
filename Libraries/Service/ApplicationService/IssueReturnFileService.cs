using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
   public class IssueReturnFileService : EntityService<Issuereturnfile>, IIssueReturnFileService
   {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIssueReturnFileRepository _issueReturnFileRepository;

        public IssueReturnFileService(IUnitOfWork unitOfWork, IIssueReturnFileRepository issueReturnFileRepository)
        : base(unitOfWork, issueReturnFileRepository)
        {
            _unitOfWork = unitOfWork;
            _issueReturnFileRepository = issueReturnFileRepository;
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
    }
}
