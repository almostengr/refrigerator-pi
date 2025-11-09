using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.Compressors.Domain;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Interfaces;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Resources;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Features.Compressors.DomainServices;

internal sealed class UpdateCompressorHistoryService : IUpdateCompressorHistoryService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateCompressorHistoryService> _logger;

    public UpdateCompressorHistoryService(
        ApplicationDbContext dbContext,
        ILogger<UpdateCompressorHistoryService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<CompressorHistoryResource>> ExecuteAsync(CompressorHistoryResource resource)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            CompressorHistory entity = await _dbContext.CompressorHistories.Where(h => h.Id == resource.Id).SingleOrDefaultAsync();
            if (entity == null)
            {
                return Result<CompressorHistoryResource>.Failure("Not found.");
            }

            Result<CompressorHistory> result = entity.Update(resource.EndedBy);
            if (result.Failed)
            {
                return Result<CompressorHistoryResource>.Failure(result.Errors);
            }

            _dbContext.Update(result.Value);
            await _dbContext.SaveChangesAsync();

            return Result<CompressorHistoryResource>.Success(resource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, CommonExtensions.LOG_MESSAGE, ex.Message);
            return Result<CompressorHistoryResource>.Failure(ex.Message);
        }
    }

    public async Task<Result<CompressorHistoryResource>> ExecuteAsync(string modifiedBy)
    {
        CompressorHistoryResource resource = new()
        {
            EndedBy = modifiedBy
        };
        return await ExecuteAsync(resource);
    }
}
