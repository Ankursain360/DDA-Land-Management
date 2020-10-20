using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Libraries.Model.Entity
{
    public partial class Watchandward : AuditableEntity<int>
    {
        public Watchandward()
        {
            Watchandwardphotofiledetails = new HashSet<Watchandwardphotofiledetails>();
            Watchandwardreportfiledetails = new HashSet<Watchandwardreportfiledetails>();
        }
        [Required (ErrorMessage ="Please fill date")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Please select locality")]
        public int? LocalityId { get; set; }

        [Required(ErrorMessage = "Please select khasra")]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = "Please fill Landmark")]
        public string Landmark { get; set; }
        [Required(ErrorMessage = "Please select")]
        public int? Encroachment { get; set; }

        [Required(ErrorMessage = "Status On Ground is required")]
        public string StatusOnGround { get; set; }

        public string PhotoPath { get; set; }
        public string ReportFiletPath { get; set; }

        [Required(ErrorMessage = "Remarks feild is required")]
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Please select  status")]
        public byte? IsActive { get; set; }



        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        public virtual Locality Locality { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }

        [NotMapped]
        public List<IFormFile> Photo { get; set; }
        [NotMapped]
        public List<IFormFile> ReportFile { get; set; }
        public virtual ICollection<Watchandwardphotofiledetails> Watchandwardphotofiledetails { get; set; }
        public virtual ICollection<Watchandwardreportfiledetails> Watchandwardreportfiledetails { get; set; }
        
    }
}
