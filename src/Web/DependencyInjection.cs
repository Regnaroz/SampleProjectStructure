using Azure.Identity;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Infrastructure.Data;
using SampleProject.Web.Services;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Web.Mapping.Config;
using SampleProject.Web.HangFireFiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Hangfire;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "SampleProject API";
            //define naming of method in nswag generated client
            configure.OperationProcessors.Add(new FlattenOperationsProcessor());

        });

        services.AddMapping();

        //HangFire settings
        services.AddHostedService<RequeryingJobService>();
        //  services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //.AddCookie(options =>
        //{
        //    options.Cookie.Name = "GPS.Job.Cookie";
        //    options.LoginPath = "/Home/Index";
        //    options.AccessDeniedPath = new PathString("/");
        //    options.Cookie.HttpOnly = true;
        //    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        //    options.ExpireTimeSpan = TimeSpan.FromHours(8);
        //    options.SlidingExpiration = true;
        //});
        services.AddHangfire(s => s.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfireServer(s =>
            {
                s.WorkerCount = 1;
            }
            );

        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keyVaultUri = configuration["KeyVaultUri"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }
}
