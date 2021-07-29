using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using LeaseForPublic.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace LeaseForPublic.Controllers
{
    public class DemandDetailsController : BaseController
    {
        private readonly IDemandDetailsService _demandDetailsService;
        public IConfiguration _configuration;
        public DemandDetailsController(IConfiguration configuration, IDemandDetailsService demandDetailsService)
        {
            _configuration = configuration;
            _demandDetailsService = demandDetailsService;
        }

        public IActionResult Index()
        
        {
            return View();
        }


        public async Task<IActionResult> Create(int Id)
        {
            
            var data= await _demandDetailsService.FetchSingleResult(Convert.ToInt32(Id));         
           
            return View(data);

        }   

        public async Task<PartialViewResult> PaymentDetails(int Id)
        {
          
            var result = await _demandDetailsService.GetPaymentDetails(Convert.ToInt32(Id));

            return PartialView("_PaymentDetails", result);
        }


        public async Task<PartialViewResult> PaymentFromBhoomi(string FileNo)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("BhoomiApi").Value + FileNo))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var data1 = JsonSerializer.Deserialize<ApiResponseBhoomiApiFileWise>(apiResponse);                      
                        return PartialView("_PaymentFromBhoomi", data1);

                    }
                    else
                    {
                        ApiResponseBhoomiApiFileWise data1 = new ApiResponseBhoomiApiFileWise();
                        List<BhoomiApiFileNowiseDto> cargo = new List<BhoomiApiFileNowiseDto>();
                        data1.cargo = cargo;                       
                        return PartialView("_PaymentFromBhoomi", data1);
                    }

                }
            }

        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandDetailsSearchDto model)
        {
            var result = await _demandDetailsService.GetPagedDemandDetails(model, "8506092802");

            return PartialView("_List", result);
        }
    }

   
}
