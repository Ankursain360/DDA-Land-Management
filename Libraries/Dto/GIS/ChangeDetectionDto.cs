using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.GIS
{
    public class ChangeDetectionDto
    {         

        [Required(ErrorMessage = "First Photo is required")]
        public IFormFile FirstPhoto { get; set; }
         
        [Required(ErrorMessage = "Second Photo is required")]
        public IFormFile SecondPhoto { get; set; }

        public string ChangedImage { get; set; }

    }
}
