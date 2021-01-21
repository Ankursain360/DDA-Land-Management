using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Noticetodamagepayee : AuditableEntity<int>
    {
        [Required(ErrorMessage = " The File No field is required")]
        public string FileNo { get; set; }

        public DateTime GenerateDate { get; set; }
        [Required(ErrorMessage = " The Name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = " The Address field is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = " The Property Details field is required")]
        public string PropertyDetails { get; set; }
        [Required(ErrorMessage = " The Area field is required")]
        public string Area { get; set; }
        [Required(ErrorMessage = " The Interest Percentage field is required")]
        public string InterestPercentage { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public List<Noticetodamagepayee> FileNoList { get; set; }
        [NotMapped]
        public int? FileId { get; set; }
    }
}
