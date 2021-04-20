using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Newlandawardplotdetails : AuditableEntity<int>
    {
        public Newlandawardplotdetails()
        {
          //  Newlandawardmasterdetail = new HashSet<Newlandawardmasterdetail>();
        }

        [Required(ErrorMessage = "Award  is mandatory")]
        public int AwardMasterId { get; set; }
        [Required(ErrorMessage = "Village  is mandatory")]
        public int VillageId { get; set; }
        [Required(ErrorMessage = "Khasra No  is mandatory")]
        public int KhasraId { get; set; }
        
        public decimal Bigha { get; set; }
        
        public decimal Biswa { get; set; }
       
       
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = " Bigha is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? ABigha { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = " Biswa is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? ABiswa { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = " Biswanshi is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? ABiswanshi { get; set; }
        public virtual Newlandawardmasterdetail NewlandAwardMaster { get; set; }
        [NotMapped]
        public List<Newlandawardmasterdetail> NewlandAwardmasterList { get; set; }

        [NotMapped]
        public List<Newlandkhasra> NewlandKhasraList { get; set; }
        public virtual Newlandkhasra NewlandKhasra { get; set; }

        [NotMapped]
        public List<Newlandvillage> NewlandVillageList { get; set; }
        public virtual Newlandvillage NewlandVillage { get; set; }

       
    }
}
