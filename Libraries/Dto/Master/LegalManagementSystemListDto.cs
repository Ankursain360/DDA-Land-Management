using Dto.Common;

namespace Dto.Master
{
   public class LegalManagementSystemListDto
    {
        public int Id { get; set; }
        public string FileNo { get; set; }
        public string CourtCaseNo { get; set; }
        public string COC { get; set; }
        public string Zone { get; set; }

        public string Village  { get; set; }

        public string StayinterimGranted { get; set; }
        public string Jugement { get; set; }
        public string IsActive { get; set; }
    }
}
