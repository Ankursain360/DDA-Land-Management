using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;

namespace SiteMaster.Controllers
{
    public class WorkFlowTemplateController : Controller
    {

        private readonly IWorkflowTemplateService _workflowtemplateService;

        public WorkFlowTemplateController(IWorkflowTemplateService workflowtemplateService)
        {
            _workflowtemplateService = workflowtemplateService;
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
        public async Task<IActionResult> Create([FromBody] WorkflowTemplateCreateDto workflowtemplatecreatedto)
        {
            WorkflowTemplate model = new WorkflowTemplate();
            model.ModuleList = await _workflowtemplateService.GetAllModuleList();
            model.Name = workflowtemplatecreatedto.name;
            model.Description = workflowtemplatecreatedto.description;
            model.ModuleId = workflowtemplatecreatedto.moduleId;
            model.IsActive = workflowtemplatecreatedto.isActive;
            model.Template = workflowtemplatecreatedto.template;

            if (ModelState.IsValid)
            {
                var result = await _workflowtemplateService.Create(model);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return View("Index");
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
            return Json(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkflowTemplate workflowtemplate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _workflowtemplateService.Update(id, workflowtemplate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _workflowtemplateService.GetAllWorkflowTemplate();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(workflowtemplate);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(workflowtemplate);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _workflowtemplateService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"WorkflowTemplate: {Name} already exist");
            }
        }


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _workflowtemplateService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }

            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View(form);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _workflowtemplateService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _workflowtemplateService.GetAllWorkflowTemplate();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _workflowtemplateService.GetAllWorkflowTemplate();
                return View("Index", result1);
            }

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _workflowtemplateService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
