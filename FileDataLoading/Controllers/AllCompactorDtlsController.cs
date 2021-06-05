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
               // using (var response = await httpClient.GetAsync("http://119.226.139.196/compactorfilesearch/Damage.asmx/GetCompactorDtls_All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   // var data1 = JsonSerializer.Deserialize<List<CompactorApiDto>>(apiResponse);
                    var data2 = JsonSerializer.Deserialize<ApiResponseCompactor>(apiResponse);

                    return View(data2);
                }
            }
        }
    }
}
