//using Libraries.Model.Common;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;


//namespace Libraries.Model.Entity
//{
//    public class Newlandidentificationkhasradetailsannx1 :AuditableEntity<int>
//    {
//        public Newlandidentificationkhasradetailsannx1()
//        {
//            Newlandidentificationannx1 = new HashSet<Newlandidentificationannx1>();
//        }

//        public int Identificationid { get; set; }
//        [Required(ErrorMessage = "Khasra No is Mandatory Field")]
//        public string KhasaNo { get; set; }

//        [Required(ErrorMessage = "Area in Bigha is Mandatory Field")]
//        public decimal AreaBigha { get; set; }

//        [Required(ErrorMessage = "Area in Biswa is Mandatory Field")]
//        public decimal AreaBiswa { get; set; }

//        [Required(ErrorMessage = "Area in Biswani is Mandatory Field")]
//        public decimal AreaBiswani { get; set; }

//        [Required(ErrorMessage = "Ownership Status is Mandatory Field")]
//        public string OwnershipStatus { get; set; }

//        public string OwnerName { get; set; }     
        
//        public byte IsActive { get; set; }
       
//        public Newlandidentificationannx1 Identification { get; set; }
//        public ICollection<Newlandidentificationannx1> Newlandidentificationannx1 { get; set; }
//    }
//}
