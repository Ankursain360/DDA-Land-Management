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
using Newtonsoft.Json;
using System.Text;




using Microsoft.AspNetCore.Http;

using System.IO;




namespace AcquiredLandInformationManagement.Controllers
{
    public class PaymentdetailController : BaseController
    {
        private readonly IPaymentdetailService _paymentdetailService;
        public IConfiguration _configuration;
        string PaymentProofDocumentFilePath = "";
        public object JsonRequestBehavior { get; private set; }



        public PaymentdetailController(IPaymentdetailService paymentdetailService, IConfiguration configuration)
        {
            _paymentdetailService = paymentdetailService;
            _configuration = configuration;
            PaymentProofDocumentFilePath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _paymentdetailService.GetAllPaymentdetail();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PaymentdetailSearchDto model)
        {
            var result = await _paymentdetailService.GetPagedPaymentdetail(model);
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
        public async Task<IActionResult> Create(Paymentdetail paymentdetail)
        {
            bool IsValidpdf = CheckMimeType(paymentdetail);
            try
            {


                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {
                        FileHelper fileHelper = new FileHelper();
                        paymentdetail.PaymentProofDocumentName = paymentdetail.PaymentProofDocumentIFormFile == null ?
                            paymentdetail.PaymentProofDocumentName : fileHelper.SaveFile1(PaymentProofDocumentFilePath,
                            paymentdetail.PaymentProofDocumentIFormFile);
                        paymentdetail.CreatedBy = SiteContext.UserId;
                        var result = await _paymentdetailService.Create(paymentdetail);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var list = await _paymentdetailService.GetAllPaymentdetail();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(paymentdetail);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(paymentdetail);

                    }
                }
                else
                {
                    return View(paymentdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(paymentdetail);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _paymentdetailService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Paymentdetail paymentdetail)
        {
            bool IsValidpdf = CheckMimeType(paymentdetail);
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {

                    try
                    {
                        FileHelper fileHelper = new FileHelper();
                        paymentdetail.PaymentProofDocumentName = paymentdetail.PaymentProofDocumentIFormFile == null ?
                            paymentdetail.PaymentProofDocumentName : fileHelper.SaveFile1(PaymentProofDocumentFilePath,
                            paymentdetail.PaymentProofDocumentIFormFile);
                        paymentdetail.ModifiedBy = SiteContext.UserId;
                        var result = await _paymentdetailService.Update(id, paymentdetail);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _paymentdetailService.GetAllPaymentdetail();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(paymentdetail);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(paymentdetail);
                    }
                }
                else
                {

                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(paymentdetail);
                }
            
            }
            else
            {
                return View(paymentdetail);
            }
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _paymentdetailService.Delete(id);
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
            var list = await _paymentdetailService.GetAllPaymentdetail();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _paymentdetailService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> PaymentdetailList()
        {
            var result = await _paymentdetailService.GetAllPaymentdetail();
            List<PaymentdetailListDto> data = new List<PaymentdetailListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PaymentdetailListDto()
                    {
                        Id = result[i].Id,
                        DemandListNo = result[i].DemandListNo,
                        ENMSNO = result[i].EnmSno,
                        BankName = result[i].BankName,
                        VoucherNo = result[i].VoucherNo,
                        ChequeNo = result[i].ChequeNo,
                        ChequeDate = Convert.ToDateTime(result[i].ChequeDate).ToString("dd-MMM-yyyy"),
                        PercentPaid = result[i].PercentPaid.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

        public async Task<IActionResult> ViewPaymentProofDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Paymentdetail Data = await _paymentdetailService.FetchSingleResult(Id);
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





        public bool CheckMimeType(Paymentdetail paymentdetail)
        {
            bool Flag = true;
            string fullpath = string.Empty;            
            string extension = string.Empty;
            PaymentProofDocumentFilePath = _configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
            IFormFile files = paymentdetail.PaymentProofDocumentIFormFile;
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
                        else
                        {
                            Flag = false;
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

