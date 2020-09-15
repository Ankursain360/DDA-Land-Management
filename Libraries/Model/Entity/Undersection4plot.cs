using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public  class Undersection4plot: AuditableEntity<int>
    {
      
        public int? UnderSection4Id { get; set; }
        public int? VillageId { get; set; }
        public int? KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }

        [NotMapped]
        public List<Undersection4> NotificationList { get; set; }
        public virtual Purpose Notification { get; set; }



    }
}
