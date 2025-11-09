using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Features.Temperatures.Services.interfaces;

public interface IQueryTemperatureService
{
    Task<IList<TemperatureModel>> GetListAsync();
    Task<IList<TemperatureModel>> GetListByDateRangeAsync(int range);
}