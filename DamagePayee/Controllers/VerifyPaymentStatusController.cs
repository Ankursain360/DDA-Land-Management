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
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        public VerifyPaymentStatusController(IConfiguration configuration, IDamagepayeeregisterService damagepayeeregisterService)
        {
            _configuration = configuration;
            _damagepayeeregisterService = damagepayeeregisterService;

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] string FileNo)
        {
            //string FileNo = "D/QS/77/2524";
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

     
        //public async Task<IActionResult> SaveDamagePaymentVerify(ApiResponseVerifyPaymentApiStatus district)        {
        //    //try
        //    //{

        //    //    if (ModelState.IsValid)
        //    //    {


        //    //       // var result = await _districtService.Create(district);

        //    //        if (result == true)
        //    //        {
        //    //            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

        //    //         //   var list = await _districtService.GetAllDistrict();
        //    //            return View("Index", list);
        //    //        }
        //    //        else
        //    //        {
        //    //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //    //            return View(district);

        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        return View(district);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //    //    return View(district);
        //    //}
        //}

    }
}
