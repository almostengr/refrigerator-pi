namespace Almostengr.Refrigerator.Models;

public static class FridgeStateModel
{
    public static bool IsDoorOpen { get; set; } = false;
    public static bool IsDoorClosed => !IsDoorOpen;
    public static DateTime? DoorLastOpened = null;
    public static DateTime? LastDefrosted = DateTime.Now;
}