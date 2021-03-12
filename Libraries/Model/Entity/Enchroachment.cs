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
            Enchroachmentpayment = new HashSet<Enchroachmentpayment>();
            EncrocherPeople = new HashSet<EncrocherPeople>();
        }


        public int VillageId { get; set; }
        public int KhasraId { get; set; }
      
        public string LandUse { get; set; }
      
        public DateTime? DateofDetection { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = " Bigha is mandatory")]
        public decimal Bigha { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Biswa; Max 18 digits")]
        [Required(ErrorMessage = " Biswa is mandatory")]

        public decimal Biswa { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Biswanshi; Max 18 digits")]
        [Required(ErrorMessage = " Biswanshi is mandatory")]
        public decimal Biswanshi { get; set; }
        
        public int NatureofencroachmentId { get; set; }
        [Required(ErrorMessage = " File Number is mandatory")]
        public string FileNo { get; set; }
      
        public DateTime? ActionDate { get; set; }

        public int ReasonsId { get; set; }
       
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
        public ICollection<Enchroachmentpayment> Enchroachmentpayment { get; set; }
        public ICollection<EncrocherPeople> EncrocherPeople { get; set; }
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
