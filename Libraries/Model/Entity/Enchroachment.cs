using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
namespace Libraries.Model.Entity
{
    public  class Enchroachment : AuditableEntity<int>
    {
        public Enchroachment()
        {
         //   EncrocherPeople = new HashSet<EncrocherPeople>();
        }
        
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
      //  [Required(ErrorMessage = " LandUse is mandatory")]
        public string LandUse { get; set; }
       // [Required(ErrorMessage = " Date of Detection is mandatory")]
        public DateTime? DateofDetection { get; set; }
        [Required(ErrorMessage = " Bigha is mandatory")]
        public decimal Bigha { get; set; }
        [Required(ErrorMessage = " Biswa is mandatory")]
        public decimal Biswa { get; set; }
        [Required(ErrorMessage = " Biswanshi is mandatory")]
        public decimal Biswanshi { get; set; }
        
        public int NatureofencroachmentId { get; set; }
        [Required(ErrorMessage = " File Number is mandatory")]
        public string FileNo { get; set; }
      //  [Required(ErrorMessage = " Actions Date is mandatory")]
        public DateTime? ActionDate { get; set; }

        public int ReasonsId { get; set; }
       // [Required(ErrorMessage = " Damage Area is mandatory")]
        public string DamageArea { get; set; }

        public string ActionRemarks { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Payment { get; set; }
        public string PaymentAddress { get; set; }
        public byte IsActive { get; set; }
        public string RecStatus { get; set; } 
        public string ActionTaken { get; set; }

        [NotMapped]
        public List<Natureofencroachment> NencroachmentList { get; set; }
        public virtual Natureofencroachment Natureofencroachment { get; set; }


        [NotMapped]
        public List<Reasons> ReasonsList { get; set; }
        public virtual Reasons Reasons { get; set; }





        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }




        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }
        //-------enchroacher Name--------
    //    public ICollection<EncrocherPeople> EncrocherPeople { get; set; }
        
        [NotMapped]
        public List<string> EName { get; set; }
              
        [NotMapped]
        public List<string> EAddress { get; set; }
        [NotMapped]
        public List<string> Amount { get; set; }
        [NotMapped]
        public List<string> ChequeNo { get; set; }
        [NotMapped]
        public List<string> ChequeDate { get; set; }
    }
}
