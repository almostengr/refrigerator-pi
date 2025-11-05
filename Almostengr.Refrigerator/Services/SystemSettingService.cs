using Almostengr.Refrigerator.Models;
using Almostengr.Refrigerator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Services;

internal sealed class SystemSettingService : ISystemSettingService
{
    private readonly ApplicationDbContext _dbContext;

    public SystemSettingService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SystemSettingModel> GetEntityByIdAsync(int id)
    {
        return await _dbContext.SystemSettings.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<SystemSettingModel> GetEntityByOptionAsync(SystemSettingOption option)
    {
        return await GetEntityByIdAsync((int)option);
    }
}
