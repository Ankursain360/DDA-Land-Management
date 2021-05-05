using Dto.Common;


namespace Dto.Master
{
    public class AllotmentEntryListDto
    {
        public int Id { get; set; }
        public string AllicationName { get; set; }
        public string TotalArea { get; set; }

        public string PlayGroundArea { get; set; }
        public string AllotmentDate { get; set; }

        public string PhaseNo { get; set; }

        public string SectorNo { get; set; }

        public string PocketNo { get; set; }

        public string PlotNo { get; set; }
        public string Status { get; set; }
    }
}
