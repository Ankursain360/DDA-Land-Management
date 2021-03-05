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
using NewLandAcquisition.Filters;
using Core.Enum;

namespace NewLandAcquisition.Controllers
{
    public class RequestApprovalProcess : BaseController
    {
        public readonly IRequestApprovalProcessService _requestApprovalProcessService;
        private readonly IRequestService _requestService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;



        public RequestApprovalProcess(IRequestApprovalProcessService requestApprovalProcessService, IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService, IConfiguration configuration, IRequestService requestService)
        {
            _workflowtemplateService = workflowtemplateService;
            _requestApprovalProcessService = requestApprovalProcessService;
            _requestService = requestService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
        }



        public IActionResult Index()
        {
            return View();
        }



        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _requestApprovalProcessService.FetchSingleResult(id);
            var DataFlow = await DataAsync();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




        [HttpPost]
   //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Request request)
        {
            var result = false;
            var Data = await _requestApprovalProcessService.FetchSingleResult(id);


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
                            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowRequestId").Value);
                            approvalproccess.ServiceId = request.Id;
                            approvalproccess.SendFrom = SiteContext.UserId;
                            approvalproccess.PendingStatus = 1;
                            approvalproccess.Remarks = request.ApprovalRemarks; ///May be comment
                            approvalproccess.Status = Convert.ToInt32(request.ApprovalStatus);
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
                                    request.ApprovedStatus = 1;
                                    request.PendingAt = 0;
                                    ViewBag.result = true;
                                }
                                else
                                {
                                    request.ApprovedStatus = 0;
                                    request.PendingAt = Convert.ToInt32(DataFlow[i + 1].parameterName);
                                }
                                result = await _requestService.UpdateBeforeApproval(id, request);
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
            var Data = await _workflowtemplateService.FetchSingleResult(19);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }





        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestApprovalSearchDto model)
        {
            try
            {
                var result = await _requestApprovalProcessService.GetPagedProcessRequest(model, SiteContext.UserId);
                ViewBag.IsApproved = model.StatusId;
                return PartialView("_List", result);
            }
            catch (Exception Ex)
            {
                return PartialView("_List", Ex);
            }
        }



        public async Task<PartialViewResult> RequestView(int id)
        {
            var Data = await _requestService.FetchSingleResult(id);

         
            return PartialView("_RequestView", Data);
        }






        //[HttpGet]
        //public async Task<JsonResult> GetApprovalDropdownList()  //Bind Dropdown of Approval Status
        //{
        //    var DataFlow = await DataAsync();

        //    for (int i = 0; i < DataFlow.Count; i++)
        //    {
        //        if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
        //        {
        //            var dropdown = DataFlow[i].parameterAction;
        //            return Json(dropdown);
        //            break;
        //        }

        //    }
        //    return Json(DataFlow);
        //}



        [HttpGet]
        public async Task<JsonResult> getannexuredetails()  //Bind Dropdown of Approval Status
        {
            var result = false;
            var DataFlow = await DataAsync();

            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                {
                    result = true;
                    if (result)
                    {

                        if (i == DataFlow.Count - 1)
                        {

                          
                            return Json(ViewBag.data = true);
                        }
                        else
                        {
                            ViewBag.data = 0;
                        }
                        break;
                    }


                }
            }
           
            return Json(DataFlow);
        }





        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails(Convert.ToInt32(_configuration.GetSection("workflowRequestId").Value), id);

            return PartialView("_HistoryDetails", Data);
        }


        public async Task<FileResult> ViewLetter(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _requestService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.LayoutPlan;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _requestService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.LayoutPlan;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }




    }
}
