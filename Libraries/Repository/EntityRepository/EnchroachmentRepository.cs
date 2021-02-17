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



        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> KhasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return KhasraList;
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
            return await _dbContext.Enchroachment.Include(x => x.Village).Include(x => x.Khasra).Include(x => x.Natureofencroachment).Include(x => x.Reasons).OrderByDescending(x => x.Id).GetPaged<Enchroachment>(model.PageNumber, model.PageSize);
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


    }
}
