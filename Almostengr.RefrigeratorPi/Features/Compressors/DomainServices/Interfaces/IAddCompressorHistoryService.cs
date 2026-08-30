using Almostengr.Common.DomainServices.Results;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Resources;

namespace Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Interfaces;

public interface IAddCompressorHistoryService
{
    Task<Result<CompressorHistoryResource>> ExecuteAsync(string modifiedBy);
}