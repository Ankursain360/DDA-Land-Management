using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class GISKhasraDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }
}
