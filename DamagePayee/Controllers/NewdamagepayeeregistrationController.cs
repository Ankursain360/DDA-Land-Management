using AutoMapper.Configuration;
using Core.Enum;
using DamagePayee.Filters;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using NPOI.OpenXml4Net.OPC.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamagePayee.Controllers
{
    public class NewdamagepayeeregistrationController : Controller
    {
        private readonly INewDamageSelfAssessmentService _selfAssessmentService;

        //private readonly IConfiguration _configuration;

        public NewdamagepayeeregistrationController(INewDamageSelfAssessmentService selfAssessmentService)
        {
            _selfAssessmentService = selfAssessmentService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagePayeeSearchDto model)
        {
            var data = await _selfAssessmentService.GetPagedDamagePayee(model,0);
            return PartialView("_List", data);

        }

        public async Task<IActionResult> View(int id)
        {
            var data = await _selfAssessmentService.FetchSingleResult(id);
            data.districtList = await _selfAssessmentService.GetAllDistrict();
            data.damagevillageList = await _selfAssessmentService.GetAllVillage(data.DistrictId.HasValue ? Convert.ToInt32(data.DistrictId) : 0);
            data.ColonyList = await _selfAssessmentService.GetAllColony(id);
            data.floorlist = await _selfAssessmentService.GetFloors();
            if (data == null)
            {
                return NotFound();
            }
            return View(data);

        }

        public async Task<JsonResult> Getallfloordetails(Newdamageselfassessmentfloordetail model , int? Id)
        {
            
            Id = Id ?? 0;
           
            var data = await _selfAssessmentService.GetAddfloorsDetails(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
             x.FloorId,
             x.CarpetArea,
             x.ElectricityKno,
             x.McdpropertyTaxId, 
             x.WaterKno,
             x.CurrentUse

            }));
        }
        public async Task<JsonResult> GetallGpaDetails( int? Id)
        {

            Id = Id ?? 0;
            var data = await _selfAssessmentService.GetAllGpaDetails(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                DateOfExecutionOfGpa = Convert.ToDateTime(x.DateOfExecutionOfGpa).ToString("yyyy-MM-dd"),
                x.NameOfTheSeller,
                x.NameOfThePayer,
                x.AddressOfThePlotAsPerGpa,
                x.AreaOfThePlotAsPerGpa

            }));
        }

        //public async Task<FileResult> ViewGpaFile(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    Newdamageselfassessmentgpadetail Data = await _selfAssessmentService.GetGpaFilePath(Id);
        //    string path = Data.SignaturePath;
        //    byte[] FileBytes = System.IO.File.ReadAllBytes(path);
        //    return File(FileBytes, file.GetContentType(path));
        //}
    }
}
