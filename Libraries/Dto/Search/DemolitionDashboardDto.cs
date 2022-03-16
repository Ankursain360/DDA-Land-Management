using Dto.Common;


namespace Dto.Search
{
    public class DemolitionDashboardDto: BaseSearchDto
    {
        public int TotalRequest { get; set; }
        public int TotalApproved { get; set; }
        public int TotalPending { get; set; }
        public int TotalRejected { get; set; } 
        public int PendingAtYou { get; set; } 
    }
}
