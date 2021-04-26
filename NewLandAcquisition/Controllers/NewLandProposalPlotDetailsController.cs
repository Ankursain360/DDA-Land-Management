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
using Dto.Master;

namespace NewLandAcquisition.Controllers
{
  
        public class NewLandProposalPlotDetailsController : BaseController
        {
            private readonly INewLandProposalPlotDetailsService _newLandProposalPlotDetailsService; 

            public NewLandProposalPlotDetailsController(INewLandProposalPlotDetailsService newLandProposalPlotDetailsService)
            {
            _newLandProposalPlotDetailsService = newLandProposalPlotDetailsService;
            }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
            {
                return View();
            }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewLandProposalplotdetailSearchDto model)
        {
            var result = await _newLandProposalPlotDetailsService.GetPagedProposalplotdetails(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandacquistionproposalplotdetails newlandacquistionproposalplotdetails = new Newlandacquistionproposalplotdetails();
            
            newlandacquistionproposalplotdetails.ProposaldetailsList = await _newLandProposalPlotDetailsService.GetAllProposaldetails();
            newlandacquistionproposalplotdetails.AcquiredlandvillageList = await _newLandProposalPlotDetailsService.GetAllVillage();
            newlandacquistionproposalplotdetails.KhasraList = await _newLandProposalPlotDetailsService.GetAllKhasra(newlandacquistionproposalplotdetails.AcquiredlandvillageId);


            return View(newlandacquistionproposalplotdetails);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandacquistionproposalplotdetails newlandacquistionproposalplotdetails)
        {
            try
            {
                newlandacquistionproposalplotdetails.ProposaldetailsList = await _newLandProposalPlotDetailsService.GetAllProposaldetails();
                newlandacquistionproposalplotdetails.AcquiredlandvillageList = await _newLandProposalPlotDetailsService.GetAllVillage();
                newlandacquistionproposalplotdetails.KhasraList = await _newLandProposalPlotDetailsService.GetAllKhasra(newlandacquistionproposalplotdetails.AcquiredlandvillageId);


                if (ModelState.IsValid)
                {


                    var result = await _newLandProposalPlotDetailsService.Create(newlandacquistionproposalplotdetails);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newLandProposalPlotDetailsService.GetAllProposalplotdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandacquistionproposalplotdetails);

                    }
                }
                else
                {
                    return View(newlandacquistionproposalplotdetails);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandacquistionproposalplotdetails);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _newLandProposalPlotDetailsService.FetchSingleResult(id);
            Data.ProposaldetailsList = await _newLandProposalPlotDetailsService.GetAllProposaldetails();
           
            Data.AcquiredlandvillageList = await _newLandProposalPlotDetailsService.GetAllVillage();
            Data.KhasraList = await _newLandProposalPlotDetailsService.GetAllKhasra(Data.AcquiredlandvillageId);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandacquistionproposalplotdetails newlandacquistionproposalplotdetails)
        {

            newlandacquistionproposalplotdetails.ProposaldetailsList = await _newLandProposalPlotDetailsService.GetAllProposaldetails();
            newlandacquistionproposalplotdetails.AcquiredlandvillageList = await _newLandProposalPlotDetailsService.GetAllVillage();
            newlandacquistionproposalplotdetails.KhasraList = await _newLandProposalPlotDetailsService.GetAllKhasra(newlandacquistionproposalplotdetails.AcquiredlandvillageId);

            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _newLandProposalPlotDetailsService.Update(id, newlandacquistionproposalplotdetails);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newLandProposalPlotDetailsService.GetAllProposalplotdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandacquistionproposalplotdetails);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(newlandacquistionproposalplotdetails);
        }



        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _newLandProposalPlotDetailsService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _newLandProposalPlotDetailsService.GetAllProposalplotdetails();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newLandProposalPlotDetailsService.FetchSingleResult(id);
           
            Data.ProposaldetailsList = await _newLandProposalPlotDetailsService.GetAllProposaldetails();

            Data.AcquiredlandvillageList = await _newLandProposalPlotDetailsService.GetAllVillage();
            Data.KhasraList = await _newLandProposalPlotDetailsService.GetAllKhasra(Data.AcquiredlandvillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Newlandacquistionproposalplotdetails> result = await _newLandProposalPlotDetailsService.GetAllProposalplotdetails();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Proposalplotdetails.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _newLandProposalPlotDetailsService.GetAllKhasra(Convert.ToInt32(villageId)));
        }


        [HttpGet]
        public async Task<JsonResult> GetKhasraAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _newLandProposalPlotDetailsService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }


        public async Task<IActionResult> NewLandProposalPlotDetailsList()
        {
            var result = await _newLandProposalPlotDetailsService.GetAllProposalplotdetails();
            List<NewLandProposalPlotDetailsListDto> data = new List<NewLandProposalPlotDetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandProposalPlotDetailsListDto()
                    {
                        Id = result[i].Id,
                        ProposalName = result[i].Proposaldetails == null ? "" : result[i].Proposaldetails.Name.ToString(),
                        VillageName= result[i].Acquiredlandvillage == null ? "" : result[i].Acquiredlandvillage.Name.ToString(),
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name.ToString(),
                        Area = result[i].Bigha.ToString() + '-' + result[i].Biswa.ToString() + '-' + result[i].Biswanshi.ToString(),
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


    }
}
