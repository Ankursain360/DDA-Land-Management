using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public  class Premiumrate : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Property Type is mandatory", AllowEmptyStrings = false)]
        public int LeasePurposesTypeId { get; set; }
        public int LeaseSubPurposeId { get; set; }

        [Required(ErrorMessage = " Premium Rate is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Premium Rate; Max 18 digits")]

        public decimal PremiumRate { get; set; }
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
        [NotMapped]
        public DateTime AllotmentDate { get; set; }
        public Leasepurpose LeasePurposesType { get; set; }
        public Leasesubpurpose LeaseSubPurpose { get; set; }
    }
}
