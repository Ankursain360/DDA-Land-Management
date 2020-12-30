using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service.ApplicationService
{
  
    public class Legalmanagementsystemservice : EntityService<Legalmanagementsystem>, ILegalmanagementsystemservice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILegalmanagementsystemRepository _legalmanagementsystemRepository;

        public Legalmanagementsystemservice(IUnitOfWork unitOfWork, ILegalmanagementsystemRepository legalmanagementsystemRepository)
        : base(unitOfWork, legalmanagementsystemRepository)
        {
            _unitOfWork = unitOfWork;
            _legalmanagementsystemRepository = legalmanagementsystemRepository;
        }
       
      
        public async Task<List<Zone>> GetZoneList()
        {
            List<Zone> zoneList = await _legalmanagementsystemRepository.GetZoneList();
            return zoneList;
        }
        public async Task<List<Locality>> GetLocalityList(int zoneId) //added by ishu
        {
            List<Locality> localityList = await _legalmanagementsystemRepository.GetLocalityList(zoneId);
            return localityList;
        }
    }
}
