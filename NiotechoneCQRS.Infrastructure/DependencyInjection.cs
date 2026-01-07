using Microsoft.Extensions.DependencyInjection;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Repositories;

namespace NiotechoneCQRS.Infrastructure;

public static class DependencyInjection
{
    public static void ServiceConfigure(this IServiceCollection services)
    {
        services.ApplicationServiceConfigure();
    }

    private static void ApplicationServiceConfigure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IConfigManagerRepository, ConfigManagerRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
    }
}

