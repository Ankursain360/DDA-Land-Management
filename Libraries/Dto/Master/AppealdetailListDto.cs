

using Dto.Common;

namespace Dto.Master
{
    public class AppealdetailListDto
    {
        public int Id { get; set; }
        public string DemandListNo { get; set; }
        public string ENMSNO { get; set; }
        public string AppealNo { get; set; }
        public string AppealByDepartment { get; set; }
        public string DateOfAppeal { get; set; }
        public string PanelLawyer { get; set; }
        public string Status { get; set; }
    }
}
