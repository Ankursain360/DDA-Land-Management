
using Dto.Master;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Judgementstatus : AuditableEntity<int>
    {
        public Judgementstatus()
        {
            Judgement = new HashSet<Judgement>();
        }

       
        public string Status { get; set; }
        public byte? IsActive { get; set; }
       

        public ICollection<Judgement> Judgement { get; set; }
    }
}
