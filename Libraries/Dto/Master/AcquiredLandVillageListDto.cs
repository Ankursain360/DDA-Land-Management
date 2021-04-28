

using Dto.Common;

namespace Dto.Master
{
    public class AcquiredLandVillageListDto
    {

        public int Id { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }

        public string DistrictName { get; set; }
        public string TehsilName { get; set; }

        public string ZoneName { get; set; }

        public string Acquired { get; set; }
        public string VillageType { get; set; }
        public string Status { get; set; }
    }
}
  