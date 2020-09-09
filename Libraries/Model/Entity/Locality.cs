using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Locality:AuditableEntity<int>
    {
        public int DepartmentId { get; set; }
        [Required]
        public int ZoneId { get; set; }
        [Required]
        public string LocalityName { get; set; }
        [Required]
        public string LocalityCode { get; set; }
        [Required]
        public string Landmark { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public byte IsActive { get; set; }
    }
}
