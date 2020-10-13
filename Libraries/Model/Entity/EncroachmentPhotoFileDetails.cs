using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class EncroachmentPhotoFileDetails:AuditableEntity<int>
    {
        public int EncroachmentRegistrationId { get; set; }
        public string PhotoFilePath { get; set; }
        public byte IsActive { get; set; }
        public virtual EncroachmentRegisteration EncroachmentRegistration { get; set; }
    }
}
