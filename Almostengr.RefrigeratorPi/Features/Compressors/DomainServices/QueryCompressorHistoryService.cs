using Almostengr.RefrigeratorPi.Features.Common.Shared;
using Almostengr.RefrigeratorPi.Features.Compressors.Domain;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Interfaces;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Resources;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.RefrigeratorPi.Features.Compressors.DomainServices;

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
