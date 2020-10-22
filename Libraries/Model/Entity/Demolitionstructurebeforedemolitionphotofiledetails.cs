using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Libraries.Model.Entity
{
    public partial class Demolitionstructurebeforedemolitionphotofiledetails : AuditableEntity<int>
    {

        public int DemolitionStructureId { get; set; }
        public string BeforePhotoFilePath { get; set; }
        public byte IsActive { get; set; }


        public virtual Demolitionstructuredetails DemolitionStructureDetails { get; set; }
    }
}