using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class NoticeToDamagePayeeService : EntityService<Damagepayeeregister>, INoticeToDamagePayeeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly INoticeToDamagePayeeRepository _noticeToDamagePayeeRepository;


        public NoticeToDamagePayeeService(IUnitOfWork unitOfWork, INoticeToDamagePayeeRepository noticeToDamagePayeeRepository)
        : base(unitOfWork, noticeToDamagePayeeRepository)
        {
            _unitOfWork = unitOfWork;
            _noticeToDamagePayeeRepository = noticeToDamagePayeeRepository;
        }



        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregister(int fileNo)
        {
            return await _noticeToDamagePayeeRepository.GetAllDamagepayeeregister(fileNo);
        }

    }
}
