
using Dto.Common;
using System;

namespace Dto.Master
{
    public class InsertIntoLIMSPaymentApidto 
    {
        public string CHLLN_NO { get; set; }
        public int CHLLN_AMNT { get; set; }
        public DateTime? DPST_DT { get; set; }
        public string USR_ID { get; set; }
        public string SCHM_ID { get; set; }
        public string FL_NMBR { get; set; }

    }
}
