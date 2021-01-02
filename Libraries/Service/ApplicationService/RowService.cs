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
    public class RowService : EntityService<Row>,IRowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRowRepository _rowRepository;
        protected readonly DataContext _dbContext;
        public RowService(IUnitOfWork unitOfWork, IRowRepository rowRepository, DataContext dbContext)
      : base(unitOfWork, rowRepository)
        {
            _unitOfWork = unitOfWork;
            _rowRepository = rowRepository;
            _dbContext = dbContext;
        }
        public async Task<List<Row>> GetAllRow()
        {
            return await _rowRepository.GetAll();
        }

        public async Task<List<Row>> GetRowUsingReport()
        {
            return await _rowRepository.GetRow();
        }

        public async Task<bool> Update(int id, Row row)
        {
            var result = await _rowRepository.FindBy(a => a.Id == id);
            Row model = result.FirstOrDefault();
            model.RowNo = row.RowNo;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = row.IsActive;
            model.ModifiedBy = 1;
            _rowRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, Row row)
        {
            var result = _dbContext.Row.Any(t => t.Id != id && t.RowNo == row.RowNo);
            return result;
        }

        public async Task<Row> FetchSingleResult(int id)
        {
            var result = await _rowRepository.FindBy(a => a.Id == id);
            Row model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Row row)
        {

            row.CreatedBy = 1;
            row.CreatedDate = DateTime.Now;
            _rowRepository.Add(row);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string row)
        {
            bool result = await _rowRepository.Any(id, row);

            return result;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _rowRepository.FindBy(a => a.Id == id);
            Row model = form.FirstOrDefault();
            model.IsActive = 0;
            _rowRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Row>> GetPagedRow(RowSearchDto model)
        {
            return await _rowRepository.GetPagedRow(model);
        }

      
    }
}
