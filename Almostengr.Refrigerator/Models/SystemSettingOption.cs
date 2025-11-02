using System.ComponentModel;

namespace Almostengr.Refrigerator.Models;

public enum SystemSettingOption
{
    [Description("Minimum Temperature (C)")]
    MinimumTemperatureC = 1,

    [Description("Maximum Temperature (C)")]
    MaximumTemperatureC = 2,

    [Description("Door Alarm Delay Seconds")]
    DoorAlarmMinutes = 3,

    [Description("Compressor GPIO")]
    CompressorGpio = 5,

    [Description("Door Sensor GPIO")]
    DoorSensorGpio = 6,

    [Description("Door Alarm GPIO")]
    DoorAlarmGpio = 7,

    [Description("Defrost Duration Minutes")]
    DefrostMinutes = 8,

    [Description("Defrost GPIO")]
    DefrostGpio = 9,
}
