using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class LawyerService : EntityService<Lawyer>, ILawyerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILawyerRepository _lawyerRepository;
        protected readonly DataContext _dbContext;


        public LawyerService(IUnitOfWork unitOfWork, ILawyerRepository lawyerRepository, DataContext dbContext)
       : base(unitOfWork, lawyerRepository)
        {
            _unitOfWork = unitOfWork;
            _lawyerRepository = lawyerRepository;
            _dbContext = dbContext;
        }

        public async Task<List<Lawyer>> GetAllLawyer()
        {
            return await _lawyerRepository.GetAll();
        }

        public async Task<List<Lawyer>> GetLawyerUsingReport()
        {
            return await _lawyerRepository.GetLawyer();
        }

        public async Task<bool> Update(int id, Lawyer lawyer)
        {
            var result = await _lawyerRepository.FindBy(a => a.Id == id);
            Lawyer model = result.FirstOrDefault();
            model.Type = lawyer.Type;
            model.CourtId = lawyer.CourtId;
            model.ChamberAddress = lawyer.ChamberAddress;
            model.CourtPhoneNo = lawyer.CourtPhoneNo;
            model.Name = lawyer.Name;
            model.PanNo = lawyer.PanNo;
            model.PhoneNo = lawyer.PhoneNo;
            model.ValidFrom = lawyer.ValidFrom;
            model.ValidTo = lawyer.ValidTo;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = lawyer.IsActive;
            model.ModifiedBy = 1;
            _lawyerRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, Lawyer lawyer)
        {
            var result = _dbContext.Lawyer.Any(t => t.Id != id && t.Name == lawyer.Name);
            return result;
        }

        public async Task<Lawyer> FetchSingleResult(int id)
        {
            var result = await _lawyerRepository.FindBy(a => a.Id == id);
            Lawyer model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Lawyer lawyer)
        {

            lawyer.CreatedBy = 1;
            lawyer.CreatedDate = DateTime.Now;
            _lawyerRepository.Add(lawyer);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string lawyer)
        {
            bool result = await _lawyerRepository.Any(id, lawyer);
            
            return result;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _lawyerRepository.FindBy(a => a.Id == id);
            Lawyer model = form.FirstOrDefault();
            model.IsActive = 0;
            _lawyerRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Lawyer>> GetPagedLawyer(LawyerSearchDto model)
        {
            return await _lawyerRepository.GetPagedLawyer(model);
        }

    }
}
