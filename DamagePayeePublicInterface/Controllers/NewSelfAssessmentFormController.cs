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


namespace DamagePayeePublicInterface.Controllers
{
    public class NewSelfAssessmentFormController : BaseController
    {
        private readonly INewDamageSelfAssessmentService _selfAssessmentService;
        private readonly IUserProfileService _userProfileService;
        public IConfiguration _configuration;
        public NewSelfAssessmentFormController(INewDamageSelfAssessmentService selfAssessmentService, IConfiguration configuration, IUserProfileService userProfileService)
        {
            _selfAssessmentService = selfAssessmentService;
            _configuration = configuration;
            _userProfileService = userProfileService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagePayeeSearchDto model, int id)
        {

            var data = await _selfAssessmentService.GetPagedDamagePayee(model, SiteContext.UserId);
            return PartialView("_List", data);

        }

        public async Task<IActionResult> Create()
        {
            Newdamagepayeeregistration model = new Newdamagepayeeregistration();
            model.districtList = await _selfAssessmentService.GetAllDistrict();
            model.floorlist = await _selfAssessmentService.GetFloors();
            model.ColonyList = await _selfAssessmentService.GetAllColony(0);
            model.damagevillageList = await _selfAssessmentService.GetAllVillage(model.DistrictId.HasValue ? Convert.ToInt32(model.DistrictId) : 0);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Newdamagepayeeregistration selfAssessment)
        {
            FileHelper fileHelper = new FileHelper();
            try
            {
                selfAssessment.districtList = await _selfAssessmentService.GetAllDistrict();
                selfAssessment.ColonyList = await _selfAssessmentService.GetAllColony(0);
                selfAssessment.damagevillageList = await _selfAssessmentService.GetAllVillage(selfAssessment.DistrictId.HasValue ? Convert.ToInt32(selfAssessment.DistrictId) : 0);
                selfAssessment.floorlist = await _selfAssessmentService.GetFloors();

                string strPhotographProperty = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PhotographProperty").Value.ToString();
                string strElectricityBillFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:ElectricityBillFilePath").Value.ToString();
                string strWaterBillDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:WaterBillDocumentFilePath").Value.ToString();
                string strPropertyTaxReceiptFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PropertyTaxReceiptFilePath").Value.ToString();

                string strPhotographOwner = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PhotographOwnerFilePath").Value.ToString();
                string strGpaFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:GpaFilePath").Value.ToString();
                string strAtsFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:AtsFilePath").Value.ToString();

                string strPaymentDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PaymentDocumentFilePath").Value.ToString();
                string strLeaseDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:LeaseDocument").Value.ToString();

                if (ModelState.IsValid)
                {
                    if (selfAssessment.LeaseDocumentFile != null)
                    {
                        selfAssessment.LeaseDocumentFilePath = fileHelper.SaveFile(strLeaseDocumentFilePath, selfAssessment.LeaseDocumentFile);
                    }
                    if (selfAssessment.FilePropertyPhoto != null)
                    {
                        selfAssessment.PropertyPhotographFilePath = fileHelper.SaveFile(strPhotographProperty, selfAssessment.FilePropertyPhoto);
                    }

                    if (selfAssessment.FileElectricityBill != null)
                    {
                        selfAssessment.ElectricityBillFilePath = fileHelper.SaveFile(strElectricityBillFilePath, selfAssessment.FileElectricityBill);
                    }

                    if (selfAssessment.FileWaterBill != null)
                    {
                        selfAssessment.WaterBillFilePath = fileHelper.SaveFile(strWaterBillDocumentFilePath, selfAssessment.FileWaterBill);
                    }

                    if (selfAssessment.FilePropertyTaxReceipt != null)
                    { // change file path strAtsFilePath
                        selfAssessment.PropertyTaxReceiptFilePath = fileHelper.SaveFile(strPropertyTaxReceiptFilePath, selfAssessment.FilePropertyTaxReceipt);
                    }
                    selfAssessment.UserId = SiteContext.UserId.ToString();
                    selfAssessment.CreatedBy = SiteContext.UserId;
                    var result = await _selfAssessmentService.Create(selfAssessment);
                    if (result)
                    {
                        //************Save Floor Details  ************

                        List<Newdamageselfassessmentfloordetail> addfloors = new List<Newdamageselfassessmentfloordetail>();
                        for (int i = 0; i < selfAssessment.FloorId.Count; i++)
                        {
                            addfloors.Add(new Newdamageselfassessmentfloordetail
                            {
                                FloorId = selfAssessment.FloorId[i],
                                CarpetArea = selfAssessment.CarpetArea[i],
                                ElectricityKno = selfAssessment.ElectricityKno[i],
                                WaterKno = selfAssessment.WaterKno[i],
                                CurrentUse = selfAssessment.CurrentUse[i],
                                McdpropertyTaxId= selfAssessment.McdpropertyTaxId[i],
                                CreatedBy = 1,//need to replace with dynamic value
                                CreatedDate = DateTime.Now,
                                NewDamageSelfAssessmentId = selfAssessment.Id

                            });
                        }
                        foreach (var item in addfloors)
                        {
                            result = await _selfAssessmentService.SaveFloorDetails(item);
                        }

                        List<Newdamagepayeeoccupantinfo> addOccupant = new List<Newdamagepayeeoccupantinfo>();
                        for (int i = 0; i < selfAssessment.FirstName.Count; i++)
                        {
                            addOccupant.Add(new Newdamagepayeeoccupantinfo
                            {
                                //LatestAtsname = selfAssessment.LatestAtsname[i],
                                //LatestGpaname = selfAssessment.LatestGpaname[i],
                                FirstName = selfAssessment.FirstName[i],
                                MiddleName = selfAssessment.MiddleName[i],
                                LastName = selfAssessment.LastName[i],
                                SpouseName = selfAssessment.SpouseName[i],
                                FatherName = selfAssessment.FatherName[i],

                                MontherName = selfAssessment.MontherName[i],
                                Epicid = selfAssessment.Epicid[i],
                                EmailId = selfAssessment.EmailId[i],
                                MobileNo = selfAssessment.MobileNo[i],
                                AadharNo = selfAssessment.AadharNo[i],
                                Dob = selfAssessment.Dob[i],//need to replace with dynamic value
                                Gender = selfAssessment.Gender.Count <= i ? string.Empty : selfAssessment.Gender[i],

                                PanNo = selfAssessment.PanNo[i],
                                ShareInProperty = selfAssessment.ShareInProperty[i],
                                IsOccupingFloor = selfAssessment.IsOccupingFloor.Count <= i ? string.Empty : selfAssessment.IsOccupingFloor[i],
                                FloorNo = selfAssessment.FloorNo.Count <= i ? string.Empty : selfAssessment.FloorNo[i],
                                DamagePaidInPast = selfAssessment.DamagePaidInPast[i],
                                OccupantPhotoPath = selfAssessment.FileOccupantPhoto == null ? "" : selfAssessment.FileOccupantPhoto.Count <= i ? null : fileHelper.SaveFile(strPhotographOwner, selfAssessment.FileOccupantPhoto[i]),
                                NewDamageSelfAssessmentId = selfAssessment.Id

                            });
                        }
                        foreach (var item in addOccupant)
                        {
                            result = await _selfAssessmentService.SaveOccupantDetails(item);
                        }


                        List<Newdamageselfassessmentgpadetail> addGPA = new List<Newdamageselfassessmentgpadetail>();
                        for (int i = 0; i < selfAssessment.DateOfExecutionOfGpa.Count; i++)
                        {
                            addGPA.Add(new Newdamageselfassessmentgpadetail
                            {
                                DateOfExecutionOfGpa = selfAssessment.DateOfExecutionOfGpa.Count <= i ? null : selfAssessment.DateOfExecutionOfGpa[i],
                                NameOfTheSeller = selfAssessment.NameOfTheSeller.Count <= i ? null : selfAssessment.NameOfTheSeller[i],
                                NameOfThePayer = selfAssessment.NameOfThePayer.Count <= i ? null : selfAssessment.NameOfThePayer[i],
                                AddressOfThePlotAsPerGpa = selfAssessment.AddressOfThePlotAsPerGpa.Count <= i ? null : selfAssessment.AddressOfThePlotAsPerGpa[i],
                                GpafilePath = selfAssessment.FileGpafile == null ? "" : selfAssessment.FileGpafile.Count <= i ? null : fileHelper.SaveFile(strGpaFilePath, selfAssessment.FileGpafile[i]),
                                AreaOfThePlotAsPerGpa = selfAssessment.AreaOfThePlotAsPerGpa.Count <= i ? null : selfAssessment.AreaOfThePlotAsPerGpa[i],
                                NewDamageSelfAssessmentId = selfAssessment.Id

                            }); ;
                        }
                        foreach (var item in addGPA)
                        {
                            result = await _selfAssessmentService.SaveGPADetails(item);
                        }

                        List<Newdamageselfassessmentatsdetail> addATS = new List<Newdamageselfassessmentatsdetail>();
                        for (int i = 0; i < selfAssessment.DateOfExecutionOfGpa.Count; i++)
                        {
                            addATS.Add(new Newdamageselfassessmentatsdetail
                            {
                                DateOfExecutionOfAts = selfAssessment.DateOfExecutionOfAts.Count <= i ? null : selfAssessment.DateOfExecutionOfAts[i],
                                NameOfTheSellerAts = selfAssessment.NameOfTheSellerAts.Count <= i ? null : selfAssessment.NameOfTheSellerAts[i],
                                NameOfThePayerAts = selfAssessment.NameOfThePayerAts.Count <= i ? null : selfAssessment.NameOfThePayerAts[i],
                                AddressOfThePlotAsPerAts = selfAssessment.AddressOfThePlotAsPerAts.Count <= i ? null : selfAssessment.AddressOfThePlotAsPerAts[i],
                                AtsfilePath = selfAssessment.FileAtsfile == null ? "" : selfAssessment.FileAtsfile.Count <= i ? null : fileHelper.SaveFile(strAtsFilePath, selfAssessment.FileAtsfile[i]),
                                AreaOfThePlotAsPerAts = selfAssessment.AreaOfThePlotAsPerAts.Count <= i ? null : selfAssessment.AreaOfThePlotAsPerAts[i],
                                NewDamageSelfAssessmentId = selfAssessment.Id

                            }); ;
                        }
                        foreach (var item in addATS)
                        {
                            result = await _selfAssessmentService.SaveATSDetails(item);
                        }


                        //********** holder details *************

                        //List<Newdamageselfassessmentholderdetail> holderdetails = new List<Newdamageselfassessmentholderdetail>();
                        //for (int i = 0; i < selfAssessment.NameOfGpaats.Count; i++)
                        //{
                        //    holderdetails.Add(new Newdamageselfassessmentholderdetail
                        //    {
                        //        NameOfGpaats = selfAssessment.NameOfGpaats.Count <= i ? null : selfAssessment.NameOfGpaats[i],
                        //        DeathCertificateNo = selfAssessment.DeathCertificateNo.Count <= i ? null : selfAssessment.DeathCertificateNo[i],
                        //        DeathCertificateDate = selfAssessment.DeathCertificateDate.Count <= i ? null : selfAssessment.DeathCertificateDate[i],
                        //        NameOfSurvivingMember = selfAssessment.NameOfSurvivingMember.Count <= i ? null : selfAssessment.NameOfSurvivingMember[i],
                        //        Relationship = selfAssessment.Relationship.Count <= i ? null : selfAssessment.Relationship[i],
                        //        IsRelinquished = selfAssessment.IsRelinquished.Count <= i ? null : selfAssessment.IsRelinquished[i],
                        //        NewDamageSelfAssessmentId = selfAssessment.Id

                        //    }); ;
                        //}
                        //foreach (var item in holderdetails)
                        //{
                        //    result = await _selfAssessmentService.SaveHolderdetails(item);
                        //}


                        List<Newdamagepaymenthistory> paymentdetails = new List<Newdamagepaymenthistory>();
                        for (int i = 0; i < selfAssessment.Name.Count; i++)
                        {
                            paymentdetails.Add(new Newdamagepaymenthistory
                            {
                                Name = selfAssessment.Name.Count <= i ? null : selfAssessment.Name[i],
                                RecieptNo = selfAssessment.RecieptNo.Count <= i ? null : selfAssessment.RecieptNo[i],
                                PaymentMode = selfAssessment.PaymentMode.Count <= i ? null : selfAssessment.PaymentMode[i],
                                PaymentDate = selfAssessment.PaymentDate.Count <= i ? null : selfAssessment.PaymentDate[i],
                                Amount = selfAssessment.Amount.Count <= i ? null : selfAssessment.Amount[i],
                                RecieptDocumentPath = selfAssessment.FileRecieptDocument == null ? "" : selfAssessment.FileRecieptDocument.Count <= i ? null : fileHelper.SaveFile(strAtsFilePath, selfAssessment.FileRecieptDocument[i]),
                                NewDamageSelfAssessmentId = selfAssessment.Id


                            });
                        }
                        foreach (var item in paymentdetails)
                        {
                            result = await _selfAssessmentService.SavePaymentdetails(item);
                        }


                        if (result)
                        {

                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            return View("Index", selfAssessment);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    }

                    return View(selfAssessment);


                }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    ViewBag.Message = Alert.Show(Messages.Error + messages, "", AlertType.Error);
                    return View(selfAssessment);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error + " " + ex.Message.ToString(), "An Error Occured", AlertType.Error);

            }
            return View("Index", selfAssessment);
        }

        [HttpGet]
        public async Task<JsonResult> GetNewVillageList(int? DistrictId)
        {

            DistrictId = DistrictId ?? 0;
            return Json(await _selfAssessmentService.GetAllVillage(Convert.ToInt32(DistrictId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetColonyList(int? VillageId)
        {
            VillageId = VillageId ?? 0;
            return Json(await _selfAssessmentService.GetAllColony(Convert.ToInt32(VillageId)));
        }

        public async Task<IActionResult> View(int id)
        {
            var data = await _selfAssessmentService.FetchSingleResult(id);
            data.districtList = await _selfAssessmentService.GetAllDistrict();
            data.damagevillageList = await _selfAssessmentService.GetAllVillage(data.DistrictId.HasValue ? Convert.ToInt32(data.DistrictId) : 0);
            data.ColonyList = await _selfAssessmentService.GetAllColony(data.VillageId.HasValue ? Convert.ToInt32(data.VillageId):0);
            data.floorlist = await _selfAssessmentService.GetFloors();
            if (data == null)
            {
                return NotFound();
            }
            return View(data);

        }
        public async Task<JsonResult> Getallfloordetail(int? Id)
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
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> fetchElectricityBillFile(int Id)
        {
            FileHelper file = new FileHelper();
            Newdamagepayeeregistration data = await _selfAssessmentService.FetchSingleResult(Id);
            string path = data.ElectricityBillFilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> fetchWaterBillPath(int Id)
        {
            FileHelper file = new FileHelper();
            Newdamagepayeeregistration data = await _selfAssessmentService.FetchSingleResult(Id);
            string path = data.WaterBillFilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> fetchPropertyTexFile(int Id)
        {
            FileHelper file = new FileHelper();
            Newdamagepayeeregistration data = await _selfAssessmentService.FetchSingleResult(Id);
           string path = data.PropertyTaxReceiptFilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> LeasePdfFile(int Id)
        {
            FileHelper file = new FileHelper();
            Newdamagepayeeregistration data = await _selfAssessmentService.FetchSingleResult(Id);
            string path = data.LeaseDocumentFilePath;
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

        public async Task<FileResult> getOccupantDetail(int id)
        {
            FileHelper file = new FileHelper();
            Newdamagepayeeoccupantinfo data = await _selfAssessmentService.GetOccupantFile(id);
            string path = data.OccupantPhotoPath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }


        public async Task<IActionResult> DownloadDamagePayeeList()
        {
            var result = await _selfAssessmentService.GetDamageSelfAssessments( SiteContext.UserId);
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
