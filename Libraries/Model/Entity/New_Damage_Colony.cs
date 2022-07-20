using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class New_Damage_Colony : AuditableEntity<int>
    {

        public New_Damage_Colony()
        {
            NewDamageSelfAssessment = new HashSet<NewDamageSelfAssessment>();

        }
        public string Code { get; set; }
        public string Name { get; set; }
        public int NewDamageVillageId { get; set; }
        public byte IsActive { get; set; }
        public string DamageColonycol { get; set; }

        public Acquiredlandvillage NewDamageVillage { get; set; }
        public ICollection<NewDamageSelfAssessment> NewDamageSelfAssessment { get; set; }
    }
}
