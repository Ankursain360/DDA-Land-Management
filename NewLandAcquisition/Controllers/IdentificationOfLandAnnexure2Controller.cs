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
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandannexure2 newlandannexure2)
        {
            try
            {
               // newlandannexure2.ReqId = ReqId;
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
                    newlandannexure2.CreatedBy = SiteContext.UserId;
                    var result = await _newlandannexure2Service.Create(newlandannexure2);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //var list = await _newlandannexure2Service.Getawardmasterdetails();
                        //return View("Index", list);
                        return View(newlandannexure2);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandannexure2);
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
                return View(newlandannexure2);
            }
        }

    }
}
