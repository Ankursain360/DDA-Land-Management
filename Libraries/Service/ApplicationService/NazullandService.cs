using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Service.ApplicationService;
using Libraries.Service.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    
    public class NazullandService : EntityService<Nazulland>, INazullandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INazullandRepository _nazullandRepository;

        public NazullandService(IUnitOfWork unitOfWork, INazullandRepository nazullandRepository)
        : base(unitOfWork, nazullandRepository)
        {
            _unitOfWork = unitOfWork;
            _nazullandRepository = nazullandRepository;
        }

        public async Task<List<Nazulland>> GetAllNazulland()
        {
            return await _nazullandRepository.GetAllNazulland();
        }

        public async Task<List<Nazulland>> GetNazullandUsingRepo()
        {
            return await _nazullandRepository.GetNazulland();
        }

        public async Task<Nazulland> FetchSingleResult(int id)
        {
            var result = await _nazullandRepository.FindBy(a => a.Id == id);
            Nazulland model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Nazulland nazulland)
        {
            var result = await _nazullandRepository.FindBy(a => a.Id == id);
            Nazulland model = result.FirstOrDefault();
            //model.Id = nazulland.Id;
            model.KhasraNo = nazulland.KhasraNo;
            model.LandAreaAcquired = nazulland.LandAreaAcquired;
            model.AwardNo = nazulland.AwardNo;
            model.AwardDate = nazulland.AwardDate;
            model.DateOfPossession = nazulland.DateOfPossession;
            model.AreaOfWhichPossessionTakenOver = nazulland.AreaOfWhichPossessionTakenOver;
            model.DateOnWhichPossessionTakenOver = nazulland.DateOnWhichPossessionTakenOver;
            model.SchemeForWhichAcquired = nazulland.SchemeForWhichAcquired;
            model.AmountOfAward = nazulland.AmountOfAward;
            model.AdiCourt = nazulland.AdiCourt;
            model.HighCourt = nazulland.HighCourt;
            model.SupremeCourt = nazulland.SupremeCourt;
            model.CertificateToCorrectnessOfEntry = nazulland.CertificateToCorrectnessOfEntry;
            model.DivisionId = nazulland.DivisionId;
            model.DateOfTransfer = nazulland.DateOfTransfer;
            model.Remarks = nazulland.Remarks;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _nazullandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Nazulland nazulland)
        {

            nazulland.CreatedBy = 1;
            nazulland.CreatedDate = DateTime.Now;
            _nazullandRepository.Add(nazulland);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Division>> GetAllDivision()
        {
            List<Division> divisionList = await _nazullandRepository.GetAllDivision();
            return divisionList;
        }

        //public async Task<bool> CheckUniqueName(int id, string page)
        //{
        //    bool result = await _pageRepository.Any(id, page);
        //    //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
        //    return result;
        //}

        public async Task<bool> Delete(int id)
        {
            var form = await _nazullandRepository.FindBy(a => a.Id == id);
            Nazulland model = form.FirstOrDefault();
            model.IsActive = 0;
            _nazullandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Nazulland>> GetPagedNazulland(NazullandSearchDto model)
        {
            return await _nazullandRepository.GetPagedNazulland(model);
        }

    }
}
