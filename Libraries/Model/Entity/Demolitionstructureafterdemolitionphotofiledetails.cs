using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Libraries.Model.Entity
{
    public partial class Demolitionstructureafterdemolitionphotofiledetails : AuditableEntity<int>
    {
      
        public int DemolitionStructureDetailsId { get; set; }
        public string AfterPhotoFilePath { get; set; }
        public byte IsActive { get; set; }
      

        public virtual Demolitionstructuredetails DemolitionStructureDetails { get; set; }
    }
}
