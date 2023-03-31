using MedicalTest2.Data;
using MedicalTest2.Helpers;
using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
using MedicalTest2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = string.Empty;
                options.User.RequireUniqueEmail = true;
            });
            services.AddOptions();
            services.AddScoped<IStoreRepo<Employee>, EmployeeRepository>();
            services.AddScoped<IStoreRepo<Nationality>, NationalityRepository>();
            services.AddScoped<IStoreRepo<Destination>, DestinationRepository>();
            services.AddScoped<IStoreRepo<Department>, DepartmentRepository>();
            services.AddScoped<IStoreRepo<ActualWork>, ActualWorkRepository>();
            services.AddScoped<IStoreRepo<JobCategory>, JobCategoryRepository>();
            services.AddScoped<IStoreRepo<Gender>, GenderRepository>();
            services.AddScoped<IStoreRepo<WorkType>, WorkTypeRepository>();
            services.AddScoped<IStoreRepo<Attachment>, AttachmentRepository>();
            services.AddScoped<IStoreRepo<EmployeeAttachment>, EmployeeAttachmentRepository>();
            services.AddScoped<IReportService, ReportService>();
            //services.AddScoped<IStoreRepo<Category>, CategorytRepository>();
            //services.AddScoped<IStoreRepo<EmployeeCategory>, EmployeeCategoryRepository>();
            services.AddMvc();

            services.AddIdentity<MyUser, IdentityRole>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI().AddDefaultTokenProviders();
            services.Configure<TwilioSettings>(Configuration.GetSection("Twilio"));
            services.AddTransient<ISMSService, SMSService>();
            services.AddPaging(options => {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
                options.HtmlIndicatorDown = " <span>&darr;</span>";
                options.HtmlIndicatorUp = " <span>&uarr;</span>";
            });            //services.AddMvc(config =>
                           //{
                           //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                           //    config.Filters.Add(new AuthorizeFilter(policy));

            //});
            //services.AddDbContext<BookStoreDbContext>(op => op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
