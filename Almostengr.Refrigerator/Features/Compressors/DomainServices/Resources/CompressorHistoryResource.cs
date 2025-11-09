
namespace Almostengr.Refrigerator.Features.Compressors.DomainServices.Resources;

public class CompressorHistoryResource
{
    public string StartedBy { get; set; }
    public string EndedBy { get; set; }
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan? Duration { get; set; }
}