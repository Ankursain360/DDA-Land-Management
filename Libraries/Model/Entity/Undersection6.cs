using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Undersection6 : AuditableEntity<int>
    {
        public Undersection6()
        {
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
        }
        public string Number { get; set; }
        public DateTime? Ndate { get; set; }
        public byte IsActive { get; set; }
        public int? Undersection4Id { get; set; }

        public Undersection4 Undersection4 { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
        public ICollection<Undersection17> Undersection17 { get; set; }
    }
}