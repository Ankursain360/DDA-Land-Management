using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS.Controllers
{
    public class GISController : BaseController
    {
        private readonly IGISService _GISService;

        public GISController(IGISService GISService)
        {
            _GISService = GISService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<JsonResult> GetVillageList(int? ZoneId)
        {
            return Json(await _GISService.GetVillageList(ZoneId ?? 0));
        }
        public async Task<JsonResult> GetPlotList(int? VillageId)
        {
            return Json(await _GISService.GetPlotList(VillageId ?? 0));
        }
        public async Task<JsonResult> GetZoneDetails(int ZoneId)
        {
            return Json(await _GISService.GetZoneDetails(ZoneId));
        }
        public async Task<JsonResult> GetVillageDetails(int VillageId)
        {
            return Json(await _GISService.GetVillageDetails(VillageId));
        }
        public async Task<JsonResult> GetZoneList()
        {
            return Json(await _GISService.GetZoneList());
        }
        public async Task<JsonResult> GetAbadiDetails(int VillageId)
        {
            return Json(await _GISService.GetAbadiDetails(VillageId));
        }
        public async Task<JsonResult> GetBurjiDetails(int VillageId)
        {
            return Json(await _GISService.GetBurjiDetails(VillageId));
        }
        public async Task<JsonResult> GetCleanDetails(int VillageId)
        {
            return Json(await _GISService.GetCleanDetails(VillageId));
        }
        public async Task<JsonResult> GetCleantextDetails(int VillageId)
        {
            return Json(await _GISService.GetCleantextDetails(VillageId));
        }
        public async Task<JsonResult> GetDimDetails(int VillageId)
        {
            return Json(await _GISService.GetDimDetails(VillageId));
        }
        public async Task<JsonResult> GetEncroachmentDetails(int VillageId)
        {
            return Json(await _GISService.GetEncroachmentDetails(VillageId));
        }
        public async Task<JsonResult> GetGoshaDetails(int VillageId)
        {
            return Json(await _GISService.GetGoshaDetails(VillageId));
        }
        public async Task<JsonResult> GetGridDetails(int VillageId)
        {
            return Json(await _GISService.GetGridDetails(VillageId));
        }
        public async Task<JsonResult> GetNalaDetails(int VillageId)
        {
            return Json(await _GISService.GetNalaDetails(VillageId));
        }
        public async Task<JsonResult> GetTextDetails(int VillageId)
        {
            return Json(await _GISService.GetTextDetails(VillageId));
        }
        public async Task<JsonResult> GetTriJunctionDetails(int VillageId)
        {
            return Json(await _GISService.GetTriJunctionDetails(VillageId));
        }
        public async Task<JsonResult> GetInitiallyStateDetails()
        {
            return Json(await _GISService.GetInitiallyStateDetails());
        }
        public async Task<JsonResult> GetDashedDetails(int VillageId)
        {
            return Json(await _GISService.GetDashedDetails(VillageId));
        }
        public async Task<JsonResult> GetCloseDetails(int VillageId)
        {
            return Json(await _GISService.GetCloseDetails(VillageId));
        }
        public async Task<JsonResult> GetCloseTextDetails(int VillageId)
        {
            return Json(await _GISService.GetCloseTextDetails(VillageId));
        }
        public async Task<JsonResult> GetDimTextDetails(int VillageId)
        {
            return Json(await _GISService.GetDimTextDetails(VillageId));
        }
        public async Task<JsonResult> GetFieldBounDetails(int VillageId)
        {
            return Json(await _GISService.GetFieldBounDetails(VillageId));
        }
        public async Task<JsonResult> GetKillaDetails(int VillageId)
        {
            return Json(await _GISService.GetKillaDetails(VillageId));
        }
        public async Task<JsonResult> GetKhasraNoDetails(int VillageId)
        {
            return Json(await _GISService.GetKhasraNoDetails(VillageId));
        }
        public async Task<JsonResult> GetKhasraLineDetails(int VillageId)
        {
            return Json(await _GISService.GetKhasraLineDetails(VillageId));
        }
        public async Task<JsonResult> GetKhasraBoundaryDetails(int VillageId)
        {
            return Json(await _GISService.GetKhasraBoundaryDetails(VillageId));
        }
        public async Task<JsonResult> GetKachaPakaLineDetails(int VillageId)
        {
            return Json(await _GISService.GetKachaPakaLineDetails(VillageId));
        }
        public async Task<JsonResult> GetInnerDetails(int VillageId)
        {
            return Json(await _GISService.GetInnerDetails(VillageId));
        }
        public async Task<JsonResult> GetNaliDetails(int VillageId)
        {
            return Json(await _GISService.GetNaliDetails(VillageId));
        }
        public async Task<JsonResult> GetRailwayLineDetails(int VillageId)
        {
            return Json(await _GISService.GetRailwayLineDetails(VillageId));
        }
        public async Task<JsonResult> GetSahedaDetails(int VillageId)
        {
            return Json(await _GISService.GetSahedaDetails(VillageId));
        }
        public async Task<JsonResult> GetRoadDetails(int VillageId)
        {
            return Json(await _GISService.GetRoadDetails(VillageId));
        }
        public async Task<JsonResult> GetZeroDetails(int VillageId)
        {
            return Json(await _GISService.GetZeroDetails(VillageId));
        }
        public async Task<JsonResult> GetVillageTextDetails(int VillageId)
        {
            return Json(await _GISService.GetVillageTextDetails(VillageId));
        }
        public async Task<JsonResult> GetVillageBoundaryDetails(int VillageId)
        {
            return Json(await _GISService.GetVillageBoundaryDetails(VillageId));
        }

        [HttpPost]
        public async Task<JsonResult> AutoComplete(string prefix)
        {
            return Json(await _GISService.GetVillageAutoCompleteDetails(prefix));
        }


        public async Task<JsonResult> GetInfrastructureDetails(int VillageId)
        {
            return Json(await _GISService.GetInfrastructureDetails(VillageId));
        }

        public async Task<JsonResult> GetGisDataLayersDetails(int VillageId)
        {
            var data = await _GISService.GetGisDataLayersDetails(VillageId);
            List<gisDataTemp> temp = new List<gisDataTemp>();

            if(data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    temp.Add(new gisDataTemp
                    {
                        Id = data[i].Id,
                        VillageId = data[i].VillageId,
                        GisLayerId = data[i].GisLayerId,
                        Xcoordinate = data[i].Xcoordinate,
                        Ycoordinate = data[i].Ycoordinate,
                        Polygon = data[i].Polygon,
                        Label = data[i].Label,
                        LabelXcoordinate = data[i].LabelXcoordinate,
                        LabelYcoordinate = data[i].LabelYcoordinate,
                        Name = data[i].GisLayer.Name,
                        Code = data[i].GisLayer.Code,
                        FillColor = data[i].GisLayer.FillColor,
                        StrokeColor = data[i].GisLayer.StrokeColor,
                        Type = data[i].GisLayer.Type

                    });
                }
            }
            var result = Json(temp);
            return result;
        }
    }
}
