using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Sakanidetail : AuditableEntity<int>
    {
       
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public DateTime? YearOfJamabandi { get; set; }
        public int KhewatId { get; set; }
        public string Location { get; set; }
        public string OwnerDetails { get; set; }
        public string LeaseDetails { get; set; }
        public string Tenant { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }


        public virtual Khewat Khewat { get; set; }
        [NotMapped]
        public List<Khewat> KhewatList { get; set; }

        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }




        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }



    }
}
