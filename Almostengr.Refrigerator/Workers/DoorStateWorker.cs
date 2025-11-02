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
                SystemSettingModel sensorSetting = await GetSystemSettingAsync(SystemSettingOption.DoorSensorGpio);
                if (sensorSetting.IntValue() == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(15));
                    continue;
                }

                PinValue state = ReadInput(sensorSetting.IntValue());
                if (state == PinValue.High)
                {
                    FridgeStateModel.IsDoorOpen = true;
                    if (FridgeStateModel.DoorLastOpened == null)
                    {
                        FridgeStateModel.DoorLastOpened = DateTime.Now;
                    }
                }
                else
                {
                    FridgeStateModel.IsDoorOpen = false;
                    FridgeStateModel.DoorLastOpened = null;
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
