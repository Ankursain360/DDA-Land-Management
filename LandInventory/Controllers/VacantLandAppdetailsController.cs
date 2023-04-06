using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Core.Enum;
using LandInventory.Filters;
using Dto.Search;
using Libraries.Model.Entity;
using Utility.Helper;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using MySqlX.XDevAPI;
using Microsoft.AspNetCore.Http;
using Dto.Master;

namespace LandInventory.Controllers
{
    public class VacantLandAppdetailsController : BaseController
    {
        private readonly IPropertyRegistrationService _propertyRegistrationService;
        public IConfiguration _configuration;
        string VacantLandFilePath = "";
        public VacantLandAppdetailsController(IPropertyRegistrationService propertyRegistrationService, IConfiguration configuration)
        {
            _propertyRegistrationService = propertyRegistrationService;
            _configuration = configuration;
            VacantLandFilePath = _configuration.GetSection("FilePaths:VacantLandAppDetailsFilesPath:VacantLandAppDocumentPath").Value.ToString();
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.DepartmentList = await _propertyRegistrationService.GetDepartmentDropDownListForApi();
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int departmentId)
        {
            var data = await _propertyRegistrationService.GetZoneDropDownListForApi(departmentId);
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int zoneId)
        {

            var data = await _propertyRegistrationService.GetDivisionDropDownListForApi(zoneId);
            return Json(data);
        }
        [HttpPost]
        public async Task<JsonResult> GetPrimaryList([FromBody] VacantLandAppDetailsSearchDto model)
        {

            var data = await _propertyRegistrationService.GetPrimaryList(model);
            return Json(data);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] VacantLandAppDetailsSearchDto model)
        {

            var result = await _propertyRegistrationService.GetPagedVacantLandAppDetails(model);
            return PartialView("_List", result);
        }
        //public class imagedto
        //{

        //    public string imagename { get; set; }
        //    public string imagedata { get; set; }
        //}
        [HttpGet]
        public async Task<IActionResult> fetchAppUploadImagesFile(int id)
        {

            FileHelper file = new FileHelper();
            List<vacantlandlistimage> data = await _propertyRegistrationService.FetchSingleVacantLandAppDetails(id);

            List<VacantLandListImagesDto> list = new List<VacantLandListImagesDto>();
           
            for (int i = 0; i < data.Count; i++)
            {
                VacantLandListImagesDto dto = new VacantLandListImagesDto();
                string images = VacantLandFilePath + data[i].ImagePath.ToString();
                byte[] fileBytes = System.IO.File.ReadAllBytes(images);
                dto.imagedata =string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(fileBytes));
               dto.imagename = data[i].ImagePath.ToString();
                list.Add(dto);
            }
            return PartialView("_fetchAppUploadImagesFile", list);
        }
    }
}
