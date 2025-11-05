using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Services.Interfaces;

public interface ISystemSettingService
{
    Task<SystemSettingModel> GetEntityByIdAsync(int id);
    Task<SystemSettingModel> GetEntityByOptionAsync(SystemSettingOption option);
}