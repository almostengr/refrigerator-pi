using System.Device.Gpio;
using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Workers;

internal sealed class DoorStateWorker : BaseWorker<DoorStateWorker>
{
    public DoorStateWorker(
        ApplicationDbContext dbContext,
        ILogger<DoorStateWorker> logger
        ) : base(dbContext, logger)
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        FridgeStateModel.IsDoorOpen = false;

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                PinValue state = ReadInput(GpioPinOption.DoorSensor);
                if (state == PinValue.High)
                {
                    FridgeStateModel.IsDoorOpen = true;
                    WriteOutput(GpioPinOption.Light, PinValue.High);

                    if (FridgeStateModel.DoorLastOpened == null)
                    {
                        FridgeStateModel.DoorLastOpened = DateTime.Now;
                    }
                }
                else
                {
                    FridgeStateModel.IsDoorOpen = false;
                    FridgeStateModel.DoorLastOpened = null;
                    WriteOutput(GpioPinOption.Light, PinValue.Low);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
