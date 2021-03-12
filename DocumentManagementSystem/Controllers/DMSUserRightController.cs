using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;

using Core.Enum;


using Model.Entity;
using Dto.Master;
using Service.IApplicationService;

using System.Collections.Generic;



namespace DocumentManagementSystem.Controllers
{
    public class DMSUserRightController : Controller
    {
        private readonly IUserRightService _userrightService;
        private readonly IDepartmentService _departmentService;
        
        public DMSUserRightController(

             IDepartmentService departmentService,
            IUserRightService userrightService)
        {
            _departmentService = departmentService;
            _userrightService = userrightService;
        }
        public async Task<IActionResult> Create()
        {
            Userprofile Model = new Userprofile();

        
            AddUserDto model = new AddUserDto()
            {
                DepartmentList = await _departmentService.GetDepartment(),

                
            };
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] UserRightsSearchDto report)
        {
            var result = await _userrightService.GetPagedUserprofile(report);
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




        public async Task<JsonResult> AddUpdateDmsRight([FromBody] List<UserRightsMapDto> model)
        {
            if (model.Count > 0)
            {
                bool result = await _userrightService.AddUpdateDmsRight(model);
                if (result)
                {
                    return Json("Permission updated successully.");
                }
                else
                {
                    return Json("Error occur during update the record.");
                }
            }
            else
            {
                return Json("Please select atleast one record.");
            }
        }

    }
}