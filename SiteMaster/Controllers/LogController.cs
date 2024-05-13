using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using System.Data;
using Newtonsoft.Json;
using Utility.Helper;
using System.Collections.Generic;

namespace SiteMaster.Controllers
{
    public class LogController : BaseController
    {
        private readonly ILogService _logService;


        public LogController(ILogService logService)
        {
            _logService = logService;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LogSearchDto model)
        {
            var result = await _logService.GetPagedLog(model);

            return PartialView("_List", result);
        }

      



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _logService.FetchSingleResult(id);
            

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Log> result = await _logService.GetLog();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Log.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }


    }
}
