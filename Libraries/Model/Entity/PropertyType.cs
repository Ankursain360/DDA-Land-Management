﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class PropertyType : AuditableEntity<int>
    {
        public PropertyType()
        {
            Branch = new HashSet<Branch>();
            Interest = new HashSet<Interest>();
            Rate = new HashSet<Rate>();
            //Premiumrate = new HashSet<Premiumrate>();
            //Documentcharges = new HashSet<Documentcharges>();
            Damagecalculation = new HashSet<Damagecalculation>();
            //Groundrate = new HashSet<Groundrent>();
            Intersetrate = new HashSet<Interestrate>();
            //Licencefees = new HashSet<Licencefees>();
            Kycform = new HashSet<Kycform>();
            Demandletters = new HashSet<Demandletters>();
        }
        [Required(ErrorMessage = "Status Code is mandatory")]
        public int StatusCode { get; set; }

        [Required(ErrorMessage = "Property Type is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        public ICollection<Interest> Interest { get; set; }
        public ICollection<Rate> Rate { get; set; }
        public ICollection<Damagecalculation> Damagecalculation { get; set; }
        //public ICollection<Premiumrate> Premiumrate { get; set; }
        //public ICollection<Groundrent> Groundrate { get; set; }
        public ICollection<Interestrate> Intersetrate { get; set; }
        //public ICollection<Documentcharges> Documentcharges { get; set; }
        ////public ICollection<Licencefees> Licencefees { get; set; }
        public ICollection<Kycform> Kycform { get; set; }
        public virtual ICollection<Branch> Branch { get; set; }
        public ICollection<Demandletters> Demandletters { get; set; }
    }
}
