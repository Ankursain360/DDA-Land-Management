using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewLandAcquisition.Filters;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;
using System.Data;
using Newtonsoft.Json;
using Dto.Master;

namespace NewLandAcquisition.Controllers
{
    
        public class NewLandProposalDetailsController : BaseController
        {
            private readonly INewlandProposaldetailsService _newlandProposaldetailsService;

            public NewLandProposalDetailsController(INewlandProposaldetailsService newlandProposaldetailsService)
            {
            _newlandProposaldetailsService = newlandProposaldetailsService;
            }
        [AuthorizeContext(ViewAction.View)]
        public  IActionResult Index()
            {
               
                return View();
            }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandacquistionproposaldetailsSearchDto model)
        {
            var result = await _newlandProposaldetailsService.GetPagedProposaldetails(model);
            return PartialView("_List", result);
        }

        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandacquistionproposaldetails newlandacquistionproposaldetails = new Newlandacquistionproposaldetails();
            newlandacquistionproposaldetails.IsActive = 1;
            newlandacquistionproposaldetails.SchemeList = await _newlandProposaldetailsService.GetAllScheme();
            return View(newlandacquistionproposaldetails);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandacquistionproposaldetails newlandacquistionproposaldetails)
        {
            try
            {
                newlandacquistionproposaldetails.SchemeList = await _newlandProposaldetailsService.GetAllScheme();
                if (ModelState.IsValid)
                {


                    var result = await _newlandProposaldetailsService.Create(newlandacquistionproposaldetails);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandProposaldetailsService.GetAllProposaldetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandacquistionproposaldetails);

                    }
                }
                else
                {
                    return View(newlandacquistionproposaldetails);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandacquistionproposaldetails);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _newlandProposaldetailsService.FetchSingleResult(id);
            Data.SchemeList = await _newlandProposaldetailsService.GetAllScheme();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandacquistionproposaldetails newlandacquistionproposaldetails)
        {
            newlandacquistionproposaldetails.SchemeList = await _newlandProposaldetailsService.GetAllScheme();
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _newlandProposaldetailsService.Update(id, newlandacquistionproposaldetails);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newlandProposaldetailsService.GetAllProposaldetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandacquistionproposaldetails);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(newlandacquistionproposaldetails);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _newlandProposaldetailsService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Page: {Name} already exist");
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _newlandProposaldetailsService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _newlandProposaldetailsService.GetAllProposaldetails();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandProposaldetailsService.FetchSingleResult(id);
            Data.SchemeList = await _newlandProposaldetailsService.GetAllScheme();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //[AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Newlandacquistionproposaldetails> result = await _newlandProposaldetailsService.GetAllProposaldetails();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Proposaldetails.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

        public async Task<IActionResult> NewLandProposalList()
        {
            var result = await _newlandProposaldetailsService.GetAllProposaldetails();
            List<NewLandProposalListDto> data = new List<NewLandProposalListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandProposalListDto()
                    {
                        Id = result[i].Id,
                        SchemeName = result[i].Scheme == null ? "" : result[i].Scheme.Name.ToString(),
                        ProposalName = result[i].Name,
                        RequiredAgency = result[i].RequiredAgency,
                        ProposalNo = result[i].ProposalFileNo,
                        ProposalDate = result[i].ProposalDate.ToString(),
                        Area = result[i].Bigha.ToString()+'-'+result[i].Biswa.ToString() + '-'+ result[i].Biswanshi.ToString(),
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
