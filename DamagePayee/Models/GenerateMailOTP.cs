using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DamagePayee.Models;

namespace DamagePayee.Models
{
    public class GenerateMailOTP
    {
        public void GenerateMailFormatForPassword(string DisplayName, String EmailID, String LoginName, string link, string path, string Action)
        {
            string body = this.PopulateBodyForPassword(DisplayName, EmailID, LoginName,"", link, path, Action);
            string Sub = "User Login Details ";

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
                msg.From = new MailAddress("shalinirai0111@gmail.com");
                if (EmailID != "" || EmailID != string.Empty)
                {
                    msg.To.Add(EmailID);
                    msg.Body = Body;
                    msg.IsBodyHtml = true;
                    msg.Subject = Subject;
                    SmtpClient smt = new SmtpClient("smtp.gmail.com");
                    smt.Port = 587;
                    smt.UseDefaultCredentials = false;
                    smt.Credentials = new NetworkCredential("shalinirai0111@gmail.com", "Gold@shal123");
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
