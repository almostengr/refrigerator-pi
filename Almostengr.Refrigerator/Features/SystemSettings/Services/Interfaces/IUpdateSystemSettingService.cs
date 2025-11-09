using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;

namespace Almostengr.Refrigerator.Features.SystemSettings.DomainServices;

public interface IUpdateSystemSettingService
{
    Task<Result<SystemSettingEntity>> ExecuteAsync(SystemSettingEntity resource);
}