using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Courtcasesmapping : AuditableEntity<int>
    {
        public int VillageId { get; set; }
        public int KhasraNoId { get; set; }
        public int LegalManagementId { get; set; }
        public byte IsActive { get; set; }

        public Khasra KhasraNo { get; set; }
        public Legalmanagementsystem LegalManagement { get; set; }
        public Acquiredlandvillage Village { get; set; }
    }
}
