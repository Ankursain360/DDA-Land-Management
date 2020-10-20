using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteMaster.Controllers
{
    public class WorkFlowTemplateController : BaseController
    {

        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IUserProfileService _userProfileService;

        public WorkFlowTemplateController(IWorkflowTemplateService workflowtemplateService, IUserProfileService userProfileService)
        {
            _workflowtemplateService = workflowtemplateService;
            _userProfileService = userProfileService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] WorkflowTemplateSearchDto model)
        {
            var result = await _workflowtemplateService.GetPagedWorkflowTemplate(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(WorkflowTemplate workflowtemplate)
        {
            workflowtemplate.ModuleList = await _workflowtemplateService.GetAllModuleList();
        }

        public async Task<IActionResult> Create()
        {
            WorkflowTemplate model = new WorkflowTemplate();
            await BindDropDown(model);
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] WorkflowLevelDto WorkflowLevelDto)
        {
            WorkflowTemplate model = new WorkflowTemplate();
          //  model.OperationId = WorkflowLevelDto.opertaionId;
            //if (model.OperationId == "Role")
            //{
                ViewBag.Items = await _workflowtemplateService.GetRolelist();
            //}
            //else
            //{
            //    ViewBag.Items = await _workflowtemplateService.GetUserlist();
            //}
            
            return  PartialView("_Levels", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkflowTemplateCreateDto workflowtemplatecreatedto)
        {
            WorkflowTemplate model = new WorkflowTemplate();
            model.ModuleList = await _workflowtemplateService.GetAllModuleList();
            model.Name = workflowtemplatecreatedto.name;
            model.Description = workflowtemplatecreatedto.description;
            model.ModuleId = workflowtemplatecreatedto.moduleId;
            model.UserType = workflowtemplatecreatedto.usertype;
            model.IsActive = workflowtemplatecreatedto.isActive;
            model.Template = workflowtemplatecreatedto.template;

            if (ModelState.IsValid)
            {
                var result = await _workflowtemplateService.Create(model);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return Json(Url.Action("Index", "WorkFlowTemplate"));
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(model);

                }
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _workflowtemplateService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data.IsActive == 1)
                Data.IsActiveData = true;
            else
                Data.IsActiveData = false;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpGet]
        public async Task<JsonResult> GetTaskDetails(int id)
        {
            var Data = await _workflowtemplateService.FetchSingleResult(id);
            var template = Data.Template;
            return Json(template);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] WorkflowTemplateCreateDto workflowtemplatecreatedto)
        {
            WorkflowTemplate model = new WorkflowTemplate();
            model.ModuleList = await _workflowtemplateService.GetAllModuleList();
            model.Name = workflowtemplatecreatedto.name;
            model.Description = workflowtemplatecreatedto.description;
            model.ModuleId = workflowtemplatecreatedto.moduleId;
            model.IsActive = workflowtemplatecreatedto.isActive;
            model.Template = workflowtemplatecreatedto.template;
            int id = workflowtemplatecreatedto.Id;

            var result = await _workflowtemplateService.Update(id, model);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                return Json(Url.Action("Index", "WorkFlowTemplate"));
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(model);

            }
        }
        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _workflowtemplateService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                return View("Index");
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View("Index");
            }

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _workflowtemplateService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data.IsActive == 1)
                Data.IsActiveData = true;
            else
                Data.IsActiveData = false;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpGet]
        public async Task<JsonResult> GetUserList(string value)
        {
            if (value == "Role")
            {
                var data = await _userProfileService.GetRole();
                return Json(data);
            }
            else
            {
                var data = await _userProfileService.GetUser();
                return Json(data);
            }
        }

    }
}
