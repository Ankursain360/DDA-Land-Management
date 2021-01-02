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
using Dto.Master;
using System.Globalization;

namespace DamagePayee.Controllers
{
    public class DamageCalculatorController : BaseController
    {
        private readonly IDamageCalculationService _damagecalculationService;
        public IConfiguration _configuration;

        int GlobalRow = 0;
        int IndxCounter2 = 0, DateChk2 = 0;
        string rateStr = "";
        public DamageCalculatorController(IDamageCalculationService damagecalculationService, IConfiguration configuration)
        {
            _configuration = configuration;
            _damagecalculationService = damagecalculationService;

        }
        public async Task<IActionResult> Index()
        {
            Damagecalculation damagecalculation = new Damagecalculation();
            damagecalculation.PropertyType1 = await _damagecalculationService.GetPropertyTypes();
            damagecalculation.LocalityList = await _damagecalculationService.GetLocalities();
            return View(damagecalculation);
        }


        [HttpPost]
        public async Task<PartialViewResult> DamageCalculate([FromBody] DamageCalculationDto dto)
        {
            List<DamageChargesCalculation> damagecalculation = new List<DamageChargesCalculation>();

            if (dto.PropertyTypeId == "" || dto.LocalityId == "" || dto.Area == "")
            {
                ViewBag.Message = Alert.Show("All Mandatory Must be Filled", "", AlertType.Warning);
                return PartialView("_DamageCalculate", damagecalculation);
            }
            if (dto.FromDate < dto.EncroachmentDate)
            {
                ViewBag.Message = Alert.Show("From Date can not be smaller than EncroachDate", "", AlertType.Warning);
                return PartialView("_DamageCalculate", damagecalculation);
            }
            else if (dto.ToDate < dto.EncroachmentDate || dto.ToDate < dto.FromDate)
            {
                ViewBag.Message = Alert.Show("To Date can not be smaller than EncroachDate/FromDate", "", AlertType.Warning);
                return PartialView("_DamageCalculate", damagecalculation);
            }
            else if (dto.EncroachmentDate.Year < 1952)
            {
                ViewBag.Message = Alert.Show("Encroachment Date start from 1952 !", "", AlertType.Warning);
                return PartialView("_DamageCalculate", damagecalculation);
            }
            else
            {
                int yr = 0;
                if (dto.FromDate.ToString() != "" && dto.ToDate.ToString() != "")
                {
                    yr = CalculateYearBetweenTwoDates(dto);
                    SetInitialRowBetweenTwoDates(yr, dto, damagecalculation);
                    //dttable = GetData_Grid_To_DataTable(Gridview2);
                }
                //else
                //{
                //    yr = CalculateYear();
                //    SetInitialRow(yr);
                //    dttable = GetData_Grid_To_DataTable(Gridview1);
                //}
                //gridInterest.DataSource = dttable;
                //gridInterest.DataBind();

                //tblfinalsubmit.Visible = true;
                //btnFinalSubmit.Visible = true;


                //damagecalculation.Add(new DamageChargesCalculation
                //{
                //    StartDate = DateTime.Now
                //});
            }

            return PartialView("_DamageCalculate", damagecalculation);
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
                EndDate = DateTime.ParseExact(ddtt, "d/M/yyyy", CultureInfo.InvariantCulture);
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

        private void SetInitialRowBetweenTwoDates(int year, DamageCalculationDto dto, List<DamageChargesCalculation> damagecalculation)
        {

            DateTime fromdate = dto.FromDate;
            DateTime toodate = dto.ToDate;

            string listFromDate;
            string listToDate;
            string listMonth;
            string listArea;
            string listRate;
            string listDamageCharge;
            string listCompoundAmt;
            string listRemainAmt;
            string listCompoundRemainAmt;

            int i = 0;
            for (i = 0; i < year; i++)
            {
                int cnt = 0;
                string fromDt;
                string toDt;
                string rt = null;
                double ValMonth = 0;
                int m1 = fromdate.Month;
                int y1 = fromdate.Year;
                int m2 = toodate.Month;

                if (DateChk2 == 0)
                {
                    if (i == 0)
                    {
                        listFromDate = fromdate.ToString();
                        if (m1 >= 4)
                        {
                            if (m2 > 3)
                            {
                                listToDate = toodate.ToString();
                            }
                            else
                            {
                                listToDate = "31/03/" + (y1 + 1).ToString();
                            }
                            fromDt = "31/03/" + (y1 + 1).ToString();
                        }
                        else
                        {
                            if (m2 > 3)
                            {
                                listToDate = toodate.ToString();
                            }
                            else
                            {
                                listToDate = "31/03/" + y1.ToString();
                            }

                            fromDt = "31/03/" + y1.ToString();
                        }

                    }
                    else if (i == 1)
                    {
                        if (m1 >= 4)
                        {
                            listFromDate = "01/04/" + (y1 + 1).ToString();
                            if (m2 > 3)
                            {
                                listToDate = toodate.ToString();
                            }
                            else
                            {
                                listToDate = "31/03/" + (fromdate.Year + IndxCounter2 + 1).ToString();
                            }

                            y1 = y1 + 1;
                        }
                        else
                        {
                            listFromDate = "01/04/" + y1.ToString();
                            if (m2 > 3)
                            {
                                listToDate = toodate.ToString();
                            }
                            else
                            {
                                listToDate = "31/03/" + (fromdate.Year + IndxCounter2).ToString();
                            }

                            y1 = y1 + 1;
                        }
                    }
                    else
                    {
                        if (m1 >= 4)
                        {
                            listFromDate = "01/04/" + (y1 + IndxCounter2).ToString();
                            if (m2 > 3)
                            {
                                listToDate = toodate.ToString();
                            }
                            else
                            {
                                listToDate = "31/03/" + (fromdate.Year + IndxCounter2 + 1).ToString();
                            }

                            y1 = y1 + 1;
                        }
                        else
                        {
                            listFromDate = "01/04/" + (y1 + IndxCounter2 - 1).ToString();
                            if (m2 > 3)
                            {
                                listToDate = toodate.ToString();
                            }
                            else
                            {
                                listToDate = "31/03/" + (fromdate.Year + IndxCounter2).ToString();
                            }
                            y1 = y1 + 1;
                        }
                    }
                    //----Month----
                    listMonth = (CalMonth(listFromDate, listToDate)).ToString();
                    damagecalculation.Add(new DamageChargesCalculation
                    {
                        StartDate = Convert.ToDateTime(listFromDate),
                        EndDate = Convert.ToDateTime(listToDate),
                        Area = Convert.ToDecimal(dto.Area),
                        Months = listMonth
                    });
                    IndxCounter2++;
                }
                else
                {
                    listFromDate = "22/06/2015";
                    listToDate = "31/03/2016";
                    DateChk2 = 0;
                    listMonth = "9";
                    damagecalculation.Add(new DamageChargesCalculation
                    {
                        StartDate = Convert.ToDateTime(listFromDate),
                        EndDate = Convert.ToDateTime(listToDate),
                        Area = Convert.ToDecimal(dto.Area),
                        //----Month----
                        Months = listMonth
                    });
                }

                //------------------------------------ Renu 15 june 2020------------------------
                if (listFromDate == "01/04/2015" && listToDate == "31/03/2016")
                {
                    listFromDate = "01/04/2015";
                    listToDate = "21/06/2015";
                    DateChk2 = 1;
                    //----Month----
                    listMonth = "3";
                    damagecalculation.Add(new DamageChargesCalculation
                    {
                        StartDate = Convert.ToDateTime(listFromDate),
                        EndDate = Convert.ToDateTime(listToDate),
                        Area = Convert.ToDecimal(dto.Area),
                        //----Month----
                        Months = listMonth
                    });

                }

                ValMonth = Convert.ToDouble(listMonth) / 12;
                //------------------------------------ Renu 15 june 2020------------------------

                //----Area-----
                listArea =dto.Area;

                //----Rate-----
                //if (dto.PropertyTypeId != "0" && dto.EncroachmentDate.ToString() != "" && dto.Area != "")
                //{
                //    if (dto.PropertyTypeId == "1")
                //    {
                //        DataTable dtt = BindStartAndEndDate(txtfromdate1.Text, txttodate1.Text);
                //        if (dtt.Rows.Count > 0)
                //        {
                //            if (dtt.Rows.Count > 1)
                //            {
                //                for (int indx = 0; indx < dtt.Rows.Count; indx++)
                //                {
                //                    rateStr = rateStr + dtt.Rows[indx]["RATE"].ToString() + "/";
                //                }

                //                rt = rateStr.Substring(0, rateStr.Length - 1);
                //            }
                //            else
                //            {
                //                rt = dtt.Rows[0]["RATE"].ToString();
                //            }
                //        }
                //    }
                //    else
                //    {
                //        DataTable dtt = BindStartAndEndDateComm(txtfromdate1.Text, txttodate1.Text);
                //        if (dtt.Rows.Count > 0)
                //        {
                //            if (dtt.Rows.Count > 1)
                //            {
                //                for (int indx = 0; indx < dtt.Rows.Count; indx++)
                //                {
                //                    rateStr = rateStr + dtt.Rows[indx]["RATE"].ToString() + "/";
                //                }

                //                rt = rateStr.Substring(0, rateStr.Length - 1);
                //            }
                //            else
                //            {
                //                rt = dtt.Rows[0]["RATE"].ToString();
                //            }
                //        }
                //    }

                //    txtrate1.Text = rt.ToString();
                //    //    rateStr = "";

                //    //    //--------------Damage Charge------------
                //    //    txtdcharge1.Text = Math.Round((Convert.ToDouble(txtrate1.Text) * Convert.ToDouble(txtarea1.Text) * Convert.ToDouble(txtmonth1.Text)),0).ToString();
                //    //    grdDamageTotal = grdDamageTotal + Convert.ToDouble(txtdcharge1.Text);
                //    //    //--------------Compound Intrest------------
                //    //    if (e.Row.RowIndex == 0)
                //    //    {
                //    //        txtCoumpound1.Text = "0";

                //    //    }
                //    //    else
                //    //    {
                //    //        GridViewRow gv = Gridview2.Rows[e.Row.RowIndex - 1];
                //    //        TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //    //        TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");
                //    //        //TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");


                //    //        //txtCoumpound1.Text =Math.Round(((Convert.ToDouble(txtCoumpound11.Text)+Convert.ToDouble(txtdcharge1.Text))*0.07),2).ToString();
                //    //        txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtdcharge11.Text)) * 1.07), 0).ToString();
                //    //    }
                //    //    grdCompoundTotal = Convert.ToDouble(txtCoumpound1.Text);
                //    //    //--------------Paid Amount Charge------------
                //    //    txtPaidAmount1.Text = "0";
                //    //    grdPaidTotal = grdPaidTotal + Convert.ToDouble(txtPaidAmount1.Text);
                //    //    //--------------Remain Amount Charge------------
                //    //    txtRemainAmount1.Text = "0";
                //    //    grdRemainTotal = grdRemainTotal + Convert.ToDouble(txtRemainAmount1.Text);
                //    //}
                //    //else
                //    //{
                //    //    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Plz Select Property and Locality and Date!')</script>");
                //    //}
                //    //lblRbt.Text = (grdCompoundTotal * 0.5).ToString();  14/02/20

                //    //---------------------------------17/02-20----------------------------------------------------
                //    rateStr = "";

                //    //--------------Damage Charge------------
                //    if (txtrate1.Text.IndexOf("/") > 0)
                //    {
                //        string[] rtstr = txtrate1.Text.Split('/');
                //        txtdcharge1.Text = ((Convert.ToDouble(rtstr[0]) * Convert.ToDouble(txtarea1.Text) * 4) + (Convert.ToDouble(rtstr[1]) * Convert.ToDouble(txtarea1.Text) * 8)).ToString();
                //        e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                //    }
                //    else
                //    {
                //        txtdcharge1.Text = (Convert.ToDouble(txtrate1.Text) * Convert.ToDouble(txtarea1.Text) * Convert.ToDouble(txtmonth1.Text)).ToString();
                //    }
                //    grdDamageTotal = grdDamageTotal + Convert.ToDouble(txtdcharge1.Text);


                //    //--------------Paid Amount Charge------------
                //    txtPaidAmount1.Text = "0";

                //    /////////////////////////////////////////////////////////////////////////
                //    string con = ConfigurationManager.ConnectionStrings["con"].ToString();
                //    int chk = 0;
                //    //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                //    //string query = "SELECT ID,START_DT,END_DT,RATE,AREA,MONTHS,DAMAGECHARGES,COMPUNDING,PAID_AMOUNT,REMAIN_AMOUNT FROM CALCULATION_TBL where CREATION_BY='" + Session["creationby"] + "' order by START_DT";
                //    string query = "select * from DAMAGE_PAYMENT_HISTORY_TEMP where Damage_id='" + Session["ID"] + "' order by PAYMENT_HISTORY_DATE ASC";
                //    using (OracleConnection con1 = new OracleConnection(con))
                //    {
                //        using (OracleDataAdapter sda = new OracleDataAdapter(query, con))
                //        {
                //            using (DataTable dt = new DataTable())
                //            {
                //                sda.Fill(dt);
                //                if (dt.Rows.Count > 0)
                //                {
                //                    for (int i = 0; i < dt.Rows.Count; i++)
                //                    {
                //                        DateTime ppdate = Convert.ToDateTime(dt.Rows[i]["PAYMENT_HISTORY_DATE"].ToString()); //DateTime.ParseExact(dt.Rows[i]["PAYMENT_HISTORY_DATE"].ToString(), "d/M/yyyy", CultureInfo.InvariantCulture);
                //                        DateTime ssdate = DateTime.ParseExact(txtfromdate1.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                //                        DateTime ttdate = DateTime.ParseExact(txttodate1.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                //                        if (ppdate >= ssdate && ppdate <= ttdate)
                //                        {
                //                            txtPaidAmount1.Text = dt.Rows[i]["PAYMENT_HISTORY_AMOUNT"].ToString();
                //                            chk = 1;
                //                        }
                //                    }
                //                    if (chk == 0)
                //                    {
                //                        txtPaidAmount1.Text = "0";
                //                    }
                //                }
                //                else
                //                {
                //                    txtPaidAmount1.Text = "0";
                //                }
                //            }
                //        }
                //    }

                //    /////////////////////////////////////////////////////////////////////////

                //    grdPaidTotal = grdPaidTotal + Convert.ToDouble(txtPaidAmount1.Text);


                //    //--------------Comulative Damage and Interest calculation------------

                //    /////////////////////////////////////sandeep\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                //    if (e.Row.RowIndex < 0)
                //    {
                //        txtCoumpound1.Text = txtdcharge1.Text;
                //        txtRemainAmount1.Text = "0";
                //    }
                //    else
                //    {
                //        if (DateTime.ParseExact("21/06/2015", "d/M/yyyy", CultureInfo.InvariantCulture) < DateTime.ParseExact(txttodate1.Text, "d/M/yyyy", CultureInfo.InvariantCulture))
                //        {
                //            if (CIntrst2 == 1)
                //            {
                //                //GridViewRow gv = Gridview2.Rows[e.Row.RowIndex];
                //                //TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //                //TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");
                //                //TextBox txtRemainAmount11 = (TextBox)gv.FindControl("txtRemainAmount");
                //                //txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //                ////txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtRemainAmount11.Text)) * 0.07), 0).ToString();
                //                //txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + grdRemainTotal) * 0.07), 0).ToString();

                //                //txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtRemainAmount11.Text)) * 0.07), 0).ToString();


                //                if (e.Row.RowIndex > 0)
                //                {
                //                    GridViewRow gv = Gridview2.Rows[e.Row.RowIndex - 1];
                //                    TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //                    TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");
                //                    txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //                    txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound1.Text) + grdRemainTotal) * 0.07 * ValMonth), 0).ToString();
                //                }
                //                else
                //                {
                //                    txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //                    txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound1.Text) + grdRemainTotal) * 0.07 * ValMonth), 0).ToString();
                //                }
                //            }
                //            else
                //            {
                //                //GridViewRow gv = Gridview2.Rows[e.Row.RowIndex];
                //                //TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //                //TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");
                //                //txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //                //txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + grdRemainTotal) * 0.07), 0).ToString();

                //                //GridViewRow gv = Gridview2.Rows[e.Row.RowIndex];
                //                //TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //                //TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");

                //                if (e.Row.RowIndex > 0)
                //                {
                //                    GridViewRow gv = Gridview2.Rows[e.Row.RowIndex - 1];
                //                    TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //                    TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");
                //                    txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //                    txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound1.Text) + grdRemainTotal) * 0.07 * ValMonth), 0).ToString();
                //                }
                //                else
                //                {
                //                    txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //                    txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound1.Text) + grdRemainTotal) * 0.07 * ValMonth), 0).ToString();
                //                }
                //                CIntrst2 = 1;
                //            }
                //            Rmd2 = 1;
                //        }
                //        else
                //        {
                //            //GridViewRow gv = Gridview2.Rows[e.Row.RowIndex];
                //            //TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //            //TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");
                //            //txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //            //txtRemainAmount1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text)) * 0.07), 0).ToString();
                //            //GridViewRow gv = Gridview2.Rows[e.Row.RowIndex];
                //            //TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //            //TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");
                //            if (e.Row.RowIndex > 0)
                //            {
                //                GridViewRow gv = Gridview2.Rows[e.Row.RowIndex - 1];
                //                TextBox txtCoumpound11 = (TextBox)gv.FindControl("txtCoumpound");
                //                TextBox txtdcharge11 = (TextBox)gv.FindControl("txtdcharge");
                //                txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtCoumpound11.Text) + Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //                txtRemainAmount1.Text = Math.Round((Convert.ToDouble(txtCoumpound1.Text) * 0.07 * ValMonth), 0).ToString();
                //            }
                //            else
                //            {
                //                txtCoumpound1.Text = Math.Round(((Convert.ToDouble(txtdcharge1.Text) - Convert.ToDouble(txtPaidAmount1.Text))), 0).ToString();
                //                txtRemainAmount1.Text = Math.Round((Convert.ToDouble(txtCoumpound1.Text) * 0.07 * ValMonth), 0).ToString();
                //            }
                //        }
                //    }
                //    grdCompoundTotal = Convert.ToDouble(txtCoumpound1.Text);
                //    grdRemainTotal = grdRemainTotal + Convert.ToDouble(txtRemainAmount1.Text);
                //    txtCommRemainAmount1.Text = grdRemainTotal.ToString();
                //    grdRemainTotal_2 = Convert.ToDouble(txtRemainAmount1.Text);
                //    str_lblCompoundTotal = txtCoumpound1.Text;
                //    str_lblCommrmnAmnt = txtCommRemainAmount1.Text;
                //}
                //else
                //{
                //    ViewBag.Message = Alert.Show("Plz Select Property and Locality and Date!", "", AlertType.Warning);
                //    List<DamageChargesCalculation> damagecalculation1 = new List<DamageChargesCalculation>();
                //}



            } //For each loop end

        }

        public int CalMonth(string from_dt, string to_dt)
        {
            int TotalMonths = 0;
            DateTime startdate = Convert.ToDateTime(from_dt.ToString());
            DateTime enddate = Convert.ToDateTime(to_dt.ToString());

            TotalMonths = (12 * (startdate.Year - enddate.Year) + (startdate.Month - enddate.Month));
            return (Math.Abs(TotalMonths) + 1);
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(Damagecalculation damagecalculation)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        return RedirectToAction("DamageCalculate", "DamageCalculator", new { id = damagecalculation });
        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return View(damagecalculation);
        //    }
        //}


        //public async Task<PartialViewResult> DamageCalculate(Damagecalculation damagecalculation)
        //{
        //    return PartialView("_DamageCalculate", damagecalculation);
        //}

    }
}
