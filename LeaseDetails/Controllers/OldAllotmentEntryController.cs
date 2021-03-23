using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
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
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] OLdAllotmentSearchDto model)
        {

            var result = await _oldAllotmentEntryService.GetPagedOldEntry(model);
            return PartialView("_List", result);
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
            
           
            var result = await _oldAllotmentEntryService.Create(lease);

            if (result == true)
            {
                //************ Save  old Allotmententry  ************  
                if (lease.AllotmentDate != null)
                {

                    Allotmententry entry = new Allotmententry();

                    entry.ApplicationId = lease.Id;
                    entry.OldNewEntry = "Old";
                    
                    entry.TotalArea = lease.TotalArea;
                    entry.AllotmentDate = lease.AllotmentDate;
                    entry.PlotNo = lease.PlotNo;
                    entry.BuildingArea = lease.BuildingArea;
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
                        plan.AllotedArea = lease.TotalArea;
                        plan.PossessionTakenDate = lease.PossessionTakenDate;


                        plan.CreatedBy = SiteContext.UserId;
                        result = await _oldAllotmentEntryService.SavepossessionDetails(plan);
                    }

                }


                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
              
                return View("Index");
               
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(lease);
            }
            

        }

            [HttpGet]
            public async Task<JsonResult> GetAllLeaseSubpurpose(int? PurposeId)
            {
                PurposeId = PurposeId ?? 0;
                return Json(await _oldAllotmentEntryService.GetAllLeaseSubpurpose(Convert.ToInt32(PurposeId)));
            }
        public async Task<IActionResult> View(int id)
        {

            var Data = await _oldAllotmentEntryService.FetchSingleResult(id);
            var Data2 = await _oldAllotmentEntryService.FetchSingleLeaseResult(Data.ApplicationId);
            /// var Data3 = await _oldAllotmentEntryService.FetchSinglePossessionResult(Data.Id);
            Data.LeaseTypeList = await _oldAllotmentEntryService.GetAllLeaseType();
            Data.LeasePurposeList = await _oldAllotmentEntryService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _oldAllotmentEntryService.GetAllLeaseSubpurpose(Data.LeasePurposesTypeId);
            Data.Application = Data2;
            //  Data.PossessionTakenDate = Data3.PossessionTakenDate;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        public async Task<IActionResult> Edit(int id)
        {
           
            var Data = await _oldAllotmentEntryService.FetchSingleResult(id);
            var Data2 = await _oldAllotmentEntryService.FetchSingleLeaseResult(Data.ApplicationId);
           /// var Data3 = await _oldAllotmentEntryService.FetchSinglePossessionResult(Data.Id);
            Data.LeaseTypeList = await _oldAllotmentEntryService.GetAllLeaseType();
            Data.LeasePurposeList = await _oldAllotmentEntryService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _oldAllotmentEntryService.GetAllLeaseSubpurpose(Data.LeasePurposesTypeId);
            Data.Application = Data2;
          //  Data.PossessionTakenDate = Data3.PossessionTakenDate;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Allotmententry entry)
        {
           
            entry.LeaseTypeList = await _oldAllotmentEntryService.GetAllLeaseType();
            entry.LeasePurposeList = await _oldAllotmentEntryService.GetAllLeasepurpose();
            entry.LeaseSubPurposeList = await _oldAllotmentEntryService.GetAllLeaseSubpurpose(entry.LeasePurposesTypeId);

            
            entry.ModifiedBy = SiteContext.UserId;
            var result = await _oldAllotmentEntryService.Update(id, entry);
            var result2 = await _oldAllotmentEntryService.UpdateLease(entry.ApplicationId, entry);
            var result3 = await _oldAllotmentEntryService.UpdatePossession(id, entry);
            if (result == true)
            {
                
                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

               
                return View("Index");
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(entry);

            }
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _oldAllotmentEntryService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                //var result1 = await _premiumrateService.GetAllPremiumrate();
                return View("Index");
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
               // var result1 = await _premiumrateService.GetAllPremiumrate();
                return View("Index");
            }
        }
    } 
}

