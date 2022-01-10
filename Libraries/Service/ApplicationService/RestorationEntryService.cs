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
    public class RestorationEntryService : EntityService<Restorationentry>, IRestorationEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestorationEntryRepository _restorationEntryRepository;

        public RestorationEntryService(IUnitOfWork unitOfWork, IRestorationEntryRepository restorationEntryRepository)
        : base(unitOfWork, restorationEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _restorationEntryRepository = restorationEntryRepository;
        } 
        public async Task<bool> Create(Restorationentry restorationentry)
        {
            restorationentry.CreatedDate = DateTime.Now;
            _restorationEntryRepository.Add(restorationentry);
            return await _unitOfWork.CommitAsync() > 0; 
             
        } 
       
    }
}
