using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Jaraidetails : AuditableEntity<int>
    {
        public Jaraidetails()
        {
            Jaraifarmer = new HashSet<Jaraifarmer>();
            Jarailessee = new HashSet<Jarailessee>();
            Jaraiowner = new HashSet<Jaraiowner>();
        }

        [Required(ErrorMessage = " Village name is mandatory")]
        public int VillageId { get; set; }
        [Required(ErrorMessage = " Khasra no is mandatory")]
        public int KhasraId { get; set; }
        public int? YearOfjamabandi { get; set; }
       
        public int? NoOfKhewat { get; set; }
        public int? NoOfKhatauni { get; set; }
        public string NaamPatti { get; set; }
        public string NaamMalik { get; set; }
        public decimal? Revenue { get; set; }
        public string OldMutationNo { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> AcquiredlandvillageList { get; set; }

        [NotMapped]
        public List<Khasra> KhasraList { get; set; }

       

        [NotMapped]
        public Khasra Khasra { get; set; }
        public Acquiredlandvillage Village { get; set; }
        public ICollection<Jaraifarmer> Jaraifarmer { get; set; }
        public ICollection<Jarailessee> Jarailessee { get; set; }
        public ICollection<Jaraiowner> Jaraiowner { get; set; }


        //****** Owner details *****

        [NotMapped]
        public List<string> OwnerName { get; set; }

        [NotMapped]
        public List<string> FatherName { get; set; }

        [NotMapped]
        public List<string> Address { get; set; }



        //****** Lessee details *****

        [NotMapped]
        public List<string> LesseeName { get; set; }

        [NotMapped]
        public List<string> Father { get; set; }

        [NotMapped]
        public List<string> LAddress { get; set; }

        [NotMapped]
        public List<string> Mortgage { get; set; }



        //****** Farmer details *****

        [NotMapped]
        public List<string> FarmerName { get; set; }

        [NotMapped]
        public List<string> FFatherName { get; set; }

        [NotMapped]
        public List<string> FAddress { get; set; }



       
    }
}
