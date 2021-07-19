using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using LeaseForPublic.Filters;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Dto.Search;

namespace LeaseForPublic.Controllers
{
    public class KYCformController : BaseController
    {
        private readonly IKycformService _kycformService;
        public IConfiguration _configuration;
        public KYCformController(IConfiguration configuration, IKycformService KycformService)
          
        {
            _configuration = configuration;
            _kycformService = KycformService;
           
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] KycformSearchDto model)
        {
            var result = await _kycformService.GetPagedKycform(model);
            return PartialView("_List", result);
        }

        // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Kycform kyc = new Kycform();
          
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList();
            return View(kyc);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Kycform kyc)
        {
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList();
            string AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            string LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            string ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();


            if (ModelState.IsValid)
                {
                FileHelper fileHelper = new FileHelper();

                if (kyc.Aadhar != null)
                {
                    kyc.AadhaarNoPath = fileHelper.SaveFile(AadharDoc, kyc.Aadhar);
                }
                if (kyc.Letter != null)
                {
                    kyc.LetterPath = fileHelper.SaveFile(LetterDoc, kyc.Letter);
                }
                if (kyc.ApplicantPan != null)
                {
                    kyc.AadhaarPanapplicantPath = fileHelper.SaveFile(ApplicantDoc, kyc.ApplicantPan);
                }

                kyc.CreatedBy = SiteContext.UserId;
                kyc.IsActive = 1;
                var result = await _kycformService.Create(kyc);
                if (result == true)
                {
                   
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                     
                    var list = await _kycformService.GetAllKycform();
                    return View("Index", list);
                }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(kyc);

                    }
                }
                else
                {
                    return View(kyc);
                }
            
        }

       
        //[AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _kycformService.FetchSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
            Data.LocalityList = await _kycformService.GetLocalityList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Kycform kyc)
        {
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList();
            string AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            string LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            string ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();


            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();

                if (kyc.Aadhar != null)
                {
                    kyc.AadhaarNoPath = fileHelper.SaveFile(AadharDoc, kyc.Aadhar);
                }
                if (kyc.Letter != null)
                {
                    kyc.LetterPath = fileHelper.SaveFile(LetterDoc, kyc.Letter);
                }
                if (kyc.ApplicantPan != null)
                {
                    kyc.AadhaarPanapplicantPath = fileHelper.SaveFile(ApplicantDoc, kyc.ApplicantPan);
                }
                kyc.ModifiedBy = SiteContext.UserId;
                var result = await _kycformService.Update(id, kyc);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _kycformService.GetAllKycform();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(kyc);

                    }
               
            }
            return View(kyc);
        }

        //[AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _kycformService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _kycformService.GetAllKycform();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _kycformService.GetAllKycform();
                return View("Index", result1);
            }
        }

        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _kycformService.FetchSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
            Data.LocalityList = await _kycformService.GetLocalityList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
