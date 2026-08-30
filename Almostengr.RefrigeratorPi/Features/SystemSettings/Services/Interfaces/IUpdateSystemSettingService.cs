using Almostengr.Common.DomainServices.Results;
using Almostengr.RefrigeratorPi.Features.SystemSettings.Domain;

namespace Almostengr.RefrigeratorPi.Features.SystemSettings.DomainServices;

public interface IUpdateSystemSettingService
{
    Task<Result<SystemSettingEntity>> ExecuteAsync(SystemSettingEntity resource);
}