using AutoMapper;
using Dto.Master;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.AutoMapperProfile
{
    public  class MasterMappingProfile : Profile
    {
        public MasterMappingProfile()
        {
            CreateMap<Country, CoutryDto>().ReverseMap();
        }
    }
}
