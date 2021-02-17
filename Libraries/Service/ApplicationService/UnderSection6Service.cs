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
   public class UnderSection6Service : EntityService<Undersection6>, IUnderSection6Service
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnderSection6Repository _undersection6Repository;
        public UnderSection6Service(IUnitOfWork unitOfWork, IUnderSection6Repository undersection6Repository)
     : base(unitOfWork, undersection6Repository)
        {
            _unitOfWork = unitOfWork;
            _undersection6Repository = undersection6Repository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _undersection6Repository.FindBy(a => a.Id == id);
            Undersection6 model = form.FirstOrDefault();
            model.IsActive = 0;
            _undersection6Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Undersection6> FetchSingleResult(int id)
        {
            var result = await _undersection6Repository.FindBy(a => a.Id == id);
            Undersection6 model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Undersection4>> GetAllundersection4()
        {
            List<Undersection4> purposeList = await _undersection6Repository.GetAllundersection4();
            return purposeList;
        }

        public async Task<List<Undersection6>> GetAllUndersection6()
        {

            return await _undersection6Repository.GetAllUndersection6();
        }



        public async Task<List<Undersection6>> GetUndersection6UsingRepo()
        {
            return await _undersection6Repository.GetAllUndersection6();
        }

        public async Task<bool> Update(int id, Undersection6 undersection4)
        {
  
            var result = await _undersection6Repository.FindBy(a => a.Id == id);
            Undersection6 model = result.FirstOrDefault();
            model.Undersection4Id = undersection4.Undersection4Id;
            model.Number = undersection4.Number;
            model.Ndate = undersection4.Ndate;
         
            //   model.TypePurpose = undersection4.TypePurpose;

            model.IsActive = undersection4.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _undersection6Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Undersection6 undersection4)
        {
            undersection4.CreatedBy = 1;
         
            undersection4.CreatedDate = DateTime.Now;

            _undersection6Repository.Add(undersection4);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Undersection6>> GetPagedUndersection6details(Undersection6SearchDto model)
        {
            return await _undersection6Repository.GetPagedUndersection6details(model);
        }




    }
}
