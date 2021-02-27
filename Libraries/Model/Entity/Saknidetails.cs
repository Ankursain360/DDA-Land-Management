using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public partial class Saknidetails : AuditableEntity<int>
    {
        public Saknidetails()
        {
            Saknikhasra = new HashSet<Saknikhasra>();
            Saknilessee = new HashSet<Saknilessee>();
            Sakniowner = new HashSet<Sakniowner>();
            Saknitenant = new HashSet<Saknitenant>();
        }

        [Required(ErrorMessage = " Village name is mandatory")]
        public int VillageId { get; set; }
        [Required(ErrorMessage = "Khasra No is Mandatory Field", AllowEmptyStrings = false)]
        public int KhasraId { get; set; }
        public int? YearOfjamabandi { get; set; }
        public int? NoOfKhewat { get; set; }
        public int? NoOfKhatauni { get; set; }
        public string Location { get; set; }
        public string Mortgage { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]

        public decimal? RentAmount { get; set; }
        public string OldMutationNo { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> AcquiredlandvillageList { get; set; }

        [NotMapped]
        public List<Khasra> KhasraList { get; set; }

        public Khasra Khasra { get; set; }
        public ICollection<Saknikhasra> Saknikhasra { get; set; }
        public Acquiredlandvillage Village { get; set; }
        public ICollection<Saknilessee> Saknilessee { get; set; }
        public ICollection<Sakniowner> Sakniowner { get; set; }
        public ICollection<Saknitenant> Saknitenant { get; set; }

        //****** Owner details *****

        [NotMapped]
        public List<string> OwnerName { get; set; }

        [NotMapped]
        public List<string> FatherName { get; set; }

        [NotMapped]
        public List<string> Address { get; set; }


        //****** Tenant details *****

        [NotMapped]
        public List<string> TName { get; set; }

        [NotMapped]
        public List<string> TFatherName { get; set; }

        [NotMapped]
        public List<string> TAddress { get; set; }


        //****** Lessee details *****

        [NotMapped]
        public List<string> LesseeName { get; set; }

        [NotMapped]
        public List<string> LFather { get; set; }

        [NotMapped]
        public List<string> LAddress { get; set; }
        [NotMapped]
        public List<string> LShare { get; set; }

        [NotMapped]
        public List<string> LMortgage { get; set; }

        //****** Khasra details *****

        [NotMapped]
        public string Plot  { get; set; }

        [NotMapped]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? Area { get; set; }
        [NotMapped]
        public string Category { get; set; }
        [NotMapped]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? LeaseAmount { get; set; }
        [NotMapped]
        
        public DateTime RenewalDate { get; set; }
    }
}
