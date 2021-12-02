using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Dto.Master
{
    public class VerifyPaymentApiStatusDto
    {
        public int CHALLAN_NO { get; set; }
        public string APPLICANT_NAME_PAYMENT { get; set; }
        public string PAYMENT_MODE { get; set; }
        public Decimal AMOUNT_RECIEVED { get; set; }
        public string PAYMENT_DATE { get; set; }
        public string BANK_TRANSACTIONID { get; set; }
        public string PG_TRANSACTIONID { get; set; }
        public string BANK { get; set; }
        public string ACCNT_NO { get; set; }
        public string FILENO { get; set; }
        public string InputFILENO { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool chkPaymentverify {get;set;}
            
    }


    public class ApiResponseVerifyPaymentApiStatus
    {
        public List<VerifyPaymentApiStatusDto> cargo { get; set; }


    }
}
