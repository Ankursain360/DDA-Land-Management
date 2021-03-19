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
            lease.PropertyTypeList = await _oldAllotmentEntryService.GetAllPropertyType();
            lease.LeaseTypeList = await _oldAllotmentEntryService.GetAllLeaseType();
            lease.LeasePurposeList = await _oldAllotmentEntryService.GetAllLeasepurpose();
            lease.LeaseSubPurposeList = await _oldAllotmentEntryService.GetAllLeaseSubpurpose(lease.PurposeId);

            return View(lease);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leaseapplication lease)
        {
            lease.ContactNo = "OLdAllotment";
            lease.RegistrationNo = "OLdAllotmentEntry";
            lease.CreatedBy = SiteContext.UserId;
            lease.IsActive = 1;
            lease.ApprovedStatus = 1;
            //if (ModelState.IsValid)
            //{
            lease.PropertyTypeList = await _oldAllotmentEntryService.GetAllPropertyType();
            var result = await _oldAllotmentEntryService.Create(lease);

                //if (result == true)
                //{
                //    //************ Save Owner  ************  
                //    if (jarai.OwnerName != null &&
                //        jarai.FatherName != null &&
                //        jarai.Address != null)

                //    {
                //        if (jarai.FatherName.Count > 0 &&
                //            jarai.FatherName.Count > 0 &&
                //            jarai.Address.Count > 0
                //           )

                //        {
                //            List<Jaraiowner> owner = new List<Jaraiowner>();
                //            for (int i = 0; i < jarai.OwnerName.Count; i++)
                //            {
                //                owner.Add(new Jaraiowner
                //                {
                //                    OwnerName = jarai.OwnerName.Count <= i ? string.Empty : jarai.OwnerName[i],
                //                    FatherName = jarai.FatherName.Count <= i ? string.Empty : jarai.FatherName[i],
                //                    Address = jarai.Address.Count <= i ? string.Empty : jarai.Address[i],
                //                    JaraiDetailId = jarai.Id,
                //                    CreatedBy = SiteContext.UserId
                //                });
                //            }
                //            foreach (var item in owner)
                //            {
                //                result = await _jaraidetailService.SaveOwner(item);
                //            }
                //        }
                //    }






                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                //var list = await _jaraidetailService.GetAllJaraidetail();
                //return View("Index", list);
                return View(lease);
                //}
                //else
                //{
                //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                //    return View(jarai);
                //}
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
