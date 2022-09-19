using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
    public class Classificationofland : AuditableEntity<int>
    {
        public Classificationofland()
        {
            Propertyregistration = new HashSet<Propertyregistration>();
            Landbankdetails = new HashSet<Landbankdetails>();
        }
        [Required(ErrorMessage = " Classification of Land Name is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte IsActive { get; set; }
        public ICollection<Propertyregistration> Propertyregistration { get; set; }
        public ICollection<Landbankdetails> Landbankdetails { get; set; }
    }
}
