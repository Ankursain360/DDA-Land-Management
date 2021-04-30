
using Dto.Common;

namespace Dto.Master
{
    public class LdolandListDto
    {
        
        public int Id { get; set; }
        public string NotificationNo { get; set; }
        public string NotificationDate { get; set; }

        public int? SerialNo { get; set; }
        public string PropertySiteNo { get; set; }
        public string LocationNameVillage { get; set; }
        public string Status { get; set; }
    }
}
