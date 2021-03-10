using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class GISDtoProfile
    {
        public int ZoneId { get; set; }
        public int VillageId { get; set; }
        public int KhasraId { get; set; }

    }
}
