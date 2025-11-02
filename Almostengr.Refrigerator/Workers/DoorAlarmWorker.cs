using System.Device.Gpio;
using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Workers;

internal sealed class DoorAlarmWorker : BaseWorker<DoorAlarmWorker>
{
    public DoorAlarmWorker(
        ApplicationDbContext dbContext,
        ILogger<DoorAlarmWorker> logger) : base(dbContext, logger)
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                SystemSettingModel timeoutSetting = await GetSystemSettingAsync(SystemSettingOption.DoorAlarmSeconds);
                if (timeoutSetting.IntValue() == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(60));
                    continue;
                }
                
                if (FridgeStateModel.IsDoorOpen && (FridgeStateModel.DoorLastOpened - DateTime.Now) >= TimeSpan.FromMinutes(timeoutSetting.IntValue()))
                {
                    WriteOutput(GpioPinOption.DoorAlarm, PinValue.High);
                }
                else
                {
                    WriteOutput(GpioPinOption.DoorAlarm, PinValue.Low);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}