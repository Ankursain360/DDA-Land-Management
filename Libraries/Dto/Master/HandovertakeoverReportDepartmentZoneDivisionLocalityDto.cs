using Dto.Common;

namespace Dto.Master
{
  public  class HandovertakeoverReportDepartmentZoneDivisionLocalityDto
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Zone { get; set; }
        public string Division { get; set; }
        public string Locality { get; set; }
        public string KhasraNo { get; set; }
        public string HandedOverBy { get; set; }
        public string HandedOverDate { get; set; }
        public string TakenOverBy { get; set; }
        public string TakenOverDate { get; set; }
        public string TransferorderIssueAuthority { get; set; }
        public string Remarks { get; set; }

    }
}

