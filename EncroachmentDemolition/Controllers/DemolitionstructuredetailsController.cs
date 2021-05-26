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
using EncroachmentDemolition.Filters;
using Core.Enum;
using Dto.Master;

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionstructuredetailsController : BaseController
    {
        public IConfiguration _configuration;
        public readonly IDemolitionstructuredetailsService _demolitionstructuredetailsService;
        string DemolitionReportFilePath = "";
        public DemolitionstructuredetailsController(IDemolitionstructuredetailsService demolitionstructuredetailsService, IConfiguration configuration)
        {
            _demolitionstructuredetailsService = demolitionstructuredetailsService;
            _configuration = configuration;
            DemolitionReportFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:DemolitionReportFilePath").Value.ToString();
        }

        [AuthorizeContext(ViewAction.View)]
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

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Demolitionstructuredetails demolitionstructuredetails = new Demolitionstructuredetails();
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
            demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
            demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();
            return View(demolitionstructuredetails);

        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Demolitionstructuredetails demolitionstructuredetails)
        {
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
          
            demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
            demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();
            string AfterPhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:AfterPhotoFilePath").Value.ToString();
            string BeforePhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:BeforePhotoFilePath").Value.ToString();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                demolitionstructuredetails.DemilitionReportPath = demolitionstructuredetails.DemolitionReportFile == null ? demolitionstructuredetails.DemilitionReportPath : fileHelper.SaveFile1(DemolitionReportFilePath, demolitionstructuredetails.DemolitionReportFile);
                demolitionstructuredetails.CreatedBy = SiteContext.UserId;
                var result = await _demolitionstructuredetailsService.Create(demolitionstructuredetails);

                if (result)
                {
                    ///for after file:

                    if (demolitionstructuredetails.AfterPhotoFile != null && demolitionstructuredetails.AfterPhotoFile.Count > 0)
                    {
                        List<Demolitionstructureafterdemolitionphotofiledetails> demolitionstructureafterdemolitionphotofiledetails = new List<Demolitionstructureafterdemolitionphotofiledetails>();
                        for (int i = 0; i < demolitionstructuredetails.AfterPhotoFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(AfterPhotoFilePath, demolitionstructuredetails.AfterPhotoFile[i]);
                            demolitionstructureafterdemolitionphotofiledetails.Add(new Demolitionstructureafterdemolitionphotofiledetails
                            {
                                DemolitionStructureDetailsId = demolitionstructuredetails.Id,
                                AfterPhotoFilePath = FilePath
                            });
                        }
                        foreach (var item in demolitionstructureafterdemolitionphotofiledetails)
                        {
                            result = await _demolitionstructuredetailsService.SaveDemolitionstructureafterdemolitionphotofiledetails(item);
                        }
                    }

                    //for before file:

                    if (demolitionstructuredetails.BeforePhotoFile != null && demolitionstructuredetails.BeforePhotoFile.Count > 0)
                    {
                        List<Demolitionstructurebeforedemolitionphotofiledetails> demolitionstructurebeforedemolitionphotofiledetails = new List<Demolitionstructurebeforedemolitionphotofiledetails>();
                        for (int i = 0; i < demolitionstructuredetails.BeforePhotoFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(BeforePhotoFilePath, demolitionstructuredetails.BeforePhotoFile[i]);
                            demolitionstructurebeforedemolitionphotofiledetails.Add(new Demolitionstructurebeforedemolitionphotofiledetails
                            {
                                DemolitionStructureId = demolitionstructuredetails.Id,
                                BeforePhotoFilePath = FilePath
                            });
                        }
                        foreach (var item in demolitionstructurebeforedemolitionphotofiledetails)
                        {
                            result = await _demolitionstructuredetailsService.SaveDemolitionstructurebeforedemolitionphotofiledetails(item);
                        }
                    }
                 if (demolitionstructuredetails.StructrureId != null)
                    {
                        List<Demolitionstructure> demolitionstructure = new List<Demolitionstructure>();
                        for (int i = 0; i < demolitionstructuredetails.StructrureId.Count(); i++)
                        {
                            demolitionstructure.Add(new Demolitionstructure
                            {

                                StructureId = demolitionstructuredetails.StructrureId[i],
                                NoOfStructrure = demolitionstructuredetails.NoOfStructrure[i],
                                DemolitionStructureDetailsId = demolitionstructuredetails.Id
                            });
                        }
                        foreach (var item in demolitionstructure)
                        {
                            result = await _demolitionstructuredetailsService.SaveDemolitionstructure(item);
                        }
                    }

                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var result1 = await _demolitionstructuredetailsService.GetAllDemolitionstructuredetails();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demolitionstructuredetails);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(demolitionstructuredetails);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demolitionstructuredetails);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var demolitionstructuredetails = await _demolitionstructuredetailsService.FetchSingleResult(id);
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
          
            demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();

            if (demolitionstructuredetails == null)
            {
                return NotFound();
            }
            return View(demolitionstructuredetails);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Demolitionstructuredetails demolitionstructuredetails)
        {
            var Data = await _demolitionstructuredetailsService.FetchSingleResult(id);
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
            demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
            demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();
            string AfterPhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:AfterPhotoFilePath").Value.ToString();
            string BeforePhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:BeforePhotoFilePath").Value.ToString();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                demolitionstructuredetails.DemilitionReportPath = demolitionstructuredetails.DemolitionReportFile == null ? demolitionstructuredetails.DemilitionReportPath : fileHelper.SaveFile1(DemolitionReportFilePath, demolitionstructuredetails.DemolitionReportFile);
                demolitionstructuredetails.ModifiedBy = SiteContext.UserId;

                var result = await _demolitionstructuredetailsService.Update(id, demolitionstructuredetails);
                if (result)
                {                   
                    //for after file:

                    if (demolitionstructuredetails.AfterPhotoFile != null && demolitionstructuredetails.AfterPhotoFile.Count > 0)
                    {
                        List<Demolitionstructureafterdemolitionphotofiledetails> demolitionstructureafterdemolitionphotofiledetails = new List<Demolitionstructureafterdemolitionphotofiledetails>();
                        result = await _demolitionstructuredetailsService.DeleteDemolitionstructureafterdemolitionphotofiledetails(id);
                        for (int i = 0; i < demolitionstructuredetails.AfterPhotoFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(AfterPhotoFilePath, demolitionstructuredetails.AfterPhotoFile[i]);
                            demolitionstructureafterdemolitionphotofiledetails.Add(new Demolitionstructureafterdemolitionphotofiledetails
                            {
                                DemolitionStructureDetailsId = demolitionstructuredetails.Id,
                                AfterPhotoFilePath = FilePath
                            });
                        }


                        foreach (var item in demolitionstructureafterdemolitionphotofiledetails)
                        {
                            result = await _demolitionstructuredetailsService.SaveDemolitionstructureafterdemolitionphotofiledetails(item);
                        }
                    }

                    //for before file:

                    if (demolitionstructuredetails.BeforePhotoFile != null && demolitionstructuredetails.BeforePhotoFile.Count > 0)
                    {
                        List<Demolitionstructurebeforedemolitionphotofiledetails> demolitionstructurebeforedemolitionphotofiledetails = new List<Demolitionstructurebeforedemolitionphotofiledetails>();
                        result = await _demolitionstructuredetailsService.DeleteDemolitionstructurebeforedemolitionphotofiledetails(id);
                        for (int i = 0; i < demolitionstructuredetails.BeforePhotoFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(BeforePhotoFilePath, demolitionstructuredetails.BeforePhotoFile[i]);
                            demolitionstructurebeforedemolitionphotofiledetails.Add(new Demolitionstructurebeforedemolitionphotofiledetails
                            {
                                DemolitionStructureId = demolitionstructuredetails.Id,
                                BeforePhotoFilePath = FilePath
                            });
                        }
                        foreach (var item in demolitionstructurebeforedemolitionphotofiledetails)
                        {
                            result = await _demolitionstructuredetailsService.SaveDemolitionstructurebeforedemolitionphotofiledetails(item);
                        }
                    }
                    if (demolitionstructuredetails.StructrureId != null)
                    {
                        List<Demolitionstructure> demolitionstructure = new List<Demolitionstructure>();
                        result = await _demolitionstructuredetailsService.DeleteDemolitionstructure(id);
                        for (int i = 0; i < demolitionstructuredetails.StructrureId.Count(); i++)
                        {
                            demolitionstructure.Add(new Demolitionstructure
                            {
                                StructureId = demolitionstructuredetails.StructrureId[i],
                                NoOfStructrure = demolitionstructuredetails.NoOfStructrure[i],
                                DemolitionStructureDetailsId = demolitionstructuredetails.Id
                            });
                        }
                        foreach (var item in demolitionstructure)
                        {
                            result = await _demolitionstructuredetailsService.SaveDemolitionstructure(item);
                        }
                    }

                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _demolitionstructuredetailsService.GetAllDemolitionstructuredetails();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demolitionstructuredetails);
                    }

                }
                else
                {
                    return View(demolitionstructuredetails);
                }
            }
            else
            {
                return View(demolitionstructuredetails);
            }
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var demolitionstructuredetails = await _demolitionstructuredetailsService.FetchSingleResult(id);
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
          
            demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();

            if (demolitionstructuredetails == null)
            {
                return NotFound();
            }
            return View(demolitionstructuredetails);
        }

        [AuthorizeContext(ViewAction.Delete)]
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
            try
            {
                DepartmentId = DepartmentId ?? 0;
                var data = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(DepartmentId));
                return Json(data);
            }
            catch (Exception ex)
            {
                throw;
            }
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
        public async Task<IActionResult> DownloadBeforePhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Demolitionstructurebeforedemolitionphotofiledetails Data = await _demolitionstructuredetailsService.GetDemolitionstructurebeforedemolitionphotofiledetails(Id);
            string filename = Data.BeforePhotoFilePath;
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


        public async Task<IActionResult> DemolitionDiaryList()
        {
            var result = await _demolitionstructuredetailsService.GetAllDemolitionstructuredetailsList();
            List<DemolitionDiaryListDto> data = new List<DemolitionDiaryListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemolitionDiaryListDto()
                    {
                        Id = result[i].Id,

                        Department = result[i].Department == null ? "" : result[i].Department.Name.ToString(),
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name.ToString(),
                        Division = result[i].Division == null ? "" : result[i].Division.Name.ToString(),
                      
                        Locality = result[i].Locality == null ? "" : result[i].Locality.Name.ToString(),
                        DateOfapprovalofdemolition = Convert.ToDateTime(result[i].DateOfApprovalDemolition).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].DateOfApprovalDemolition).ToString("dd-MMM-yyyy"),


                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


        public async Task<IActionResult> ViewDemolitionReport(int Id)
        {
            FileHelper file = new FileHelper();
            Demolitionstructuredetails Data = await _demolitionstructuredetailsService.FetchSingleResult(Id);
            string filename = DemolitionReportFilePath + Data.DemilitionReportPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
            //string filename = Data.DocumentFileName;
            //return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }


    }
}
