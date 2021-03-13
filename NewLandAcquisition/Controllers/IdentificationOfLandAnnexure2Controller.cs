using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;
using NewLandAcquisition.Filters;
using Core.Enum;
namespace NewLandAcquisition.Controllers
{
    public class IdentificationOfLandAnnexure2Controller : BaseController
    {
        int ReqId;
        public readonly INewlandannexure2Service _newlandannexure2Service;
        public IConfiguration _configuration;
        public IdentificationOfLandAnnexure2Controller(INewlandannexure2Service newlandannexure2Service, IConfiguration configuration)
        {
            _newlandannexure2Service = newlandannexure2Service;
            _configuration = configuration;
        }

       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            
            Newlandannexure2 model = new Newlandannexure2();
            model.IsActive = 1;
            return View(model);
        }
        [HttpPost]
       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id,Newlandannexure2 newlandannexure2)
        {
            try
            {
               
                string Sn7FilePath = _configuration.GetSection("FilePaths:NewlandAnnx2:SN7").Value.ToString();
                string Sn8FilePath = _configuration.GetSection("FilePaths:NewlandAnnx2:SN8").Value.ToString();
                string Sn9FilePath = _configuration.GetSection("FilePaths:NewlandAnnx2:SN9").Value.ToString();
                string Sn12FilePath = _configuration.GetSection("FilePaths:NewlandAnnx2:SN12").Value.ToString();
                
                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (newlandannexure2.Sn7Filep != null)
                    {
                        newlandannexure2.Sn7File = fileHelper.SaveFile(Sn7FilePath, newlandannexure2.Sn7Filep);
                    }
                    if (newlandannexure2.Sn8File != null)
                    {
                        newlandannexure2.Sn8filePath = fileHelper.SaveFile(Sn8FilePath, newlandannexure2.Sn8File);
                    }
                    if (newlandannexure2.Sn9File != null)
                    {
                        newlandannexure2.Sn9filePath = fileHelper.SaveFile(Sn9FilePath, newlandannexure2.Sn9File);
                    }
                    if (newlandannexure2.Sn12File != null)
                    {
                        newlandannexure2.Sn12filePath = fileHelper.SaveFile(Sn12FilePath, newlandannexure2.Sn12File);
                    }
                    newlandannexure2.Id = 0;
                    newlandannexure2.ReqId = id;
                    newlandannexure2.CreatedBy = SiteContext.UserId;
                    var result = await _newlandannexure2Service.Create(newlandannexure2);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                       
                        return RedirectToAction("Index", "RequestApprovalProcess");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        
                        return RedirectToAction("Index", "RequestApprovalProcess");
                    }
                }
                else
                {
                    return View(newlandannexure2);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
              
                return RedirectToAction("Index", "RequestApprovalProcess");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _newlandannexure2Service.FetchSingleResultAnnx2(id);
            ViewBag.ExistSn9File = Data.Sn9filePath;
            ViewBag.ExistSn8File = Data.Sn8filePath;
            ViewBag.ExistSn7File = Data.Sn7File;
            ViewBag.ExistSn12File = Data.Sn12filePath;

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public async Task<IActionResult> S7Download(int Id)
        {
            string filename = _newlandannexure2Service.GetS7Download(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> S8Download(int Id)
        {
            string filename = _newlandannexure2Service.GetS8Download(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> S9Download(int Id)
        {
            string filename = _newlandannexure2Service.GetS9Download(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> S12Download(int Id)
        {
            string filename = _newlandannexure2Service.GetS12Download(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandannexure2Service.FetchSingleResultAnnx2(id);
            ViewBag.ExistSn9File = Data.Sn9filePath;
            ViewBag.ExistSn8File = Data.Sn8filePath;
            ViewBag.ExistSn7File = Data.Sn7File;
            ViewBag.ExistSn12File = Data.Sn12filePath;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        //    [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandannexure2 newlandannexure2)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _newlandannexure2Service.Update(id, newlandannexure2);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                  
                    return RedirectToAction("Index", "RequestApprovalProcess");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return RedirectToAction("Index", "RequestApprovalProcess");
                }
            }
            else
            {
                return RedirectToAction("Index", "RequestApprovalProcess");
            }
        }
    }
}
