using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Refrigerator.Models;

public sealed class SystemSettingModel
{
    [Required, Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Value is required."), StringLength(150)]
    public string Value { get; set; }

    [StringLength(100)]
    public string ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }
    public SystemSettingOption SettingOption => (SystemSettingOption)Id;

    internal Result<SystemSettingModel> AssignToEntity(SystemSettingModel entity, string modifiedBy)
    {
        Result<SystemSettingModel> result = Result<SystemSettingModel>.Create();

        if (SettingOption == SystemSettingOption.CompressorGpio && (entity.IntValue() < 1 || entity.IntValue() > 30))
        {
            result.AddError("Invalid GPIO number.");
        }

        if (SettingOption == SystemSettingOption.DoorSensorGpio && (entity.IntValue() < 0 || entity.IntValue() > 30))
        {
            result.AddError("Invalid GPIO number.");
        }

        if (SettingOption == SystemSettingOption.DoorAlarmGpio && (entity.IntValue() < 0 || entity.IntValue() > 30))
        {
            result.AddError("Invalid GPIO number.");
        }

        if (SettingOption == SystemSettingOption.DefrostGpio && (entity.IntValue() < 0 || entity.IntValue() > 30))
        {
            result.AddError("Invalid GPIO number.");
        }

        if (SettingOption == SystemSettingOption.DefrostMinutes && entity.IntValue() < 0)
        {
            result.AddError("Value must be greater than zero (0).");
        }

        if (result.Succeeded)
        {
            entity.Value = Value;
            entity.ModifiedBy = modifiedBy;
            entity.ModifiedDate = DateTime.Now;
            result.SetValue(entity);
        }

        return result;
    }

    public int IntValue()
    {
        return int.TryParse(Value, out int returnValue) ? returnValue : default;
    }

    public decimal DecimalValue()
    {
        return decimal.TryParse(Value, out decimal returnValue) ? returnValue : default;
    }
}