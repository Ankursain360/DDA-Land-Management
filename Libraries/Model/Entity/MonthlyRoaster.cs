using Dto.Master;
using Libraries.Model.Common;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class MonthlyRoaster : AuditableEntity<int>
    {
        public MonthlyRoaster()
        {
            Dailyroaster = new HashSet<DailyRoaster>();
        }
        public int DepartmentId { get; set; }
        public int ZoneId { get; set; }
        public int DivisionId { get; set; }
        public int? LocalityId { get; set; }
        public int UserprofileId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Template { get; set; }
        public byte IsActive { get; set; }
        public Department Department { get; set; }
        public Division Division { get; set; }
        public Locality Locality { get; set; }
        public Userprofile Userprofile { get; set; }
        public Zone Zone { get; set; }
        public ICollection<DailyRoaster> Dailyroaster { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Propertyregistration> PrimaryList { get; set; }
        [NotMapped]
        public List<MonthlyRoasterPartial> MonthlyRoasterPartial { get; set; }
        [NotMapped]
        public List<int> PrimaryListNo { get; set; }
        [NotMapped]
        public List<Userprofile> SecurityGuardList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Userprofile> UserprofileList { get; set; }
        [NotMapped]
        public List<Propertyregistration> PropertyregistrationList { get; set; }
        [NotMapped]
        public List<DropdownDto> YearList { get; set; }
        [NotMapped]
        public List<DropdownDto> MonthList { get; set; }
    }
}
