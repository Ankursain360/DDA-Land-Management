using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Mutation : AuditableEntity<int>
    {
        public Mutation()
        {
            Mutationparticulars = new HashSet<Mutationparticulars>();
        }

        public int Id { get; set; }
        public int AcquiredVillageId { get; set; }
        public int KhasraId { get; set; }
        public string MutationOwnerLessee { get; set; }
        public string MutationNo { get; set; }
        public decimal MutationFees { get; set; }
        public DateTime MutationDate { get; set; }
        public string NewAccountCode { get; set; }
        public string JaraiSakniCode { get; set; }
        public string MutationType { get; set; }
        public string Remark { get; set; }
        public byte IsActive { get; set; }

        public Acquiredlandvillage AcquiredVillage { get; set; }
        public Khasra Khasra { get; set; }
        public ICollection<Mutationparticulars> Mutationparticulars { get; set; }
    }
}