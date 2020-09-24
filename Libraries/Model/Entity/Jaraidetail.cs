using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Jaraidetail : AuditableEntity<int>
    {
       
        public int VillageId { get; set; }
        public DateTime? YearOfJamabandi { get; set; }
        public int KhewatId { get; set; }
        public int KhatauniId { get; set; }
        public int TarafId { get; set; }
        public int KhasraId { get; set; }
        public string OwnerDetails { get; set; }
        public string FarmerDetails { get; set; }
        public string Kaifiyat { get; set; }
        public string Ahwal { get; set; }
        public string Revenue { get; set; }
        public string OldMutationNo { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }


        public virtual Khewat Khewat { get; set; }
        [NotMapped]
        public List<Khewat> KhewatList { get; set; }


        [NotMapped]
        public List<Taraf> TarafList { get; set; }
        public virtual Taraf Taraf { get; set; }


        public virtual Khatauni Khatauni { get; set; }
        [NotMapped]
        public List<Khatauni> KhatauniList { get; set; }




        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }



    }
}
