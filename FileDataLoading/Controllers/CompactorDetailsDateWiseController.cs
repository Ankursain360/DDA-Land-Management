using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Dto.Search;
using FileDataLoading.Filters;
using Core.Enum;


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
            ApiResponseCompactorDateWise data = new ApiResponseCompactorDateWise();
            List<CompactorApiDateWiseDto> cargo = new List<CompactorApiDateWiseDto>();
            data.FromDate = DateTime.Now.AddDays(-7);
            data.Todate = DateTime.Now;
            data.cargo = cargo;
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Index(DateTime fromdate, DateTime todate)
        {
            //Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("compactorDateWiseApi").Value + "fromdate_mm_dd_yyyy=" + Convert.ToDateTime(fromdate).ToString("MM-dd-yyyy") + "&todate_mm_dd_yyyy=" + Convert.ToDateTime(todate).ToString("MM-dd-yyyy")))

                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<ApiResponseCompactorDateWise>(apiResponse);
                        data.FromDate = fromdate;
                        data.Todate = todate;
                        return View(data);

                    }
                    else
                    {
                        ApiResponseCompactorDateWise data = new ApiResponseCompactorDateWise();
                        List<CompactorApiDateWiseDto> cargo = new List<CompactorApiDateWiseDto>();
                        data.FromDate = fromdate;
                        data.Todate = todate;
                        data.cargo = cargo;
                        return View(data);
                    }

                }
            }


        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DateWiseCompactordetailsList(DateTime fromdate,  DateTime todate)

        {
            List<DateWiseCompactorListDto> data = new List<DateWiseCompactorListDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("compactorDateWiseApi").Value + "fromdate_mm_dd_yyyy=" + Convert.ToDateTime(fromdate).ToString("MM-dd-yyyy") + "&todate_mm_dd_yyyy=" + Convert.ToDateTime(todate).ToString("MM-dd-yyyy")))
              

                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<ApiResponseCompactorDateWise>(apiResponse);

                    if (result != null)
                    {
                        for (int i = 0; i < result.cargo.Count(); i++)
                        {
                            data.Add(new DateWiseCompactorListDto()
                            {
                                SNO = result.cargo[i].SNO,
                                DEPT_NAME = result.cargo[i].DEPT_NAME,
                                FINALLY = result.cargo[i].FINALLY,
                                TOTAL = result.cargo[i].TOTAL,
                                WEEKLY = result.cargo[i].WEEKLY,
                                TOTAL_WEEKLY = result.cargo[i].TOTAL_WEEKLY


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
