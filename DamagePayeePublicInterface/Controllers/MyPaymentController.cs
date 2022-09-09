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
using DamagePayeePublicInterface.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Dto.Master;
using Core.Enum;
using DamagePayeePublicInterface.Filters;
using Service.IApplicationService;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;


namespace DamagePayeePublicInterface.Controllers
{
    public class MyPaymentController : BaseController
    {
        public IConfiguration _configuration;

        private readonly IDamagepayeeregisterService _damagepayeeregisterService;


        public MyPaymentController(IConfiguration configuration, IDamagepayeeregisterService damagepayeeregisterService)
        {
            _configuration = configuration;
            _damagepayeeregisterService = damagepayeeregisterService;
        }

        public async Task<IActionResult> Index()
        {
            string FileNo = _damagepayeeregisterService.GetFileNo(SiteContext.UserId);
            if (!string.IsNullOrEmpty(FileNo))
            {
                using (var httpClient = new HttpClient())
                {

                    List<VerifyPaymentApiStatusDto> damagecalculation = new List<VerifyPaymentApiStatusDto>();
                    string url = _configuration.GetSection("VerifyPaymentStatusApi").Value + FileNo;
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            if (!apiResponse.Contains("No Record Found"))
                            {
                                var data = JsonSerializer.Deserialize<ApiResponseVerifyPaymentApiStatus>(apiResponse);

                                return View(data);
                            }
                            else
                            {
                                ApiResponseVerifyPaymentApiStatus data = new ApiResponseVerifyPaymentApiStatus();
                                return View(data);

                            }
                        }
                        else
                        {
                            ApiResponseVerifyPaymentApiStatus data = new ApiResponseVerifyPaymentApiStatus();
                            return View(data);
                        }
                    }
                }
            }
            else
            {
                return View();
            }
        }
    }
}
