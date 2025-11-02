using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Refrigerator.Models;

public sealed class TemperatureModel
{
    public int Id { get; set; }
    public decimal ReadingC { get; set; }

    [Required, StringLength(150)]
    public string ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public static Result<TemperatureModel> Create(decimal readingC, string modifiedBy)
    {
        if (string.IsNullOrWhiteSpace(modifiedBy))
        {
            throw new ArgumentNullException(modifiedBy, nameof(modifiedBy));
        }

        Result<TemperatureModel> result = Result<TemperatureModel>.Create();

        if (result.Succeeded)
        {
            TemperatureModel temperature = new();
            temperature.ReadingC = readingC;
            temperature.ModifiedBy = modifiedBy;
            temperature.ModifiedDate = DateTime.Now;
            result.SetValue(temperature);
        }

        return result;
    }
}