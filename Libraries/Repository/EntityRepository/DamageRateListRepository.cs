using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class DamageRateListRepository : GenericRepository<Resratelisttypea>, IDamageRateListRepository
    {

        public DamageRateListRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<DamageRateListDataDto>> GetSearchResultPagedData(DamageRateListSearchDto model, List<DamageRateListDataDto> DamageRateListDataDto)
        {
            dynamic result1 = null;
            Encrochmenttype encrochmenttype = new Encrochmenttype();

            var propertytype = await _dbContext.PropertyType
                                    .Where(x => x.Id == model.propertyid)
                                    .FirstOrDefaultAsync();

            if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential) //on the basis of sunencroachment type find EncroachmentType data
            {
                var data = await _dbContext.Ressubencroacherstype
                                   .Where(a => a.IsActive == 1 && a.Id == model.daterangeid)
                                   .FirstOrDefaultAsync();
                encrochmenttype = await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= data.EncroachStartDate && x.EncroachEndDate >= data.EncroachStartDate)
                                .FirstOrDefaultAsync();
            }
            else
            {
                var data = await _dbContext.Comsubencroacherstype
                                   .Where(a => a.IsActive == 1 && a.Id == model.daterangeid)
                                   .FirstOrDefaultAsync();

                encrochmenttype = await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= data.EncroachStartDate && x.EncroachEndDate >= data.EncroachStartDate)
                                .FirstOrDefaultAsync();
            }

            if (encrochmenttype.Id == 1) // on the basis of EncroachmentType choose table type of either residential or commercial
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    result1 = await _dbContext.Resratelisttypea
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
                else
                {
                    result1 = await _dbContext.Comratelisttypea
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
            }
            else if (encrochmenttype.Id == 2)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    result1 = await _dbContext.Resratelisttypeb
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
                else
                {
                    result1 = await _dbContext.Comratelisttypeb
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
            }
            else if (encrochmenttype.Id == 3)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    result1 = await _dbContext.Resratelisttypec
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
                else
                {
                    result1 = await _dbContext.Comratelisttypec
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
            }

            if (result1 != null) //Final data add in dto
            {
                for (int i = 0; i < result1.Count; i++)
                {
                    DamageRateListDataDto.Add(new DamageRateListDataDto
                    {
                        Id = result1[i].Id,
                        FromDate = result1[i].StartDate,
                        ToDate = result1[i].EndDate,
                        Rate = result1[i].Rate,
                        EncroachmentTypeId = result1[i].EncroachId,
                        LocalityId = result1[i].ColonyId,
                        SubEncroachmentId = result1[i].SubEncroachId,
                        PropertypeId = model.propertyid
                    });
                }
            }

            return DamageRateListDataDto;
        }
        public async Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListCommercial()
        {
            List<DateRangeConcatedListDto> listData = new List<DateRangeConcatedListDto>();

            var Data = await _dbContext.Comsubencroacherstype
                                   .Where(a => a.IsActive == 1)
                                   .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new DateRangeConcatedListDto()
                    {
                        Id = Data[i].Id,
                        StartDate = Data[i].EncroachStartDate,
                        EndDate = Data[i].EncroachEndDate,
                        Name = string.Format("{0} To {1}", Data[i].EncroachStartDate.ToString("dd/MMM/yyyy"), Data[i].EncroachEndDate.ToString("dd/MMM/yyyy"))
                    });
                }
            }

            return listData;
        }

        public async Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListResidential()
        {
            List<DateRangeConcatedListDto> listData = new List<DateRangeConcatedListDto>();

            var Data = await _dbContext.Ressubencroacherstype
                                   .Where(a => a.IsActive == 1)
                                   .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new DateRangeConcatedListDto()
                    {
                        Id = Data[i].Id,
                        StartDate = Data[i].EncroachStartDate,
                        EndDate = Data[i].EncroachEndDate,
                        Name = string.Format("{0} To {1}", Data[i].EncroachStartDate.ToString("dd/MMM/yyyy"), Data[i].EncroachEndDate.ToString("dd/MMM/yyyy"))
                    });
                }
            }

            return listData;
        }

        public async Task<List<Locality>> GetLocalities()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }

        public async Task<List<PropertyType>> GetPropertyTypes()
        {
            var propertytypeList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertytypeList;
        }

        public async Task<PropertyType> FetchSinglePropertyType(int id)
        {
            return await _dbContext.PropertyType
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<bool> Create(DamageRateListCreateDto damageRateListCreateDto)
        {
            Encrochmenttype encrochmenttype = new Encrochmenttype();

            var propertytype = await _dbContext.PropertyType
                                    .Where(x => x.Id == damageRateListCreateDto.PropertyId)
                                    .FirstOrDefaultAsync();

            if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential) //on the basis of sunencroachment type find EncroachmentType data
            {
                var data = await _dbContext.Ressubencroacherstype
                                   .Where(a => a.IsActive == 1 && a.Id == damageRateListCreateDto.DateRangeId)
                                   .FirstOrDefaultAsync();
                encrochmenttype = await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= data.EncroachStartDate && x.EncroachEndDate >= data.EncroachStartDate)
                                .FirstOrDefaultAsync();
            }
            else
            {
                var data = await _dbContext.Comsubencroacherstype
                                   .Where(a => a.IsActive == 1 && a.Id == damageRateListCreateDto.DateRangeId)
                                   .FirstOrDefaultAsync();

                encrochmenttype = await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= data.EncroachStartDate && x.EncroachEndDate >= data.EncroachStartDate)
                                .FirstOrDefaultAsync();
            }

            if (encrochmenttype.Id == 1) // on the basis of EncroachmentType choose table type of either residential or commercial
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    Resratelisttypea model = new Resratelisttypea();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.CreatedBy = damageRateListCreateDto.CreatedBy;
                    model.CreatedDate = DateTime.Now;
                    await _dbContext.Resratelisttypea.AddAsync(model);
                }
                else
                {
                    Comratelisttypea model = new Comratelisttypea();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.CreatedBy = damageRateListCreateDto.CreatedBy;
                    model.CreatedDate = DateTime.Now;
                    await _dbContext.Comratelisttypea.AddAsync(model);
                }
            }
            else if (encrochmenttype.Id == 2)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    Resratelisttypeb model = new Resratelisttypeb();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.CreatedBy = damageRateListCreateDto.CreatedBy;
                    model.CreatedDate = DateTime.Now;
                    await _dbContext.Resratelisttypeb.AddAsync(model);
                }
                else
                {
                    Comratelisttypeb model = new Comratelisttypeb();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.CreatedBy = damageRateListCreateDto.CreatedBy;
                    model.CreatedDate = DateTime.Now;
                    await _dbContext.Comratelisttypeb.AddAsync(model);
                }
            }
            else if (encrochmenttype.Id == 3)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    Resratelisttypec model = new Resratelisttypec();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.CreatedBy = damageRateListCreateDto.CreatedBy;
                    model.CreatedDate = DateTime.Now;
                    await _dbContext.Resratelisttypec.AddAsync(model);
                }
                else
                {
                    Comratelisttypec model = new Comratelisttypec();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.CreatedBy = damageRateListCreateDto.CreatedBy;
                    model.CreatedDate = DateTime.Now;
                    await _dbContext.Comratelisttypec.AddAsync(model);
                }
            }
                       
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> Update(DamageRateListCreateDto damageRateListCreateDto)
        {
            Encrochmenttype encrochmenttype = new Encrochmenttype();

            var propertytype = await _dbContext.PropertyType
                                    .Where(x => x.Id == damageRateListCreateDto.PropertyId)
                                    .FirstOrDefaultAsync();

            if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential) //on the basis of sunencroachment type find EncroachmentType data
            {
                var data = await _dbContext.Ressubencroacherstype
                                   .Where(a => a.IsActive == 1 && a.Id == damageRateListCreateDto.DateRangeId)
                                   .FirstOrDefaultAsync();
                encrochmenttype = await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= data.EncroachStartDate && x.EncroachEndDate >= data.EncroachStartDate)
                                .FirstOrDefaultAsync();
            }
            else
            {
                var data = await _dbContext.Comsubencroacherstype
                                   .Where(a => a.IsActive == 1 && a.Id == damageRateListCreateDto.DateRangeId)
                                   .FirstOrDefaultAsync();

                encrochmenttype = await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= data.EncroachStartDate && x.EncroachEndDate >= data.EncroachStartDate)
                                .FirstOrDefaultAsync();
            }

            if (encrochmenttype.Id == 1) // on the basis of EncroachmentType choose table type of either residential or commercial
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    Resratelisttypea model =await  _dbContext.Resratelisttypea.Where(a => a.Id == damageRateListCreateDto.Id).FirstOrDefaultAsync();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.ModifiedBy = damageRateListCreateDto.ModifiedBy;
                    model.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Comratelisttypea model = await _dbContext.Comratelisttypea.Where(a => a.Id == damageRateListCreateDto.Id).FirstOrDefaultAsync();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.ModifiedBy = damageRateListCreateDto.ModifiedBy;
                    model.ModifiedDate = DateTime.Now;
                }
            }
            else if (encrochmenttype.Id == 2)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    Resratelisttypeb model = await _dbContext.Resratelisttypeb.Where(a => a.Id == damageRateListCreateDto.Id).FirstOrDefaultAsync();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.ModifiedBy = damageRateListCreateDto.ModifiedBy;
                    model.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Comratelisttypeb model = await _dbContext.Comratelisttypeb.Where(a => a.Id == damageRateListCreateDto.Id).FirstOrDefaultAsync();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.ModifiedBy = damageRateListCreateDto.ModifiedBy;
                    model.ModifiedDate = DateTime.Now;
                }
            }
            else if (encrochmenttype.Id == 3)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    Resratelisttypec model = await _dbContext.Resratelisttypec.Where(a => a.Id == damageRateListCreateDto.Id).FirstOrDefaultAsync();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.ModifiedBy = damageRateListCreateDto.ModifiedBy;
                    model.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Comratelisttypec model = await _dbContext.Comratelisttypec.Where(a => a.Id == damageRateListCreateDto.Id).FirstOrDefaultAsync();
                    model.EncroachId = encrochmenttype.Id;
                    model.ColonyId = damageRateListCreateDto.LocalityId;
                    model.StartDate = Convert.ToDateTime(damageRateListCreateDto.StartDate);
                    model.EndDate = Convert.ToDateTime(damageRateListCreateDto.EndDate);
                    model.SubEncroachId = damageRateListCreateDto.DateRangeId;
                    model.Rate = damageRateListCreateDto.Rate;
                    model.IsActive = 1;
                    model.ModifiedBy = damageRateListCreateDto.ModifiedBy;
                    model.ModifiedDate = DateTime.Now;
                }
            }

            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<dynamic> FetchSingleResult(int id, int EncroachmentTypeId, int LocalityId, int PropertypeId)
        {
            dynamic result1 = null;
            Encrochmenttype encrochmenttype = new Encrochmenttype();

            var propertytype = await _dbContext.PropertyType
                                    .Where(x => x.Id == PropertypeId)
                                    .FirstOrDefaultAsync();
                       
            if (EncroachmentTypeId == 1) // on the basis of EncroachmentType choose table type of either residential or commercial
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    result1 = await _dbContext.Resratelisttypea
                                            .Where(x => x.Id == id )
                                            .FirstOrDefaultAsync();
                }
                else
                {
                    result1 = await _dbContext.Comratelisttypea
                                              .Where(x => x.Id == id)
                                              .FirstOrDefaultAsync();
                }
            }
            else if (EncroachmentTypeId == 2)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    result1 = await _dbContext.Resratelisttypeb
                                            .Where(x => x.Id == id)
                                              .FirstOrDefaultAsync();
                }
                else
                {
                    result1 = await _dbContext.Comratelisttypeb
                                            .Where(x => x.Id == id)
                                              .FirstOrDefaultAsync();
                }
            }
            else if (EncroachmentTypeId == 3)
            {
                if (propertytype.StatusCode == (int)PropertyTypeStatus.Residential)
                {
                    result1 = await _dbContext.Resratelisttypec
                                            .Where(x => x.Id == id)
                                              .FirstOrDefaultAsync();
                }
                else
                {
                    result1 = await _dbContext.Comratelisttypec
                                            .Where(x => x.Id == id)
                                              .FirstOrDefaultAsync();
                }
            }

            return result1;
        }
    }
}
