using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Libraries.Model;
using SiteMaster.Infrastructure.Extensions;
using System.IdentityModel.Tokens.Jwt;
using SiteMaster.Filters;
using Service.Common;
using Libraries.Model.Entity;
using Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace SiteMaster
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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<DataContext>(a => a.UseMySQL(Configuration.GetSection("ConnectionString:Con").Value));
            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
            })
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
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            services.RegisterDependency();
            services.AddAutoMapperSetup();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              
            })
             .AddCookie("Cookies", options =>
             {
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                 options.SlidingExpiration = false;
                 options.Cookie.Name = "Auth-cookie";
                 options.Cookie.SameSite = SameSiteMode.None;
                 // options.Cookie.Path = "/Home";
                 options.Events = new CookieAuthenticationEvents
                 {
                     OnRedirectToLogin = redirectContext =>
                     {
                         redirectContext.HttpContext.Response.StatusCode = 401;
                         return Task.CompletedTask;
                     }
                 };
             })
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
                options.UseTokenLifetime = true;
                options.Events.OnRedirectToIdentityProvider = context => // <- HERE
                {                                                        // <- HERE
                    context.ProtocolMessage.Prompt = "login";            // <- HERE
                    return Task.CompletedTask;                           // <- HERE
                };                                                       // <- HERE
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
            app.UseSession();
            //prevent session hijacking
            app.preventSessionHijacking();
            //
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
            });
        }
    }
}