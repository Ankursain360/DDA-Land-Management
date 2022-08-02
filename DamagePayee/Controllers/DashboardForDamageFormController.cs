using DamagePayee.Helper;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Helper;

namespace DamagePayee.Controllers
{
    public class DashboardForDamageFormController : Controller
    {
        private readonly INewDamageSelfAssessmentService _newDamageSelf;

        public DashboardForDamageFormController(INewDamageSelfAssessmentService newDamageSelf)
        {
            _newDamageSelf = newDamageSelf;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DamagePayeeDashboardSearchDto report)
        {
            var result = await _newDamageSelf.GetDamagePayee(report);
            if (result != null)
            {
                return PartialView("_GetDetails", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }

        public async Task<IActionResult> DamagePayeeReport(DamagePayeeDashboardSearchDto Model)
        {
            var result = await _newDamageSelf.GetDamagePayee(Model);
            List<NewDamagePayeeDashboardListDto> data = new List<NewDamagePayeeDashboardListDto>(); 
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewDamagePayeeDashboardListDto()
                    {
                        villageName = result[i].VillageName,
                        totalapplicationReceived = result[i].TotalapplicationReceived,
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

    }
}
