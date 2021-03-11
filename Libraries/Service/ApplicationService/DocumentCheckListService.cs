using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
using Dto.Master;
using AutoMapper;

namespace Libraries.Service.ApplicationService
{

    public class DocumentCheckListService : EntityService<Documentchecklist>, IDocumentCheckListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentCheckListRepository _documentCheckListRepository;
        private readonly IMapper _mapper;
        public DocumentCheckListService(IUnitOfWork unitOfWork,
            IDocumentCheckListRepository documentCheckListRepository,
            IMapper mapper)
        : base(unitOfWork, documentCheckListRepository)
        {
            _unitOfWork = unitOfWork;
            _documentCheckListRepository = documentCheckListRepository;
            _mapper = mapper;
        }
        public async Task<List<Zone>> GetAllDetails()
        {
            return await _documentCheckListRepository.GetAllDetails();
        }

        public async Task<List<ZoneDto>> GetZone()
        {
            var zones = await _documentCheckListRepository.FindBy(a => a.IsActive == 1);
            var result = _mapper.Map<List<ZoneDto>>(zones);
            return result;
        }

        public async Task<List<Department>> GetDropDownList()
        {
            List<Department> departmentList = await _documentCheckListRepository.GetDepartmentList();
            return departmentList;
        }
        //public async Task<Zone> FetchSingleResult(int id)
        //{
        //    var result = await _documentCheckListRepository.FindBy(a => a.Id == id);
        //    Zone model = result.FirstOrDefault();
        //    return model;
        //}
        //public async Task<bool> Update(int id, Zone zone)
        //{
        //    var result = await _documentCheckListRepository.FindBy(a => a.Id == id);
        //    Zone model = result.FirstOrDefault();
        //    model.DepartmentId = zone.DepartmentId;
        //    model.Name = zone.Name;
        //    model.Code = zone.Code;
        //    model.IsActive = zone.IsActive;
        //    model.ModifiedDate = DateTime.Now;
        //    model.ModifiedBy = 1;
        //    _documentCheckListRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}

        //public async Task<bool> Create(Zone zone)
        //{

        //    zone.CreatedBy = 1;
        //    zone.CreatedDate = DateTime.Now;
        //    _documentCheckListRepository.Add(zone);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}


        public async Task<bool> CheckUniqueName(int id, string zone)
        {
            bool result = await _documentCheckListRepository.Any(id, zone);
            return result;
        }
        public async Task<bool> CheckUniqueCode(int id, string code)
        {
            bool result = await _documentCheckListRepository.anyCode(id, code);
            return result;
        }

        //public async Task<bool> Delete(int id)
        //{
        //    var form = await _documentCheckListRepository.FindBy(a => a.Id == id);
        //    Zone model = form.FirstOrDefault();
        //    model.IsActive = 0;
        //    _documentCheckListRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}

        public async Task<PagedResult<Zone>> GetPagedZone(ZoneSearchDto model)
        {
            return await _documentCheckListRepository.GetPagedZone(model);
        }
    }
}
