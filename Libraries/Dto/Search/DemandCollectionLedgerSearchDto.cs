using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DemandCollectionLedgerSearchDto : BaseSearchDto
    {
        public string FileNo { get; set; }
        public int Locality { get; set; }
        public string PropertyNo { get; set; }
    }
}
