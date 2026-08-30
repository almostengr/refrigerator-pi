
using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;
using Almostengr.RefrigeratorPi.Features.SystemSettings.Domain;

namespace Almostengr.RefrigeratorPi.Models;

public sealed class SystemSettingViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Value is required."), StringLength(150)]
    public string Value { get; set; }

    public SystemSettingOption SettingOption => (SystemSettingOption)Id;

    public string ModifiedBy { get; internal set; }

    internal void AssignToEntity(SystemSettingEntity entity, string modifiedBy)
    {
        if (entity == null)
        {
            return;
        }

        
    }
}