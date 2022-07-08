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
        public NewSelfAssessmentFormController(INewDamageSelfAssessmentService selfAssessmentService,IConfiguration configuration)
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
            NewDamageSelfAssessment model = new NewDamageSelfAssessment();
            model.DistrictList = await _selfAssessmentService.GetAllDistrict();
            model.LocalitieList = await _selfAssessmentService.GetLocalityList();
            model.AcquiredlandvillageList = await _selfAssessmentService.GetAllVillage(model.Districtid);         
            return View(model);
           
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewDamageSelfAssessment selfAssessment)
        {
            FileHelper fileHelper = new FileHelper();
            try
            {
                selfAssessment.DistrictList = await _selfAssessmentService.GetAllDistrict();
                selfAssessment.LocalitieList = await _selfAssessmentService.GetLocalityList();
                selfAssessment.AcquiredlandvillageList = await _selfAssessmentService.GetAllVillage(selfAssessment.Districtid);

                string strPhotographProperty = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PhotographProperty").Value.ToString();
                
                string strPhotographOwner = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PhotographOwnerFilePath").Value.ToString();
                string strGpaFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:GpaFilePath").Value.ToString();
                string strAtsFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:AtsFilePath").Value.ToString();
                string strElectricityBillFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:ElectricityBillFilePath").Value.ToString();
                string strPaymentDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PaymentDocumentFilePath").Value.ToString();

                string strWillDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:WillDocumentFilePath").Value.ToString();
                string strPossessionDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:PossessionDocumentFilePath").Value.ToString();

                string strMutationDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:MutationDocumentFilePath").Value.ToString();
                string strCoordinateDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:CoordinateDocumentFilePath").Value.ToString();
                string strChainDocumentFilePath = _configuration.GetSection("FilePaths:NewDamagePayeeAssmt:ChainDocumentFilePath").Value.ToString();
                
                if (ModelState.IsValid)
                {
                    if (selfAssessment.PhotographarFP != null)
                    {
                        selfAssessment.PhotographProperty = fileHelper.SaveFile(strPhotographProperty, selfAssessment.PhotographarFP);
                    }

                    if (selfAssessment.PhotographOwnerFP != null)
                    {
                        selfAssessment.PhotographOwner = fileHelper.SaveFile(strPhotographOwner, selfAssessment.PhotographOwnerFP);
                    }

                    if (selfAssessment.GpaFilePathFP != null)
                    {
                        selfAssessment.Gpa = fileHelper.SaveFile(strGpaFilePath, selfAssessment.GpaFilePathFP);
                    }

                    if (selfAssessment.AtsFilePathFP != null)
                    {
                        selfAssessment.Ats = fileHelper.SaveFile(strAtsFilePath, selfAssessment.AtsFilePathFP);
                    }

                    if (selfAssessment.ElectricityBillFP != null)
                    {
                        selfAssessment.ElectricityBill = fileHelper.SaveFile(strElectricityBillFilePath, selfAssessment.ElectricityBillFP);
                    }


                    if (selfAssessment.PaymentDocumentFP != null)
                    {
                        selfAssessment.PaymentDocument = fileHelper.SaveFile(strPaymentDocumentFilePath, selfAssessment.PaymentDocumentFP);
                    }

                    if (selfAssessment.WillDocumentFP != null)
                    {
                        selfAssessment.WillDocument = fileHelper.SaveFile(strWillDocumentFilePath, selfAssessment.WillDocumentFP);
                    }

                    if (selfAssessment.PossessionDocumentFP != null)
                    {
                        selfAssessment.PossessionDocument = fileHelper.SaveFile(strPossessionDocumentFilePath, selfAssessment.PossessionDocumentFP);
                    }

                    if (selfAssessment.MutationDocumentFP != null)
                    {
                        selfAssessment.MutationDocument = fileHelper.SaveFile(strMutationDocumentFilePath, selfAssessment.MutationDocumentFP);
                    }

                    if (selfAssessment.CoordinateDocumentFP != null)
                    {
                        selfAssessment.CoordinateDocument = fileHelper.SaveFile(strCoordinateDocumentFilePath, selfAssessment.CoordinateDocumentFP);
                    }
                    if (selfAssessment.ChainDocumentFP != null)
                    {
                        selfAssessment.ChainDocument = fileHelper.SaveFile(strChainDocumentFilePath, selfAssessment.ChainDocumentFP);
                    }

                    var result = await _selfAssessmentService.Create(selfAssessment);

                    //************ Save Floor Details  ************  

                    if (selfAssessment.AFloorName != null &&
                        selfAssessment.AFloorName != null &&
                        selfAssessment.AElectricityNumber != null)

                    {
                        if (selfAssessment.AFloorName.Count > 0 &&
                            selfAssessment.ACarpetArea.Count > 0 &&
                            selfAssessment.AElectricityNumber.Count > 0
                           )

                        {
                            List<NewdamageAddfloor> addfloors = new List<NewdamageAddfloor>();
                            for (int i = 0; i < selfAssessment.AFloorName.Count; i++)
                            {
                                addfloors.Add(new NewdamageAddfloor
                                {
                                    FloorName = selfAssessment.AFloorName.Count <= i ? string.Empty : selfAssessment.AFloorName[i],
                                    CarpetArea = selfAssessment.ACarpetArea.Count <= i ? 0 :Convert.ToDecimal(selfAssessment.ACarpetArea[i]),
                                    ElectricityNumber = selfAssessment.AElectricityNumber.Count <= i ? string.Empty : selfAssessment.AElectricityNumber[i],
                                    MuncipleTaxId= selfAssessment.AMuncipleTaxId.Count <= i ? string.Empty : selfAssessment.AMuncipleTaxId[i],
                                    NewDamageSelfAssessmentId = selfAssessment.Id
                                    
                                });
                            }
                            foreach (var item in addfloors)
                            {
                                result = await _selfAssessmentService.SaveFloorDetails(item);
                            }
                        }
                    }
                    //****** code for Floor Details *****


                    //************ Save GPA Details  ************  

                    if (selfAssessment.ADateOfExecutionOfGpa != null &&
                        selfAssessment.ANameOfTheSeller != null &&
                        selfAssessment.ANameOfThePayer != null)

                    {
                        if (selfAssessment.ADateOfExecutionOfGpa.Count > 0 &&
                            selfAssessment.ANameOfTheSeller.Count > 0 &&
                            selfAssessment.ANameOfThePayer.Count > 0
                           )

                        {
                            List<NewDamageSelfAssessmentGpaDetails> addGPA = new List<NewDamageSelfAssessmentGpaDetails>();
                            for (int i = 0; i < selfAssessment.AFloorName.Count; i++)
                            {
                                addGPA.Add(new NewDamageSelfAssessmentGpaDetails
                                {
                                    DateOfExecutionOfGpa = selfAssessment.ADateOfExecutionOfGpa.Count <= i ? DateTime.Now : selfAssessment.ADateOfExecutionOfGpa[i],
                                    NameOfTheSeller = selfAssessment.ANameOfTheSeller.Count <= i ? string.Empty : selfAssessment.ANameOfTheSeller[i],
                                    NameOfThePayer = selfAssessment.ANameOfThePayer.Count <= i ? string.Empty : selfAssessment.ANameOfThePayer[i],
                                    AddressOfThePlotAsPerGpa = selfAssessment.AAddressOfThePlotAsPerGpa.Count <= i ? string.Empty : selfAssessment.AAddressOfThePlotAsPerGpa[i],
                                    AreaOfThePlotAsPerGpa = selfAssessment.AAreaOfThePlotAsPerGpa.Count <= i ? string.Empty : selfAssessment.AAreaOfThePlotAsPerGpa[i],
                                 
                                    NewDamageSelfAssessmentId = selfAssessment.Id

                                });
                            }
                            foreach (var item in addGPA)
                            {
                                result = await _selfAssessmentService.SaveGPADetails(item);
                            }
                        }
                    }
                    //****** code for saving  GPA Details *****



                    //************ Save ATS Details  ************  

                    if (selfAssessment.ADateOfExecutionOfAts != null &&
                        selfAssessment.ANameOfTheSellerAts != null &&
                        selfAssessment.ANameOfThePayerAts != null)

                    {
                        if (selfAssessment.ADateOfExecutionOfAts.Count > 0 &&
                            selfAssessment.ANameOfTheSellerAts.Count > 0 &&
                            selfAssessment.ANameOfThePayerAts.Count > 0
                           )

                        {
                            List<NewDamageSelfAssessmentAtsDetails> addATS = new List<NewDamageSelfAssessmentAtsDetails>();
                            for (int i = 0; i < selfAssessment.AFloorName.Count; i++)
                            {
                                addATS.Add(new NewDamageSelfAssessmentAtsDetails
                                {
                                    DateOfExecutionOfAts = selfAssessment.ADateOfExecutionOfAts.Count <= i ? DateTime.Now : selfAssessment.ADateOfExecutionOfAts[i],
                                    NameOfTheSellerAts = selfAssessment.ANameOfTheSellerAts.Count <= i ? string.Empty : selfAssessment.ANameOfTheSellerAts[i],
                                    NameOfThePayerAts = selfAssessment.ANameOfThePayerAts.Count <= i ? string.Empty : selfAssessment.ANameOfThePayerAts[i],
                                    AddressOfThePlotAsPerAts = selfAssessment.AAddressOfThePlotAsPerAts.Count <= i ? string.Empty : selfAssessment.AAddressOfThePlotAsPerAts[i],
                                    AreaOfThePlotAsPerAts = selfAssessment.AAreaOfThePlotAsPerAts.Count <= i ? string.Empty : selfAssessment.AAreaOfThePlotAsPerAts[i],

                                    NewDamageSelfAssessmentId = selfAssessment.Id

                                });
                            }
                            foreach (var item in addATS)
                            {
                                result = await _selfAssessmentService.SaveATSDetails(item);
                            }
                        }
                    }
                    //****** code for saving  ATS Details *****




                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _selfAssessmentService.GetAllDistrict();
                        return View("Index", list);

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(selfAssessment);

                    }
                }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    return View(selfAssessment);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
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
