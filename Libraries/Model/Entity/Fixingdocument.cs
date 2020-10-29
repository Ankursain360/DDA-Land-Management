using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Fixingdocument : AuditableEntity<int>
    {


       
        public int FixingdemolitionId { get; set; }
        public int DemolitionDocumentId { get; set; }
        public string DocumentDetails { get; set; }
        public byte IsActive { get; set; }

        public Demolitiondocument DemolitionDocument { get; set; }
        public Fixingdemolition Fixingdemolition { get; set; }


    }
}
