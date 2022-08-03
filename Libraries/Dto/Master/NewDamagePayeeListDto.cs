using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class NewDamagePayeeListDto
    {
        public string FileNo { get; set; }
        public string DamagePayee { get; set; }
        public string CurrentOccupant { get; set; }
        public string TypeOfUse { get; set; }
        public decimal? TotalConstructedArea { get; set; }
        public string HouseNo { get; set; }
        public string RevenueEstate { get; set; }
        public string Colony { get; set; }
    }
}
