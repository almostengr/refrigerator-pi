using Almostengr.Refrigerator.Features.SystemSettings.Services;
using Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;

namespace Almostengr.Refrigerator.Features.SystemSettings.DomainServices.Interfaces;

public static class SystemSettingExtensions
{
    public static void AddSystemSettingServices(this IServiceCollection services)
    {
        services.AddTransient<IQuerySystemSettingService, QuerySystemSettingService>();
        services.AddTransient<IUpdateSystemSettingService, UpdateSystemSettingService>();
    }
}