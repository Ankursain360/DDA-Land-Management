using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Bundle : AuditableEntity<int>
    {
        public Bundle()
        {
            Datastoragedetails = new HashSet<Datastoragedetails>();
        }

        [Required(ErrorMessage = "Bundle No is Mandatory Field")]
        public string BundleNo { get; set; }
        public byte? IsActive { get; set; }
        public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
    }
}
