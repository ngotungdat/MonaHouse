using Sample.Interface.Repository;
using Sample.Interface.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sample.Service.Repository;
using Sample.Interface.UnitOfWork;
using Sample.Service;
using Sample.Service.Services.Auth;
using Sample.Interface.Services;
using Sample.Service.Services;
using Sample.Interface.Services.Catalogue;
using Sample.Service.Services.Catalogue;
using Sample.Interface.Services.Configuration;
using Sample.Service.Services.Configurations;
using Sample.Interface.DbContext;

namespace Sample.BaseAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, Sample.AppDbContext.AppDbContext>();
            services.AddScoped(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            services.AddScoped(typeof(ICatalogueRepository<>), typeof(CatalogueRepository<>));
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
        }

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddLocalization(o => { o.ResourcesPath = "Resources"; });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                CultureInfo[] supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("he")
                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddTransient<ITokenManagerService, TokenManagerService>();

            #region Authenticate

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserInGroupService, UserInGroupService>();
            services.AddScoped<IUserGroupService, UserGroupService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPermitObjectService, PermitObjectService>();
            services.AddScoped<IPermitObjectPermissionService, PermitObjectPermissionService>();

            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IVehicleFeeConfigService, VehicleFeeConfigService>();
            services.AddScoped<IUtilitiesConfigService, UtilitiesConfigService>();
            services.AddScoped<ICustomerResourceService, CustomerResourceService>();
            services.AddScoped<IFeedBackTypeService, FeedBackTypeService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationUserService, NotificationUserService>();

            #endregion

            #region Catalogue

            services.AddScoped<IWardService, WardService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<INationService, NationService>();
            // by BaoNguyen-License
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<ILicenseSampleService, LicenseSampleService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IFeedbackCommentService, FeedbackCommentService>();

            #endregion

            #region Configuration

            services.AddScoped<IEmailConfigurationService, EmailConfigurationService>();
            services.AddScoped<ISMSConfigurationService, SMSConfigurationService>();
            services.AddScoped<ISMSEmailTemplateService, SMSEmailTemplateService>();
            services.AddScoped<IOTPHistoryService, OTPHistoryService>();


            #endregion
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SAMPLE API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });

                var dir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
                foreach (var fi in dir.EnumerateFiles("*.xml"))
                {
                    c.IncludeXmlComments(fi.FullName);
                }

                c.EnableAnnotations();
            });
        }

    }
}
