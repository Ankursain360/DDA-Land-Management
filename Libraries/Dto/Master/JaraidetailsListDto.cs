

using Dto.Common;

namespace Dto.Master
{
    public class JaraidetailsListDto
    {
        
        public int Id { get; set; }
        public string VillageName { get; set; }
        public string KhasraNo { get; set; }
        public int? YearOfjamabandi { get; set; }
        public int? NoOfKhewat { get; set; }
        public int? NoOfKhatauni { get; set; }
        public string NaamPatti { get; set; }
        public string Status { get; set; }
    }
}
