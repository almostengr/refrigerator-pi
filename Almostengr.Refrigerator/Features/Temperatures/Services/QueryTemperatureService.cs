using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.Temperatures.Services.interfaces;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Features.Temperatures.Services;

public sealed class QueryTemperatureService : IQueryTemperatureService
{
    private readonly ApplicationDbContext _dbContext;

    public QueryTemperatureService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<TemperatureModel>> GetListAsync()
    {
        return await _dbContext.Temperatures
            .OrderByDescending(t => t.Id)
            .ToListAsync();
    }

    public async Task<IList<TemperatureModel>> GetListByDateRangeAsync(int range)
    {
        return await _dbContext.Temperatures
            .Where(t => t.ModifiedDate <= DateTime.Now.AddDays(-range))
            .ToListAsync();
    }
}
