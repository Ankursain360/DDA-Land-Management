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
        private readonly IDPPublicPaymentService _dPPublicPaymentService;


        public MyPaymentController(IDPPublicPaymentService dPPublicPaymentService, IConfiguration configuration,IDamagepayeeregisterService damagepayeeregisterService)
        {
            _configuration = configuration;
            _dPPublicPaymentService = dPPublicPaymentService;         
            _damagepayeeregisterService = damagepayeeregisterService;
        }

        public async Task<IActionResult> Index()
        {
            string FileNo = _damagepayeeregisterService.GetFileNo(SiteContext.UserId);
            if (FileNo != null || FileNo!="")
            {
                using (var httpClient = new HttpClient())
                {
                  
                    List<VerifyPaymentApiStatusDto> damagecalculation = new List<VerifyPaymentApiStatusDto>();
                    string url = _configuration.GetSection("VerifyPaymentStatusApi").Value + FileNo;
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        var data = JsonSerializer.Deserialize<ApiResponseVerifyPaymentApiStatus>(apiResponse);

                        return View(data);
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
