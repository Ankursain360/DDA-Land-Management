using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
//using AutoMapper.Configuration;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace EncroachmentDemolition.Controllers
{
    public class OnlineComplaintApprovalController : BaseController
    {
        public readonly IOnlinecomplaintApprovalService _onlinecomplaintApprovalService;
        private readonly IOnlinecomplaintService _onlinecomplaintService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;


        public OnlineComplaintApprovalController(IOnlinecomplaintApprovalService onlinecomplaintApprovalService, IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService, IConfiguration configuration, IOnlinecomplaintService onlinecomplaintService)
        {
            _workflowtemplateService = workflowtemplateService;
            _onlinecomplaintApprovalService = onlinecomplaintApprovalService;
            _onlinecomplaintService = onlinecomplaintService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] OnlinecomplaintApprovalSearchDto model)
        {
            try
            {
                var result = await _onlinecomplaintApprovalService.GetPagedOnlinecomplaint(model, SiteContext.UserId);
                ViewBag.IsApproved = model.StatusId;
                return PartialView("_List", result);
            }
            catch (Exception Ex) {
                return PartialView("_List", Ex);
            }
        }




        public async Task<IActionResult> Create(int id)
        {
            var Data = await _onlinecomplaintApprovalService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, Onlinecomplaint onlinecomplaint)
        {
            var result = false;
            var Data = await _onlinecomplaintService.FetchSingleResult(id);
            Data.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            Data.LocationList = await _onlinecomplaintService.GetAllLocation();

           
            var DataFlow = await DataAsync();
            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (!DataFlow[i].parameterSkip)
                {
                    if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                    {
                        result = true;
                        if (result)
                        {
                            Approvalproccess approvalproccess = new Approvalproccess();
                            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessOnlineComplaintId").Value);
                            approvalproccess.ServiceId = onlinecomplaint.Id;
                            approvalproccess.SendFrom = SiteContext.UserId;
                            approvalproccess.PendingStatus = 1;
                            approvalproccess.Remarks = onlinecomplaint.ApprovalRemarks; ///May be comment
                            approvalproccess.Status = Convert.ToInt32(onlinecomplaint.ApprovalStatus);
                            if (i == DataFlow.Count - 1)
                                approvalproccess.SendTo = null;
                            else
                            {
                                approvalproccess.SendTo = Convert.ToInt32(DataFlow[i + 1].parameterName);
                            }
                            // if (i != DataFlow.Count - 1)  ///May be Uncomment
                            result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table

                            if (result)
                            {
                                if (i == DataFlow.Count - 1)
                                {
                                    onlinecomplaint.ApprovedStatus = 1;
                                    onlinecomplaint.PendingAt = 0;
                                }
                                else
                                {
                                    onlinecomplaint.ApprovedStatus = 0;
                                    onlinecomplaint.PendingAt = Convert.ToInt32(DataFlow[i + 1].parameterName);
                                }
                                result = await _onlinecomplaintService.UpdateBeforeApproval(id, onlinecomplaint);
                            }
                        }
                        break;
                    }

                }
            }

         

            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
            return View("Index");
        }

        private async Task<List<TemplateStructure>> DataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(18);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }


        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails(Convert.ToInt32(_configuration.GetSection("workflowPreccessOnlineComplaintId").Value), id);

            return PartialView("_HistoryDetails", Data);
        }


        public async Task<PartialViewResult> OnlineComplaintView(int id)
        {
            var Data = await _onlinecomplaintService.FetchSingleResult(id);

            Data.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            Data.LocationList = await _onlinecomplaintService.GetAllLocation();
            return PartialView("_OnlineComplaintView", Data);
        }

        [HttpGet]
        public async Task<JsonResult> GetApprovalDropdownList()  //Bind Dropdown of Approval Status
        {
            var DataFlow = await DataAsync();

            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                {
                    var dropdown = DataFlow[i].parameterAction;
                    return Json(dropdown);
                    break;
                }

            }
            return Json(DataFlow);
        }





        public async Task<FileResult> ViewLetter(int Id)
        {
            try { 
            FileHelper file = new FileHelper();
            var Data = await _onlinecomplaintService.FetchSingleResult(Id);
            string path = Data.PhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
                }
            catch(Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _onlinecomplaintService.FetchSingleResult(Id);
                string path = Data.PhotoPath;
                byte[] FileBytes = System.IO.File.ReadAllBytes(path);
                return File(FileBytes, file.GetContentType(path));

            }
        }

    }
}
