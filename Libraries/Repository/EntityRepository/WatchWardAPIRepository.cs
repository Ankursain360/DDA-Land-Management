
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
    public class WatchWardAPIRepository : GenericRepository<Watchandward>, IWatchWardAPIRepository
    {
        public WatchWardAPIRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<APIGetPrimaryListNoListDto>> GetPrimaryListNoList()
        {
            List<APIGetPrimaryListNoListDto> listData = new List<APIGetPrimaryListNoListDto>();

            var Data = await _dbContext.Propertyregistration
                                       .Where(x => x.IsActive == 1
                                        && x.IsDeleted == 1
                                        && x.IsValidate == 1
                                        && x.IsDisposed != 0
                                        && x.PrimaryListNo != null).ToListAsync();
                                 
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new APIGetPrimaryListNoListDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].PrimaryListNo,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }
        //**************multiple files methods********************* added by ishu

        public async Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails item)
        {
            _dbContext.Watchandwardphotofiledetails.Add(item);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails item)
        {
            _dbContext.Watchandwardreportfiledetails.Add(item);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        // *******
        public async Task<List<ApiSaveWatchandwardDto>> GetAllWatchandward(ApiWatchWardParmsDto dto)
           
        {
            List<ApiSaveWatchandwardDto> listData = new List<ApiSaveWatchandwardDto>();
             

            var Data = await _dbContext.Watchandward
                                  .Include(x => x.PrimaryListNoNavigation)
                                  .Include(x => x.PrimaryListNoNavigation.Locality)
                                  .Where(a => (a.IsActive == 1)
                                //   && (a.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                     && a.Date == (dto.date == "" ? a.Date : Convert.ToDateTime(dto.date))
                                     && (string.IsNullOrEmpty(dto.locality) || a.PrimaryListNoNavigation.Locality.Name.Contains(dto.locality))
                                     && (string.IsNullOrEmpty(dto.khasrano) || a.PrimaryListNoNavigation.KhasraNo.Contains(dto.khasrano))
                                     && (string.IsNullOrEmpty(dto.primarylistno) || a.PrimaryListNoNavigation.PrimaryListNo.Contains(dto.primarylistno))
                                     )
                                  .OrderByDescending(a => a.Id)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    //List<string> propertyDocument = new List<string>();
                    //List<string> identityDocument = new List<string>();
                    ////Fetch identity document
                    //var identityDocumentData = await _dbContext.Doortodoorsurveyidentityproof
                    //                                     .Where(x => x.DoorToDoorSurveyId == Data[i].Id)
                    //                                     .ToListAsync();
                    //if (identityDocumentData != null)
                    //{
                    //    for (int h = 0; h < identityDocumentData.Count; h++)
                    //    {
                    //        identityDocument.Add(identityDocumentPath + identityDocumentData[h].OccupantIdentityPrrofFilePath);
                    //    }
                    //}
                    //Fetch Property document
                    //var propertyDocumentData = await _dbContext.Doortodoorsurveypropertyproof
                    //                                    .Where(x => x.DoorToDoorSurveyId == Data[i].Id)
                    //                                    .ToListAsync();
                    //if (propertyDocumentData != null)
                    //{
                    //    for (int h = 0; h < propertyDocumentData.Count; h++)
                    //    {
                    //        propertyDocument.Add(propertyDocumentPath + propertyDocumentData[h].PropertyFilePath);
                    //    }
                    //}



                    listData.Add(new ApiSaveWatchandwardDto()
                    {
                     Id = Data[i].Id,
                     RefNo = Data[i].RefNo,
                     Date = Data[i].Date,
                     PrimaryListNo = Data[i].PrimaryListNo,
                     LocalityId = Data[i].LocalityId,
                     KhasraId = Data[i].KhasraId,
                     PrimaryListNoName = Data[i].PrimaryListNoNavigation.PrimaryListNo,
                     LocalityName = Data[i].PrimaryListNoNavigation.Locality.Name,
                    // KhasraName = Data[i].PrimaryListNoNavigation.KhasraNo,
                     Landmark = Data[i].Landmark,
                     Encroachment = Data[i].Encroachment,
                     StatusOnGround = Data[i].StatusOnGround,
                     IsActive = Data[i].IsActive,
                     PhotoPath = Data[i].PhotoPath,
                     Latitude = Data[i].Latitude,
                     Longitude = Data[i].Longitude,
                     Remarks = Data[i].Remarks,
                     CreatedBy = Data[i].CreatedBy,
                     CreatedDate = Data[i].CreatedDate,
                    });
                }
            }
            return listData;
        }

    }
}
