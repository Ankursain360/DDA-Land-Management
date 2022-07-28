using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Newdamagepayeeoccupantinfo : AuditableEntity<int>
    {
        public int NewDamageSelfAssessmentId { get; set; }

        //public string LatestAtsname { get; set; }
        //public string LatestGpaname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SpouseName { get; set; }
        public string FatherName { get; set; }
        public string MontherName { get; set; }
        public string Epicid { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string AadharNo { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string PanNo { get; set; }
        public string ShareInProperty { get; set; }
        public string IsOccupingFloor { get; set; }
        public string FloorNo { get; set; }
        public string DamagePaidInPast { get; set; }
        public string OccupantPhotoPath { get; set; }
       // public string GpafilePath { get; set; }
       // public string AtsfilePath { get; set; }
        public byte? IsActive { get; set; }

        public Newdamagepayeeregistration NewDamageSelfAssessment { get; set; }
    }
}
