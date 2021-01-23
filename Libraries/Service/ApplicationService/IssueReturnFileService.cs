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
        public async Task<List<Datastoragedetails>> GetFileNoList()
        {
            List<Datastoragedetails> fileNoList = await _issueReturnFileRepository.GetFileNoList();
            return fileNoList;
        }
   }
}
