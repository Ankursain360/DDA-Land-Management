using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewLandAcquisition.Filters;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;


namespace NewLandAcquisition.Controllers
{
    public class NewlandNotificationController : BaseController
    {
        public IConfiguration _configuration;
        public readonly INewlandnotificationService _newlandnotificationService;
        string NewlandNotificationFilePath = string.Empty;


        public NewlandNotificationController(INewlandnotificationService newlandnotificationService, IConfiguration configuration)
        {
            _newlandnotificationService = newlandnotificationService;
            _configuration = configuration;
             NewlandNotificationFilePath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();


        }
        // [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandnotificationSearchDto model)
        {
            var result = await _newlandnotificationService.GetPagedNewlandnotificationdetails(model);

            return PartialView("_List", result);
        }
         [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandnotification newlandnotification = new Newlandnotification();
            newlandnotification.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();
            return View(newlandnotification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandnotification newlandnotification)
        {
            try
            {
                newlandnotification.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();
                string NewlandNotificationFilePath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles1:NewlandNotificationFilePath1").Value.ToString();

                if (ModelState.IsValid)
                {

                    FileHelper fileHelper = new FileHelper();

                    if (newlandnotification.NewlandNotificationFile != null)
                    {
                        newlandnotification.GazetteNotificationFilePath = fileHelper.SaveFile1(NewlandNotificationFilePath, newlandnotification.NewlandNotificationFile);

                    }




                    newlandnotification.CreatedBy = SiteContext.UserId;
                    var result = await _newlandnotificationService.Create(newlandnotification);

                    if (result)
                    {
                       
                        ///for notification file:


                        //if (newlandnotification.NewlandNotificationFile != null && newlandnotification.NewlandNotificationFile.Count > 0)
                        //{
                        //    List<Newlandnotificationfilepath> newlandnotificationfilepath = new List<Newlandnotificationfilepath>();
                        //    for (int i = 0; i < newlandnotification.NewlandNotificationFile.Count; i++)
                        //    {
                        //        string FilePath = fileHelper.SaveFile(NewlandNotificationFilePath, newlandnotification.NewlandNotificationFile[i]);
                        //        newlandnotificationfilepath.Add(new Newlandnotificationfilepath
                        //        {
                        //            NewlandNotificationId = newlandnotification.Id,
                        //            FilePath = FilePath
                        //        });
                        //    }
                        //    foreach (var item in newlandnotificationfilepath)
                        //    {
                        //        result = await _newlandnotificationService.Savefiledetails(item);
                        //    }
                        //}
                    }

                        if (result)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandnotificationService.GetAllNewlandNotification();
                          
                        return View("Index",list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandnotification);
                    }
                }
                else
                {
                    return View(newlandnotification);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandnotification);
            }
        }


       


        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandnotificationService.FetchSingleResult1(id);          
            Data.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        


       [HttpPost]
       
        public async Task<IActionResult> Edit(int id, Newlandnotification newlandnotification)
        {
            newlandnotification.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();

            if (ModelState.IsValid)
            {
                newlandnotification.ModifiedBy = SiteContext.UserId;
                var result = await _newlandnotificationService.Update(id, newlandnotification);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _newlandnotificationService.GetAllNewlandNotification();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandnotification);

                }

            }
            return View(newlandnotification);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandnotificationService.FetchSingleResult1(id);
            Data.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        //public async Task<IActionResult> ViewPaymentProofDocument(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    Newlandpaymentdetail Data = await _newlandnotificationService.FetchSingleResult(Id);
        //    string filename = PaymentProofDocumentFilePath + Data.PaymentProofDocumentName;
        //    byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
        //    return File(FileBytes, file.GetContentType(filename));
        //}



        public async Task<IActionResult> ViewDocumenbtsdG(int Id)
        {
            FileHelper fileHelper = new FileHelper();
            Newlandnotification Data = await _newlandnotificationService.FetchSingleResult1(Id);
        
            string filename = NewlandNotificationFilePath + Data.GazetteNotificationFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, fileHelper.GetContentType(filename));
        }
    }
}
