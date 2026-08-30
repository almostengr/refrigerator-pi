using Almostengr.Common.DomainServices.Results;
using Almostengr.RefrigeratorPi.Features.Common.Shared;
using Almostengr.RefrigeratorPi.Features.Compressors.Domain;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Interfaces;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Resources;

namespace Almostengr.RefrigeratorPi.Features.Compressors.DomainServices;

internal sealed class AddCompressorHistoryService : IAddCompressorHistoryService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<AddCompressorHistoryService> _logger;
    private readonly IQueryCompressorHistoryService _queryService;

    public AddCompressorHistoryService(
        ApplicationDbContext dbContext,
        ILogger<AddCompressorHistoryService> logger,
        IQueryCompressorHistoryService queryService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _queryService = queryService;
    }

    public async Task<Result<CompressorHistoryResource>> ExecuteAsync(CompressorHistoryResource resource)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            Result<CompressorHistory> result = CompressorHistory.Create(resource.StartedBy);
            if (result.Failed)
            {
                return Result<CompressorHistoryResource>.Failure(result.Errors);
            }

            await _dbContext.CompressorHistories.AddAsync(result.Value);
            await _dbContext.SaveChangesAsync();

            resource = _queryService.ToResource(result.Value);
            return Result<CompressorHistoryResource>.Success(resource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, CommonConstants.LOG_MESSAGE, ex.Message);
            return Result<CompressorHistoryResource>.Failure(ex.Message);
        }
    }
    
    public async Task<Result<CompressorHistoryResource>> ExecuteAsync(string modifiedBy)
    {
        CompressorHistoryResource resource = new()
        {
            StartedBy = modifiedBy,
        };

        return await ExecuteAsync(resource);
    }
}
