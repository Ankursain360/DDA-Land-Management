using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Utility.Helper
{
    public class MailSMSHelper
    {

        public IConfiguration _configuration;
        public MailSMSHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Mail 

        public void GenerateMailFormatForPassword(string DisplayName, String EmailID, String LoginName, string link, string path, string Action)
        {
            string body = this.PopulateBodyForPassword(DisplayName, EmailID, LoginName, "", link, path, Action);
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
                string SenderMailId = _configuration.GetSection("FilePaths:SenderMailId").Value.ToString();
                string SenderMailIdPassword = _configuration.GetSection("FilePaths:SenderMailIdPassword").Value.ToString();
                int port = Convert.ToInt32(_configuration.GetSection("FilePaths:Port").Value);
                msg.From = new MailAddress(SenderMailId);
                if (EmailID != "" || EmailID != string.Empty)
                {
                    msg.To.Add(EmailID);
                    msg.Body = Body;
                    msg.IsBodyHtml = true;
                    msg.Subject = Subject;
                    SmtpClient smt = new SmtpClient("smtp.gmail.com");
                    smt.Port = port;
                    smt.UseDefaultCredentials = false;
                    smt.Credentials = new NetworkCredential(SenderMailId, SenderMailIdPassword);
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

        #endregion
    }
}
