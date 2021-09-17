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
     public class AuditService : EntityService<AuditModel>, IAuditService
    {
          private readonly IUnitOfWork _unitOfWork;
          private readonly IAuditRepository _auditRepository;
           protected readonly DataContext _dbContext;
        public AuditService(IUnitOfWork unitOfWork, IAuditRepository auditrepository, DataContext dbContext)
    : base(unitOfWork, auditrepository)
        {
            _unitOfWork = unitOfWork;
            _auditRepository = auditrepository;
            _dbContext = dbContext;
        }

        public Task<bool> InsertAuditLogs(AuditModel model)
        {
         
            return  _auditRepository.InsertAuditLogs(model);
        }

    }
}
