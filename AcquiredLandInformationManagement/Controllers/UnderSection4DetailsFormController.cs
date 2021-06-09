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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Microsoft.Extensions.Configuration;

namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection4DetailsFormController : BaseController
    {
        private readonly IUndersection4service _undersection4service;
        public IConfiguration _configuration;
        string DocumentFilePath = "";


        public UnderSection4DetailsFormController(IUndersection4service undersection4service, IConfiguration configuration)
        {
            _undersection4service = undersection4service;
            _configuration = configuration;
            DocumentFilePath = _configuration.GetSection("FilePaths:US4:DocumentFIlePath").Value.ToString();
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Undersection4SearchDto model)
        {
            var result = await _undersection4service.GetPagedUndersection4details(model);

            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Undersection4 undersection4 = new Undersection4();
            undersection4.IsActive = 1;
            undersection4.ProposalList = await _undersection4service.GetAllProposal();
           
            return View(undersection4);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection4 undersection4)
        {
            try
            {
                undersection4.ProposalList = await _undersection4service.GetAllProposal();

                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    undersection4.DocumentName = undersection4.DocumentIFormFile == null ? undersection4.DocumentName : fileHelper.SaveFile1(DocumentFilePath, undersection4.DocumentIFormFile);
                    undersection4.CreatedBy = SiteContext.UserId;
                    var result = await _undersection4service.Create(undersection4);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4service.GetAllUndersection4();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4);
                    }
                }
                else
                {
                    return View(undersection4);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection4);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection4service.FetchSingleResult(id);
         
            Data.ProposalList = await _undersection4service.GetAllProposal();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection4 undersection4)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FileHelper fileHelper = new FileHelper();
                    undersection4.DocumentName = undersection4.DocumentIFormFile == null ? undersection4.DocumentName : fileHelper.SaveFile1(DocumentFilePath, undersection4.DocumentIFormFile);
                    undersection4.ModifiedBy = SiteContext.UserId;

                    var result = await _undersection4service.Update(id, undersection4);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4service.GetAllUndersection4();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection4);
                }
            }
            else
            {
                return View(undersection4);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection4service.Delete(id);
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
            var list = await _undersection4service.GetAllUndersection4();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection4service.FetchSingleResult(id);
            Data.ProposalList = await _undersection4service.GetAllProposal();
           

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]


        public async Task<IActionResult> UnderSection4List()
        {
            var result = await _undersection4service.GetAllUndersection4();
            List<UnderSection4ListDto> data = new List<UnderSection4ListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new UnderSection4ListDto()
                    {
                        Id = result[i].Id,
                        ProposalName = result[i].Proposal == null ? "" : result[i].Proposal.Name,
                        NotificationNo = result[i].Number,
                        NotificationDate = Convert.ToDateTime(result[i].Ndate).ToString("dd-MMM-yyyy"),
                        Type = result[i].TypeDetails,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }


        public async Task<IActionResult> ViewUploadedDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Undersection4 Data = await _undersection4service.FetchSingleResult(Id);
            string filename = DocumentFilePath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
    }
}