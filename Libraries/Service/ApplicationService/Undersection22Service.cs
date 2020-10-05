using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
     public class Undersection22Service : EntityService<Undersection22>, IUndersection22Service
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUndersection22Repository _undersection22Repository;

        public Undersection22Service(IUnitOfWork unitOfWork, IUndersection22Repository undersection22Repository)
        : base(unitOfWork, undersection22Repository)
        {
            _unitOfWork = unitOfWork;
            _undersection22Repository = undersection22Repository;
        }

        public async Task<List<Undersection22>> GetAllUndersection22()
        {
            return await _undersection22Repository.GetAll();
        }

        public async Task<List<Undersection22>> GetUndersection22UsingRepo()
        {
            return await _undersection22Repository.GetUndersection22();
        }

        public async Task<Undersection22> FetchSingleResult(int id)
        {
            var result = await _undersection22Repository.FindBy(a => a.Id == id);
            Undersection22 model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Undersection22 undersection22)
        {
            var result = await _undersection22Repository.FindBy(a => a.Id == id);
            Undersection22 model = result.FirstOrDefault();
            model.NotificationNo = undersection22.NotificationNo;
            model.NotificationDate = undersection22.NotificationDate;
            model.IsActive = undersection22.IsActive;



            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _undersection22Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Undersection22 undersection22)
        {

            undersection22.CreatedBy = 1;
            undersection22.CreatedDate = DateTime.Now;
            _undersection22Repository.Add(undersection22);
            return await _unitOfWork.CommitAsync() > 0;
        }


        //public async Task<bool> CheckUniqueName(int id, string module)
        //{
        //    bool result = await _undersection22Repository.Any(id, module);
        //    //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
        //    return result;
        //}

        public async Task<bool> Delete(int id)
        {
            var form = await _undersection22Repository.FindBy(a => a.Id == id);
            Undersection22 model = form.FirstOrDefault();
            model.IsActive = 0;
            _undersection22Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        
        public async Task<PagedResult<Undersection22>> GetPagedUndersection22(Undersection22SearchDto model)
        {
            return await _undersection22Repository.GetPagedUndersection22(model);
        }
    }
}
