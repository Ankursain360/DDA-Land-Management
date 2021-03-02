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
    public class Newlandannexure1Service : EntityService<Newlandannexure1>, INewlandannexure1Service
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandannexure1Repository _newlandannexure1Repository;


        public Newlandannexure1Service(IUnitOfWork unitOfWork, INewlandannexure1Repository newlandannexure1Repository) : base(unitOfWork, newlandannexure1Repository)
        {
            _unitOfWork = unitOfWork;
            _newlandannexure1Repository = newlandannexure1Repository;
        }
        public async Task<PagedResult<Newlandannexure1>> GetPagedNewlandannexure1(Newlandannexure1SearchDto model)
        {
            return await _newlandannexure1Repository.GetPagedNewlandannexure1(model);
        }
        public async Task<List<Newlandannexure1>> GetAllNewlandannexure1()
        {
            return await _newlandannexure1Repository.GetAllNewlandannexure1();
        }
        public async Task<List<Muncipality>> GetAllMunicipality()
        {
            List<Muncipality> list = await _newlandannexure1Repository.GetAllMunicipality();
            return list;
        }
        public async Task<List<District>> GetAllDistrict()
        {
            List<District> list = await _newlandannexure1Repository.GetAllDistrict();
            return list;
        }
       
        public async Task<bool> Create(Newlandannexure1 Annexure1)
        {
            Annexure1.CreatedBy = Annexure1.CreatedBy;
            Annexure1.CreatedDate = DateTime.Now;
            Annexure1.IsActive = 1;

            _newlandannexure1Repository.Add(Annexure1);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Newlandannexure1> FetchSingleResult(int id)
        {
            var result = await _newlandannexure1Repository.FindBy(a => a.Id == id);
            Newlandannexure1 model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Newlandannexure1 Annexure1)
        {
            var result = await _newlandannexure1Repository.FindBy(a => a.Id == id);
            Newlandannexure1 model = result.FirstOrDefault();

            model.VillageName = Annexure1.VillageName;
            model.Address = Annexure1.Address;
            model.TalukName = Annexure1.TalukName;
            model.MunicipalityId = Annexure1.MunicipalityId;
            model.DistrictId = Annexure1.DistrictId;
            model.AreaUnit = Annexure1.AreaUnit;
            model.Area = Annexure1.Area;
            model.AreaAcquiredEast = Annexure1.AreaAcquiredEast;
            model.AreaAcquiredWest = Annexure1.AreaAcquiredWest;
            model.AreaAcquiredNorth = Annexure1.AreaAcquiredNorth;
            model.AreaAcquiredSouth = Annexure1.AreaAcquiredSouth;
            model.AgriculturalLandArea = Annexure1.AgriculturalLandArea;
            model.Reasons = Annexure1.Reasons;
            model.BuildingNo = Annexure1.BuildingNo;
            model.BuildingDesc = Annexure1.BuildingDesc;
            model.TanksNo = Annexure1.TanksNo;
            model.TanksDesc = Annexure1.TanksDesc;
            model.WellsNo = Annexure1.WellsNo;
            model.WellsDesc = Annexure1.WellsDesc;
            model.TreesNo = Annexure1.TreesNo;
            model.TreesDesc = Annexure1.TreesDesc;
            model.ReligiousBuildingNo = Annexure1.ReligiousBuildingNo;
            model.ReligiousBuildingDesc = Annexure1.ReligiousBuildingDesc;
            model.TombNo = Annexure1.TombNo;
            model.TombDesc = Annexure1.TombDesc;
            model.OthersNo = Annexure1.OthersNo;
            model.OthersDesc = Annexure1.OthersDesc;
            model.IsActive = Annexure1.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = Annexure1.ModifiedBy;
            _newlandannexure1Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _newlandannexure1Repository.FindBy(a => a.Id == id);
            Newlandannexure1 model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandannexure1Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        //********* rpt ! Khasra Details **********

        public async Task<bool> SaveKhasraRpt(Newlandannexure1khasrarpt Khasra)
        {
            Khasra.CreatedBy = Khasra.CreatedBy;
            Khasra.CreatedDate = DateTime.Now;
            Khasra.IsActive = 1;
            return await _newlandannexure1Repository.SaveKhasraRpt(Khasra);
        }
        public async Task<List<Newlandannexure1khasrarpt>> GetAllKhasraRpt(int id)
        {
            return await _newlandannexure1Repository.GetAllKhasraRpt(id);
        }
        public async Task<bool> DeleteKhasraRpt(int Id)
        {
            return await _newlandannexure1Repository.DeleteKhasraRpt(Id);
        }
    }
}
