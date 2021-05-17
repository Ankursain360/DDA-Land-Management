using Dto.Common;
using System;

namespace Dto.Search
{
    public class DemandCollectionLedgerListDataDto
    {
        public int Id { get; set; }
        public string FileNo { get; set; }
        public string PropertyNo { get; set; }
        public string LocalityName { get; set; }
        public string PayeeName { get; set; }
        public string DemandAmount { get; set; }
        public string PaidAmount { get; set; }
        public string DemandNo { get; set; }
        public DateTime DemandDate { get; set; }
    }
}