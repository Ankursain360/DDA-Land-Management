using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EncroachmentDemolition.Models;

namespace EncroachmentDemolition.Models
{
    public class GenerateMailOTP
    {
        public void GenerateMailFormatForPassword(string DisplayName, String EmailID, String LoginName, string Password, string path, string Action)
        {

            string link = "Now You Can Login";
            string body = this.PopulateBodyForPassword(DisplayName, EmailID, LoginName, Password, link, path, Action);
            string Sub = "User Login Details ";

            this.sendMail(EmailID, Sub, body, link);

        }

        public void GenerateMailFormatForComplaint(string DisplayName, String EmailID,  string Action)
        {

           
            string body = Action;
            string Sub = "Complain Register ";
            string link = "";

            this.sendMail(EmailID, Sub, body, link);

        }

        private string PopulateBodyForPassword(string DisplayName, String EmailID, String LoginName, string Password, string Link, string Path, string Action)
        {
            string body = string.Empty; 
            using (StreamReader reader = new StreamReader(Path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{LoginName}", LoginName);
            body = body.Replace("{Password}", Password);
            body = body.Replace("{Link}", Link);
            body = body.Replace("{Action}", Action);


            return body;
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
