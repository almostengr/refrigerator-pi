using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Interfaces;

namespace Almostengr.RefrigeratorPi.Features.Compressors.Shared;

internal static class CompressorHistoryExtensions
{
    public static void AddCompressorHistoryServices(this IServiceCollection services)
    {
        services.AddTransient<IAddCompressorHistoryService, AddCompressorHistoryService>();
        services.AddTransient<IQueryCompressorHistoryService, QueryCompressorHistoryService>();
        services.AddTransient<IUpdateCompressorHistoryService, UpdateCompressorHistoryService>();
    }
}
