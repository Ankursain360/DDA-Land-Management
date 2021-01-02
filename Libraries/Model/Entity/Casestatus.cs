using Libraries.Model.Common;
using System;
using System.Collections.Generic;


namespace Libraries.Model.Entity
{
    public partial class Casestatus : AuditableEntity<int>
   
    {
        public Casestatus()
        {
            Legalmanagementsystem = new HashSet<Legalmanagementsystem>();
        }

       
        public string CaseStatus { get; set; }
        public byte? IsActive { get; set; }
       

        public ICollection<Legalmanagementsystem> Legalmanagementsystem { get; set; }
    }
}
