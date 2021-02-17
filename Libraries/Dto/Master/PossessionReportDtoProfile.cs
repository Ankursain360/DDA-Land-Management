using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class PossessionReportDtoProfile
    {
        // public int Possession { get; set; }
        public DateTime ID { get; set; }
        public string POSSDATE { get; set; }
    }
}
