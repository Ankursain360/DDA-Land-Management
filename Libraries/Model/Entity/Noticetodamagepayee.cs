using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Noticetodamagepayee : AuditableEntity<int>
    {

        public string FileNo { get; set; }
        public DateTime GenerateDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PropertyDetails { get; set; }
        public string Area { get; set; }
        public string InterestPercentage { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public List<Noticetodamagepayee> FileNoList { get; set; }
        [NotMapped]
        public int? FileId { get; set; }
    }
}
