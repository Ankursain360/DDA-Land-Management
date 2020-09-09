using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Common
{
    public static class AddAutoMapper
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            IMapper mapper = AutoMapperConfiguration.RegisterMappings().CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
