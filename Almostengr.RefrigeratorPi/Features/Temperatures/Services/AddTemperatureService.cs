using Almostengr.Common.DomainServices.Results;
using Almostengr.RefrigeratorPi.Features.Common.Shared;
using Almostengr.RefrigeratorPi.Features.Temperatures.Services.interfaces;
using Almostengr.RefrigeratorPi.Models;

namespace Almostengr.RefrigeratorPi.Features.Temperatures.Services;

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

            Result<TemperatureModel> result = TemperatureModel.Create(resource.ReadingC, CommonConstants.SYSTEM_USER);
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
            _logger.LogError(ex, CommonConstants.LOG_MESSAGE, ex.Message);
            return Result<TemperatureModel>.Failure(ex.Message);
        }
    }
}
