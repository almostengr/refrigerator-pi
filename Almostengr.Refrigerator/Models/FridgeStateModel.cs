namespace Almostengr.Refrigerator.Models;

public static class FridgeStateModel
{
    public static bool IsDoorOpen { get; set; } = false;
    public static DateTime? DoorLastOpened = null;
    public static bool IsCompressorRunning { get; set; } = false;
}