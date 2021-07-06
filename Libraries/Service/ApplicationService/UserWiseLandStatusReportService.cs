using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.EntityRepository;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libraries.Model;

namespace Libraries.Service.ApplicationService
{
    public class UserWiseLandStatusReportService : EntityService<Vacantlandimage>, IUserWiseLandStatusReportService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserWiseLandStatusReportRepository _UserWiseLandStatusReportRepository;
        public UserWiseLandStatusReportService(IUnitOfWork unitOfWork, IUserWiseLandStatusReportRepository UserWiseLandStatusReportRepository)
        : base(unitOfWork, UserWiseLandStatusReportRepository)
        {
            _unitOfWork = unitOfWork;
            _UserWiseLandStatusReportRepository = UserWiseLandStatusReportRepository;
        }




        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _UserWiseLandStatusReportRepository.GetAllDepartment();
            return departmentList;
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _UserWiseLandStatusReportRepository.GetAllZone(departmentId);
            return zoneList;
        }

        public async Task<List<Division>> GetAllDivisionList(int zone)
        {
            List<Division> divisionList = await _UserWiseLandStatusReportRepository.GetAllDivision(zone);
            return divisionList;
        }


        public async Task<PagedResult<Vacantlandimage>> GetPagedUserWiseLandStatusReport(UserWiseLandStatusReportSearchDto model)

        {
            return await _UserWiseLandStatusReportRepository.GetPagedUserWiseLandStatusReport(model);
        }
    }

}