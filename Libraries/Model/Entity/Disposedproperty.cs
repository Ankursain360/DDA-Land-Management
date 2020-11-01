using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Disposedproperty : AuditableEntity<int>
    {
        public int PropertyRegistrationId { get; set; }
        public int? DisposalTypeId { get; set; }
        public DateTime? DisposalDate { get; set; }
        public string DisposalTypeFilePath { get; set; }
        public string DisposalComments { get; set; }
        public byte IsDisposed { get; set; }
        public int? DisposedBy { get; set; }
        public DateTime DisposedDate { get; set; }

        public Propertyregistration PropertyRegistration { get; set; }
    }
}
