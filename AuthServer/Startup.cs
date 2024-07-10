using AuthServer.Data;
using AuthServer.Filters;
using AuthServer.Infrastructure.Extensions;
using AuthServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Services;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using Service.Common;
using Libraries.Model;
namespace AuthServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            string connectionString = (Configuration.GetSection("ConnectionString:connectionString").Value).ToString();
            string lmsConnection = (Configuration.GetSection("ConnectionString:lmsConnection").Value).ToString();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(lmsConnection));
            if (HostEnvironment.IsProduction())
            {
                services.Configure<CookiePolicyOptions>(options =>
                                                    {
                                                        options.CheckConsentNeeded = context => false;
                                                       // options.MinimumSameSitePolicy = SameSiteMode.Lax;
                                                        options.HttpOnly = HttpOnlyPolicy.Always;
                                                        options.Secure = CookieSecurePolicy.Always;
                                                    });

            }
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = (3);
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(Configuration.GetSection("CookiesSettings:DefaultLockoutTimeSpan").Value));
                options.Password.RequiredLength = 7;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;                
            }).AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
          opt.TokenLifespan = TimeSpan.FromHours(2));
            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(ExceptionLogFilter));
               
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt32(Configuration.GetSection("CookiesSettings:CookiesTimeout").Value));
                options.Cookie.HttpOnly = true;
                options.Cookie.Domain = HostEnvironment.IsProduction() ? (Configuration.GetSection("CookiesSettings:CookiesDomain").Value).ToString() : "localhost";
                // options.Cookie.Path = Configuration.GetSection("CookiesSettings:CookiesPath").Value.ToString();
                options.Cookie.IsEssential = true;

            });
            services.AddDbContext<DataContext>(a => a.UseMySQL(Configuration.GetSection("ConnectionString:lmsConnection").Value));

            services.RegisterDependency();
            services.AddAutoMapperSetup();
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
                options.Authentication.CookieLifetime = TimeSpan.FromMinutes(20);
                options.Authentication.CookieSlidingExpiration = true;
                options.EmitStaticAudienceClaim = true;
                options.AccessTokenJwtType = "JWT";

            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseMySQL(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseMySQL(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<ApplicationUser>();

            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseRouting();
            if (env.IsProduction())
            {
                app.UseCookiePolicy(new CookiePolicyOptions
                {
                    HttpOnly = HttpOnlyPolicy.Always,
                    Secure = CookieSecurePolicy.Always,
                    MinimumSameSitePolicy = SameSiteMode.Lax
                });
            }
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
