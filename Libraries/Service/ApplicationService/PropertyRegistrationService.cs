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

namespace Libraries.Service.ApplicationService
{

    public class PropertyRegistrationService : EntityService<Propertyregistration>, IPropertyRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyRegistrationRepository _propertyregistrationRepository;

        public PropertyRegistrationService(IUnitOfWork unitOfWork, IPropertyRegistrationRepository propertyregistrationRepository)
        : base(unitOfWork, propertyregistrationRepository)
        {
            _unitOfWork = unitOfWork;
            _propertyregistrationRepository = propertyregistrationRepository;

        }
        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownList()
        {
            List<Classificationofland> ClassificationoflandList = await _propertyregistrationRepository.GetClassificationOfLandDropDownList();
            return ClassificationoflandList;
        }
        public async Task<List<Zone>> GetZoneDropDownList()
        {
            List<Zone> zoneList = await _propertyregistrationRepository.GetZoneDropDownList();
            return zoneList;
        }
        public async Task<List<Locality>> GetLocalityDropDownList()
        {
            List<Locality> LocalityList = await _propertyregistrationRepository.GetLocalityDropDownList();
            return LocalityList;
        }
        public async Task<List<Landuse>> GetLandUseDropDownList()
        {
            List<Landuse> LanduseList = await _propertyregistrationRepository.GetLandUseDropDownList();
            return LanduseList;
        }
        public async Task<List<Disposaltype>> GetDisposalTypeDropDownList()
        {
            List<Disposaltype> DisposaltypeList = await _propertyregistrationRepository.GetDisposalTypeDropDownList();
            return DisposaltypeList;
        }

        public async Task<List<Propertyregistration>> GetAllPropertyregistration()
        {
            return await _propertyregistrationRepository.GetAllPropertyregistration();
        }

        public async Task<Propertyregistration> FetchSingleResult(int id)
        {
            var result = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Propertyregistration propertyregistration)
        {
            var result = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = result.FirstOrDefault();
            //model.ClassificationOfLandId = propertyregistration.ClassificationOfLandId;
            //model.UniqueId = propertyregistration.UniqueId;
            //model.ZoneDivisionId = propertyregistration.ZoneDivisionId;
            //model.LocalityId = propertyregistration.LocalityId;
            //model.KhasraNo = propertyregistration.KhasraNo;
            //model.Boundary = propertyregistration.Boundary;
            //model.BoundaryRemarks = propertyregistration.BoundaryRemarks;
            //model.TotalArea = propertyregistration.TotalArea;
            //model.Encroached = propertyregistration.Encroached;
            //model.Vacant = propertyregistration.Vacant;
            //model.LandUseId = propertyregistration.LandUseId;
            //model.BuiltUp = propertyregistration.BuiltUp;
            //model.BuiltUpRemarks = propertyregistration.BuiltUpRemarks;
            //model.LayoutPlan = propertyregistration.LayoutPlan;
            //model.LitigationStatus = propertyregistration.LitigationStatus;
            //model.LitigationStatusRemarks = propertyregistration.LitigationStatusRemarks;
            //model.GeoReferencing = propertyregistration.GeoReferencing;
            //model.TakenOverName = propertyregistration.TakenOverName;
            //model.TakenOverDate = propertyregistration.TakenOverDate;
            //model.TakenOverComments = propertyregistration.TakenOverComments;
            //model.HandedOverName = propertyregistration.HandedOverName;
            //model.HandedOverDate = propertyregistration.HandedOverDate;
            //model.HandedOverComments = propertyregistration.HandedOverComments;
            //model.DisposalTypeId = propertyregistration.DisposalTypeId;
            //model.DisposalDate = propertyregistration.DisposalDate;
            //model.DisposalComments = propertyregistration.DisposalComments;
            //model.Remarks = propertyregistration.Remarks;
            propertyregistration.ModifiedDate = DateTime.Now;
            propertyregistration.ModifiedBy = 1;
            _propertyregistrationRepository.Edit(propertyregistration);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Propertyregistration propertyregistration)
        {

            propertyregistration.CreatedBy = 1;
            propertyregistration.CreatedDate = DateTime.Now;
            _propertyregistrationRepository.Add(propertyregistration);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = form.FirstOrDefault();
            _propertyregistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

    }
}
