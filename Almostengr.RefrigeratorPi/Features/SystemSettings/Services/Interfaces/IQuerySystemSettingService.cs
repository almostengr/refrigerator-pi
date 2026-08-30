using Almostengr.RefrigeratorPi.Features.SystemSettings.Domain;
using Almostengr.RefrigeratorPi.Models;

namespace Almostengr.RefrigeratorPi.Features.SystemSettings.Services.Interfaces;

public interface IQuerySystemSettingService
{
    Task<SystemSettingEntity> GetEntityByIdAsync(int id);
    Task<SystemSettingEntity> GetEntityByOptionAsync(SystemSettingOption option);
    Task<IList<SystemSettingEntity>> GetListAsync();
}
