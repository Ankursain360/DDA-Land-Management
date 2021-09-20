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
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace NewLandAcquisition.Controllers
{
    public class NewLandPaymentdetailController : BaseController
    {
        private readonly INewLandPaymentdetailService _newLandPaymentdetailService;
        public IConfiguration _configuration;
        string PaymentProofDocumentFilePath = "";
        public object JsonRequestBehavior { get; private set; }



        public NewLandPaymentdetailController(INewLandPaymentdetailService newLandPaymentdetailService, IConfiguration configuration)
        {
            _newLandPaymentdetailService = newLandPaymentdetailService;
            _configuration = configuration;
            PaymentProofDocumentFilePath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _newLandPaymentdetailService.GetAllPaymentdetail();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewLandPaymentDetailSearchDto model)
        {
            var result = await _newLandPaymentdetailService.GetPagedPaymentdetail(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandpaymentdetail newlandpaymentdetail)
        {
            try
            {

                bool IsValidpdf = CheckMimeType(newlandpaymentdetail);
                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {

                        FileHelper fileHelper = new FileHelper();
                        newlandpaymentdetail.PaymentProofDocumentName = newlandpaymentdetail.PaymentProofDocumentIFormFile == null ? newlandpaymentdetail.PaymentProofDocumentName : fileHelper.SaveFile1(PaymentProofDocumentFilePath, newlandpaymentdetail.PaymentProofDocumentIFormFile);
                        newlandpaymentdetail.CreatedBy = SiteContext.UserId;
                        var result = await _newLandPaymentdetailService.Create(newlandpaymentdetail);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var list = await _newLandPaymentdetailService.GetAllPaymentdetail();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(newlandpaymentdetail);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(newlandpaymentdetail);

                    }
                }
                else
                {
                    return View(newlandpaymentdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandpaymentdetail);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newLandPaymentdetailService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandpaymentdetail newlandpaymentdetail)
        {
            bool IsValidpdf = CheckMimeType(newlandpaymentdetail);
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {

                    try
                    {
                        FileHelper fileHelper = new FileHelper();
                        newlandpaymentdetail.PaymentProofDocumentName = newlandpaymentdetail.PaymentProofDocumentIFormFile == null ? newlandpaymentdetail.PaymentProofDocumentName : fileHelper.SaveFile1(PaymentProofDocumentFilePath, newlandpaymentdetail.PaymentProofDocumentIFormFile);
                        newlandpaymentdetail.ModifiedBy = SiteContext.UserId;
                        var result = await _newLandPaymentdetailService.Update(id, newlandpaymentdetail);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _newLandPaymentdetailService.GetAllPaymentdetail();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(_newLandPaymentdetailService);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(_newLandPaymentdetailService);
                    }
                }
                else
                {

                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(newlandpaymentdetail);
                }
            }
            else
            {
                return View(newlandpaymentdetail);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _newLandPaymentdetailService.Delete(id);
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
            var list = await _newLandPaymentdetailService.GetAllPaymentdetail();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newLandPaymentdetailService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        public async Task<IActionResult> NewLandPaymentDetailsList()
        {
            var result = await _newLandPaymentdetailService.GetAllPaymentdetail();
            List<NewLandPaymentDelailsListDto> data = new List<NewLandPaymentDelailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandPaymentDelailsListDto()
                    {
                        Id = result[i].Id,
                        DemandListNo = result[i].DemandListNo,
                        EnmSno = result[i].EnmSno,
                        BankName = result[i].BankName,
                        VoucherNo = result[i].VoucherNo,
                        ChequeNo = result[i].ChequeNo,
                        ChequeDate = result[i].ChequeDate.ToString(),
                        PercentPaid = result[i].PercentPaid.ToString(),
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<IActionResult> ViewPaymentProofDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Newlandpaymentdetail Data = await _newLandPaymentdetailService.FetchSingleResult(Id);
            string filename = PaymentProofDocumentFilePath + Data.PaymentProofDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }



        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            PaymentProofDocumentFilePath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                PaymentProofDocumentFilePath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(PaymentProofDocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(PaymentProofDocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(PaymentProofDocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                IsImg = false;
                            }

                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        IsImg = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Json(IsImg, JsonRequestBehavior);
        }



        public bool CheckMimeType(Newlandpaymentdetail newlandpaymentdetail)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            PaymentProofDocumentFilePath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
            IFormFile files = newlandpaymentdetail.PaymentProofDocumentIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                PaymentProofDocumentFilePath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(PaymentProofDocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(PaymentProofDocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(PaymentProofDocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                Flag = false;
                            }

                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        Flag = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Flag;
        }



    }
}

