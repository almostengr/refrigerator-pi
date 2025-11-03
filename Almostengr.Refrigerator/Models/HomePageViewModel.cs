
namespace Almostengr.Refrigerator.Models;

internal class HomePageViewModel
{
    public HomePageViewModel(List<TemperatureModel> temperature, bool isCompressorRunning)
    {
        IsCompressorRunning = isCompressorRunning;
        Temperatures = temperature;
    }

    public decimal ReadingC { get; private set; }
    public bool IsCompressorRunning { get; private set; }
    public List<TemperatureModel> Temperatures { get; private set; }
}