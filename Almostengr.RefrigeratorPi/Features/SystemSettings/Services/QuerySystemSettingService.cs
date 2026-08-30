using Almostengr.RefrigeratorPi.Features.Common.Shared;
using Almostengr.RefrigeratorPi.Features.SystemSettings.Domain;
using Almostengr.RefrigeratorPi.Features.SystemSettings.Services.Interfaces;
using Almostengr.RefrigeratorPi.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.RefrigeratorPi.Features.SystemSettings.Services;

internal sealed class QuerySystemSettingService : IQuerySystemSettingService
{
    private readonly ApplicationDbContext _dbContext;

    public QuerySystemSettingService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SystemSettingEntity> GetEntityByIdAsync(int id)
    {
        return await _dbContext.SystemSettings.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<SystemSettingEntity> GetEntityByOptionAsync(SystemSettingOption option)
    {
        return await GetEntityByIdAsync((int)option);
    }

    public async Task<IList<SystemSettingEntity>> GetListAsync()
    {
        return await _dbContext.SystemSettings.OrderBy(s => s.Id).ToListAsync();
    }
}
