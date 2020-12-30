using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.IEntityRepository
{
    public interface ILegalmanagementsystemRepository : IGenericRepository<Legalmanagementsystem>
    {
        Task<List<Zone>> GetZoneList();
        Task<List<Locality>> GetLocalityList(int zoneId);

        Task<List<Legalmanagementsystem>> GetFileNoList();
        Task<List<Legalmanagementsystem>> GetCourtCaseNoList(int filenoId);
    }
}
