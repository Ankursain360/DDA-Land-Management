using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Libraries.Model.Entity
{
    public partial class Newlandnotificationfilepath : AuditableEntity<int>
    {

        public int NewlandNotificationId { get; set; }
        public string FilePath { get; set; }
        public byte IsActive { get; set; }

         public Newlandnotification NewlandNotification { get; set; }
    }
}
