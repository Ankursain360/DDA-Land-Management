using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LandTransfer.Controllers;
using Utility;
using Utility.Helper;

namespace LandTransfer.Controllers
{
    public class LandTransferController : BaseController
    {
        public IConfiguration _configuration;
        public readonly ILandTransferService _landTransferService;
        string targetPathLayout = string.Empty;
        public LandTransferController(ILandTransferService landTransferService,IConfiguration configuration)
        {
            _landTransferService = landTransferService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LandTransferSearchDto model)
        {
            var result = await _landTransferService.GetPagedLandTransfer(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Index()
        {
            List<Landtransfer> list = await _landTransferService.GetAllLandTransfer();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            Landtransfer model = new Landtransfer();
            model.DepartmentList = await _landTransferService.GetAllDepartment();
            model.ZoneList = await _landTransferService.GetAllZone(model.DepartmentId);
            model.DivisionList = await _landTransferService.GetAllDivisionList(model.ZoneId);
            model.LocalityList = await _landTransferService.GetAllLocalityList(model.DivisionId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Landtransfer landtransfer)
        {
            landtransfer.DepartmentList = await _landTransferService.GetAllDepartment();
            landtransfer.ZoneList = await _landTransferService.GetAllZone(landtransfer.DepartmentId);
            landtransfer.DivisionList = await _landTransferService.GetAllDivisionList(landtransfer.ZoneId);
            landtransfer.LocalityList = await _landTransferService.GetAllLocalityList(landtransfer.DivisionId);
            if (ModelState.IsValid)
            {
                targetPathLayout = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
                if (landtransfer.CopyofOrder != null)
                {
                    FileHelper file = new FileHelper();
                    landtransfer.CopyofOrderDocPath = file.SaveFile(targetPathLayout, landtransfer.CopyofOrder);
                }
                var result = await _landTransferService.Create(landtransfer);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var result1 = await _landTransferService.GetAllLandTransfer();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(landtransfer);
                }
            }

            return View(landtransfer);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _landTransferService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var result1 = await _landTransferService.GetAllLandTransfer();
            return View("Index", result1);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _landTransferService.FetchSingleResult(id);
            Data.DepartmentList = await _landTransferService.GetAllDepartment();
            Data.ZoneList = await _landTransferService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _landTransferService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _landTransferService.GetAllLocalityList(Data.DivisionId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Landtransfer landtransfer)
        {
            var Data = await _landTransferService.FetchSingleResult(landtransfer.Id);
            Data.DepartmentList = await _landTransferService.GetAllDepartment();
            Data.ZoneList = await _landTransferService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _landTransferService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _landTransferService.GetAllLocalityList(Data.DivisionId);
            if (ModelState.IsValid)
            {
                targetPathLayout = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
                if (landtransfer.CopyofOrder != null)
                {
                    FileHelper file = new FileHelper();
                    landtransfer.CopyofOrderDocPath = file.SaveFile(targetPathLayout, landtransfer.CopyofOrder);
                }
                var result = await _landTransferService.Update(id, landtransfer);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var result1 = await _landTransferService.GetAllLandTransfer();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(landtransfer);
                }
            }
            else
            {
                return View(landtransfer);
            }
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _landTransferService.FetchSingleResult(id);
            Data.DepartmentList = await _landTransferService.GetAllDepartment();
            Data.ZoneList = await _landTransferService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _landTransferService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _landTransferService.GetAllLocalityList(Data.DivisionId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _landTransferService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _landTransferService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _landTransferService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }
        public async Task<PartialViewResult> GetHistoryDetails(string KhasraNo)
        {
                var result = await _landTransferService.GetHistoryDetails(KhasraNo);
                if (result != null)
                {
                    return PartialView("_HistoryDetails", result);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return PartialView();
                }
        }
        public async Task<IActionResult> Download(int Id)
        {
            FileHelper file = new FileHelper();
            var Data =await _landTransferService.FetchSingleResult(Id);
            string filename = Data.CopyofOrderDocPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
    }
}
