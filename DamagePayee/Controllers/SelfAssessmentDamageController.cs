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
        async Task BindDropDown(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            damagepayeeregistertemp.LocalityList = await _selfAssessmentDamageService.GetLocalityList();
            damagepayeeregistertemp.DistrictList = await _selfAssessmentDamageService.GetDistrictList();
        }
        public async Task<IActionResult> Create()
        {
            Damagepayeeregistertemp damagepayeeregistertemp = new Damagepayeeregistertemp();
            var Data =await _selfAssessmentDamageService.FetchSelfAssessmentUserId(SiteContext.UserId);
            var value = await _selfAssessmentDamageService.GetRebateValue();

            ViewBag.RebateValue = 3.00;
            if (Data != null)
            {
                await BindDropDown(Data);
                return View(Data);
            }
            else
            {
                await BindDropDown(damagepayeeregistertemp);
                return View(damagepayeeregistertemp);
            }
            
        }
        [HttpPost]

        public async Task<IActionResult> Create(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            await BindDropDown(damagepayeeregistertemp);
            string PhotoFilePathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATSGPADocument").Value.ToString();
            string RecieptDocumentPathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:RecieptDocument").Value.ToString();
            string AadharNoDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:AadharNoDocument").Value.ToString();
            string PanNoDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:PanNoDocument").Value.ToString();
            string PhotographPersonelDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:PhotographPersonelDocument").Value.ToString();
            string SignaturePersonelDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:SignaturePersonelDocument").Value.ToString();
            string PropertyPhotographLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:PropertyPhotograph").Value.ToString();
            string ShowCauseNoticeDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:ShowCauseNotice").Value.ToString();
            string FGFormDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:FGForm").Value.ToString();
            string BillDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:Bill").Value.ToString();
            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();

                if (damagepayeeregistertemp.PropertyPhoto != null)
                {
                    damagepayeeregistertemp.PropertyPhotoPath = fileHelper.SaveFile(PropertyPhotographLayout, damagepayeeregistertemp.PropertyPhoto);
                }
                if (damagepayeeregistertemp.ShowCauseNotice != null)
                {
                    damagepayeeregistertemp.ShowCauseNoticePath = fileHelper.SaveFile(ShowCauseNoticeDocument, damagepayeeregistertemp.ShowCauseNotice);
                }
                if (damagepayeeregistertemp.Fgform != null)
                {
                    damagepayeeregistertemp.FgformPath = fileHelper.SaveFile(FGFormDocument, damagepayeeregistertemp.Fgform);
                }
                if (damagepayeeregistertemp.DocumentForFile != null)
                {
                    damagepayeeregistertemp.DocumentForFilePath = fileHelper.SaveFile(BillDocument, damagepayeeregistertemp.DocumentForFile);
                }

                var result = await _selfAssessmentDamageService.Create(damagepayeeregistertemp);
                if (result)
                {
                    //****** code for saving  Damage payee personal info *****

                    if (damagepayeeregistertemp.payeeName != null &&
                      damagepayeeregistertemp.Gender != null &&
                      damagepayeeregistertemp.Address != null &&
                      damagepayeeregistertemp.MobileNo != null)
                    {
                        if (damagepayeeregistertemp.payeeName.Count > 0 &&
                    damagepayeeregistertemp.Gender.Count > 0 &&
                    damagepayeeregistertemp.Address.Count > 0 &&
                    damagepayeeregistertemp.MobileNo.Count > 0)

                        {
                            List<Damagepayeepersonelinfotemp> damagepayeepersonelinfotemp = new List<Damagepayeepersonelinfotemp>();
                            for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                            {
                                damagepayeepersonelinfotemp.Add(new Damagepayeepersonelinfotemp
                                {
                                    Name = damagepayeeregistertemp.payeeName[i],
                                    FatherName = damagepayeeregistertemp.payeeFatherName[i],
                                    Gender = damagepayeeregistertemp.Gender[i],
                                    Address = damagepayeeregistertemp.Address[i],
                                    MobileNo = damagepayeeregistertemp.MobileNo[i],
                                    EmailId = damagepayeeregistertemp.EmailId[i],
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                    AadharNoFilePath = damagepayeeregistertemp.Aadhar[i] == null ? string.Empty : fileHelper.SaveFile(AadharNoDocument, damagepayeeregistertemp.Aadhar[i]),
                                    PanNoFilePath = damagepayeeregistertemp.Pan[i] == null ? string.Empty : fileHelper.SaveFile(PanNoDocument, damagepayeeregistertemp.Pan[i]),
                                    PhotographPath = damagepayeeregistertemp.Photograph[i] == null ? string.Empty : fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregistertemp.Photograph[i]),
                                    SignaturePath = damagepayeeregistertemp.SignatureFile[i] == null ? string.Empty : fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregistertemp.SignatureFile[i])
                                });
                            }
                            foreach (var item in damagepayeepersonelinfotemp)
                            {
                                result = await _selfAssessmentDamageService.SavePayeePersonalInfoTemp(item);
                            }
                        }
                    }

                    //****** code for saving  Allotte Type *****
                    if (damagepayeeregistertemp.Name != null &&
                     damagepayeeregistertemp.FatherName != null &&
                     damagepayeeregistertemp.Date != null)
                    {
                        if (
                         damagepayeeregistertemp.Name.Count > 0 &&
                         damagepayeeregistertemp.FatherName.Count > 0 &&
                         damagepayeeregistertemp.Date.Count > 0
                         )
                        {
                            List<Allottetypetemp> allottetypetemp = new List<Allottetypetemp>();
                            for (int i = 0; i < damagepayeeregistertemp.Name.Count; i++)
                            {
                                allottetypetemp.Add(new Allottetypetemp
                                {
                                    Name = damagepayeeregistertemp.Name[i],
                                    FatherName = damagepayeeregistertemp.FatherName[i],
                                    Date = damagepayeeregistertemp.Date[i],
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                    AtsgpadocumentPath = damagepayeeregistertemp.ATSGPA[i] == null ? string.Empty : fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregistertemp.ATSGPA[i])
                                });
                            }
                            result = await _selfAssessmentDamageService.SaveAllotteTypeTemp(allottetypetemp);
                        }
                    }

                    //****** code for saving  Damage payment history *****

                    if (damagepayeeregistertemp.PaymntName != null &&
                          damagepayeeregistertemp.RecieptNo != null &&
                          damagepayeeregistertemp.PaymentMode != null &&
                          damagepayeeregistertemp.PaymentDate != null &&
                          damagepayeeregistertemp.Amount != null)
                    {
                        if (
                             damagepayeeregistertemp.PaymntName.Count > 0 &&
                             damagepayeeregistertemp.RecieptNo.Count > 0 &&
                             damagepayeeregistertemp.PaymentMode.Count > 0 &&
                             damagepayeeregistertemp.PaymentDate.Count > 0 &&
                             damagepayeeregistertemp.Amount.Count > 0
                             )

                        {
                            List<Damagepaymenthistorytemp> damagepaymenthistorytemp = new List<Damagepaymenthistorytemp>();
                            for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                            {
                                damagepaymenthistorytemp.Add(new Damagepaymenthistorytemp
                                {
                                    Name = damagepayeeregistertemp.PaymntName[i],
                                    RecieptNo = damagepayeeregistertemp.RecieptNo[i],
                                    PaymentMode = damagepayeeregistertemp.PaymentMode[i],
                                    PaymentDate = damagepayeeregistertemp.PaymentDate[i],
                                    Amount = damagepayeeregistertemp.Amount[i],
                                    RecieptDocumentPath = damagepayeeregistertemp.Reciept[i] == null ? "" : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]),
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id
                                });
                            }

                            result = await _selfAssessmentDamageService.SavePaymentHistoryTemp(damagepaymenthistorytemp);

                        }
                    }
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return View(damagepayeeregistertemp);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(damagepayeeregistertemp);
                }

            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregistertemp);
            }
        }
    }
}
