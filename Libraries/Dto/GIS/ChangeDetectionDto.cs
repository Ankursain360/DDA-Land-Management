using Dto.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.GIS
{
    public class ChangeDetectionDto : BaseSearchDto
    {

        [Required(ErrorMessage = "First Photo is required")]
        public IFormFile FirstPhoto { get; set; }

        [Required(ErrorMessage = "Second Photo is required")]
        public IFormFile SecondPhoto { get; set; }

        public string ChangedImage { get; set; }
        public string ChangedImagePath { get; set; }
        public int? ZoneId { get; set; }

        public int? VillageID { get; set; }

        public string FirstPhotoPath { get; set; }

        public string SecondPhotoPath { get; set; }

        public string Similarity { get; set; }

        public string FirstImageResoultion { get; set; }

        public string SecondImageResoultion { get; set; }
        public int CreatedBy { get; set; }
    }
}
