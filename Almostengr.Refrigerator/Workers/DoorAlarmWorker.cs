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
                SystemSettingModel alarmSetting = await GetSystemSettingAsync(SystemSettingOption.DoorAlarmGpio);
                if (alarmSetting.IntValue() == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(15));
                    continue;
                }

                SystemSettingModel timeoutSetting = await GetSystemSettingAsync(SystemSettingOption.DoorAlarmMinutes);
                
                if (FridgeStateModel.IsDoorOpen && (FridgeStateModel.DoorLastOpened - DateTime.Now) >= TimeSpan.FromMinutes(timeoutSetting.IntValue()))
                {
                    WriteOutput(alarmSetting.IntValue(), PinValue.High);
                }
                else
                {
                    WriteOutput(alarmSetting.IntValue(), PinValue.Low);
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