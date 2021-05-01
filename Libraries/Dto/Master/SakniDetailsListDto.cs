
using Dto.Common;

namespace Dto.Master
{
    public class SakniDetailsListDto
    {
       
        public int Id { get; set; }
        public string VillageName { get; set; }
        public string KhasraNo { get; set; }
       
        public int? NoOfKhewat { get; set; }
        public int? NoOfKhatauni { get; set; }
        public string Location { get; set; }
        public string Mortgage { get; set; }
        public string RentAmount { get; set; }
        public string Status { get; set; }
    }
}
