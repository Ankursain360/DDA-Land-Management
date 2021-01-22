
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace FileDataLoading.Controllers
{
    public class DataStorageDetailsController : BaseController
    {
        private readonly IDataStorageService _datastorageService;

        public DataStorageDetailsController(IDataStorageService datastorageService)
        {
            _datastorageService = datastorageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            Datastoragedetails datastoragedetails = new Datastoragedetails();
            datastoragedetails.AlmirahList = await _datastorageService.GetAlmirahs();
            datastoragedetails.RowList = await _datastorageService.GetRows();
            datastoragedetails.ZoneList = await _datastorageService.GetZones();
            datastoragedetails.BundleList = await _datastorageService.GetBundles();
            datastoragedetails.ColumnList = await _datastorageService.GetColumns();
            datastoragedetails.LocalityList = await _datastorageService.GetLocalities();
            datastoragedetails.schemaList = await _datastorageService.GetSchemes();
            return View(datastoragedetails);
        }




        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DataStorgaeDetailsSearchDto model)
        {
            var result = await _datastorageService.GetPagedDataStorageDetails(model);
            return PartialView("_List", result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Datastoragedetails dataStorageDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _datastorageService.Create(dataStorageDetails);

                    if (result)
                    {
                        FileHelper fileHelper = new FileHelper();
                        if (
                            dataStorageDetails.Category != null &&
                            dataStorageDetails.Header != null &&
                            dataStorageDetails.SequenceNoForPartFile != null &&
                            dataStorageDetails.Subject != null &&
                            dataStorageDetails.LocalityIdForPartFile.Count > 0 &&
                            dataStorageDetails.SchemeDptBranch.Count > 0)
                        {
                            List<Datastoragepartfilenodetails> datastoragepartfilenodetails = new List<Datastoragepartfilenodetails>();
                            for (int i = 0; i < dataStorageDetails.Category.Count; i++)
                            {
                                datastoragepartfilenodetails.Add(new Datastoragepartfilenodetails
                                {
                                    Category = dataStorageDetails.Category[i],
                                    Header = dataStorageDetails.Header[i],
                                    SequenceNo = dataStorageDetails.SequenceNoForPartFile[i],
                                    Subject = dataStorageDetails.Subject[i],
                                    LocalityId = dataStorageDetails.LocalityIdForPartFile[i],
                                    SchemeDptBranch = dataStorageDetails.SchemeDptBranch[i],
                                    Year = dataStorageDetails.YearForPartFile[i],
                                    DataStorageDetailsId = dataStorageDetails.Id,
                                    CreatedBy = SiteContext.UserId,
                                    CreatedDate = DateTime.Now
                                });
                            }
                            result = await _datastorageService.SaveDetailsOfPartFile(datastoragepartfilenodetails);
                        }

                        if (result)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var list = await _datastorageService.GetAllDataStorageDetail();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(dataStorageDetails);

                        }
                    }
                    else
                    {
                        return View(dataStorageDetails);
                    }
                }
                else
                {
                    return View(dataStorageDetails);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(dataStorageDetails);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _datastorageService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Datastoragedetails datastoragedetails)
        {
            if (ModelState.IsValid)
            {
                var result = await _datastorageService.Update(id, datastoragedetails);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                    var list = await _datastorageService.GetAllDataStorageDetail();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(datastoragedetails);
                }
            }
            return View(datastoragedetails);
        }


        public async Task<IActionResult> View(int id)
        {
            var Data = await _datastorageService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            var result = await _datastorageService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "DataStorageDetailsDetails");
        }


        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            try
            {
                var result = await _datastorageService.Delete(id);
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
            var list = await _datastorageService.GetAllDataStorageDetail();
            return View("Index", list);
        }

    }
}