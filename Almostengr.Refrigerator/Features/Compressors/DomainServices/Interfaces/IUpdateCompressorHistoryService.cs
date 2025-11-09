using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Resources;

namespace Almostengr.Refrigerator.Features.Compressors.DomainServices.Interfaces;

internal interface IUpdateCompressorHistoryService
{
    Task<Result<CompressorHistoryResource>> ExecuteAsync(CompressorHistoryResource resource);
    Task<Result<CompressorHistoryResource>> ExecuteAsync(string modifiedBy);
}