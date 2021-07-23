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
using FileDataLoading.Filters;
using Core.Enum;

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


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> ZoneWiseCompactordetailsList()
        {
            List<ZoneWiseCompactorListDto> data = new List<ZoneWiseCompactorListDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("compactorZoneWiseApi").Value))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<ApiResponseCompactorZoneWise>(apiResponse);

                    if (result != null)
                    {
                        for (int i = 0; i < result.cargo.Count(); i++)
                        {
                            data.Add(new ZoneWiseCompactorListDto()
                            {
                                SNO = result.cargo[i].SNO,
                                DEPT_NAME = result.cargo[i].DEPT_NAME,
                                BRANCH_NAME = result.cargo[i].BRANCH_NAME,
                                ISSUED = result.cargo[i].ISSUED,
                                UNISSUED = result.cargo[i].UNISSUED,
                                TOTAL = result.cargo[i].TOTAL,

                            }); ;
                        }
                    }
                }
            }



            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
