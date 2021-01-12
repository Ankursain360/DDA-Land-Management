using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DamagePayee.Models
{
    public class GenerateMailUser
    {
        public void GenerateMailFormatForUserDetails(string DisplayName, string EmailID, string LoginName, string path, string Password,string ContactNo)
        {
            string body = this.PopulateBodyForUserDetails(DisplayName, EmailID,LoginName, path, Password, ContactNo);
            string Sub = "Damage Payee User Details ";
            this.sendMail(EmailID, Sub, body);
        }

        private string PopulateBodyForUserDetails(string DisplayName, string EmailID, string LoginName,  string Path,string Password, string ContactNo)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{LoginName}", LoginName);
            body = body.Replace("{Password}", Password);
            body = body.Replace("{EmailID}", EmailID);
            body = body.Replace("{Password}", Password);
            body = body.Replace("{PhoneNumber}", ContactNo);
            

            return body;
        }

        private void sendMail(string EmailID, string Subject, string Body)
        {
            using (MailMessage msg = new MailMessage())
            try
            {
                msg.From = new MailAddress("isharana24@gmail.com");
                if (EmailID != "" || EmailID != string.Empty)
                {
                    msg.To.Add(EmailID);
                    msg.Body = Body;
                    msg.IsBodyHtml = true;
                    msg.Subject = Subject;
                    SmtpClient smt = new SmtpClient("smtp.gmail.com");
                    smt.Port = 587;
                    smt.UseDefaultCredentials = false;
                    smt.Credentials = new NetworkCredential("isharana24@gmail.com", "Suth@1990");
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
