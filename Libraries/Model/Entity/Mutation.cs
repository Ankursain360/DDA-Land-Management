using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace Libraries.Model.Entity
{
    public class Mutation : AuditableEntity<int>
    {
        public Mutation()
        {
            Mutationparticulars = new HashSet<Mutationparticulars>();
        }

        [Required(ErrorMessage = "Village Name is Mandatory", AllowEmptyStrings = false)]
        public int AcquiredVillageId { get; set; }

        [Required(ErrorMessage = "Khasra is Mandatory", AllowEmptyStrings = false)]
        public int KhasraId { get; set; }

        [Required(ErrorMessage = "Mutation Owner/Lessee is Mandatory")]
        public string MutationOwnerLessee { get; set; }

        [Required(ErrorMessage = "Mutation No. is Mandatory")]
        public string MutationNo { get; set; }

        [Required(ErrorMessage = "Mutation Fees is Mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Mutation Fees; Max 18 digits")]
        public decimal MutationFees { get; set; }

        [Required(ErrorMessage = "Mutation Date is Mandatory")]
        public DateTime MutationDate { get; set; }
        public string NewAccountCode { get; set; }
        public string JaraiSakniCode { get; set; }
        public string MutationType { get; set; }
        public string Remark { get; set; }
        public byte IsActive { get; set; }
        public string DocumentName { get; set; }
        public Acquiredlandvillage AcquiredVillage { get; set; }
        public Khasra Khasra { get; set; }
        public ICollection<Mutationparticulars> Mutationparticulars { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Khasra> KhasraNoList { get; set; }

        [NotMapped]
        public List<string> Name { get; set; }
        [NotMapped]
        public List<string> FatherName { get; set; }
        [NotMapped]
        public List<string> Share { get; set; }
        [NotMapped]
        public List<string> Address { get; set; }
        [NotMapped]
        public IFormFile DocumentIFormFile { get; set; }
    }
}