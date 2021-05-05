
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using SiteMaster.Filters;
using Utility.Helper;

using Dto.Master;

namespace SiteMaster.Controllers
{

    public class LicenceFeesController : BaseController
    {
        private readonly ILicenceFeesService _licenceFeesService;

        public LicenceFeesController(ILicenceFeesService licenceFeesService)
        {
            _licenceFeesService = licenceFeesService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LicencefeesSearchDto model)
        {

            var result = await _licenceFeesService.GetPagedLicencefees(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Licencefees licencefees = new Licencefees();
            licencefees.IsActive = 1;
            licencefees.LeasePurposeList = await _licenceFeesService.GetAllLeasepurpose();
            licencefees.LeaseSubPurposeList = await _licenceFeesService.GetAllLeaseSubpurpose(licencefees.LeasePurposesTypeId);
            return View(licencefees);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Licencefees licfees)
        {
            licfees.LeasePurposeList = await _licenceFeesService.GetAllLeasepurpose();
            licfees.LeaseSubPurposeList = await _licenceFeesService.GetAllLeaseSubpurpose(licfees.LeasePurposesTypeId);
            try
            {

                if (ModelState.IsValid)
                {

                    licfees.CreatedBy = SiteContext.UserId;
                    var result = await _licenceFeesService.Create(licfees);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return View();
                        var list = await _licenceFeesService.GetAllLicencefees();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(licfees);

                    }
                }
                else
                {
                    return View(licfees);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(licfees);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _licenceFeesService.FetchSingleResult(id);
            Data.LeasePurposeList = await _licenceFeesService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _licenceFeesService.GetAllLeaseSubpurpose(Data.LeasePurposesTypeId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Licencefees licfees)
        {
            licfees.LeasePurposeList = await _licenceFeesService.GetAllLeasepurpose();
            licfees.LeaseSubPurposeList = await _licenceFeesService.GetAllLeaseSubpurpose(licfees.LeasePurposesTypeId);
            if (ModelState.IsValid)
            {
                try
                {
                    licfees.ModifiedBy = SiteContext.UserId;
                    var result = await _licenceFeesService.Update(id, licfees);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _licenceFeesService.GetAllLicencefees();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(licfees);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(licfees);

                }
            }
            return View(licfees);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _licenceFeesService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _licenceFeesService.GetAllLicencefees();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _licenceFeesService.GetAllLicencefees();
                return View("Index", result1);
            }
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _licenceFeesService.FetchSingleResult(id);
            Data.LeasePurposeList = await _licenceFeesService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _licenceFeesService.GetAllLeaseSubpurpose(Data.LeasePurposesTypeId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Licencefees> result = await _licenceFeesService.GetAllLicencefees();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"LicenceFees.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
        [HttpGet]
        public async Task<JsonResult> GetAllLeaseSubpurpose(int? purposeUseId)
        {
            purposeUseId = purposeUseId ?? 0;
            return Json(await _licenceFeesService.GetAllLeaseSubpurpose(Convert.ToInt32(purposeUseId)));
        }

        public async Task<IActionResult> LicenceFeesList()
        {
            var result = await _licenceFeesService.GetAllLicencefeesList();
            List<LicenceFeesListDto> data = new List<LicenceFeesListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LicenceFeesListDto()
                    {
                        Id = result[i].Id,
                       
                        LeaseSubPurpose = result[i].LeaseSubPurpose == null ? "" : result[i].LeaseSubPurpose.SubPurposeUse.ToString(),
                        LicenceFees = result[i].LicenceFees.ToString(),
                        FromDate = result[i].FromDate.ToString("dd/MM/yyyy"),
                        ToDate = result[i].ToDate.ToString("dd/MM/yyyy"),


                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



    }
}

