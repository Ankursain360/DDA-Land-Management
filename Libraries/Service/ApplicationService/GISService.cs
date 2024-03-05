using AutoMapper;
using Dto.GIS;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.EntityRepository;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class GISService : EntityService<Zone>, IGISService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGISSRepository _iGISSRepository;
        private readonly IMapper _mapper;
        public GISService(IUnitOfWork unitOfWork, IGISSRepository iGisSRepository, IMapper mapper) : base(unitOfWork, iGisSRepository)
        {
            _unitOfWork = unitOfWork;
            _iGISSRepository = iGisSRepository;
            _mapper = mapper;

        }

        public async Task<List<Gisaabadi>> GetAbadiDetails(int villageId)
        {
            return await _iGISSRepository.GetAbadiDetails(villageId);
        }

        public async Task<List<Gisburji>> GetBurjiDetails(int villageId)
        {
            return await _iGISSRepository.GetBurjiDetails(villageId);
        }

        public async Task<List<Gisclean>> GetCleanDetails(int villageId)
        {
            return await _iGISSRepository.GetCleanDetails(villageId);
        }

        public async Task<List<Giscleantext>> GetCleantextDetails(int villageId)
        {
            return await _iGISSRepository.GetCleantextDetails(villageId);
        }

        public async Task<List<Gisclose>> GetCloseDetails(int villageId)
        {
            return await _iGISSRepository.GetCloseDetails(villageId);
        }

        public async Task<List<Gisclosetext>> GetCloseTextDetails(int villageId)
        {
            return await _iGISSRepository.GetCloseTextDetails(villageId);
        }

        public async Task<List<Gisdashed>> GetDashedDetails(int villageId)
        {
            return await _iGISSRepository.GetDashedDetails(villageId);
        }

        public async Task<List<Gisdim>> GetDimDetails(int villageId)
        {
            return await _iGISSRepository.GetDimDetails(villageId);
        }

        public async Task<List<Gisdimtext>> GetDimTextDetails(int villageId)
        {
            return await _iGISSRepository.GetDimTextDetails(villageId);
        }

        public async Task<List<Gisencroachment>> GetEncroachmentDetails(int villageId)
        {
            return await _iGISSRepository.GetEncroachmentDetails(villageId);
        }
        
        public async Task<List<Gisfieldboun>> GetFieldBounDetails(int villageId)
        {
            return await _iGISSRepository.GetFieldBounDetails(villageId);
        }

        public async Task<List<Gisdata>> GetGisDataLayersDetails(int villageId)
        {
            return await _iGISSRepository.GetGisDataLayersDetails(villageId);
        }

        public async Task<List<Gisgosha>> GetGoshaDetails(int villageId)
        {
            return await _iGISSRepository.GetGoshaDetails(villageId);
        }

        public async Task<List<Gisgrid>> GetGridDetails(int villageId)
        {
            return await _iGISSRepository.GetGridDetails(villageId);
        }

        public async  Task<List<gisDataTemp>> GetInfrastructureDetails(int villageId)
        {
            return await _iGISSRepository.GetInfrastructureDetails(villageId);
        }

        public async Task<List<State>> GetInitiallyStateDetails()
        {
            return await _iGISSRepository.GetInitiallyStateDetails();
        }

        public async Task<List<Gisinner>> GetInnerDetails(int villageId)
        {
            return await _iGISSRepository.GetInnerDetails(villageId);
        }

        public async Task<List<Giskachapakaline>> GetKachaPakaLineDetails(int villageId)
        {
            return await _iGISSRepository.GetKachaPakaLineDetails(villageId);
        }

        public async Task<List<GISKhasraBasisOtherDetailsDto>> GetKhasraBasisOtherDetails(int villageId, string khasraNo, string RectNo)
        {
            return await _iGISSRepository.GetKhasraBasisOtherDetails(villageId, khasraNo,RectNo);
        }

        public async Task<List<GISKhasraBasisOtherDetailsDto>> GetKhasraBasisOtherDetailsForCourtCases(int villageId, string khasraNo, string RectNo)
        {
            return await _iGISSRepository.GetKhasraBasisOtherDetailsForCourtCases(villageId, khasraNo,RectNo);
        }

        public async Task<List<Giskhasraboundary>> GetKhasraBoundaryDetails(int villageId)
        {
            return await _iGISSRepository.GetKhasraBoundaryDetails(villageId);
        }

        public async Task<List<Giskhasraline>> GetKhasraLineDetails(int villageId)
        {
            return await _iGISSRepository.GetKhasraLineDetails(villageId);
        }

        public async Task<List<GISKhasraDto>> GetKhasraList(int villageId)
        {
            var khasra = await _iGISSRepository.GetKhasraList(villageId);
            var result = _mapper.Map<List<GISKhasraDto>>(khasra);
            return result;
        }

        public async Task<List<Giskhasrano>> GetKhasraNoDetails(int villageId)
        {
            return await _iGISSRepository.GetKhasraNoDetails(villageId);
        }

        public async Task<List<Gisdata>> GetKhasraNoPolygon(int gisDataId)
        {
            return await _iGISSRepository.GetKhasraNoPolygon(gisDataId);
        }

        public async Task<List<Giskilla>> GetKillaDetails(int villageId)
        {
            return await _iGISSRepository.GetKillaDetails(villageId);
        }

        public async Task<List<Gisnala>> GetNalaDetails(int villageId)
        {
            return await _iGISSRepository.GetNalaDetails(villageId);
        }

        public async Task<List<Gisnali>> GetNaliDetails(int villageId)
        {
            return await _iGISSRepository.GetNaliDetails(villageId);
        }

        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _iGISSRepository.GetPlotList(VillageId);
        }

        public async Task<List<Gisrailwayline>> GetRailwayLineDetails(int villageId)
        {
            return await _iGISSRepository.GetRailwayLineDetails(villageId);
        }

        public async Task<List<Gisroad>> GetRoadDetails(int villageId)
        {
            return await _iGISSRepository.GetRoadDetails(villageId);
        }

        public async Task<List<Gissaheda>> GetSahedaDetails(int villageId)
        {
            return await _iGISSRepository.GetSahedaDetails(villageId);
        }

        public async Task<List<Gistext>> GetTextDetails(int villageId)
        {
            return await _iGISSRepository.GetTextDetails(villageId);
        }

        public async  Task<List<Gistrijunction>> GetTriJunctionDetails(int villageId)
        {
            return await _iGISSRepository.GetTriJunctionDetails(villageId);
        }

        public async Task<List<VillageDto>> GetVillageAutoCompleteDetails(string prefix)
        {
            var villages = await _iGISSRepository.GetVillageAutoCompleteDetails(prefix);
            var result = _mapper.Map<List<VillageDto>>(villages);
            return result;
        }

        public async Task<List<Gisvillageboundary>> GetVillageBoundaryDetails(int villageId)
        {
            return await _iGISSRepository.GetVillageBoundaryDetails(villageId);
        }

        public async Task<List<Village>> GetVillageDetails(int villageId)
        {
            return await _iGISSRepository.GetVillageDetails(villageId);
        }

        public async Task<List<Village>> GetVillageList(int ZoneId)
        {
            return await _iGISSRepository.GetVillageList(ZoneId);
        }

        public async Task<List<Gisvillagetext>> GetVillageTextDetails(int villageId)
        {
            return await _iGISSRepository.GetVillageTextDetails(villageId);
        }

        public async Task<List<Giszero>> GetZeroDetails(int villageId)
        {
            return await _iGISSRepository.GetZeroDetails(villageId);
        }

        public async Task<List<Zone>> GetZoneDetails(int zoneId)
        {
            return await _iGISSRepository.GetZoneDetails(zoneId);
        }

        public async Task<List<Zone>> GetZoneList()
        {
            return await _iGISSRepository.GetZoneList();
        }
        public async Task<PagedResult<Gisdata>> NavigateGCPDetails(NavigateGCPDetailsSearchDto dto)
        {
            var data = await _iGISSRepository.NavigateGCPDetails(dto);
            return data;
        }
        public async Task<GISKhasraUpdateResponseDto> UpdatekhasraNo(int khasraid, string KhasraNo,int Userid)
        {
            var result = await _iGISSRepository.UpdatekhasraNo(khasraid, KhasraNo, Userid); 
             
            return result;
        }

        public async Task<List<Gisdata>> GetGCPList(int villageId)
        {
            var GCPPoints = await _iGISSRepository.GetGCPList(villageId);
            
            return GCPPoints;
        }

        public async Task<List<GISKhasraExport>> GetKhasraListforExport(int villageId)
        {
            var khasra = await _iGISSRepository.GetKhasraListforExport(villageId); 
            return khasra;
        }

        public async Task<PagedResult<AIchangedetectiondata>> GetChangeDetectionData(AIchangeDetectionSearchDto model)
        {
            var data = await _iGISSRepository.GetChangeDetectionData(model);
            return data;
        }
        public async Task<AIchangedetectiondata> GetAIchangedetectionImageDetails(int id)
        {
            return await _iGISSRepository.GetAIchangedetectionImageDetails(id);
        }
        public async Task<bool> InsertchangeDetectiondata(ChangeDetectionDto dto)
        {
            AIchangedetectiondata model = new AIchangedetectiondata();
            model.Zoneid = dto.ZoneId;
            model.Villageid = dto.VillageID;
            model.FirstPhotoPath = dto.FirstPhotoPath;
            model.SecondPhotoPath = dto.SecondPhotoPath;
            model.ChangedImage = dto.ChangedImagePath;
            model.Similarity = dto.Similarity;
            model.FirstImageResoultion = dto.FirstImageResoultion;
            model.SecondImageResoultion = dto.SecondImageResoultion;
            model.IsActive = 1;
            model.CreatedBy = dto.CreatedBy;

            var result = await  _iGISSRepository.InsertchangeDetectiondata(model);
              
            
            return result;
        }
    }
}
