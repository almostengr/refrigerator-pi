using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.Compressors.Domain;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Interfaces;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Resources;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Features.Compressors.DomainServices;

internal sealed class QueryCompressorHistoryService : IQueryCompressorHistoryService
{
    private readonly ApplicationDbContext _dbContext;

    public QueryCompressorHistoryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CompressorHistory> GetLatestAsync()
    {
        return await _dbContext.CompressorHistories
            .OrderByDescending(h => h.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<IList<CompressorHistory>> GetListAsync()
    {
        return await _dbContext.CompressorHistories
            .OrderByDescending(h => h.Id)
            .ToListAsync();
    }

    public CompressorHistoryResource ToResource(CompressorHistory entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new CompressorHistoryResource
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            StartedBy = entity.StartedBy,
            EndedBy = entity.EndedBy,
            EndDate = entity.EndDate,
            Duration = entity.EndDate.HasValue ? entity.EndDate.Value - entity.StartDate  : null,
        };
    }
}
