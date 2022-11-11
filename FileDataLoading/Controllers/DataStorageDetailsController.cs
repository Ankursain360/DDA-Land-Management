
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
using FileDataLoading.Filters;
using Core.Enum;
using Dto.Master;

namespace FileDataLoading.Controllers
{
    public class DataStorageDetailsController : BaseController
    {
        private readonly IDataStorageService _datastorageService;

        public DataStorageDetailsController(IDataStorageService datastorageService)
        {
            _datastorageService = datastorageService;
        }



        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Datastoragedetails datastoragedetails = new Datastoragedetails();
            datastoragedetails.AlmirahList = await _datastorageService.GetAlmirahs();
            datastoragedetails.RowList = await _datastorageService.GetRows();
            datastoragedetails.ZoneList = await _datastorageService.GetZones();
            datastoragedetails.BundleList = await _datastorageService.GetBundles();
            datastoragedetails.ColumnList = await _datastorageService.GetColumns();
            datastoragedetails.LocalityList = await _datastorageService.GetLocalities();         
            datastoragedetails.SchemeFileLoadingList = await _datastorageService.GetSchemesFileLoading();
            return View(datastoragedetails);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DataStorgaeDetailsSearchDto model)
        {
            var result = await _datastorageService.GetPagedDataStorageDetails(model);
            return PartialView("_List", result);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Datastoragedetails dataStorageDetails)
        {
            try
            {
              
                if (dataStorageDetails.IsPartOfMainFile == 0)
                {
                    ModelState.Remove("SchemeDptBranch");                   
                    ModelState.Remove("LocalityIdForPartFile");
                    ModelState.Remove("IsPartOfMainFile");
                    ModelState.Remove("YearForPartFile");
                }
                if (ModelState.IsValid)
                {
                    dataStorageDetails.FileNo = (dataStorageDetails.CategoryNo + "/" + dataStorageDetails.SequenceNo + "/" + dataStorageDetails.HeaderNo + dataStorageDetails.Year).ToString();
                    dataStorageDetails.DepartmentId = SiteContext.DepartmentId;

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
                                    YearofPartFile = dataStorageDetails.YearForPartFile[i],
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


        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Datastoragedetails datastoragedetails)
        {
            try
            {
                datastoragedetails.AlmirahList = await _datastorageService.GetAlmirahs();
                datastoragedetails.RowList = await _datastorageService.GetRows();
                datastoragedetails.ZoneList = await _datastorageService.GetZones();
                datastoragedetails.BundleList = await _datastorageService.GetBundles();
                datastoragedetails.ColumnList = await _datastorageService.GetColumns();
                datastoragedetails.LocalityList = await _datastorageService.GetLocalities();
                datastoragedetails.SchemeFileLoadingList = await _datastorageService.GetSchemesFileLoading();
                datastoragedetails.ZoneList = await _datastorageService.GetZones();
                if (datastoragedetails.IsPartOfMainFile == 0)
                {
                    ModelState.Remove("SchemeDptBranch");
                    ModelState.Remove("LocalityIdForPartFile");
                    ModelState.Remove("IsPartOfMainFile");
                    ModelState.Remove("YearForPartFile");
                }
                if (ModelState.IsValid)
                {

                    //For File No Add Category No,Header No,Sequence No,From Year and To Year
                    datastoragedetails.FileNo = (datastoragedetails.CategoryNo + "/" + datastoragedetails.SequenceNo + "/" + datastoragedetails.HeaderNo + datastoragedetails.Year).ToString();
                    datastoragedetails.DepartmentId = SiteContext.DepartmentId;
                    var result = await _datastorageService.Update(id, datastoragedetails);

                    if (result)
                    {
                        if(datastoragedetails.IsPartOfMainFile==1) //For Part File
                        { 
                          FileHelper fileHelper = new FileHelper();
                            if (
                                datastoragedetails.Category != null &&
                                datastoragedetails.Header != null &&
                                datastoragedetails.SequenceNoForPartFile != null &&
                                datastoragedetails.Subject != null &&
                                datastoragedetails.LocalityIdForPartFile.Count > 0 &&
                                datastoragedetails.SchemeDptBranch.Count > 0)
                            {
                                List<Datastoragepartfilenodetails> datastoragepartfilenodetails = new List<Datastoragepartfilenodetails>();
                                result = await _datastorageService.DeleteDataStoragePartFile(id);
                                for (int i = 0; i < datastoragedetails.Category.Count; i++)
                                {

                                    datastoragepartfilenodetails.Add(new Datastoragepartfilenodetails
                                    {

                                        Id = 0,
                                        Category = datastoragedetails.Category[i],
                                        Header = datastoragedetails.Header[i],
                                        SequenceNo = datastoragedetails.SequenceNoForPartFile[i],
                                        Subject = datastoragedetails.Subject[i],
                                        LocalityId = datastoragedetails.LocalityIdForPartFile[i],
                                        SchemeDptBranch = datastoragedetails.SchemeDptBranch[i],
                                        YearofPartFile = datastoragedetails.YearForPartFile[i],
                                        DataStorageDetailsId = datastoragedetails.Id,

                                    }) ;
                                   
                                   
                                }
                                    result = await _datastorageService.SaveDetailsOfPartFile(datastoragepartfilenodetails);
                            }

                            

                           
                        }

                        if (result)
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
                    else
                    {
                        return View(datastoragedetails);
                    }
                }
                return View(datastoragedetails);
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(datastoragedetails);
            }
        }





        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            Datastoragedetails datastoragedetails = new Datastoragedetails();
            datastoragedetails =await _datastorageService.FetchSingleResult(id);
            datastoragedetails.AlmirahList = await _datastorageService.GetAlmirahs();
            datastoragedetails.RowList = await _datastorageService.GetRows();
            datastoragedetails.ZoneList = await _datastorageService.GetZones();
            datastoragedetails.BundleList = await _datastorageService.GetBundles();
            datastoragedetails.ColumnList = await _datastorageService.GetColumns();
            datastoragedetails.LocalityList = await _datastorageService.GetLocalities();
            datastoragedetails.SchemeFileLoadingList = await _datastorageService.GetSchemesFileLoading();
           
            if (datastoragedetails == null)
            {
                return NotFound();
            }
            return View(datastoragedetails);
        }


        public async Task<JsonResult> DetailsOfRepeater(int? Id)
        {
            Id = Id ?? 0;
            var data = await _datastorageService.GetDetailsOfPartFileDetails(Convert.ToInt32(Id));
          return Json(data);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {

            Datastoragedetails datastoragedetails = new Datastoragedetails();
            datastoragedetails = await _datastorageService.FetchSingleResult(id);
            datastoragedetails.AlmirahList = await _datastorageService.GetAlmirahs();
            datastoragedetails.RowList = await _datastorageService.GetRows();
            datastoragedetails.ZoneList = await _datastorageService.GetZones();
            datastoragedetails.BundleList = await _datastorageService.GetBundles();
            datastoragedetails.ColumnList = await _datastorageService.GetColumns();
            datastoragedetails.LocalityList = await _datastorageService.GetLocalities();
            datastoragedetails.SchemeFileLoadingList = await _datastorageService.GetSchemesFileLoading();

            if (datastoragedetails == null)
            {
                return NotFound();
            }
            return View(datastoragedetails);
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
       // [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> DatastoragedetailsList([FromBody] DataStorgaeDetailsSearchDto model)
        {
            var result = await _datastorageService.GetAllDataStorageDetailsList(model);
            List<DatastoragedetailsListDto> data = new List<DatastoragedetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DatastoragedetailsListDto()
                    {
                        Id = result[i].Id,
                        FileNo = result[i].FileNo,
                        NameSubject = result[i].Name,
                        RecordRoomNo = result[i].RecordRoomNo,
                        AlNoCompactorNo = result[i].Almirah == null ? "" : result[i].Almirah.AlmirahNo,
                        RowNo = result[i].Row == null ? "" : result[i].Row.RowNo,
                        ColNo = result[i].Column == null ? "" : result[i].Column.ColumnNo,
                        BnNo = result[i].Bundle == null ? "" : result[i].Bundle.BundleNo,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }

        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}