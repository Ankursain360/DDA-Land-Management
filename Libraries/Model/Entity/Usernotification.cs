using Dto.Master;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Usernotification : AuditableEntity<int>
    {
        public string Message { get; set; }
        public string IsSeen { get; set; }
        public string UserNotificationGuid { get; set; }
        public string ProcessGuid { get; set; }
        public int ServiceId { get; set; }
        public string SendFrom { get; set; }
        public string SendTo { get; set; }
        public DateTime? SeenDateTime { get; set; }
    }
}
