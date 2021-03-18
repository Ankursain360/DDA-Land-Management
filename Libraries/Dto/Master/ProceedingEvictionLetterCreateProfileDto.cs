using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class ProceedingEvictionLetterCreateProfileDto
    {
        [Required(ErrorMessage = "RefNoName is Mandatory")]
        public int RefNoNameId { get; set; }

        [Required(ErrorMessage = "Letter Reference No. is Mandatory")]
        public string LetterReferenceNo { get; set; }

        [NotMapped]
        public List<RefNoNameDto> RefNoNameList { get; set; }
    }
}
