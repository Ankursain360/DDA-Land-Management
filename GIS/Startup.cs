using GIS.Filters;
using GIS.Infrastructure.Extensions;
using Libraries.Model;
using Libraries.Model.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Model.Entity;
using Service.Common;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.CookiePolicy;


namespace GIS
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

        // This method gets called by the runtime. Use this method to add services to the container.
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
                options.HttpOnly = HttpOnlyPolicy.Always;
               // options.Secure = CookieSecurePolicy.Always;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt32(Configuration.GetSection("CookiesSettings:CookiesTimeout").Value));
                options.Cookie.HttpOnly = true;
                //options.Cookie.Domain = (Configuration.GetSection("CookiesSettings:CookiesDomain").Value).ToString();
                //options.Cookie.Path = "/Home";
                //options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<DataContext>(a => a.UseMySQL(Configuration.GetSection("ConnectionString:Con").Value, y => y.CommandTimeout(1000)));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(ExceptionLogFilter));
               // option.Filters.Add(typeof(AuditFilterAttribute));
            });

            services.RegisterDependency();
            services.AddAutoMapperSetup();
            //Response Compression
            services.Configure<GzipCompressionProviderOptions>(options =>
            options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
            //end of  Response Compression
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie("Cookies")
           //.AddCookie("Cookies", options =>
           //{
           //    options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(Configuration.GetSection("CookiesSettings:CookiesTimeout").Value));
           //    options.SlidingExpiration = true;
           //    options.Cookie.Name = "Auth-cookie";
           //})
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
               //options.UseTokenLifetime = true;
               //options.Events.OnRedirectToIdentityProvider = context => // <- HERE
               //{                                                        // <- HERE
               //    context.ProtocolMessage.Prompt = "login";            // <- HERE
               //    return Task.CompletedTask;                           // <- HERE
               //};                                                       // <- HERE
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
            //Response Compression
            app.UseResponseCompression();
            //
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            //prevent session hijacking
          //  app.preventSessionHijacking();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
            });

        }
    }
}
