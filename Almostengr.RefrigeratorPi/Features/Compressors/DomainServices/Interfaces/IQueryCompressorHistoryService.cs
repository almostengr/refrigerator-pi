using Almostengr.RefrigeratorPi.Features.Compressors.Domain;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Resources;

namespace Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Interfaces;

public interface IQueryCompressorHistoryService
{
    Task<IList<CompressorHistory>> GetListAsync();
    Task<CompressorHistory> GetLatestAsync();
    CompressorHistoryResource ToResource(CompressorHistory entity);
}