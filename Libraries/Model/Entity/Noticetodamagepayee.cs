using Libraries.Model.Common;
using System;

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

    }
}
