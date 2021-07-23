using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class NewLandDemandListDetailsRepository : GenericRepository<Newlanddemandlistdetails>, INewLandDemandListDetailsRepository
    {
        public NewLandDemandListDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlanddemandlistdetails>> GetPagedDMSFileUploadList(NewLandDemandListDetailsSearchDto model)
        {
            var data = await _dbContext.Newlanddemandlistdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)
                                        && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                        .GetPaged<Newlanddemandlistdetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Newlanddemandlistdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)
                                        && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "DEMANDLIST" ? s.DemandListNo
                                : model.SortBy.ToUpper() == "VILLAGE" ? (s.Village == null ? null : s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.Name : null) : s.DemandListNo)
                                )
                                .GetPaged<Newlanddemandlistdetails>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Newlanddemandlistdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)
                                        && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                .OrderByDescending(s =>
                                 (model.SortBy.ToUpper() == "DEMANDLIST" ? s.DemandListNo
                                : model.SortBy.ToUpper() == "VILLAGE" ? (s.Village == null ? null : s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.Name : null) : s.DemandListNo)
                                )
                                .GetPaged<Newlanddemandlistdetails>(model.PageNumber, model.PageSize);
            }
            return data;
        }
        public async Task<List<Newlanddemandlistdetails>> GetAllDemandlistdetails()
        {
            return await _dbContext.Newlanddemandlistdetails
                                   .Include(x => x.Village)
                                   .Include(x => x.KhasraNo)
                                   .ToListAsync();
        }
        public async Task<Newlanddemandlistdetails> FetchSingleResult(int id)
        {
            return await _dbContext.Newlanddemandlistdetails
                                       .Include(x => x.Newlandappealdetail)
                                   .Include(x => x.Newlandpaymentdetail)
                                   .Include(x => x.Village)
                                   .Include(x => x.KhasraNo)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();


        }

        public int GetLocalityByName(string name)
        {
            var File = (from f in _dbContext.Locality
                        where f.Name.ToUpper().Trim() == name.ToUpper().Trim()
                        select f.Id).FirstOrDefault();

            return File;
        }

        public int GetKhasraByName(string name)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.KhasraNo.ToUpper().Trim() == name.ToUpper().Trim()
                        select f.Id).FirstOrDefault();

            return File;
        }

        public async Task<bool> Any(int id, string fileNo)
        {
            return await _dbContext.Dmsfileupload.AnyAsync(t => t.Id != id && t.FileNo.ToLower() == fileNo.ToLower());
        }
        public async Task<List<Newlandvillage>> GetVillageList()
        {
            return await _dbContext.Newlandvillage
                                     .Where(x => x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Newlandkhasra>> GetKhasraList(int id)
        {
            return await _dbContext.Newlandkhasra
                                     .Where(x => x.IsActive == 1 && x.NewLandvillageId == id)
                                     .ToListAsync();
        }
        ///******Appeal*****//////
        public async Task<bool> SaveAppeal(Newlandappealdetail newlandappealdetail)
        {
            _dbContext.Newlandappealdetail.Add(newlandappealdetail);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> UpdateAppeal(int id, Newlandappealdetail newlandappealdetail)
        {
            var result = await FetchSingleAppeal(id);
            Newlandappealdetail model = result;
            model.DemandListNo = newlandappealdetail.DemandListNo;
            model.EnmSno = newlandappealdetail.EnmSno;
            model.AppealNo = newlandappealdetail.AppealNo;
            model.AppealByDept = newlandappealdetail.AppealByDept;
            model.DateOfAppeal = newlandappealdetail.DateOfAppeal;
            model.PanelLawer = newlandappealdetail.PanelLawer;
            model.Department = newlandappealdetail.Department;

            model.IsActive = newlandappealdetail.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = newlandappealdetail.ModifiedBy;

            _dbContext.Newlandappealdetail.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<Newlandappealdetail>> GetAllAppeal(int id)
        {
            return await _dbContext.Newlandappealdetail.Where(x => x.DemandListId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeleteAppeal(int Id)
        {
            _dbContext.Remove(_dbContext.Newlandappealdetail.Where(x => x.DemandListId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Newlandappealdetail> FetchSingleAppeal(int id)
        {
            return await _dbContext.Newlandappealdetail.Where(x => x.DemandListId == id)
                                   .OrderByDescending(s => s.Id)
                                   .FirstOrDefaultAsync();
        }

        /////*****payment*****/////
        public async Task<bool> SavePayment(Newlandpaymentdetail newlandpaymentdetail)
        {
            _dbContext.Newlandpaymentdetail.Add(newlandpaymentdetail);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> UpdatePayment(int id, Newlandpaymentdetail  newlandpaymentdetail)
        {
            var result = await FetchSinglePayment(id);
            Newlandpaymentdetail model = result;
            model.DemandListNo = newlandpaymentdetail.DemandListNo;
            model.EnmSno = newlandpaymentdetail.EnmSno;
            model.AmountPaid = newlandpaymentdetail.AmountPaid;
            model.ChequeDate = newlandpaymentdetail.ChequeDate;
            model.ChequeNo = newlandpaymentdetail.ChequeNo;
            model.BankName = newlandpaymentdetail.BankName;
            model.VoucherNo = newlandpaymentdetail.VoucherNo;
            model.PercentPaid = newlandpaymentdetail.PercentPaid;
            model.PaymentProofDocumentName = newlandpaymentdetail.PaymentProofDocumentName;

            model.IsActive = newlandpaymentdetail.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = newlandpaymentdetail.ModifiedBy;

            _dbContext.Newlandpaymentdetail.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<Newlandpaymentdetail>> GetAllPayment(int id)
        {
            return await _dbContext.Newlandpaymentdetail.Where(x => x.DemandListId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> Deletepayment(int Id)
        {
            _dbContext.Remove(_dbContext.Newlandpaymentdetail.Where(x => x.DemandListId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Newlandpaymentdetail> FetchSinglePayment(int id)
        {
            return await _dbContext.Newlandpaymentdetail.Where(x => x.DemandListId == id)
                                   .OrderByDescending(s => s.Id)
                                   .FirstOrDefaultAsync();
        }
        public async Task<Newlandpaymentdetail> GetPaymentProofDocument(int Id)
        {
            return await _dbContext.Newlandpaymentdetail.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
    }
}
