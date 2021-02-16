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
            Saknilessee = new HashSet<Saknilessee>();
            Sakniowner = new HashSet<Sakniowner>();
            Saknitenant = new HashSet<Saknitenant>();
        }

        [Required(ErrorMessage = " Village name is mandatory")]
        public int VillageId { get; set; }
        [Required(ErrorMessage = " Khasra no is mandatory")]
        public int KhasraId { get; set; }
        public int? YearOfjamabandi { get; set; }
        public int? NoOfKhewat { get; set; }
        public int? NoOfKhatauni { get; set; }
        public string Location { get; set; }
        public string Mortgage { get; set; }
        public decimal? RentAmount { get; set; }
        public string OldMutationNo { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> AcquiredlandvillageList { get; set; }

        [NotMapped]
        public List<Khasra> KhasraList { get; set; }

        public Khasra Khasra { get; set; }
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




    }
}
