using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Deletedproperty : AuditableEntity<int>
    {
        public int PropertyRegistrationId { get; set; }
        public string Reason { get; set; }
        public byte IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public Propertyregistration PropertyRegistration { get; set; }
    }
}
