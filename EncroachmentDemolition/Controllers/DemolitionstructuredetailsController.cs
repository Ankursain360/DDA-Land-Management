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
using Microsoft.AspNetCore.Hosting;

using Service.IApplicationService;
using System.Text;


using Microsoft.AspNetCore.Http;



namespace EncroachmentDemolition.Controllers
{
    public class DemolitionstructuredetailsController : BaseController
    {
        public IConfiguration _configuration;
        public readonly IDemolitionstructuredetailsService _demolitionstructuredetailsService;
        string DemolitionReportFilePath = "";
        string AfterPhotoFilePath = "";
        string BeforePhotoFilePath = "";

       public readonly IAnnexureAApprovalService _annexureAApprovalService;
        public readonly IAnnexureAService _annexureAService;
        public readonly IEncroachmentRegisterationApprovalService _encroachmentRegisterationApprovalService;       
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public object JsonRequestBehavior { get; private set; }


        public DemolitionstructuredetailsController(IDemolitionstructuredetailsService demolitionstructuredetailsService,
             IEncroachmentRegisterationApprovalService encroachmentRegisterationApprovalService,
            IEncroachmentRegisterationService encroachmentRegisterationService,
            IConfiguration configuration, IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IAnnexureAService annexureAService, IAnnexureAApprovalService annexureAApprovalService,
             IHostingEnvironment en)
        {
            _demolitionstructuredetailsService = demolitionstructuredetailsService;
            _configuration = configuration;
            _encroachmentRegisterationApprovalService = encroachmentRegisterationApprovalService;
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _annexureAService = annexureAService;
            _annexureAApprovalService = annexureAApprovalService;
            _hostingEnvironment = en; 
            AfterPhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:AfterPhotoFilePath").Value.ToString();
            BeforePhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:BeforePhotoFilePath").Value.ToString();
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
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _demolitionstructuredetailsService.FetchSingleResultonId(id);
            if (Data == null)
            {
                Demolitionstructuredetails Data1 = new Demolitionstructuredetails();

                var Data2 = await _demolitionstructuredetailsService.FetchSingleResultOfFixingDemolition(id);
                Data1.FixingDemolitionId = id;
                ViewBag.PrimaryId = 0;
                ViewBag.EncroachmentId = Data2.EncroachmentId;
                ViewBag.WatchWardId = Data2.Encroachment.WatchWardId;
                Data1.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
                Data1.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(Data1.DepartmentId ?? 0));
                Data1.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(Data1.ZoneId ?? 0));
                Data1.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(Data1.DivisionId ?? 0));
                Data1.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
                Data1.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();

                return View(Data1);
            }
            else
            {
                Data.FixingDemolitionId = id;
                ViewBag.PrimaryId = Data.Id;
                ViewBag.EncroachmentId = Data.FixingDemolition.EncroachmentId;
                ViewBag.WatchWardId = Data.FixingDemolition.Encroachment.WatchWardId;
                Data.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
                Data.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(Data.DepartmentId ?? 0));
                Data.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(Data.ZoneId ?? 0));
                Data.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(Data.DivisionId ?? 0));
                Data.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
                Data.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();

                return View(Data);
            }
            //  Demolitionstructuredetails demolitionstructuredetails = new Demolitionstructuredetails();
            //  demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            //  demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            //  demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            //  demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
            //  demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
            //  demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();
            //return View(demolitionstructuredetails);

        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Demolitionstructuredetails demolitionstructuredetails)
        {
            var result = false;
            ViewBag.PrimaryId = demolitionstructuredetails.Id;
            bool IsValidpdf = CheckMimeType(demolitionstructuredetails);
            var Data1 = await _demolitionstructuredetailsService.FetchSingleResultOfFixingDemolition(demolitionstructuredetails.FixingDemolitionId);

            ViewBag.EncroachmentId = Data1.EncroachmentId;
            ViewBag.WatchWardId = Data1.Encroachment.WatchWardId;
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));

            demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
            demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();



            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    FileHelper fileHelper = new FileHelper();
                    demolitionstructuredetails.DemilitionReportPath = demolitionstructuredetails.DemolitionReportFile == null ? demolitionstructuredetails.DemilitionReportPath : fileHelper.SaveFile1(DemolitionReportFilePath, demolitionstructuredetails.DemolitionReportFile);
                    demolitionstructuredetails.CreatedBy = SiteContext.UserId;
                    // var result = await _demolitionstructuredetailsService.Create(demolitionstructuredetails);


                    var DiaryData = await _demolitionstructuredetailsService.FetchSingleResultonId(demolitionstructuredetails.FixingDemolitionId);
                    if (DiaryData == null)
                    {
                        demolitionstructuredetails.CreatedBy = SiteContext.UserId;
                        result = await _demolitionstructuredetailsService.Create(demolitionstructuredetails);
                        if (result)
                        {
                            ///for after file:

                            if (demolitionstructuredetails.AfterPhotoFile != null && demolitionstructuredetails.AfterPhotoFile.Count > 0)
                            {
                                List<Demolitionstructureafterdemolitionphotofiledetails> demolitionstructureafterdemolitionphotofiledetails = new List<Demolitionstructureafterdemolitionphotofiledetails>();
                                for (int i = 0; i < demolitionstructuredetails.AfterPhotoFile.Count; i++)
                                {
                                    string FilePath = fileHelper.SaveFile1(AfterPhotoFilePath, demolitionstructuredetails.AfterPhotoFile[i]);
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
                                    string FilePath = fileHelper.SaveFile1(BeforePhotoFilePath, demolitionstructuredetails.BeforePhotoFile[i]);
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



                            //************ Save Demolishedstructurerpt  ************  

                            if (demolitionstructuredetails.Date1 != null &&
                                demolitionstructuredetails.StructureId1 != null)

                            {
                                if (demolitionstructuredetails.StructureId1.Count > 0)

                                {
                                    List<Demolishedstructurerpt> rpt1 = new List<Demolishedstructurerpt>();
                                    for (int i = 0; i < demolitionstructuredetails.StructureId1.Count; i++)
                                    {
                                        rpt1.Add(new Demolishedstructurerpt
                                        {
                                            DemolitionDate = demolitionstructuredetails.Date1.Count <= i ? null : demolitionstructuredetails.Date1[i],
                                            StructureId = demolitionstructuredetails.StructureId1.Count <= i ? null : demolitionstructuredetails.StructureId1[i],
                                            NoOfStructureDemolished = demolitionstructuredetails.NoOfStructureDemolished.Count <= i ? null : demolitionstructuredetails.NoOfStructureDemolished[i],
                                            NoOfStructureRemaining = demolitionstructuredetails.NoOfStructureRemaining.Count <= i ? null : demolitionstructuredetails.NoOfStructureRemaining[i],
                                            DemolitionStructureDetailsId = demolitionstructuredetails.Id,
                                            CreatedBy = SiteContext.UserId
                                        });
                                    }
                                    foreach (var item in rpt1)
                                    {
                                        result = await _demolitionstructuredetailsService.SaveDemolishedstructurerpt(item);
                                    }
                                }
                            }
                            //************ Save Areareclaimedrpt  ************  
                            if (demolitionstructuredetails.Date2 != null &&
                                demolitionstructuredetails.Area1 != null)

                            {
                                if (demolitionstructuredetails.Area1.Count > 0)

                                {
                                    List<Areareclaimedrpt> rpt1 = new List<Areareclaimedrpt>();
                                    for (int i = 0; i < demolitionstructuredetails.Area1.Count; i++)
                                    {
                                        rpt1.Add(new Areareclaimedrpt
                                        {
                                            DemolitionDate = demolitionstructuredetails.Date2.Count <= i ? null : demolitionstructuredetails.Date2[i],
                                            AreaReclaimed = demolitionstructuredetails.Area1.Count <= i ? null : demolitionstructuredetails.Area1[i],
                                            AreaToBeReclaimed = demolitionstructuredetails.AreaToBeReclaimed.Count <= i ? null : demolitionstructuredetails.AreaToBeReclaimed[i],
                                            DemolitionStructureDetailsId = demolitionstructuredetails.Id,
                                            CreatedBy = SiteContext.UserId
                                        });
                                    }
                                    foreach (var item in rpt1)
                                    {
                                        result = await _demolitionstructuredetailsService.SaveAreareclaimedrpt(item);
                                    }
                                }
                            }
                            if (result)
                            {
                                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                                return View("Index1");
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
                        demolitionstructuredetails.ModifiedBy = SiteContext.UserId;
                        result = await _demolitionstructuredetailsService.Update(demolitionstructuredetails.Id, demolitionstructuredetails);
                        if (result)
                        {
                            //for after file:

                            if (demolitionstructuredetails.AfterPhotoFile != null && demolitionstructuredetails.AfterPhotoFile.Count > 0)
                            {
                                List<Demolitionstructureafterdemolitionphotofiledetails> demolitionstructureafterdemolitionphotofiledetails = new List<Demolitionstructureafterdemolitionphotofiledetails>();
                                result = await _demolitionstructuredetailsService.DeleteDemolitionstructureafterdemolitionphotofiledetails(demolitionstructuredetails.Id);
                                for (int i = 0; i < demolitionstructuredetails.AfterPhotoFile.Count; i++)
                                {
                                    string FilePath = fileHelper.SaveFile1(AfterPhotoFilePath, demolitionstructuredetails.AfterPhotoFile[i]);
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
                                result = await _demolitionstructuredetailsService.DeleteDemolitionstructurebeforedemolitionphotofiledetails(demolitionstructuredetails.Id);
                                for (int i = 0; i < demolitionstructuredetails.BeforePhotoFile.Count; i++)
                                {
                                    string FilePath = fileHelper.SaveFile1(BeforePhotoFilePath, demolitionstructuredetails.BeforePhotoFile[i]);
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


                            //************ Save Demolishedstructurerpt  ************  

                            if (demolitionstructuredetails.Date1 != null &&
                             demolitionstructuredetails.StructureId1 != null)

                            {
                                if (demolitionstructuredetails.StructureId1.Count > 0)

                                {
                                    List<Demolishedstructurerpt> rpt1 = new List<Demolishedstructurerpt>();
                                    result = await _demolitionstructuredetailsService.Deletedemolitionrptdetails(demolitionstructuredetails.Id);
                                    for (int i = 0; i < demolitionstructuredetails.StructureId1.Count; i++)
                                    {
                                        rpt1.Add(new Demolishedstructurerpt
                                        {
                                            DemolitionDate = demolitionstructuredetails.Date1.Count <= i ? null : demolitionstructuredetails.Date1[i],
                                            StructureId = demolitionstructuredetails.StructureId1.Count <= i ? null : demolitionstructuredetails.StructureId1[i],
                                            NoOfStructureDemolished = demolitionstructuredetails.NoOfStructureDemolished.Count <= i ? null : demolitionstructuredetails.NoOfStructureDemolished[i],
                                            NoOfStructureRemaining = demolitionstructuredetails.NoOfStructureRemaining.Count <= i ? null : demolitionstructuredetails.NoOfStructureRemaining[i],
                                            DemolitionStructureDetailsId = demolitionstructuredetails.Id,
                                            CreatedBy = SiteContext.UserId
                                        });
                                    }
                                    foreach (var item in rpt1)
                                    {
                                        result = await _demolitionstructuredetailsService.SaveDemolishedstructurerpt(item);
                                    }
                                }
                            }

                            //************ Save Areareclaimedrpt  ************  

                            if (demolitionstructuredetails.Date2 != null &&
                                demolitionstructuredetails.Area1 != null)

                            {
                                if (demolitionstructuredetails.Area1.Count > 0)

                                {
                                    List<Areareclaimedrpt> rpt1 = new List<Areareclaimedrpt>();
                                    result = await _demolitionstructuredetailsService.Deletedearearptdetails(demolitionstructuredetails.Id);
                                    for (int i = 0; i < demolitionstructuredetails.Area1.Count; i++)
                                    {
                                        rpt1.Add(new Areareclaimedrpt
                                        {
                                            DemolitionDate = demolitionstructuredetails.Date2.Count <= i ? null : demolitionstructuredetails.Date2[i],
                                            AreaReclaimed = demolitionstructuredetails.Area1.Count <= i ? null : demolitionstructuredetails.Area1[i],
                                            AreaToBeReclaimed = demolitionstructuredetails.AreaToBeReclaimed.Count <= i ? null : demolitionstructuredetails.AreaToBeReclaimed[i],
                                            DemolitionStructureDetailsId = demolitionstructuredetails.Id,
                                            CreatedBy = SiteContext.UserId
                                        });
                                    }
                                    foreach (var item in rpt1)
                                    {
                                        result = await _demolitionstructuredetailsService.SaveAreareclaimedrpt(item);
                                    }
                                }
                            }


                            if (result)
                            {
                                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                                return View("Index1");
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
                  
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(demolitionstructuredetails);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demolitionstructuredetails);
            }
        }

        //[HttpPost]
        //[AuthorizeContext(ViewAction.Add)]
        //public async Task<IActionResult> Create(Demolitionstructuredetails demolitionstructuredetails)
        //{
        //    demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
        //    demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
        //    demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
        //    demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));

        //    demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
        //   demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();


        //    string AfterPhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:AfterPhotoFilePath").Value.ToString();
        //    string BeforePhotoFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:BeforePhotoFilePath").Value.ToString();

        //    if (ModelState.IsValid)
        //    {
        //        FileHelper fileHelper = new FileHelper();
        //        demolitionstructuredetails.DemilitionReportPath = demolitionstructuredetails.DemolitionReportFile == null ? demolitionstructuredetails.DemilitionReportPath : fileHelper.SaveFile1(DemolitionReportFilePath, demolitionstructuredetails.DemolitionReportFile);
        //        demolitionstructuredetails.CreatedBy = SiteContext.UserId;
        //        var result = await _demolitionstructuredetailsService.Create(demolitionstructuredetails);

        //        if (result)
        //        {
        //            ///for after file:

        //            if (demolitionstructuredetails.AfterPhotoFile != null && demolitionstructuredetails.AfterPhotoFile.Count > 0)
        //            {
        //                List<Demolitionstructureafterdemolitionphotofiledetails> demolitionstructureafterdemolitionphotofiledetails = new List<Demolitionstructureafterdemolitionphotofiledetails>();
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

        //            //for before file:

        //            if (demolitionstructuredetails.BeforePhotoFile != null && demolitionstructuredetails.BeforePhotoFile.Count > 0)
        //            {
        //                List<Demolitionstructurebeforedemolitionphotofiledetails> demolitionstructurebeforedemolitionphotofiledetails = new List<Demolitionstructurebeforedemolitionphotofiledetails>();
        //                for (int i = 0; i < demolitionstructuredetails.BeforePhotoFile.Count; i++)
        //                {
        //                    string FilePath = fileHelper.SaveFile(BeforePhotoFilePath, demolitionstructuredetails.BeforePhotoFile[i]);
        //                    demolitionstructurebeforedemolitionphotofiledetails.Add(new Demolitionstructurebeforedemolitionphotofiledetails
        //                    {
        //                        DemolitionStructureId = demolitionstructuredetails.Id,
        //                        BeforePhotoFilePath = FilePath
        //                    });
        //                }
        //                foreach (var item in demolitionstructurebeforedemolitionphotofiledetails)
        //                {
        //                    result = await _demolitionstructuredetailsService.SaveDemolitionstructurebeforedemolitionphotofiledetails(item);
        //                }
        //            }
        //            //if (demolitionstructuredetails.StructrureId != null)
        //            //   {
        //            //       List<Demolitionstructure> demolitionstructure = new List<Demolitionstructure>();
        //            //       for (int i = 0; i < demolitionstructuredetails.StructrureId.Count(); i++)
        //            //       {
        //            //           demolitionstructure.Add(new Demolitionstructure
        //            //           {

        //            //               StructureId = demolitionstructuredetails.StructrureId[i],
        //            //               NoOfStructrure = demolitionstructuredetails.NoOfStructrure[i],
        //            //               DemolitionStructureDetailsId = demolitionstructuredetails.Id
        //            //           });
        //            //       }
        //            //       foreach (var item in demolitionstructure)
        //            //       {
        //            //           result = await _demolitionstructuredetailsService.SaveDemolitionstructure(item);
        //            //       }
        //            //   }


        //            //************ Save Demolishedstructurerpt  ************  

        //            if (demolitionstructuredetails.Date1 != null &&
        //                demolitionstructuredetails.StructureId1 != null)

        //            {
        //                if (demolitionstructuredetails.StructureId1.Count > 0 )

        //                {
        //                    List<Demolishedstructurerpt> rpt1 = new List<Demolishedstructurerpt>();
        //                    for (int i = 0; i < demolitionstructuredetails.StructureId1.Count; i++)
        //                    {
        //                        rpt1.Add(new Demolishedstructurerpt
        //                        {
        //                            DemolitionDate = demolitionstructuredetails.Date1.Count <= i ? null : demolitionstructuredetails.Date1[i],
        //                            StructureId = demolitionstructuredetails.StructureId1.Count <= i ? null : demolitionstructuredetails.StructureId1[i],
        //                            NoOfStructureDemolished = demolitionstructuredetails.NoOfStructureDemolished.Count <= i ? null : demolitionstructuredetails.NoOfStructureDemolished[i],
        //                            NoOfStructureRemaining = demolitionstructuredetails.NoOfStructureRemaining.Count <= i ? null : demolitionstructuredetails.NoOfStructureRemaining[i],
        //                            DemolitionStructureDetailsId = demolitionstructuredetails.Id,
        //                            CreatedBy = SiteContext.UserId
        //                        });
        //                    }
        //                    foreach (var item in rpt1)
        //                    {
        //                        result = await _demolitionstructuredetailsService.SaveDemolishedstructurerpt(item);
        //                    }
        //                }
        //            }
        //            //************ Save Areareclaimedrpt  ************  
        //            if (demolitionstructuredetails.Date2 != null &&
        //                demolitionstructuredetails.Area1 != null)

        //            {
        //                if (demolitionstructuredetails.Area1.Count > 0)

        //                {
        //                    List<Areareclaimedrpt> rpt1 = new List<Areareclaimedrpt>();
        //                    for (int i = 0; i < demolitionstructuredetails.Area1.Count; i++)
        //                    {
        //                        rpt1.Add(new Areareclaimedrpt
        //                        {
        //                            DemolitionDate = demolitionstructuredetails.Date2.Count <= i ? null : demolitionstructuredetails.Date2[i],
        //                            AreaReclaimed = demolitionstructuredetails.Area1.Count <= i ? null : demolitionstructuredetails.Area1[i],
        //                            AreaToBeReclaimed = demolitionstructuredetails.AreaToBeReclaimed.Count <= i ? null : demolitionstructuredetails.AreaToBeReclaimed[i],
        //                            DemolitionStructureDetailsId = demolitionstructuredetails.Id,
        //                            CreatedBy = SiteContext.UserId
        //                        });
        //                    }
        //                    foreach (var item in rpt1)
        //                    {
        //                        result = await _demolitionstructuredetailsService.SaveAreareclaimedrpt(item);
        //                    }
        //                }
        //            }
        //            if (result)
        //            {
        //                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
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

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var demolitionstructuredetails = await _demolitionstructuredetailsService.FetchSingleResult(id);
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
            demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
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
            bool IsValidpdf = CheckMimeType(demolitionstructuredetails);
            var Data = await _demolitionstructuredetailsService.FetchSingleResult(id);
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
            demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
            demolitionstructuredetails.DemolitionStructure = await _demolitionstructuredetailsService.GetStructure();

            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
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
                            string FilePath = fileHelper.SaveFile1(AfterPhotoFilePath, demolitionstructuredetails.AfterPhotoFile[i]);
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
                            string FilePath = fileHelper.SaveFile1(BeforePhotoFilePath, demolitionstructuredetails.BeforePhotoFile[i]);
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
                    //if (demolitionstructuredetails.StructrureId != null)
                    //{
                    //    List<Demolitionstructure> demolitionstructure = new List<Demolitionstructure>();
                    //    result = await _demolitionstructuredetailsService.DeleteDemolitionstructure(id);
                    //    for (int i = 0; i < demolitionstructuredetails.StructrureId.Count(); i++)
                    //    {
                    //        demolitionstructure.Add(new Demolitionstructure
                    //        {
                    //            StructureId = demolitionstructuredetails.StructrureId[i],
                    //            NoOfStructrure = demolitionstructuredetails.NoOfStructrure[i],
                    //            DemolitionStructureDetailsId = demolitionstructuredetails.Id
                    //        });
                    //    }
                    //    foreach (var item in demolitionstructure)
                    //    {
                    //        result = await _demolitionstructuredetailsService.SaveDemolitionstructure(item);
                    //    }
                    //}

                    //************ Save Demolishedstructurerpt  ************  

                    if (demolitionstructuredetails.Date1 != null &&
                     demolitionstructuredetails.StructureId1 != null)

                    {
                        if (demolitionstructuredetails.StructureId1.Count > 0)

                        {
                            List<Demolishedstructurerpt> rpt1 = new List<Demolishedstructurerpt>();
                            result = await _demolitionstructuredetailsService.Deletedemolitionrptdetails(id);
                            for (int i = 0; i < demolitionstructuredetails.StructureId1.Count; i++)
                            {
                                rpt1.Add(new Demolishedstructurerpt
                                {
                                    DemolitionDate = demolitionstructuredetails.Date1.Count <= i ? null : demolitionstructuredetails.Date1[i],
                                    StructureId = demolitionstructuredetails.StructureId1.Count <= i ? null : demolitionstructuredetails.StructureId1[i],
                                    NoOfStructureDemolished = demolitionstructuredetails.NoOfStructureDemolished.Count <= i ? null : demolitionstructuredetails.NoOfStructureDemolished[i],
                                    NoOfStructureRemaining = demolitionstructuredetails.NoOfStructureRemaining.Count <= i ? null : demolitionstructuredetails.NoOfStructureRemaining[i],
                                    DemolitionStructureDetailsId = demolitionstructuredetails.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in rpt1)
                            {
                                result = await _demolitionstructuredetailsService.SaveDemolishedstructurerpt(item);
                            }
                        }
                    }

                    //************ Save Areareclaimedrpt  ************  

                    if (demolitionstructuredetails.Date2 != null &&
                        demolitionstructuredetails.Area1 != null)

                    {
                        if (demolitionstructuredetails.Area1.Count > 0)

                        {
                            List<Areareclaimedrpt> rpt1 = new List<Areareclaimedrpt>();
                            result = await _demolitionstructuredetailsService.Deletedearearptdetails(id);
                            for (int i = 0; i < demolitionstructuredetails.Area1.Count; i++)
                            {
                                rpt1.Add(new Areareclaimedrpt
                                {
                                    DemolitionDate = demolitionstructuredetails.Date2.Count <= i ? null : demolitionstructuredetails.Date2[i],
                                    AreaReclaimed = demolitionstructuredetails.Area1.Count <= i ? null : demolitionstructuredetails.Area1[i],
                                    AreaToBeReclaimed = demolitionstructuredetails.AreaToBeReclaimed.Count <= i ? null : demolitionstructuredetails.AreaToBeReclaimed[i],
                                    DemolitionStructureDetailsId = demolitionstructuredetails.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in rpt1)
                            {
                                result = await _demolitionstructuredetailsService.SaveAreareclaimedrpt(item);
                            }
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
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
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
            demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetMasterStructure();
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

        public async Task<IActionResult> DownloadAfterPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Demolitionstructureafterdemolitionphotofiledetails Data = await _demolitionstructuredetailsService.GetAfterphotofile(Id);
            string filename = AfterPhotoFilePath +  Data.AfterPhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadBeforePhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Demolitionstructurebeforedemolitionphotofiledetails Data = await _demolitionstructuredetailsService.GetBeforephotofile(Id);
            string filename = BeforePhotoFilePath + Data.BeforePhotoFilePath;
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

        public async Task<JsonResult> GetDetailsDemolitionRpt(int? Id)
        {
            Id = Id ?? 0;
            var data = await _demolitionstructuredetailsService.GetAlldemolitionrptdetails(Convert.ToInt32(Id));
            //data.StructureId = await _demolitionstructuredetailsService.GetMasterStructure();

            return Json(data.Select(x => new
            {
                x.Id,
              
                Date = Convert.ToDateTime(x.DemolitionDate).ToString("yyyy-MM-dd"),
               // Structname = (x.Structure.Name.Split('\t')).FirstOrDefault(),
                 x.StructureId,
                x.NoOfStructureDemolished,
                x.NoOfStructureRemaining
            }));
        }
        public async Task<JsonResult> GetDetailsAreaRpt(int? Id)
        {
            Id = Id ?? 0;
            var data = await _demolitionstructuredetailsService.GetAllArearptdetails(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                //x.DemolitionDate,
                Date = Convert.ToDateTime(x.DemolitionDate).ToString("yyyy-MM-dd"),
                x.AreaReclaimed,
                x.AreaToBeReclaimed
            }));
        }

        //added by ishu 17june2021


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index1()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List1([FromBody] DemolitionstructuredetailsDto1 model)
        {

            var result = await _demolitionstructuredetailsService.GetPagedDemolitiondiary(model, SiteContext.UserId, (int)ApprovalActionStatus.Approved);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List1", result);


        }

        #region Watch & Ward  Details
        public async Task<PartialViewResult> WatchWardView(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            if (Data != null)
                Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            return PartialView("_WatchWard", Data);
        }



        public async Task<FileResult> ViewDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string path = Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion

        #region EncroachmentRegisteration Details
        public async Task<PartialViewResult> EncroachmentRegisterView(int id)
        {
            var encroachmentRegisterations = await _encroachmentRegisterationService.FetchSingleResult(id);
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            //encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            encroachmentRegisterations.PropertyInventoryKhasraList = await _encroachmentRegisterationService.GetAllKhasraListFromPropertyInventory(encroachmentRegisterations.ZoneId, encroachmentRegisterations.DepartmentId);
            return PartialView("_EncroachmentRegisterView", encroachmentRegisterations);
        }




        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentPhotoFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentPhotoFileDetails(Id);
            string filename = Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<JsonResult> DetailsOfRepeater(int? Id)
        {
            Id = Id ?? 0;
            var data = await _encroachmentRegisterationService.GetDetailsOfEncroachment(Convert.ToInt32(Id));
            return Json(data.Select(x => new { x.CountOfStructure, x.DateOfEncroachment, x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus, x.ReligiousStructure }));
        }

        public async Task<IActionResult> DownloadFirfile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentFirFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentFirFileDetails(Id);
            string filename = Data.FirFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadLocationMapFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentLocationMapFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentLocationMapFileDetails(Id);
            string filename = Data.LocationMapFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        #endregion

        #region AnnexureA Details
        public async Task<PartialViewResult> AnnexureADetails(int id)
        {
            var Data = await _annexureAService.FetchSingleResult(id);
            Data.Demolitionchecklist = await _annexureAService.GetDemolitionchecklist();
            Data.Demolitionprogram = await _annexureAService.GetDemolitionprogram();
            Data.Demolitiondocument = await _annexureAService.GetDemolitiondocument();
            return PartialView("_AnnexureAView", Data);
        }

        public async Task<FileResult> ViewDocumentAnnexureA(int Id)
        {
            FileHelper file = new FileHelper();
            Fixingdocument Data = await _annexureAService.GetAnnexureAfiledetails(Id);
            string path = Data.DocumentDetails;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion



        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            DemolitionReportFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:DemolitionReportFilePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DemolitionReportFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:DemolitionReportFilePath").Value.ToString();
                string FilePath = Path.Combine(DemolitionReportFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DemolitionReportFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DemolitionReportFilePath);// Try to create the directory.
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
                                fullpath =_configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:DemolitionReportFilePath").Value.ToString();

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



        public bool CheckMimeType(Demolitionstructuredetails demolitionstructuredetails)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            string extension = string.Empty;
            DemolitionReportFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:DemolitionReportFilePath").Value.ToString();
            IFormFile files = demolitionstructuredetails.DemolitionReportFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DemolitionReportFilePath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:DemolitionReportFilePath").Value.ToString();
                string FilePath = Path.Combine(DemolitionReportFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DemolitionReportFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DemolitionReportFilePath);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:DemolitionstructuredetailsFiles:DemolitionReportFilePath").Value.ToString();

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


        //Demolistion Dashboard

        public async Task<PartialViewResult> GetDashboard(int userId, int roleId)
        {  
           
            var results = await _demolitionstructuredetailsService.GetDashboardData(userId, roleId);
              
            return PartialView("_dashboard", results);
        }

        public async Task<PartialViewResult> GetDashboardListData([FromBody] DemolitionDasboardDataDto model)
        {

            var results = await _demolitionstructuredetailsService.GetDashboardListData(model);

            return PartialView("_ModelDashboardData", results);
        }

        //


    }
}
