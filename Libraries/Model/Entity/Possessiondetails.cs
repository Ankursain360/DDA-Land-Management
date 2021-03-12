using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Possessiondetails : AuditableEntity<int>
    {

        [Required(ErrorMessage = "Village name is mandatory", AllowEmptyStrings = false)]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = "Khasra No name is mandatory", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = "Type of Possession name is mandatory")]
        public string PossType { get; set; }
        [Required(ErrorMessage = "Reason is mandatory")]
        public string ReasonNonPoss { get; set; }
        [Required(ErrorMessage = "Date is mandatory")]
        public DateTime PossDate { get; set; }
        public string PlotNo { get; set; }
        
        [Required(ErrorMessage = " Bigha is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal Bigha { get; set; }
        [Required(ErrorMessage = " Biswa is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal Biswa { get; set; }
        [Required(ErrorMessage = " Biswanshi is mandatory")]

       
        public string Remarks { get; set; }
  
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public Khasra Khasra { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public Acquiredlandvillage Village { get; set; }
    }
}
