
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

namespace Libraries.Service.ApplicationService
{
   public class OnlinecomplaintApprovalService : EntityService<Onlinecomplaint>, IOnlinecomplaintApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOnlinecomplaintApprovalRepository _onlinecomplaintApprovalRepository;

        public OnlinecomplaintApprovalService(IUnitOfWork unitOfWork, IOnlinecomplaintApprovalRepository onlinecomplaintApprovalRepository)
       : base(unitOfWork, onlinecomplaintApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _onlinecomplaintApprovalRepository = onlinecomplaintApprovalRepository;
        }


        public async Task<Onlinecomplaint> FetchSingleResult(int id)
        {
            var result = await _onlinecomplaintApprovalRepository.FindBy(a => a.Id == id);
            Onlinecomplaint model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<ComplaintType>> GetAllComplaintType()
        {
            List<ComplaintType> ComplaintList = await _onlinecomplaintApprovalRepository.GetAllComplaintType();
            return ComplaintList;
        }

        public async Task<List<Location>> GetAllLocation()
        {

            return await _onlinecomplaintApprovalRepository.GetAllLocation();
        }


      


        public async Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintApprovalSearchDto model, int userId)
        {
            return await _onlinecomplaintApprovalRepository.GetPagedOnlinecomplaint(model, userId);
        }



    }
}
