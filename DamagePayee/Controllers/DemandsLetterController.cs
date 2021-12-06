using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;
using DamagePayee.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using System.Collections.Generic;
namespace DamagePayee.Controllers
{
    public class DemandsLetterController : Controller
    {
        private readonly IDemandLetterService _demandLetterService;
        public DemandsLetterController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        async Task BindDropDownView(Demandletters demandletters)
        {
            demandletters.LocalityList = await _demandLetterService.GetLocalityList();
           
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Demandletters model = new Demandletters();
            await BindDropDownView(model);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Demandletters demandletter)
        {
            await BindDropDownView(demandletter);
            try
            {
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                demandletter.DemandNo = "DMN" + finalString;

                if (ModelState.IsValid)
                {
                    var result = await _demandLetterService.Create(demandletter);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                       
                        return View("_List", demandletter);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demandletter);
                    }
                }
                else
                {
                    return View(demandletter);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demandletter);
            }
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandletterSearchDto model)
        {
            try
            {
                var result = await _demandLetterService.GetPagedDemandletter(model);

                return PartialView("_List1", result);
            } catch(Exception ex)
            {
                return PartialView(ex);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _demandLetterService.FetchSingleResult(id);
            Data.LocalityList = await _demandLetterService.GetLocalityList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Demandletters demandletter)
        {
            await BindDropDownView(demandletter);
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _demandLetterService.Update(id, demandletter);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View("_List", demandletter);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demandletter);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(demandletter);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _demandLetterService.FetchSingleResult(id);
            Data.LocalityList = await _demandLetterService.GetLocalityList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



        public async Task<IActionResult> DemandsLetterList()
        {
            var result = await _demandLetterService.GetAllDemandletter();
            List<DemandLetterListDto> data = new List<DemandLetterListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemandLetterListDto()
                    {
                        Id = result[i].Id,
                        Locality = result[i].Locality == null ? "" : result[i].Locality.Name.ToString(),
                        DemandNo = result[i].DemandNo,
                        DueAmount = result[i].DepositDue,
                        ReliefAmount = result[i].ReliefAmount.ToString(),
                        FileNo = result[i].FileNo,
                        Name = result[i].Name,
                        FatherName = result[i].FatherName,
                        Address = result[i].Address,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }





    }
}
