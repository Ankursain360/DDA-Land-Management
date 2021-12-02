using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.IO;
using Unidecode.NET;
using System.Net;
using DamagePayee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Dto.Master;
using Core.Enum;
using DamagePayee.Filters;
using Service.IApplicationService;
using System.Text;
using Microsoft.AspNetCore.Http;
using Core.Enum;
using System.Net.Http;
using System.Text.Json;

namespace DamagePayee.Controllers
{
    public class VerifyPaymentStatusController : Controller
    {
        public IConfiguration _configuration;
        private readonly IPaymentverificationService _paymentverificationService;
        public VerifyPaymentStatusController(IConfiguration configuration, IPaymentverificationService paymentverificationService)
        {
            _configuration = configuration;
            _paymentverificationService = paymentverificationService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> GetVerifyPayment([FromBody] VerifyPaymentStatusForFileNoDto model)
        {
            using (var httpClient = new HttpClient())
            {
                var fileno = model.fileNo;
                List<VerifyPaymentApiStatusDto> damagecalculation = new List<VerifyPaymentApiStatusDto>();
                string url = _configuration.GetSection("VerifyPaymentStatusApi").Value + fileno;
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var data2 = JsonSerializer.Deserialize<ApiResponseVerifyPaymentApiStatus>(apiResponse);

                    return PartialView("_List", data2);
                }
            }
        }


        //public async Task<PartialViewResult> DamageCalculate([FromBody] DamageCalculationDto dto)

    }
}
