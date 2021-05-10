using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using System.Threading.Tasks;
using SiteMaster.Filters;
using Core.Enum;
using System.Collections.Generic;
using Utility.Helper;
using System;
using Dto.Master;

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

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var message = TempData["Msg"] as string;
            if (message != null)
            {
                ViewBag.Message = message;
            }
            WorkflowTemplate model = new WorkflowTemplate();
            await BindDropDown(model);
            return View(model);
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


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            WorkflowTemplate model = new WorkflowTemplate();
            await BindDropDown(model);
            var StatusList = await _workflowtemplateService.GetApprovalStatusListData();
            for (int i = 0; i < StatusList.Count; i++)
            {
                if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Revert)
                    ViewBag.RevertCodeValue = StatusList[i].Id;
                else if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Approved)
                    ViewBag.ApprovedCodeValue = StatusList[i].Id;
                else if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Forward)
                    ViewBag.ForwardCodeValue = StatusList[i].Id;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] WorkflowLevelDto WorkflowLevelDto)
        {
            WorkflowTemplate model = new WorkflowTemplate();
            //ViewBag.Items = await _userProfileService.GetRole();
            ViewBag.Items = await _userProfileService.GetUserWithRole();
            ViewBag.ApprovalStatus = await _workflowtemplateService.GetApprovalStatusListData();
            return PartialView("_Levels", model);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create([FromBody] WorkflowTemplateCreateDto workflowtemplatecreatedto)
        {
            WorkflowTemplate model = new WorkflowTemplate();
            List<string> JsonMsg = new List<string>();
            if (workflowtemplatecreatedto == null)
            {
                JsonMsg.Add("false");
                JsonMsg.Add("Please Enter Correct data");
                return Json(JsonMsg);
            }
            model.ModuleList = await _workflowtemplateService.GetAllModuleList();
            model.Name = workflowtemplatecreatedto.name;
            model.Description = workflowtemplatecreatedto.description;
            model.ModuleId = workflowtemplatecreatedto.moduleId;
            model.Slatime = workflowtemplatecreatedto.slatime;
            model.EffectiveDate = Convert.ToDateTime(workflowtemplatecreatedto.effectivedate);
            model.IsActive = workflowtemplatecreatedto.isActive;
            model.Template = workflowtemplatecreatedto.template;
            model.ProcessGuid = Guid.NewGuid().ToString();
            var ProcessGuidBasisCount = _workflowtemplateService.ProcessGuidBasisCount(model.Name);
            Random r = new Random();
            int num = r.Next();
            model.Version = "V1(" + num + ")";

            if (ModelState.IsValid)
            {
                if (model.Template != null)
                {
                    model.CreatedBy = SiteContext.UserId;
                    var result = await _workflowtemplateService.Create(model);

                    if (result == true)
                    {
                        //TempData["Msg"] = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return Json(Url.Action("Index", "WorkFlowTemplate"));
                        JsonMsg.Add("/WorkFlowTemplate/Index");
                        JsonMsg.Add("Record added successfully.");
                        return Json(JsonMsg);
                    }
                    else
                    {
                        //ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        //return Json(Url.Action("Create", "WorkFlowTemplate"));
                        JsonMsg.Add("false");
                        JsonMsg.Add("Unable to process the request.");
                        return Json(JsonMsg);
                    }
                }
                else
                {
                    //return Json(Url.Action("Create", "WorkFlowTemplate"));
                    JsonMsg.Add("false");
                    JsonMsg.Add("Unable to process the request.");
                    return Json(JsonMsg);
                }

            }
            else
            {
                //return Json(Url.Action("Create", "WorkFlowTemplate"));
                JsonMsg.Add("false");
                JsonMsg.Add("Unable to process the request.");
                return Json(JsonMsg);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _workflowtemplateService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data.IsActive == 1)
                Data.IsActiveData = true;
            else
                Data.IsActiveData = false;

            var StatusList = await _workflowtemplateService.GetApprovalStatusListData();
            for (int i = 0; i < StatusList.Count; i++)
            {
                if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Revert)
                    ViewBag.RevertCodeValue = StatusList[i].Id;
                else if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Approved)
                    ViewBag.ApprovedCodeValue = StatusList[i].Id;
                else if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Forward)
                    ViewBag.ForwardCodeValue = StatusList[i].Id;
            }

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
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Edit([FromBody] WorkflowTemplateCreateDto workflowtemplatecreatedto)
        {
            WorkflowTemplate model = new WorkflowTemplate();
            List<string> JsonMsg = new List<string>();
            if (workflowtemplatecreatedto == null)
            {
                JsonMsg.Add("false");
                JsonMsg.Add("Please Enter Correct data");
                return Json(JsonMsg);
            }
            model.ModuleList = await _workflowtemplateService.GetAllModuleList();
            model.Name = workflowtemplatecreatedto.name;
            model.Description = workflowtemplatecreatedto.description;
            model.ModuleId = workflowtemplatecreatedto.moduleId;
            model.Slatime = workflowtemplatecreatedto.slatime;
            model.EffectiveDate = Convert.ToDateTime(workflowtemplatecreatedto.effectivedate);
            model.IsActive = workflowtemplatecreatedto.isActive;
            model.Template = workflowtemplatecreatedto.template;
            int id = workflowtemplatecreatedto.Id;

            var Data = await _workflowtemplateService.FetchSingleResult(id);
            model.ProcessGuid = Data.ProcessGuid;

            var ProcessGuidBasisCount = _workflowtemplateService.ProcessGuidBasisCount(Data.ProcessGuid);
            Random r = new Random();
            int num = r.Next();
            model.Version = "V" + (ProcessGuidBasisCount + 1) + "(" + num + ")";

            model.CreatedBy = SiteContext.UserId;
            //var result = await _workflowtemplateService.Update(id, model);
            var result = await _workflowtemplateService.Create(model);
            if (result == true)
            {
                JsonMsg.Add("/WorkFlowTemplate/Index");
                JsonMsg.Add("New Version Created Sucessfully");
                // return Json(Url.Action("Index", "WorkFlowTemplate"));
                return Json(JsonMsg);
            }
            else
            {
                JsonMsg.Add("false");
                JsonMsg.Add("Unable to process the request.");
                return Json(JsonMsg);

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


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _workflowtemplateService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data.IsActive == 1)
                Data.IsActiveData = true;
            else
                Data.IsActiveData = false;

            var StatusList = await _workflowtemplateService.GetApprovalStatusListData();
            for (int i = 0; i < StatusList.Count; i++)
            {
                if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Revert)
                    ViewBag.RevertCodeValue = StatusList[i].Id;
                else if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Approved)
                    ViewBag.ApprovedCodeValue = StatusList[i].Id;
                else if (StatusList[i].StatusCode == (int)ApprovalActionStatus.Forward)
                    ViewBag.ForwardCodeValue = StatusList[i].Id;
            }

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
                var data = await _userProfileService.GetUserWithRole();
                return Json(data);
            }
        }


        public async Task<IActionResult> Download()
        {
            List<WorkflowTemplate> result = await _workflowtemplateService.GetAllWorkflowTemplate();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"WorkFlowTemplate.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

        public async Task<IActionResult> WorkFlowTemplateList()
        {
            var result = await _workflowtemplateService.GetAllWorkflowTemplate();
            List<WorkFlowTemplateListDto> data = new List<WorkFlowTemplateListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new WorkFlowTemplateListDto()
                    {
                        Id = result[i].Id,
                        Version = result[i].Version,
                        SLATimeLine = result[i].Slatime.ToString(),
                        Name = result[i].Name,
                        Description = result[i].Description,
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }



    }
}
