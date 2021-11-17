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

using Microsoft.Extensions.Configuration;

using System.IO;
using Unidecode.NET;
using System.Net;
using DamagePayee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;

using Service.IApplicationService;
using System.Text;
using System.Collections.Generic;

namespace DamagePayee.Controllers
{
    public class NoticeToDamagePayeeController : Controller
    {
        private readonly INoticeToDamagePayeeService _noticeToDamagePayeeService;

        public NoticeToDamagePayeeController(INoticeToDamagePayeeService noticeToDamagePayeeService)
        {
            _noticeToDamagePayeeService = noticeToDamagePayeeService;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _noticeToDamagePayeeService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Noticetodamagepayee noticetodamagepayee)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _noticeToDamagePayeeService.Update(id, noticetodamagepayee);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View("_List", noticetodamagepayee);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(noticetodamagepayee);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return View(noticetodamagepayee);
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Noticetodamagepayee noticetodamagepayee)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _noticeToDamagePayeeService.Create(noticetodamagepayee);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                       
                        return View("_List", noticetodamagepayee);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(noticetodamagepayee);
                    }
                }
                else
                {
                    return View(noticetodamagepayee);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(noticetodamagepayee);
            }
        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NoticetodamagepayeeSearchDto model)
        {
            var result = await _noticeToDamagePayeeService.GetPagedNoticetodamagepayee(model);

            return PartialView("_List1", result);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _noticeToDamagePayeeService.FetchSingleResult(id);
            Data.RebatePercentage = _noticeToDamagePayeeService.GetRebateCharges();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        public async Task<IActionResult> NoticeToDamagePayeeList()
        {
            var result = await _noticeToDamagePayeeService.GetAllNoticetoDamagePayee();
           List<NoticeToDamagePayeeListDto> data = new List<NoticeToDamagePayeeListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NoticeToDamagePayeeListDto()
                    {
                        Id = result[i].Id,
                        FileNo = result[i].FileNo,
                        Name = result[i].Name,
                        Address = result[i].Address,
                        PropertyDetails = result[i].PropertyDetails,
                        Area = result[i].Area,
                        InterestPercentage = result[i].InterestPercentage,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                                     }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }







    }
}
