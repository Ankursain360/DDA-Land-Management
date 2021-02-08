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
    
    public class TehsilService : EntityService<Tehsil>, ITehsilService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITehsilRepository _tehsilRepository;

        public TehsilService(IUnitOfWork unitOfWork, ITehsilRepository tehsilRepository)
        : base(unitOfWork, tehsilRepository)
        {
            _unitOfWork = unitOfWork;
            _tehsilRepository = tehsilRepository;
        }

        public async Task<List<Tehsil>> GetAllTehsil()
        {
            return await _tehsilRepository.GetAllTehsil();
        }

        public async Task<List<Tehsil>> GetTehsilUsingRepo()
        {
            return await _tehsilRepository.GetTehsil();//1
        }

        public async Task<Tehsil> FetchSingleResult(int id)
        {
            var result = await _tehsilRepository.FindBy(a => a.Id == id);
            Tehsil model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Tehsil tehsil)
        {
            var result = await _tehsilRepository.FindBy(a => a.Id == id);
            Tehsil model = result.FirstOrDefault();
            model.Name = tehsil.Name;
            //model.SchemeId = proposaldetails.SchemeId;
           // model.Name = proposaldetails.Name;
            //model.RequiredAgency = proposaldetails.RequiredAgency;

           // model.ProposalFileNo = proposaldetails.ProposalFileNo;
           //model.Bigha = proposaldetails.Bigha;
           // model.Biswa = proposaldetails.Biswa;
           // model.Biswanshi = proposaldetails.Biswanshi;
           // model.Description = proposaldetails.Description;
           // model.ProposalDate = proposaldetails.ProposalDate;
            model.IsActive = tehsil.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _tehsilRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Tehsil tehsil)
        {

            tehsil.CreatedBy = 1;
            tehsil.CreatedDate = DateTime.Now;
            _tehsilRepository.Add(tehsil);
            return await _unitOfWork.CommitAsync() > 0;
        }
        //public async Task<List<Scheme>> GetAllScheme()
        //{
        //    List<Scheme> schemeList = await _proposaldetailsRepository.GetAllScheme();
        //    return schemeList;
        //}

        public async Task<bool> CheckUniqueName(int id, string tehsil)
        {
            bool result = await _tehsilRepository.Any(id, tehsil);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _tehsilRepository.FindBy(a => a.Id == id);
            Tehsil model = form.FirstOrDefault();
            model.IsActive = 0;
            _tehsilRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Tehsil>> GetPagedTehsil(TehsilSearchDto model)
        {
            return await _tehsilRepository.GetPagedTehsil(model);
        }

    }
}
