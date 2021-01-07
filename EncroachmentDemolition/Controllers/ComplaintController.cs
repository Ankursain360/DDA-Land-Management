using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncroachmentDemolition.Models;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Microsoft.Extensions.Configuration;
using Utility.Helper;



namespace EncroachmentDemolition.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IOnlinecomplaintService _onlinecomplaintService;
        public IConfiguration _configuration;
        string targetPhotoPathLayout = string.Empty;
        string targetReportfilePathLayout = string.Empty;


        public ComplaintController(IOnlinecomplaintService onlinecomplaintService, IConfiguration configuration)
        {
            _onlinecomplaintService = onlinecomplaintService;
            _configuration = configuration;
        }


       


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] OnlinecomplaintSearchDto model)
        {
            var result = await _onlinecomplaintService.GetPagedOnlinecomplaint(model);

            return PartialView("_List", result);
        }




        public async Task<IActionResult> Create()
        {
            Onlinecomplaint onlinecomplaint = new Onlinecomplaint();
            onlinecomplaint.IsActive = 1;
            onlinecomplaint.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            onlinecomplaint.LocationList = await _onlinecomplaintService.GetAllLocation();
             return View(onlinecomplaint);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Onlinecomplaint onlinecomplaint)
        {
            try
            {
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                onlinecomplaint.ReferenceNo = "TRN" + finalString;
                onlinecomplaint.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
                onlinecomplaint.LocationList = await _onlinecomplaintService.GetAllLocation();
              
                if (ModelState.IsValid)
                {
                    targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
                   // targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
                    FileHelper file = new FileHelper();
                    if (onlinecomplaint.Photo != null)
                    {
                        onlinecomplaint.PhotoPath = file.SaveFile(targetPhotoPathLayout, onlinecomplaint.Photo);
                    }
                   

                    var result = await _onlinecomplaintService.Create(onlinecomplaint);

                    if (result == true)
                    {
                        string DisplayName = onlinecomplaint.Name.ToString();
                        string EmailID = onlinecomplaint.Email.ToString();
                       
                        string Action = "Dear " + DisplayName + ",  Your Complaint Register succesfully. Your Reference No is  "  +onlinecomplaint.ReferenceNo;
                       
                        GenerateMailOTP mail = new GenerateMailOTP();
                        
                        mail.GenerateMailFormatForComplaint(DisplayName, EmailID,  Action);
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _onlinecomplaintService.GetAllOnlinecomplaint();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(onlinecomplaint);
                    }
                }
                else
                {
                    return View(onlinecomplaint);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(onlinecomplaint);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _onlinecomplaintService.FetchSingleResult(id);
            Data.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            Data.LocationList = await _onlinecomplaintService.GetAllLocation();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Onlinecomplaint onlinecomplaint)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _onlinecomplaintService.Update(id, onlinecomplaint);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _onlinecomplaintService.GetAllOnlinecomplaint();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(onlinecomplaint);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(onlinecomplaint);
                }
            }
            else
            {
                return View(onlinecomplaint);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _onlinecomplaintService.Delete(id);
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
            var list = await _onlinecomplaintService.GetAllOnlinecomplaint();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _onlinecomplaintService.FetchSingleResult(id);
            Data.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            Data.LocationList = await _onlinecomplaintService.GetAllLocation();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }





    }
}