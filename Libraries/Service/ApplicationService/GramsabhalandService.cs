using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
  public  class GramsabhalandService : EntityService<Gramsabhaland>, IGramsabhalandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGramsabhalandRepository _gramsabhalandRepository;
        protected readonly DataContext _dbContext;

        public GramsabhalandService(IUnitOfWork unitOfWork, IGramsabhalandRepository gramsabhalandRepository, DataContext dbContext)
     : base(unitOfWork, gramsabhalandRepository)
        {
            _unitOfWork = unitOfWork;
            _gramsabhalandRepository = gramsabhalandRepository;
            _dbContext = dbContext;
        }

        public async Task<List<Gramsabhaland>> GetAllGramsabhaland()
        {
            return await _gramsabhalandRepository.GetAllGramsabhaland();
        }
        public async Task<List<Gramsabhaland>> GetAllGramsabhalandList(GramsabhalandSearchDto model)
        {
            return await _gramsabhalandRepository.GetAllGramsabhalandList(model);
        }
        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _gramsabhalandRepository.GetAllZone();
            return zoneList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage(int? zoneId)
        {
            List<Acquiredlandvillage> VillageList = await _gramsabhalandRepository.GetAllVillage(zoneId);
            return VillageList;
        }

        public async Task<PagedResult<Gramsabhaland>> GetPagedGramsabhaland(GramsabhalandSearchDto model)
        {
            return await _gramsabhalandRepository.GetPagedGramsabhaland(model);
        }


        public async Task<bool> Update(int id, Gramsabhaland gramsabhaland)
        {
            var result = await _gramsabhalandRepository.FindBy(a => a.Id == id);
            Gramsabhaland model = result.FirstOrDefault();
            model.ZoneId = gramsabhaland.ZoneId;
            model.VillageId = gramsabhaland.VillageId;
            model.KhasraNo = gramsabhaland.KhasraNo;

            model.TotalAreaBigha = gramsabhaland.TotalAreaBigha;
            model.TotalAreaBiswa = gramsabhaland.TotalAreaBiswa;
            model.TotalAreaBiswanshi = gramsabhaland.TotalAreaBiswanshi;

            model.VacantAreaBigha = gramsabhaland.VacantAreaBigha;
            model.VacantAreaBiswa = gramsabhaland.VacantAreaBiswa;
            model.VacantAreaBiswanshi = gramsabhaland.VacantAreaBiswanshi;

            model.BuiltupAreaBigha = gramsabhaland.BuiltupAreaBigha;
            model.BuiltupAreaBiswa = gramsabhaland.BuiltupAreaBiswa;
            model.BuiltupAreaBiswanshi = gramsabhaland.BuiltupAreaBiswanshi;

            model.EncroachedAreaBigha = gramsabhaland.EncroachedAreaBigha;
            model.EncroachedAreaBiswa = gramsabhaland.EncroachedAreaBiswa;
            model.EncroachedAreaBiswanshi = gramsabhaland.EncroachedAreaBiswanshi;

            model.Us507notificationNo = gramsabhaland.Us507notificationNo;
            model.Us507notificationDate = gramsabhaland.Us507notificationDate;
            model.GazzetteNotificationUs507document = gramsabhaland.GazzetteNotificationUs507document;

            model.Us22notificationNo = gramsabhaland.Us22notificationNo;
            model.Us22notificationDate = gramsabhaland.Us22notificationDate;
            model.Us22notificationDocument = gramsabhaland.Us22notificationDocument;


            model.Us22otherNotificationDocument = gramsabhaland.Us22otherNotificationDocument;
            model.TypeOfStructureOnGramLand = gramsabhaland.TypeOfStructureOnGramLand;
            model.WhetherTssSurveyDone = gramsabhaland.WhetherTssSurveyDone;

            model.UploadTssSurveyReport = gramsabhaland.UploadTssSurveyReport;
            model.BoundaryWallDone = gramsabhaland.BoundaryWallDone;
            model.KabzaProceeding = gramsabhaland.KabzaProceeding;

            model.TakenFrom = gramsabhaland.TakenFrom;
            model.DateofTakenOver = gramsabhaland.DateofTakenOver;
            model.HandedOverTo = gramsabhaland.HandedOverTo;

            model.HandedOverDate = gramsabhaland.HandedOverDate;
            model.LandRecordReceivedGnctd = gramsabhaland.LandRecordReceivedGnctd;
            model.Remarks = gramsabhaland.Remarks;

            model.IsActive = gramsabhaland.IsActive;


            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = gramsabhaland.ModifiedBy;
            _gramsabhalandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }


        public async Task<bool> Create(Gramsabhaland gramsabhaland)
        {
            gramsabhaland.CreatedBy = gramsabhaland.CreatedBy;
            gramsabhaland.CreatedDate = DateTime.Now;
            _gramsabhalandRepository.Add(gramsabhaland);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Gramsabhaland> FetchSingleResult(int id)
        {
            var result = await _gramsabhalandRepository.FindBy(a => a.Id == id);
            Gramsabhaland model = result.FirstOrDefault();
            return model;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _gramsabhalandRepository.FindBy(a => a.Id == id);
            Gramsabhaland model = form.FirstOrDefault();
            model.IsActive = 0;
            _gramsabhalandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

    }
}
