using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Features.SystemSettings.Services;

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
