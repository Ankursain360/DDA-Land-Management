using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using EncroachmentDemolition.Filters;
using Core.Enum;
using System.Data;
using Dto.Master;

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionPoliceAssistenceLetterController : BaseController
    {
        public readonly IDemolitionPoliceAssistenceLetterService _demolitionPoliceAssistenceLetterService;
        public readonly IAnnexureAApprovalService _annexureAApprovalService;
        public readonly IAnnexureAService _annexureAService;
        public readonly IEncroachmentRegisterationApprovalService _encroachmentRegisterationApprovalService;
        public IConfiguration _configuration;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IHostingEnvironment _hostingEnvironment;

        string targetPathDocument = "";
        public DemolitionPoliceAssistenceLetterController(IDemolitionPoliceAssistenceLetterService demolitionPoliceAssistenceLetterService,
            IEncroachmentRegisterationApprovalService encroachmentRegisterationApprovalService, 
            IEncroachmentRegisterationService encroachmentRegisterationService,
            IConfiguration configuration, IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IAnnexureAService annexureAService, IAnnexureAApprovalService annexureAApprovalService,
            IHostingEnvironment en)
        {
            _demolitionPoliceAssistenceLetterService = demolitionPoliceAssistenceLetterService;
            _encroachmentRegisterationApprovalService = encroachmentRegisterationApprovalService;
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _configuration = configuration;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _annexureAService = annexureAService;
            _annexureAApprovalService = annexureAApprovalService;
            _hostingEnvironment = en;
            targetPathDocument = _configuration.GetSection("FilePaths:DemolitionPoliceAssistenceFiles:LetterFilePath").Value.ToString();
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> _PoliceAssistanceLetter(int id)
        {

            var Data = await _demolitionPoliceAssistenceLetterService.Fetchletterdetails(id);
            if (Data == null)
            {
                return NotFound();
            }
            return PartialView(Data);

        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemolitionPoliceAssistenceLetterSearchDto model)
        {
            
                var result = await _demolitionPoliceAssistenceLetterService.GetPagedApprovedAnnexureA(model, SiteContext.UserId, (int)ApprovalActionStatus.Approved);
                ViewBag.IsApproved = model.StatusId;
                return PartialView("_List", result);
           
           
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var demolitionpoliceData = await _demolitionPoliceAssistenceLetterService.FetchSingleResultButOnAneexureId(id);
            if(demolitionpoliceData == null)
            {
                Demolitionpoliceassistenceletter Data = new Demolitionpoliceassistenceletter();
                var Data1 = await _demolitionPoliceAssistenceLetterService.FetchSingleResultOfFixingDemolition(id);
                Data.FixingDemolitionId = id;
                ViewBag.PrimaryId = 0;
                ViewBag.EncroachmentId = Data1.EncroachmentId;
                ViewBag.WatchWardId = Data1.Encroachment.WatchWardId;
                Data.LetterDate = DateTime.Now;
                return View(Data);
            }
            else
            {
                demolitionpoliceData.FixingDemolitionId = id;
                ViewBag.PrimaryId = demolitionpoliceData.Id;
                ViewBag.EncroachmentId = demolitionpoliceData.FixingDemolition.EncroachmentId;
                ViewBag.WatchWardId = demolitionpoliceData.FixingDemolition.Encroachment.WatchWardId;
                return View(demolitionpoliceData);
            }
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        {
            var result = false;
            ViewBag.PrimaryId = demolitionpoliceassistenceletter.Id;
            var Data1 = await _demolitionPoliceAssistenceLetterService.FetchSingleResultOfFixingDemolition(demolitionpoliceassistenceletter.FixingDemolitionId);
            ViewBag.EncroachmentId = Data1.EncroachmentId;
            ViewBag.WatchWardId = Data1.Encroachment.WatchWardId;
            if (ModelState.IsValid)
            {
                //if (demolitionpoliceassistenceletter.GenerateUpload == 0)
                //{
                //    if (demolitionpoliceassistenceletter.MeetingDate == null || demolitionpoliceassistenceletter.MeetingTime == null)
                //    {
                //        ViewBag.Message = Alert.Show("Meeting Date and Time is Mandatory", "", AlertType.Warning);
                //        return View(demolitionpoliceassistenceletter);
                //    }
                //}
                //else if (demolitionpoliceassistenceletter.GenerateUpload == 1)
                //{
                //    if (demolitionpoliceassistenceletter.Document == null)
                //    {
                //        ViewBag.Message = Alert.Show("Document is Mandatory", "", AlertType.Warning);
                //        return View(demolitionpoliceassistenceletter);
                //    }
                //}
                string LetterFileName = "";
                string LetterfilePath = "";
                //if (demolitionpoliceassistenceletter.Document != null)
                //{
                //    if (!Directory.Exists(targetPathDocument))
                //    {
                //        DirectoryInfo di = Directory.CreateDirectory(targetPathDocument);// Try to create the directory.
                //    }
                //    LetterFileName = Guid.NewGuid().ToString() + "_" + demolitionpoliceassistenceletter.Document.FileName;
                //    LetterfilePath = Path.Combine(targetPathDocument, LetterFileName);
                //    using (var stream = new FileStream(LetterfilePath, FileMode.Create))
                //    {
                //        demolitionpoliceassistenceletter.Document.CopyTo(stream);
                //    }
                //    demolitionpoliceassistenceletter.FilePath = LetterFileName;
                //}


                var demolitionpoliceData =await _demolitionPoliceAssistenceLetterService.FetchSingleResultButOnAneexureId(demolitionpoliceassistenceletter.FixingDemolitionId);
                if (demolitionpoliceData == null)
                {
                    demolitionpoliceassistenceletter.CreatedBy = SiteContext.UserId;
                    result = await _demolitionPoliceAssistenceLetterService.Create(demolitionpoliceassistenceletter);
                }
                else
                {
                    demolitionpoliceassistenceletter.ModifiedBy = SiteContext.UserId;
                    result = await _demolitionPoliceAssistenceLetterService.Update(demolitionpoliceData.Id, demolitionpoliceassistenceletter);
                }
              

                if(result)
                {
                    var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResultButOnAneexureId(demolitionpoliceassistenceletter.FixingDemolitionId);
                   // demolitionpoliceassistenceletter.FilePath = Data.FilePath;
                    string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "DemolitionLetter.html");
                    var Body = PopulateBody(Data, path);
                    if (demolitionpoliceassistenceletter.GenerateUpload == 0)
                    {
                        ViewBag.IsVisible = true;
                        ViewBag.DataLetter = Body;
                        ViewBag.Message = Alert.Show("Generate Letter Successfully", "", AlertType.Success);
                    }
                    else
                    {
                        ViewBag.IsVisible = false;
                        ViewBag.Message = Alert.Show("Record Submitted and  Uploaded Successfully", "", AlertType.Success);
                    }
                    return View(demolitionpoliceassistenceletter);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Success);
                    return View("Index");
                }
                
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demolitionpoliceassistenceletter);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        {
            targetPathDocument = _configuration.GetSection("FilePaths:DemolitionPoliceAssistenceFiles:LetterFilePath").Value.ToString();
            if (ModelState.IsValid)
            {
               
                //if(demolitionpoliceassistenceletter.GenerateUpload == 0 )
                //{
                //    if(demolitionpoliceassistenceletter.MeetingDate == null || demolitionpoliceassistenceletter.MeetingTime == null)
                //    {
                //        ViewBag.Message = Alert.Show("Meeting Date and Time is Mandatory", "", AlertType.Warning);
                //        return View(demolitionpoliceassistenceletter);
                //    }
                //}
                //else if (demolitionpoliceassistenceletter.GenerateUpload == 1)
                //{
                //    if (demolitionpoliceassistenceletter.Document == null )
                //    {
                //        ViewBag.Message = Alert.Show("Document is Mandatory", "", AlertType.Warning);
                //        return View(demolitionpoliceassistenceletter);
                //    }
                //}
                string LetterFileName = "";
                string LetterfilePath = "";
                //if (demolitionpoliceassistenceletter.Document != null)
                //{
                //    if (!Directory.Exists(targetPathDocument))
                //    {
                //        DirectoryInfo di = Directory.CreateDirectory(targetPathDocument);// Try to create the directory.
                //    }
                //    LetterFileName = Guid.NewGuid().ToString() + "_" + demolitionpoliceassistenceletter.Document.FileName;
                //    LetterfilePath = Path.Combine(targetPathDocument, LetterFileName);
                //    using (var stream = new FileStream(LetterfilePath, FileMode.Create))
                //    {
                //        demolitionpoliceassistenceletter.Document.CopyTo(stream);
                //    }
                //    demolitionpoliceassistenceletter.FilePath = LetterFileName;
                //}

                demolitionpoliceassistenceletter.ModifiedBy = SiteContext.UserId;
                var result = await _demolitionPoliceAssistenceLetterService.Update(id, demolitionpoliceassistenceletter);

                if (result)
                {
                    var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(id);
                  //  demolitionpoliceassistenceletter.FilePath = Data.FilePath;
                    string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "DemolitionLetter.html");
                    var Body = PopulateBody(Data, path);
                    ViewBag.IsVisible = true;
                    ViewBag.DataLetter = Body;
                    ViewBag.Message = Alert.Show("Generate Successfully", "", AlertType.Success);
                    return View(demolitionpoliceassistenceletter);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Success);
                    return View("Index");
                }

            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demolitionpoliceassistenceletter);
            }

        }
        private string PopulateBody(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter, string Path)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Path))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{OfficeName}", demolitionpoliceassistenceletter.OfficeName);
            body = body.Replace("{OfficeDepartment}", demolitionpoliceassistenceletter.OfficeDepartment);
            body = body.Replace("{OfficeZone}", demolitionpoliceassistenceletter.OfficeZone);
            body = body.Replace("{OfficeAddress}", demolitionpoliceassistenceletter.OfficeAddress);
            body = body.Replace("{FileNo}", demolitionpoliceassistenceletter.FileNo);
            body = body.Replace("{LetterDate}", Convert.ToDateTime((demolitionpoliceassistenceletter.LetterDate)).ToString("dd-MMM-yyyy"));
            body = body.Replace("{DyCommOffcAddress}", demolitionpoliceassistenceletter.DyCommOffcAddress);
            body = body.Replace("{KhasraNo}", demolitionpoliceassistenceletter.KhasraNo);
            body = body.Replace("{VillageName}", demolitionpoliceassistenceletter.VillageName);
            body = body.Replace("{KhasraAddress}", demolitionpoliceassistenceletter.KhasraAddress);
            body = body.Replace("{PoliceStationName}", demolitionpoliceassistenceletter.PoliceStationName);
            body = body.Replace("{OperationDate}", Convert.ToDateTime((demolitionpoliceassistenceletter.OperationDate)).ToString("dd-MMM-yyyy"));
            body = body.Replace("{OperationDay}", demolitionpoliceassistenceletter.MeetingTime);
            body = body.Replace("{RevenueOfficerZone}", demolitionpoliceassistenceletter.RevenueOfficerZone);
            body = body.Replace("{RevenueOfficerWing}", demolitionpoliceassistenceletter.RevenueOfficerWing);
            body = body.Replace("{RevenueOfficerBranch}", demolitionpoliceassistenceletter.RevenueOfficerBranch);
            body = body.Replace("{MeetingDate}", Convert.ToDateTime((demolitionpoliceassistenceletter.MeetingDate)).ToString("dd-MMM-yyyy"));
            body = body.Replace("{MeetingTime}", demolitionpoliceassistenceletter.MeetingTime);
            body = body.Replace("{ChiefEngineerAddress}", demolitionpoliceassistenceletter.ChiefEngineerAddress);
            body = body.Replace("{MeetingTime}", demolitionpoliceassistenceletter.MeetingTime);
            body = body.Replace("{SHOAddress}", demolitionpoliceassistenceletter.Shoaddress);
            body = body.Replace("{GeneralConditions}", demolitionpoliceassistenceletter.GeneralConditions==null?"": "4. General Conditions:<br/>"+ demolitionpoliceassistenceletter.GeneralConditions);



            return body;
        }

        //public async Task<FileResult> ViewLetter(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(Id);
        //   // string path = targetPathDocument + Data.FilePath;
        //    byte[] FileBytes = System.IO.File.ReadAllBytes(path);
        //    return File(FileBytes, file.GetContentType(path));
        //}

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
            encroachmentRegisterations.PropertyInventoryKhasraList = 
                await _encroachmentRegisterationService.GetAllKhasraListFromPropertyInventory(encroachmentRegisterations.ZoneId, encroachmentRegisterations.DepartmentId);
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

        public async Task<IActionResult> GetAllDemolitionPoliceAssistenceLetter()   //
        {
            var result = await _demolitionPoliceAssistenceLetterService.GetAllDemolitionPoliceAssistenceLetterList((int)ApprovalActionStatus.Approved);
            List<DemolitionPoliceLetterDto> data = new List<DemolitionPoliceLetterDto>();
            if (result != null)
            {

                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemolitionPoliceLetterDto()
                    {
                        InspectionDate = result[i].Encroachment.EncrochmentDate == null?"_":Convert.ToDateTime(result[i].Encroachment.EncrochmentDate).ToString("dd-MM-yyyy"),
                        Department = result[i].Encroachment.Department == null ?"_":result[i].Encroachment.Department.Name,
                        Zone = result[i].Encroachment.Zone != null ? result[i].Encroachment.Zone.Name : "_",
                        KhasraNo_PlotNo = result[i].Encroachment.KhasraNoNavigation.LocalityId == null ?result[i].Encroachment.KhasraNoNavigation.PlotNo : result[i].Encroachment.KhasraNoNavigation.KhasraNo,
                        Status = result[i].ApprovedStatus == 0 ? "Pending" : "Approved" ,
                        LetterStatus = result[i].Demolitionpoliceassistenceletter.Count != 0 ? "Generated" : "Not Generated"

                    });

                }
            }
            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DemolitionReport2Data.xlsx");
        }
    }
}
