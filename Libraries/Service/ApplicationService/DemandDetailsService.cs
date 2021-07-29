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
using Dto.Master;

namespace Libraries.Service.ApplicationService
{
   public  class DemandDetailsService : EntityService<Kycform>, IDemandDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemandDetailsRepository _demandDetailsRepository;


        public DemandDetailsService(IUnitOfWork unitOfWork, IDemandDetailsRepository demandDetailsRepository)
: base(unitOfWork, demandDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _demandDetailsRepository = demandDetailsRepository;
        }



        public async Task<PagedResult<Kycform>> GetPagedDemandDetails(DemandDetailsSearchDto model,string MobileNo)
        {
            return await _demandDetailsRepository.GetPagedDemandDetails(model,MobileNo);
        }

        public async Task<List<DemandPaymentDetailsDto>> GetPaymentDetails(int FileNo)
        {
            return await _demandDetailsRepository.GetPaymentDetails(FileNo);
        }


        
        public async Task<Kycform> FetchSingleResult(int Id)
        {

            var result = await _demandDetailsRepository.FindBy(a => a.Id == Id);
            Kycform model = result.FirstOrDefault();
            return model;
        }
      


    }
}
