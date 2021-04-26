using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using EncroachmentDemolition.Filters;
using Core.Enum;
using Dto.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EncroachmentDemolition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] WatchandwardApprovalSearchDto model)
        {
            var result = await _watchAndWardApprovalService.GetPagedWatchandward(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _watchAndWardApprovalService.FetchSingleResult(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Watchandward watchandward)
        {
            var result = false;
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            //#region Approval Proccess At Further level start Added by Renu 27 Nov 2020
            //var DataFlow = await DataAsync();
            //for (int i = 0; i < DataFlow.Count; i++)
            //{
            //    if (!DataFlow[i].parameterSkip)
            //    {
            //        if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
            //        {
            //            result = true;  ///May be comment
            //            if (result)
            //            {
            //                Approvalproccess approvalproccess = new Approvalproccess();
            //                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
            //                approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessId").Value);
            //                approvalproccess.ServiceId = watchandward.Id;
            //                approvalproccess.SendFrom = SiteContext.UserId;
            //                approvalproccess.PendingStatus = 1;
            //                approvalproccess.Remarks = watchandward.ApprovalRemarks; ///May be comment
            //                approvalproccess.Status = Convert.ToInt32(watchandward.ApprovalStatus);
            //                if (i == DataFlow.Count - 1)
            //                    approvalproccess.SendTo = null;
            //                else
            //                {
            //                    approvalproccess.SendTo = Convert.ToInt32(DataFlow[i + 1].parameterName);
            //                }
            //                // if (i != DataFlow.Count - 1)  ///May be Uncomment
            //                result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table

            //                if (result)
            //                {
            //                    if (i == DataFlow.Count - 1)
            //                    {
            //                        watchandward.ApprovedStatus = 1;
            //                        watchandward.PendingAt = 0;
            //                    }
            //                    else
            //                    {
            //                        watchandward.ApprovedStatus = 0;
            //                        watchandward.PendingAt = Convert.ToInt32(DataFlow[i + 1].parameterName);
            //                    }
            //                    result = await _watchandwardService.UpdateBeforeApproval(id, watchandward);
            //                }
            //            }
            //            break;
            //        }

            //    }
            //}

            //#endregion

            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
            return View("Index");
        }

        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails((_configuration.GetSection("workflowPreccessId").Value), id);

            return PartialView("_HistoryDetails", Data);
        }

        #region Watch & Ward  Details
        public async Task<PartialViewResult> WatchWardView(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            return PartialView("_WatchWardView", Data);
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

        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 26 Nov 2020
        private async Task<List<TemplateStructure>> DataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(2);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
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
        #endregion


        [HttpPost]
        public async Task<IActionResult> Back()
        {
            return Redirect(_configuration.GetSection("ApprovalProccessPath:SiteMaster").Value.ToString());
        }


        public async Task<IActionResult> WatchWardApprovalList()
        {
            var result = await _watchAndWardApprovalService.GetAllWatchandward();
            List<WatchWardApprovalListDto> data = new List<WatchWardApprovalListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new WatchWardApprovalListDto()
                    {
                        Id = result[i].Id,
                        Date = result[i].Date.ToString() == null ? "" : result[i].Date.ToString(),
                        Loaclity = result[i].PrimaryListNoNavigation == null ? "" : result[i].PrimaryListNoNavigation.Locality == null ? "" : result[i].PrimaryListNoNavigation.Locality.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name.ToString(),
                        PrimaryListNo = result[i].PrimaryListNo.ToString(),
                        StatusOnGround = result[i].StatusOnGround.ToString(),
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
