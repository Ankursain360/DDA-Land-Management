using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FileDataLoading.Controllers
{
    public class CompactorDetailsDateWiseController : Controller
    { 
        public IConfiguration _configuration;
        public CompactorDetailsDateWiseController(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }


        public IActionResult Index1()
        {
            return View();
        }
      

        [HttpPost]
        public async Task<IActionResult> Index1(DateTime fromdate, DateTime todate)
        {
            //Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("compactorDateWiseApi").Value + "fromdate_mm_dd_yyyy=" +  Convert.ToDateTime(fromdate).ToString("MM-dd-yyyy") + "&todate_mm_dd_yyyy=" + Convert.ToDateTime(todate).ToString("MM-dd-yyyy")))

                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<ApiResponseCompactorDateWise>(apiResponse);
                        return View(data);

                    }
                    else
                    {
                        return View();

                    }

                }
            }


        }
    }
}
