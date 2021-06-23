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

        public async Task<List<ApiSaveDoor2DoorSurveyDto>> GetSurveyDetails(ApiSaveDoor2DoorSurveyDto dto)
        {
            List<ApiSaveDoor2DoorSurveyDto> listData = new List<ApiSaveDoor2DoorSurveyDto>();

            var Data = await _dbContext.Doortodoorsurvey
                                  .Include(a => a.PresentUseNavigation)
                                  .Where(a => a.IsActive == 1 && a.Id == dto.Id)
                                  .ToListAsync();
            if (Data != null)
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
    }
}
