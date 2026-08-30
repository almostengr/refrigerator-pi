using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.RefrigeratorPi.Features.Compressors.Domain;

public class CompressorHistory
{
    public CompressorHistory(string modifiedBy)
    {
        StartedBy = modifiedBy;
        StartDate = DateTime.Now;
    }

    [Required, Key]
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required, StringLength(100)]
    public string StartedBy { get; set; }

    [StringLength(100)]
    public string EndedBy { get; set; }

    public static Result<CompressorHistory> Create(string modifiedBy)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifiedBy, nameof(modifiedBy));

        CompressorHistory history = new(modifiedBy);
        return Result<CompressorHistory>.Success(history);
    }

    public Result<CompressorHistory> Update(string modifiedBy)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifiedBy, nameof(modifiedBy));

        EndDate = DateTime.Now;
        EndedBy = modifiedBy;
        return Result<CompressorHistory>.Success(this);
    }
}
