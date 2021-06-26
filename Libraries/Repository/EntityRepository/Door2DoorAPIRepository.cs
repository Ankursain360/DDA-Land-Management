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
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class Door2DoorAPIRepository : GenericRepository<Doortodoorsurvey>, IDoor2DoorAPIRepository
    {
        public Door2DoorAPIRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<ApiSaveDoor2DoorSurveyDto>> GetAllSurveyDetails(ApiSaveDoor2DoorSurveyDto dto, int adminroleid)
        {
            List<ApiSaveDoor2DoorSurveyDto> listData = new List<ApiSaveDoor2DoorSurveyDto>();

            var Data = await _dbContext.Doortodoorsurvey
                                  .Include(a => a.PresentUseNavigation)
                                  .Where(a => a.CreatedBy == (adminroleid == dto.RoleId ? a.CreatedBy : dto.UserId))
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
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
                        OccupantAadharNo = Data[i].OccupantAadharNo,
                        VoterIdNo = Data[i].VoterIdNo,
                        OccupantIdentityPrrofFilePath = Data[i].OccupantIdentityPrrofFilePath,
                        PropertyFilePath = Data[i].PropertyFilePath,
                        CreatedBy = Data[i].CreatedBy
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

        public async Task<List<ApiSaveDoor2DoorSurveyDto>> GetSurveyDetails(ApiSaveDoor2DoorSurveyDto dto)
        {
            List<ApiSaveDoor2DoorSurveyDto> listData = new List<ApiSaveDoor2DoorSurveyDto>();

            var Data = await _dbContext.Doortodoorsurvey
                                  .Include(a => a.PresentUseNavigation)
                                  .Where(a => a.IsActive == 1 && a.Id == dto.Id)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new ApiSaveDoor2DoorSurveyDto()
                    {
                        Id = Data[i].Id,
                        PropertyAddress = Data[i].PropertyAddress,
                        GeoReferencingLattitude = Data[i].GeoReferencingLattitude,
                        Longitude = Data[i].Longitude,
                        PresentUseId = Data[i].PresentUseId,
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
                        OccupantAadharNo = Data[i].OccupantAadharNo,
                        VoterIdNo = Data[i].VoterIdNo,
                        OccupantIdentityPrrofFilePath = Data[i].OccupantIdentityPrrofFilePath,
                        PropertyFilePath = Data[i].PropertyFilePath,
                        CreatedBy = Data[i].CreatedBy
                    });
                }
            }
            //if (Data != null)
            //{
            //    listData.PropertyAddress = Data.PropertyAddress;
            //    listData.GeoReferencingLattitude = Data.GeoReferencingLattitude;
            //    listData.Longitude = Data.Longitude;
            //    listData.PresentUseId = Data.PresentUseId;
            //    listData.ApproxPropertyArea = Data.ApproxPropertyArea;
            //    listData.NumberOfFloors = Data.NumberOfFloors;
            //    listData.CaelectricityNo = Data.CaelectricityNo;
            //    listData.IsActive = Data.IsActive;
            //    listData.KwaterNo = Data.KwaterNo;
            //    listData.PropertyHouseTaxNo = Data.PropertyHouseTaxNo;
            //    listData.OccupantName = Data.OccupantName;
            //    listData.Email = Data.Email;
            //    listData.IsActive = 1;
            //    listData.Remarks = Data.Remarks;
            //    listData.OccupantIdentityPrrofFilePath = Data.OccupantIdentityPrrofFilePath;
            //    listData.PropertyFilePath = Data.PropertyFilePath;
            //    listData.Remarks = Data.Remarks;
            //}

            return listData;
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
    }
}
