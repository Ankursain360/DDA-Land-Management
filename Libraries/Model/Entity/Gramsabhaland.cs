using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;


namespace Libraries.Model.Entity
{
    public  class Gramsabhaland : AuditableEntity<int>
    {
      
        public int VillageId { get; set; }
        public int ZoneId { get; set; }
        public string KhasraNo { get; set; }
        public int TotalAreaBigha { get; set; }
        public int? TotalAreaBiswa { get; set; }
        public int? TotalAreaBiswanshi { get; set; }
        public int VacantAreaBigha { get; set; }
        public int? VacantAreaBiswa { get; set; }
        public int? VacantAreaBiswanshi { get; set; }
        public int BuiltupAreaBigha { get; set; }
        public int? BuiltupAreaBiswa { get; set; }
        public int? BuiltupAreaBiswanshi { get; set; }
        public int EncroachedAreaBigha { get; set; }
        public int? EncroachedAreaBiswa { get; set; }
        public int? EncroachedAreaBiswanshi { get; set; }
        public string Us507notificationNo { get; set; }
        public DateTime? Us507notificationDate { get; set; }
        public string GazzetteNotificationUs507document { get; set; }
        public string Us22notificationNo { get; set; }
        public DateTime? Us22notificationDate { get; set; }
        public string Us22notificationDocument { get; set; }
        public string Us22otherNotificationDocument { get; set; }
        public string TypeOfStructureOnGramLand { get; set; }
        public string WhetherTssSurveyDone { get; set; }
        public string UploadTssSurveyReport { get; set; }
        public string BoundaryWallDone { get; set; }
        public string KabzaProceeding { get; set; }
        public string TakenFrom { get; set; }
        public DateTime? DateofTakenOver { get; set; }
        public string HandedOverTo { get; set; }
        public DateTime? HandedOverDate { get; set; }
        public string LandRecordReceivedGnctd { get; set; }
        public string Remarks { get; set; }
     
        public byte? IsActive { get; set; }

        public Acquiredlandvillage Village { get; set; }
        public Zone Zone { get; set; }

        [NotMapped]
        public List<Zone> ZoneList { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }

        [NotMapped]
        public IFormFile GNus507Document1 { get; set; }

        [NotMapped]
        public IFormFile Us22Document2 { get; set; }

        [NotMapped]
        public IFormFile Us22OtherDocument3 { get; set; }

        [NotMapped]
        public IFormFile TssSurveyDocument4 { get; set; }

        [NotMapped]
        public IFormFile KabzaDocument5 { get; set; }


       

    }
}
