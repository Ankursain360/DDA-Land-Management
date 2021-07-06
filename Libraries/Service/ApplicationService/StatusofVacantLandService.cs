using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class StatusofVacantLandService : EntityService<Vacantlandimage>, IStatusofVacantLandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatusofVacantLandRepository _statusofVacantLandRepository;
        public StatusofVacantLandService(IUnitOfWork unitOfWork, IStatusofVacantLandRepository statusofVacantLandRepository)
      : base(unitOfWork, statusofVacantLandRepository)
        {
            _unitOfWork = unitOfWork;
            _statusofVacantLandRepository = statusofVacantLandRepository;
        }



        //public async Task<PagedResult<Vacantlandimage>> GetPagedStatusOfVacantLandReportData(StatusOfVacantLandSearchDto model)
        //{
        //    return await _statusofVacantLandRepository.GetPagedStatusOfVacantLandReportData(model);
        //}
        public async Task<List<StatusOfVacantLandListDataDto>> GetStatusOfVacantLandReportData(StatusOfVacantLandSearchDto model)
        {
            return await _statusofVacantLandRepository.GetStatusOfVacantLandReportData(model);
        }

    }
}
