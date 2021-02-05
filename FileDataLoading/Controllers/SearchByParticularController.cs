using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace FileDataLoading.Controllers
{
    public class SearchByParticularController : BaseController
    {
        private readonly IDataStorageService _dataStorageService;
        public SearchByParticularController(IDataStorageService dataStorageService)
        {
            _dataStorageService = dataStorageService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            SearchByParameterDtoProfile datastoragedetails = new SearchByParameterDtoProfile();
           // ViewBag.FileNoList = await _dataStorageService.BindFileNoList();
            ViewBag.LocalityList = await _dataStorageService.GetLocalities();
            ViewBag.departmentList = await _dataStorageService.GetDepartment();
            ViewBag.almirahList = await _dataStorageService.GetAlmirahs();
            ViewBag.rowList = await _dataStorageService.GetRows();
            ViewBag.bundleList = await _dataStorageService.GetBundles();
            ViewBag.columnList = await _dataStorageService.GetColumns();
            
            return View(datastoragedetails);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] SearchByParticularSearchDto model)
        {
            int UserId = SiteContext.UserId;
            var result = await _dataStorageService.GetPagedListofSearchByParticular(model,UserId);

            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }
        public async Task<PartialViewResult> List([FromBody] SearchByParticularSearchDto model)
        {
            int UserId = SiteContext.UserId;
            var result = await _dataStorageService.GetPagedListofSearchByParticular(model, UserId);
            return PartialView("_List", result);
        }
        //  [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
           //  var Data = await _dataStorageService.FetchSingleResult(id);
           var Data = await _dataStorageService.GetDatastorageListName(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetFileHistory([FromBody] SearchByParticularFileHistorySearchDto model)
        {
            
            var result = await _dataStorageService.GetPagedListofFileHistory(model);

            if (result != null)
            {
                return PartialView("_ListHistory", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }
        //public async Task<PartialViewResult> List([FromBody] SearchByParticularSearchDto model)
        //{
        //    int UserId = SiteContext.UserId;
        //    var result = await _dataStorageService.GetPagedListofSearchByParticular(model, UserId);
        //    return PartialView("_List", result);
        //}
    }
}


