using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class InsertVacantLandImagesService : EntityService<Vacantlandimage>, IInsertVacantLandImagesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInsertVacantLandImagesRepository _insertVacantLandImagesRepository;

        public InsertVacantLandImagesService(IUnitOfWork unitOfWork, IInsertVacantLandImagesRepository insertVacantLandImagesRepository) : 
            base(unitOfWork, insertVacantLandImagesRepository)
        {
            _unitOfWork = unitOfWork;
            _insertVacantLandImagesRepository = insertVacantLandImagesRepository;
        }

        public async Task<bool> Create(ApiInsertVacantLandImageDto dto)
        {
            Vacantlandimage model = new Vacantlandimage();
            model.ZoneId = dto.ZoneId;
            model.Zone = dto.Zone;
            model.DepartmentId = dto.DepartmentId;
            model.Department = dto.Department;
            model.DivisionId = dto.DivisionId;
            model.Division = dto.Division;
            model.PrimaryListId = dto.PrimaryListId;
            model.PrimaryList = dto.PrimaryList;
            model.Location = dto.Location;
            model.ImagePath = dto.ImagePath;
            model.Longitude = dto.Longitude;
            model.Latitude = dto.Latitude;
            model.SrNoInPrimaryList = dto.SrNoInPrimaryList;
            model.Flag = dto.Flag;
            model.Mobile = dto.Mobile;
            model.CheckingPoint = dto.CheckingPoint;
            model.BoundaryWall = dto.BoundaryWall;
            model.Fencing = dto.Fencing;
            model.Ddaboard = dto.Ddaboard;
            model.ScurityGuard = dto.ScurityGuard;
            model.UniqueId = dto.UniqueId;
            model.IsExistanceEncroachment = dto.IsExistanceEncroachment;
            model.EncroachmentDetails = dto.EncroachmentDetails;
            model.IsEncroached = dto.IsEncroached;
            model.PerEncroached = dto.PerEncroached;
            model.AreaEncroached = dto.AreaEncroached;
            model.IsActionInitiated = dto.IsActionInitiated;
            model.Remarks = dto.Remarks;
            model.CreatedBy = dto.CreatedBy;
            model.CreatedDate = DateTime.Now;
            _insertVacantLandImagesRepository.Add(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
