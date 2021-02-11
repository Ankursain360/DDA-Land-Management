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
    public class WeeklyFileReportController : BaseController
    {
        private readonly IDepartmenttargetService _departmenttargetService;
        public WeeklyFileReportController(IDepartmenttargetService departmenttargetService)
        {
            _departmenttargetService = departmenttargetService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            WeeklyFileReportDtoProfile departmenttarget = new WeeklyFileReportDtoProfile();
            ViewBag.departmentList = await _departmenttargetService.GetAllDepartment();
            return View(departmenttarget);
            //Departmenttarget model = new Departmenttarget();
            //model.IsActive = 1;
            //model.DepartmentList = await _departmenttargetService.GetAllDepartment();
            //return View(model);
        }
        public async Task<IActionResult> Create()
        {
            WeeklyFileReportDtoProfile departmenttarget = new WeeklyFileReportDtoProfile();
            ViewBag.departmentList = await _departmenttargetService.GetAllDepartment();
            return View(departmenttarget);
            //Departmenttarget model = new Departmenttarget();
            //model.IsActive = 1;
            //model.DepartmentList = await _departmenttargetService.GetAllDepartment();
            //return View(model);
        }
        public async Task<PartialViewResult> List([FromBody] WeeklyFileReportSearchDto model)
        {
            int UserId = SiteContext.UserId;
            var result = await _departmenttargetService.GetPagedWeeklyFileReport(model, UserId);
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
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] WeeklyFileReportSearchDto model)
        {
            int UserId = SiteContext.UserId;
            var result = await _departmenttargetService.GetPagedWeeklyFileReport(model, UserId);

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

    }
}
