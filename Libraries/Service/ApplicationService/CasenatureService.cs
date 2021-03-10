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
using AutoMapper;
using Dto.Master;

namespace Libraries.Service.ApplicationService
{
    public class CasenatureService : EntityService<Casenature>, ICasenatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICasenatureRepository _casenatureRepository;
        private readonly IMapper _mapper;
        public CasenatureService(IUnitOfWork unitOfWork,  ICasenatureRepository casenatureRepository,    IMapper mapper)  : base(unitOfWork, casenatureRepository)
        {
            _unitOfWork = unitOfWork;
            _casenatureRepository = casenatureRepository;
            _mapper = mapper;
        }

        public async Task<List<Casenature>> GetAllcasenature()
        {
            return await _casenatureRepository.GetAll();
        }

        public async Task<List<CasenatureSearchDto>> Getcasenature()
        {
            var casenature = await _casenatureRepository.FindBy(a => a.IsActive == 1);
            var result = _mapper.Map<List<CasenatureSearchDto>>(casenature);
            return result;
        }

        public async Task<PagedResult<Casenature>> GetPagedcasenature(CasenatureSearchDto model)
        {
            return await _casenatureRepository.GetPagedcasenature(model);
        }

        public async Task<List<Casenature>> GetcasenatureUsingRepo()
        {
            return await _casenatureRepository.Getcasenature();
        }

        public async Task<Casenature> FetchSingleResult(int id)
        {
            var result = await _casenatureRepository.FindBy(a => a.Id == id);
            Casenature model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Casenature casenature)
        {
            var result = await _casenatureRepository.FindBy(a => a.Id == id);
            Casenature model = result.FirstOrDefault();
            model.Name = casenature.Name;
            model.IsActive = casenature.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _casenatureRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public new async Task<bool> Create(Casenature casenature)
        {

            casenature.CreatedBy = 1;
            casenature.CreatedDate = DateTime.Now;
            _casenatureRepository.Add(casenature);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string casenature)
        {
            bool result = await _casenatureRepository.Any(id, casenature);
        
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _casenatureRepository.FindBy(a => a.Id == id);
            Casenature model = form.FirstOrDefault();
            model.IsActive = 0;
            _casenatureRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

       
    }
}
