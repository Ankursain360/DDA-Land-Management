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

namespace DamagePayee.Controllers
{
    public class SelfAssessmentDamageController : BaseController
    {
        private readonly ISelfAssessmentDamageService _selfAssessmentDamageService;
        public IConfiguration _configuration;
        public SelfAssessmentDamageController(ISelfAssessmentDamageService selfAssessmentDamageService, IConfiguration configuration)
        {
            _configuration = configuration;
            _selfAssessmentDamageService = selfAssessmentDamageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeregisterSearchDto model)
        {
            var result = await _selfAssessmentDamageService.GetPagedDamagepayeeregister(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregister damagepayeeregister)
        {
            damagepayeeregister.LocalityList = await _selfAssessmentDamageService.GetLocalityList();
            damagepayeeregister.DistrictList = await _selfAssessmentDamageService.GetDistrictList();
        }
        public async Task<IActionResult> Create()
        {
            Damagepayeeregister damagepayeeregister = new Damagepayeeregister();

            await BindDropDown(damagepayeeregister);
            return View(damagepayeeregister);
        }
        [HttpPost]

        public async Task<IActionResult> Create(Damagepayeeregister damagepayeeregister)
        {
            await BindDropDown(damagepayeeregister);
            string PhotoFilePathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATSGPADocument").Value.ToString();
            string RecieptDocumentPathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:RecieptDocument").Value.ToString();
            if (ModelState.IsValid)
            {

                var result = await _selfAssessmentDamageService.Create(damagepayeeregister);
                if (result)
                {
                    FileHelper fileHelper = new FileHelper();

                    //****** code for saving  Damage payee personal info *****

                    if (damagepayeeregister.payeeName != null &&
                      damagepayeeregister.Gender != null &&
                      damagepayeeregister.Address != null &&
                      damagepayeeregister.MobileNo != null)
                    {
                        if (damagepayeeregister.payeeName.Count > 0 &&
                    damagepayeeregister.Gender.Count > 0 &&
                    damagepayeeregister.Address.Count > 0 &&
                    damagepayeeregister.MobileNo.Count > 0)

                        {
                            List<Damagepayeepersonelinfo> damagepayeepersonelinfo = new List<Damagepayeepersonelinfo>();
                            for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                            {
                                damagepayeepersonelinfo.Add(new Damagepayeepersonelinfo
                                {
                                    Name = damagepayeeregister.payeeName[i],
                                    FatherName = damagepayeeregister.payeeFatherName[i],
                                    Gender = damagepayeeregister.Gender[i],
                                    Address = damagepayeeregister.Address[i],
                                    MobileNo = damagepayeeregister.MobileNo[i],
                                    EmailId = damagepayeeregister.EmailId[i],
                                    DamagePayeeRegisterId = damagepayeeregister.Id
                                });
                            }
                            foreach (var item in damagepayeepersonelinfo)
                            {
                                result = await _selfAssessmentDamageService.SavePayeePersonalInfo(item);
                            }
                        }
                    }

                    //****** code for saving  Allotte Type *****
                    if (damagepayeeregister.Name != null &&
                     damagepayeeregister.FatherName != null &&
                     damagepayeeregister.Date != null)
                    {
                        if (
                         damagepayeeregister.Name.Count > 0 &&
                         damagepayeeregister.FatherName.Count > 0 &&
                         damagepayeeregister.Date.Count > 0
                         )
                        {
                            List<Allottetype> allottetype = new List<Allottetype>();
                            for (int i = 0; i < damagepayeeregister.Name.Count; i++)
                            {
                                allottetype.Add(new Allottetype
                                {
                                    Name = damagepayeeregister.Name[i],
                                    FatherName = damagepayeeregister.FatherName[i],
                                    Date = damagepayeeregister.Date[i],
                                    DamagePayeeRegisterId = damagepayeeregister.Id,
                                    AtsgpadocumentPath = damagepayeeregister.ATSGPA == null ? "" : damagepayeeregister.ATSGPA[i] == null ? string.Empty : fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregister.ATSGPA[i])
                                });
                            }
                            result = await _selfAssessmentDamageService.SaveAllotteType(allottetype);
                        }
                    }

                    //****** code for saving  Damage payment history *****

                    if (damagepayeeregister.PaymntName != null &&
                          damagepayeeregister.RecieptNo != null &&
                          damagepayeeregister.PaymentMode != null &&
                          damagepayeeregister.PaymentDate != null &&
                          damagepayeeregister.Amount != null)
                    {

                        //damagepayeeregister.Reciept != null &&)
                        if (
                             damagepayeeregister.PaymntName.Count > 0 &&
                             damagepayeeregister.RecieptNo.Count > 0 &&
                             damagepayeeregister.PaymentMode.Count > 0 &&
                             damagepayeeregister.PaymentDate.Count > 0 &&
                             damagepayeeregister.Amount.Count > 0
                             )

                        {
                            List<Damagepaymenthistory> damagepaymenthistory = new List<Damagepaymenthistory>();
                            for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                            {
                                damagepaymenthistory.Add(new Damagepaymenthistory
                                {
                                    Name = damagepayeeregister.PaymntName[i],
                                    RecieptNo = damagepayeeregister.RecieptNo[i],
                                    PaymentMode = damagepayeeregister.PaymentMode[i],
                                    PaymentDate = damagepayeeregister.PaymentDate[i],
                                    Amount = damagepayeeregister.Amount[i],

                                    //RecieptDocumentPath = damagepayeeregister.Reciept[i] == null ? string.Empty : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregister.Reciept[i]),
                                    RecieptDocumentPath = damagepayeeregister.Reciept == null ? "" : damagepayeeregister.Reciept[i] == null ? string.Empty : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregister.Reciept[i]),



                                    DamagePayeeRegisterId = damagepayeeregister.Id
                                });
                            }

                            result = await _selfAssessmentDamageService.SavePaymentHistory(damagepaymenthistory);

                        }
                    }
                    return View(damagepayeeregister);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(damagepayeeregister);
                }

            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregister);
            }
        }
    }
}
