using Dto.Master;
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
        #region Mail 
        public string PopulateBody(RegisterationBodyDTO element)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(element.path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", element.displayName);
            body = body.Replace("{LoginName}", element.loginName);
            body = body.Replace("{Password}", element.password);
            body = body.Replace("{Link}", element.link);
            body = body.Replace("{Action}", element.action);


            return body;
        }
        public bool SendMailWithAttachment(string strMailSubject, string strBodyMsg, string strMailTo, string strMailCC, string strMailBCC, string strAttachPath)
        {
            bool result = false;
            MailMessage message = null;
            try
            {
                message = new MailMessage();
                message.From = new MailAddress(AppConstantsHelper.fromMail);
                message.IsBodyHtml = true;
                message.Body = strBodyMsg;
                message.Subject = strMailSubject;
                SmtpClient smtp = new SmtpClient(AppConstantsHelper.mailHost);
                smtp.Port = AppConstantsHelper.port;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(AppConstantsHelper.fromMail, AppConstantsHelper.fromMailPwd);

                //recipients To
                if (strMailTo != "")
                {
                    string[] multiTo = strMailTo.Split(',');
                    foreach (string MultiEmailID in multiTo)
                    {
                        message.To.Add(new MailAddress(MultiEmailID));
                    }
                }
                //recipients CC
                if (strMailCC != "")
                {
                    string[] multiCC = strMailCC.Split(',');
                    foreach (string MultiEmailID in multiCC)
                    {
                        message.CC.Add(new MailAddress(strMailCC));
                    }
                }
                //recipients BCC
                if (strMailBCC != "")
                {
                    string[] multiBCC = strMailBCC.Split(',');
                    foreach (string MultiEmailID in multiBCC)
                    {
                        message.Bcc.Add(new MailAddress(strMailBCC));
                    }
                }
                if (strAttachPath != "")
                {
                    Attachment attachment = new System.Net.Mail.Attachment(strAttachPath);
                    message.Attachments.Add(attachment);
                }

                smtp.EnableSsl = true;
                smtp.Send(message);

                result = true;

                return result;

            }
            catch (Exception ex)
            {
                return result;
            }
            finally
            {
                message.Dispose();
            }
        }

        public bool SendMailWithAttachment(SentMailGenerationDto maildto)
        {
            bool result = false;
            MailMessage message = null;
            try
            {
                message = new MailMessage();
                message.From = new MailAddress(maildto.fromMail);
                message.IsBodyHtml = true;
                message.Body = maildto.strBodyMsg;
                message.Subject = maildto.strMailSubject;
                SmtpClient smtp = new SmtpClient(maildto.mailHost);
                smtp.Port = maildto.port;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(maildto.fromMail, maildto.fromMailPwd);

                //recipients To
                if (maildto.strMailTo != "")
                {
                    string[] multiTo = maildto.strMailTo.Split(',');
                    foreach (string MultiEmailID in multiTo)
                    {
                        message.To.Add(new MailAddress(MultiEmailID));
                    }
                }
                //recipients CC
                if (maildto.strMailCC != "")
                {
                    string[] multiCC = maildto.strMailCC.Split(',');
                    foreach (string MultiEmailID in multiCC)
                    {
                        message.CC.Add(new MailAddress(maildto.strMailCC));
                    }
                }
                //recipients BCC
                if (maildto.strMailBCC != "")
                {
                    string[] multiBCC = maildto.strMailBCC.Split(',');
                    foreach (string MultiEmailID in multiBCC)
                    {
                        message.Bcc.Add(new MailAddress(maildto.strMailBCC));
                    }
                }
                if (maildto.strAttachPath != "")
                {
                    Attachment attachment = new System.Net.Mail.Attachment(maildto.strAttachPath);
                    message.Attachments.Add(attachment);
                }

                smtp.EnableSsl = true;
                smtp.Send(message);

                result = true;

                return result;

            }
            catch (Exception ex)
            {
                return result;
            }
            finally
            {
                message.Dispose();
            }
        }

        public string PopulateBodyRegister(DamageRegisterBodyDTO bodyDTO)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(bodyDTO.path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", bodyDTO.displayName);
            body = body.Replace("{LoginName}", bodyDTO.loginName);
            body = body.Replace("{Password}", bodyDTO.password);
            body = body.Replace("{EmailID}", bodyDTO.emailId);
            body = body.Replace("{PhoneNumber}", bodyDTO.contactNo);
            body = body.Replace("{Link}", bodyDTO.Link);


            return body;
        }

        public string PopulateBodyLeaseRefernceNo(LeaseRefBodyDto element)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(element.path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", element.displayName);
            body = body.Replace("{RefNo}", element.RefNo);
            body = body.Replace("{Link}", element.link);


            return body;
        }


        public string PopulateBodyApprovalMailDetails(ApprovalMailBodyDto element)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(element.path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ApplicationName}", element.ApplicationName);
            body = body.Replace("{AppRefNo}", element.AppRefNo);
            body = body.Replace("{SubmitDate}", element.SubmitDate);
            body = body.Replace("{SenderName}", element.SenderName);
            body = body.Replace("{Remarks}", element.Remarks);
            body = body.Replace("{Status}", element.Status);
            body = body.Replace("{Link}", element.Link);


            return body;
        }

        public string GenerateMailFormatForComplaint(ComplaintRegisteredMailBodyDto element)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(element.path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", element.DisplayName);
            body = body.Replace("{complainttype}", element.complainttype);
            body = body.Replace("{ReferenceNo}", element.ReferenceNo);
            return body;
        }

        #endregion

        #region SMS
        public void SendSMS(string Message, string Mobile)
        {
            string url = "http://sms.justclicksky.com/pushsms.php?username=GNIDA&api_password=242f38iadg4voobux&sender=VCSSMS&to=" + Mobile + "&message= " + Message + " Thank you .&priority=11";
            WebRequest request = WebRequest.Create(url);
        }
        #endregion
    }
}
