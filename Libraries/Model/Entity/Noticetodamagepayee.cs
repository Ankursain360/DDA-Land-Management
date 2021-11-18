using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Noticetodamagepayee : AuditableEntity<int>
    {
        [Required(ErrorMessage = "File No is Mandatory Field")]
        public string FileNo { get; set; }

        public DateTime GenerateDate { get; set; }
        [Required(ErrorMessage = "Name is Mandatory Field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is Mandatory Field")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Property Details is Mandatory Field")]
        public string PropertyDetails { get; set; }
        [Required(ErrorMessage = "Area field is Mandatory Field")]
        public string Area { get; set; }
        [Required(ErrorMessage = "Interest Percentage field is Mandatory Field")]
        public string InterestPercentage { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public Decimal RebatePercentage { get; set; }

        [NotMapped]
        public List<Noticetodamagepayee> FileNoList { get; set; }
        [NotMapped]
        public int? FileId { get; set; }
    }
}
