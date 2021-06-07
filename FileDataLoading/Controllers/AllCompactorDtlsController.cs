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
                    string apiResponse = await response.Content.ReadAsStringAsync();
                
                    var data2 = JsonSerializer.Deserialize<ApiResponseCompactor>(apiResponse);

                    return View(data2);
                }
            }
        }
        public IActionResult Index1()
        {
            return View();
        }
            [HttpPost]
        public async Task<IActionResult> Index1(string fileno)
        {
            //Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("compactorFileNoApi").Value + fileno))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<ApiResponseCompactor>(apiResponse);
                        return View(data);
                       
                    }
                    else
                    {
                        return View(fileno);

                    }
                   
                }
            }
           

        }
       
    }
}
