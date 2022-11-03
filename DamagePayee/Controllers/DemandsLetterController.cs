using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;
using DamagePayee.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Globalization; 
using System.IO;
using Newtonsoft.Json;
namespace DamagePayee.Controllers
{
    public class DemandsLetterController : Controller
    {
        private readonly IDemandLetterService _demandLetterService;
        int GlobalRow = 0;
        int IndxCounter2 = 0, DateChk2 = 0, CIntrst2 = 0, Rmd2 = 0;
        string rateStr = "", str_lblCompoundTotal = "", str_lblCommrmnAmnt = "";
        double grdDamageTotal = 0, grdRemainTotal = 0, grdRemainTotal_2 = 0, grdCompoundTotal = 0;
        public DemandsLetterController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        async Task BindDropDownView(Demandletters demandletters)
        {
            demandletters.LocalityList = await _demandLetterService.GetLocalityList();
            demandletters.propertyTypeList = await _demandLetterService.GetPropertyType();

        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Demandletters model = new Demandletters();
            await BindDropDownView(model);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Demandletters demandletter)
        {
            await BindDropDownView(demandletter);
            try
            {
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                demandletter.DemandNo = "DMN" + finalString;

                if (ModelState.IsValid)
                {
                    var result = await _demandLetterService.Create(demandletter);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        return View("_List", demandletter);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demandletter);
                    }
                }
                else
                {
                    return View(demandletter);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demandletter);
            }
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandletterSearchDto model)
        {
            try
            {
                var result = await _demandLetterService.GetPagedDemandletter(model);

                return PartialView("_List1", result);
            }
            catch (Exception ex)
            {
                return PartialView(ex);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _demandLetterService.FetchSingleResult(id);
            Data.LocalityList = await _demandLetterService.GetLocalityList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Demandletters demandletter)
        {
            await BindDropDownView(demandletter);
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _demandLetterService.Update(id, demandletter);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View("_List", demandletter);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demandletter);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(demandletter);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _demandLetterService.FetchSingleResult(id);
            Data.LocalityList = await _demandLetterService.GetLocalityList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



        public async Task<IActionResult> DemandsLetterList([FromBody] DemandletterSearchDto model)
        {
            var result = await _demandLetterService.GetAllDemandletterList(model);
            List<DemandLetterListDto> data = new List<DemandLetterListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemandLetterListDto()
                    {
                        Id = result[i].Id,
                        Locality = result[i].Locality == null ? "" : result[i].Locality.Name.ToString(),
                        DemandNo = result[i].DemandNo,
                        DueAmount = result[i].DepositDue,
                        ReliefAmount = result[i].ReliefAmount.ToString(),
                        FileNo = result[i].FileNo,
                        Name = result[i].Name,
                        FatherName = result[i].FatherName,
                        Address = result[i].Address,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }

        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        [HttpPost]
        public async Task<JsonResult> AutoComplete(string prefix)
        {
            return Json(await _demandLetterService.GetFileAutoCompleteDetails(prefix));
        }
        public async Task<JsonResult> GetFileDetails(int fileid)
        {
            return Json(await _demandLetterService.GetFileNODetail(fileid));
        }

        #region damage Calculation
        [HttpPost]
        public async Task<JsonResult> DamageCalculate([FromBody] DamageCalculationDto dto)
        {
            List<DamageChargesCalculation> damagecalculation = new List<DamageChargesCalculation>();

            int yr = 0;
            yr = CalculateYearBetweenTwoDates(dto);
            try
            {

                SetInitialRowBetweenTwoDates(yr, dto, damagecalculation);
            }
            catch (Exception)
            {

                throw;
            }
            var list = new List<string>();
            list.Add(damagecalculation.Sum(x => x.DamageCharges).ToString());
            list.Add(damagecalculation.Sum(x => x.TotalInterest).ToString());
            return Json(list);
        }
        public int CalculateYearBetweenTwoDates(DamageCalculationDto dto)
        {
            int YrRes = 0;
            DateTime startDate = dto.FromDate;
            DateTime EndDate = dto.ToDate;

            //Excel documentation says "COMPLETE calendar years in between dates"
            if (EndDate.Month > 3)
            {
                string ddtt = "31/03/" + ((EndDate.Year) + 1).ToString();
                //log
                this.WriteToFile("year line number 95:" + ddtt.ToString());
                //  ddtt = Convert.ToDateTime(ddtt).ToString("d/M/yyyy");
                EndDate = DateTime.ParseExact(ddtt, "d/M/yyyy", CultureInfo.InvariantCulture);
                //log
                this.WriteToFile("year line number 79:" + EndDate.ToString());
            }

            int years = EndDate.Year - startDate.Year;

            if (startDate.Month == EndDate.Month &&// if the start month and the end month are the same
                EndDate.Day < startDate.Day// AND the end day is less than the start day
                || EndDate.Month < startDate.Month)// OR if the end month is less than the start month
            {
                years--;
            }

            if (startDate.Year <= 2015 && EndDate.Year >= 2015)
            {
                YrRes = years + 2;
            }
            else
            {
                YrRes = years + 1;
            }
            return YrRes;

        }

        private async void SetInitialRowBetweenTwoDates(int year, DamageCalculationDto dto, List<DamageChargesCalculation> damagecalculation)
        {
            try
            {



                DateTime fromdate = dto.FromDate;
                DateTime toodate = dto.ToDate;
                GlobalRow = year;
                string listFromDate;
                string listToDate;
                string listMonth;
                string listArea;
                string listRate = "0";
                string listDamageCharge = "0";
                string listCompoundAmt = "0";
                string listRemainAmt = "0";
                string listCompoundRemainAmt = "0";
                string listPaidAmt = "0";

                int i = 0;
                for (i = 0; i < year; i++)
                { //log
                    this.WriteToFile("year and I loop value line number 149:  i:" + i.ToString() + "  year:" + year.ToString());
                    int cnt = 0;
                    string fromDt;
                    string toDt;
                    string rt = "";
                    double ValMonth = 0;
                    //log
                    this.WriteToFile("fromdate value line number 156:" + fromdate.ToString());
                    int m1 = fromdate.Month;
                    int y1 = fromdate.Year;
                    int m2 = toodate.Month;

                    if (DateChk2 == 0)
                    {
                        if (i == 0)
                        {
                            listFromDate = fromdate.ToString("dd/MM/yyyy");
                            //log
                            this.WriteToFile("listFromDate line number 167:" + listFromDate);
                            if (m1 >= 4)
                            {
                                if (i == GlobalRow - 1 && m2 > 3)
                                {
                                    listToDate = toodate.ToString("dd/MM/yyyy");
                                    //log
                                    this.WriteToFile("listToDate line number 174:" + listFromDate);
                                }
                                else
                                {
                                    listToDate = "31/03/" + (y1 + 1).ToString();
                                    //log
                                    this.WriteToFile("listToDate line number 180:" + listFromDate);
                                }
                                fromDt = "31/03/" + (y1 + 1).ToString();
                                //log
                                this.WriteToFile("fromDt line number 184:" + fromDt);
                            }
                            else
                            {
                                if (i == GlobalRow - 1 && m2 > 3)
                                {
                                    listToDate = toodate.ToString("dd/MM/yyyy");
                                    //log
                                    this.WriteToFile("listToDate line number 192:" + listToDate);
                                }
                                else
                                {
                                    listToDate = "31/03/" + y1.ToString();
                                    //log
                                    this.WriteToFile("listToDate line number 198:" + listToDate);
                                }

                                fromDt = "31/03/" + y1.ToString();
                                //log
                                this.WriteToFile("fromDt line number 202:" + fromDt);
                            }

                        }
                        else if (i == 1)
                        {
                            if (m1 >= 4)
                            {
                                listFromDate = "01/04/" + (y1 + 1).ToString();
                                //log
                                this.WriteToFile("listFromDate line number 213:" + listFromDate);

                                if (i == GlobalRow - 1 && m2 > 3)
                                {
                                    listToDate = toodate.ToString("dd/MM/yyyy");
                                    //log
                                    this.WriteToFile("listToDate line number 219:" + listToDate);
                                }
                                else
                                {
                                    listToDate = "31/03/" + (fromdate.Year + IndxCounter2 + 1).ToString();
                                    //log
                                    this.WriteToFile("listToDate line number 225:" + listToDate);
                                }

                                y1 = y1 + 1;
                            }
                            else
                            {
                                listFromDate = "01/04/" + y1.ToString();
                                //log
                                this.WriteToFile("listFromDate line number 234:" + listFromDate);
                                if (i == GlobalRow - 1 && m2 > 3)
                                {
                                    listToDate = toodate.ToString("dd/MM/yyyy");
                                    //log
                                    this.WriteToFile("listToDate line number 239:" + listToDate);
                                }
                                else
                                {
                                    listToDate = "31/03/" + (fromdate.Year + IndxCounter2).ToString();
                                    //log
                                    this.WriteToFile("listToDate line number 245:" + listToDate);
                                }

                                y1 = y1 + 1;
                            }
                        }
                        else
                        {
                            if (m1 >= 4)
                            {
                                listFromDate = "01/04/" + (y1 + IndxCounter2).ToString();
                                //log
                                this.WriteToFile("listFromDate line number 257:" + listFromDate);
                                if (i == GlobalRow - 1 && m2 > 3)
                                {
                                    listToDate = toodate.ToString("dd/MM/yyyy");
                                    //log
                                    this.WriteToFile("listToDate line number 262:" + listToDate);
                                }
                                else
                                {
                                    listToDate = "31/03/" + (fromdate.Year + IndxCounter2 + 1).ToString();
                                    //log
                                    this.WriteToFile("listToDate line number 268:" + listToDate);
                                }

                                y1 = y1 + 1;
                            }
                            else
                            {
                                listFromDate = "01/04/" + (y1 + IndxCounter2 - 1).ToString();
                                //log
                                this.WriteToFile("listFromDate line number 278:" + listFromDate);
                                if (i == GlobalRow - 1 && m2 > 3)
                                {
                                    listToDate = toodate.ToString("dd/MM/yyyy");
                                    //log
                                    this.WriteToFile("listToDate line number 282:" + listToDate);
                                }
                                else
                                {
                                    listToDate = "31/03/" + (fromdate.Year + IndxCounter2).ToString();
                                    //log
                                    this.WriteToFile("listToDate line number 287:" + listToDate);
                                }
                                y1 = y1 + 1;
                            }
                        }
                        //----Month----
                        listMonth = (CalMonth(listFromDate, listToDate)).ToString();
                        //log
                        this.WriteToFile("listMonth line number 296:" + listMonth);
                        IndxCounter2++;
                    }
                    else
                    {
                        listFromDate = "22/06/2015";
                        listToDate = "31/03/2016";
                        DateChk2 = 0;
                        listMonth = "9";
                    }

                    //------------------------------------ Renu 15 june 2020------------------------
                    if (listFromDate == "01/04/2015" && listToDate == "31/03/2016")
                    {
                        listFromDate = "01/04/2015";
                        listToDate = "21/06/2015";
                        DateChk2 = 1;
                        //----Month----
                        listMonth = "3";

                    }

                    ValMonth = Convert.ToDouble(listMonth) / 12;
                    //------------------------------------ Renu 15 june 2020------------------------

                    //----Area-----
                    listArea = dto.Area;

                    //----Rate-----
                    if (dto.PropertyTypeId != "0" && dto.EncroachmentDate.ToString() != "" && dto.Area != "")
                    {
                        if (dto.PropertyTypeId == "1")
                        {
                            var dtt = await BindStartAndEndDate(listFromDate, listToDate, dto);
                            if (dtt.Count > 0)
                            {
                                if (dtt.Count > 1)
                                {
                                    for (int indx = 0; indx < dtt.Count; indx++)
                                    {
                                        rateStr = rateStr + dtt[indx].Rate.ToString() + "/";
                                    }

                                    rt = rateStr.Substring(0, rateStr.Length - 1);
                                }
                                else
                                {
                                    rt = dtt[0].Rate.ToString();
                                }
                            }
                        }
                        else
                        {
                            var dtt = await BindStartAndEndDateComm(listFromDate, listToDate, dto);
                            if (dtt.Count > 0)
                            {
                                if (dtt.Count > 1)
                                {
                                    for (int indx = 0; indx < dtt.Count; indx++)
                                    {
                                        rateStr = rateStr + dtt[indx].Rate.ToString() + "/";
                                    }

                                    rt = rateStr.Substring(0, rateStr.Length - 1);
                                }
                                else
                                {
                                    rt = dtt[0].Rate.ToString();
                                }
                            }
                        }

                        listRate = rt;
                        //log
                        this.WriteToFile("listRate line number 370:" + listRate);
                        //---------------------------------17/02-20----------------------------------------------------
                        rateStr = "";

                        //--------------Damage Charge------------
                        if (listRate.IndexOf("/") > 0)
                        {
                            string[] rtstr = listRate.Split('/');
                            listDamageCharge = Math.Round((((Convert.ToDouble(rtstr[0]) * Convert.ToDouble(listArea) * 4) + (Convert.ToDouble(rtstr[1]) * Convert.ToDouble(listArea) * 8))), 2).ToString();
                            // e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            listDamageCharge = Math.Round((((listRate == "" ? 0 : Convert.ToDouble(listRate)) * Convert.ToDouble(listArea) * Convert.ToDouble(listMonth))), 2).ToString();
                        }
                        grdDamageTotal = grdDamageTotal + Convert.ToDouble(listDamageCharge);

                        //--------------Comulative Damage and Interest calculation------------

                        /////////////////////////////////////Renu\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                        if (i < 0)
                        {
                            listCompoundAmt = listDamageCharge;
                            listRemainAmt = "0";
                        }
                        else
                        {
                            listToDate = DateTime.ParseExact(listToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                            //  listToDate = Convert.ToDateTime(listToDate).ToString("d/M/yyyy");
                            //log
                            this.WriteToFile("listToDate line number 400:" + listToDate);
                            //  if (Convert.ToDateTime("21/06/2015") < Convert.ToDateTime(listToDate))
                            if (DateTime.ParseExact("21/06/2015", "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.ParseExact(listToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                            {
                                if (CIntrst2 == 1)
                                {
                                    if (i > 0)
                                    {
                                        listCompoundAmt = Math.Round(((Convert.ToDouble(listDamageCharge) == 0 ? 0 : Convert.ToDouble(listCompoundAmt) + Convert.ToDouble(listDamageCharge) - Convert.ToDouble(listPaidAmt))), 0).ToString();
                                        listRemainAmt = Math.Round(((Convert.ToDouble(listCompoundAmt) + grdRemainTotal) * 0.07 * ValMonth), 0).ToString();

                                    }
                                    else
                                    {
                                        listCompoundAmt = Math.Round(((Convert.ToDouble(listDamageCharge) - Convert.ToDouble(listPaidAmt))), 0).ToString();
                                        listRemainAmt = Math.Round(((Convert.ToDouble(listCompoundAmt) + grdRemainTotal) * 0.07 * ValMonth), 0).ToString();
                                    }
                                }
                                else
                                {
                                    if (i > 0)
                                    {
                                        listCompoundAmt = Math.Round(((Convert.ToDouble(listCompoundAmt) + Convert.ToDouble(listDamageCharge) - Convert.ToDouble(listPaidAmt))), 0).ToString();
                                        listRemainAmt = Math.Round(((Convert.ToDouble(listCompoundAmt) + grdRemainTotal) * 0.07 * ValMonth), 0).ToString();
                                    }
                                    else
                                    {
                                        listCompoundAmt = Math.Round(((Convert.ToDouble(listDamageCharge) - Convert.ToDouble(listPaidAmt))), 0).ToString();
                                        listRemainAmt = Math.Round(((Convert.ToDouble(listCompoundAmt) + grdRemainTotal) * 0.07 * ValMonth), 0).ToString();
                                    }
                                    CIntrst2 = 1;
                                }
                                Rmd2 = 1;
                            }
                            else
                            {
                                if (i > 0)
                                {
                                    listCompoundAmt = Math.Round(((Convert.ToDouble(listCompoundAmt) + Convert.ToDouble(listDamageCharge) - Convert.ToDouble(listPaidAmt))), 0).ToString();
                                    listRemainAmt = Math.Round((Convert.ToDouble(listCompoundAmt) * 0.07 * ValMonth), 0).ToString();
                                }
                                else
                                {
                                    listCompoundAmt = Math.Round(((Convert.ToDouble(listDamageCharge) - Convert.ToDouble(listPaidAmt))), 0).ToString();
                                    listRemainAmt = Math.Round((Convert.ToDouble(listCompoundAmt) * 0.07 * ValMonth), 0).ToString();
                                }
                            }
                        }
                        grdCompoundTotal = Convert.ToDouble(listCompoundAmt);
                        grdRemainTotal = grdRemainTotal + Convert.ToDouble(listRemainAmt);
                        listCompoundRemainAmt = grdRemainTotal.ToString();
                        grdRemainTotal_2 = Convert.ToDouble(listRemainAmt);
                        str_lblCompoundTotal = listCompoundAmt;
                        str_lblCommrmnAmnt = listCompoundRemainAmt;
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show("Plz Select Property and Locality and Date!", "", AlertType.Warning);
                        List<DamageChargesCalculation> damagecalculation1 = new List<DamageChargesCalculation>();
                    }
                    //log
                    this.WriteToFile("listFromDate line number 461:" + listFromDate);
                    //    this.WriteToFile("listFromDate line number 463:" + DateTime.ParseExact(listFromDate, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));
                    this.WriteToFile("listToDate line number 464:" + listToDate);
                    damagecalculation.Add(new DamageChargesCalculation
                    {
                        StartDate = DateTime.ParseExact(listFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), //Convert.ToDateTime(listFromDate),
                        EndDate = DateTime.ParseExact(listToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),// Convert.ToDateTime(listToDate),
                        Area = Convert.ToDecimal(dto.Area),
                        Months = listMonth,
                        Rate = (listRate == "" ? "" : listRate),
                        DamageCharges = (listDamageCharge == "" ? 0 : Convert.ToDecimal(listDamageCharge)),
                        Compunding = (listCompoundAmt == "" ? 0 : Convert.ToDecimal(listCompoundAmt)),
                        TotalInterest = (listRate == "" ? 0 : Convert.ToDecimal(listRemainAmt)),
                        TotalPayAmount = (listRate == "" ? 0 : Convert.ToDecimal(listCompoundRemainAmt))
                    });
                    //log
                    this.WriteToFile("List data at 474:" + JsonConvert.SerializeObject(damagecalculation));

                    ViewBag.GrandCompoundInterest = listCompoundRemainAmt;
                } //For each loop end
                ViewBag.GrandDamageTotoal = damagecalculation.Sum(x => x.DamageCharges);
                ViewBag.GrandCompoundInterest = damagecalculation.Sum(x => x.TotalInterest);
            }
            catch (Exception ex)
            {
                this.WriteToFile("Error in Damage Calculator ex:  " + ex.Message.ToString());
                this.WriteToFile("Error in Damage Calculator  Stack details: " + ex.StackTrace.ToString());
                this.WriteToFile("Error in Damage Calculator  inner exception: " + ex.InnerException.Message.ToString());
            }
        }

        public double CalMonth(string from_dt, string to_dt)
        {
            //log
            this.WriteToFile("from_dt, to_dt line number 489: fromdate" + from_dt + "  todate:" + to_dt);
            double TotalMonths = 0;
            DateTime startdate = DateTime.ParseExact(from_dt, "dd/MM/yyyy", CultureInfo.InvariantCulture); // Convert.ToDateTime(from_dt.ToString());
            DateTime enddate = DateTime.ParseExact(to_dt, "dd/MM/yyyy", CultureInfo.InvariantCulture); // Convert.ToDateTime(to_dt.ToString());
            this.WriteToFile("startdate, enddate line number 489: startdate" + startdate.ToString() + "  enddate:" + enddate.ToString());
            if (startdate.Day >= 16 && startdate.Day <= 31)
            {
                TotalMonths = (12 * (startdate.Year - enddate.Year) + (startdate.Month - enddate.Month) + (0.5));
            }
            else
            {
                TotalMonths = (12 * (startdate.Year - enddate.Year) + (startdate.Month - enddate.Month));
            }
            return (Math.Abs(TotalMonths) + 1);
        }

        private void WriteToFile(string text)
        {
            string Short_path = @"C:\Vedang\Applications\Logs";
            string path = @"C:\Vedang\Applications\Logs\Public_DamageCalculatorLog_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            if ((Short_path != "") && Directory.Exists(Short_path))
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("------------------ Log Start At " + DateTime.Now.ToString() + " ------------------");
                    writer.WriteLine(" ");
                    writer.WriteLine(text);
                    writer.WriteLine(" ");
                    writer.WriteLine("------------------------------------");
                    writer.WriteLine(" ");
                }
            }
        }

        public async Task<List<DamageCalculatorRateMappingDto>> BindStartAndEndDate(string s_date, string e_date, DamageCalculationDto dto)
        {
            List<DamageCalculatorRateMappingDto> damageCalculatorRateMappingDto = new List<DamageCalculatorRateMappingDto>();
            try
            {

                DateTime date1 = dto.EncroachmentDate;
                var dt = await _demandLetterService.FetchResultEncroachmentType(date1);

                dynamic result1 = null;
                if (dt != null)
                {
                    if (date1.Year > 1992)
                    {
                        if (dt.EncroachName == "TYPE_A" && dt.Id.ToString() == "1")
                        {
                            if (date1 <= DateTime.ParseExact("31/03/1960", "dd/MM/yyyy", CultureInfo.InvariantCulture))
                            // if (date1 <= Convert.ToDateTime("31/03/1960"))//31/3/1960
                            {
                                var subEncroachersId = new[] { 1, 4 };
                                //result1 = await _demandLetterService.RateListTypeA(Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                                result1 = await _demandLetterService.RateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);

                            }
                            else if (date1 >= DateTime.ParseExact("01/04/1960", "dd/MM/yyyy", CultureInfo.InvariantCulture) && date1 <= DateTime.ParseExact("31/03/1981", "dd/MM/yyyy", CultureInfo.InvariantCulture))//31/3/1981  1/4/1960)
                                                                                                                                                                                                                      //else if (date1 >= Convert.ToDateTime("01/04/1960") && date1 <= Convert.ToDateTime("31/03/1981"))//31/3/1981  1/4/1960
                            {
                                var subEncroachersId = new[] { 2, 4 };
                                // result1 = await _demandLetterService.RateListTypeA(Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                                result1 = await _demandLetterService.RateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            }
                            else
                            {
                                var subEncroachersId = new[] { 3, 4 };
                                //result1 = await _demandLetterService.RateListTypeA(Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                                result1 = await _demandLetterService.RateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            }
                        }
                        else if (dt.EncroachName == "TYPE_B" && dt.Id.ToString() == "2")
                        {
                            if (e_date == "31/03/2002")
                            {
                                var subEncroachersId = new[] { 4 };
                                //result1 = await _demandLetterService.RateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                result1 = await _demandLetterService.RateListTypeBSpecific(DateTime.ParseExact("31/03/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            }
                            else
                            {
                                var subEncroachersId = new[] { 4 };
                                //result1 = await _demandLetterService.RateListTypeB(Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                                result1 = await _demandLetterService.RateListTypeB(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            }
                        }
                        else if (dt.EncroachName == "TYPE_C" && dt.Id.ToString() == "3")
                        {
                            var subEncroachersId = new[] { 4 };
                            // result1 = await _demandLetterService.RateListTypeC(Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                            result1 = await _demandLetterService.RateListTypeC(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                        }
                    }
                    else
                    {
                        if (dt.EncroachName == "TYPE_A" && dt.Id.ToString() == "1")
                        {
                            ///if (date1 <= Convert.ToDateTime("31/03/1960"))//31/3/1960
                            if (date1 <= DateTime.ParseExact("31/03/1960", "dd/MM/yyyy", CultureInfo.InvariantCulture))
                            {
                                if (s_date == "01/04/2001" && e_date == "31/03/2002")
                                {
                                    DateTime specificDateTime = DateTime.ParseExact("31/07/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    var subEncroachersId = new[] { 1, 4 };
                                    // result1 = await _demandLetterService.RateListTypeASpecific(specificDateTime, Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                                    result1 = await _demandLetterService.RateListTypeASpecific(specificDateTime, DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                }
                                else
                                {
                                    var subEncroachersId = new[] { 1, 4 };
                                    // result1 = await _demandLetterService.RateListTypeA(Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                                    result1 = await _demandLetterService.RateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                }
                            }
                            else if (date1 >= DateTime.ParseExact("04/01/1960", "dd/MM/yyyy", CultureInfo.InvariantCulture) && date1 <= DateTime.ParseExact("31/03/1981", "dd/MM/yyyy", CultureInfo.InvariantCulture))//31/3/1981  1/4/1960
                            {
                                if (s_date == "01/04/2001" && e_date == "31/03/2002")
                                {
                                    DateTime specificDateTime = DateTime.ParseExact("31/07/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    var subEncroachersId = new[] { 2, 4 };
                                    // result1 = await _demandLetterService.RateListTypeASpecific(specificDateTime, Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                                    result1 = await _demandLetterService.RateListTypeASpecific(specificDateTime, DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                }
                                else
                                {
                                    var subEncroachersId = new[] { 2, 4 };
                                    //result1 = await _demandLetterService.RateListTypeA(Convert.ToDateTime(e_date), dto.LocalityId, subEncroachersId);
                                    result1 = await _demandLetterService.RateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                }
                            }
                            else
                            {
                                var subEncroachersId = new[] { 3, 4 };
                                result1 = await _demandLetterService.RateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            }
                        }
                        else if (dt.EncroachName == "TYPE_B" && dt.Id.ToString() == "2")
                        {
                            var subEncroachersId = new[] { 4 };
                            result1 = await _demandLetterService.RateListTypeB(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                        }
                        else if (dt.EncroachName == "TYPE_C" && dt.Id.ToString() == "3")
                        {
                            var subEncroachersId = new[] { 4 };
                            result1 = await _demandLetterService.RateListTypeC(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                        }
                    }

                }
                else
                {

                    ViewBag.Message = Alert.Show("Plz Enter Valid Date!", "", AlertType.Warning);
                    List<DamageChargesCalculation> damagecalculation1 = new List<DamageChargesCalculation>();
                }

                if (result1 != null)
                {
                    for (int i = 0; i < result1.Count; i++)
                    {
                        damageCalculatorRateMappingDto.Add(new DamageCalculatorRateMappingDto
                        {
                            Id = result1[i].Id,
                            EncroachId = result1[i].EncroachId,
                            ColonyId = result1[i].ColonyId,
                            StartDate = result1[i].StartDate,
                            EndDate = result1[i].EndDate,
                            SubEncroachId = result1[i].SubEncroachId,
                            Rate = result1[i].Rate,
                            IsActive = result1[i].IsActive
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                //log
                this.WriteToFile("BindStartAndEndDate error :" + ex.Message.ToString());
            }
            return damageCalculatorRateMappingDto;
        }

        public async Task<List<DamageCalculatorRateMappingDto>> BindStartAndEndDateComm(string s_date, string e_date, DamageCalculationDto dto)
        {
            List<DamageCalculatorRateMappingDto> damageCalculatorRateMappingDto = new List<DamageCalculatorRateMappingDto>();
            try
            {
                //log
                this.WriteToFile(" line 678 Comm rate function s_date :" + s_date + "  end date: " + e_date);

                DateTime date1 = dto.EncroachmentDate;
                var dt = await _demandLetterService.FetchResultCOMEncroachmentType(date1);

                dynamic result1 = null;

                if (dt != null)
                {
                    if (date1.Year > 1992)
                    {  //log
                        this.WriteToFile(" line 689 Comm rate function dt.EncroachName :" + dt.EncroachName + "  Id: " + dt.Id.ToString());
                        if (dt.EncroachName == "TYPE_A" && dt.Id.ToString() == "1")
                        {
                            //  if (date1 <= Convert.ToDateTime("31/03/1960"))//31/3/1960 DateTime.ParseExact(from_dt, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
                            if (date1 <= DateTime.ParseExact("31/03/1960", "dd/MM/yyyy", CultureInfo.InvariantCulture))
                            {//log
                                this.WriteToFile(" line 695 Comm rate function Encroacher id :1,5  and e_date:" + e_date);
                                var subEncroachersId = new[] { 1, 5 };
                                result1 = await _demandLetterService.ComRateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                //log
                                this.WriteToFile("List data at 699:" + JsonConvert.SerializeObject(result1));
                            }
                            else if (date1 >= DateTime.ParseExact("01/04/1960", "dd/MM/yyyy", CultureInfo.InvariantCulture) && date1 <= DateTime.ParseExact("31/03/1976", "dd/MM/yyyy", CultureInfo.InvariantCulture))//31/3/1981  1/4/1960
                            {
                                this.WriteToFile(" line 703 Comm rate function Encroacher id :2,5  and e_date:" + e_date);
                                var subEncroachersId = new[] { 2, 5 };
                                result1 = await _demandLetterService.ComRateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                this.WriteToFile("List data at 706:" + JsonConvert.SerializeObject(result1));
                            }
                            else if (date1 >= DateTime.ParseExact("01/04/1976", "dd/MM/yyyy", CultureInfo.InvariantCulture) && date1 <= DateTime.ParseExact("31/03/1981", "dd/MM/yyyy", CultureInfo.InvariantCulture))//31/3/1981  1/4/1960
                            {
                                this.WriteToFile(" line 710 Comm rate function Encroacher id :3,5  and e_date:" + e_date);
                                var subEncroachersId = new[] { 3, 5 };
                                result1 = await _demandLetterService.ComRateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                this.WriteToFile("List data at 713:" + JsonConvert.SerializeObject(result1));
                            }
                            else
                            {
                                this.WriteToFile(" line 717 Comm rate function Encroacher id :4,5  and e_date:" + e_date);
                                var subEncroachersId = new[] { 4, 5 };
                                result1 = await _demandLetterService.ComRateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                this.WriteToFile("List data at 720:" + JsonConvert.SerializeObject(result1));
                            }
                        }
                        else if (dt.EncroachName == "TYPE_B" && dt.Id.ToString() == "2")
                        {
                            this.WriteToFile(" line 725 Comm rate function Encroacher id :5  and e_date:" + e_date);
                            var subEncroachersId = new[] { 5 };
                            result1 = await _demandLetterService.ComRateListTypeB(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            this.WriteToFile("List data at 728:" + JsonConvert.SerializeObject(result1));
                        }
                        else if (dt.EncroachName == "TYPE_C" && dt.Id.ToString() == "3")
                        {
                            this.WriteToFile(" line 732 Comm rate function Encroacher id :5  and e_date:" + e_date);
                            var subEncroachersId = new[] { 5 };
                            result1 = await _demandLetterService.ComRateListTypeC(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            this.WriteToFile("List data at 735:" + JsonConvert.SerializeObject(result1));
                        }
                    }
                    else
                    {
                        if (dt.EncroachName == "TYPE_A" && dt.Id.ToString() == "1")
                        {
                            if (date1 <= DateTime.ParseExact("31/03/1960", "dd/MM/yyyy", CultureInfo.InvariantCulture))//03/31/1960
                            {
                                if (s_date == "01/04/2001" && e_date == "31/03/2002")
                                {
                                    this.WriteToFile(" line 746 Comm rate function Encroacher id :5  and e_date:" + e_date);
                                    DateTime specificDateTime = DateTime.ParseExact("31/07/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    var subEncroachersId = new[] { 1, 5 };
                                    result1 = await _demandLetterService.ComRateListTypeASpecific(specificDateTime, DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                    this.WriteToFile("List data at 750:" + JsonConvert.SerializeObject(result1));
                                }
                                else
                                {
                                    var subEncroachersId = new[] { 1, 5 };
                                    result1 = await _demandLetterService.ComRateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                    this.WriteToFile("List data at 756:" + JsonConvert.SerializeObject(result1));
                                }
                            }
                            else if (date1 >= DateTime.ParseExact("04/01/1960", "dd/MM/yyyy", CultureInfo.InvariantCulture) && date1 <= DateTime.ParseExact("31/03/1976", "dd/MM/yyyy", CultureInfo.InvariantCulture))//03/31/1981  1/4/1960
                            {
                                if (s_date == "01/04/2001" && e_date == "31/03/2002")
                                {
                                    DateTime specificDateTime = DateTime.ParseExact("31/07/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    var subEncroachersId = new[] { 2, 5 };
                                    result1 = await _demandLetterService.ComRateListTypeASpecific(specificDateTime, DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                    this.WriteToFile("List data at 766:" + JsonConvert.SerializeObject(result1));
                                }
                                else
                                {
                                    var subEncroachersId = new[] { 2, 5 };
                                    result1 = await _demandLetterService.ComRateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                    this.WriteToFile("List data at 772:" + JsonConvert.SerializeObject(result1));
                                }
                            }
                            else if (date1 >= DateTime.ParseExact("04/01/1976", "dd/MM/yyyy", CultureInfo.InvariantCulture) && date1 <= DateTime.ParseExact("31/03/1981", "dd/MM/yyyy", CultureInfo.InvariantCulture))//03/31/1981  1/4/1960
                            {
                                if (s_date == "01/04/2001" && e_date == "31/03/2002")
                                {
                                    DateTime specificDateTime = DateTime.ParseExact("31/07/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    var subEncroachersId = new[] { 3, 5 };
                                    result1 = await _demandLetterService.ComRateListTypeASpecific(specificDateTime, DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                    this.WriteToFile("List data at 782:" + JsonConvert.SerializeObject(result1));
                                }
                                else
                                {
                                    var subEncroachersId = new[] { 3, 5 };
                                    result1 = await _demandLetterService.ComRateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                    this.WriteToFile("List data at 788:" + JsonConvert.SerializeObject(result1));
                                }
                            }
                            else
                            {
                                if (s_date == "01/04/2001" && e_date == "31/03/2002")
                                {

                                    DateTime specificDateTime = DateTime.ParseExact("31/07/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    var subEncroachersId = new[] { 4, 5 };
                                    result1 = await _demandLetterService.ComRateListTypeASpecific(specificDateTime, DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                    this.WriteToFile("List data at 799:" + JsonConvert.SerializeObject(result1));
                                }
                                else
                                {
                                    var subEncroachersId = new[] { 4, 5 };
                                    result1 = await _demandLetterService.ComRateListTypeA(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                                    this.WriteToFile("List data at 805:" + JsonConvert.SerializeObject(result1));
                                }
                            }
                        }
                        else if (dt.EncroachName == "TYPE_B" && dt.Id.ToString() == "2")
                        {
                            var subEncroachersId = new[] { 5 };
                            result1 = await _demandLetterService.ComRateListTypeB(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            this.WriteToFile("List data at 813:" + JsonConvert.SerializeObject(result1));
                        }
                        else if (dt.EncroachName == "TYPE_C" && dt.Id.ToString() == "3")
                        {
                            var subEncroachersId = new[] { 5 };
                            result1 = await _demandLetterService.ComRateListTypeC(DateTime.ParseExact(e_date, "dd/MM/yyyy", CultureInfo.InvariantCulture), dto.LocalityId, subEncroachersId);
                            this.WriteToFile("List data at 819:" + JsonConvert.SerializeObject(result1));
                        }
                    }

                }
                else
                {

                    ViewBag.Message = Alert.Show("Plz Enter Valid Date!", "", AlertType.Warning);
                    List<DamageChargesCalculation> damagecalculation1 = new List<DamageChargesCalculation>();
                }

                if (result1 != null)
                {
                    for (int i = 0; i < result1.Count; i++)
                    {
                        damageCalculatorRateMappingDto.Add(new DamageCalculatorRateMappingDto
                        {
                            Id = result1[i].Id,
                            EncroachId = result1[i].EncroachId,
                            ColonyId = result1[i].ColonyId,
                            StartDate = result1[i].StartDate,
                            EndDate = result1[i].EndDate,
                            SubEncroachId = result1[i].SubEncroachId,
                            Rate = result1[i].Rate,
                            IsActive = result1[i].IsActive
                        });
                    }
                }


            }
            catch (Exception ex)
            {

                //log
                this.WriteToFile("BindStartAndEndDateComm error :" + ex.Message.ToString());

            }
            return damageCalculatorRateMappingDto;
        }

        #endregion
    }
}
