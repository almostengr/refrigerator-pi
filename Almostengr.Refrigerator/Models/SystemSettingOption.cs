using System.ComponentModel;

namespace Almostengr.Refrigerator.Models;

public enum SystemSettingOption
{
    [Description("Minimum Temperature (C)")]
    MinimumTemperatureC = 1,

    [Description("Maximum Temperature (C)")]
    MaximumTemperatureC = 2,

    [Description("Door Alarm Delay Seconds")]
    DoorAlarmSeconds = 3,

    [Description("Defrost Duration Minutes")]
    DefrostMinutes = 8,
}
