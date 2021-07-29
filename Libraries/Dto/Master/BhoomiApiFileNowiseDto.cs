using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
    public class BhoomiApiFileNowiseDto
    {
       
        public int CHLLN_NMBR { get; set; }
        public decimal CHLLN_AMNT { get; set; }
        public string DPST_DT { get; set; }
        public List<BhoomiApiFileNowiseDto> A { get; set; }
    }
    public class ApiResponseBhoomiApiFileWise
    {
        public List<BhoomiApiFileNowiseDto> cargo { get; set; }
        [NotMapped]     
        public string DemandPeriod { get; set; }
        [NotMapped]
        public string GroundRentLeaseRent { get; set; }
        [NotMapped]
        public string InterestAmount { get; set; }
        [NotMapped]
        public string TotalDues { get; set; }
        [NotMapped]
        public string FileNo { get; set; }
       
    }
}
