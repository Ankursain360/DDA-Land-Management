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
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;

        public VerifyPaymentStatusController(IConfiguration configuration, IPaymentverificationService paymentverificationService, IDamagepayeeregisterService damagepayeeregisterService)
        {
            _configuration = configuration;
            _paymentverificationService = paymentverificationService;
            _damagepayeeregisterService = damagepayeeregisterService;
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
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (!apiResponse.Contains("No Record Found"))
                        {
                            var data2 = JsonSerializer.Deserialize<ApiResponseVerifyPaymentApiStatus>(apiResponse);

                            return PartialView("_List", data2);
                        }
                        else
                        {
                            ApiResponseVerifyPaymentApiStatus data = new ApiResponseVerifyPaymentApiStatus();
                            return PartialView("_List", data);
                        }
                    }
                    else
                    {
                        ApiResponseVerifyPaymentApiStatus data = new ApiResponseVerifyPaymentApiStatus();
                        return PartialView("_List", data);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] VerifyPaymentStatusForFileNoDto model)
        {
            using (var httpClient = new HttpClient())
            {
                var fileno = model.fileNo;
                string Propertyno = _damagepayeeregisterService.GetPropertyNo(fileno);
                using (var response = await httpClient.GetAsync(_configuration.GetSection("VerifyPaymentStatusApi").Value + fileno))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var result2 = JsonSerializer.Deserialize<ApiResponseVerifyPaymentApiStatus>(apiResponse);

                        List<Paymentverification> paymentverifications = new List<Paymentverification>();
                        if (result2 != null)
                        {
                            for (int i = 0; i < result2.cargo.Count(); i++)
                            {
                                paymentverifications.Add(new Paymentverification()
                                {
                                    PayeeName = result2.cargo[i].APPLICANT_NAME_PAYMENT == null ? "N/A" : result2.cargo[i].APPLICANT_NAME_PAYMENT,
                                    PaymentMode = result2.cargo[i].PAYMENT_MODE == null ? "N/A" : result2.cargo[i].PAYMENT_MODE,
                                    BankTransactionId = result2.cargo[i].BANK_TRANSACTIONID == null ? "N/A" : result2.cargo[i].BANK_TRANSACTIONID,
                                    AmountPaid = Convert.ToDecimal(result2.cargo[i].AMOUNT_RECIEVED),
                                    BankName = result2.cargo[i].BANK.ToString() == null ? "N/A" : result2.cargo[i].BANK.ToString(),
                                    FileNo = result2.cargo[i].FILENO == null ? "N/A" : result2.cargo[i].FILENO,
                                    TransactionId = result2.cargo[i].PG_TRANSACTIONID == null ? "N/A" : result2.cargo[i].PG_TRANSACTIONID,
                                    PropertyNo = Propertyno,
                                    InterestPaid = 0,
                                    TotalAmount = Convert.ToDecimal(result2.cargo[i].AMOUNT_RECIEVED) + 0,
                                    IsVerified = 0,
                                    CreatedBy = 1,
                                    CreatedDate = DateTime.Now
                                });
                            }

                            foreach (var item in paymentverifications)
                            {
                                var result3 = await _paymentverificationService.SaveDemandPaymentAPIDetails(item);
                                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            }

                        }


                    }
                }
            }
            return View();
        }




    }
}
