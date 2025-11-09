using Almostengr.Refrigerator.Features.Compressors.Domain;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Resources;

namespace Almostengr.Refrigerator.Features.Compressors.DomainServices.Interfaces;

public interface IQueryCompressorHistoryService
{
    Task<IList<CompressorHistory>> GetListAsync();
    Task<CompressorHistory> GetLatestAsync();
    CompressorHistoryResource ToResource(CompressorHistory entity);
}