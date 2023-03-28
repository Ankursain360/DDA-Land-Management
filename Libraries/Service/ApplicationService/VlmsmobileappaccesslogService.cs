using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class VlmsmobileappaccesslogService : EntityService<Vlmsmobileappaccesslog>, IVlmsmobileappaccesslogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVlmsmobileappaccesslogRepository _repository;

        public VlmsmobileappaccesslogService(IUnitOfWork unitOfWork, IVlmsmobileappaccesslogRepository repository) : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<bool> Create(ApiSaveVlmsmobileappaccesslogDto Dto)
        {
            Vlmsmobileappaccesslog Model = new Vlmsmobileappaccesslog();
            Model.UserId = Dto.UserId == 0?null:Dto.UserId;
            Model.UserName = Dto.UserName;
            Model.IPAddress = Dto.IPAddress;
            Model.Brand = Dto.Brand;
            Model.OSVersion = Dto.OSVersion;
            Model.LoginStatus = Dto.LoginStatus.ToUpper() == "T"?"T":"F";
            Model.ModelNumber = Dto.ModelNumber;
            Model.IsActive = 1;
            Model.CreatedDate = DateTime.Now;
            Model.CreatedBy = 1;
            _repository.Add(Model);
            return await _unitOfWork.CommitAsync() > 0;            

        }
    }
}
