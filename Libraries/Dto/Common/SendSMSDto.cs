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
            string url = "https://api.authkey.io/request?authkey=9ebb4d6454214d29&mobile="+ Mobile + "&country_code=91&sid=956&sender=PRANEE&pe_id=1201160914878702009&template_id=1207162246819654702&var=" + Message + "";



            //WebRequest request = WebRequest.Create(url);



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

    }
}
