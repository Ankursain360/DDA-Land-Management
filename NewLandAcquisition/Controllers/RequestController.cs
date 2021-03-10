using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using NewLandAcquisition.Filters;
using Microsoft.Extensions.Configuration;
using System.IO;
using Core.Enum;

using Utility.Helper;

using Dto.Common;



namespace NewLandAcquisition.Controllers
{
    public class RequestController : BaseController
    {
        private readonly IRequestService _requestService;
        public IConfiguration _configuration;
        string documentPhotoPathLayout = string.Empty;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;


        public RequestController(IRequestService requestService, IConfiguration configuration, IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService)
        {
            _workflowtemplateService = workflowtemplateService;
            _requestService = requestService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
        }


        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestSearchDto model)
        {
            var result = await _requestService.GetPagedRequest(model);

            return PartialView("_List", result);
        }


        public async Task<IActionResult> Create()
        {
            Request request = new Request();
            request.IsActive = 1;
            return View(request);
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Request request)
        {
            try
            {
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                request.ReferenceNo = "TRN" + finalString;

                documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
                if (ModelState.IsValid)
                {
                    FileHelper file = new FileHelper();
                    if (request.RequestPhotos != null)
                    {
                        request.LayoutPlan = file.SaveFile(documentPhotoPathLayout, request.RequestPhotos);
                    }



                    var result = await _requestService.Create(request);
                    var DataFlow = await dataAsync();
                    
                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            request.ApprovedStatus = 0;
                            request.PendingAt = Convert.ToInt32(DataFlow[i].parameterName);
                            result = await _requestService.UpdateBeforeApproval(request.Id, request);  //Update Table details 


                            if (result)
                            {


                                Approvalproccess approvalproccess = new Approvalproccess();
                                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowRequestId").Value);
                                approvalproccess.ServiceId = request.Id;
                                 approvalproccess.SendFrom = SiteContext.UserId;
                                approvalproccess.SendTo = Convert.ToInt32(DataFlow[i].parameterName);
                                approvalproccess.PendingStatus = 1;   //1
                                approvalproccess.Status = null;   //1
                                approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
                               result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table



                            }

                            break;
                        }

                    }



                    if (result == true)


                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _requestService.GetAllRequest();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View("Index");
                    }
                }
                else
                {
                    return View(request);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(request);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _requestService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Request scheme)
        {
            //ViewBag.viewDocFile = scheme.LayoutPlan;

            documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
            if (ModelState.IsValid)
            {
                FileHelper file = new FileHelper();
                if (scheme.RequestPhotos != null)
                {
                    scheme.LayoutPlan = file.SaveFile(documentPhotoPathLayout, scheme.RequestPhotos);
                }



                try
                {
                    var result = await _requestService.Update(id, scheme);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _requestService.GetAllRequest();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(scheme);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(scheme);
                }
            }
            else
            {
                return View(scheme);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _requestService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _requestService.GetAllRequest();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _requestService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(19);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
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
