using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Muncipality : AuditableEntity<int>
    {
        //public Muncipality()
        //{
        //    Newlandidentificationannx1 = new HashSet<Newlandidentificationannx1>();
        //}

       
        public string Name { get; set; }   
        
        public byte IsActive { get; set; }

        //public ICollection<Newlandidentificationannx1> Newlandidentificationannx1 { get; set; }
    }
}
