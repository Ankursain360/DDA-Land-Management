using Libraries.Model.Common;
using System;
using System.Collections.Generic;


namespace Libraries.Model.Entity
{
    public partial class Courttype : AuditableEntity<int>
  
    {
        public Courttype()
        {
            Legalmanagementsystem = new HashSet<Legalmanagementsystem>();
        }

      
        public string CourtType { get; set; }
        public byte? IsActive { get; set; }
      

        public ICollection<Legalmanagementsystem> Legalmanagementsystem { get; set; }
    }
}
