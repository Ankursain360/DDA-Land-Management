using AutoMapper;
using Service.AutoMapperProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Common
{
    public static class AutoMapperConfiguration
    {
		public static MapperConfiguration RegisterMappings()
		{
			return new MapperConfiguration(config =>
			{
				config.AddProfile(new MasterMappingProfile());	
			});
		}
	}
}
