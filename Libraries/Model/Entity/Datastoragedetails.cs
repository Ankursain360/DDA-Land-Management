using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Datastoragedetails : AuditableEntity<int>
    {
        public Datastoragedetails()
        {
            Issuereturnfile = new HashSet<Issuereturnfile>();
            Datastoragepartfilenodetails = new HashSet<Datastoragepartfilenodetails>();
        }

        public int IsFileDocument { get; set; }

        public string FileNo { get; set; }
        public string Name { get; set; }
        public int? IsPartOfMainFile { get; set; }
        public string RecordRoomNo { get; set; }

        [Required(ErrorMessage = "Almirah no is Mandatory Field", AllowEmptyStrings = false)]

        public int? AlmirahId { get; set; }

        [Required(ErrorMessage = "Row no is Mandatory Field", AllowEmptyStrings = false)]
        public int? RowId { get; set; }


        [Required(ErrorMessage = "Coloumn no is Mandatory Field", AllowEmptyStrings = false)]
        public int? ColumnId { get; set; }


        [Required(ErrorMessage = "Bundle is Mandatory Field", AllowEmptyStrings = false)]
        public int? BundleId { get; set; }


        public int? ZoneId { get; set; }


        [Required(ErrorMessage = "Locality is Mandatory Field", AllowEmptyStrings = false)]
        public int? LocalityId { get; set; }

      
        public string KhasraNoPropertyNo { get; set; }
        public string Area { get; set; }

        public byte? IsActive { get; set; }
        public string SectorId { get; set; }
        public string PocketId { get; set; }
        public string BlockId { get; set; }
        public string FlatNo { get; set; }
        public string FlatCategoryId { get; set; }
        public string CompactorNo { get; set; }


        [Required(ErrorMessage = "Sequence No is Mandatory Field")]
        public string SequenceNo { get; set; }
        public string SttsNo { get; set; }
        public int? BranchSno { get; set; }
        public int? YearTo { get; set; }
        public string IsFreeHold { get; set; }
        public string DocumentSequenceNo { get; set; }
        public string DocumentType { get; set; }
        public int? DepartmentId { get; set; }
        public int? UserId { get; set; }


        public int? Year { get; set; }

        [Required(ErrorMessage = "Category No is Mandatory Field")]
        public string CategoryNo { get; set; }

        [Required(ErrorMessage = "Header No is Mandatory Field")]
        public string HeaderNo { get; set; }


        public string FileStatus { get; set; }
        public int? BranchId { get; set; }

        public Almirah Almirah { get; set; }
        public Bundle Bundle { get; set; }
        public Branch Branch { get; set; }
        public Column Column { get; set; }
        public Locality Locality { get; set; }
        public Department Department { get; set; }

        public Schemefileloading SchemeFileLoading { get; set; }
        //public Branch Branch { get; set; }
        public Row Row { get; set; }
        public Zone Zone { get; set; }

        public int? SchemeId { get; set; }



        [NotMapped]
        public List<Almirah> AlmirahList { get; set; }
        [NotMapped]
        public List<Row> RowList { get; set; }
        [NotMapped]
        public List<Column> ColumnList { get; set; }
        [NotMapped]
        public List<Bundle> BundleList { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        //[NotMapped]
        //public List<Scheme> schemaList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Branch> BranchList { get; set; }
        [NotMapped]
        public List<Department> DepartmentName { get; set; }
        [NotMapped]
        public int? TotalFiles { get; set; }
        [NotMapped]
        public int? IssuedFiles { get; set; }
        [NotMapped]
        public int? UnissuedFiles { get; set; }
        [NotMapped]
        public List<Schemefileloading> SchemeFileLoadingList { get; set; }




        [NotMapped]
        public List<string> Category { get; set; }
        [NotMapped]
        public List<string> Header { get; set; }
        [NotMapped]
        public List<string> SequenceNoForPartFile { get; set; }

        [NotMapped]
        public Schemefileloading SchemeDptBranchNavigation { get; set; }

        [NotMapped]
        public List<string> Subject { get; set; }

        [NotMapped]
        public List<int> LocalityIdForPartFile { get; set; }

        [NotMapped]
        public List<int> SchemeDptBranch { get; set; }

        [NotMapped]
        public List<int> YearForPartFile { get; set; }

        public ICollection<Issuereturnfile> Issuereturnfile { get; set; }
      
        [NotMapped]
        public List<Datastoragedetails> FileNoList { get; set; }

        public ICollection<Datastoragepartfilenodetails> Datastoragepartfilenodetails { get; set; }
    }
}

