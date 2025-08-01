﻿using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Datastoragepartfilenodetails : AuditableEntity<int>
    {
     
        public int? DataStorageDetailsId { get; set; }
        public string Category { get; set; }
        public string Header { get; set; }
        public string SequenceNo { get; set; }
       
        public int YearofPartFile { get; set; }
        public int SchemeDptBranch { get; set; }
        public int LocalityId { get; set; }
        public string Subject { get; set; }
       

        public Datastoragedetails DataStorageDetails { get; set; }
        public Locality Locality { get; set; }
        public Schemefileloading SchemeDptBranchNavigation { get; set; }
    }
    }

