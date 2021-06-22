using emsiproject.WEB.APIClasses;
using emsiproject.WEB.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace emsiproject.WEB
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
            string EveryApplyAPIBaseAddress = Configuration.GetSection("EmsiAPI")["BaseAddress"];
            services.AddHttpClient<emsiproject_API>()
               .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
               {
                   UseDefaultCredentials = true,
                   ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
               })
               .ConfigureHttpClient(option =>
               {
                   option.BaseAddress = new Uri(EveryApplyAPIBaseAddress);
               })
               .SetHandlerLifetime(TimeSpan.FromMinutes(5));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromDays(2);
                options.Cookie.IsEssential = true;
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
           
            app.UseStaticFiles();
            app.UseSession();
            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
