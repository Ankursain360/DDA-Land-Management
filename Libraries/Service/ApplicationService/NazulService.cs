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

        

        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villageList = await _nazulRepository.GetAllVillage();
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
            model.JaraiSakni = nazul.JaraiSakni;
            model.Language = nazul.Language;
            model.YearOfConsolidation = nazul.YearOfConsolidation;
            model.YearOfJamabandi = nazul.YearOfJamabandi;
            model.LastMutationNo = nazul.LastMutationNo;
            model.Remarks = nazul.Remarks;

            

            model.IsActive = nazul.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _nazulRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Nazul nazul)
        {
            nazul.CreatedBy = 1;
            nazul.CreatedDate = DateTime.Now;

            _nazulRepository.Add(nazul);
            return await _unitOfWork.CommitAsync() > 0;
        }








    }
}
