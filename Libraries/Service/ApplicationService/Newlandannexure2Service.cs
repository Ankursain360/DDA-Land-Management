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
    public class Newlandannexure2Service : EntityService<Newlandannexure2>, INewlandannexure2Service
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandannexure2Repository _newlandannexure2Repository;


        public Newlandannexure2Service(IUnitOfWork unitOfWork, INewlandannexure2Repository newlandannexure2Repository) : base(unitOfWork, newlandannexure2Repository)
        {
            _unitOfWork = unitOfWork;
            _newlandannexure2Repository = newlandannexure2Repository;
        }
        public async Task<PagedResult<Newlandannexure2>> GetPagedNewlandannexure2(Newlandannexure1SearchDto model)
        {
            return await _newlandannexure2Repository.GetPagedNewlandannexure2(model);
        }
        public async Task<bool> Create(Newlandannexure2 Annexure2)
        {
            Annexure2.CreatedBy = Annexure2.CreatedBy;
            Annexure2.CreatedDate = DateTime.Now;
            Annexure2.IsActive = 1;
            Annexure2.ReqId = Annexure2.ReqId;
            _newlandannexure2Repository.Add(Annexure2);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public Task<List<Newlandannexure2>> GetAllNewlandannexure2()
        {
            throw new NotImplementedException();
        }
    }
}
