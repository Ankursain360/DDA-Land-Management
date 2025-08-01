﻿using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
     
    public class KycformRepository : GenericRepository<Kycform>, IKycformRepository
    {
        public KycformRepository(DataContext dbContext) : base(dbContext)
        {

        }
       
        public async Task<List<Leasetype>> GetAllLeasetypeList()
        {
            List<Leasetype> List = await _dbContext.Leasetype.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
        public async Task<List<Branch>> GetAllBranchList()
        {
            List<Branch> List = await _dbContext.Branch.Where(x => x.IsActive == 1&& x.DepartmentId == 50).ToListAsync();
            return List;
        }
        public async Task<List<PropertyType>> GetAllPropertyTypeList()
        {
            List<PropertyType> List = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
        public async Task<List<Zone>> GetAllZoneList()
        {
            List<Zone> List = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
        public async Task<List<Locality>> GetLocalityList(int? zoneid)
        {
            List<Locality> List = await _dbContext.Locality.Where(x => x.ZoneId == zoneid && x.IsActive == 1).ToListAsync();
            return List;
        }
        public async Task<List<Branch>> GetAllBranch(int? propertyTypeId)
        {
            List<Branch> List = await _dbContext.Branch.Where(x => x.PropertytypeId == propertyTypeId && x.IsActive == 1 && x.DepartmentId == 50).ToListAsync();
            return List;
        }
        


        public async Task<List<Kycform>> GetAllKycform()
        {
            return await _dbContext.Kycform
                                   .Include(x =>x.Branch)
                                   .Include(x => x.LeaseType)
                                   .Include(x => x.Locality)
                                   .Include(x => x.PropertyType)
                                   .Include(x => x.Zone)
                                   .Include(x => x.ApprovedStatusNavigation)
                                   .Where(x => x.IsActive == 1)
                                   .ToListAsync();
        }


        public async Task<List<Kycform>> GetAlldownloadKycform(string mobileno)
        {
            return await _dbContext.Kycform
                                   .Include(x => x.Branch)
                                   .Include(x => x.LeaseType)
                                   .Include(x => x.Locality)
                                   .Include(x => x.PropertyType)
                                   .Include(x => x.Zone)
                                   .Include(x => x.ApprovedStatusNavigation)
                                   .Where(x => x.IsActive == 1 && x.MobileNo==mobileno)
                                   .ToListAsync();
        }
        public async Task<Kycform> FetchKYCSingleResult(int id)
        {
            var data = await _dbContext.Kycform
                                       .Include(x => x.Branch)
                                       .Include(x => x.LeaseType)
                                       .Include(x => x.Locality)
                                       .Include(x => x.PropertyType)
                                       .Include(x => x.Zone)
                                       .Where(x => x.Id == id)
                                       .FirstOrDefaultAsync();
            return data;
        }
        public async Task<PagedResult<Kycform>> GetPagedKycform(KycformSearchDto model)
        {

            try
            {
                var data = await _dbContext.Kycform
                                       .Include(x => x.Branch)
                                       .Include(x => x.LeaseType)
                                       .Include(x => x.Locality)
                                       .Include(x => x.PropertyType)
                                       .Include(x => x.Zone)
                                       .Include(x => x.ApprovedStatusNavigation)
                                       .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                       && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                       && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                       && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                       && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                       && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                       && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                       )
                                       .GetPaged<Kycform>(model.PageNumber, model.PageSize);




                int SortOrder = (int)model.SortOrder;
                if (SortOrder == 1)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("NAME"):
                            data = null;
                            data = await _dbContext.Kycform
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.LeaseType)
                                                   .Include(x => x.Locality)
                                                   .Include(x => x.PropertyType)
                                                   .Include(x => x.Zone)
                                                   .Include(x => x.ApprovedStatusNavigation)
                                                   .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                                    && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                                   && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                   && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                                   && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                                   && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                                   && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                                   ).OrderBy(x => x.Property)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;
                        case ("STATUS"):
                            data = null;
                            data = await _dbContext.Kycform
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.LeaseType)
                                                   .Include(x => x.Locality)
                                                   .Include(x => x.PropertyType)
                                                   .Include(x => x.Zone)
                                                   .Include(x => x.ApprovedStatusNavigation)
                                                   .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                                    && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                                   && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                   && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                                   && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                                   && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                                   && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                                   ).OrderByDescending(x => x.ApprovedStatus)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;

                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("NAME"):
                            data = null;
                            data = await _dbContext.Kycform
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.LeaseType)
                                                   .Include(x => x.Locality)
                                                   .Include(x => x.PropertyType)
                                                   .Include(x => x.Zone)
                                                   .Include(x => x.ApprovedStatusNavigation)
                                                   .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                                    && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                                   && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                   && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                                   && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                                   && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                                   && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                                   ).OrderByDescending(x => x.Property)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;
                        case ("STATUS"):
                            data = null;
                            data = await _dbContext.Kycform
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.LeaseType)
                                                   .Include(x => x.Locality)
                                                   .Include(x => x.PropertyType)
                                                   .Include(x => x.Zone)
                                                   .Include(x => x.ApprovedStatusNavigation)
                                                   .Where(x => x.IsActive == 1 && x.MobileNo == model.Mobileno.ToString()
                                                    && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                                   && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                   && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                                   && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                                   && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                                   && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                                   ).OrderBy(x => x.ApprovedStatus)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;
                    }
                }
                return data;





            }
            catch (Exception ex)
            {
                throw;

            }
        }


        //For Internal Users
        public async Task<PagedResult<Kycform>> GetPagedKycformForInternalUser(KycformSearchDto model)
        {

            try
            {
                var data = await _dbContext.Kycform
                                       .Include(x => x.Branch)
                                       .Include(x => x.LeaseType)
                                       .Include(x => x.Locality)
                                       .Include(x => x.PropertyType)
                                       .Include(x => x.Zone)
                                       .Include(x => x.ApprovedStatusNavigation)
                                       .Where(x => x.IsActive == 1
                                       && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                       && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                       && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                       && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                       && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                       && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                       )
                                       .GetPaged<Kycform>(model.PageNumber, model.PageSize);




                int SortOrder = (int)model.SortOrder;
                if (SortOrder == 1)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("NAME"):
                            data = null;
                            data = await _dbContext.Kycform
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.LeaseType)
                                                   .Include(x => x.Locality)
                                                   .Include(x => x.PropertyType)
                                                   .Include(x => x.Zone)
                                                   .Include(x => x.ApprovedStatusNavigation)
                                                   .Where(x => x.IsActive == 1
                                                    && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                                   && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                   && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                                   && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                                   && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                                   && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                                   ).OrderBy(x => x.Property)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;
                        case ("STATUS"):
                            data = null;
                            data = await _dbContext.Kycform
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.LeaseType)
                                                   .Include(x => x.Locality)
                                                   .Include(x => x.PropertyType)
                                                   .Include(x => x.Zone)
                                                   .Include(x => x.ApprovedStatusNavigation)
                                                   .Where(x => x.IsActive == 1 
                                                    && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                                   && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                   && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                                   && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                                   && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                                   && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                                   ).OrderByDescending(x => x.ApprovedStatus)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;

                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("NAME"):
                            data = null;
                            data = await _dbContext.Kycform
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.LeaseType)
                                                   .Include(x => x.Locality)
                                                   .Include(x => x.PropertyType)
                                                   .Include(x => x.Zone)
                                                   .Include(x => x.ApprovedStatusNavigation)
                                                   .Where(x => x.IsActive == 1
                                                    && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                                   && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                   && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                                   && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                                   && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                                   && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                                   ).OrderByDescending(x => x.Property)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;
                        case ("STATUS"):
                            data = null;
                            data = await _dbContext.Kycform
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.LeaseType)
                                                   .Include(x => x.Locality)
                                                   .Include(x => x.PropertyType)
                                                   .Include(x => x.Zone)
                                                   .Include(x => x.ApprovedStatusNavigation)
                                                   .Where(x => x.IsActive == 1
                                                    && (string.IsNullOrEmpty(model.Id) || x.Id.ToString().Contains(model.Id))
                                                   && (string.IsNullOrEmpty(model.property) || x.Property.Contains(model.property))
                                                   && (string.IsNullOrEmpty(model.Fileno) || x.FileNo.Contains(model.Fileno))
                                                   && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                                   && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                                   && (string.IsNullOrEmpty(model.PlotNo) || x.PlotNo.Contains(model.PlotNo))
                                                   ).OrderBy(x => x.ApprovedStatus)
                                                   .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                            break;
                    }
                }
                return data;





            }
            catch (Exception ex)
            {
                throw;

            }
        }

        //********* rpt ! Kycleasepaymentrpt Details **********
        public async Task<bool> Saveleasepayment(Kycleasepaymentrpt payment)
        {
            _dbContext.Kycleasepaymentrpt.Add(payment);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        //********* rpt ! Kyclicensepaymentrpt Details **********
        public async Task<bool> Savelicensepayment(Kyclicensepaymentrpt payment)
        {
            _dbContext.Kyclicensepaymentrpt.Add(payment);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //KYC Approval process methods : Added by ishu 20/7/2021
        public async Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid)
       
        {
            return await _dbContext.Kycworkflowtemplate
                                    .Where(x => x.ProcessGuid == processguid && x.EffectiveDate <= DateTime.Now
                                    && x.IsActive == 1
                                    )
                                    .OrderByDescending(x => x.Id)
                                    .Take(1)
                                    .FirstOrDefaultAsync();
        }
        public async Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid)
        {
            return await _dbContext.Kycworkflowtemplate
                                     .Where(x => x.ProcessGuid == processguid
                                     && x.IsActive == 1
                                     )
                                     .OrderByDescending(x => x.Id)
                                     .ToListAsync();
        }
        public async Task<bool> CreatekycApproval(Kycapprovalproccess kycapproval)
        {
            _dbContext.Kycapprovalproccess.Add(kycapproval);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


        public async Task<List<KycFormApprovalDetailsSearchDto>> GetKycFormApprovalDetails(int Id, string ApprovalType)
        {
            try
            {
                

                var data = await _dbContext.LoadStoredProcedure("GetKycFormApprovalDetailsForChart")
                                         .WithSqlParams(("User_Id",Id),("ApprovalType", ApprovalType)
                                            )
                                            .ExecuteStoredProcedureAsync<KycFormApprovalDetailsSearchDto>();

                return (List<KycFormApprovalDetailsSearchDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<KycFormDemandPaymentApprovalSearchDto>> GetKycFromDemandPaymantApproval(int Id, string ApprovalType)
        {
            try
            {

                var data = await _dbContext.LoadStoredProcedure("GetKycDemandPaymentFormApprovalDetailsForChart")
                                         .WithSqlParams(("User_Id", Id), ("ApprovalType", ApprovalType)
                                            )
                                            .ExecuteStoredProcedureAsync<KycFormDemandPaymentApprovalSearchDto>();

                return (List<KycFormDemandPaymentApprovalSearchDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
