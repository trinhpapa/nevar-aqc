using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NEVAR_AQC.Business.Logic.Managerments;
using NEVAR_AQC.Business.Logic.Notification;
using NEVAR_AQC.Business.Logic.ReceptionDepartment;
using NEVAR_AQC.Business.Logic.Statistic;
using NEVAR_AQC.Business.Logic.SystemLog;
using NEVAR_AQC.Business.Logic.TestDepartment;
using NEVAR_AQC.Business.Logic.User;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Business.ReceptionDepartment;
using NEVAR_AQC.Business.Statistic;
using NEVAR_AQC.Business.SystemLog;
using NEVAR_AQC.Business.TestDepartment;
using NEVAR_AQC.Business.User;
using NEVAR_AQC.Core.Hubs;
using NEVAR_AQC.Data.EF;
using NEVAR_AQC.Data.EF.Repositories.Managements;
using NEVAR_AQC.Data.EF.Repositories.ReceptionDepartment;
using NEVAR_AQC.Data.EF.Repositories.Statistic;
using NEVAR_AQC.Data.EF.Repositories.SystemLog;
using NEVAR_AQC.Data.EF.Repositories.TestDepartment;
using NEVAR_AQC.Data.EF.Repositories.User;
using NEVAR_AQC.Data.Managements;
using NEVAR_AQC.Data.ReceptionDepartment;
using NEVAR_AQC.Data.Statistic;
using NEVAR_AQC.Data.SystemLog;
using NEVAR_AQC.Data.TestDepartment;
using NEVAR_AQC.Data.User;
using NEVAR_AQC.Mapper;
using NEVAR_AQC.Service.Facade.Managements;
using NEVAR_AQC.Service.Facade.ReceptionDepartment;
using NEVAR_AQC.Service.Facade.Report;
using NEVAR_AQC.Service.Facade.Statistic;
using NEVAR_AQC.Service.Facade.SystemLog;
using NEVAR_AQC.Service.Facade.TestDepartment;
using NEVAR_AQC.Service.Facade.User;
using NEVAR_AQC.Service.Managements;
using NEVAR_AQC.Service.ReceptionDepartment;
using NEVAR_AQC.Service.Report;
using NEVAR_AQC.Service.Statistic;
using NEVAR_AQC.Service.SystemLog;
using NEVAR_AQC.Service.TestDepartment;
using NEVAR_AQC.Service.User;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace NEVAR_AQC
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      public void ConfigureServices(IServiceCollection services)
      {
         services.Configure<CookiePolicyOptions>(options =>
         {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => false;
            options.MinimumSameSitePolicy = SameSiteMode.None;
         });
         services.AddSession(options =>
         {
            options.IdleTimeout = TimeSpan.FromHours(4);
            options.Cookie.HttpOnly = true;
         });
         services.Configure<RequestLocalizationOptions>(options =>
         {
            options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-GB");
            options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-GB"), new CultureInfo("en-GB") };
         });
         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
               options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
               options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

         services.AddSignalR();
         services.AddAutoMapper(typeof(AutoMapperConfiguration));
         services.AddDbContext<NEVARDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NEVARConnection")));
         services.AddDbContext<NEVARLogDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NEVARLogConnection")));

         services.AddTransient<DbInitializer>();
         services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

         //Dependency Injection
         services.AddNotificationService();

         services.AddTransient<ILOGHandleRepository, LOGHandleRepository>();
         services.AddTransient<ILOGLoginRepository, LOGLoginRepository>();
         services.AddTransient<ILOGLoginBusiness, LOGLoginBusiness>();
         services.AddTransient<ILOGLoginService, LOGLoginService>();

         services.AddTransient<IHomeStatisticRepository, HomeStatisticRepository>();
         services.AddTransient<ISYSUserRepository, CTGUserRepository>();
         services.AddTransient<ISYSCustomerRepository, SYSCustomerRepository>();
         services.AddTransient<ICTGRequirementProcessStatusRepository, CTGRequirementProcessStatusRepository>();
         services.AddTransient<ICTGRequirementTypeRepository, CTGRequirementTypeRepository>();
         services.AddTransient<ICTGSystemFunctionRepository, CTGSystemFunctionRepository>();
         services.AddTransient<ICTGReturnInvoiceResultTypeRepository, CTGReturnInvoiceResultTypeRepository>();
         services.AddTransient<ICTGDepartmentRepository, CTGDepartmentRepository>();
         services.AddTransient<ICTGFieldRepository, CTGFieldRepository>();
         services.AddTransient<ICTGTestMethodRepository, CTGTestMethodRepository>();
         services.AddTransient<ICTGTestPropertyRepository, CTGTestPropertyRepository>();
         services.AddTransient<ICTGTestObjectRepository, CTGTestObjectRepository>();
         services.AddTransient<ICTGCustomerTypeRepository, CTGCustomerTypeRepository>();
         services.AddTransient<ISYSRequirementInvoiceRepository, SYSRequirementInvoiceRepository>();
         services.AddTransient<IIDTestRequirementRepository, IDTestRequirementRepository>();
         services.AddTransient<ICTGRoleRepository, CTGRoleRepository>();
         services.AddTransient<ISYSRoleFunctionrepository, SYSRoleFunctionRepository>();
         services.AddTransient<IIDTRImplementerRepository, IDTRImplementerRepository>();
         services.AddTransient<IIDTRTestPropertyRepository, IDTRTestPropertyRepository>();

         services.AddTransient<IHomeStatisticBusiness, HomeStatisticBusiness>();
         services.AddTransient<ISYSUserBusiness, SYSUserBusiness>();
         services.AddTransient<ISYSCustomerBusiness, SYSCustomerBusiness>();
         services.AddTransient<ICTGSystemFunctionBusiness, CTGSystemFunctionBusiness>();
         services.AddTransient<ICTGDepartmentBusiness, CTGDepartmentBusiness>();
         services.AddTransient<ICTGRequirementTypeBusiness, CTGRequirementTypeBusiness>();
         services.AddTransient<ICTGTestObjectBusiness, CTGTestObjectBusiness>();
         services.AddTransient<ICTGTestMethodBusiness, CTGTestMethodBusiness>();
         services.AddTransient<ICTGTestPropertyBusiness, CTGTestPropertyBusiness>();
         services.AddTransient<ICTGFieldBusiness, CTGFieldBusiness>();
         services.AddTransient<ISYSRequirementInvoiceBusiness, SYSRequirementInvoiceBusiness>();
         services.AddTransient<IIDTestRequirementBusiness, IDTestRequirementBusiness>();
         services.AddTransient<ICTGRoleBusiness, CTGRoleBusiness>();
         services.AddTransient<ISYSRoleFunctionBusiness, SYSRoleFunctionBusiness>();
         services.AddTransient<ICTGReturnInvoiceResultTypeBusiness, CTGReturnInvoiceResultTypeBusiness>();
         services.AddTransient<IIDTRImplementerBusiness, IDTRImplementerBusiness>();
         services.AddTransient<IIDTRTestPropertyBusiness, IDTRTestPropertyBusiness>();

         services.AddTransient<IHomeStatisticService, HomeStatisticService>();
         services.AddTransient<ISYSUserService, SYSUserService>();
         services.AddTransient<ISYSCustomerService, SYSCustomerService>();
         services.AddTransient<ISYSRequirementInvoiceService, SYSRequirementInvoiceService>();
         services.AddTransient<ICTGRequirementTypeService, CTGRequirementTypeService>();
         services.AddTransient<ICTGDepartmentService, CTGDepartmentService>();
         services.AddTransient<ICTGFieldService, CTGFieldService>();
         services.AddTransient<ICTGRoleService, CTGRoleService>();
         services.AddTransient<ICTGTestObjectService, CTGTestObjectService>();
         services.AddTransient<ICTGTestMethodService, CTGTestMethodService>();
         services.AddTransient<ICTGTestPropertyService, CTGTestPropertyService>();
         services.AddTransient<ICTGReturnInvoiceResultTypeService, CTGReturnInvoiceResultTypeService>();
         services.AddTransient<ICTGSystemFunctionService, CTGSystemFunctionService>();
         services.AddTransient<ITestRequirementReportService, TestRequirementReportService>();
         services.AddTransient<IImplementationPlanService, ImplementationPlanService>();
         services.AddTransient<ITestPlanService, TestPlanService>();
         services.AddTransient<ITestProcessReportService, TestProcessReportService>();
         services.AddTransient<ITestResultReportService, TestResultReportService>();
      }

      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
      {
         loggerFactory.AddSerilog();
         app.UseSignalR(hubs =>
         {
            hubs.MapHub<SystemHub>("/systemHub");
         });
         app.UseStaticFiles();
         app.UseCookiePolicy();
         app.UseSession();
         app.UseRequestLocalization();
         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "Login",
                   template: "login",
                   defaults: new { controller = "Login", action = "Index" });

            routes.MapRoute(
                   name: "Home",
                   template: "home",
                   defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                   name: "Access Denied",
                   template: "home/access-denied",
                   defaults: new { controller = "Home", action = "AccessDenied" });

            routes.MapRoute(
                   name: "Reception Department",
                   template: "reception-department",
                   defaults: new { controller = "ReceptionDepartment", action = "Index" });

            routes.MapRoute(
                   name: "Test Department",
                   template: "test-department",
                   defaults: new { controller = "TestDepartment", action = "Index" });

            routes.MapRoute(
                   name: "Test Department Manager",
                   template: "test-department/manager",
                   defaults: new { controller = "TestDepartment", action = "Manager" });

            routes.MapRoute(
                   name: "Test Department Engineer",
                   template: "test-department/engineer",
                   defaults: new { controller = "TestDepartment", action = "Engineer" });

            routes.MapRoute(
                   name: "User Management",
                   template: "system-management/user",
                   defaults: new { controller = "User", action = "Index" });

            routes.MapRoute(
                   name: "Customer Management",
                   template: "system-management/customer",
                   defaults: new { controller = "Customer", action = "Index" });

            routes.MapRoute(
                   name: "Role Management",
                   template: "system-management/role",
                   defaults: new { controller = "Role", action = "Index" });

            routes.MapRoute(
                   name: "Field Management",
                   template: "system-management/field",
                   defaults: new { controller = "Field", action = "Index" });
            routes.MapRoute(
                   name: "Test Object Management",
                   template: "system-management/test-object",
                   defaults: new { controller = "TestObject", action = "Index" });

            routes.MapRoute(
                   name: "Test Method Management",
                   template: "system-management/test-method",
                   defaults: new { controller = "TestMethod", action = "Index" });

            routes.MapRoute(
                   name: "Test Property Management",
                   template: "system-management/test-property",
                   defaults: new { controller = "TestProperty", action = "Index" });

            routes.MapRoute(
                   name: "Statistic",
                   template: "statistic",
                   defaults: new { controller = "Statistic", action = "Index" });

            routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
         });
      }
   }
}