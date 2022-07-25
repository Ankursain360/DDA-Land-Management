using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using DamagePayeePublicInterface.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;
using DamagePayeePublicInterface.Helper;

namespace DamagePayeePublicInterface.Controllers
{
    public class NewSelfAssessmentFormController : Controller
    {
        private readonly INewDamageSelfAssessmentService _selfAssessmentService;

        public IConfiguration _configuration;
        public NewSelfAssessmentFormController(INewDamageSelfAssessmentService selfAssessmentService, IConfiguration configuration)
        {
            _selfAssessmentService = selfAssessmentService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
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
                string strWaterBillDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:WaterNillDocumentFilePath").Value.ToString();
                string strPropertyTaxReceiptFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PropertyTaxReceiptFilePath").Value.ToString();

                string strPhotographOwner = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PhotographOwnerFilePath").Value.ToString();
                string strGpaFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:GpaFilePath").Value.ToString();
                string strAtsFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:AtsFilePath").Value.ToString();

                string strPaymentDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PaymentDocumentFilePath").Value.ToString();

                if (ModelState.IsValid)
                {
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
                        for (int i = 0; i < selfAssessment.LatestAtsname.Count; i++)
                        {
                            addOccupant.Add(new Newdamagepayeeoccupantinfo
                            {
                                LatestAtsname = selfAssessment.LatestAtsname[i],
                                LatestGpaname = selfAssessment.LatestGpaname[i],
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
                                GpafilePath = selfAssessment.FileGpafile == null ? "" : selfAssessment.FileGpafile.Count <= i ? null : fileHelper.SaveFile(strGpaFilePath, selfAssessment.FileGpafile[i]),
                                AtsfilePath = selfAssessment.FileAtsfile == null ? "" : selfAssessment.FileAtsfile.Count <= i ? null : fileHelper.SaveFile(strAtsFilePath, selfAssessment.FileAtsfile[i]),
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
                                AreaOfThePlotAsPerAts = selfAssessment.AreaOfThePlotAsPerAts.Count <= i ? null : selfAssessment.AreaOfThePlotAsPerAts[i],
                                NewDamageSelfAssessmentId = selfAssessment.Id

                            }); ;
                        }
                        foreach (var item in addATS)
                        {
                            result = await _selfAssessmentService.SaveATSDetails(item);
                        }


                        List<Newdamageselfassessmentholderdetail> holderdetails = new List<Newdamageselfassessmentholderdetail>();
                        for (int i = 0; i < selfAssessment.NameOfGpaats.Count; i++)
                        {
                            holderdetails.Add(new Newdamageselfassessmentholderdetail
                            {
                                NameOfGpaats = selfAssessment.NameOfGpaats.Count <= i ? null : selfAssessment.NameOfGpaats[i],
                                DeathCertificateNo = selfAssessment.DeathCertificateNo.Count <= i ? null : selfAssessment.DeathCertificateNo[i],
                                DeathCertificateDate = selfAssessment.DeathCertificateDate.Count <= i ? null : selfAssessment.DeathCertificateDate[i],
                                NameOfSurvivingMember = selfAssessment.NameOfSurvivingMember.Count <= i ? null : selfAssessment.NameOfSurvivingMember[i],
                                Relationship = selfAssessment.Relationship.Count <= i ? null : selfAssessment.Relationship[i],
                                IsRelinquished = selfAssessment.IsRelinquished.Count <= i ? null : selfAssessment.IsRelinquished[i],
                                NewDamageSelfAssessmentId = selfAssessment.Id

                            }); ;
                        }
                        foreach (var item in holderdetails)
                        {
                            result = await _selfAssessmentService.SaveHolderdetails(item);
                        }
                        List<Newdamagepaymenthistory> paymentdetails = new List<Newdamagepaymenthistory>();
                        for (int i = 0; i < selfAssessment.Name.Count; i++)
                        {
                            paymentdetails.Add(new Newdamagepaymenthistory
                            {
                                Name = selfAssessment.NameOfGpaats.Count <= i ? null : selfAssessment.NameOfGpaats[i],
                                RecieptNo = selfAssessment.DeathCertificateNo.Count <= i ? null : selfAssessment.DeathCertificateNo[i],
                                PaymentMode = selfAssessment.PaymentMode.Count <= i ? null : selfAssessment.PaymentMode[i],
                                PaymentDate = selfAssessment.PaymentDate.Count <= i ? null : selfAssessment.PaymentDate[i],
                                Amount = selfAssessment.Amount.Count <= i ? null : selfAssessment.Amount[i],
                                RecieptDocumentPath = selfAssessment.FileRecieptDocument.Count <= i ? null : fileHelper.SaveFile(strPaymentDocumentFilePath, selfAssessment.FileRecieptDocument[i]),
                                NewDamageSelfAssessmentId = selfAssessment.Id

                            }); ;
                        }
                        foreach (var item in paymentdetails)
                        {
                            result = await _selfAssessmentService.SavePaymentdetails(item);
                        }


                        if (result)
                        {

                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
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
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Error);
                return View(selfAssessment);
            }
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


    }
}
