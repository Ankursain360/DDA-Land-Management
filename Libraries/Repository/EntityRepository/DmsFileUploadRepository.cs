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
    public class DmsFileUploadRepository : GenericRepository<Dmsfileupload>, IDmsFileUploadRepository
    {
        public DmsFileUploadRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Department>> GetDepartmentList()
        {
            return await _dbContext.Department
                                     .Where(x => x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Propertyregistration>> GetKhasraNoList()
        {
            try
            {
                var InId = (from x in _dbContext.Propertyregistration
                            where x.KhasraNo == null || x.KhasraNo == string.Empty
                            select x.Id).ToArray();

                var data =  await _dbContext.Propertyregistration
                                         .Where(x => x.IsActive == 1 && x.IsDeleted != 0 && x.IsValidate == 1 && x.IsDisposed != 0
                                         // && ( x.KhasraNo != null || x.KhasraNo != string.Empty)
                                         && !(InId).Contains(x.Id)
                                         )
                                         .ToListAsync();
                var result = data.GroupBy(x => x)
                                  .Where(g => g.Count() > 1)
                                  .Select(y => y.Key)
                                  .ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<Dmsfileupload>> GetAllDMSFileUploadList()
        {
            return await _dbContext.Dmsfileupload
                 .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                          .Include(x => x.Zone)
                                        .Include(x => x.Village)
                .ToListAsync();
        }

        public async Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model)
        {
            var data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                         .Include(x => x.Zone)
                                        .Include(x => x.Village)
                                        .Include(x => x.Category)
                                        .Where(x => x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId)
                                        && (x.LocalityId == (model.localityId == 0 ? x.LocalityId : model.localityId))
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                        .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                           .Include(x => x.Zone)
                                        .Include(x => x.Village)
                                        .Include(x => x.Category)
                                        .Where(x => x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId)
                                        && (x.LocalityId == (model.localityId == 0 ? x.LocalityId : model.localityId))
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                : model.SortBy.ToUpper() == "DEPARTMENT" ? (s.Department == null ? null : s.Department.Name)
                                : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality != null ? s.Locality.Name : null)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.KhasraNo : null) : s.FileNo)
                                )
                                .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                           .Include(x => x.Zone)
                                        .Include(x => x.Village)
                                        .Include(x => x.Category)
                                        .Where(x => x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId)
                                        && (x.LocalityId == (model.localityId == 0 ? x.LocalityId : model.localityId))
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                .OrderByDescending(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                : model.SortBy.ToUpper() == "DEPARTMENT" ? (s.Department == null ? null : s.Department.Name)
                                : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality != null ? s.Locality.Name : null)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.KhasraNo : null) : s.FileNo)
                                )
                                .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            }
            return data;
        }

        public async Task<Dmsfileupload> FetchSingleResult(int id)
        {
            return await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                        .Include(x => x.Category)
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

        public int GetZoneByName(string name)
        {
            var File = (from f in _dbContext.Zone
                        where f.Name.ToUpper().Trim() == name.ToUpper().Trim()
                        select f.Id).FirstOrDefault();

            return File;
        }

        public int GetVillageByName(string name)
        {
            var File = (from f in _dbContext.Village
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
            return await _dbContext.Dmsfileupload.AnyAsync(t => t.Id != id &&  t.FileNo.ToLower() == fileNo.ToLower());
        }

        public async Task<PagedResult<Dmsfileupload>> GetPagedDMSRetriveFileReport(DMSRetriveFileSearchDto model)
        {

            var data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                        .Include(x => x.Category)
                                        .Where(x => x.DepartmentId == (model.Department == 0 ? x.DepartmentId : model.Department)
                                        && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                        && (x.KhasraNoId == (model.Khasra == 0 ? x.KhasraNoId : model.Khasra))
                                        && (x.FileNo.ToUpper().Trim().Contains(model.FileNo == "" ? x.FileNo.ToUpper().Trim() : model.FileNo.ToUpper().Trim()))
                                        && (x.PropertyNoAddress.ToUpper().Trim().Contains(model.PropertyNo == "" ? x.PropertyNoAddress.ToUpper().Trim() : model.PropertyNo.ToUpper().Trim()))
                                        && (x.AlmirahNo.ToUpper().Trim().Contains(model.AlmirahNo == "" ? x.AlmirahNo.ToUpper().Trim() : model.AlmirahNo.ToUpper().Trim()))
                                        && (x.Title.ToUpper().Trim().Contains(model.Title == "" ? x.Title.ToUpper().Trim() : model.Title.ToUpper().Trim()))
                                        )
                                        .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                        .Include(x => x.Category)
                                        .Where(x => x.DepartmentId == (model.Department == 0 ? x.DepartmentId : model.Department)
                                        && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                        && (x.KhasraNoId == (model.Khasra == 0 ? x.KhasraNoId : model.Khasra))
                                        && (x.FileNo.ToUpper().Trim().Contains(model.FileNo == "" ? x.FileNo.ToUpper().Trim() : model.FileNo.ToUpper().Trim()))
                                        && (x.PropertyNoAddress.ToUpper().Trim().Contains(model.PropertyNo == "" ? x.PropertyNoAddress.ToUpper().Trim() : model.PropertyNo.ToUpper().Trim()))
                                        && (x.AlmirahNo.ToUpper().Trim().Contains(model.AlmirahNo == "" ? x.AlmirahNo.ToUpper().Trim() : model.AlmirahNo.ToUpper().Trim()))
                                        && (x.Title.ToUpper().Trim().Contains(model.Title == "" ? x.Title.ToUpper().Trim() : model.Title.ToUpper().Trim()))
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                : model.SortBy.ToUpper() == "DEPARTMENT" ? (s.Department == null ? null : s.Department.Name)
                                : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality != null ? s.Locality.Name : null)
                                //: model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.KhasraNo : null)
                                : s.FileNo)
                                )
                                .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Dmsfileupload
                                        .Include(x => x.Department)
                                        .Include(x => x.Locality)
                                        .Include(x => x.KhasraNo)
                                        .Include(x => x.Category)
                                        .Where(x => x.DepartmentId == (model.Department == 0 ? x.DepartmentId : model.Department)
                                        && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                        && (x.KhasraNoId == (model.Khasra == 0 ? x.KhasraNoId : model.Khasra))
                                        && (x.FileNo.ToUpper().Trim().Contains(model.FileNo == "" ? x.FileNo.ToUpper().Trim() : model.FileNo.ToUpper().Trim()))
                                        && (x.PropertyNoAddress.ToUpper().Trim().Contains(model.PropertyNo == "" ? x.PropertyNoAddress.ToUpper().Trim() : model.PropertyNo.ToUpper().Trim()))
                                        && (x.AlmirahNo.ToUpper().Trim().Contains(model.AlmirahNo == "" ? x.AlmirahNo.ToUpper().Trim() : model.AlmirahNo.ToUpper().Trim()))
                                        && (x.Title.ToUpper().Trim().Contains(model.Title == "" ? x.Title.ToUpper().Trim() : model.Title.ToUpper().Trim()))
                                        )
                                .OrderByDescending(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                : model.SortBy.ToUpper() == "DEPARTMENT" ? (s.Department == null ? null : s.Department.Name)
                                : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality != null ? s.Locality.Name : null)
                                // : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.KhasraNo : null) 
                                : s.FileNo)
                                )
                                .GetPaged<Dmsfileupload>(model.PageNumber, model.PageSize);
            }
            return data;
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            return await _dbContext.Locality
                                     .Where(x => x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<Dmsfileright> GetDMSUserRights(int userId)
        {
            return await _dbContext.Dmsfileright
                                    .Where(x => x.UserId == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<Zone>> allZoneList()
        {
            return await _dbContext.Zone
                                     .Where(x => x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Village>> allVillageList(int? zoneid)
        {
            return await _dbContext.Village
                                     .Where(x => x.IsActive == 1 && x.ZoneId == zoneid)
                                     .ToListAsync();
        }

        public async Task<List<Documentcategory>> allcategoryList()
        {
            return await _dbContext.documentcategory.Where(x => x.IsActive == 1).ToListAsync();
        }

    }
}
