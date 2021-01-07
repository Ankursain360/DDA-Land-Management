using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Mutationdetailstemp : AuditableEntity<int>
    {
        public int? DamagePayeeRegisterId { get; set; }
        public string FileNo { get; set; }
        public string MutationPurpose { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string AtsfilePath { get; set; }
        public string GpafilePath { get; set; }
        public string MoneyRecieptFilePath { get; set; }
        public string SignatureSpecimenFilePath { get; set; }
        public string DeathCertificatePath { get; set; }
        public string RelationshipUploadPath { get; set; }
        public string AffidevitLegalUploadPath { get; set; }
        public string NonObjectHeirUploadPath { get; set; }
        public string SpecimenSignLegalUpload { get; set; }
        public string IsAddressProof { get; set; }
        public string AddressProofFilePath { get; set; }
        public string AffidavitFilePath { get; set; }
        public string IndemnityFilePath { get; set; }
        public string Declaration1 { get; set; }
        public byte? IsActive { get; set; }
        public int? ApprovedStatus { get; set; }
        public Damagepayeeregister DamagePayeeRegister { get; set; }
    }
}

