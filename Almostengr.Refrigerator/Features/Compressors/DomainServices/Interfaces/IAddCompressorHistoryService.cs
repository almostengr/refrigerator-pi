using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Resources;

namespace Almostengr.Refrigerator.Features.Compressors.DomainServices.Interfaces;

public interface IAddCompressorHistoryService
{
    Task<Result<CompressorHistoryResource>> ExecuteAsync(string modifiedBy);
}