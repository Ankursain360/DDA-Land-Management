using AutoMapper;
using Dto.Master;
using Libraries.Model.Entity;
using Model.Entity;

namespace Service.AutoMapperProfile
{
    public  class MasterMappingProfile : Profile
    {
        public MasterMappingProfile()
        {
            CreateMap<Country, CoutryDto>().ReverseMap();
            CreateMap<Village, VillageDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<District, DistrictDto>().ReverseMap();
            CreateMap<Zone, ZoneDto>().ReverseMap();
            CreateMap<ApplicationRole, RoleDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Userprofile, UserProfileDto>();
            CreateMap<Gisdata, GISKhasraDto>();
        }
    }
}
