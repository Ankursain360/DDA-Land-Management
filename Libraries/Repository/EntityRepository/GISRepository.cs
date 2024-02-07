using Dto.GIS;
using Dto.Master;
using Dto.Search;
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
    public class GISRepository : GenericRepository<Zone>, IGISSRepository
    {
        public GISRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Gisaabadi>> GetAbadiDetails(int villageId)
        {
            return await _dbContext.Gisaabadi
                                    .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                    .ToListAsync();
        }

        public async Task<List<Gisburji>> GetBurjiDetails(int villageId)
        {
            return await _dbContext.Gisburji
                                   .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                   .ToListAsync();
        }

        public async Task<List<Gisclean>> GetCleanDetails(int villageId)
        {
            try
            {
                return await _dbContext.Gisclean
                                       .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Giscleantext>> GetCleantextDetails(int villageId)
        {
            try
            {

                return await _dbContext.Giscleantext
                                       .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Gisclose>> GetCloseDetails(int villageId)
        {
            return await _dbContext.Gisclose
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Gisclosetext>> GetCloseTextDetails(int villageId)
        {
            return await _dbContext.Gisclosetext
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Gisdashed>> GetDashedDetails(int villageId)
        {
            return await _dbContext.Gisdashed
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Gisdim>> GetDimDetails(int villageId)
        {
            return await _dbContext.Gisdim
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<Gisdimtext>> GetDimTextDetails(int villageId)
        {
            return await _dbContext.Gisdimtext
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Gisencroachment>> GetEncroachmentDetails(int villageId)
        {
            try
            {
                return await _dbContext.Gisencroachment
                                       .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Gisfieldboun>> GetFieldBounDetails(int villageId)
        {
            return await _dbContext.Gisfieldboun
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gisdata>> GetGisDataLayersDetails(int villageId)
        {
            try
            {
                var data = await _dbContext.Gisdata
                                        .Include(x => x.GisLayer)
                                         .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                         .ToListAsync();
                return data;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Gisgosha>> GetGoshaDetails(int villageId)
        {
            return await _dbContext.Gisgosha
                                .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                .ToListAsync();
        }

        public async Task<List<Gisgrid>> GetGridDetails(int villageId)
        {
            return await _dbContext.Gisgrid
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<gisDataTemp>> GetInfrastructureDetails(int villageId)
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("GISInfrastructureColorCodeBind")
                                            .WithSqlParams(("p_villageid", villageId)
                                            )
                                            .ExecuteStoredProcedureAsync<gisDataTemp>();

                return (List<gisDataTemp>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<State>> GetInitiallyStateDetails()
        {
            return await _dbContext.State
                                 .Where(x => x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<Gisinner>> GetInnerDetails(int villageId)
        {
            return await _dbContext.Gisinner
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Giskachapakaline>> GetKachaPakaLineDetails(int villageId)
        {
            return await _dbContext.Giskachapakaline
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<GISKhasraBasisOtherDetailsDto>> GetKhasraBasisOtherDetails(int villageId, string khasraNo, string RectNo)
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("GISKhasraBasisOtherDetails")
                                            .WithSqlParams(("P_villageid", villageId), ("P_KhasraNo", khasraNo), ("P_RectNo", RectNo)
                                            )
                                            .ExecuteStoredProcedureAsync<GISKhasraBasisOtherDetailsDto>();

                return (List<GISKhasraBasisOtherDetailsDto>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GISKhasraBasisOtherDetailsDto>> GetKhasraBasisOtherDetailsForCourtCases(int villageId, string khasraNo, string RectNo)
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("GISKhasraBasisCourtCasesDetails")
                                            .WithSqlParams(("P_villageid", villageId), ("P_KhasraNo", khasraNo), ("P_RectNo", RectNo)
                                            )
                                            .ExecuteStoredProcedureAsync<GISKhasraBasisOtherDetailsDto>();

                return (List<GISKhasraBasisOtherDetailsDto>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Giskhasraboundary>> GetKhasraBoundaryDetails(int villageId)
        {
            return await _dbContext.Giskhasraboundary
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Giskhasraline>> GetKhasraLineDetails(int villageId)
        {
            return await _dbContext.Giskhasraline
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gisdata>> GetKhasraList(int villageId)
        {
            return await _dbContext.Gisdata
                                    .Where(x => x.VillageId == villageId && x.IsActive == 1 && x.GisLayerId == 30)
                                    .ToListAsync();
        }

        public async Task<List<Giskhasrano>> GetKhasraNoDetails(int villageId)
        {
            return await _dbContext.Giskhasrano
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gisdata>> GetKhasraNoPolygon(int gisDataId)
        {
            return await _dbContext.Gisdata
                                     .Where(x => x.Id == gisDataId)
                                     .ToListAsync();
        }

        public async Task<List<Giskilla>> GetKillaDetails(int villageId)
        {
            return await _dbContext.Giskilla
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gisnala>> GetNalaDetails(int villageId)
        {
            return await _dbContext.Gisnala
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<Gisnali>> GetNaliDetails(int villageId)
        {
            return await _dbContext.Gisnali
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _dbContext.Plot.Where(x => x.VillageId == VillageId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Gisrailwayline>> GetRailwayLineDetails(int villageId)
        {
            return await _dbContext.Gisrailwayline
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gisroad>> GetRoadDetails(int villageId)
        {
            return await _dbContext.Gisroad
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gissaheda>> GetSahedaDetails(int villageId)
        {
            return await _dbContext.Gissaheda
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gistext>> GetTextDetails(int villageId)
        {

            var data = await _dbContext.Gistext
                                .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                .ToListAsync();
            return data;
        }

        public async Task<List<Gistrijunction>> GetTriJunctionDetails(int villageId)
        {
            return await _dbContext.Gistrijunction
                                .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                .ToListAsync();
        }

        public async Task<List<Village>> GetVillageAutoCompleteDetails(string prefix)
        {
            var data = await _dbContext.Village.Where(x => x.Name.Contains(prefix) && x.IsActive == 1).OrderBy(p => p.Name).ToListAsync();
            return data;

        }

        public async Task<List<Gisvillageboundary>> GetVillageBoundaryDetails(int villageId)
        {
            return await _dbContext.Gisvillageboundary
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Village>> GetVillageDetails(int villageId)
        {
            return await _dbContext.Village
                                    .Where(x => x.IsActive == 1 && x.Id == villageId).OrderBy(p => p.Name)
                                    .ToListAsync();
        }

        public async Task<List<Village>> GetVillageList(int ZoneId)
        {
            return await _dbContext.Village.Where(x => x.ZoneId == ZoneId && x.IsActive == 1).OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<List<Gisvillagetext>> GetVillageTextDetails(int villageId)
        {
            return await _dbContext.Gisvillagetext
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Giszero>> GetZeroDetails(int villageId)
        {
            return await _dbContext.Giszero
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Zone>> GetZoneDetails(int zoneId)
        {
            return await _dbContext.Zone.Include(x => x.Village).Where(x => x.IsActive == 1 && x.Id == zoneId).ToListAsync();
        }

        public async Task<List<Zone>> GetZoneList()  //Demo
        {

            var data = await _dbContext.Zone.Include(x => x.Village)
                                        .Where(x => x.IsActive == 1
                                                 && x.Xcoordinate != null).OrderBy(z => z.Name)
                                                                         .ToListAsync();
            data.ForEach(t => t.Village = t.Village.OrderBy(n => n.Name).ToList());
            return data;
        }

        public async Task<GISKhasraUpdateResponseDto> UpdatekhasraNo(int khasraid, string khasraNo, int Userid)
        {
            GISKhasraUpdateResponseDto _obj = new GISKhasraUpdateResponseDto();
            gisdatahistory _objhistory = new gisdatahistory();
            var data = await _dbContext.Gisdata
                                        .Where(x => x.Id == khasraid && x.GisLayerId == 30).AsNoTracking()
                                        .FirstOrDefaultAsync();


            //update history
            _objhistory.Id = 0;
            _objhistory.VillageId = data.VillageId;
            _objhistory.GisLayerId = data.GisLayerId;
            _objhistory.Xcoordinate = data.Xcoordinate;
            _objhistory.Ycoordinate = data.Ycoordinate;
            _objhistory.Polygon = data.Polygon;
            _objhistory.OldLabel = data.Label;
            _objhistory.NewLabel = khasraNo;
            _objhistory.LabelXcoordinate = data.LabelXcoordinate;
            _objhistory.LabelYcoordinate = data.LabelYcoordinate;
            _objhistory.CreatedBy = Userid;
            _objhistory.CreatedDate = DateTime.Now;
            _dbContext.Add(_objhistory);
            var Historyresult = _dbContext.SaveChangesAsync();
            //end history
            if (Historyresult.Result > 0)
            {

                data.Label = khasraNo;
                data.ModifiedDate = DateTime.Now;
                data.ModifiedBy = Userid;
                _dbContext.Update(data);
                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    _obj.status = "1";
                    _obj.responseMessage = "Khasra No Successfully Updated.Kindly refresh the page to see the changes done by you.";
                }
                else
                {
                    _obj.status = "0";
                    _obj.responseMessage = " System is unable to update Khasra No.try again";
                }
            }
            else
            {
                _obj.status = "0";
                _obj.responseMessage = " System is unable to update Khasra No, because there is some issue with data history records.";
            }
            return _obj;
        }

        public async Task<List<Gisdata>> GetGCPList(int villageId)
        {
            return await _dbContext.Gisdata
                                    .Where(x => x.VillageId == villageId && x.IsActive == 1 && x.GisLayerId == 37)
                                    .ToListAsync();
        }

        public async Task<List<GISKhasraExport>> GetKhasraListforExport(int villageId)
        {
            List<GISKhasraExport> _khasralist = new List<GISKhasraExport>();

            var khasra = await _dbContext.Gisdata
                                    .Include(x => x.Village)
                                    .Where(x => x.VillageId == villageId && x.IsActive == 1 && x.GisLayerId == 30)
                                    .ToListAsync();
            int count = 1;
            foreach (var item in khasra)
            {
                GISKhasraExport _obj = new GISKhasraExport();
                _obj.SrNo = count;
                _obj.VillageName = item.Village.Name;
                _obj.RectNo_With_KhasraNo = item.Label;
                _khasralist.Add(_obj);
                count++;
            }
            return _khasralist;
        }

        public async Task<PagedResult<AIchangedetectiondata>> GetChangeDetectionData(AIchangeDetectionSearchDto model)
        {
            var data = await _dbContext.aichangedetectiondata
                .Include(x => x.Zone)
                .Include(x => x.Village)
                .Where(x => x.IsActive == 1)
                                    .GetPaged<AIchangedetectiondata>(model.PageNumber, model.PageSize);
            return data;
        }

        public async Task<bool> InsertchangeDetectiondata(AIchangedetectiondata dto)
        {
            _dbContext.Add(dto);
            var result = await _dbContext.SaveChangesAsync();
            //end history
            if (result > 0)
            {
                return true;
            }
            else
            { return false; }
        }

    }
}
