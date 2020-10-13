using AutoMapper;
using Dto.Master;
using Libraries.Model.Entity;
using Model.Entity;
using System;
using System.Collections.Generic;

namespace Service.AutoMapperProfile
{
    public  class MasterMappingProfile : Profile
    {
        public MasterMappingProfile()
        {
            CreateMap<Country, CoutryDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Zone, ZoneDto>().ReverseMap();
            CreateMap<ApplicationRole, RoleDto>().ReverseMap();        
        }
    }
}
