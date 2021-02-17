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
        [Required]
        public int? UnderSection4Id { get; set; }
        [Required]
        public int? VillageId { get; set; }
        [Required]
        public int? KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }


        [NotMapped]
        public List<Undersection4> NotificationList { get; set; }
        public virtual Undersection4 UnderSection4 { get; set; }


        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }



       [NotMapped]
       public List<Khasra> KhasraList { get; set; }
       public virtual Khasra Khasra { get; set; }






    }
}
