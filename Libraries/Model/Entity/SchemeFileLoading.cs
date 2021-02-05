using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
   public class SchemeFileLoading : AuditableEntity<int>
    {
        public SchemeFileLoading()
        {
            Datastoragedetails = new HashSet<Datastoragedetails>();
            Datastoragepartfilenodetails = new HashSet<Datastoragepartfilenodetails>();
        }
        public string SchemeCode { get; set; }
        public string SchemeName { get; set; }       
        public byte? IsActive { get; set; }

        public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
        public ICollection<Datastoragepartfilenodetails> Datastoragepartfilenodetails { get; set; }

    }
}
