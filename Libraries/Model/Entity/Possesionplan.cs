using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public partial class Possesionplan : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Allotment Number is Mandatory")]
        public int AllotmentId { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid; Max 18 digits")]
        public string NorthEast { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid; Max 18 digits")]
        public string SouthEast { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid; Max 18 digits")]
        public string NorthWest { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid; Max 18 digits")]
        public string SouthWest { get; set; }
        public string SitePlanFilePath { get; set; }
        public decimal? AllotedArea { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid; Max 18 digits")]
        public decimal PossessionArea { get; set; }
        public decimal? DiffernceArea { get; set; }

        [Required(ErrorMessage = "Possession Taken Name is Mandatory")]
        public string PossessionTakenName { get; set; }
        [Required(ErrorMessage = "Possession Taken is Mandatory")]
        public DateTime? PossessionTakenDate { get; set; }
        [Required(ErrorMessage = "Possession Hand Over Name is Mandatory")]
        public string PossesionHandOverName { get; set; }
        [Required(ErrorMessage = "Possession Hand Over Date is Mandatory")]
        public DateTime? PossesionHandOverDate { get; set; }
        public string Remark { get; set; }
        public byte IsActive { get; set; }


        public Allotmententry Allotment { get; set; }
     //   public Allotmententry Allotmententry { get; set; }
        [NotMapped]
        public List<Allotmententry> AllotmententryList { get; set; }
        [NotMapped]
        public List<Leaseapplication> LeaseApplicationList { get; set; }
        
        [NotMapped]
        public IFormFile StayFile { get; set; }


    }
}
