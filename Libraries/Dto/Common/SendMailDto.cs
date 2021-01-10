using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Dto.Common
{
  public  class SendMailDto
    {

        public void GenerateMailFormatForComplaint(string DisplayName, String EmailID, string Action)
        {


            string body = Action;
            string Sub = "Complain Register ";
            string link = "";

            this.sendMail(EmailID, Sub, body, link);

        }




        private void sendMail(string EmailID, string Subject, string Body, string link)
        {
            MailMessage msg = new MailMessage();
            try
            {
               msg.From = new MailAddress("vedangofficeserver@gmail.com");
            
                if (EmailID != "" || EmailID != string.Empty)
                {
                    msg.To.Add(EmailID);
                    msg.Body = Body;
                    msg.IsBodyHtml = true;
                    msg.Subject = Subject;
                    SmtpClient smt = new SmtpClient("smtp.gmail.com");
                    smt.Port = 587;
                    smt.Credentials = new NetworkCredential("vedangofficeserver@gmail.com", "Vedang@1234");
                  
                    smt.EnableSsl = true;
                    smt.Send(msg);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {

                msg.Dispose();
            }
        }





    }
}
