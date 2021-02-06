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

namespace FileDataLoading.Controllers
{
    public class ListOfTotalDocReportUserWiseController : BaseController
    {
        private readonly IDataStorageService _dataStorageService;
        public ListOfTotalDocReportUserWiseController(IDataStorageService dataStorageService)
        {
            _dataStorageService = dataStorageService;
        }
        //[AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<PartialViewResult> List([FromBody] ListOfTotalDocReportUserWiseSearchDto model)
        {
            int UserId = SiteContext.UserId;
            var result = await _dataStorageService.GetPagedListofReportDoc(model, UserId);
            return PartialView("_List", result);
        }
        //  [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _dataStorageService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
