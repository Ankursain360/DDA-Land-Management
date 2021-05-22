using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class DamageRateListController : BaseController
    {

        private readonly IDamageRateListService _damageRateListService;

        public DamageRateListController(IDamageRateListService damageRateListService)
        {
            _damageRateListService = damageRateListService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Create()
        {
            DamageRateListCreateDto damageRateListCreatedto = new DamageRateListCreateDto();
            ViewBag.PropertyTypeList = await _damageRateListService.GetPropertyTypes();
            ViewBag.LocalityList = await _damageRateListService.GetLocalities();
            return View(damageRateListCreatedto);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamageRateListSearchDto model)
        {
            List<DamageRateListDataDto> damageRateListDataDtos = new List<DamageRateListDataDto>();
            var result = await _damageRateListService.GetSearchResultPagedData(model, damageRateListDataDtos);
            return PartialView("_List", result);
        }

        [HttpGet]
        public async Task<JsonResult> GetDateRangeList(int? Id)
        {
            var Idvalue = Id ?? 0;
            var propertytype = await _damageRateListService.FetchSinglePropertyType(Idvalue);
            if (propertytype != null)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    var data = await _damageRateListService.GetDateRangeDropdownListResidential();
                    return Json(data);
                }
                else
                {
                    var data = await _damageRateListService.GetDateRangeDropdownListCommercial();
                    return Json(data);
                }
            }

            return Json(null);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create([FromBody] DamageRateListCreateDto damageRateListCreateDto)
        {
            List<string> JsonMsg = new List<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    if (damageRateListCreateDto == null)
                    {
                        JsonMsg.Add("false");
                        JsonMsg.Add("Please Enter Correct data");
                        return Json(JsonMsg);
                    }
                    if (damageRateListCreateDto.Id == 0)
                    {
                        damageRateListCreateDto.CreatedBy = SiteContext.UserId;
                        var result = await _damageRateListService.Create(damageRateListCreateDto);

                        if (result == true)
                        {
                            JsonMsg.Add("true");
                            JsonMsg.Add("Record added successfully.");
                            return Json(JsonMsg);
                        }
                        else
                        {
                            JsonMsg.Add("false");
                            JsonMsg.Add("Unable to process the request.");
                            return Json(JsonMsg);
                        }
                    }
                    else
                    {
                        damageRateListCreateDto.ModifiedBy = SiteContext.UserId;
                        var result = await _damageRateListService.Update(damageRateListCreateDto);

                        if (result == true)
                        {
                            JsonMsg.Add("/WorkFlowTemplate/Index");
                            JsonMsg.Add("Record Updated successfully.");
                            return Json(JsonMsg);
                        }
                        else
                        {
                            JsonMsg.Add("false");
                            JsonMsg.Add("Unable to process the request.");
                            return Json(JsonMsg);
                        }
                    }
                }
                else
                {
                    JsonMsg.Add("false");
                    JsonMsg.Add("Please enter a correct number in Rate Field, format xxxx.xxx");
                    return Json(JsonMsg);
                }
            }
            catch(Exception ex)
            {
                JsonMsg.Add("false");
                JsonMsg.Add("Unable to process the request.");
                return Json(JsonMsg);
            }
           
        }

        [HttpGet]
        public async Task<JsonResult> EditDetails(int Id, int EncroachmentTypeId, int LocalityId, int PropertypeId)
        {
            return Json(await _damageRateListService.FetchSingleResult(Id, EncroachmentTypeId, LocalityId, PropertypeId));
        }


    }
}