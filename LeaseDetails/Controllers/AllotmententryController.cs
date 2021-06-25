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
using Utility.Helper;
using Dto.Master;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using LeaseDetails.Filters;
using Core.Enum;

namespace LeaseDetails.Controllers
{
    public class AllotmentEntryController : BaseController
    {
        static string result = string.Empty;
        private readonly IHostingEnvironment _hostingEnvironment;
        public IConfiguration _configuration;
        private readonly IAllotmentEntryService _allotmentEntryService;
        private readonly ILeaseApplicationFormService _leaseApplicationFormService;

        public AllotmentEntryController(IAllotmentEntryService allotmentEntryService, IHostingEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _allotmentEntryService = allotmentEntryService;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AllotmentEntrySearchDto model)
        {
            var result = await _allotmentEntryService.GetPagedAllotmententry(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()

        {
            Allotmententry allotmententry = new Allotmententry();
            allotmententry.IsActive = 1;          


            allotmententry.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication((int)ApprovalActionStatus.Approved);
            allotmententry.LeaseTypeList = await _allotmentEntryService.GetAllLeasetype();
            allotmententry.LeasePurposeList = await _allotmentEntryService.GetAllLeasepurpose();
            allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(allotmententry.PurposeId);
            return View(allotmententry);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Allotmententry allotmententry)
        {
            try
            {
                
                //Allotmententry allotmententry = new Allotmententry();
                allotmententry.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication((int)ApprovalActionStatus.Approved);
                allotmententry.LeaseTypeList = await _allotmentEntryService.GetAllLeasetype();
                allotmententry.LeasePurposeList = await _allotmentEntryService.GetAllLeasepurpose();
                //allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(allotmententry.PurposeId);
                allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(Convert.ToInt32(allotmententry.LeasePurposesTypeId));

                if (ModelState.IsValid)
                {
                    allotmententry.CreatedBy = SiteContext.UserId;
                    var result = await _allotmentEntryService.Create(allotmententry);

                    if (result == true)
                    {
                        #region Insert Row related to premium , Ground Rent in Payment Table Added By Renu 07 April 2021
                        if (allotmententry.LeasesTypeId == 1 )
                        {
                            result = await _allotmentEntryService.CreatePaymentPremiumDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentPremiumId").Value), SiteContext.UserId);
                            result = await _allotmentEntryService.CreatePaymentGroundRentDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentGroundRentId").Value), SiteContext.UserId);
                            result = await _allotmentEntryService.CreatePaymentDocumentChargesDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentDocumentChargesId").Value), SiteContext.UserId);
                        }
                        else if (allotmententry.LeasesTypeId == 2)
                        {
                            result = await _allotmentEntryService.CreatePaymentDocumentChargesDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentDocumentChargesId").Value), SiteContext.UserId);
                            result = await _allotmentEntryService.CreatePaymentLicenceFeesDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentLicenseFeesId").Value), SiteContext.UserId);
                        }
                        else if (allotmententry.LeasesTypeId == 3)
                        {
                            result = await _allotmentEntryService.CreatePaymentPremiumDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentPremiumId").Value), SiteContext.UserId);
                            result = await _allotmentEntryService.CreatePaymentGroundRentDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentGroundRentId").Value), SiteContext.UserId);
                            result = await _allotmentEntryService.CreatePaymentDocumentChargesDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentDocumentChargesId").Value), SiteContext.UserId);
                            result = await _allotmentEntryService.CreatePaymentLicenceFeesDr(allotmententry, Convert.ToInt32(_configuration.GetSection("LeasePaymentLicenseFeesId").Value), SiteContext.UserId);
                        }
                        #endregion


                        Random r = new Random();
                        int num = r.Next();
                        //******* creating  user ******
                        var username = "LD" + num;
                        var resultpassword = await _allotmentEntryService.CreateUser(allotmententry, username);
                        if (!resultpassword.Equals("False"))
                        {
                            //At successfull completion send mail and sms
                            //string DisplayName = allotmententry.Application.RefNo;
                            //string EmailID = damagepayeeregister.EmailId[0].ToString();
                            //string Id = allotmententry.Id.ToString().Unidecode();
                            string LoginName = allotmententry.Application.RefNo;
                            //string ContactNo = damagepayeeregister.MobileNo[0].ToString();
                            string Password = resultpassword;
                            var Data = await _allotmentEntryService.FetchLeaseApplicationmailDetails(allotmententry.ApplicationId);

                            if (Data != null && Data.EmailId != null)
                            {
                                #region Mail Generate
                                //At successfull completion send mail and sms
                                Uri uri = new Uri("http://localhost:1018/AllotmentEntry");
                                string Action = "Dear " + allotmententry.Name + ",  click  below link :-  " + uri;
                                string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "AllotmentEntryMailDetails.html");

                                #region Mail Generation Added By Renu

                                MailSMSHelper mailG = new MailSMSHelper();

                                #region HTML Body Generation
                                LeaseRefBodyDto bodyDTO = new LeaseRefBodyDto();
                                bodyDTO.displayName = Data.Name;
                                bodyDTO.RefNo = Data.RefNo;
                                bodyDTO.link = Action;
                                bodyDTO.path = path;
                                string strBodyMsg = mailG.PopulateBodyLeaseRefernceNo(bodyDTO);
                                #endregion

                              //  var sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, Data.EmailId, strMailCC, strMailBCC, strAttachPath);
                                #region Common Mail Genration
                                SentMailGenerationDto maildto = new SentMailGenerationDto();
                                maildto.strMailSubject = "User Reference No. Details ";
                                maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
                                maildto.strBodyMsg = strBodyMsg;
                                maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                                maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                                maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                                maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                                maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

                                maildto.strMailTo = Data.EmailId;
                                var sendMailResult = mailG.SendMailWithAttachment(maildto);
                                #endregion
                                #endregion

                                if (sendMailResult)
                                {
                                    ViewBag.Message = Alert.Show("Dear User,<br/>" + Data.Name + "  Your password is send to Registered email and Mobile No", "", AlertType.Success);
                                }
                                else
                                {
                                    ViewBag.Message = Alert.Show("Dear User,<br/>" + Data.Name + "  Successfully allotment is done but Enable to send Reference No. on your mail or sms due to network issue", "", AlertType.Info);

                                }

                                #endregion
                                return View(allotmententry);

                            }

                            else
                            {
                                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                                return View(allotmententry);
                            }
                        }
                        else
                        {
                            return View(allotmententry);
                        }

                    }
                    else
                    {
                        return View(allotmententry);
                    }


                }
                else
                {
                    return View(allotmententry);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(allotmententry);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _allotmentEntryService.FetchSingleResult(id);


            Data.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication((int)ApprovalActionStatus.Approved);
            Data.LeaseTypeList = await _allotmentEntryService.GetAllLeasetype();
            Data.LeasePurposeList = await _allotmentEntryService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(Convert.ToInt32(Data.LeasePurposesTypeId));

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Allotmententry allotmententry)
        {
            allotmententry.ModifiedBy = SiteContext.UserId;
            allotmententry.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication((int)ApprovalActionStatus.Approved);
            allotmententry.LeaseTypeList = await _allotmentEntryService.GetAllLeasetype();
            allotmententry.LeasePurposeList = await _allotmentEntryService.GetAllLeasepurpose();
            allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(Convert.ToInt32(allotmententry.LeasePurposesTypeId));
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _allotmentEntryService.Update(id, allotmententry);
                    if (result == true)
                    {                      

                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _allotmentEntryService.GetAllAllotmententry();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(allotmententry);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(allotmententry);
                }
            }
            else
            {
                return View(allotmententry);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _allotmentEntryService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _allotmentEntryService.GetAllAllotmententry();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _allotmentEntryService.FetchSingleResult(id);
            Data.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication((int)ApprovalActionStatus.Approved);
            Data.LeaseTypeList = await _allotmentEntryService.GetAllLeasetype();
            Data.LeasePurposeList = await _allotmentEntryService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(Convert.ToInt32(Data.LeasePurposesTypeId));

            

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }







        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? applicationid)
        {
            applicationid = applicationid ?? 0;

            return Json(await _allotmentEntryService.FetchSingleLeaseapplicationResult(Convert.ToInt32(applicationid)));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllLeaseSubpurpose(int? purposeUseId)
        {
            purposeUseId = purposeUseId ?? 0;
            return Json(await _allotmentEntryService.GetAllLeaseSubpurpose(Convert.ToInt32(purposeUseId)));
        }
      

        [HttpGet]
        public async Task<JsonResult> GetRateList(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            leasePurposeId = leasePurposeId ?? 0;
            leaseSubPurposeId = leaseSubPurposeId ?? 0;


            return Json(await _allotmentEntryService.FetchSinglerateResult(leasePurposeId, leaseSubPurposeId, allotmentDate));
           

        }
        [HttpGet]
        public async Task<JsonResult> GetGroundRateList(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            leasePurposeId = leasePurposeId ?? 0;
            leaseSubPurposeId = leaseSubPurposeId ?? 0;


            return Json(await _allotmentEntryService.FetchSinglegroundrentResult(leasePurposeId, leaseSubPurposeId, allotmentDate));
           
            

        }
        public async Task<JsonResult> GetFeeList(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            leasePurposeId = leasePurposeId ?? 0;
            leaseSubPurposeId = leaseSubPurposeId ?? 0;


            return Json(await _allotmentEntryService.FetchSinglefeeResult(leasePurposeId, leaseSubPurposeId, allotmentDate));
           
           
                }
        public async Task<JsonResult> GetDocumentList(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            leasePurposeId = leasePurposeId ?? 0;
            leaseSubPurposeId = leaseSubPurposeId ?? 0;


            return Json(await _allotmentEntryService.FetchSingledocumentResult(leasePurposeId, leaseSubPurposeId, allotmentDate));
           

        }


        public async Task<IActionResult> AllotmententryList()
        {
            var result = await _allotmentEntryService.GetAllAllotmententry();
            List<AllotmentEntryListDto> data = new List<AllotmentEntryListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new AllotmentEntryListDto()
                    {
                        Id = result[i].Id,
                        AllicationName = result[i].Application == null ? "" : result[i].Application.Name,
                        TotalArea = result[i].TotalArea.ToString(),
                        PlayGroundArea = result[i].PlayGroundArea.ToString(),
                        AllotmentDate = result[i].AllotmentDate == null ? "" : result[i].AllotmentDate.ToString(),
                        PhaseNo = result[i].PhaseNo == null ? "" : result[i].PhaseNo,
                        SectorNo = result[i].SectorNo == null ? "" : result[i].SectorNo,
                        PocketNo = result[i].PocketNo == null ? "" : result[i].PocketNo,
                        PlotNo = result[i].PlotNo == null ? "" : result[i].PlotNo,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ; ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

    }
}
