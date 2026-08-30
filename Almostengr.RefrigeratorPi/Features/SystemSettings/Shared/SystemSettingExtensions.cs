using Almostengr.RefrigeratorPi.Features.SystemSettings.Services;
using Almostengr.RefrigeratorPi.Features.SystemSettings.Services.Interfaces;

namespace Almostengr.RefrigeratorPi.Features.SystemSettings.DomainServices.Interfaces;

public static class SystemSettingExtensions
{
    public static void AddSystemSettingServices(this IServiceCollection services)
    {
        services.AddTransient<IQuerySystemSettingService, QuerySystemSettingService>();
        services.AddTransient<IUpdateSystemSettingService, UpdateSystemSettingService>();
    }
}