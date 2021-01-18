using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class WatchAndWardPeriodReportDtoProfile
    {
        public string name { get; set; }
        [Required(ErrorMessage = "From Date is required")]
        public DateTime? FromDate { get; set; }
        [Required(ErrorMessage = "To date is required")]
        public DateTime? ToDate { get; set; }
        [Required(ErrorMessage = "Locality is required")]
        public int localityId { get; set; }
    }
}
