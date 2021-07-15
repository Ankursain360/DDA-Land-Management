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
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Kycform kyc)
        {
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList();
            string Document = _configuration.GetSection("FilePaths:KycFiles:PaymentProofDocument").Value.ToString();


            if (ModelState.IsValid)
                {
                FileHelper fileHelper = new FileHelper();

                var result = await _kycformService.Create(kyc);
                if (result == true)
                {
                    //************ Save Kycleasepaymentrpt  ************  
                    if (kyc.ChallanNo != null &&
                        kyc.PaymentDate != null)

                    {
                        if (kyc.ChallanNo.Count > 0 )
                           
                        {
                            List<Kycleasepaymentrpt> payment = new List<Kycleasepaymentrpt>();
                            for (int i = 0; i < kyc.ChallanNo.Count; i++)
                            {
                                payment.Add(new Kycleasepaymentrpt
                                {
                                    ChallanNo = kyc.ChallanNo.Count <= i ? string.Empty : kyc.ChallanNo[i],
                                    PaymentDate = kyc.PaymentDate[i],
                                    PaymentAmount =  kyc.PaymentAmount[i],
                                    BankName = kyc.BankName.Count <= i ? string.Empty : kyc.BankName[i],
                                    Purpose = kyc.Purpose.Count <= i ? string.Empty : kyc.Purpose[i],
                                    PaymentDocPath = kyc.PaymentDocument != null ?
                                                                    kyc.PaymentDocument.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(Document, kyc.PaymentDocument[i]) :
                                                                    kyc.PaymentDocPath[i] != null || kyc.PaymentDocPath[i] != "" ?
                                                                    kyc.PaymentDocPath[i] : string.Empty,
                                    KycformId = kyc.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in payment)
                            {
                                result = await _kycformService.Saveleasepayment(item);
                            }
                        }
                    }
                    //************ Save Kycleasepaymentrpt  ************  
                    if (kyc.ChallanNo != null &&
                        kyc.PaymentDate != null)

                    {
                        if (kyc.ChallanNo.Count > 0)

                        {
                            List<Kyclicensepaymentrpt> payment = new List<Kyclicensepaymentrpt>();
                            for (int i = 0; i < kyc.ChallanNo.Count; i++)
                            {
                                payment.Add(new Kyclicensepaymentrpt
                                {
                                    ChallanNo = kyc.ChallanNo.Count <= i ? string.Empty : kyc.ChallanNo[i],
                                    PaymentDate = kyc.PaymentDate[i],
                                    PaymentAmount = kyc.PaymentAmount[i],
                                    PaymentPeriod = kyc.PaymentPeriod.Count <= i ? string.Empty : kyc.PaymentPeriod[i],
                                    Purpose = kyc.Purpose.Count <= i ? string.Empty : kyc.Purpose[i],
                                    PaymentDocPath = kyc.PaymentDocument != null ?
                                                                    kyc.PaymentDocument.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(Document, kyc.PaymentDocument[i]) :
                                                                    kyc.PaymentDocPath[i] != null || kyc.PaymentDocPath[i] != "" ?
                                                                    kyc.PaymentDocPath[i] : string.Empty,
                                    
                                    KycformId = kyc.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in payment)
                            {
                                result = await _kycformService.Savelicensepayment(item);
                            }
                        }
                    }
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View(kyc);
                        //var list = await _structureService.GetAllStructure();
                        // return View("Index", list);
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
    }
}
