using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Features.SystemSettings.DomainServices;
using Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;

namespace Almostengr.Refrigerator.Features.SystemSettings.Services;

public sealed class UpdateSystemSettingService : IUpdateSystemSettingService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateSystemSettingService> _logger;
    private readonly IQuerySystemSettingService _querySystemSettingService;

    public UpdateSystemSettingService(
        ApplicationDbContext dbContext,
        ILogger<UpdateSystemSettingService> logger,
        IQuerySystemSettingService querySystemSettingService
        )
    {
        _dbContext = dbContext;
        _logger = logger;
        _querySystemSettingService = querySystemSettingService;
    }

    public async Task<Result<SystemSettingEntity>> ExecuteAsync(SystemSettingEntity resource)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource);

            SystemSettingEntity entity = await _querySystemSettingService.GetEntityByIdAsync(resource.Id);
            if (entity == null)
            {
                return Result<SystemSettingEntity>.Failure("Not found.");
            }

            Result<SystemSettingEntity> result = entity.Update(resource.Value, resource.ModifiedBy);
            if (result.Failed)
            {
                return Result<SystemSettingEntity>.Failure(result.Errors);
            }

            _dbContext.SystemSettings.Update(result.Value);
            await _dbContext.SaveChangesAsync();

            return Result<SystemSettingEntity>.Success(result.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, CommonExtensions.LOG_MESSAGE, ex.Message);
            return Result<SystemSettingEntity>.Failure(ex.Message);
        }
    }
}
