using Libraries.Model.Common;
using System;
using System.Collections.Generic;


namespace Libraries.Model.Entity
{
    public class Newlandus4plot : AuditableEntity<int>
    {
        
        public int NotificationId { get; set; }
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public decimal? Bigha { get; set; }
        public decimal? Biswa { get; set; }
        public decimal? Biswanshi { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
       

        //public Newlandkhasra Khasra { get; set; }
        //public Landnotification Notification { get; set; }
    }
}
