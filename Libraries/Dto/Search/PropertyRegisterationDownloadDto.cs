using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PropertyRegisterationDownloadDto
    {
        public int Id { get; set; }
        public string InventoriedInId { get; set; }
        public string PlannedUnplannedLand { get; set; }
        public string ClassificationOfLand { get; set; }
        public string Department { get; set; }
        public string Zone { get; set; }
        public string Division { get; set; }
        public string PrimaryListNo { get; set; }

    }
}
