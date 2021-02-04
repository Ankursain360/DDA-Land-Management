//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Service.ApplicationService
//{
//    class BranchService
//    {
//    }
//}
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
using Dto.Master;
using AutoMapper;

namespace Libraries.Service.ApplicationService
{

    public class BranchService : EntityService<Branch>, IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        public BranchService(IUnitOfWork unitOfWork,
            IBranchRepository branchRepository,
            IMapper mapper)
        : base(unitOfWork, branchRepository)
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
            _mapper = mapper;
        }
        public async Task<List<Branch>> GetAllDetails()
        {
            return await _branchRepository.GetAllDetails();
        }

        //public async Task<List<ZoneDto>> GetBranch()
        //{
        //    var zones = await _branchRepository.FindBy(a => a.IsActive == 1);
        //    var result = _mapper.Map<List<ZoneDto>>(zones);
        //    return result;
        
        public async Task<List<Branch>> GetBranchUsingRepo()
        {
            return await _branchRepository.GetAllDetails();
        }
        //}

        public async Task<List<Department>> GetDropDownList()
        {
            List<Department> departmentList = await _branchRepository.GetDepartmentList();
            return departmentList;
        }
        public async Task<Branch> FetchSingleResult(int id)
        {
            var result = await _branchRepository.FindBy(a => a.Id == id);
            Branch model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Update(int id, Branch branch)
        {
            var result = await _branchRepository.FindBy(a => a.Id == id);
            Branch model = result.FirstOrDefault();
            model.DepartmentId = branch.DepartmentId;
            model.Name = branch.Name;
            model.Code = branch.Code;
            model.IsActive = branch.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _branchRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Branch branch)
        {

            branch.CreatedBy = 1;
            branch.CreatedDate = DateTime.Now;
            _branchRepository.Add(branch);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string branch)
        {
            bool result = await _branchRepository.Any(id, branch);
            return result;
        }
        public async Task<bool> CheckUniqueCode(int id, string code)
        {
            bool result = await _branchRepository.anyCode(id, code);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _branchRepository.FindBy(a => a.Id == id);
            Branch model = form.FirstOrDefault();
            model.IsActive = 0;
            _branchRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Branch>> GetPagedBranch(BranchSearchDto model)
        {
            return await _branchRepository.GetPagedBranch(model);
        }

        public async Task<List<BranchDto>> GetBranch()
        {
            var branches = await _branchRepository.FindBy(a => a.IsActive == 1);
            var result = _mapper.Map<List<BranchDto>>(branches);
            return result;
        }
        public async Task<List<BranchDto>> GetGetBranchList(int departmentId)
        {
            var branches = await _branchRepository.GetGetBranchList(departmentId);
            var result = _mapper.Map<List<BranchDto>>(branches);
            return result;
        }
    }
}

