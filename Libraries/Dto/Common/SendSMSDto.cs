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
            string url = "http://sms.justclicksky.com/pushsms.php?username=GNIDA&api_password=242f38iadg4voobux&sender=VCSSMS&to=" + Mobile + "&message= " + Message + " Thank you .&priority=11";

            WebRequest request = WebRequest.Create(url);


        }

    }
}
