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

namespace Libraries.Service.ApplicationService
{

    public class CalculationSheetService : EntityService<Allotmententry>, ICalculationSheetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICalculationSheetRepository _calculationSheetRepository;

        public CalculationSheetService(IUnitOfWork unitOfWork, ICalculationSheetRepository calculationSheetRepository)
        : base(unitOfWork, calculationSheetRepository)
        {
            _unitOfWork = unitOfWork;
            _calculationSheetRepository = calculationSheetRepository;
        }
     
        public async Task<List<Allotmententry>> GetAllApplications()
        {
            List<Allotmententry> list = await _calculationSheetRepository.GetAllApplications();
            return list;
        }
      
    }
}
