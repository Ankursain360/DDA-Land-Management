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

namespace DamagePayee.Controllers
{
    public class DamagePayeeRegisterController : BaseController
    {
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        public IConfiguration _configuration;
        public DamagePayeeRegisterController(IDamagepayeeregisterService damagepayeeregisterService, IConfiguration configuration)
        {
            _configuration = configuration;
            _damagepayeeregisterService = damagepayeeregisterService;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeregisterSearchDto model)
        {
            var result = await _damagepayeeregisterService.GetPagedDamagepayeeregister(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            damagepayeeregistertemp.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            damagepayeeregistertemp.DistrictList = await _damagepayeeregisterService.GetDistrictList();
        }
        public async Task<IActionResult> Create()
        {
            Damagepayeeregistertemp damagepayeeregistertemp = new Damagepayeeregistertemp();

            await BindDropDown(damagepayeeregistertemp);
            return View(damagepayeeregistertemp);
        }
        [HttpPost]

        public async Task<IActionResult> Create(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            await BindDropDown(damagepayeeregistertemp);
            string PhotoFilePathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATSGPADocument").Value.ToString();
            string RecieptDocumentPathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:RecieptDocument").Value.ToString();
            if (ModelState.IsValid)
            {
                
                var result = await _damagepayeeregisterService.Create(damagepayeeregistertemp);
                if (result)
                {
                    FileHelper fileHelper = new FileHelper();

                    //****** code for saving  Damage payee personal info *****

                    if (damagepayeeregistertemp.payeeName != null &&
                      damagepayeeregistertemp.Gender != null &&
                      damagepayeeregistertemp.Address != null &&
                      damagepayeeregistertemp.MobileNo != null)
                    {
                        if (damagepayeeregistertemp.payeeName.Count > 0 &&
                    damagepayeeregistertemp.Gender.Count > 0 &&
                    damagepayeeregistertemp.Address.Count > 0 &&
                    damagepayeeregistertemp.MobileNo.Count > 0)

                        {
                            List<Damagepayeepersonelinfotemp> damagepayeepersonelinfotemp = new List<Damagepayeepersonelinfotemp>();
                            for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                            {
                                damagepayeepersonelinfotemp.Add(new Damagepayeepersonelinfotemp
                                {
                                    Name = damagepayeeregistertemp.payeeName[i],
                                    FatherName = damagepayeeregistertemp.payeeFatherName[i],
                                    Gender = damagepayeeregistertemp.Gender[i],
                                    Address = damagepayeeregistertemp.Address[i],
                                    MobileNo = damagepayeeregistertemp.MobileNo[i],
                                    EmailId = damagepayeeregistertemp.EmailId[i],
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id
                                });
                            }
                            foreach (var item in damagepayeepersonelinfotemp)
                            {
                                result = await _damagepayeeregisterService.SavePayeePersonalInfoTemp(item);
                            }
                        }
                    }

                    //****** code for saving  Allotte Type *****
                    if (damagepayeeregistertemp.Name != null &&
                     damagepayeeregistertemp.FatherName != null &&
                     damagepayeeregistertemp.Date != null)
                    {
                        if (
                         damagepayeeregistertemp.Name.Count > 0 &&
                         damagepayeeregistertemp.FatherName.Count > 0 &&
                         damagepayeeregistertemp.Date.Count > 0
                         )
                        {
                            List<Allottetypetemp> allottetypetemp = new List<Allottetypetemp>();
                            for (int i = 0; i < damagepayeeregistertemp.Name.Count; i++)
                            {
                                allottetypetemp.Add(new Allottetypetemp
                                {
                                    Name = damagepayeeregistertemp.Name[i],
                                    FatherName = damagepayeeregistertemp.FatherName[i],
                                    Date = damagepayeeregistertemp.Date[i],
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                    AtsgpadocumentPath = damagepayeeregistertemp.ATSGPA == null ? "" : damagepayeeregistertemp.ATSGPA[i] == null ? string.Empty : fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregistertemp.ATSGPA[i])
                                });
                            }
                            result = await _damagepayeeregisterService.SaveAllotteTypeTemp(allottetypetemp);
                        }
                    }

                    //****** code for saving  Damage payment history *****

                    if (damagepayeeregistertemp.PaymntName != null &&
                          damagepayeeregistertemp.RecieptNo != null &&
                          damagepayeeregistertemp.PaymentMode != null &&
                          damagepayeeregistertemp.PaymentDate != null &&
                          damagepayeeregistertemp.Amount != null)
                    {

                        //damagepayeeregister.Reciept != null &&)
                        if (
                             damagepayeeregistertemp.PaymntName.Count > 0 &&
                             damagepayeeregistertemp.RecieptNo.Count > 0 &&
                             damagepayeeregistertemp.PaymentMode.Count > 0 &&
                             damagepayeeregistertemp.PaymentDate.Count > 0 &&
                             damagepayeeregistertemp.Amount.Count > 0
                             )

                        {
                            List<Damagepaymenthistorytemp> damagepaymenthistorytemp = new List<Damagepaymenthistorytemp>();
                            for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                            {
                                damagepaymenthistorytemp.Add(new Damagepaymenthistorytemp
                                {
                                    Name = damagepayeeregistertemp.PaymntName[i],
                                    RecieptNo = damagepayeeregistertemp.RecieptNo[i],
                                    PaymentMode = damagepayeeregistertemp.PaymentMode[i],
                                    PaymentDate = damagepayeeregistertemp.PaymentDate[i],
                                    Amount = damagepayeeregistertemp.Amount[i],

                                    //RecieptDocumentPath = damagepayeeregister.Reciept[i] == null ? string.Empty : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregister.Reciept[i]),
                                    RecieptDocumentPath = damagepayeeregistertemp.Reciept == null ? "" : damagepayeeregistertemp.Reciept[i] == null ? string.Empty : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]),

                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id
                                });
                            }

                            result = await _damagepayeeregisterService.SavePaymentHistoryTemp(damagepaymenthistorytemp);

                        }
                    }
                    return View(damagepayeeregistertemp);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(damagepayeeregistertemp);
                }

            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregistertemp);
            }
        }
    }
}
