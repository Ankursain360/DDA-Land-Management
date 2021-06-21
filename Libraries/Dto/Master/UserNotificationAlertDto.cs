using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class UserNotificationAlertDto
    {
        public int TotalNotification { get; set; }
        public int User { get; set; }
        public string ProcessGuid { get; set; }
        public string Message { get; set; }
        public string SentFrom { get; set; }
        public string IsSeen { get; set; }
        public string SubmitDate { get; set; }
        public string URL { get; set; }


    }
}
