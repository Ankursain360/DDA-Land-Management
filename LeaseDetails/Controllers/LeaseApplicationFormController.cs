using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using LeaseDetails.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using LeaseDetails.Filters;
using Core.Enum;
using Utility.Helper;
using Microsoft.Extensions.Configuration;

namespace LeaseDetails.Controllers
{
    public class LeaseApplicationFormController : BaseController
    {

        private readonly ILeaseApplicationFormService _leaseApplicationFormService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        public LeaseApplicationFormController(ILeaseApplicationFormService leaseApplicationFormService,
            IConfiguration configuration, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService)
        {
            _leaseApplicationFormService = leaseApplicationFormService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _workflowtemplateService = workflowtemplateService;
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<PartialViewResult> List([FromBody] DocumentChecklistSearchDto model)
        //{
        //    var result = await _leaseApplicationFormService.GetPagedDocumentChecklistData(model);
        //    return PartialView("_List", result);
        //}
        //async Task BindDropDown(Documentchecklist documentchecklist)
        //{
        //    documentchecklist.ServiceTypeList = await _leaseApplicationFormService.GetServiceTypeList();
        //}

        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            Leaseapplication leaseapplication = new Leaseapplication();
            return View(leaseapplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leaseapplication leaseapplication)
        {
            try
            {
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                leaseapplication.RefNo = leaseapplication.RegistrationNo + leaseapplication.ContactNo + finalString;
                string FilePath = _configuration.GetSection("FilePaths:LeaseApplicationForm:DocumentFilePath").Value.ToString();
                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    leaseapplication.CreatedBy = 1;
                    leaseapplication.ApprovedSataus = 0;
                    leaseapplication.PendingAt = 1;
                    leaseapplication.IsActive = 1;
                    var result = await _leaseApplicationFormService.Create(leaseapplication);

                    if(result)
                    {
                        if (leaseapplication.DocumentName != null && leaseapplication.Mandatory != null)
                        {
                            if (leaseapplication.DocumentName.Count > 0 && leaseapplication.Mandatory.Count > 0 )
                            {
                                List<Leaseapplicationdocuments> leaseapplicationdocuments = new List<Leaseapplicationdocuments>();
                                for (int i = 0; i < leaseapplication.DocumentName.Count; i++)
                                {
                                    leaseapplicationdocuments.Add(new Leaseapplicationdocuments
                                    {
                                        DocumentChecklistId = leaseapplication.DocumentChecklistId.Count <= i ? 0 : leaseapplication.DocumentChecklistId[i],
                                        LeaseApplicationId = leaseapplication.Id,
                                        DocumentFileName = leaseapplication.FileUploaded != null ?
                                                                leaseapplication.FileUploaded.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile1(FilePath, leaseapplication.FileUploaded[i]) :
                                                                leaseapplication.FileUploaded[i] != null || leaseapplication.FileUploadedPath[i] != "" ?
                                                                leaseapplication.FileUploadedPath[i] : string.Empty
                                    });
                                }
                                result = await _leaseApplicationFormService.SaveLeaseApplicationDocuments(leaseapplicationdocuments);
                            }
                        }
                    }
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View("Create");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(leaseapplication);

                    }
                }
                else
                {
                    return View(leaseapplication);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(leaseapplication);
            }
        }
        public async Task<JsonResult> GetDocumentChecklistDetails()
        {
            var data = await _leaseApplicationFormService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdLeaseAppForm").Value));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.ServiceTypeId,
                x.IsMandatory
            }));
        }
    }
}