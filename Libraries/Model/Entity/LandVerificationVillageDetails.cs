﻿using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class LandVerificationVillageDetails :AuditableEntity<int>
    {
        public string villageName { get; set; }
        public string khasra_No { get; set; }
        public decimal? Bhigha { get; set; }
        public decimal? Biswa { get; set; } 
        public decimal? Biswana { get; set; }
        public string notification_s_US_4 { get; set; }
        public string notification_s_US_6 { get; set; }
        public string notification_s_US_17 { get; set; }
        public string notification_s_US_22 { get; set; }
        public int? LandVerificationSignatureId { get; set; }
        public int? IsActive { get; set; }
        public LandVerificationSignatureData GetLandVerificationSignature {  get; set; }

    }
}
