using Almostengr.RefrigeratorPi.Models;

namespace Almostengr.RefrigeratorPi.Features.Temperatures.Services.interfaces;

public interface IQueryTemperatureService
{
    Task<IList<TemperatureModel>> GetListAsync();
    Task<IList<TemperatureModel>> GetListByDateRangeAsync(int range);
    Task<TemperatureModel> GetLatestAsync();
}