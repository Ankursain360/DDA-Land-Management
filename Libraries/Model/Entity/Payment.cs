using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Payment : AuditableEntity<int>
    {
        public int AllotmentId { get; set; }
        public int LeasePaymentTypeId { get; set; }
        public string RecieptNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string PaymentTransactionNo { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public decimal? InterestRate { get; set; }
        public int? NoOfDays { get; set; }
        public string Utrno { get; set; }
        public string PaymentStatus { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public byte? IsActive { get; set; }

        public Allotmententry Allotment { get; set; }
        public Leasepaymenttype LeasePaymentType { get; set; }

    }
}
