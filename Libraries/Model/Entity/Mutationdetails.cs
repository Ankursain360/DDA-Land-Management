using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;

namespace Libraries.Model.Entity
{
    public class Mutationdetails : AuditableEntity<int>
    {
        public Mutationdetails()
        {
            Mutationnewdamageassesse = new HashSet<Mutationnewdamageassesse>();
            Mutationolddamageassesse = new HashSet<Mutationolddamageassesse>();
            Mutationdetailsphotoproperty = new HashSet<Mutationdetailsphotoproperty>();
            Mutationnewownertemp = new HashSet<Mutationnewownertemp>();
            Mutationoriginalowner = new HashSet<Mutationoriginalowner>();
            Mutationoriginalownertemp = new HashSet<Mutationoriginalownertemp>();
            Mutationnewowner = new HashSet<Mutationnewowner>();

        }

        [Required]
        public int? DamagePayeeRegisterId { get; set; }
        public string FileNo { get; set; }
        public string PropertyNo { get; set; }
        public int? LocalityId { get; set; }
        public string FloorNo { get; set; }
        public string StreetName { get; set; }
        public string PindCode { get; set; }
        public int ZoneId { get; set; }
        public decimal? PlotAreaSqYds { get; set; }
        public decimal? FloorAreaSqYds { get; set; }
        public string MutationPurpose { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string AtsfilePath { get; set; }
        public string GpafilePath { get; set; }
        public string MoneyRecieptFilePath { get; set; }
        public string SignatureSpecimenFilePath { get; set; }
        public string IsAddressProof { get; set; }
        public string AddressProofFilePath { get; set; }
        public string AffidavitFilePath { get; set; }
        public string IndemnityFilePath { get; set; }
        public string LitigationStatus { get; set; }
        public string CourtName { get; set; }
        public string CaseNo { get; set; }
        public string OppositionPartyName { get; set; }
        public string PetitionerRespondent { get; set; }
        public string Declaration1 { get; set; }
       
        public byte? IsActive { get; set; }
        public string ColonyName { get; set; }
        public int? PinCode { get; set; }
        public string InheritancePurchaser { get; set; }
        public string DeathCertificatePath { get; set; }
        public string RelationshipUploadPath { get; set; }
        public string NonObjectUploadPath { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string RelationshipOrignalOwner { get; set; }
        public DateTime? DateOrder { get; set; }
        public string HouseTaxUploadPath { get; set; }
        public string ElectricityUploadPath { get; set; }
        public string WaterUploadPath { get; set; }
        public string AnyOtherUploadPath { get; set; }
        public string Declaration2 { get; set; }
        public string Declaration3 { get; set; }
        public string AffidevitLegalUploadPath { get; set; }
        public string SpecimenSignLegalUpload { get; set; }

        
        public Locality Locality { get; set; }
        public Zone Zone { get; set; }
        public Damagepayeeregistertemp DamagePayeeRegister { get; set; }

        public ICollection<Mutationnewowner> Mutationnewowner { get; set; }
        public ICollection<Mutationnewownertemp> Mutationnewownertemp { get; set; }
        public ICollection<Mutationolddamageassesse> Mutationolddamageassesse { get; set; }
        public ICollection<Mutationoriginalowner> Mutationoriginalowner { get; set; }
        public ICollection<Mutationoriginalownertemp> Mutationoriginalownertemp { get; set; }
        public ICollection<Mutationnewdamageassesse> Mutationnewdamageassesse { get; set; }
        public ICollection<Mutationdetailsphotoproperty> Mutationdetailsphotoproperty { get; set; }

        [NotMapped]
        public IFormFile AtsfilePathNew { get; set; }
        [NotMapped]
        public IFormFile GpafilePathNew { get; set; }
        [NotMapped]
        public IFormFile MoneyfilePathNew { get; set; }
        [NotMapped]
        public IFormFile SignSpecPathNew { get; set; }
        [NotMapped]
        public IFormFile AddressProofPathNew { get; set; }
        [NotMapped]
        public IFormFile AffidavitPathNew { get; set; }
        [NotMapped]
        public IFormFile IndemnityPathNew { get; set; }
        [NotMapped]
        public List<IFormFile> PropertyPhoto { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }

        /* 1st Repeater*/
        [NotMapped]
        public List<string> Name2 { get; set; }
        [NotMapped]
        public List<string> FatherName2 { get; set; }
        [NotMapped]
        public List<DateTime> DateGpadead { get; set; }
        [NotMapped]
        public List<string> GpastafilePath { get; set; }


        /* 2nd Repeater*/
        [NotMapped]
        public List<string> NameNew { get; set; }
        [NotMapped]
        public List<string> GuardianName { get; set; }
        [NotMapped]
        public List<string> Gender { get; set; }
        [NotMapped]
        public List<string> Address { get; set; }
        [NotMapped]
        public List<string> MobileNo { get; set; }
        [NotMapped]
        public List<string> Email { get; set; }
        [NotMapped]
        public List<string> AadharNo { get; set; }
        [NotMapped]
        public List<string> PanNo { get; set; }
        [NotMapped]
        public List<IFormFile> PhotoFilePath { get; set; }
        [NotMapped]
        public List<IFormFile> SignatureFilePath { get; set; }


    }
}
