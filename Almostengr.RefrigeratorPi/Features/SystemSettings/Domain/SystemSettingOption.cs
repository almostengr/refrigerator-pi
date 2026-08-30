using System.ComponentModel;

namespace Almostengr.RefrigeratorPi.Models;

public enum SystemSettingOption
{
    [Description("Minimum Temperature (C)")]
    MinimumTemperatureC = 1,

    [Description("Maximum Temperature (C)")]
    MaximumTemperatureC = 2,

    [Description("Door Alarm Delay Seconds")]
    DoorAlarmSeconds = 3,

    [Description("Retain Temperature Reading Days")]
    TemperatureReadingDays = 4,

    [Description("Defrost Duration Minutes")]
    DefrostMinutes = 8,
}
