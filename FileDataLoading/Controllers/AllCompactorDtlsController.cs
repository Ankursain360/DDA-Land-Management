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
using NPOI.POIFS.Crypt.Dsig;

namespace FileDataLoading.Controllers
{
    public class AllCompactorDtlsController : Controller
    {
        public IConfiguration _configuration;
        public AllCompactorDtlsController(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }
        public async Task<IActionResult> Index()
        {
              
        List<CompactorApiDto> CompactorList = new List<CompactorApiDto>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("compactorallApi").Value))
               {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (!apiResponse.Contains("No Record Found"))
                        {
                            var data2 = JsonSerializer.Deserialize<ApiResponseCompactor>(apiResponse);
                            return View(data2);
                        }
                        else
                        {
                            ApiResponseCompactor data = new ApiResponseCompactor();
                            return View(data);

                        }
                    }
                    else
                    {
                        ApiResponseCompactor data = new ApiResponseCompactor();
                        return View(data);
                    }
                }
            }
        }
        public IActionResult Index1()
        {
            return View();
        }
            [HttpPost]
        public async Task<IActionResult> Index1(string fileno,string subject,string scheme)
        {
            //Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("compactorFileNoApi").Value + fileno + "&subject="+subject+ "&SchemeWise="+scheme))
                   
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<ApiResponseCompactor>(apiResponse);
                        return View(data);
                       
                    }
                    else
                    {
                        return View();

                    }
                   
                }
            }          

        }
        public IActionResult DemoPetApi() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DemoPetApi(DemoPetApiConsume demo)
        {


            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _configuration.GetSection("PetOwnerLoginDetailsApi").Value.ToString());
            var content = new StringContent("{\r\n    \"User_Name\":\"yash001\",\r\n    \"Mobile_No\":\"8398835630\"\r\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            var data = await response.Content.ReadAsStringAsync();

            return View();

            ////Reservation reservation = new Reservation();
            //using (var httpClient = new HttpClient()) 
            //{
            //    using (var response = await httpClient.PostAsync(_configuration.GetSection("PetOwnerLoginDetailsApi").Value + demo.User_Name +  demo.Mobile_No))

            //    {
            //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            string apiResponse = await response.Content.ReadAsStringAsync();
            //            var data = JsonSerializer.Deserialize<DemoPetApiConsume>(apiResponse);
            //            return View(data);

            //        }
            //        else
            //        {
            //            return View();

            //        }

            //    }
            //}

        }

    }
}
