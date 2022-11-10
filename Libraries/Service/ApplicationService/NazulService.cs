using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class NazulService : EntityService<Nazul>, INazulService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INazulRepository _nazulRepository;
        public NazulService(IUnitOfWork unitOfWork, INazulRepository nazulRepository)
      : base(unitOfWork, nazulRepository)
        {
            _unitOfWork = unitOfWork;
            _nazulRepository = nazulRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _nazulRepository.FindBy(a => a.Id == id);
            Nazul model = form.FirstOrDefault();
            model.IsActive = 0;
            _nazulRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Nazul> FetchSingleResult(int id)
        {
            var result = await _nazulRepository.FindBy(a => a.Id == id);
            Nazul model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Nazul>> GetAllNazul()
        {

            return await _nazulRepository.GetAllNazul();
        }
        public async Task<List<Nazul>> GetAllNazulList(NazulSearchDto model)
        {
            return await _nazulRepository.GetAllNazulList(model);
        }


        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _nazulRepository.GetAllVillageList();
            return villageList;
        }

        public async Task<List<Nazul>> GetNazulUsingRepo()
        {
            return await _nazulRepository.GetAllNazul();
        }

        public async Task<bool> Update(int id, Nazul nazul)
        {
            var result = await _nazulRepository.FindBy(a => a.Id == id);
            Nazul model = result.FirstOrDefault();
            model.VillageId = nazul.VillageId;
            //model.JaraiSakani = nazul.JaraiSakani;
            //model.Language = nazul.Language;
            //model.YearOfConsolidation = nazul.YearOfConsolidation;
            //model.YearOfJamabandi = nazul.YearOfJamabandi;
            //model.LastMutationNo = nazul.LastMutationNo;
            model.DocumentName = nazul.DocumentName;
            model.DocumentNameSizra = nazul.DocumentNameSizra;
            model.Bigha = nazul.Bigha;
            model.Biswa = nazul.Biswa;
            model.Biswanshi = nazul.Biswanshi;
            model.DateOfNotification = nazul.DateOfNotification;

            model.IsActive = nazul.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _nazulRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Nazul nazul)
        {
            nazul.CreatedBy = nazul.CreatedBy;
            nazul.CreatedDate = DateTime.Now;

            _nazulRepository.Add(nazul);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<PagedResult<Nazul>> GetPagedNazul(NazulSearchDto model)
        {
            return await _nazulRepository.GetPagedNazul(model);
        }
        public async Task<PagedResult<Nazul>> GetNazulReportData(NazulVillageReportSearchDto nazulVillageReportSearchDto)
        {
            return await _nazulRepository.GetNazulReportData(nazulVillageReportSearchDto);
        }






    }
}
