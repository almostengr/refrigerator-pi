using Almostengr.Refrigerator.Features.Temperatures.Services.interfaces;

namespace Almostengr.Refrigerator.Features.Temperatures.Services;

internal static class TemperatureExtensions
{
    public static void AddTemperatureServices(this IServiceCollection services)
    {
        services.AddTransient<IAddTemperatureService, AddTemperatureService>();
        services.AddTransient<IQueryTemperatureService, QueryTemperatureService>();
    }
}