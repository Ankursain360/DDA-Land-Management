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
    public class ColumnService : EntityService<Column>, IColumnService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IColumnRepository _columnRepository;
        protected readonly DataContext _dbContext;


        public ColumnService(IUnitOfWork unitOfWork, IColumnRepository columnRepository, DataContext dbContext)
       : base(unitOfWork, columnRepository)
        {
            _unitOfWork = unitOfWork;
            _columnRepository = columnRepository;
            _dbContext = dbContext;
        }

        public async Task<List<Column>> GetAllColumn()
        {
            return await _columnRepository.GetAll();
        }

        public async Task<List<Column>> GetColumnUsingReport()
        {
            return await _columnRepository.GetColumn();
        }

        public async Task<bool> Update(int id, Column column)
        {
            var result = await _columnRepository.FindBy(a => a.Id == id);
            Column model = result.FirstOrDefault();
            model.ColumnNo = column.ColumnNo;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = column.IsActive;
            model.ModifiedBy = 1;
            _columnRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, Column column)
        {
            var result = _dbContext.Column.Any(t => t.Id != id && t.ColumnNo == column.ColumnNo);
            return result;
        }

        public async Task<Column> FetchSingleResult(int id)
        {
            var result = await _columnRepository.FindBy(a => a.Id == id);
            Column model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Column column)
        {

            column.CreatedBy = 1;
            column.CreatedDate = DateTime.Now;
            _columnRepository.Add(column);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string column)
        {
            bool result = await _columnRepository.Any(id, column);

            return result;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _columnRepository.FindBy(a => a.Id == id);
            Column model = form.FirstOrDefault();
            model.IsActive = 0;
            _columnRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Column>> GetPagedColumn(ColumnSearchDto model)
        {
            return await _columnRepository.GetPagedColumn(model);
        }

     
    }
}
