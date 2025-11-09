using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;

public interface IQuerySystemSettingService
{
    Task<SystemSettingEntity> GetEntityByIdAsync(int id);
    Task<SystemSettingEntity> GetEntityByOptionAsync(SystemSettingOption option);
    Task<IList<SystemSettingEntity>> GetListAsync();
}
