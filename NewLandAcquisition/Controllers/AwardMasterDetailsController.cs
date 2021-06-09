using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using NewLandAcquisition.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;
using Dto.Master;
using Microsoft.Extensions.Configuration;

namespace NewLandAcquisition.Controllers
{
    public class AwardMasterDetailsController : BaseController
    {
        public readonly INewlandawardmasterdetailService _newlandawardmasterdetailService;
        public IConfiguration _configuration;
        string DocumentFilePath = "";
        public AwardMasterDetailsController(INewlandawardmasterdetailService newlandawardmasterdetailsService, IConfiguration configuration)
        {
            _newlandawardmasterdetailService = newlandawardmasterdetailsService;
            _configuration = configuration;
            DocumentFilePath = _configuration.GetSection("FilePaths:AwardMaster:DocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }


        
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandawardmasterSearchDto model)
        {
            var result = await _newlandawardmasterdetailService.GetPagedawardmasterdetails(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandawardmasterdetail model = new Newlandawardmasterdetail();
            model.IsActive = 1;
            model.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
            model.section17List = await _newlandawardmasterdetailService.Getundersection17();
            model.section4List = await _newlandawardmasterdetailService.Getundersection4();
            model.section6List = await _newlandawardmasterdetailService.Getundersection6();
            model.purposalList = await _newlandawardmasterdetailService.GetPurposal();
            return View(model);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandawardmasterdetail awardmasterdetail)
        {
            try
            {
                awardmasterdetail.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
                awardmasterdetail.purposalList = await _newlandawardmasterdetailService.GetPurposal();
                awardmasterdetail.section6List = await _newlandawardmasterdetailService.Getundersection6();
                awardmasterdetail.section4List = await _newlandawardmasterdetailService.Getundersection4();
                awardmasterdetail.section17List = await _newlandawardmasterdetailService.Getundersection17();
                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    awardmasterdetail.DocumentName = awardmasterdetail.DocumentIFormFile == null ? awardmasterdetail.DocumentName : fileHelper.SaveFile1(DocumentFilePath, awardmasterdetail.DocumentIFormFile);
                    awardmasterdetail.CreatedBy = SiteContext.UserId;
                    var result = await _newlandawardmasterdetailService.Create(awardmasterdetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandawardmasterdetailService.Getawardmasterdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(awardmasterdetail);
                    }
                }
                else
                {
                    return View(awardmasterdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(awardmasterdetail);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandawardmasterdetailService.FetchSingleResult(id);
            Data.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
            Data.purposalList = await _newlandawardmasterdetailService.GetPurposal();
            Data.section6List = await _newlandawardmasterdetailService.Getundersection6();
            Data.section4List = await _newlandawardmasterdetailService.Getundersection4();
            Data.section17List = await _newlandawardmasterdetailService.Getundersection17();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandawardmasterdetailService.FetchSingleResult(id);
            Data.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
            Data.purposalList = await _newlandawardmasterdetailService.GetPurposal();
            Data.section6List = await _newlandawardmasterdetailService.Getundersection6();
            Data.section4List = await _newlandawardmasterdetailService.Getundersection4();
            Data.section17List = await _newlandawardmasterdetailService.Getundersection17();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandawardmasterdetail newlandawardmasterdetail)
        {
            newlandawardmasterdetail.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
            newlandawardmasterdetail.purposalList = await _newlandawardmasterdetailService.GetPurposal();
            newlandawardmasterdetail.section6List = await _newlandawardmasterdetailService.Getundersection6();
            newlandawardmasterdetail.section4List = await _newlandawardmasterdetailService.Getundersection4();
            newlandawardmasterdetail.section17List = await _newlandawardmasterdetailService.Getundersection17();
            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                newlandawardmasterdetail.DocumentName = newlandawardmasterdetail.DocumentIFormFile == null ? newlandawardmasterdetail.DocumentName : fileHelper.SaveFile1(DocumentFilePath, newlandawardmasterdetail.DocumentIFormFile);
                newlandawardmasterdetail.ModifiedBy = SiteContext.UserId;

                var result = await _newlandawardmasterdetailService.Update(id, newlandawardmasterdetail);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _newlandawardmasterdetailService.Getawardmasterdetails();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandawardmasterdetail);
                }
            }
            else
            {
                return View(newlandawardmasterdetail);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality 
        {
            var result = await _newlandawardmasterdetailService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _newlandawardmasterdetailService.Getawardmasterdetails();
            return View("Index", list);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string AwardNumber)
        {
            var result = await _newlandawardmasterdetailService.CheckUniqueName(Id, AwardNumber);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Award Number : {AwardNumber} already exist");
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistCode(int Id, string name)
        {
            var result = await _newlandawardmasterdetailService.CheckUniqueName(Id, name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Award Number : {name} already exist");
            }
        }


     

        public async Task<IActionResult> AwardMasterDetailsList()
        {
            var result = await _newlandawardmasterdetailService.Getawardmasterdetails();
            List<NewLandAwardMasterDetailsListDto> data = new List<NewLandAwardMasterDetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandAwardMasterDetailsListDto()
                    {
                        Id = result[i].Id,
                        AwardNo=result[i].AwardNumber,
                        VillageName= result[i].Newlandvillage == null ? "" : result[i].Newlandvillage.Name.ToString(),
                        AwardDate =result[i].AwardDate.ToString(),
                        ProposalName= result[i].Proposal == null ? "" : result[i].Proposal.Name.ToString(),
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<IActionResult> ViewUploadedDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Newlandawardmasterdetail Data = await _newlandawardmasterdetailService.FetchSingleResult(Id);
            string filename = DocumentFilePath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
    }
}
