using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class HonbleService : EntityService<Honble>, IHonbleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHonbleRepository _HonbleRepository;

        public HonbleService(IUnitOfWork unitOfWork, IHonbleRepository HonbleRepository) : base(unitOfWork, HonbleRepository)
        {
            _unitOfWork = unitOfWork;
            _HonbleRepository = HonbleRepository;
        }

        public async Task<List<Honble>> GetAllHonble()
        {
            return await _HonbleRepository.GetAllHonble();
        }


        public async Task<Honble> FetchSingleResult(int id)
        {
            var result = await _HonbleRepository.FindBy(a => a.Id == id);
            Honble model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Honble rent)
        {
            var result = await _HonbleRepository.FindBy(a => a.Id == id);
            Honble model = result.FirstOrDefault();
            model.HonbleName = rent.HonbleName;


            model.IsActive = rent.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = rent.ModifiedBy;
            _HonbleRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Honble rate)
        {
            rate.CreatedBy = rate.CreatedBy;

            rate.CreatedDate = DateTime.Now;
            _HonbleRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _HonbleRepository.FindBy(a => a.Id == id);
            Honble model = form.FirstOrDefault();
            model.IsActive = 0;
            _HonbleRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Honble>> GetPagedHonble(HonbleSearchDto model)
        {
            return await _HonbleRepository.GetPagedHonble(model);
        }
    }
}
