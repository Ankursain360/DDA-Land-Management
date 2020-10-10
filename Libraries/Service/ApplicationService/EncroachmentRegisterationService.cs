using Dto.Search;
using Libraries.Model.Common;
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
    public class EncroachmentRegisterationService:EntityService<EncroachmentRegisteration>, IEncroachmentRegisterationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncroachmentRegisterationRepository _encroachmentRegisterationRepository;
        public EncroachmentRegisterationService(IUnitOfWork unitOfWork, IEncroachmentRegisterationRepository encroachmentRegisterationRepository)
      : base(unitOfWork, encroachmentRegisterationRepository)
        {
            _unitOfWork = unitOfWork;
            _encroachmentRegisterationRepository = encroachmentRegisterationRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _encroachmentRegisterationRepository.FindBy(a => a.Id == id);
            EncroachmentRegisteration model = form.FirstOrDefault();
            model.IsActive = 0;
            _encroachmentRegisterationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<EncroachmentRegisteration> FetchSingleResult(int id)
        {
            var result = await _encroachmentRegisterationRepository.FindBy(a => a.Id == id);
            EncroachmentRegisteration model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            return await _encroachmentRegisterationRepository.GetAllDepartment();
        }

        public async Task<List<Division>> GetAllDivisionList(int zone)
        {
            return await _encroachmentRegisterationRepository.GetAllDivision(zone);
        }

        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        {
            return await _encroachmentRegisterationRepository.GetAllEncroachmentRegisteration();
        }

        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _encroachmentRegisterationRepository.GetAllLocalityList(divisionId);
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _encroachmentRegisterationRepository.GetAllZone(departmentId);
        }

        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedLandTransfer(EncroachmentRegisterationDto model)
        {
            return await _encroachmentRegisterationRepository.GetPagedEncroachmentRegisteration(model);
        }

        public async Task<bool> Update(int id, EncroachmentRegisteration encroachmentRegisteration)
        {
            var result = await _encroachmentRegisterationRepository.FindBy(a => a.Id == id);
            EncroachmentRegisteration model = result.FirstOrDefault();
            model.Area = encroachmentRegisteration.Area;
            model.DepartmentId = encroachmentRegisteration.DepartmentId;
            model.DivisionId = encroachmentRegisteration.DivisionId;
            model.EncrochmentDate = encroachmentRegisteration.EncrochmentDate;
            model.IsPossession = encroachmentRegisteration.IsPossession;
            model.KhasraNo = encroachmentRegisteration.KhasraNo;
            model.LocalityId = encroachmentRegisteration.LocalityId;
            model.OtherDepartment = encroachmentRegisteration.OtherDepartment;
            model.PoliceStation = encroachmentRegisteration.PoliceStation;
            model.PossessionType = encroachmentRegisteration.PossessionType;
            model.Remarks = encroachmentRegisteration.Remarks;
            model.SecurityGuardOnDuty = encroachmentRegisteration.SecurityGuardOnDuty;
            model.StatusOfLand = encroachmentRegisteration.StatusOfLand;
            model.ZoneId = encroachmentRegisteration.ZoneId;
            model.FirfilePath = encroachmentRegisteration.FirfilePath != null ? encroachmentRegisteration.FirfilePath : model.FirfilePath;
            model.PhotoFilePath = encroachmentRegisteration.PhotoFilePath != null ? encroachmentRegisteration.PhotoFilePath : model.PhotoFilePath;
            model.LocationMapFilePath = encroachmentRegisteration.LocationMapFilePath != null ? encroachmentRegisteration.LocationMapFilePath : model.LocationMapFilePath;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _encroachmentRegisterationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(EncroachmentRegisteration encroachmentRegisteration)
        {
            encroachmentRegisteration.CreatedBy = 1;
            encroachmentRegisteration.CreatedDate = DateTime.Now;
            _encroachmentRegisterationRepository.Add(encroachmentRegisteration);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
