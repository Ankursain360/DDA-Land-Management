using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public class Newlandannexure1 : AuditableEntity<int>
    {
        public Newlandannexure1()
        {
            Newlandannexure1khasrarpt = new HashSet<Newlandannexure1khasrarpt>();
        }

       
        public string VillageName { get; set; }
        public string Address { get; set; }
        public string TalukName { get; set; }
        public int MunicipalityId { get; set; }
        public int DistrictId { get; set; }
        public string AreaUnit { get; set; }
        public decimal Area { get; set; }
        public decimal? AreaAcquiredEast { get; set; }
        public decimal? AreaAcquiredWest { get; set; }
        public decimal? AreaAcquiredNorth { get; set; }
        public decimal? AreaAcquiredSouth { get; set; }
        public decimal AgriculturalLandArea { get; set; }
        public string Reasons { get; set; }
        public string BuildingNo { get; set; }
        public string BuildingDesc { get; set; }
        public string TanksNo { get; set; }
        public string TanksDesc { get; set; }
        public string WellsNo { get; set; }
        public string WellsDesc { get; set; }
        public string TreesNo { get; set; }
        public string TreesDesc { get; set; }
        public string ReligiousBuildingNo { get; set; }
        public string ReligiousBuildingDesc { get; set; }
        public string TombNo { get; set; }
        public string TombDesc { get; set; }
        public string OthersNo { get; set; }
        public string OthersDesc { get; set; }
        public byte? IsActive { get; set; }
        public District District { get; set; }
        public Muncipality Municipality { get; set; }
        public ICollection<Newlandannexure1khasrarpt> Newlandannexure1khasrarpt { get; set; }
    }
}
