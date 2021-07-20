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
    public class CompactorDetailsZoneWiseController : Controller
    {
        public IConfiguration _configuration;
        public CompactorDetailsZoneWiseController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            List<CompactorApiZoneWiseDto> CompactorList = new List<CompactorApiZoneWiseDto>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("compactorZoneWiseApi").Value))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var data2 = JsonSerializer.Deserialize<ApiResponseCompactorZoneWise>(apiResponse);

                    return View(data2);
                }
            }
        }
    }
}
