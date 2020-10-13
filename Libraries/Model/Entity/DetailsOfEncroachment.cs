using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class DetailsOfEncroachment:AuditableEntity<int>
    {
        public int EncroachmentRegisterationId { get; set; }
        public string NameOfStructure { get; set; }
        public decimal? Area { get; set; }
        public string Type { get; set; }
        public DateTime? DateOfEncroachment { get; set; }
        public decimal? CountOfStructure { get; set; }
        public string ReferenceNoOnLocation { get; set; }
        public int? IsActive { get; set; }
        public virtual EncroachmentRegisteration EncroachmentRegisteration { get; set; }

    }
}
