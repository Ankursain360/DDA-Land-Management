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

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionstructuredetailsController : BaseController
    {
        public IConfiguration _configuration;
        public readonly IDemolitionstructuredetailsService _demolitionstructuredetailsService;
        public DemolitionstructuredetailsController(IDemolitionstructuredetailsService demolitionstructuredetailsService, IConfiguration configuration)
        {
            _demolitionstructuredetailsService = demolitionstructuredetailsService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemolitionstructuredetailsDto model)
        {
            var result = await _demolitionstructuredetailsService.GetPagedDemolitionstructuredetails(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Demolitionstructuredetails demolitionstructuredetails = new Demolitionstructuredetails();
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId??0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId??0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId??0));
            demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetStructure();
            return View(demolitionstructuredetails);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Demolitionstructuredetails demolitionstructuredetails)
        {
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));

            string AfterPhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:AfterPhotoFilePath").Value.ToString();
            //string LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
            //string FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();
            //if (ModelState.IsValid)
            //{
            //    var result = await _demolitionstructuredetailsService.Create(demolitionstructuredetails);
            //    if (result)
            //    {
            //        FileHelper fileHelper = new FileHelper();
            //        if (demolitionstructuredetails.NameOfStructure != null && demolitionstructuredetails.NoOfStructrure != null && demolitionstructuredetails.NameOfStructure.Count > 0 && demolitionstructuredetails.NoOfStructrure.Count > 0 )
            //        {
            //            List<Demolitionstructure> demolitionstructure = new List<Demolitionstructure>();
            //            for (int i = 0; i < demolitionstructuredetails.NameOfStructure.Count; i++)
            //            {
            //                demolitionstructure.Add(new Demolitionstructure
            //                {
            //                    StructureId = demolitionstructuredetails.Id,
            //                    NoOfStructrure = demolitionstructuredetails.NoOfStructrure[i],

            //                    DemolitionStructureDetailsId = demolitionstructuredetails.Id
            //                });
            //            }
            //            foreach (var item in demolitionstructure)
            //            {
            //                result = await _demolitionstructuredetailsService.SaveDemolitionstructure(item);
            //            }
            //        }

            //        if (demolitionstructuredetails.AfterPhotoFile != null && demolitionstructuredetails.AfterPhotoFile.Count > 0)
            //        {
            //            List<Demolitionstructureafterdemolitionphotofiledetails> demolitionstructureafterdemolitionphotofiledetails = new List<Demolitionstructureafterdemolitionphotofiledetails>();
            //            for (int i = 0; i < demolitionstructuredetails.AfterPhotoFile.Count; i++)
            //            {
            //                string FilePath = fileHelper.SaveFile(AfterPhotoFilePath, demolitionstructuredetails.AfterPhotoFile[i]);
            //                demolitionstructureafterdemolitionphotofiledetails.Add(new Demolitionstructureafterdemolitionphotofiledetails
            //                {
            //                    DemolitionStructureDetailsId = demolitionstructuredetails.Id,
            //                    AfterPhotoFilePath = FilePath
            //                });
            //            }
            //            foreach (var item in demolitionstructureafterdemolitionphotofiledetails)
            //            {
            //                result = await _demolitionstructuredetailsService.SaveDemolitionstructureafterdemolitionphotofiledetails(item);
            //            }
            //        }
            //        if (demolitionstructuredetails.BeforePhotoFile != null && demolitionstructuredetails.BeforePhotoFile.Count > 0)
            //        {
            //            List<Demolitionstructurebeforedemolitionphotofiledetails> demolitionstructurebeforedemolitionphotofiledetails = new List<Demolitionstructurebeforedemolitionphotofiledetails>();
            //            for (int i = 0; i < demolitionstructuredetails.BeforePhotoFile.Count; i++)
            //            {
            //                string FilePath = fileHelper.SaveFile(AfterPhotoFilePath, demolitionstructuredetails.BeforePhotoFile[i]);
            //                demolitionstructurebeforedemolitionphotofiledetails.Add(new Demolitionstructurebeforedemolitionphotofiledetails
            //                {
            //                    DemolitionStructureId = demolitionstructuredetails.Id,
            //                    BeforePhotoFilePath = FilePath
            //                });
            //            }
            //            foreach (var item in demolitionstructurebeforedemolitionphotofiledetails)
            //            {
            //                result = await _demolitionstructuredetailsService.SaveDemolitionstructurebeforedemolitionphotofiledetails(item);
            //            }
            //        }
            //        if (result)
            //        {
            //            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
            //            var result1 = await _demolitionstructuredetailsService.GetAllDemolitionstructuredetails();
            //            return View("Index", result1);
            //        }
            //        else
            //        {
            //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            //            return View(demolitionstructuredetails);
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            //        return View(demolitionstructuredetails);
            //    }
            //}
            //else
            //{
            //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            //    return View(demolitionstructuredetails);
            //}
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var demolitionstructuredetails = await _demolitionstructuredetailsService.FetchSingleResult(id);
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
            if (demolitionstructuredetails == null)
            {
                return NotFound();
            }
            return View(demolitionstructuredetails);
        }
        public async Task<IActionResult> View(int id)
        {
            var demolitionstructuredetails = await _demolitionstructuredetailsService.FetchSingleResult(id);
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId??0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId??0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId??0));
            if (demolitionstructuredetails == null)
            {
                return NotFound();
            }
            return View(demolitionstructuredetails);
        }
        public async Task<JsonResult> DetailsOfRepeater(int? Id)
        {
            Id = Id ?? 0;
            var data = await _demolitionstructuredetailsService.GetDemolitionstructure(Convert.ToInt32(Id));
            return Json(data.Select(x => new { x.NoOfStructrure,  x.StructureId }));
        }
        [HttpPost]
        //public async Task<IActionResult> Edit(int id, Demolitionstructuredetails demolitionstructuredetails)
        //{
        //    var Data = await _demolitionstructuredetailsService.FetchSingleResult(id);
        //    Data.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
        //    Data.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(Data.DepartmentId??0));
        //    Data.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(Data.ZoneId ?? 0));
        //    Data.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(Data.DivisionId??0));
           
        //    string AfterPhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:AfterPhotoFilePath").Value.ToString();
        //    string BeforePhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:BeforePhotoFilePath").Value.ToString();
        //    //string LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
        //    //string FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _demolitionstructuredetailsService.Update(id, demolitionstructuredetails);
        //        if (result)
        //        {
        //            FileHelper fileHelper = new FileHelper();
        //            if (demolitionstructuredetails.NameOfStructure != null && demolitionstructuredetails.NoOfStructrure != null && demolitionstructuredetails.NameOfStructure.Count > 0 && demolitionstructuredetails.NoOfStructrure.Count > 0)
                       
        //                {
        //                    List<Demolitionstructure> demolitionstructure = new List<Demolitionstructure>();
        //                    for (int i = 0; i < demolitionstructuredetails.NameOfStructure.Count; i++)
        //                    {
        //                    demolitionstructure.Add(new Demolitionstructure
        //                    {
                                    
        //                            //NameOfStructure = demolitionstructuredetails.NameOfStructure[i],
        //                            NoOfStructrure = demolitionstructuredetails.NoOfStructrure[i],
                                   
        //                            DemolitionStructureDetailsId = demolitionstructuredetails.Id
        //                        });
        //                    }
        //                    foreach (var item in demolitionstructure)
        //                    {
        //                        result = await _demolitionstructuredetailsService.SaveDemolitionstructure(item);
        //                    }
        //                }
        //            if (demolitionstructuredetails.AfterPhotoFile != null && demolitionstructuredetails.AfterPhotoFile.Count > 0)
        //            {
        //                List<Demolitionstructureafterdemolitionphotofiledetails> demolitionstructureafterdemolitionphotofiledetails = new List<Demolitionstructureafterdemolitionphotofiledetails>();
        //                result = await _demolitionstructuredetailsService.DeleteDemolitionstructureafterdemolitionphotofiledetails(id);
        //                for (int i = 0; i < demolitionstructuredetails.AfterPhotoFile.Count; i++)
        //                {
        //                    string FilePath = fileHelper.SaveFile(AfterPhotoFilePath, demolitionstructuredetails.AfterPhotoFile[i]);
        //                    demolitionstructureafterdemolitionphotofiledetails.Add(new Demolitionstructureafterdemolitionphotofiledetails
        //                    {
        //                        DemolitionStructureDetailsId = demolitionstructuredetails.Id,
        //                        AfterPhotoFilePath = FilePath
        //                    });
        //                }
        //                foreach (var item in demolitionstructureafterdemolitionphotofiledetails)
        //                {
        //                    result = await _demolitionstructuredetailsService.SaveDemolitionstructureafterdemolitionphotofiledetails(item);
        //                }
        //            }

        //            if (result)
        //            {
        //                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
        //                var result1 = await _demolitionstructuredetailsService.GetAllDemolitionstructuredetails();
        //                return View("Index", result1);
        //            }
        //            else
        //            {
        //                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //                return View(demolitionstructuredetails);
        //            }

        //        }
        //        else
        //        {
        //            return View(demolitionstructuredetails);
        //        }
        //    }
        //    else
        //    {
        //        return View(demolitionstructuredetails);
        //    }
        //}
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _demolitionstructuredetailsService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var result1 = await _demolitionstructuredetailsService.GetAllDemolitionstructuredetails();
            return View("Index", result1);
        }

        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }
       
        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Demolitionstructureafterdemolitionphotofiledetails Data = await _demolitionstructuredetailsService.GetDemolitionstructureafterdemolitionphotofiledetails(Id);
            string filename = Data.AfterPhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public IActionResult DemolitionstructuredetailsApproval()
        {
            return View();
        }

        public IActionResult DemolitionstructuredetailsApprovalCreate()
        {
            return View();
        }
    }
}
