﻿using Dto.Master;
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
            model.UniqueId = dto.UniqueId;
            model.ZoneId = dto.ZoneId;
            model.Zone = dto.Zone;
            model.DepartmentId = dto.DepartmentId;
            model.Department = dto.Department;
            model.DivisionId = dto.DivisionId;
            model.Division = dto.Division;
            model.BoundaryWall = dto.BoundaryWall.ToLower() == "y" || dto.BoundaryWall.ToLower() == "yes" ? "Yes" : "No";
            model.Fencing = dto.Fencing.ToLower() =="y" || dto.Fencing.ToLower() == "yes" ? "Yes":"No";
            model.Ddaboard = dto.Ddaboard.ToLower() == "y" || dto.Fencing.ToLower() == "yes" ? "Yes" : "No";
            model.ScurityGuard = dto.ScurityGuard.ToLower() == "y" || dto.Fencing.ToLower() == "yes" ? "Yes" : "No";
            model.PrimaryListId = dto.PrimaryListId;
            model.PrimaryList = dto.PrimaryList;
            model.Location = dto.Location;
            model.Longitude = dto.Longitude;
            model.Latitude = dto.Latitude;         
            model.Flag = "M";    
            model.Location = dto.Location;
            model.IsExistanceEncroachment = dto.CertifiedPlot.ToLower() == "y" || dto.CertifiedPlot.ToLower() == "yes" ? "Yes" : "No";
            model.PerEncroached = dto.percentageEncroached; 
            model.AreaEncroached = dto.AreaEncroached;
            model.IsActionInitiated = dto.IsActionInitiated;         
            model.Remarks = dto.Remarks;
            model.CreatedBy = dto.createdby;
            model.CreatedDate = DateTime.Now;
            model.IsActive = 1;
            _insertVacantLandImagesRepository.Add(model);
            var result = await _unitOfWork.CommitAsync() > 0;
            dto.Id = model.Id;
            dto.createdby = model.CreatedBy;
            return result;
        }

        public async Task<bool> SaveVacantlandlistimage(vacantlandlistimage vacantlandlistimage)
        {
            vacantlandlistimage.CreatedDate = DateTime.Now;
            return await _insertVacantLandImagesRepository.SaveVacantlandlistimage(vacantlandlistimage);
        }
    }
}
