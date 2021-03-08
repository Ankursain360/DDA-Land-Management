using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Repository.Common;

namespace Libraries.Repository.EntityRepository

{
    public class EnchroachmentRepository : GenericRepository<Enchroachment>, IEnchroachmentRepository
    {
        public EnchroachmentRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Enchroachment>> GetAllEnchroachment()
        {
            return await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra).Include(x => x.Natureofencroachment).Include(x => x.Reasons).OrderByDescending(x => x.Id).ToListAsync();
        }



        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }
        public async Task<List<Reasons>> GetAllReasons()
        {
            List<Reasons> ReasonsList = await _dbContext.Reasons.Where(x => x.IsActive == 1).ToListAsync();
            return ReasonsList;
        }
        public async Task<List<Natureofencroachment>> GetAllNencroachment()
        {
            List<Natureofencroachment> NencroachmentList = await _dbContext.Natureofencroachment.Where(x => x.IsActive == 1).ToListAsync();
            return NencroachmentList;
        }

        public async Task<PagedResult<Enchroachment>> GetPagedEnchroachment(EnchroachmentSearchDto model)
        {
            //return await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra)
            //    .Include(x => x.Natureofencroachment).Include(x => x.Reasons)
            //    .OrderByDescending(x => x.Id).GetPaged<Enchroachment>(model.PageNumber, model.PageSize);

            var data = await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra)
                .Include(x => x.Natureofencroachment).Include(x => x.Reasons)
                .OrderByDescending(x => x.Id).GetPaged<Enchroachment>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra)
                                .Include(x => x.Natureofencroachment).Include(x => x.Reasons)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.Village.Name.Contains(model.name))
                                .OrderBy(s => s.Village.Name)
                                .GetPaged<Enchroachment>(model.PageNumber, model.PageSize);
                                            break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra)
                                .Include(x => x.Natureofencroachment).Include(x => x.Reasons)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.Village.Name.Contains(model.name))
                                .OrderBy(s => s.Khasra.Name)
                                .GetPaged<Enchroachment>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra)
                                .Include(x => x.Natureofencroachment).Include(x => x.Reasons)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.Village.Name.Contains(model.name))
                               .OrderBy(x => x.IsActive == 0)
                                .GetPaged<Enchroachment>(model.PageNumber, model.PageSize);
                            
                        break;
                      }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra)
                                .Include(x => x.Natureofencroachment).Include(x => x.Reasons)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.Village.Name.Contains(model.name))
                                .OrderByDescending(s => s.Village.Name)
                                .GetPaged<Enchroachment>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra)
                                .Include(x => x.Natureofencroachment).Include(x => x.Reasons)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.Village.Name.Contains(model.name))
                                .OrderByDescending(s => s.Khasra.Name)
                                .GetPaged<Enchroachment>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra)
                                .Include(x => x.Natureofencroachment).Include(x => x.Reasons)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.Village.Name.Contains(model.name))
                               .OrderByDescending(x => x.IsActive == 0)
                                .GetPaged<Enchroachment>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            return data;
        }
        public async Task<List<EncrochpeopleListDataDto>> GetPagedEncrocherPeople(EncrocherNameSearchDto model, int UserId)
        {
            //return await _dbContext.EncrocherPeople
            //    .Where(x => x.FileNo == model.fileno)
            //    .OrderByDescending(x => x.Id).GetPaged<EncrocherPeople>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("BindEncrocmentpeople")
                                             .WithSqlParams(("File_No", model.fileno),
                                               ("Name", model.name),
                                              ("Address", model.address),
                                              ("Recstate", model.Recstate),
                                              ("Cdate", DateTime.Now),
                                                ("UserId", UserId)               )
                                             .ExecuteStoredProcedureAsync<EncrochpeopleListDataDto>();
            return (List<EncrochpeopleListDataDto>)data;
        }

        //********* repeater ! Owner Details **********

        public async Task<bool> SaveEName(EncrocherPeople encrocherPeople)
        {
            _dbContext.EncrocherPeople.Add(encrocherPeople);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<EncrocherPeople>> GetAllOwner(int id)
        {
            return await _dbContext.EncrocherPeople.Where(x => x.EnchId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeleteOwner(int Id)
        {
            _dbContext.RemoveRange(_dbContext.EncrocherPeople.Where(x => x.EnchId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        //********* repeater ! Payment Details **********

        public async Task<bool> SavePayment(Enchroachmentpayment enchroachmentpayment)
        {
            _dbContext.Enchroachmentpayment.Add(enchroachmentpayment);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Enchroachmentpayment>> GetAllPayment(int id)
        {
            return await _dbContext.Enchroachmentpayment.Where(x => x.EnchId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeletePayment(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Enchroachmentpayment.Where(x => x.EnchId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
    }
}
