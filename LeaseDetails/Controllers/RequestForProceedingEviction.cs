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


using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;
using LeaseDetails.Filters;
using Core.Enum;

namespace LeaseDetails.Controllers
{
    public class RequestForProceedingEviction : BaseController
    {
        private readonly IRequestforproceedingService _undersection4PlotService;
        public IConfiguration _configuration;
        string DemandletterFilePath = string.Empty;
        string NOCFilePath = string.Empty;
        string CancellationOrderFilePath = string.Empty;

        public RequestForProceedingEviction(IRequestforproceedingService undersection4PlotService, IConfiguration configuration)
        {
            _configuration = configuration;
            _undersection4PlotService = undersection4PlotService;
        }
        public async Task<IActionResult> Index()
        {
            var Msg = TempData.Peek("Message");
            if (Msg != null)
                ViewBag.Message = Msg;
            var list = await _undersection4PlotService.GetAllRequestForProceeding();
            return View(list);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestForProceedingSearchDto model)
        {
            var result = await _undersection4PlotService.GetPagedRequestForProceeding(model);

            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()
        {
            Requestforproceeding undersection4plot = new Requestforproceeding();
            undersection4plot.IsActive = 1;
            undersection4plot.HonbleList = await _undersection4PlotService.GetAllHonble();
            undersection4plot.AllotmententryList = await _undersection4PlotService.GetAllAllotment();
            undersection4plot.UserNameList = await _undersection4PlotService.BindUsernameNameList();



            return View(undersection4plot);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Requestforproceeding undersection4plot)
        {




            DemandletterFilePath = _configuration.GetSection("FilePaths:Demandletter:DemandletterFilePath").Value.ToString();
            NOCFilePath = _configuration.GetSection("FilePaths:NOC:NOCFilePath").Value.ToString();
            CancellationOrderFilePath = _configuration.GetSection("FilePaths:CancellationOrder:CancellationOrderFilePath").Value.ToString();

            try
            {
                undersection4plot.AllotmententryList = await _undersection4PlotService.GetAllAllotment();
                undersection4plot.UserNameList = await _undersection4PlotService.BindUsernameNameList();

                undersection4plot.HonbleList = await _undersection4PlotService.GetAllHonble();

                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (undersection4plot.DemandLetterPhoto != null)
                    {
                        undersection4plot.DemandLetter = fileHelper.SaveFile(DemandletterFilePath, undersection4plot.DemandLetterPhoto);
                    }


                    if (undersection4plot.NocPhoto != null)
                    {
                        undersection4plot.Noc = fileHelper.SaveFile(NOCFilePath, undersection4plot.NocPhoto);
                    }

                    if (undersection4plot.CancellationPhoto != null)
                    {
                        undersection4plot.CancellationOrder = fileHelper.SaveFile(CancellationOrderFilePath, undersection4plot.CancellationPhoto);
                    }



                    undersection4plot.CreatedBy = SiteContext.UserId;
                    //undersection4plot.PendingAt = 0;
                    var result = await _undersection4PlotService.Create(undersection4plot);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4PlotService.GetAllRequestForProceeding();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4plot);
                    }
            }
                else
            {
                return View(undersection4plot);
            }
        }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection4plot);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection4PlotService.FetchSingleResult(id);
            Data.HonbleList = await _undersection4PlotService.GetAllHonble();
            Data.AllotmententryList = await _undersection4PlotService.GetAllAllotment();
            Data.UserNameList = await _undersection4PlotService.BindUsernameNameList();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Requestforproceeding undersection4plot)
        {
            undersection4plot.AllotmententryList = await _undersection4PlotService.GetAllAllotment();
            undersection4plot.UserNameList = await _undersection4PlotService.BindUsernameNameList();

            undersection4plot.HonbleList = await _undersection4PlotService.GetAllHonble();

            DemandletterFilePath = _configuration.GetSection("FilePaths:Demandletter:DemandletterFilePath").Value.ToString();
            NOCFilePath = _configuration.GetSection("FilePaths:NOC:NOCFilePath").Value.ToString();
            CancellationOrderFilePath = _configuration.GetSection("FilePaths:CancellationOrder:CancellationOrderFilePath").Value.ToString();


            //if (ModelState.IsValid)
            //{

                FileHelper fileHelper = new FileHelper();
                if (undersection4plot.DemandLetterPhoto != null)
                {
                    undersection4plot.DemandLetter = fileHelper.SaveFile(DemandletterFilePath, undersection4plot.DemandLetterPhoto);
                }


                if (undersection4plot.NocPhoto != null)
                {
                    undersection4plot.Noc = fileHelper.SaveFile(NOCFilePath, undersection4plot.NocPhoto);
                }

                if (undersection4plot.CancellationPhoto != null)
                {
                    undersection4plot.CancellationOrder = fileHelper.SaveFile(CancellationOrderFilePath, undersection4plot.CancellationPhoto);
                }



                try
                {
                   

                    var result = await _undersection4PlotService.Update(id, undersection4plot);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4PlotService.GetAllRequestForProceeding();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4plot);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection4plot);
                }
        //}
        //    else
        //    {
                return View(undersection4plot);
  //  }
}




        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection4PlotService.FetchSingleResult(id);
            Data.HonbleList = await _undersection4PlotService.GetAllHonble();
            Data.AllotmententryList = await _undersection4PlotService.GetAllAllotment();
            Data.UserNameList = await _undersection4PlotService.BindUsernameNameList();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection4PlotService.Delete(id);
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
            var list = await _undersection4PlotService.GetAllRequestForProceeding();
            return View("Index", list);
        }

        public async Task<FileResult> ViewLetter(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.DemandLetter;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.DemandLetter;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }


        public async Task<FileResult> ViewLetter1(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }


        public async Task<FileResult> ViewLetter2(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }







    }
}
