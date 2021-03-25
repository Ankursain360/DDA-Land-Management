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
namespace LeaseDetails.Controllers
{
    public class AllotmentEntryController : BaseController
    {
        private readonly IAllotmentEntryService _allotmentEntryService;


        public AllotmentEntryController(IAllotmentEntryService allotmentEntryService)
        {
            _allotmentEntryService = allotmentEntryService;
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
                allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(allotmententry.PurposeId);

                if (ModelState.IsValid)
                {
                    var result = await _allotmentEntryService.Create(allotmententry);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _allotmentEntryService.GetAllAllotmententry();
                        return View("Index", list);
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
            Data.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(Data.PurposeId);

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
            allotmententry.LeaseSubPurposeList = await _allotmentEntryService.GetAllLeaseSubpurpose(allotmententry.PurposeId);
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
        [HttpGet]
        public async Task<JsonResult> GetDocumentList(int? leasesTypeId)
        {
            leasesTypeId = leasesTypeId ?? 0;

            return Json(await _allotmentEntryService.FetchSingledocumentResult(Convert.ToInt32(leasesTypeId)));
        }

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
    }
}
