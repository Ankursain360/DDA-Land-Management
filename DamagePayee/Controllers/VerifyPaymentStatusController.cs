using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dto.Master;
using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Dto.Search;
using DamagePayee.Filters;
using Core.Enum;

namespace DamagePayee.Controllers
{
    public class VerifyPaymentStatusController : Controller
    {
        public IConfiguration _configuration;

        public VerifyPaymentStatusController(IConfiguration configuration)
        {
            _configuration = configuration;
                
        }

    
        public async Task<IActionResult> Index()
        
        {
            string FileNo = "D/QS/77/2524";
            List<VerifyPaymentApiStatusDto> VerifyPaymentStatusList = new List<VerifyPaymentApiStatusDto>();

            using (var httpClient = new HttpClient())
            {
                string url = _configuration.GetSection("VerifyPaymentStatusApi").Value + FileNo;
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var data2 = JsonSerializer.Deserialize<ApiResponseVerifyPaymentApiStatus>(apiResponse);

                    return View(data2);
                }
            }
        }
    }
}
