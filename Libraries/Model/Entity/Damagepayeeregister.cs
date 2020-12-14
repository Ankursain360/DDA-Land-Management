using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public  class Damagepayeeregister : AuditableEntity<int>
    {
        public Damagepayeeregister()
        {
            Allottetype = new HashSet<Allottetype>();
            Damagepayeepersonelinfo = new HashSet<Damagepayeepersonelinfo>();
            Damagepaymenthistory = new HashSet<Damagepaymenthistory>();
        }

       
        public int? FileNo { get; set; }
        public string TypeOfDamageAssessee { get; set; }
        public string PropertyNo { get; set; }
        public int? LocalityId { get; set; }
        public string FloorNo { get; set; }
        public string StreetNo { get; set; }
        public string PinCode { get; set; }
        public int? DistrictId { get; set; }
        public decimal? PlotAreaSqYard { get; set; }
        public decimal? FloorAreaSqYard { get; set; }
        public decimal? PlotAreaSqMt { get; set; }
        public decimal? FloorAreaSqMt { get; set; }
        public string UseOfProperty { get; set; }
        public decimal? ResidentialSqYard { get; set; }
        public decimal? ResidentialSqMt { get; set; }
        public decimal? CommercialSqYard { get; set; }
        public decimal? CommercialSqMt { get; set; }
        public string LitigationStatus { get; set; }
        public string CourtName { get; set; }
        public string CaseNo { get; set; }
        public string OppositionName { get; set; }
        public string PetitionerRespondent { get; set; }
        public string IsDdadamagePayee { get; set; }
        public int? DamageProperty { get; set; }
        public DateTime? EncroachmentDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? OnlinePaymentLocalityId { get; set; }
        public decimal? OnlinePaymentAreaSqYds { get; set; }
       
        public byte? IsActive { get; set; }

        public District District { get; set; }
        [NotMapped]
        public List<District> DistrictList { get; set; }
        public Locality Locality { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        public Locality OnlinePaymentLocality { get; set; }
        public ICollection<Allottetype> Allottetype { get; set; }
        public ICollection<Damagepayeepersonelinfo> Damagepayeepersonelinfo { get; set; }
        public ICollection<Damagepaymenthistory> Damagepaymenthistory { get; set; }


        //****** ALLOTTE TYPE *****

        [NotMapped]
       
        public List <string> Name { get; set; }
        [NotMapped]
        public List <string> FatherName { get; set; }
        [NotMapped]
        public List <DateTime?> Date { get; set; }
      
        [NotMapped]
        public List <IFormFile> ATSGPA { get; set; }

        //****** Damage payee personal info *****
        [NotMapped]
        public List<string> payeeName { get; set; }
        [NotMapped]
        public List <string> payeeFatherName { get; set; }
        [NotMapped]
        public List <string> Gender { get; set; }
        [NotMapped]
        public List <string> Address { get; set; }
        [NotMapped]
        public List <string> MobileNo { get; set; }
        [NotMapped]
        public List <string> EmailId { get; set; }


        //****** Damagepaymenthistory ***
        [NotMapped]
        public List <string> PaymntName { get; set; }
        [NotMapped]
        public List <string> RecieptNo { get; set; }
        [NotMapped]
        public List <string> PaymentMode { get; set; }
        [NotMapped]
        public List <DateTime?> PaymentDate { get; set; }
        [NotMapped]
        public List <decimal?> Amount { get; set; }
       
       
        [NotMapped]
        public List<IFormFile> Reciept { get; set; }
       

    }
}
