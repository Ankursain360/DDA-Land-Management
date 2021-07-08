using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Groundrent : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Purpose is mandatory", AllowEmptyStrings = false)]
        public int LeasePurposesTypeId { get; set; }
        [Required(ErrorMessage = " Sub Purpose is mandatory")]
        public int LeaseSubPurposeId { get; set; }
        [Required(ErrorMessage = " Ground Rent is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Premium Rate; Max 18 digits")]

        public decimal GroundRate { get; set; }
        [Required(ErrorMessage = " From Date is mandatory")]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = " To Date is mandatory")]
        public DateTime ToDate { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }


        [NotMapped]
        public List<Leasepurpose> LeasePurposeList { get; set; }
        [NotMapped]
        public List<Leasesubpurpose> LeaseSubPurposeList { get; set; }
        public Leasepurpose LeasePurposesType { get; set; }
        public Leasesubpurpose LeaseSubPurpose { get; set; }
    }
}

