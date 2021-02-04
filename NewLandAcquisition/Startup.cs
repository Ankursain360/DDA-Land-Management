using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Libraries.Model;
using NewLandAcquisition.Infrastructure.Extensions;
using System.IdentityModel.Tokens.Jwt;
using NewLandAcquisition.Filters;
using Libraries.Model.Entity;
using Model.Entity;
using Microsoft.AspNetCore.Identity;
using Service.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace NewLandAcquisition
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (HostEnvironment.IsDevelopment())
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            }
            else
            {
                services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            }

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<DataContext>(a => a.UseMySQL(Configuration.GetSection("ConnectionString:Con").Value));

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<DataContext>()
               .AddDefaultTokenProviders();

            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(ExceptionLogFilter));
            });

            services.RegisterDependency();
            services.AddAutoMapperSetup();



//#if DEBUG
//            if (HostEnvironment.IsDevelopment())
//            {
//                services.AddControllersWithViews().AddRazorRuntimeCompilation();
//            }
//            else
//            {
//                services.AddControllersWithViews();
//            }
//#endif

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = Configuration.GetSection("AuthSetting:Authority").Value;
                options.RequireHttpsMetadata = Convert.ToBoolean(Configuration.GetSection("AuthSetting:RequireHttpsMetadata").Value);
                options.ClientId = "mvc";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.Scope.Add("api1");
                options.SaveTokens = true;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
            });
        }
    }
}