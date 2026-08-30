using Almostengr.Common.DomainServices.Results;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Resources;

namespace Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Interfaces;

internal interface IUpdateCompressorHistoryService
{
    Task<Result<CompressorHistoryResource>> ExecuteAsync(CompressorHistoryResource resource);
    Task<Result<CompressorHistoryResource>> ExecuteAsync(string modifiedBy);
}