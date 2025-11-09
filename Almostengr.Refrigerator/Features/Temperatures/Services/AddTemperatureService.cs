using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.Temperatures.Services.interfaces;
using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Features.Temperatures.Services;

public sealed class AddTemperatureService : IAddTemperatureService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<AddTemperatureService> _logger;

    public AddTemperatureService(
        ApplicationDbContext dbContext,
        ILogger<AddTemperatureService> logger
        )
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<TemperatureModel>> ExecuteAsync(TemperatureModel resource)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource);

            Result<TemperatureModel> result = TemperatureModel.Create(resource.ReadingC, CommonExtensions.SYSTEM_USER);
            if (result.Failed)
            {
                return Result<TemperatureModel>.Failure(result.Errors);
            }

            await _dbContext.Temperatures.AddAsync(result.Value);
            await _dbContext.SaveChangesAsync();

            return Result<TemperatureModel>.Success(resource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, CommonExtensions.LOG_MESSAGE, ex.Message);
            return Result<TemperatureModel>.Failure(ex.Message);
        }
    }
}
