using Almostengr.RefrigeratorPi.Features.Temperatures.Services.interfaces;

namespace Almostengr.RefrigeratorPi.Features.Temperatures.Services;

internal static class TemperatureExtensions
{
    public static void AddTemperatureServices(this IServiceCollection services)
    {
        services.AddTransient<IAddTemperatureService, AddTemperatureService>();
        services.AddTransient<IQueryTemperatureService, QueryTemperatureService>();
    }
}