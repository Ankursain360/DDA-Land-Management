using Dto.Common;

namespace Dto.Master
{
  public  class NewLandAppealDetailListDto
    {
           public int Id { get; set; }
        public string DemandListNo { get; set; }
        public string EnmSno { get; set; }
        public string Appealno { get; set; }
        public string AppealByDepartment { get; set; }
        public string DateOfApproval { get; set; }
        public string PanelLawyer { get; set; }
     

        public string IsActive { get; set; }
    }
}
