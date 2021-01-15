using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class ImpositionOfChargesDtoProfile
    {
        [Required(ErrorMessage = "File No. is required")]
        public int FileNo { get; set; }

        [Required(ErrorMessage = "Locality is required")]
        public int LocalityId { get; set; }

        [Required(ErrorMessage = "From Date is required")]
        public decimal FromDate { get; set; }

        [Required(ErrorMessage = "To Date is required")]
        public decimal ToDate { get; set; }

    }
}
