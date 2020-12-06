using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Utility.Helper;
namespace EncroachmentDemolition.Controllers
{
    public class AnnexureAController : BaseController
    {
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        public readonly IAnnexureAService _annexureAService;
        public IConfiguration _configuration;
        string targetPhotoPathLayout = string.Empty;
        string targetReportfilePathLayout = string.Empty;

        public AnnexureAController(IEncroachmentRegisterationService encroachmentRegisterationService, IAnnexureAService annexureAService, IConfiguration configuration)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _annexureAService = annexureAService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AnnexureASearchDto model)
        {
            var result = await _annexureAService.GetPagedDetails(model);
            return PartialView("_List", result);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(int id)
        {
            Fixingdemolition Model = new Fixingdemolition();
            var Data = await _encroachmentRegisterationService.FetchSingleResult(id);
            Data.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            Data.ZoneList = await _encroachmentRegisterationService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(Data.DivisionId);
            Data.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(Data.LocalityId);
            Model.Demolitionchecklist = await _annexureAService.GetDemolitionchecklist();
            Model.Demolitionprogram = await _annexureAService.GetDemolitionprogram();
            Model.Demolitiondocument = await _annexureAService.GetDemolitiondocument();
            Model.Encroachment = Data;
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, Fixingdemolition fixingdemolition)
        {
            var Data = await _encroachmentRegisterationService.FetchSingleResult(id);
            fixingdemolition.Demolitionprogram = await _annexureAService.GetDemolitionprogram();

            fixingdemolition.Encroachment = Data;
            fixingdemolition.Id = 0;
            if (fixingdemolition.EncroachmentId == 0)
            {
                return NotFound();
            }
            fixingdemolition.EncroachmentId = fixingdemolition.Encroachment.Id;
            var result = await _annexureAService.Create(fixingdemolition);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                var result1 = await _encroachmentRegisterationService.GetAllEncroachmentRegisteration();
                //  return View("Index", result1);
            }
            if (fixingdemolition.DemolitionProgramId != null)
            {
                List<Fixingprogram> fixingprogram = new List<Fixingprogram>();
                for (int i = 0; i < fixingdemolition.DemolitionProgramId.Count(); i++)
                {
                    fixingprogram.Add(new Fixingprogram
                    {

                        DemolitionProgramId = (int)fixingdemolition.DemolitionProgramId[i],
                        ItemsDetails = fixingdemolition.ItemsDetails[i],
                        FixingdemolitionId = fixingdemolition.Id
                    });
                }
                foreach (var item in fixingprogram)
                {
                    result = await _annexureAService.SaveFixingprogram(item);
                }
            }
            if (fixingdemolition.DemolitionChecklistId != null)
            {
                List<Fixingchecklist> fixingchecklist = new List<Fixingchecklist>();
                for (int i = 0; i < fixingdemolition.DemolitionChecklistId.Count(); i++)
                {
                    fixingchecklist.Add(new Fixingchecklist
                    {

                        DemolitionChecklistId = (int)fixingdemolition.DemolitionChecklistId[i],
                        ChecklistDetails = fixingdemolition.ChecklistDetails[i],
                        FixingdemolitionId = fixingdemolition.Id
                    });
                }
                foreach (var item in fixingchecklist)
                {
                    result = await _annexureAService.Savefixingchecklist(item);
                }
            }
            //if (fixingdemolition.DemolitionDocumentId != null)
            //{
            //    List<Fixingdocument> fixingdocument = new List<Fixingdocument>();
            //    for (int i = 0; i < fixingdemolition.DemolitionDocumentId.Count(); i++)
            //    {
            //        fixingdocument.Add(new Fixingdocument
            //        {

            //            DemolitionDocumentId = (int)fixingdemolition.DemolitionDocumentId[i],
            //            DocumentDetails = fixingdemolition.DocumentDetails[i],
            //            FixingdemolitionId = fixingdemolition.Id
            //        });
            //    }
            //    foreach (var item in fixingdocument)
            //    {
            //        result = await _annexureAService.SaveFixingdocument(item);
            //    }
            //}
            string DocumentFilePath = _configuration.GetSection("FilePaths:FixingDemolitionFiles:DocumentFilePath").Value.ToString();
            // targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            FileHelper fileHelper = new FileHelper();
            if (fixingdemolition.DocumentDetails != null && fixingdemolition.DocumentDetails.Count > 0)
            {
                List<Fixingdocument> fixingdocument = new List<Fixingdocument>();
                for (int i = 0; i < fixingdemolition.DocumentDetails.Count; i++)
                {
                    string FilePath = fileHelper.SaveFile(DocumentFilePath, fixingdemolition.DocumentDetails[i]);
                    fixingdocument.Add(new Fixingdocument
                    {
                        DemolitionDocumentId = (int)fixingdemolition.DemolitionDocumentId[i],
                        DocumentDetails = FilePath,
                        FixingdemolitionId = fixingdemolition.Id
                    });
                }
                foreach (var item in fixingdocument)
                {
                    result = await _annexureAService.SaveFixingdocument(item);
                }
            }
            if (result)
            {
                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                var result1 = await _encroachmentRegisterationService.GetAllEncroachmentRegisteration();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(fixingdemolition);
            }
        }
        public async Task<IActionResult> View(int id)
        {
            List<Fixingdemolition> list = await _annexureAService.GetFixingdemolition(id);
            return View(list);
            //return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(Fixingdemolition fixingdemolition)
        //{
        //    fixingdemolition.Demolitionchecklist = await _annexureAService.GetDemolitionchecklist();
        //      if (ModelState.IsValid)
        //    {

        //        var result = await _annexureAService.Create(fixingdemolition);



        //            ///for after file:




        //            //for before file:


        //            //if (demolitionstructuredetails.NameOfStructure != null && demolitionstructuredetails.NoOfStructrure != null && demolitionstructuredetails.NameOfStructure.Count > 0 && demolitionstructuredetails.NoOfStructrure.Count > 0)

        //            //if (demolitionstructuredetails.StructrureId != null && demolitionstructuredetails.NoOfStructrure != null && demolitionstructuredetails.NameOfStructure.Count > 0 && demolitionstructuredetails.NoOfStructrure.Count > 0)
        //            if (demolitionstructuredetails.StructrureId != null)
        //            {
        //                List<Demolitionstructure> demolitionstructure = new List<Demolitionstructure>();
        //                for (int i = 0; i < demolitionstructuredetails.StructrureId.Count(); i++)
        //                {
        //                    demolitionstructure.Add(new Demolitionstructure
        //                    {

        //                        StructureId = (int)demolitionstructuredetails.StructrureId[i],
        //                        NoOfStructrure = demolitionstructuredetails.NoOfStructrure[i],
        //                        DemolitionStructureDetailsId = demolitionstructuredetails.Id
        //                    });
        //                }
        //                foreach (var item in demolitionstructure)
        //                {
        //                    result = await _demolitionstructuredetailsService.SaveDemolitionstructure(item);
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

        //    }







        //public async Task<IActionResult> Create()
        //{


        //    demolitionstructuredetails.Structure = await _demolitionstructuredetailsService.GetStructure();



        //    //     var list = await _annexureAService.GetDemolitionchecklist();

        //    // var list1 = await _annexureAService.GetDemolitiondocument();
        //    return View();
        //   // return View(list1);
        //}

    }
}