using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Dto.Common
{
  public  class SendSMSDto
    {
        
        public void GenerateSendSMS(string Message, string Mobile)
        {
          //  var authkey = "9ebb4d6454214d29";
            ////  string url = "http://sms.justclicksky.com/pushsms.php?username=GNIDA&api_password=242f38iadg4voobux&sender=VCSSMS&to=" + Mobile + "&message= " + Message + " Thank you .&priority=11";
            //string url = "https://api.authkey.io/request?authkey=9ebb4d6454214d29&mobile= " + Mobile + "&message= " + Message + " Thank you .&priority=11";
          //  string url = "https://api.authkey.io/request?authkey=9ebb4d6454214d29&mobile="+ Mobile + "&country_code=91&sms="+Message+ "&sender=PRANEE&pe_id=1201160914878702009&template_id=1207162177883821156";
          //  string url = "https://api.authkey.io/request?authkey=9ebb4d6454214d29&mobile="+ Mobile + "&country_code=91&sid=956&sender=PRANEE&pe_id=1201160914878702009&template_id=1207162246819654702&var=" + Message + "";
          // string url = "http://gateway.leewaysoftech.com/xml-transconnect-api.php?username=Ddauth&password=m1kw6vu2&mobile=9639377980&message=Dear%20User%2C%20Your%20registration%20OTP%20is%201111.%20This%20OTP%20is%20valid%20for%205%20minutes.%20Land%20Management%20DDA&senderid=DDASVY&peid=1201159308150125712&contentid=1607100000000189095";
            //WebRequest request = WebRequest.Create(url);
            string url = "https://gateway.leewaysoftech.com/xml-transconnect-api.php?username=Ddauth&password=m1kw6vu2&mobile="+Mobile+ "&message=Dear%20User%2C%20Your%20registration%20OTP%20is%20" + Message+ ".%20This%20OTP%20is%20valid%20for%205%20minutes.%20Land%20Management%20DDA&senderid=DDASVY&peid=1201159308150125712&contentid=1607100000000189095";

            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
              //  StreamReader sr = new StreamReader(httpres.GetResponseStream());
              //  results = sr.ReadToEnd();
              //  logger.Error("Message Sent Status:{0}", "Mobile No:" + MobileNo + " and Result " + results);
              //  sr.Close();
            }
            catch (Exception e)
            {
              //  results = "0";
            }



        }

        public void GenerateOTP(string Message, string Mobile)
        {
           
            string url = "https://gateway.leewaysoftech.com/xml-transconnect-api.php?username=Ddauth&password=m1kw6vu2&mobile=" + Mobile + "&message=Dear%20User%2C%20Your%20OTP%20is%20"+Message+ ".%20This%20OTP%20is%20valid%20for%205%20minutes.%20Land%20Management%20.DDASVY%0A&senderid=DDASVY&peid=1201159308150125712&contentid=1607100000000200000";

            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
             
            }
            catch (Exception e)
            {
                //  results = "0";
            }



        }


        public void GenerateSendOTPForVerifyProperty(string Message, string Mobile)
        {
           
            string url = "https://gateway.leewaysoftech.com/xml-transconnect-api.php?username=Ddauth&password=m1kw6vu2&mobile=" + Mobile + "&message=Dear%20User%2C%20Your%20property%20verification%20OTP%20is%20"+Message+".%20This%20OTP%20is%20valid%20for%205%20minutes.%20Land%20Management%20DDA&senderid=DDASVY&peid=1201159308150125712&contentid=1607100000000189082";

            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                
            }
            catch (Exception e)
            {
                //  results = "0";
            }



        }
        //For Save Property Registration 
        public void GenerateSendSMSForSaveLandInventory(string PrimaryListNo, string Mobile)
        {
            string url = "https://gateway.leewaysoftech.com/xml-transconnect-api.php?username=Ddauth&password=m1kw6vu2&mobile=" + Mobile + "&message=You%20have%20successfully%20entered%20the%20Land%20Inventory%20Record%20having%20Primary%20List%20no%20"+PrimaryListNo+ "%20and%20forwarded%20for%20verification%2Fapproval%20to%20your%20reporting%20officer.%20Land%20Management%20DDA&senderid=DDASVY&peid=1201159308150125712&contentid=1607100000000189078";
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            
            }
            catch (Exception e)           
            {
              
            }

        }
        public void GenerateSendSMSForVerifyProperty(string PrimaryListNo, string Mobile)
        {
        
            string url = "https://gateway.leewaysoftech.com/xml-transconnect-api.php?username=Ddauth&password=m1kw6vu2&mobile=" + Mobile + "&message=You%20have%20successfully%20verified%20and%20added%20the%20record%20having%20Primary%20List%20no%20"+PrimaryListNo+"%20in%20Land%20Inventory.%20Land%20Management%20DDA&senderid=DDASVY&peid=1201159308150125712&contentid=1607100000000189083";
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

            }
            catch (Exception e)
            {

            }

        }


        public void GenerateSendSMSForSaveEncroachmentRegistration(string RefNo, string Mobile)
        {

            string url = "https://gateway.leewaysoftech.com/xml-transconnect-api.php?username=Ddauth&password=m1kw6vu2&mobile=" + Mobile + "&message=Your%20inspection%20request%20containing%20inspection%20report%20vide%20Reference%20no%20"+RefNo+ "%20has%20been%20successfully%20submitted%20and%20forwarded%20for%20approval%20to%20your%20reporting%20officer.%20Land%20Management%20DDA%0A&senderid=DDASVY&peid=1201159308150125712&contentid=1607100000000189081";
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

            }
            catch (Exception e)
            {

            }

        }
        public void GenerateSendSMSForSaveDemolation(string RefNo, string Mobile)
        {

            string url = "https://gateway.leewaysoftech.com/xml-transconnect-api.php?username=Ddauth&password=m1kw6vu2&mobile=" + Mobile + "&message=Your%20encroachment%20removal%2Fdemolition%20request%20vide%20Reference%20no%20"+RefNo+ "%20has%20been%20successfully%20submitted%20and%20forwarded%20for%20approval%20to%20your%20reporting%20officer.%20Land%20Management%20DDA&senderid=DDASVY&peid=1201159308150125712&contentid=1607100000000189087";
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

            }
            catch (Exception e)
            {

            }

        }
    }
}
