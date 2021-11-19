using Dto.Master;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class Door2DoorAPIRepository : GenericRepository<Doortodoorsurvey>, IDoor2DoorAPIRepository
    {
        public Door2DoorAPIRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> DeleteDoorToDoorSurveyIdentityProofs(int id)
        {
            _dbContext.RemoveRange(_dbContext.Doortodoorsurveyidentityproof.Where(x => x.DoorToDoorSurveyId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> DeleteDoorToDoorSurveyPropertyProofs(int id)
        {
            _dbContext.RemoveRange(_dbContext.Doortodoorsurveypropertyproof.Where(x => x.DoorToDoorSurveyId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<ApiSaveDoor2DoorSurveyDto>> GetAllSurveyDetails(ApiGetAllDoor2DoorSurveyParamsDto dto, int adminroleid, string identityDocumentPath, string propertyDocumentPath)
        {
            List<ApiSaveDoor2DoorSurveyDto> listData = new List<ApiSaveDoor2DoorSurveyDto>();


            var Data = await _dbContext.Doortodoorsurvey
                                  .Include(a => a.PresentUseNavigation)
                                  .Include(a => a.CreatedByNavigation)
                                  .Include(a => a.AreaUnitNavigation)
                                  .Include(a => a.NumberOfFloorsNavigation)
                                  .Where(a => a.CreatedBy == (adminroleid == dto.RoleId ? a.CreatedBy : dto.UserId)
                                  && (a.OccupantName.ToUpper().Trim().Contains(dto.OccupantName == "" || dto.OccupantName == null ? a.OccupantName.ToUpper().Trim() : dto.OccupantName.ToUpper().Trim()))
                                  && (a.MobileNo.ToUpper().Trim().Contains(dto.OccupantContactNo == "" || dto.OccupantContactNo == null ? a.MobileNo.ToUpper().Trim() : dto.OccupantContactNo.ToUpper().Trim()))
                                  && (a.PropertyAddress.ToUpper().Trim().Contains(dto.PropertyAddress == "" || dto.PropertyAddress == null ? a.PropertyAddress.ToUpper().Trim() : dto.PropertyAddress.ToUpper().Trim()))
                                  && (a.CreatedDate.Date >= ((dto.FromDate != null && dto.FromDate != "") ? Convert.ToDateTime(dto.FromDate).Date : a.CreatedDate.Date))
                                  && (a.CreatedDate.Date <= ((dto.ToDate != null && dto.ToDate != "") ? Convert.ToDateTime(dto.ToDate).Date : a.CreatedDate.Date))
                                  )
                                  .OrderByDescending(a => a.Id)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    List<string> propertyDocument = new List<string>();
                    List<string> identityDocument = new List<string>();
                    //Fetch identity document
                    var identityDocumentData = await _dbContext.Doortodoorsurveyidentityproof
                                                         .Where(x => x.DoorToDoorSurveyId == Data[i].Id)
                                                         .ToListAsync();
                    if (identityDocumentData != null)
                    {
                        for (int h = 0; h < identityDocumentData.Count; h++)
                        {
                            identityDocument.Add(identityDocumentPath + identityDocumentData[h].OccupantIdentityPrrofFilePath);
                        }
                    }
                    //Fetch Property document
                    var propertyDocumentData = await _dbContext.Doortodoorsurveypropertyproof
                                                        .Where(x => x.DoorToDoorSurveyId == Data[i].Id)
                                                        .ToListAsync();
                    if (propertyDocumentData != null)
                    {
                        for (int h = 0; h < propertyDocumentData.Count; h++)
                        {
                            propertyDocument.Add(propertyDocumentPath + propertyDocumentData[h].PropertyFilePath);
                        }
                    }



                    listData.Add(new ApiSaveDoor2DoorSurveyDto()
                    {
                        Id = Data[i].Id,
                        PropertyAddress = Data[i].PropertyAddress,
                        GeoReferencingLattitude = Data[i].GeoReferencingLattitude,
                        Longitude = Data[i].Longitude,
                        PresentUseId = Data[i].PresentUseId,
                        PresentUseName = Data[i].PresentUseNavigation.Name,
                        ApproxPropertyArea = Data[i].ApproxPropertyArea,
                        NumberOfFloors = Data[i].NumberOfFloors,
                        CaelectricityNo = Data[i].CaelectricityNo,
                        IsActive = Data[i].IsActive,
                        KwaterNo = Data[i].KwaterNo,
                        PropertyHouseTaxNo = Data[i].PropertyHouseTaxNo,
                        OccupantName = Data[i].OccupantName,
                        Email = Data[i].Email,
                        Remarks = Data[i].Remarks,
                        MobileNo = Data[i].MobileNo,
                        OccupantIdentityPrrofFilePath = identityDocument,
                        PropertyFilePath = propertyDocument,
                        OccupantAadharNo = Data[i].OccupantAadharNo,
                        VoterIdNo = Data[i].VoterIdNo,
                        CreatedBy = Data[i].CreatedBy,
                        CreatedByName = Data[i].CreatedByNavigation.UserName,
                        CreatedDate = Data[i].CreatedDate,
                        NumberOfFloorsName = Data[i].NumberOfFloorsNavigation.Name,
                        FileNo = Data[i].FileNo,
                        AreaUnitName = Data[i].AreaUnitNavigation.Name
                    });
                }
            }
            return listData;
        }

        public async Task<List<ApiGetPresentUseDto>> GetPresentUseDetails()
        {
            List<ApiGetPresentUseDto> listData = new List<ApiGetPresentUseDto>();

            var Data = await _dbContext.Presentuse
                                  .Where(a => a.IsActive == 1)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new ApiGetPresentUseDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].Name,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }

        public async Task<List<ApiSaveDoor2DoorSurveyDto>> GetSurveyDetails(ApiSaveDoor2DoorSurveyDto dto, string identityDocumentPath, string propertyDocumentPath)
        {
            List<ApiSaveDoor2DoorSurveyDto> listData = new List<ApiSaveDoor2DoorSurveyDto>();
            List<string> propertyDocument = new List<string>();
            List<string> identityDocument = new List<string>();
            var Data = await _dbContext.Doortodoorsurvey
                                  .Include(a => a.PresentUseNavigation)
                                  .Include(a => a.CreatedByNavigation)
                                  .Include(a => a.AreaUnitNavigation)
                                  .Include(a => a.NumberOfFloorsNavigation)
                                  .Include(a => a.Doortodoorsurveyidentityproof)
                                  .Include(a => a.Doortodoorsurveypropertyproof)
                                  .Where(a => a.IsActive == 1 && a.Id == dto.Id)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    //Fetch identity document
                    var identityDocumentData = await _dbContext.Doortodoorsurveyidentityproof
                                                         .Where(x => x.DoorToDoorSurveyId == Data[i].Id)
                                                         .ToListAsync();
                    if (identityDocumentData != null)
                    {
                        for (int h = 0; h < identityDocumentData.Count; h++)
                        {
                            identityDocument.Add(identityDocumentPath + identityDocumentData[h].OccupantIdentityPrrofFilePath);
                        }
                    }
                    //Fetch Property document
                    var propertyDocumentData = await _dbContext.Doortodoorsurveypropertyproof
                                                        .Where(x => x.DoorToDoorSurveyId == Data[i].Id)
                                                        .ToListAsync();
                    if (propertyDocumentData != null)
                    {
                        for (int h = 0; h < propertyDocumentData.Count; h++)
                        {
                            propertyDocument.Add(propertyDocumentPath + propertyDocumentData[h].PropertyFilePath);
                        }
                    }
                    listData.Add(new ApiSaveDoor2DoorSurveyDto()
                    {
                        Id = Data[i].Id,
                        PropertyAddress = Data[i].PropertyAddress,
                        GeoReferencingLattitude = Data[i].GeoReferencingLattitude,
                        Longitude = Data[i].Longitude,
                        PresentUseId = Data[i].PresentUseId,
                        PresentUseName = Data[i].PresentUseNavigation.Name,
                        ApproxPropertyArea = Data[i].ApproxPropertyArea,
                       // NumberOfFloors = Data[i].NumberOfFloors,
                        CaelectricityNo = Data[i].CaelectricityNo,
                        IsActive = Data[i].IsActive,
                        KwaterNo = Data[i].KwaterNo,
                        PropertyHouseTaxNo = Data[i].PropertyHouseTaxNo,
                        OccupantName = Data[i].OccupantName,
                        Email = Data[i].Email,
                        Remarks = Data[i].Remarks,
                        MobileNo = Data[i].MobileNo,
                        OccupantAadharNo = Data[i].OccupantAadharNo,
                        VoterIdNo = Data[i].VoterIdNo,
                        OccupantIdentityPrrofFilePath = identityDocument,
                        PropertyFilePath = propertyDocument,
                        CreatedBy = Data[i].CreatedBy,
                        CreatedByName = Data[i].CreatedByNavigation.UserName,
                        CreatedDate = Data[i].CreatedDate,
                        NumberOfFloorsName=Data[i].NumberOfFloorsNavigation.Name,
                        FileNo=Data[i].FileNo,
                        AreaUnitName=Data[i].AreaUnitNavigation.Name
                    });
                }
            }
            return listData;
        }

        public async Task<bool> SaveDoorToDoorSurveyIdentityProofs(Doortodoorsurveyidentityproof item)
        {
            _dbContext.Doortodoorsurveyidentityproof.Add(item);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> SaveDoorToDoorSurveyPropertyProofs(Doortodoorsurveypropertyproof item)
        {
            _dbContext.Doortodoorsurveypropertyproof.Add(item);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<ApiSurveyUserDetailsDto>> VerifySurveyUserDetailsLogin(ApiSurveyUserLoginDto dto)
        {
            List<ApiSurveyUserDetailsDto> listData = new List<ApiSurveyUserDetailsDto>();

            var Data = await _dbContext.Surveyuserdetail
                                  .Include(a => a.Role)
                                  .Where(a => a.UserName == dto.username && a.Password == dto.password && a.IsActive == 1)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new ApiSurveyUserDetailsDto()
                    {
                        Id = Data[i].Id,
                        FirstName = Data[i].FirstName,
                        LastName = Data[i].LastName,
                        UserName = Data[i].UserName,
                        PhoneNo = Data[i].PhoneNo,
                        RoleName = Data[i].Role.Name,
                        EmailId = Data[i].EmailId,
                        Password = Data[i].Password,
                        RoleId = Data[i].RoleId,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }

        public async Task<List<ApiGetAreaUnitDto>> Getareaunit()
        {
            List<ApiGetAreaUnitDto> listData = new List<ApiGetAreaUnitDto>();

            var Data = await _dbContext.Areaunit
                                  .Where(a => a.IsActive == 1)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new ApiGetAreaUnitDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].Name,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }

        public async Task<List<ApiGetFloorNoDto>> GetFloorNo()
        {
            List<ApiGetFloorNoDto> listData = new List<ApiGetFloorNoDto>();

            var Data = await _dbContext.Floors
                                  .Where(a => a.IsActive == 1)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new ApiGetFloorNoDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].Name,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }
    }
}
