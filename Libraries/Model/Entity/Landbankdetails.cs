﻿using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Landbankdetails : AuditableEntity<int>
    {
        public string ZoneName { get; set; }
        public string VillageName { get; set; }
        public decimal Area { get; set; }
        public int LandCategory { get; set; }
        public string NotificationNo { get; set; }
        public DateTime? NotificationDate { get; set; }
        public byte? IsActive { get; set; }
        public Classificationofland LandCategoryNavigation { get; set; }
    }
}
