using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LeaseForPublic.Controllers
{
    public class DemandDetailsController : BaseController
    {
        private readonly IDemandDetailsService _demandDetailsService;
        private readonly IKycformService _kycformService;
        private readonly IKycdemandpaymentdetailstableaService __kycdemandpaymentdetailstableaService;
        private readonly IKycdemandpaymentdetailstablebService _kycdemandpaymentdetailstablebService;
        private readonly IKycdemandpaymentdetailstablecService _kycdemandpaymentdetailstablecService;
        public IConfiguration _configuration;
        public DemandDetailsController(IConfiguration configuration, IDemandDetailsService demandDetailsService,IKycformService kycform,IKycdemandpaymentdetailstableaService kycdemandpaymentdetailstableaService,IKycdemandpaymentdetailstablebService kycdemandpaymentdetailstablebService,IKycdemandpaymentdetailstablecService kycdemandpaymentdetailstablecService)
        {
            _configuration = configuration;
            _demandDetailsService = demandDetailsService;
            _kycformService = kycform;
            __kycdemandpaymentdetailstableaService = kycdemandpaymentdetailstableaService;
            _kycdemandpaymentdetailstablebService = kycdemandpaymentdetailstablebService;
            _kycdemandpaymentdetailstablecService=kycdemandpaymentdetailstablecService;
        }

        public IActionResult Index()
        
        {
            return View();
        }


        public async Task<IActionResult> Create(int Id)
        {
            LeasePublicDemandPaymentDetailsDto dto = new LeasePublicDemandPaymentDetailsDto();
            var data= await _kycformService.FetchSingleResult(Id);
            dto.KycId = data.Id;
            dto.FileNo = data.FileNo;           
            return View(dto);

        }


        public async Task<PartialViewResult> KYCFormView(int id)
        {
            var Data = await _kycformService.FetchKYCSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
            Data.LocalityList = await _kycformService.GetLocalityList();
            
            return PartialView("_KYCFormView", Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeasePublicDemandPaymentDetailsDto dto)
        {
            Kycdemandpaymentdetails oKycdemandpaymentdetails = new Kycdemandpaymentdetails();
            try
            {
                ModelState.Remove("Amount");
                ModelState.Remove("DateofPaymentByAllottee");

                if (ModelState.IsValid)
                {
                    oKycdemandpaymentdetails.KycId = dto.KycId;
                    oKycdemandpaymentdetails.PendingAt = dto.PendingAt;
                    oKycdemandpaymentdetails.TotalPayable = dto.TotalPayable;
                    oKycdemandpaymentdetails.TotalDues=dto.TotalDues;
                    oKycdemandpaymentdetails.IsPaymentAgreed = dto.IsPaymentAgreed;
                    oKycdemandpaymentdetails.CreatedBy = SiteContext.UserId;
                    oKycdemandpaymentdetails.IsActive = 1;
                    oKycdemandpaymentdetails.CreatedDate = DateTime.Now;
                    var result = await _demandDetailsService.Create(oKycdemandpaymentdetails);

                    if (result == true)
                    {
                        var result1 = await _demandDetailsService.GetPaymentDetails(Convert.ToInt32(dto.KycId));
                        List<Kycdemandpaymentdetailstablea> data = new List<Kycdemandpaymentdetailstablea>();

                        //*********************************************** Save Payment Deatails  ****************************  
                        if (result1 != null)
                        {
                            for (int i = 0; i < result1.Count; i++)
                            {
                                data.Add(new Kycdemandpaymentdetailstablea()
                                {
                                    DemandPeriod = result1[i].DemandPeriod,
                                    GroundRent = result1[i].GroundRentLeaseRent,
                                    InterestRate = result1[i].InterestAmount,
                                    TotdalDues = result1[i].TotalDues,                                
                                    KycId= oKycdemandpaymentdetails.KycId,
                                    DemandPaymentId = oKycdemandpaymentdetails.Id,
                                    IsActive = 1,
                                    CreatedBy =SiteContext.UserId,
                                    CreatedDate= DateTime.Now,
                            });
                            }

                            foreach (var item in data)
                            {
                                result = await __kycdemandpaymentdetailstableaService.SaveDemandPaymentDetails(item);
                            }
                        }

                        //*********************************************** Save Payment API Details  ********************************  
                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.GetAsync(_configuration.GetSection("BhoomiApi").Value + dto.FileNo))
                            {
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    string apiResponse = await response.Content.ReadAsStringAsync();
                                    var result2 = JsonSerializer.Deserialize<ApiResponseBhoomiApiFileWise>(apiResponse);

                                    List<Kycdemandpaymentdetailstableb> datac = new List<Kycdemandpaymentdetailstableb>();
                                    if (result2 != null)
                                    {
                                        for (int i = 0; i < result2.cargo.Count(); i++)
                                        {
                                            datac.Add(new Kycdemandpaymentdetailstableb()
                                            {
                                                ChallanNo = result2.cargo[i].CHLLN_NMBR,
                                                ChallanAmount = result2.cargo[i].CHLLN_AMNT.ToString(),
                                                DepositeDate =Convert.ToDateTime(result2.cargo[i].DPST_DT),
                                                KycId = oKycdemandpaymentdetails.KycId,
                                                DemandPaymentId = oKycdemandpaymentdetails.Id,
                                                CreatedBy=SiteContext.UserId,
                                                CreatedDate=DateTime.Now,
                                                IsActive=1,
                                            });
                                        }

                                        foreach (var item in datac)
                                        {
                                            var result3 = await _kycdemandpaymentdetailstablebService.SaveDemandPaymentAPIDetails(item);
                                        }
                                    }


                                }
                            }
                        }

                        //********************************** Save Payment Challan Details  ********************************************  

                        if (oKycdemandpaymentdetails.IsPaymentAgreed == "N")
                        {
                            
                          
                                List<Kycdemandpaymentdetailstablec> okycdemandpaymentdetailstablec = new List<Kycdemandpaymentdetailstablec>();
                                for (int i = 0; i < dto.PaymentType.Count; i++)
                                {
                                    okycdemandpaymentdetailstablec.Add(new Kycdemandpaymentdetailstablec
                                    {
                                        PaymentType = dto.PaymentType[i],
                                        Period = dto.Period[i],
                                        ChallanNo = dto.ChallanNoForPayment[i],   
                                        Amount = dto.Amount[i],
                                        Proofinpdf = dto.Proofinpdf[i],
                                        DateofPaymentByAllottee = dto.DateofPaymentByAllottee[i],
                                        Ddabankcredit = dto.Ddabankcredit[i],
                                        CreatedBy=SiteContext.UserId,
                                         IsActive=1,
                                         CreatedDate=DateTime.Now,
                                         KycId=oKycdemandpaymentdetails.KycId,
                                        DemandPaymentId = oKycdemandpaymentdetails.Id,
                                    });
                                }
                                foreach (var item in okycdemandpaymentdetailstablec)
                                {
                                    var result4 = await _kycdemandpaymentdetailstablecService.SaveKycChallanDetails(item);
                                }

                            }

                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(dto);

                    }
                }
                else
                {
                    return View(dto);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(dto);
            }
            return View(dto);
        }

    





        public async Task<PartialViewResult> PaymentDetails(int Id)
        {
          
            var result = await _demandDetailsService.GetPaymentDetails(Id);

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
