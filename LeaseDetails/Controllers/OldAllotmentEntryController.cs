using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace LeaseDetails.Controllers
{
    public class OldAllotmentEntryController : BaseController
    {
        private readonly IOldAllotmentEntryService _oldAllotmentEntryService;
        public OldAllotmentEntryController(IOldAllotmentEntryService oldAllotmentEntryService)
        {
            _oldAllotmentEntryService = oldAllotmentEntryService;

        }
        public async Task<IActionResult> Create()
        {
            Leaseapplication lease = new Leaseapplication();
           // lease.PropertyTypeList = await _oldAllotmentEntryService.GetAllPropertyType();
            lease.LeaseTypeList = await _oldAllotmentEntryService.GetAllLeaseType();
            lease.LeasePurposeList = await _oldAllotmentEntryService.GetAllLeasepurpose();
            lease.LeaseSubPurposeList = await _oldAllotmentEntryService.GetAllLeaseSubpurpose(lease.PurposeId);

            return View(lease);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leaseapplication lease)
        {
           // lease.PropertyTypeList = await _oldAllotmentEntryService.GetAllPropertyType();
            lease.LeaseTypeList = await _oldAllotmentEntryService.GetAllLeaseType();
            lease.LeasePurposeList = await _oldAllotmentEntryService.GetAllLeasepurpose();
            lease.LeaseSubPurposeList = await _oldAllotmentEntryService.GetAllLeaseSubpurpose(lease.PurposeId);

            lease.ContactNo = "OLdAllotment";
            lease.RegistrationNo = "OLdAllotmentEntry";
            lease.CreatedBy = SiteContext.UserId;
            lease.IsActive = 1;
            lease.ApprovedStatus = 1;
            //if (ModelState.IsValid)
            //{
           
            var result = await _oldAllotmentEntryService.Create(lease);

            if (result == true)
            {
                //************ Save Owner  ************  
                if (lease.AllotmentDate != null)
                {

                    Allotmententry entry = new Allotmententry();

                    entry.ApplicationId = lease.Id;
                    //entry.AllotedArea = lease.AllotedArea;
                    entry.AllotmentDate = lease.AllotmentDate;
                   // entry.IsPlayground = lease.IsPlayground;
                    entry.PlayGroundArea = lease.PlayGroundArea;
                    entry.PremiumRate = lease.Rate;
                    entry.PremiumAmount = lease.PremiumAmount;
                    entry.GroundRent = lease.GroundRent;
                    entry.AmountLicFee = lease.AmountLicFee;
                    entry.NoOfYears = lease.NoOfYears;
                    entry.LeasesTypeId = lease.LeaseTypeId;
                    entry.LeasePurposesTypeId = lease.PurposeId;
                    entry.LeaseSubPurposeId = lease.SubPurposeId;

                    entry.CreatedBy = SiteContext.UserId;
                    


                   var Id = await _oldAllotmentEntryService.SaveAllotmentDetails(entry);
                    if (Id !=0)
                    {
                        Possesionplan plan = new Possesionplan();

                        plan.AllotmentId = Id;
                        plan.AllotedArea = lease.AllotedArea;
                        plan.PossessionTakenDate = lease.PossessionTakenDate;


                        plan.CreatedBy = SiteContext.UserId;
                        result = await _oldAllotmentEntryService.SavepossessionDetails(plan);
                    }

                }


                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                //var list = await _jaraidetailService.GetAllJaraidetail();
                //return View("Index", list);
                return View(lease);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(lease);
            }
            //}
            //else
            //{
            //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            //    return View(lease);
            //}

        }

            [HttpGet]
            public async Task<JsonResult> GetAllLeaseSubpurpose(int? PurposeId)
            {
                PurposeId = PurposeId ?? 0;
                return Json(await _oldAllotmentEntryService.GetAllLeaseSubpurpose(Convert.ToInt32(PurposeId)));
            }
    } 
}

