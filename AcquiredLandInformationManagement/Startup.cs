using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Demolition.Common;
//using Demolition.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using BotDetect.Web;
using Newtonsoft.Json.Serialization;
//using DDAPropertyREG.Models;
using Microsoft.Extensions.Hosting;
using AcquiredLandInformationManagement.Infrastructure.Extensions;
using Libraries.Model;
namespace AcquiredLandInformationManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<DataContext>(a => a.UseMySQL(Configuration.GetSection("ConnectionString:Con").Value));

            //services.AddDbContext<lmsContext>(a => a.UseMySQL(Configuration.GetSection("ConnectionString:Con").Value));
            //  services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            // services.AddMvc()
            //.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddSession();
            services.AddMvc();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddMvc().AddViewOptions(options =>
            {
                //   options.SuppressTempDataAttributePrefix = true;
            });
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.Name = "MyTempDataCookie";
            });
            // services.AddScoped<ILogger, Logger>();
            // Add Session services.
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
            });
            services.RegisterDependency();
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

            app.UseAuthorization();
            app.UseSession();
            // app.UseMvc();
            app.UseCookiePolicy();
            app.UseCaptcha(Configuration);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Create}/{id?}");
            });
        }
    }
}
