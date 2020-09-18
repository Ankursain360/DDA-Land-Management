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
  
    public class DisposallandService : EntityService<Disposalland>, IDisposallandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDisposallandRepository _disposallandRepository;

        public DisposallandService(IUnitOfWork unitOfWork, IDisposallandRepository disposallandRepository)
        : base(unitOfWork, disposallandRepository)
        {
            _unitOfWork = unitOfWork;
            _disposallandRepository = disposallandRepository;
        }

        public async Task<List<Disposalland>> GetAllDisposalland()
        {
            return await _disposallandRepository.GetAllDisposalland();
        }

        public async Task<List<Disposalland>> GetDisposallandUsingRepo()
        {
            return await _disposallandRepository.GetDisposalland();
        }

        public async Task<Disposalland> FetchSingleResult(int id)
        {
            var result = await _disposallandRepository.FindBy(a => a.Id == id);
            Disposalland model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Disposalland disposalland)
        {
            var result = await _disposallandRepository.FindBy(a => a.Id == id);
            Disposalland model = result.FirstOrDefault();

            model.UtilizationtypeId = disposalland.UtilizationtypeId;
            model.VillageId = disposalland.VillageId;
            model.KhasraId = disposalland.KhasraId;


            model.TransferToWhichDept = disposalland.TransferToWhichDept;
            model.AreaDisposed = disposalland.AreaDisposed;
            model.DateOfDisposed = disposalland.DateOfDisposed;
            model.TransferTo = disposalland.TransferTo;
            model.TransferBy = disposalland.TransferBy;



            model.FileNoRefNo = disposalland.FileNoRefNo;
            model.Remarks = disposalland.Remarks;



            model.IsActive = disposalland.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _disposallandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Disposalland disposalland)
        {

            disposalland.CreatedBy = 1;
            disposalland.CreatedDate = DateTime.Now;
            _disposallandRepository.Add(disposalland);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Utilizationtype>> GetAllUtilizationtype()
        {
            List<Utilizationtype> utilizationtypeList = await _disposallandRepository.GetAllUtilizationtype();
            return utilizationtypeList;
        }
        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villageList = await _disposallandRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _disposallandRepository.GetAllKhasra();
            return khasraList;
        }

       

        public async Task<bool> Delete(int id)
        {
            var form = await _disposallandRepository.FindBy(a => a.Id == id);
            Disposalland model = form.FirstOrDefault();
            model.IsActive = 0;
            _disposallandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



    }
}
