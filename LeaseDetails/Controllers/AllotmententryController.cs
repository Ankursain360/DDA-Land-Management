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

namespace LeaseDetails.Controllers
{
    public class AllotmentEntryController : BaseController
    {
        static string result = string.Empty;
        private readonly IHostingEnvironment _hostingEnvironment;
        public Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IAllotmentEntryService _allotmentEntryService;
        private readonly ILeaseApplicationFormService _leaseApplicationFormService;

        public AllotmentEntryController(IAllotmentEntryService allotmentEntryService, IHostingEnvironment hostingEnvironment)
        {
            _allotmentEntryService = allotmentEntryService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _allotmentEntryService.GetAllAllotmententry();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AllotmentEntrySearchDto model)
        {
            var result = await _allotmentEntryService.GetPagedAllotmententry(model);
            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()

        {
            Allotmententry allotmententry = new Allotmententry();
            allotmententry.IsActive = 1;



            allotmententry.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication();
            allotmententry.LeaseTypeList = await _allotmentEntryService.GetAllLeasetype();
            allotmententry.LeasePurposeList = await _allotmentEntryService.GetAllLeasepurpose();
            allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(allotmententry.PurposeId);
            return View(allotmententry);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Allotmententry allotmententry)
        {
            try
            {
                //Allotmententry allotmententry = new Allotmententry();
                allotmententry.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication();
                allotmententry.LeaseTypeList = await _allotmentEntryService.GetAllLeasetype();
                allotmententry.LeasePurposeList = await _allotmentEntryService.GetAllLeasepurpose();
                //allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(allotmententry.PurposeId);
                allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(Convert.ToInt32(allotmententry.LeasePurposesTypeId));

                if (ModelState.IsValid)
                {
                    //var result = await _allotmentEntryService.Create(allotmententry);

                    //if (result == true)
                    //{
                    //    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    //    var list = await _allotmentEntryService.GetAllAllotmententry();
                    //    return View("Index", list);
                    //}app
                    var result = await _allotmentEntryService.Create(allotmententry);

                    if (result == true)
                    {
                        //var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetails(Convert.ToInt32(id));
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

                            string strMailSubject = "User Reference No. Details ";
                            string strMailCC = "", strMailBCC = "", strAttachPath = "";
                            var sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, Data.EmailId, strMailCC, strMailBCC, strAttachPath);
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
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(allotmententry);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _allotmentEntryService.FetchSingleResult(id);


            Data.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication();
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
        public async Task<IActionResult> Edit(int id, Allotmententry allotmententry)
        {

            allotmententry.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication();
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

        public async Task<IActionResult> View(int id)
        {
            var Data = await _allotmentEntryService.FetchSingleResult(id);
            Data.LeaseappList = await _allotmentEntryService.GetAllLeaseapplication();
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
        //[HttpGet]
        //public async Task<JsonResult> GetCalculationList(int? LeasesTypeId)
        //{
        //    LeasesTypeId = LeasesTypeId ?? 0;

        //    return Json(await _allotmentEntryService.FetchSingleCalculationDetails(Convert.ToInt32(LeasesTypeId)));
        //}
        //[HttpGet]
        //public async Task<JsonResult> GetDocumentList(int? leasesTypeId)
        //{
        //    leasesTypeId = leasesTypeId ?? 0;

        //    return Json(await _allotmentEntryService.FetchSingledocumentResult(Convert.ToInt32(leasesTypeId)));
        //}

        [HttpGet]
        public async Task<JsonResult> GetRateList(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            leasePurposeId = leasePurposeId ?? 0;
            leaseSubPurposeId = leaseSubPurposeId ?? 0;


            return Json(await _allotmentEntryService.FetchSinglerateResult(leasePurposeId, leaseSubPurposeId, allotmentDate));
            //var Data = await _allotmentEntryService.FetchSinglerateResult(leasePurposeId, leaseSubPurposeId, allotmentDate);

            //return Json(Data.PremiumRate);

        }
        [HttpGet]
        public async Task<JsonResult> GetGroundRateList(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            leasePurposeId = leasePurposeId ?? 0;
            leaseSubPurposeId = leaseSubPurposeId ?? 0;


            return Json(await _allotmentEntryService.FetchSinglegroundrentResult(leasePurposeId, leaseSubPurposeId, allotmentDate));
            //var Data = await _allotmentEntryService.FetchSinglerateResult(leasePurposeId, leaseSubPurposeId, allotmentDate);

            //return Json(Data.PremiumRate);
            

        }
        public async Task<JsonResult> GetFeeList(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            leasePurposeId = leasePurposeId ?? 0;
            leaseSubPurposeId = leaseSubPurposeId ?? 0;


            return Json(await _allotmentEntryService.FetchSinglefeeResult(leasePurposeId, leaseSubPurposeId, allotmentDate));
            //var Data = await _allotmentEntryService.FetchSinglerateResult(leasePurposeId, leaseSubPurposeId, allotmentDate);

            //return Json(Data.PremiumRate);
           
                }
        public async Task<JsonResult> GetDocumentList(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            leasePurposeId = leasePurposeId ?? 0;
            leaseSubPurposeId = leaseSubPurposeId ?? 0;


            return Json(await _allotmentEntryService.FetchSingledocumentResult(leasePurposeId, leaseSubPurposeId, allotmentDate));
            //var Data = await _allotmentEntryService.FetchSinglerateResult(leasePurposeId, leaseSubPurposeId, allotmentDate);

            //return Json(Data.PremiumRate);

        }
    }
}
