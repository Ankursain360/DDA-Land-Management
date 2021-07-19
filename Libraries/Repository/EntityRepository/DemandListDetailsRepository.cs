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
    public class DemandListDetailsRepository : GenericRepository<Demandlistdetails>, IDemandListDetailsRepository
    {
        public DemandListDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Demandlistdetails>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model)
        {
            var data = await _dbContext.Demandlistdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)
                                        && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                        .GetPaged<Demandlistdetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Demandlistdetails
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
                                .GetPaged<Demandlistdetails>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Demandlistdetails
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
                                .GetPaged<Demandlistdetails>(model.PageNumber, model.PageSize);
            }
            return data;
        }
        public async Task<List<Demandlistdetails>> GetAllDemandlistdetails()
        {
            return await _dbContext.Demandlistdetails
                                   .Include(x => x.Village)
                                   .Include(x => x.KhasraNo)
                                   .ToListAsync();
        }
        public async Task<Demandlistdetails> FetchSingleResult(int id)
        {
            return await _dbContext.Demandlistdetails
                                   .Include(x => x.Appealdetail)
                                   .Include(x => x.Paymentdetail)
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
        public async Task<List<Acquiredlandvillage>> GetVillageList()
        {
            return await _dbContext.Acquiredlandvillage
                                     .Where(x => x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Khasra>> GetKhasraList(int id)
        {
            return await _dbContext.Khasra
                                     .Where(x => x.IsActive == 1 && x.AcquiredlandvillageId == id)
                                     .ToListAsync();
        }

        ///******Appeal*****//////
        public async Task<bool> SaveAppeal(Appealdetail appealdetail)
        {
            _dbContext.Appealdetail.Add(appealdetail);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> UpdateAppeal(int id, Appealdetail appealdetail)
        {
            var result = await FetchSingleAppeal(id);
            Appealdetail model = result;
            model.DemandListNo = appealdetail.DemandListNo;
            model.EnmSno = appealdetail.EnmSno;
            model.AppealNo = appealdetail.AppealNo;
            model.AppealByDept = appealdetail.AppealByDept;
            model.DateOfAppeal = appealdetail.DateOfAppeal;
            model.PanelLawer = appealdetail.PanelLawer;
            model.Department = appealdetail.Department;

            model.IsActive = appealdetail.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = appealdetail.ModifiedBy;

            _dbContext.Appealdetail.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<Appealdetail>> GetAllAppeal(int id)
        {
            return await _dbContext.Appealdetail.Where(x => x.DemandListId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeleteAppeal(int Id)
        {
            _dbContext.Remove(_dbContext.Appealdetail.Where(x => x.DemandListId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Appealdetail> FetchSingleAppeal(int id)
        {
            return await _dbContext.Appealdetail.Where(x => x.DemandListId == id)
                                   .OrderByDescending(s => s.Id)
                                   .FirstOrDefaultAsync();
        }

        /////*****payment*****/////
        public async Task<bool> SavePayment(Paymentdetail paymentdetail)
        {
            _dbContext.Paymentdetail.Add(paymentdetail);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> UpdatePayment(int id, Paymentdetail paymentdetail)
        {
            var result = await FetchSinglePayment(id);
            Paymentdetail model = result;
            model.DemandListNo = paymentdetail.DemandListNo;
            model.EnmSno = paymentdetail.EnmSno;
            model.AmountPaid = paymentdetail.AmountPaid;
            model.ChequeDate = paymentdetail.ChequeDate;
            model.ChequeNo = paymentdetail.ChequeNo;
            model.BankName = paymentdetail.BankName;
            model.VoucherNo = paymentdetail.VoucherNo;
            model.PercentPaid = paymentdetail.PercentPaid;
            model.PaymentProofDocumentName = paymentdetail.PaymentProofDocumentName;

            model.IsActive = paymentdetail.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = paymentdetail.ModifiedBy;

            _dbContext.Paymentdetail.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<Paymentdetail>> GetAllPayment(int id)
        {
            return await _dbContext.Paymentdetail.Where(x => x.DemandListId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> Deletepayment(int Id)
        {
            _dbContext.Remove(_dbContext.Paymentdetail.Where(x => x.DemandListId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Paymentdetail> FetchSinglePayment(int id)
        {
            return await _dbContext.Paymentdetail.Where(x => x.DemandListId == id)
                                   .OrderByDescending(s => s.Id)
                                   .FirstOrDefaultAsync();
        }
    }
}
