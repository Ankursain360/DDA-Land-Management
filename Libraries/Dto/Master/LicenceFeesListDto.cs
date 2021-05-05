using Dto.Common;

namespace Dto.Master
{
   public class LicenceFeesListDto
    {
        public int Id { get; set; }

     

        public string LeaseSubPurpose { get; set; }

        public string LicenceFees { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Status { get; set; }
    }
}
