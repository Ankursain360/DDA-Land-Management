using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.Text;
using Service.IApplicationService;
using Dto.Master;
using Core.Enum;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using DamagePayee.Filters;

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

        //[AuthorizeContext(ViewAction.Index)]
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

        public async Task<JsonResult> Getallfloordetails( int? Id)
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
        public async Task<JsonResult> GetallGpaDetails(int? Id)
        {

            Id = Id ?? 0;
            var data = await _selfAssessmentService.GetAllGpaDetails(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                DateOfExecutionOfGpa = Convert.ToDateTime(x.DateOfExecutionOfGpa).ToString("yyyy-MM-dd"),
                x.NameOfTheSeller,
                x.NameOfThePayer,
                x.AddressOfThePlotAsPerGpa,
                x.AreaOfThePlotAsPerGpa,
                x.GpafilePath,
                x.Id


            }));
        }

        public async Task<FileResult> viewGpaFile(int Id)
        {
            FileHelper file = new FileHelper();
            Newdamageselfassessmentgpadetail data = await _selfAssessmentService.GetGpaFilePath(Id);
            string path = data.GpafilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        public async Task<JsonResult> GetallAtsDetails(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentService.GetAllAtsDetails(Convert.ToInt32(Id));
            return Json(data.Select(X => new
            {
                DateOfExecutionOfAts = Convert.ToDateTime(X.DateOfExecutionOfAts).ToString("yyyy-MM-dd"),
                X.NameOfTheSellerAts,
                X.NameOfThePayerAts,
                X.AddressOfThePlotAsPerAts,
                X.AreaOfThePlotAsPerAts,
                X.AtsfilePath,
                X.Id

            }));
        }

        public async Task<FileResult> viewAtsFile(int Id)
        {
            FileHelper file = new FileHelper();
            Newdamageselfassessmentatsdetail data = await _selfAssessmentService.GetAtsFilePath(Id);
            string path = data.AtsfilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }
        public async Task<JsonResult> getPaymentDetails(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentService.Getpaymentdetail(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Name,
                x.RecieptNo,
                x.PaymentMode,
                PaymentDate = Convert.ToDateTime(x.PaymentDate).ToString("yyyy-MM-dd"),
                x.Amount,
                x.RecieptDocumentPath,
                x.Id
            }));
        }
        public async Task<FileResult> getPaymentFile(int Id)
        {
            FileHelper file = new FileHelper();
            Newdamagepaymenthistory data = await _selfAssessmentService.GetpaymentFile(Id);
            string path = data.RecieptDocumentPath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }

        public async Task<FileResult> fetchPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Newdamagepayeeregistration data = await _selfAssessmentService.FetchSingleResult(Id);
            string path = data.PropertyPhotographFilePath;
            path = data.ElectricityBillFilePath;
            path = data.WaterBillFilePath;
            path = data.PropertyTaxReceiptFilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }
        public async Task<JsonResult> getAllOccupantDetails(int? Id)
        {
            Id = Id ?? 0;

            var data = await _selfAssessmentService.GetOccupantDetails(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.FirstName,
                x.MiddleName,
                x.LastName,
                x.SpouseName,
                x.FatherName,
                x.MontherName,
                x.Epicid,
                x.EmailId,
                x.MobileNo,
                x.AadharNo,
                Dob = Convert.ToDateTime(x.Dob).ToString("yyyy-MM-dd"),
                x.Gender,
                x.PanNo,
                x.ShareInProperty,
                x.IsOccupingFloor,
                x.FloorNo,
                x.DamagePaidInPast,
                x.OccupantPhotoPath,
                x.Id
            }));

        }

        public async Task<FileResult> getOccupantDetails(int id)
        {
            FileHelper file = new FileHelper();
            Newdamagepayeeoccupantinfo data = await _selfAssessmentService.GetOccupantFile(id);
            string path = data.OccupantPhotoPath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }

        public async Task<IActionResult> DownloadDamagePayeeList(int id)
        {
            var result = await _selfAssessmentService.GetDamageSelfAssessments(id);
            List<NewDamagePayeeListDto> data = new List<NewDamagePayeeListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewDamagePayeeListDto()
                    {
                        FileNo = result[i].FileNo,
                        DamagePayee = result[i].WhetherDamageProp,
                        CurrentOccupant = result[i].Occupanttype,
                        TypeOfUse = result[i].UseType,
                        TotalConstructedArea = result[i].TotalConstructedArea,
                        HouseNo = result[i].HousePropertyNo,
                        RevenueEstate = result[i].GetVillage == null ? "" : result[i].GetVillage.Name,
                        Colony = result[i].GetColony == null ? "" : result[i].GetColony.Name


                    });

                }

            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
