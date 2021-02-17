using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Mutationparticulars : AuditableEntity<int>
    {
        public int MutationId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string Share { get; set; }

        public Mutation Mutation { get; set; }
    }
}
