using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class EncroachmentLocationMapFileDetails:AuditableEntity<int>
    {
        public int EncroachmentRegistrationId { get; set; }
        public string LocationMapFilePath { get; set; }
        public byte IsActive { get; set; }
        public virtual EncroachmentRegisteration EncroachmentRegistration { get; set; }
    }
}
