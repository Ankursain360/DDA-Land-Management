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
    public class DemolitionprogrammasterService : EntityService<Demolitionprogram>, IDemolitionprogrammasterService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemolitionprogrammasterRepository _demolitionprogrammasterRepository;

        public DemolitionprogrammasterService(IUnitOfWork unitOfWork, IDemolitionprogrammasterRepository demolitionprogrammasterRepository)
  : base(unitOfWork, demolitionprogrammasterRepository)
        {
            _unitOfWork = unitOfWork;
            _demolitionprogrammasterRepository = demolitionprogrammasterRepository;
        }



        public async Task<List<Demolitionprogram>> GetDemolitionprogrammasterUsingRepo()
        {
            return await _demolitionprogrammasterRepository.GetDemolitionprogrammaster();
        }

        public async Task<Demolitionprogram> FetchSingleResult(int id)
        {
            var result = await _demolitionprogrammasterRepository.FindBy(a => a.Id == id);
            Demolitionprogram model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Demolitionprogram demolitionprogrammaster)
        {
            var result = await _demolitionprogrammasterRepository.FindBy(a => a.Id == id);
            Demolitionprogram model = result.FirstOrDefault();
            model.Items = demolitionprogrammaster.Items;
            model.ItemsType = demolitionprogrammaster.ItemsType;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _demolitionprogrammasterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Demolitionprogram demolitionprogrammaster)
        {

            demolitionprogrammaster.CreatedBy = 1;
            demolitionprogrammaster.CreatedDate = DateTime.Now;
            _demolitionprogrammasterRepository.Add(demolitionprogrammaster);
            return await _unitOfWork.CommitAsync() > 0;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _demolitionprogrammasterRepository.FindBy(a => a.Id == id);
            Demolitionprogram model = form.FirstOrDefault();
            model.IsActive = 0;
            _demolitionprogrammasterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<List<Demolitionprogram>> GetDemolitionprogrammaster()
        {
            return await _demolitionprogrammasterRepository.GetDemolitionprogrammaster();
        }



        public async Task<PagedResult<Demolitionprogram>> GetPagedDemolitionprogrammaster(DemolitionprogrammasterSearchDto model)
        {
            return await _demolitionprogrammasterRepository.GetPagedDemolitionprogrammaster(model);
        }






    }
}
