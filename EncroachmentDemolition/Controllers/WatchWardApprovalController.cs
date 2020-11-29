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
    public class WatchWardApprovalController : BaseController
    {
        public readonly IWatchAndWardApprovalService _watchAndWardApprovalService;
        private readonly IWatchandwardService _watchandwardService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        public WatchWardApprovalController(IWatchAndWardApprovalService watchAndWardApprovalService, IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService, IConfiguration configuration, IWatchandwardService watchandwardService)
        {
            _workflowtemplateService = workflowtemplateService;
            _watchAndWardApprovalService = watchAndWardApprovalService;
            _watchandwardService = watchandwardService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] WatchandwardApprovalSearchDto model)
        {
            var result = await _watchAndWardApprovalService.GetPagedWatchandward(model, SiteContext.UserId);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _watchAndWardApprovalService.FetchSingleResult(id);
            //Data.VillageList = await _watchAndWardApprovalService.GetAllVillage();
            //Data.LocalityList = await _watchAndWardApprovalService.GetAllLocality();
            //Data.KhasraList = await _watchAndWardApprovalService.GetAllKhasra();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, Watchandward watchandward)
        {
            var result = false;
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
            var DataFlow = await DataAsync();
            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (!DataFlow[i].parameterSkip)
                {
                    if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                    {
                        //int previousApprovalId = _approvalproccessService.GetPreviousApprovalId(Convert.ToInt32(_configuration.GetSection("workflowPreccessId").Value), watchandward.Id);
                        //Approvalproccess approvalproccess1 = new Approvalproccess();
                        //// approvalproccess1.Remarks = watchandward.ApprovalRemarks;  ///May be Uncomment
                        //approvalproccess1.PendingStatus = 0;
                        //approvalproccess1.Status = Convert.ToInt32(watchandward.ApprovalStatus);
                        //result = await _approvalproccessService.UpdatePreviousApprovalProccess(previousApprovalId, approvalproccess1, SiteContext.UserId);
                        result = true;  ///May be comment
                        if (result)
                        {
                            Approvalproccess approvalproccess = new Approvalproccess();
                            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessId").Value);
                            approvalproccess.ServiceId = watchandward.Id;
                            approvalproccess.SendFrom = SiteContext.UserId;
                            approvalproccess.PendingStatus = 1;
                            approvalproccess.Remarks = watchandward.ApprovalRemarks; ///May be comment
                            approvalproccess.Status = Convert.ToInt32(watchandward.ApprovalStatus);
                            if (i == DataFlow.Count - 1)
                                approvalproccess.SendTo =null;
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
                                    watchandward.ApprovedStatus = 0;
                                    watchandward.PendingAt = 0;
                                }
                                else
                                {
                                    watchandward.ApprovedStatus = 1;
                                    watchandward.PendingAt = Convert.ToInt32(DataFlow[i + 1].parameterName);
                                }
                                result = await _watchandwardService.UpdateBeforeApproval(id, watchandward);
                            }
                        }
                        break;
                    }

                }
            }

            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
            return View("Index");
        }

        public async Task<PartialViewResult> WatchWardView(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            return PartialView("_WatchWardView", Data);
        }

        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails(Convert.ToInt32(_configuration.GetSection("workflowPreccessId").Value), id);

            return PartialView("_HistoryDetails", Data);
        }

        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string filename = Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<FileResult> ViewDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string path = Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        private async Task<List<TemplateStructure>> DataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(2);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }

        [HttpGet]
        public async Task<JsonResult> GetApprovalDropdownList()
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
    }
}
