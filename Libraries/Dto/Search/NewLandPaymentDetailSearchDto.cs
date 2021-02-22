using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class NewLandPaymentDetailSearchDto : BaseSearchDto
    {
        public string demandListNo { get; set; }

        public string bankName { get; set; }
        public string chequeNo { get; set; }

    }
}

